using CRMPluginUtils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace MicrosoftDynamicsCRMPlugin
{
  public abstract class AbsLookupInfo
  {
    private int resultIndex;
    private string contactNumber;
    private bool exactMatch;
    private QueryExpression queryExpression;
    
    private FilterExpression getFilterExpression(ConditionExpression[] conditionArray)
    {
      FilterExpression filter = new FilterExpression();
      filter.FilterOperator = LogicalOperator.Or;
      foreach (ConditionExpression conditionExpression in conditionArray)
        filter.AddCondition(conditionExpression);
      return filter;
    }

    protected bool match(string telephone)
    {
      if (String.IsNullOrEmpty(contactNumber)) return true;
      if (String.IsNullOrEmpty(telephone)) return false;

      return PhoneMatchHelper.TelephoneMatches(contactNumber, telephone, exactMatch, GeneralConfiguration.MaxCompareLength);
    }

    protected void configureQueryExpression(string entityName, ConditionExpression[] conditionArray)
    {
      queryExpression.EntityName = entityName;
      queryExpression.ColumnSet = getColumns();
      queryExpression.Criteria = getFilterExpression(conditionArray);
    }

    protected ConditionExpression createCondition(string attributeName, ConditionOperator conditionOperator, object[] values)
    {
      return new ConditionExpression(attributeName, conditionOperator, values);
    }
    
    protected abstract ColumnSet getColumns();

    public AbsLookupInfo(int resultIndex, string contactNumber, bool exactMatch)
    {
      this.resultIndex = resultIndex;
      this.contactNumber = contactNumber;
      this.exactMatch = exactMatch;
      this.queryExpression = new QueryExpression();
    }

    public abstract ContactInfo ProcessEntity(Entity entity);
    
    public int ResultIndex
    {
      get { return resultIndex; }
    }
    
    public QueryExpression QueryExpression
    {
      get { return queryExpression; }
    }
  }
}
