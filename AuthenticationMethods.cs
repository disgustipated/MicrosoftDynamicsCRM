using CRMPluginUtils;
using System;

namespace MicrosoftDynamicsCRMPlugin
{
  public enum AuthenticationMethods
  {
    OnPremise,
    IFD,
    Office365
  }
  
  
  public class AuthenticationMethodHelper
  {
    public static AuthenticationMethods FromString(string s)
    {
      switch (s)
      {
        case "OnPremise": return AuthenticationMethods.OnPremise;
        case "IFD": return AuthenticationMethods.IFD;
        case "Office365": return AuthenticationMethods.Office365;
        default: throw new ArgumentException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidAuthenticationMethod"), s));
      }
    }

    public static string ToString(AuthenticationMethods am)
    {
      switch (am)
      {
        case AuthenticationMethods.OnPremise: return "OnPremise";
        case AuthenticationMethods.IFD: return "IFD";
        case AuthenticationMethods.Office365: return "Office365";
        default: throw new ArgumentException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidAuthenticationMethod"), am));
      }
    }
  }
}
