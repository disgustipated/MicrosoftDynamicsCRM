using CRMPluginUtils;
using MicrosoftDynamicsCRMPlugin.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MicrosoftDynamicsCRMPlugin
{
  public partial class DynamicsPluginConfigurationControl : PluginConfigurationControl
  {
    private ConfigurationManager configurationManager = new ConfigurationManager("3CXCRMUser.ini");

    private const string onPremiseText = "OnPremise";
    private const string ifdText = "IFD";
    private const string office365Text = "Office365";

    private string contactsText = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.Contacts.Text");
    private string leadsText = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.Leads.Text");
    private string accountsText = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.Accounts.Text");

    private OnPremiseAuthenticationConfigurationControl onPremiseAuthenticationConfigurationControl = new OnPremiseAuthenticationConfigurationControl();
    private IFDAuthenticationConfigurationControl ifdAuthenticationConfigurationControl = new IFDAuthenticationConfigurationControl();
    private Office365AuthenticationConfigurationControl office365AuthenticationConfigurationControl = new Office365AuthenticationConfigurationControl();
    
    private void loadLabelsFromResources()
    {
      grpBoxLoginInformation.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.grpBoxLoginInformation.Text");
      lblOrganization.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.lblOrganization.Text");
      lblAuthenticationMethod.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.lblAuthenticationMethod.Text");
      comboAuthenticationMethod.Items.AddRange(new string[] { onPremiseText, ifdText, office365Text });

      authenticationControlPanel.Controls.AddRange(new Control[] { onPremiseAuthenticationConfigurationControl,
                                                                   ifdAuthenticationConfigurationControl ,
                                                                   office365AuthenticationConfigurationControl});
      onPremiseAuthenticationConfigurationControl.Dock = DockStyle.Fill;
      onPremiseAuthenticationConfigurationControl.Visible = false;
      onPremiseAuthenticationConfigurationControl.Changed += new ChangedHandler(notifyControlChanged);
      
      ifdAuthenticationConfigurationControl.Dock = DockStyle.Fill;
      ifdAuthenticationConfigurationControl.Visible = false;
      ifdAuthenticationConfigurationControl.Changed += new ChangedHandler(notifyControlChanged);

      office365AuthenticationConfigurationControl.Dock = DockStyle.Fill;
      office365AuthenticationConfigurationControl.Visible = false;
      office365AuthenticationConfigurationControl.Changed += new ChangedHandler(notifyControlChanged);

      grpBoxContactLookup.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.grpBoxContactLookup.Text");
      chkLookupContacts.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkLookupContacts.Text");
      chkLookupLeads.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkLookupLeads.Text");
      chkLookupAccounts.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkLookupAccounts.Text");
      lblLookupOrder.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.lblLookupOrder.Text");
      chkNewCase.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkNewCase.Text");
      chkOpenPhoneCall.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkOpenPhoneCall.Text");
      chkBoxUseDefaultBrowser.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.chkBoxUseDefaultBrowser.Text");
      lblSelectBrowser.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.lblSelectBrowser.Text");
      selectBrowserButton.Text = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.selectBrowserButton.Text");
      selectBrowserDialog.Title = LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.selectBrowserDialog.Title");
      
      int maxWidth = Math.Max(lblOrganization.Width, lblAuthenticationMethod.Width);
      txtOrganization.Location = new Point(6 + lblOrganization.Location.X + maxWidth, txtOrganization.Location.Y);
      txtOrganization.Size = new Size(grpBoxLoginInformation.Width - txtOrganization.Location.X - 6, txtOrganization.Height);
      comboAuthenticationMethod.Location = new Point(6 + lblAuthenticationMethod.Location.X + maxWidth, comboAuthenticationMethod.Location.Y);
      comboAuthenticationMethod.Size = new Size(grpBoxLoginInformation.Width - comboAuthenticationMethod.Location.X - 6, comboAuthenticationMethod.Height);
    }
    
    private void loadInitialValues()
    {
      try
      {
        disableNotifications();

        txtOrganization.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "Organization", "");
        string authenticationMethod = configurationManager.GetValue("Microsoft Dynamics Plug-in", "AuthenticationMethod", onPremiseText);
        comboAuthenticationMethod.SelectedIndex = authenticationMethod == office365Text ? 2 : (authenticationMethod == ifdText ? 1 : 0);

        int lookupContactsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupContactsOrder", "0"));
        int lookupLeadsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupLeadsOrder", "1"));
        int lookupAccountsOrder = Convert.ToInt32(configurationManager.GetValue("Microsoft Dynamics Plug-in", "LookupAccountsOrder", "-1"));

        for (int index = 0; index < 3; ++index)
          addLookupOrderItem(index, lookupContactsOrder, lookupLeadsOrder, lookupAccountsOrder);

        if (lookupOrderListBox.Items.Count == 0)
        {
          moveDownButton.Enabled = false;
          moveUpButton.Enabled = false;
        }
        else
          lookupOrderListBox.SelectedIndex = 0;

        chkNewCase.Checked = configurationManager.GetValue("Microsoft Dynamics CRM Plug-in", "CreateNewCase", "False") == "False";
        chkOpenPhoneCall.Checked = configurationManager.GetValue("Microsoft Dynamics CRM Plug-in", "OpenPhoneCall", "False") == "False";
        chkBoxUseDefaultBrowser.Checked = configurationManager.GetValue("Microsoft Dynamics Plug-in", "UseDefaultBrowser", "True") == "True";
        txtBrowser.Text = configurationManager.GetValue("Microsoft Dynamics Plug-in", "CustomBrowser", "");
      }
      catch (Exception exc)
      {
        MessageBox.Show(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.Error.LoadingConfiguration"), ErrorHelper.GetErrorDescription(exc)), "3CX Microsoft Dynamics CRM Plug-in", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        enableNotifications();
      }
    }
    
    private void addLookupOrderItem(int index, int lookupContactsOrder, int lookupLeadsOrder, int lookupAccountsOrder)
    {
      if (lookupContactsOrder == index)
        chkLookupContacts.Checked = true;
      else if (lookupLeadsOrder == index)
        chkLookupLeads.Checked = true;
      else if (lookupAccountsOrder == index)
        chkLookupAccounts.Checked = true;
    }

    public DynamicsPluginConfigurationControl()
    {
      InitializeComponent();
      loadLabelsFromResources();
      loadInitialValues();
    }

    public override Image GetPluginIcon()
    {
      return Resources.Dynamics_CRM;
    }

    public override string GetPluginName()
    {
      return "Microsoft Dynamics CRM";
    }

    public override void LoadConfiguration()
    {
      onPremiseAuthenticationConfigurationControl.LoadConfiguration();
      ifdAuthenticationConfigurationControl.LoadConfiguration();
      office365AuthenticationConfigurationControl.LoadConfiguration();
      loadInitialValues();
    }

    public override void Save()
    {
      if (!chkBoxUseDefaultBrowser.Checked && (String.IsNullOrEmpty(txtBrowser.Text) || !File.Exists(txtBrowser.Text)))
        throw new ApplicationException(String.Format(LocalizedResourceManager.GetString("DotNetScript", "DynamicsPluginConfigurationControl.Error.InvalidCustomBrowser"), txtBrowser.Text));

      switch (comboAuthenticationMethod.SelectedIndex)
      {
        case 0: onPremiseAuthenticationConfigurationControl.Save(); break;
        case 1: ifdAuthenticationConfigurationControl.Save(); break;
        case 2: office365AuthenticationConfigurationControl.Save(); break;
      }

      configurationManager.SetValue("Microsoft Dynamics Plug-in", "Organization", txtOrganization.Text);
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "AuthenticationMethod", comboAuthenticationMethod.SelectedItem.ToString());
      
      // Initially set every item to -1 (disabled), then enable them in order, as specified in lookupOrderListBox
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupContactsOrder", "-1");
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupLeadsOrder", "-1");
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupAccountsOrder", "-1");
      for (int index = 0; index < lookupOrderListBox.Items.Count; ++index)
      {
        if (lookupOrderListBox.Items[index].ToString() == contactsText)
          configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupContactsOrder", index.ToString());
        else if (lookupOrderListBox.Items[index].ToString() == leadsText)
          configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupLeadsOrder", index.ToString());
        else if (lookupOrderListBox.Items[index].ToString() == accountsText)
          configurationManager.SetValue("Microsoft Dynamics Plug-in", "LookupAccountsOrder", index.ToString());
      }
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "CreateNewCase", chkNewCase.Checked ? "True" : "False");
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "OpenPhoneCall", chkOpenPhoneCall.Checked ? "True" : "False");

      configurationManager.SetValue("Microsoft Dynamics Plug-in", "UseDefaultBrowser", chkBoxUseDefaultBrowser.Checked ? "True" : "False");
      configurationManager.SetValue("Microsoft Dynamics Plug-in", "CustomBrowser", txtBrowser.Text);
    }

    private void comboAuthenticationMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
      onPremiseAuthenticationConfigurationControl.Visible = false;
      ifdAuthenticationConfigurationControl.Visible = false;
      office365AuthenticationConfigurationControl.Visible = false;

      switch (comboAuthenticationMethod.SelectedIndex)
      {
        case 0: onPremiseAuthenticationConfigurationControl.Visible = true; break;
        case 1: ifdAuthenticationConfigurationControl.Visible = true; break;
        case 2: office365AuthenticationConfigurationControl.Visible = true; break;
      }
      notifyControlChanged();
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
      MaskedTextBox textBox = sender as MaskedTextBox;
      textBox.SelectAll();
    }
    
    private void maskedTextBox_TextChanged(object sender, EventArgs e)
    {
      notifyControlChanged();
    }

    private void chkLookupContacts_CheckedChanged(object sender, EventArgs e)
    {
      if (chkLookupContacts.Checked)
        lookupOrderListBox.Items.Add(contactsText);
      else
        lookupOrderListBox.Items.Remove(contactsText);
      
      notifyControlChanged();
    }

    private void chkLookupLeads_CheckedChanged(object sender, EventArgs e)
    {
      if (chkLookupLeads.Checked)
        lookupOrderListBox.Items.Add(leadsText);
      else
        lookupOrderListBox.Items.Remove(leadsText);

      notifyControlChanged();
    }

    private void chkLookupAccounts_CheckedChanged(object sender, EventArgs e)
    {
      if (chkLookupAccounts.Checked)
        lookupOrderListBox.Items.Add(accountsText);
      else
        lookupOrderListBox.Items.Remove(accountsText);

      notifyControlChanged();
    }

    private void lookupOrderListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      moveDownButton.Enabled = lookupOrderListBox.SelectedIndex >= 0 && lookupOrderListBox.SelectedIndex < lookupOrderListBox.Items.Count - 1;
      moveUpButton.Enabled = lookupOrderListBox.SelectedIndex > 0;
    }

    private void moveUpButton_Click(object sender, EventArgs e)
    {
      string selectedItem = lookupOrderListBox.SelectedItem.ToString();
      int selectedIndex = lookupOrderListBox.SelectedIndex;
      lookupOrderListBox.Items.RemoveAt(selectedIndex);
      lookupOrderListBox.Items.Insert(selectedIndex - 1, selectedItem);
      lookupOrderListBox.SelectedIndex = selectedIndex - 1;
      
      notifyControlChanged();
    }

    private void moveDownButton_Click(object sender, EventArgs e)
    {
      string selectedItem = lookupOrderListBox.SelectedItem.ToString();
      int selectedIndex = lookupOrderListBox.SelectedIndex;
      lookupOrderListBox.Items.RemoveAt(selectedIndex);
      lookupOrderListBox.Items.Insert(selectedIndex + 1, selectedItem);
      lookupOrderListBox.SelectedIndex = selectedIndex + 1;
      
      notifyControlChanged();
    }

    private void chkBoxUseDefaultBrowser_CheckedChanged(object sender, EventArgs e)
    {
      txtBrowser.Enabled = !chkBoxUseDefaultBrowser.Checked;
      selectBrowserButton.Enabled = !chkBoxUseDefaultBrowser.Checked;
      notifyControlChanged();
    }

    private void chkNewCase_CheckedChanged(object sender, EventArgs e)
    {
        txtBrowser.Enabled = !chkNewCase.Checked;
        notifyControlChanged();
    }

    private void chkOpenPhoneCall_CheckedChanged(object sender, EventArgs e)
    {
        txtBrowser.Enabled = !chkOpenPhoneCall.Checked;
        notifyControlChanged();
    }

    private void selectBrowserButton_Click(object sender, EventArgs e)
    {
      if (selectBrowserDialog.ShowDialog() == DialogResult.OK)
        txtBrowser.Text = selectBrowserDialog.FileName;
    }

        private void chkNewCase_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
