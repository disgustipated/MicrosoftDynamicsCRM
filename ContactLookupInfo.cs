using CRMPluginUtils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace MicrosoftDynamicsCRMPlugin
{
  public class ContactLookupInfo : AbsLookupInfo
  {
    private string phoneType = String.Empty;

    private string getPhoneField(Microsoft.Crm.Sdk.Contact c)
    {
      switch (phoneType)
      {
        case "address1_fax": return c.Address1_Fax;
        case "address1_telephone1": return c.Address1_Telephone1;
        case "address1_telephone2": return c.Address1_Telephone2;
        case "address1_telephone3": return c.Address1_Telephone3;
        case "address2_fax": return c.Address2_Fax;
        case "address2_telephone1": return c.Address2_Telephone1;
        case "address2_telephone2": return c.Address2_Telephone2;
        case "address2_telephone3": return c.Address2_Telephone3;
        case "assistantphone": return c.AssistantPhone;
        case "fax": return c.Fax;
        case "managerphone": return c.ManagerPhone;
        case "mobilephone": return c.MobilePhone;
        case "telephone1": return c.Telephone1;
        case "telephone2": return c.Telephone2;
        case "telephone3": return c.Telephone3;
      }
      return String.Empty;
    }
    
    private string[] getTelephoneAttributeNames()
    {
      return new string[] { "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "assistantphone", "fax", "managerphone", "mobilephone", "telephone1", "telephone2", "telephone3" };
    }

    protected override ColumnSet getColumns()
    {
      return new ColumnSet(new string[] { "contactid", "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "assistantphone", "fax", "managerphone", "mobilephone", "telephone1", "telephone2", "telephone3" });
    }
    
    public ContactLookupInfo(string phoneType, string contactId)
    : base(-1, "", true)
    {
      this.phoneType = phoneType;
      
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      conditionList.Add(createCondition("contactid", ConditionOperator.Equal, new string[] { contactId }));

      configureQueryExpression(Microsoft.Crm.Sdk.Contact.EntityLogicalName.ToString(), conditionList.ToArray());
    }
    
    public ContactLookupInfo(int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, true)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] {contactNumber}));
      
      configureQueryExpression(Microsoft.Crm.Sdk.Contact.EntityLogicalName.ToString(), conditionList.ToArray());
    }
    
    public ContactLookupInfo(string likeCriteria, int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, false)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
      {
        conditionList.Add(createCondition(attributeName, ConditionOperator.Like, new string[] { "%" + likeCriteria }));
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] { "skype:" + contactNumber }));
      }
      
      configureQueryExpression(Microsoft.Crm.Sdk.Contact.EntityLogicalName.ToString(), conditionList.ToArray());
    }

    public override ContactInfo ProcessEntity(Entity entity)
    {
      Microsoft.Crm.Sdk.Contact c = entity as Microsoft.Crm.Sdk.Contact;
      if (c != null)
      {
        string[] telephoneArray = String.IsNullOrEmpty(phoneType) ?
                                  new string[] { c.Address1_Fax, c.Address1_Telephone1, c.Address1_Telephone2, c.Address1_Telephone3, c.Address2_Fax, c.Address2_Telephone1, c.Address2_Telephone2, c.Address2_Telephone3, c.AssistantPhone, c.Fax, c.ManagerPhone, c.MobilePhone, c.Telephone1, c.Telephone2, c.Telephone3 } :
                                  new string[] { getPhoneField(c) };
        foreach (string telephone in telephoneArray)
        {
          if (match(telephone)) return new ContactInfo(c.ContactId.Value.ToString(), telephone, ContactTypes.Contact);
        }
      }
      return null;
    }
  }
}
