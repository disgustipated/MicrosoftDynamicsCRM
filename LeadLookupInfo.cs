using CRMPluginUtils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace MicrosoftDynamicsCRMPlugin
{
  public class LeadLookupInfo : AbsLookupInfo
  {
    private string phoneType = String.Empty;

    private string getPhoneField(Microsoft.Crm.Sdk.Lead l)
    {
      switch (phoneType)
      {
        case "address1_fax": return l.Address1_Fax;
        case "address1_telephone1": return l.Address1_Telephone1;
        case "address1_telephone2": return l.Address1_Telephone2;
        case "address1_telephone3": return l.Address1_Telephone3;
        case "address2_fax": return l.Address2_Fax;
        case "address2_telephone1": return l.Address2_Telephone1;
        case "address2_telephone2": return l.Address2_Telephone2;
        case "address2_telephone3": return l.Address2_Telephone3;
        case "fax": return l.Fax;
        case "mobilephone": return l.MobilePhone;
        case "telephone1": return l.Telephone1;
        case "telephone2": return l.Telephone2;
        case "telephone3": return l.Telephone3;
      }
      return String.Empty;
    }

    private string[] getTelephoneAttributeNames()
    {
      return new string[] { "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "fax", "mobilephone", "telephone1", "telephone2", "telephone3" };
    }

    protected override ColumnSet getColumns()
    {
      return new ColumnSet(new string[] { "leadid", "address1_fax", "address1_telephone1", "address1_telephone2", "address1_telephone3", "address2_fax", "address2_telephone1", "address2_telephone2", "address2_telephone3", "fax", "mobilephone", "telephone1", "telephone2", "telephone3" });
    }

    public LeadLookupInfo(string phoneType, string leadId)
    : base(-1, "", true)
    {
      this.phoneType = phoneType;
      
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      conditionList.Add(createCondition("leadid", ConditionOperator.Equal, new string[] { leadId }));

      configureQueryExpression(Microsoft.Crm.Sdk.Lead.EntityLogicalName.ToString(), conditionList.ToArray());
    }
    
    public LeadLookupInfo(int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, true)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] {contactNumber}));
      
      configureQueryExpression(Microsoft.Crm.Sdk.Lead.EntityLogicalName.ToString(), conditionList.ToArray());
    }

    public LeadLookupInfo(string likeCriteria, int resultIndex, string contactNumber)
    : base(resultIndex, contactNumber, false)
    {
      // Create ConditionExpressions
      List<ConditionExpression> conditionList = new List<ConditionExpression>();
      foreach (string attributeName in getTelephoneAttributeNames())
      {
        conditionList.Add(createCondition(attributeName, ConditionOperator.Like, new string[] { "%" + likeCriteria }));
        conditionList.Add(createCondition(attributeName, ConditionOperator.Equal, new string[] { "skype:" + contactNumber }));
      }
      
      configureQueryExpression(Microsoft.Crm.Sdk.Lead.EntityLogicalName.ToString(), conditionList.ToArray());
    }

    public override ContactInfo ProcessEntity(Entity entity)
    {
      Microsoft.Crm.Sdk.Lead l = entity as Microsoft.Crm.Sdk.Lead;
      if (l != null)
      {
        string[] telephoneArray = String.IsNullOrEmpty(phoneType) ?
                                  new string[] { l.Address1_Fax, l.Address1_Telephone1, l.Address1_Telephone2, l.Address1_Telephone3, l.Address2_Fax, l.Address2_Telephone1, l.Address2_Telephone2, l.Address2_Telephone3, l.Fax, l.MobilePhone, l.Telephone1, l.Telephone2, l.Telephone3 } :
                                  new string[] { getPhoneField(l) };
        foreach (string telephone in telephoneArray)
        {
          if (match(telephone)) return new ContactInfo(l.LeadId.Value.ToString(), telephone, ContactTypes.Lead);
        }
      }
      return null;
    }
  }
}
