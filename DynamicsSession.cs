using CRMPluginUtils;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MicrosoftDynamicsCRMPlugin
{
  public class DynamicsSession
  {
    private ConfigurationManager configurationManager = new ConfigurationManager("3CXCRMUser.ini");

    private LoginMgr loginMgr;
    private OrganizationServiceProxy serviceProxy = null;
    private Guid currentUserId = Guid.Empty;
    private bool isActive = false;

    private string createUrl(string id, ContactTypes contactType)
    {
      string currentServiceEndpointUri = serviceProxy.ServiceManagement.CurrentServiceEndpoint.Address.Uri.AbsoluteUri;
      int xrmServicesIndex = currentServiceEndpointUri.IndexOf("/XRMServices", StringComparison.InvariantCultureIgnoreCase);
      string url = xrmServicesIndex < 0 ? currentServiceEndpointUri : currentServiceEndpointUri.Substring(0, xrmServicesIndex);
      switch (contactType)
      {
        case ContactTypes.Account:
          url += String.Format("/main.aspx?etn=account&id={{{0}}}&pagetype=entityrecord", id);
          break;
        case ContactTypes.Contact:
          url += String.Format("/main.aspx?etn=contact&id={{{0}}}&pagetype=entityrecord", id);
          break;
        case ContactTypes.Lead:
          url += String.Format("/main.aspx?etn=lead&id={{{0}}}&pagetype=entityrecord", id);
          break;
      }
      return url;
    }

    private void launchUrl(string url)
    {
      if (configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseDefaultBrowser", "True") == "True")
        Process.Start(url);
      else
      {
        string customBrowserPath = configurationManager.GetValue("Microsoft Dynamics Plug-in", "CustomBrowser", "");
        if (String.IsNullOrEmpty(customBrowserPath))
          throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.CustomBrowserIsEmpty"));

        Process.Start(customBrowserPath, url);
      }
    }

    private void connectivityTest()
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      // Perform a dummy request
      WhoAmIResponse whoResp = (WhoAmIResponse)serviceProxy.Execute(new WhoAmIRequest());
      currentUserId = whoResp.UserId;
    }

    private ContactInfo createNewContactRecord(string contactNumber)
    {
      try
      {
        if (configurationManager.GetValue("Microsoft Dynamics Plug-in", "CreateNewRecordType", "Contact") == "Contact")
        {
          Microsoft.Crm.Sdk.Contact contactItem = new Microsoft.Crm.Sdk.Contact();
          contactItem.Telephone1 = contactNumber;
          contactItem.Description = "Contact created with 3cx plugin";
          Guid newContactId = serviceProxy.Create(contactItem);
          launchUrl(createUrl(newContactId.ToString(), ContactTypes.Contact));
          return new ContactInfo(newContactId.ToString(), contactNumber, ContactTypes.Contact);
        }
        else
        {
          Microsoft.Crm.Sdk.Lead leadItem = new Microsoft.Crm.Sdk.Lead();
          leadItem.Telephone1 = contactNumber;
          Guid newLeadId = serviceProxy.Create(leadItem);
          launchUrl(createUrl(newLeadId.ToString(), ContactTypes.Lead));
          return new ContactInfo(newLeadId.ToString(), contactNumber, ContactTypes.Lead);
        }
      }
      catch (System.ServiceModel.Security.ExpiredSecurityTokenException)
      {
        throw;
      }
      catch (Exception exc)
      {
        throw new ApplicationException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.CreatingContact"), ErrorHelper.GetErrorDescription(exc)));
      }
    }

    public DynamicsSession()
    {
		     var ver = GetVersion();
            if(!string.IsNullOrEmpty(ver))
                LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Plugin Version: " +  ver);

    }
	
	public string GetVersion()
	{
	  try
	  {
			var path = AppDomain.CurrentDomain.BaseDirectory + @"DotNetScripts\MicrosoftDynamicsCRM\VERSION";
			LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", path);
			return System.IO.File.ReadAllText(path);
		}
	  catch (Exception e)
	  {
			LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "VERSION not found");
	  }

		return string.Empty;
	}


    public bool IsActive
    {
      get { return isActive; }
    }

    public void Login(LoginMgr loginMgr)
    {
      this.loginMgr = loginMgr;
      this.serviceProxy = loginMgr.Login();
      this.serviceProxy.EnableProxyTypes();
      this.isActive = true;
      connectivityTest();
    }
    
    public void Logout()
    {
      if (serviceProxy != null)
      {
        serviceProxy.Dispose();
        serviceProxy = null;
        isActive = false;
      }
    }
    
    public string GetContactNumberByContactId(string phoneType, string contactId)
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      ContactFinder contactFinder = new ContactFinder(serviceProxy);
      ContactInfo contactInfo = contactFinder.GetContactInformationByContactId(phoneType, contactId);
      return contactInfo != null ? contactInfo.Number : null;
    }

    public string GetContactNumberByLeadId(string phoneType, string leadId)
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      ContactFinder contactFinder = new ContactFinder(serviceProxy);
      ContactInfo contactInfo = contactFinder.GetContactInformationByLeadId(phoneType, leadId);
      return contactInfo != null ? contactInfo.Number : null;
    }

    public string GetContactNumberByAccountId(string phoneType, string accountId)
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      ContactFinder contactFinder = new ContactFinder(serviceProxy);
      ContactInfo contactInfo = contactFinder.GetContactInformationByAccountId(phoneType, accountId);
      return contactInfo != null ? contactInfo.Number : null;
    }

    public ContactInfo ShowContactRecord(string contactNumber, bool createIfNotFound)
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      ContactFinder contactFinder = new ContactFinder(serviceProxy);
      ContactInfo contactInfo = contactFinder.GetContactInformation(contactNumber, 4);
      if (contactInfo == null) contactInfo = contactFinder.GetContactInformation(contactNumber, 2);

      if (contactInfo == null)
      {
        if (createIfNotFound)
          return createNewContactRecord(contactNumber);
        else
          throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.ContactNotFound"));
      }
      else
      {
        launchUrl(createUrl(contactInfo.Id, contactInfo.Type));
        return contactInfo;
      }
    }
    
    public void StoreCallInformation(CallInformation callInformation)
    {
      if (serviceProxy == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.NotLoggedIn"));

      ContactInfo contactInfo = callInformation.ContactInfo;
      ContactFinder contactFinder = new ContactFinder(serviceProxy);
      if (contactInfo == null) contactInfo = contactFinder.GetContactInformation(callInformation.ContactNumber, 4);
      if (contactInfo == null) contactInfo = contactFinder.GetContactInformation(callInformation.ContactNumber, 2);

      if (contactInfo == null)
        throw new ApplicationException(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.ContactNotFound"));
      
      try
      {
        Microsoft.Crm.Sdk.PhoneCall pc = new Microsoft.Crm.Sdk.PhoneCall();
        pc.ActualEnd = callInformation.End;
        pc.ActualStart = callInformation.Start;
        pc.ActualDurationMinutes = callInformation.CallState == CallStates.Answered ? Convert.ToInt32((callInformation.End - callInformation.Start).TotalMinutes) : 0;
        pc.DirectionCode = callInformation.CallType != CallTypes.Inbound;
        pc.PhoneNumber = callInformation.ContactNumber;
        pc.Description = ("Call created with 3CX integration");

        if (callInformation.CallType == CallTypes.Inbound)
        {
          if (callInformation.CallState == CallStates.Answered)
            pc.Subject = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Activities.InboundCallText"), callInformation.ContactNumber);
          else
            pc.Subject = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Activities.MissedInboundCallText"), callInformation.ContactNumber);
        }
        else
        {
          if (callInformation.CallState == CallStates.Answered)
            pc.Subject = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Activities.OutboundCallText"), callInformation.ContactNumber);
          else
            pc.Subject = String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Activities.NotAnweredOutboundCallText"), callInformation.ContactNumber);
        }

        EntityReference entityReference;
        switch (contactInfo.Type)
        {
          case ContactTypes.Account:
            entityReference = new EntityReference { Id = Guid.Parse(contactInfo.Id), LogicalName = Microsoft.Crm.Sdk.Account.EntityLogicalName };
            break;
          case ContactTypes.Contact:
            entityReference = new EntityReference { Id = Guid.Parse(contactInfo.Id), LogicalName = Microsoft.Crm.Sdk.Contact.EntityLogicalName };
            break;
          case ContactTypes.Lead:
            entityReference = new EntityReference { Id = Guid.Parse(contactInfo.Id), LogicalName = Microsoft.Crm.Sdk.Lead.EntityLogicalName };
            break;
          default:
            throw new ApplicationException("Invalid ContactType while storing call information: " + contactInfo.Type);
        }

        pc.RegardingObjectId = entityReference;
        ActivityParty contactActivityParty = new ActivityParty { PartyId = new EntityReference(entityReference.LogicalName, entityReference.Id) };
        ActivityParty currentUserActivityParty = new ActivityParty { PartyId = new EntityReference(SystemUser.EntityLogicalName, currentUserId) };
        if (callInformation.CallType == CallTypes.Inbound)
        {
          pc.From = new List<ActivityParty>() { contactActivityParty };
          if (currentUserId != Guid.Empty) pc.To = new List<ActivityParty>() { currentUserActivityParty };
        }
        else
        {
          if (currentUserId != Guid.Empty) pc.From = new List<ActivityParty>() { currentUserActivityParty };
          pc.To = new List<ActivityParty>() { contactActivityParty };
        }
        
        pc.Id = serviceProxy.Create(pc);
        serviceProxy.Execute(new SetStateRequest
        {
          EntityMoniker = pc.ToEntityReference(),
          State = new OptionSetValue((int)Microsoft.Crm.Sdk.PhoneCallState.Open),
          Status = new OptionSetValue(1) // 1=open -> this is only valid for closed calls -> callInformation.CallType == CallTypes.Inbound ? 4 : 2
        });
      }
      catch (System.ServiceModel.Security.ExpiredSecurityTokenException)
      {
        throw;
      }
      catch (Exception exc)
      {
        throw new ApplicationException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsSession.Error.StoringCallInformation"), exc.ToString()));
      }
    }
  }
}
