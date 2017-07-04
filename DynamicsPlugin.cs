using CRMPluginUtils;
using MyPhonePlugins;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MicrosoftDynamicsCRMPlugin
{
  public class DynamicsPlugin : AbsCallNotifier, IDisposable
  {
    private static object lockObj = new object();

    private ConfigurationManager configurationManager = null;
    private ConfigurationManager serviceConfigurationManager = null;
    private DynamicsSession session = null;

    private string organization = String.Empty;
    private AuthenticationMethods authenticationMethod = AuthenticationMethods.IFD;
    private string server = String.Empty;
    private int port = 0;
    private bool useDefaultCredentials = false;
    private string userName = String.Empty;
    private string password = String.Empty;
    private string domain = String.Empty;
    private bool useSecureConnection = false;
    private WindowsLiveLocations windowsLiveLocation;

    private TcpClient tcpClient = null; // Used in Terminal Server mode
    private TcpServer tcpServer = null; // Used in non Terminal Server mode
    private System.Threading.Timer reconnectTimer = null;

    private bool onDataArrival(string data, bool hasToRespond, ref string response)
    {
      bool hasToExit = false;
      bool requested = false;
      bool faviconRequest = false;
      int tryCount = 2;
      while (!hasToExit && --tryCount >= 0)
      {
        try
        {
          string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
          foreach (string line in lines)
          {
            if (line.StartsWith("GET"))
            {
              string[] lineParts = line.Split(' ');
              if (lineParts.Length == 3)
              {
                string uri = lineParts[1];
                if (uri == "/favicon.ico")
                  faviconRequest = true;
                else
                {
                  string[] uriParts = uri.Split('&', '?');
                  if (uriParts.Length == 7)
                  {
                    string phoneType = uriParts[0].Substring(1);
                    string entityType = String.Empty;
                    string entityId = String.Empty;

                    foreach (string part in uriParts)
                    {
                      if (part.StartsWith("typename=") && part.Length > 9)
                        entityType = part.Substring(9);
                      else if (part.StartsWith("id=") && part.Length > 3)
                        entityId = Uri.UnescapeDataString(part.Substring(3));
                    }

                    if (!session.IsActive) doLogin();

                    string contactNumber = null;
                    ContactTypes contactType = ContactTypes.Contact;
                    switch (entityType)
                    {
                      case "account":
                        contactNumber = session.GetContactNumberByAccountId(phoneType, entityId);
                        contactType = ContactTypes.Account;
                        break;
                      case "contact":
                        contactNumber = session.GetContactNumberByContactId(phoneType, entityId);
                        contactType = ContactTypes.Contact;
                        break;
                      case "lead":
                        contactNumber = session.GetContactNumberByLeadId(phoneType, entityId);
                        contactType = ContactTypes.Lead;
                        break;
                      default:
                        throw new ApplicationException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidEntityType"), entityType));
                    }

                    if (String.IsNullOrEmpty(contactNumber))
                      throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.DestinationIsEmpty"));
                    else
                    {
                      response = "Call request in progress to destination '" + contactNumber + "'...";
                      LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Requesting outbound call to destination: " + contactNumber);
                      MakeCall(new ContactInfo(entityId, contactNumber, contactType));
                      requested = true;
                      hasToExit = true;
                    }
                  }
                  else
                    throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidRequestFormat"));
                }
              }
              else
                throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidRequestFormat"));
            }
          }

          if (!requested && !faviconRequest)
            LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Data received could not be processed, ignoring: " + data);
        }
        catch (System.ServiceModel.Security.ExpiredSecurityTokenException tokenExpiredExc)
        {
          response = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.ProcessingOutboundCall"), ErrorHelper.GetErrorDescription(tokenExpiredExc));
          LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", response);
          doLoginNoError();
        }
        catch (Exception exc)
        {
          response = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.ProcessingOutboundCall"), ErrorHelper.GetErrorDescription(exc));
          LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", response);
          if (!hasToRespond) MessageBox.Show(response, "3CX Microsoft Dynamics CRM Plug-in", MessageBoxButtons.OK, MessageBoxIcon.Error);
          hasToExit = true;
        }
      }

      return requested;
    }

    private void tcpServer_OnDataArrival(int index, string data)
    {
      string response = String.Empty;
      bool requested = onDataArrival(data, true, ref response);
      string htmlResponse = String.Format("<html><head><title>3CX Microsoft Dynamics CRM plug-in</title></head><body>{1}<div style=\"font-size: 10pt; color: darkblue; font-family: Tahoma\">{0}</div></body></html>", response, requested ? "<script>setTimeout('self.close();',5000);</script>" : "");
      DateTime utcNow = DateTime.Now.ToUniversalTime();
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("HTTP/1.1 200 OK");
      sb.AppendLine(String.Format("Content-Length: {0}", htmlResponse.Length));
      sb.AppendLine("Content-Type: text/html");
      sb.AppendLine("Cache-Control: no-cache");
      sb.AppendLine("Cache-Control: no-store");
      sb.AppendLine(String.Format("Last-Modified: {0}", utcNow.ToString("R")));
      sb.AppendLine("Accept-Ranges: bytes");
      sb.AppendLine(String.Format("Date: {0}", utcNow.ToString("R")));
      sb.AppendLine();
      sb.AppendLine(htmlResponse);

      tcpServer.Send(index, sb.ToString());
    }

    private void tcpClient_OnDataArrival(string data)
    {
      string response = String.Empty;
      onDataArrival(data, false, ref response);
    }

    private void tcpClient_OnDisconnect()
    {
      reconnectTimer = new System.Threading.Timer(new System.Threading.TimerCallback(connectTimerCallback), null, 10000, 10000);
    }

    private void connectTimerCallback(object state)
    {
      try
      {
        LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "3CX Outbound Call Listener is disconnected, trying to re-connect.");
        tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, Convert.ToInt32(serviceConfigurationManager.GetValue("3CX Outbound Call Listener", "ClientsPort", "47281"))));
        
        LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "3CX Outbound Call Listener successfully re-connected.");
        reconnectTimer.Dispose();
        reconnectTimer = null;
      }
      catch (Exception exc)
      {
        LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "3CX Outbound Call Listener could not be re-connected: " + ErrorHelper.GetErrorDescription(exc));
      }
    }

    private void doLoginNoError()
    {
      try
      {
        doLogin();
      }
      catch (Exception exc)
      {
        LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Error logging in: " + ErrorHelper.GetErrorDescription(exc));
      }
    }

    private void doLogin()
    {
      session.Logout();
      session.Login(new LoginMgr(authenticationMethod, organization, server, port, useDefaultCredentials, userName, password, domain, useSecureConnection, windowsLiveLocation));
    }

    public void OnMyPhoneStatusChanged(IExtensionInfo extensionInfo, MyPhoneStatus status)
    {
      onMyPhoneStatusChanged(extensionInfo, status);
    }

    public void OnCallStatusChanged(CallStatus callInfo)
    {
      onCallStatusChanged(null, callInfo);
    }

    public DynamicsPlugin(IMyPhoneCallHandler callHandler)
    : base("MicrosoftDynamicsCRM.log", callHandler)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
      this.configurationManager = new ConfigurationManager("3CXCRMUser.ini");
      this.serviceConfigurationManager = new ConfigurationManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "3CXPhone for Windows"), "3CX Outbound Call Listener.ini");
      this.session = new DynamicsSession();

      if (serviceConfigurationManager.GetValue("3CX Outbound Call Listener", "TerminalServerEnabled", "False") == "True")
      {
        this.tcpClient = new TcpClient();
        this.tcpClient.OnDataArrival += new AbsTcpClient.DataArrivalHandler(tcpClient_OnDataArrival);
        this.tcpClient.OnDisconnect += new AbsTcpClient.DisconnectHandler(tcpClient_OnDisconnect);
        tcpClient_OnDisconnect();
      }
      else
      {
        this.tcpServer = new TcpServer();
        this.tcpServer.OnDataArrival += new AbsTcpServer.DataArrivalHandler(tcpServer_OnDataArrival);
        this.tcpServer.Listen("127.0.0.1", Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "OutboundCallRequestsPort", "47280")));
      }

      ThreadPool.QueueUserWorkItem(new WaitCallback(reloadConfiguration));
    }

    public void Dispose()
    {
      if (tcpClient != null)
      {
        tcpClient.Dispose();
        tcpClient = null;
      }

      if (tcpServer != null)
      {
        tcpServer.Dispose();
        tcpServer = null;
      }

      if (reconnectTimer != null)
      {
        reconnectTimer.Dispose();
        reconnectTimer = null;
      }
    }

    protected override ContactInfo onShowContact(string contactNumber, bool createIfNotFound)
    {
      lock (lockObj)
      {
        reloadConfiguration(null);

        if (!session.IsActive) doLogin();

        int tryCount = 2;
        while (--tryCount >= 0)
        {
          try
          {
            return session.ShowContactRecord(contactNumber, createIfNotFound);
          }
          catch (System.ServiceModel.Security.ExpiredSecurityTokenException)
          {
            doLoginNoError();
          }
        }

        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.LoginSessionExpired"));
      }
    }

    protected override void onStoreCallInformation(CallInformation callInformation)
    {
      lock (lockObj)
      {
        if (!session.IsActive) doLogin();

        int tryCount = 2;
        while (--tryCount >= 0)
        {
          try
          {
            session.StoreCallInformation(callInformation);
            return;
          }
          catch (System.ServiceModel.Security.ExpiredSecurityTokenException)
          {
            doLoginNoError();
          }
        }

        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.LoginSessionExpired"));
      }
    }

    private void reloadConfiguration(object state)
    {
      lock (lockObj)
      {
        try
        {
          string newOrganization = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Organization", "");
          AuthenticationMethods newAuthenticationMethod = AuthenticationMethodHelper.FromString(configurationManager.GetValue("Microsoft Dynamics Plug-in", "AuthenticationMethod", "OnPremise"));
          string newServer = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Server", "localhost");
          String newPortStr = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Port", "80");
          int newPort = String.IsNullOrEmpty(newPortStr) ? 80 : Convert.ToInt32(newPortStr);
          bool newUseDefaultCredentials = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseDefaultCredentials", "True") == "True";
          string newUserName = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UserName", "");
          string newPassword = configurationManager.GetCryptedValue("Microsoft Dynamics Plug-in", "Password", "");
          string newDomain = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Domain", "");
          bool newUseSecureConnection = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseSecureConnection", "False") == "True";
          WindowsLiveLocations newWindowsLiveLocation = WindowsLiveLocationHelper.FromString(configurationManager.GetValue("Microsoft Dynamics Plug-in", "WindowsLiveLocation", "North America"));

          if (organization != newOrganization || authenticationMethod != newAuthenticationMethod ||
              server != newServer || port != newPort || useDefaultCredentials != newUseDefaultCredentials ||
              userName != newUserName || password != newPassword || domain != newDomain || useSecureConnection != newUseSecureConnection ||
              windowsLiveLocation != newWindowsLiveLocation)
          {
            organization = newOrganization;
            authenticationMethod = newAuthenticationMethod;
            server = newServer;
            port = newPort;
            useDefaultCredentials = newUseDefaultCredentials;
            userName = newUserName;
            password = newPassword;
            domain = newDomain;
            useSecureConnection = newUseSecureConnection;
            windowsLiveLocation = newWindowsLiveLocation;

            doLogin();
          }
        }
        catch (Exception exc)
        {
          LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Error loading configuration: " + ErrorHelper.GetErrorDescription(exc));
        }
      }
    }
  }
}
