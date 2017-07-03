using CRMPluginUtils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace MicrosoftDynamicsCRMPlugin
{
  public class AccountLookupInfo : AbsLookupInfo
  {
    private string phoneType = String.Empty;

    private string getPhoneField(Microsoft.Crm.Sdk.Account a)
    {
      switch (phoneType)
      {
        case "address1_fax": return a.Address1_Fax;
        case "address1_telephone1": return a.Address1_Telephone1;
        case "address1_telephone2": return a.Address1_Telephone2;
        case "address1_telephone3": return a.Address1_Telephone3;
        case "address2_fax": return a.Address2_Fax;
        case "address2_telephone1": return a.Address2_Telephone1;
        case "address2_telephone2": return a.Address2_Telephone2;
        case "address2_telephone3": return a.Address2_Telephone3;
        case "fax": return a.Fax;
        case "telephone1": return a.Telephone1;
        case "telephone2": return a.Telephone2;
        case "telephone3": return a.Telephone3;
      }
      return String.Empty;
    }
    
    private string[] getTelephoneAttributeNames()
    {
      return new string[] { "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "fax", "telephone1", "telephone2", "telephone3" };
    }

    protected override ColumnSet getColumns()
    {
      return new ColumnSet(new string[] { "accountid", "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "fax", "telephone1", "telephone2", "telephone3" });
    }
    
    public AccountLookupInfo(string accountName)
    : base(-1, "", true)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      conditionList.Add(createCondition("name", ConditionOperator.Equal, new string[] { accountName }));

      configureQueryExpression(Microsoft.Crm.Sdk.Account.EntityLogicalName.ToString(), conditionList.ToArray());
    }

    public AccountLookupInfo(string phoneType, string accountId)
    : base(-1, "", true)
    {
      this.phoneType = phoneType;
      
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      conditionList.Add(createCondition("accountid", ConditionOperator.Equal, new string[] { accountId }));
      
      configureQueryExpression(Microsoft.Crm.Sdk.Account.EntityLogicalName.ToString(), conditionList.ToArray());
    }
    
    public AccountLookupInfo(int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, true)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] {contactNumber}));

      configureQueryExpression(Microsoft.Crm.Sdk.Account.EntityLogicalName.ToString(), conditionList.ToArray());
    }

    public AccountLookupInfo(string likeCriteria, int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, false)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
      {
        conditionList.Add(createCondition(attributeName, ConditionOperator.Like, new string[] { "%" + likeCriteria }));
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] { "skype:" + contactNumber }));
      }

      configureQueryExpression(Microsoft.Crm.Sdk.Account.EntityLogicalName.ToString(), conditionList.ToArray());
    }
    
    public override ContactInfo ProcessEntity(Entity entity)
    {
      Microsoft.Crm.Sdk.Account a = entity as Microsoft.Crm.Sdk.Account;
      if (a != null)
      {
        string[] telephoneArray = String.IsNullOrEmpty(phoneType) ? 
                                  new string[] { a.Address1_Fax, a.Address1_Telephone1, a.Address1_Telephone2, a.Address1_Telephone3, a.Address2_Fax, a.Address2_Telephone1, a.Address2_Telephone2, a.Address2_Telephone3, a.Fax, a.Telephone1, a.Telephone2, a.Telephone3 } :
                                  new string[] { getPhoneField(a) };
        foreach (string telephone in telephoneArray)
        {
          if (match(telephone)) return new ContactInfo(a.AccountId.Value.ToString(), telephone, ContactTypes.Account);
        }
      }
      return null;
    }
  }
}
