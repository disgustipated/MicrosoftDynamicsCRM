using CRMPluginUtils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MicrosoftDynamicsCRMPlugin
{
  public partial class Office365AuthenticationConfigurationControl : PluginConfigurationPartControl
  {
    private ConfigurationManager configurationManager = new ConfigurationManager("3CXCRMUser.ini");

    private const string locationNorthAmericaText = "North America";
    private const string locationNorthAmerica2Text = "North America 2";
    private const string locationEMEAText = "EMEA";
    private const string locationAPACText = "APAC";
    private const string locationOceaniaText = "Oceania";
    private const string locationJapanText = "Japan";
    private const string locationSouthAmericaText = "South America";
    private const string locationIndiaText = "India";
    private const string locationCanadaText = "Canada";

    private int getLocationIndex(string location)
    {
      if (location == locationNorthAmericaText) return 0;
      if (location == locationNorthAmerica2Text) return 1;
      if (location == locationEMEAText) return 2;
      if (location == locationAPACText) return 3;
      if (location == locationOceaniaText) return 4;
      if (location == locationJapanText) return 5;
      if (location == locationSouthAmericaText) return 6;
      if (location == locationIndiaText) return 7;
      if (location == locationCanadaText) return 8;
      return 0;
    }

    private void loadLabelsFromResources()
    {
      lblUserName.Text = LocalizedResourceManager.GetString("DotNetScript", "Office365AuthenticationConfigurationControl.lblUserName.Text");
      lblPassword.Text = LocalizedResourceManager.GetString("DotNetScript", "Office365AuthenticationConfigurationControl.lblPassword.Text");
      lblLocation.Text = LocalizedResourceManager.GetString("DotNetScript", "Office365AuthenticationConfigurationControl.lblLocation.Text");
      comboLocation.Items.AddRange(new string[] { locationNorthAmericaText, locationNorthAmerica2Text, locationEMEAText, locationAPACText, locationOceaniaText, locationJapanText, locationSouthAmericaText, locationIndiaText, locationCanadaText });

      int maxWidth = Math.Max(lblUserName.Width, Math.Max(lblPassword.Width, lblLocation.Width));
      txtUserName.Location = new Point(6 + lblUserName.Location.X + maxWidth, txtUserName.Location.Y);
      txtUserName.Size = new Size(Width - txtUserName.Location.X - 6, txtUserName.Height);
      txtPassword.Location = new Point(6 + lblPassword.Location.X + maxWidth, txtPassword.Location.Y);
      txtPassword.Size = new Size(Width - txtPassword.Location.X - 6, txtPassword.Height);
      comboLocation.Location = new Point(6 + lblLocation.Location.X + maxWidth, comboLocation.Location.Y);
      comboLocation.Size = new Size(Width - comboLocation.Location.X - 6, comboLocation.Height);
    }

    private void loadInitialValues()
    {
      try
      {
        disableNotifications();

        txtUserName.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UserName", "");
        txtPassword.Text = configurationManager.GetCryptedValue("Microsoft Dynamics Plug-in", "Password", "");

        string location = configurationManager.GetValue("Microsoft Dynamics Plug-in", "WindowsLiveLocation", locationNorthAmericaText);
        comboLocation.SelectedIndex = getLocationIndex(location);
      }
      catch (Exception exc)
      {
        MessageBox.Show(String.Format(LocalizedResourceManager.GetString("DotNetScript", "Office365AuthenticationConfigurationControl.Error.LoadingConfiguration"), ErrorHelper.GetErrorDescription(exc)), "3CX Microsoft Dynamics CRM Plug-in", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        enableNotifications();
      }
    }

    public Office365AuthenticationConfigurationControl()
    {
      InitializeComponent();
      loadLabelsFromResources();
      loadInitialValues();
    }

    public override void LoadConfiguration()
    {
      loadInitialValues();
    }

    public override void Save()
    {
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "UserName", txtUserName.Text);
      configurationManager.SetCryptedValue("Microsoft Dynamics Plug-in", "Password", txtPassword.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "WindowsLiveLocation", comboLocation.SelectedItem.ToString());
    }
    
    private void textBox_GotFocus(object sender, EventArgs e)
    {
      TextBox textBox = sender as TextBox;
      textBox.SelectAll();
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      notifyControlChanged();
    }

    private void comboLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
      notifyControlChanged();
    }
  }
}
