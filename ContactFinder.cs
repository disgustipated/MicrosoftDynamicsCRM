using CRMPluginUtils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Threading;

namespace MicrosoftDynamicsCRMPlugin
{
  public class ContactFinder
  {
    private ConfigurationManager configurationManager = new ConfigurationManager("3CXCRMUser.ini");
    private OrganizationServiceProxy serviceProxy;
    
    private string contactNumber = String.Empty;
    
    private ContactInfo[] results = new ContactInfo[3];
    private AutoResetEvent[] waitHandles = new AutoResetEvent[3];
    
    
    private void asyncLookup(object state)
    {
      AbsLookupInfo lookupInfo = state as AbsLookupInfo;
      try
      {
        EntityCollection entityCollection = serviceProxy.RetrieveMultiple(lookupInfo.QueryExpression);
        foreach (Entity entity in entityCollection.Entities)
        {
          ContactInfo contactInfo = lookupInfo.ProcessEntity(entity);
          if (contactInfo != null)
          {
            results[lookupInfo.ResultIndex] = contactInfo;
            break;
          }
        }
      }
      catch (Exception exc)
      {
        LogHelper.Log(Environment.SpecialFolder.ApplicationData, "MicrosoftDynamicsCRM.log", "Error looking for contact: " + ErrorHelper.GetErrorDescription(exc));
      }
      finally
      {
        waitHandles[lookupInfo.ResultIndex].Set();
      }
    }

    private ContactInfo syncLookup(AbsLookupInfo lookupInfo)
    {
      EntityCollection entityCollection = serviceProxy.RetrieveMultiple(lookupInfo.QueryExpression);
      foreach (Entity entity in entityCollection.Entities)
      {
        ContactInfo contactInfo = lookupInfo.ProcessEntity(entity);
        if (contactInfo != null) return contactInfo;
      }
      return null;
    }
    
    private void launchLookupsInOrder(string likeCriteria, int index, int lookupContactsOrder, int lookupLeadsOrder, int lookupAccountsOrder)
    {
      if (lookupContactsOrder == index)
        ThreadPool.QueueUserWorkItem(new WaitCallback(asyncLookup), new ContactLookupInfo(likeCriteria, index, contactNumber));
      else if (lookupLeadsOrder == index)
        ThreadPool.QueueUserWorkItem(new WaitCallback(asyncLookup), new LeadLookupInfo(likeCriteria, index, contactNumber));
      else if (lookupAccountsOrder == index)
        ThreadPool.QueueUserWorkItem(new WaitCallback(asyncLookup), new AccountLookupInfo(likeCriteria, index, contactNumber));
      else
        waitHandles[index].Set();
    }
    
    public ContactFinder(OrganizationServiceProxy serviceProxy)
    {
      this.serviceProxy = serviceProxy;
    }

    public ContactInfo GetContactInformation(string contactNumber, int likeCriteriaLength)
    {
      this.contactNumber = contactNumber;
      
      for (int i = 0; i < waitHandles.Length; ++i)
      {
        results[i] = null;
        waitHandles[i] = new AutoResetEvent(false);
      }
      
      int lookupContactsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupContactsOrder", "1"));
      int lookupLeadsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupLeadsOrder", "2"));
      int lookupAccountsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupAccountsOrder", "0"));

      string likeCriteria = contactNumber.Length > likeCriteriaLength ? contactNumber.Substring(contactNumber.Length - likeCriteriaLength) : contactNumber;
      for (int index = 0; index < waitHandles.Length; ++index)
        launchLookupsInOrder(likeCriteria, index, lookupContactsOrder, lookupLeadsOrder, lookupAccountsOrder);
      
      // Return results with the configured order
      for (int index = 0; index < results.Length; ++index)
      {
        waitHandles[index].WaitOne();
        if (results[index] != null) return results[index];
      }
      
      return null;
    }

    public ContactInfo GetContactInformationByLeadId(string phoneType, string leadId)
    {
      return syncLookup(new LeadLookupInfo(phoneType, leadId));
    }

    public ContactInfo GetContactInformationByContactId(string phoneType, string contactId)
    {
      return syncLookup(new ContactLookupInfo(phoneType, contactId));
    }

    public ContactInfo GetContactInformationByAccountId(string phoneType, string accountId)
    {
      return syncLookup(new AccountLookupInfo(phoneType, accountId));
    }
  }
}
