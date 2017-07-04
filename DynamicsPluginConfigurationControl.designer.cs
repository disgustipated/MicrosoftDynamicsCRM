namespace MicrosoftDynamicsCRMPlugin
{
  partial class DynamicsPluginConfigurationControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.chkLookupLeads = new System.Windows.Forms.CheckBox();
            this.chkLookupContacts = new System.Windows.Forms.CheckBox();
            this.chkLookupAccounts = new System.Windows.Forms.CheckBox();
            this.lookupOrderListBox = new System.Windows.Forms.ListBox();
            this.txtOrganization = new System.Windows.Forms.TextBox();
            this.lblOrganization = new System.Windows.Forms.Label();
            this.authenticationControlPanel = new System.Windows.Forms.Panel();
            this.lblAuthenticationMethod = new System.Windows.Forms.Label();
            this.comboAuthenticationMethod = new System.Windows.Forms.ComboBox();
            this.selectBrowserButton = new System.Windows.Forms.Button();
            this.txtBrowser = new System.Windows.Forms.TextBox();
            this.lblSelectBrowser = new System.Windows.Forms.Label();
            this.chkBoxUseDefaultBrowser = new System.Windows.Forms.CheckBox();
            this.lblLookupOrder = new System.Windows.Forms.Label();
            this.selectBrowserDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpBoxLoginInformation = new System.Windows.Forms.GroupBox();
            this.grpBoxContactLookup = new System.Windows.Forms.GroupBox();
            this.chkOpenPhoneCall = new System.Windows.Forms.CheckBox();
            this.chkOpenNewCase = new System.Windows.Forms.CheckBox();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.grpBoxLoginInformation.SuspendLayout();
            this.grpBoxContactLookup.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkLookupLeads
            // 
            this.chkLookupLeads.AutoSize = true;
            this.chkLookupLeads.Location = new System.Drawing.Point(9, 42);
            this.chkLookupLeads.Name = "chkLookupLeads";
            this.chkLookupLeads.Size = new System.Drawing.Size(134, 17);
            this.chkLookupLeads.TabIndex = 1;
            this.chkLookupLeads.Text = "Look up in Leads table";
            this.chkLookupLeads.UseVisualStyleBackColor = true;
            this.chkLookupLeads.CheckedChanged += new System.EventHandler(this.chkLookupLeads_CheckedChanged);
            // 
            // chkLookupContacts
            // 
            this.chkLookupContacts.AutoSize = true;
            this.chkLookupContacts.Location = new System.Drawing.Point(9, 19);
            this.chkLookupContacts.Name = "chkLookupContacts";
            this.chkLookupContacts.Size = new System.Drawing.Size(147, 17);
            this.chkLookupContacts.TabIndex = 0;
            this.chkLookupContacts.Text = "Look up in Contacts table";
            this.chkLookupContacts.UseVisualStyleBackColor = true;
            this.chkLookupContacts.CheckedChanged += new System.EventHandler(this.chkLookupContacts_CheckedChanged);
            // 
            // chkLookupAccounts
            // 
            this.chkLookupAccounts.AutoSize = true;
            this.chkLookupAccounts.Location = new System.Drawing.Point(9, 65);
            this.chkLookupAccounts.Name = "chkLookupAccounts";
            this.chkLookupAccounts.Size = new System.Drawing.Size(150, 17);
            this.chkLookupAccounts.TabIndex = 2;
            this.chkLookupAccounts.Text = "Look up in Accounts table";
            this.chkLookupAccounts.UseVisualStyleBackColor = true;
            this.chkLookupAccounts.CheckedChanged += new System.EventHandler(this.chkLookupAccounts_CheckedChanged);
            // 
            // lookupOrderListBox
            // 
            this.lookupOrderListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupOrderListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.lookupOrderListBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lookupOrderListBox.FormattingEnabled = true;
            this.lookupOrderListBox.Location = new System.Drawing.Point(9, 149);
            this.lookupOrderListBox.Name = "lookupOrderListBox";
            this.lookupOrderListBox.Size = new System.Drawing.Size(204, 82);
            this.lookupOrderListBox.TabIndex = 4;
            this.lookupOrderListBox.SelectedIndexChanged += new System.EventHandler(this.lookupOrderListBox_SelectedIndexChanged);
            // 
            // txtOrganization
            // 
            this.txtOrganization.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrganization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.txtOrganization.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtOrganization.Location = new System.Drawing.Point(126, 13);
            this.txtOrganization.Name = "txtOrganization";
            this.txtOrganization.Size = new System.Drawing.Size(118, 20);
            this.txtOrganization.TabIndex = 1;
            this.txtOrganization.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txtOrganization.GotFocus += new System.EventHandler(this.textBox_GotFocus);
            // 
            // lblOrganization
            // 
            this.lblOrganization.AutoSize = true;
            this.lblOrganization.Location = new System.Drawing.Point(6, 16);
            this.lblOrganization.Name = "lblOrganization";
            this.lblOrganization.Size = new System.Drawing.Size(66, 13);
            this.lblOrganization.TabIndex = 0;
            this.lblOrganization.Text = "Organization";
            // 
            // authenticationControlPanel
            // 
            this.authenticationControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authenticationControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.authenticationControlPanel.Location = new System.Drawing.Point(9, 66);
            this.authenticationControlPanel.Name = "authenticationControlPanel";
            this.authenticationControlPanel.Size = new System.Drawing.Size(235, 169);
            this.authenticationControlPanel.TabIndex = 4;
            // 
            // lblAuthenticationMethod
            // 
            this.lblAuthenticationMethod.AutoSize = true;
            this.lblAuthenticationMethod.Location = new System.Drawing.Point(6, 42);
            this.lblAuthenticationMethod.Name = "lblAuthenticationMethod";
            this.lblAuthenticationMethod.Size = new System.Drawing.Size(114, 13);
            this.lblAuthenticationMethod.TabIndex = 2;
            this.lblAuthenticationMethod.Text = "Authentication Method";
            // 
            // comboAuthenticationMethod
            // 
            this.comboAuthenticationMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAuthenticationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAuthenticationMethod.FormattingEnabled = true;
            this.comboAuthenticationMethod.Location = new System.Drawing.Point(126, 39);
            this.comboAuthenticationMethod.Name = "comboAuthenticationMethod";
            this.comboAuthenticationMethod.Size = new System.Drawing.Size(118, 21);
            this.comboAuthenticationMethod.TabIndex = 3;
            this.comboAuthenticationMethod.SelectedIndexChanged += new System.EventHandler(this.comboAuthenticationMethod_SelectedIndexChanged);
            // 
            // selectBrowserButton
            // 
            this.selectBrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBrowserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectBrowserButton.Location = new System.Drawing.Point(9, 318);
            this.selectBrowserButton.Name = "selectBrowserButton";
            this.selectBrowserButton.Size = new System.Drawing.Size(235, 23);
            this.selectBrowserButton.TabIndex = 10;
            this.selectBrowserButton.Text = "&Browse";
            this.selectBrowserButton.UseVisualStyleBackColor = true;
            this.selectBrowserButton.Click += new System.EventHandler(this.selectBrowserButton_Click);
            // 
            // txtBrowser
            // 
            this.txtBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.txtBrowser.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtBrowser.Location = new System.Drawing.Point(9, 292);
            this.txtBrowser.Name = "txtBrowser";
            this.txtBrowser.Size = new System.Drawing.Size(235, 20);
            this.txtBrowser.TabIndex = 9;
            this.txtBrowser.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txtBrowser.GotFocus += new System.EventHandler(this.textBox_GotFocus);
            // 
            // lblSelectBrowser
            // 
            this.lblSelectBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectBrowser.AutoSize = true;
            this.lblSelectBrowser.Location = new System.Drawing.Point(6, 276);
            this.lblSelectBrowser.Name = "lblSelectBrowser";
            this.lblSelectBrowser.Size = new System.Drawing.Size(78, 13);
            this.lblSelectBrowser.TabIndex = 8;
            this.lblSelectBrowser.Text = "Select Browser";
            // 
            // chkBoxUseDefaultBrowser
            // 
            this.chkBoxUseDefaultBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBoxUseDefaultBrowser.AutoSize = true;
            this.chkBoxUseDefaultBrowser.Location = new System.Drawing.Point(9, 256);
            this.chkBoxUseDefaultBrowser.Name = "chkBoxUseDefaultBrowser";
            this.chkBoxUseDefaultBrowser.Size = new System.Drawing.Size(120, 17);
            this.chkBoxUseDefaultBrowser.TabIndex = 7;
            this.chkBoxUseDefaultBrowser.Text = "Use default browser";
            this.chkBoxUseDefaultBrowser.UseVisualStyleBackColor = true;
            this.chkBoxUseDefaultBrowser.CheckedChanged += new System.EventHandler(this.chkBoxUseDefaultBrowser_CheckedChanged);
            // 
            // lblLookupOrder
            // 
            this.lblLookupOrder.AutoSize = true;
            this.lblLookupOrder.Location = new System.Drawing.Point(6, 133);
            this.lblLookupOrder.Name = "lblLookupOrder";
            this.lblLookupOrder.Size = new System.Drawing.Size(70, 13);
            this.lblLookupOrder.TabIndex = 3;
            this.lblLookupOrder.Text = "Lookup order";
            // 
            // selectBrowserDialog
            // 
            this.selectBrowserDialog.DefaultExt = "exe";
            this.selectBrowserDialog.Filter = "Exe Files|*.exe|All files|*.*";
            this.selectBrowserDialog.Title = "Select Browser";
            // 
            // grpBoxLoginInformation
            // 
            this.grpBoxLoginInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxLoginInformation.Controls.Add(this.txtOrganization);
            this.grpBoxLoginInformation.Controls.Add(this.lblOrganization);
            this.grpBoxLoginInformation.Controls.Add(this.comboAuthenticationMethod);
            this.grpBoxLoginInformation.Controls.Add(this.authenticationControlPanel);
            this.grpBoxLoginInformation.Controls.Add(this.lblAuthenticationMethod);
            this.grpBoxLoginInformation.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grpBoxLoginInformation.Location = new System.Drawing.Point(3, 3);
            this.grpBoxLoginInformation.Name = "grpBoxLoginInformation";
            this.grpBoxLoginInformation.Size = new System.Drawing.Size(250, 241);
            this.grpBoxLoginInformation.TabIndex = 0;
            this.grpBoxLoginInformation.TabStop = false;
            this.grpBoxLoginInformation.Text = "Login Information";
            // 
            // grpBoxContactLookup
            // 
            this.grpBoxContactLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxContactLookup.Controls.Add(this.chkOpenPhoneCall);
            this.grpBoxContactLookup.Controls.Add(this.chkOpenNewCase);
            this.grpBoxContactLookup.Controls.Add(this.selectBrowserButton);
            this.grpBoxContactLookup.Controls.Add(this.chkLookupContacts);
            this.grpBoxContactLookup.Controls.Add(this.txtBrowser);
            this.grpBoxContactLookup.Controls.Add(this.moveDownButton);
            this.grpBoxContactLookup.Controls.Add(this.lblSelectBrowser);
            this.grpBoxContactLookup.Controls.Add(this.chkLookupLeads);
            this.grpBoxContactLookup.Controls.Add(this.chkBoxUseDefaultBrowser);
            this.grpBoxContactLookup.Controls.Add(this.moveUpButton);
            this.grpBoxContactLookup.Controls.Add(this.lblLookupOrder);
            this.grpBoxContactLookup.Controls.Add(this.chkLookupAccounts);
            this.grpBoxContactLookup.Controls.Add(this.lookupOrderListBox);
            this.grpBoxContactLookup.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grpBoxContactLookup.Location = new System.Drawing.Point(3, 250);
            this.grpBoxContactLookup.Name = "grpBoxContactLookup";
            this.grpBoxContactLookup.Size = new System.Drawing.Size(250, 347);
            this.grpBoxContactLookup.TabIndex = 1;
            this.grpBoxContactLookup.TabStop = false;
            this.grpBoxContactLookup.Text = "Contact Lookup";
            // 
            // chkOpenPhoneCall
            // 
            this.chkOpenPhoneCall.AutoSize = true;
            this.chkOpenPhoneCall.Location = new System.Drawing.Point(9, 112);
            this.chkOpenPhoneCall.Name = "chkOpenPhoneCall";
            this.chkOpenPhoneCall.Size = new System.Drawing.Size(183, 17);
            this.chkOpenPhoneCall.TabIndex = 12;
            this.chkOpenPhoneCall.Text = "Open phone call activity after call";
            this.chkOpenPhoneCall.UseVisualStyleBackColor = true;
            // 
            // chkOpenNewCase
            // 
            this.chkOpenNewCase.AutoSize = true;
            this.chkOpenNewCase.Location = new System.Drawing.Point(9, 88);
            this.chkOpenNewCase.Name = "chkOpenNewCase";
            this.chkOpenNewCase.Size = new System.Drawing.Size(189, 17);
            this.chkOpenNewCase.TabIndex = 11;
            this.chkOpenNewCase.Text = "Open new case instead of contact";
            this.chkOpenNewCase.UseVisualStyleBackColor = true;
            // 
            // moveDownButton
            // 
            this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDownButton.FlatAppearance.BorderSize = 0;
            this.moveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveDownButton.Image = global::MicrosoftDynamicsCRMPlugin.Properties.Resources.MoveDown;
            this.moveDownButton.Location = new System.Drawing.Point(219, 174);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(25, 25);
            this.moveDownButton.TabIndex = 6;
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveUpButton.FlatAppearance.BorderSize = 0;
            this.moveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveUpButton.Image = global::MicrosoftDynamicsCRMPlugin.Properties.Resources.MoveUp;
            this.moveUpButton.Location = new System.Drawing.Point(219, 143);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(25, 25);
            this.moveUpButton.TabIndex = 5;
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // DynamicsPluginConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Controls.Add(this.grpBoxContactLookup);
            this.Controls.Add(this.grpBoxLoginInformation);
            this.MaximumSize = new System.Drawing.Size(300, 600);
            this.MinimumSize = new System.Drawing.Size(200, 551);
            this.Name = "DynamicsPluginConfigurationControl";
            this.Size = new System.Drawing.Size(256, 600);
            this.grpBoxLoginInformation.ResumeLayout(false);
            this.grpBoxLoginInformation.PerformLayout();
            this.grpBoxContactLookup.ResumeLayout(false);
            this.grpBoxContactLookup.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkLookupContacts;
    private System.Windows.Forms.CheckBox chkLookupLeads;
    private System.Windows.Forms.CheckBox chkLookupAccounts;
    private System.Windows.Forms.ListBox lookupOrderListBox;
    private System.Windows.Forms.Button moveUpButton;
    private System.Windows.Forms.Button moveDownButton;
    private System.Windows.Forms.Label lblLookupOrder;
    private System.Windows.Forms.Label lblAuthenticationMethod;
    private System.Windows.Forms.ComboBox comboAuthenticationMethod;
    private System.Windows.Forms.Panel authenticationControlPanel;
    private System.Windows.Forms.TextBox txtOrganization;
    private System.Windows.Forms.Label lblOrganization;
    private System.Windows.Forms.Button selectBrowserButton;
    private System.Windows.Forms.TextBox txtBrowser;
    private System.Windows.Forms.Label lblSelectBrowser;
    private System.Windows.Forms.CheckBox chkBoxUseDefaultBrowser;
    private System.Windows.Forms.OpenFileDialog selectBrowserDialog;
    private System.Windows.Forms.GroupBox grpBoxLoginInformation;
    private System.Windows.Forms.GroupBox grpBoxContactLookup;
        private System.Windows.Forms.CheckBox chkOpenPhoneCall;
        private System.Windows.Forms.CheckBox chkOpenNewCase;
    }
}
