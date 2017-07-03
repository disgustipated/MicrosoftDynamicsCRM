using CRMPluginUtils;
using System;

namespace MicrosoftDynamicsCRMPlugin
{
  public enum WindowsLiveLocations
  {
    NorthAmerica,
    NorthAmerica2,
    EMEA,
    APAC,
    Oceania,
    Japan,
    SouthAmerica,
    India,
    Canada
  }


  public class WindowsLiveLocationHelper
  {
    public static WindowsLiveLocations FromString(string s)
    {
      switch (s)
      {
        case "North America": return WindowsLiveLocations.NorthAmerica;
        case "North America 2": return WindowsLiveLocations.NorthAmerica2;
        case "EMEA": return WindowsLiveLocations.EMEA;
        case "APAC": return WindowsLiveLocations.APAC;
        case "Oceania": return WindowsLiveLocations.Oceania;
        case "Japan": return WindowsLiveLocations.Japan;
        case "South America": return WindowsLiveLocations.SouthAmerica;
        case "India": return WindowsLiveLocations.India;
        case "Canada": return WindowsLiveLocations.Canada;
        default: throw new ArgumentException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidWindowsLiveLocation"), s));
      }
    }

    public static string ToString(WindowsLiveLocations wll)
    {
      switch (wll)
      {
        case WindowsLiveLocations.NorthAmerica: return "North America";
        case WindowsLiveLocations.NorthAmerica2: return "North America 2";
        case WindowsLiveLocations.EMEA: return "EMEA";
        case WindowsLiveLocations.APAC: return "APAC";
        case WindowsLiveLocations.Oceania: return "Oceania";
        case WindowsLiveLocations.Japan: return "Japan";
        case WindowsLiveLocations.SouthAmerica: return "South America";
        case WindowsLiveLocations.India: return "India";
        case WindowsLiveLocations.Canada: return "Canada";
        default: throw new ArgumentException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPlugin.Error.InvalidWindowsLiveLocation"), wll));
      }
    }
  }
}
