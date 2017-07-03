using CRMPluginUtils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MicrosoftDynamicsCRMPlugin
{
  public partial class OnPremiseAuthenticationConfigurationControl : PluginConfigurationPartControl
  {
    private ConfigurationManager configurationManager = new ConfigurationManager("3CXCRMUser.ini");


    private void loadLabelsFromResources()
    {
      lblServer.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.lblServer.Text");
      lblPort.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.lblPort.Text");
      chkUseDefaultCredentials.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.chkUseDefaultCredentials.Text");
      lblUserName.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.lblUserName.Text");
      lblPassword.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.lblPassword.Text");
      lblDomain.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.lblDomain.Text");
      chkSecureConnection.Text = LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.chkSecureConnection.Text");

      int maxWidth = Math.Max(lblServer.Width, Math.Max(lblPort.Width, Math.Max(lblUserName.Width, Math.Max(lblPassword.Width, lblDomain.Width))));
      txtServer.Location = new Point(6 + lblServer.Location.X + maxWidth, txtServer.Location.Y);
      txtServer.Size = new Size(Width - txtServer.Location.X - 6, txtServer.Height);
      txtPort.Location = new Point(6 + lblPort.Location.X + maxWidth, txtPort.Location.Y);
      txtPort.Size = new Size(Width - txtPort.Location.X - 6, txtPort.Height);
      txtUserName.Location = new Point(6 + lblUserName.Location.X + maxWidth, txtUserName.Location.Y);
      txtUserName.Size = new Size(Width - txtUserName.Location.X - 6, txtUserName.Height);
      txtPassword.Location = new Point(6 + lblPassword.Location.X + maxWidth, txtPassword.Location.Y);
      txtPassword.Size = new Size(Width - txtPassword.Location.X - 6, txtPassword.Height);
      txtDomain.Location = new Point(6 + lblDomain.Location.X + maxWidth, txtDomain.Location.Y);
      txtDomain.Size = new Size(Width - txtDomain.Location.X - 6, txtDomain.Height);
    }

    private void loadInitialValues()
    {
      try
      {
        disableNotifications();

        txtServer.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Server", "localhost");
        txtPort.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Port", "5555");
        chkUseDefaultCredentials.Checked = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseDefaultCredentials", "True") == "True";
        txtUserName.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UserName", "");
        txtPassword.Text = configurationManager.GetCryptedValue("Microsoft Dynamics Plug-in", "Password", "");
        txtDomain.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Domain", "");
        chkSecureConnection.Checked = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseSecureConnection", "False") == "True";
      }
      catch (Exception exc)
      {
        MessageBox.Show(String.Format(LocalizedResourceManager.GetString("DotNetScript", "OnPremiseAuthenticationConfigurationControl.Error.LoadingConfiguration"), ErrorHelper.GetErrorDescription(exc)), "3CX Microsoft Dynamics CRM Plug-in", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        enableNotifications();
      }
    }

    public OnPremiseAuthenticationConfigurationControl()
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
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "Server", txtServer.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "Port", txtPort.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "UseDefaultCredentials", chkUseDefaultCredentials.Checked ? "True" : "False");
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "UserName", txtUserName.Text);
      configurationManager.SetCryptedValue("Microsoft Dynamics Plug-in", "Password", txtPassword.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "Domain", txtDomain.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "UseSecureConnection", chkSecureConnection.Checked ? "True" : "False");
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

    private void maskedTextBox_GotFocus(object sender, EventArgs e)
    {
      MaskedTextBox maskedTextBox = sender as MaskedTextBox;
      maskedTextBox.SelectAll();
    }

    private void maskedTextBox_TextChanged(object sender, EventArgs e)
    {
      notifyControlChanged();
    }

    private void chkUseDefaultCredentials_CheckedChanged(object sender, EventArgs e)
    {
      txtUserName.Enabled = !chkUseDefaultCredentials.Checked;
      txtPassword.Enabled = !chkUseDefaultCredentials.Checked;
      txtDomain.Enabled = !chkUseDefaultCredentials.Checked;
      notifyControlChanged();
    }

    private void chkSecureConnection_CheckedChanged(object sender, EventArgs e)
    {
      notifyControlChanged();
    }
  }
}
