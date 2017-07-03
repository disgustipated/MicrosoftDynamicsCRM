using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Samples;


namespace MicrosoftDynamicsCRMPlugin
{
  public class LoginMgr
  {
    private AuthenticationMethods authenticationMethod;
    private string organization;
    private string server;
    private int port;
    private bool useDefaultCredentials;
    private string userName;
    private string password;
    private string domain;
    private bool useSecureConnection;
    private WindowsLiveLocations windowsLiveLocation;

    public LoginMgr(AuthenticationMethods authenticationMethod, string organization, string server, int port, bool useDefaultCredentials, string userName, string password, string domain, bool useSecureConnection, WindowsLiveLocations windowsLiveLocation)
    {
      this.authenticationMethod = authenticationMethod;
      this.organization = organization;
      this.server = server;
      this.port = port;
      this.useDefaultCredentials = useDefaultCredentials;
      this.userName = userName;
      this.password = password;
      this.domain = domain;
      this.useSecureConnection = useSecureConnection;
      this.windowsLiveLocation = windowsLiveLocation;
    }

    public OrganizationServiceProxy Login()
    {
      ServerConnection serverConnection = new ServerConnection();
      ServerConnection.Configuration config = serverConnection.GetServerConfiguration(authenticationMethod, organization, server, port, useDefaultCredentials, userName, password, domain, useSecureConnection, windowsLiveLocation);
      return ServerConnection.GetOrganizationProxy(config);
    }
  }
}
