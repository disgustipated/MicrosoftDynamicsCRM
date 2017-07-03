namespace MicrosoftDynamicsCRMPlugin
{
  partial class OnPremiseAuthenticationConfigurationControl
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
      this.chkUseDefaultCredentials = new System.Windows.Forms.CheckBox();
      this.lblUserName = new System.Windows.Forms.Label();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtDomain = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.lblDomain = new System.Windows.Forms.Label();
      this.lblPort = new System.Windows.Forms.Label();
      this.txtServer = new System.Windows.Forms.TextBox();
      this.lblServer = new System.Windows.Forms.Label();
      this.txtPort = new System.Windows.Forms.MaskedTextBox();
      this.chkSecureConnection = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // chkUseDefaultCredentials
      // 
      this.chkUseDefaultCredentials.AutoSize = true;
      this.chkUseDefaultCredentials.Location = new System.Drawing.Point(6, 55);
      this.chkUseDefaultCredentials.Name = "chkUseDefaultCredentials";
      this.chkUseDefaultCredentials.Size = new System.Drawing.Size(137, 17);
      this.chkUseDefaultCredentials.TabIndex = 4;
      this.chkUseDefaultCredentials.Text = "Use Default Credentials";
      this.chkUseDefaultCredentials.UseVisualStyleBackColor = true;
      this.chkUseDefaultCredentials.CheckedChanged += new System.EventHandler(this.chkUseDefaultCredentials_CheckedChanged);
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(3, 81);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(57, 13);
      this.lblUserName.TabIndex = 5;
      this.lblUserName.Text = "UserName";
      // 
      // txtUserName
      // 
      this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtUserName.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtUserName.Location = new System.Drawing.Point(66, 78);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(241, 20);
      this.txtUserName.TabIndex = 6;
      this.txtUserName.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtUserName.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // txtPassword
      // 
      this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtPassword.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtPassword.Location = new System.Drawing.Point(66, 104);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(241, 20);
      this.txtPassword.TabIndex = 8;
      this.txtPassword.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtPassword.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // txtDomain
      // 
      this.txtDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDomain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtDomain.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtDomain.Location = new System.Drawing.Point(66, 130);
      this.txtDomain.Name = "txtDomain";
      this.txtDomain.Size = new System.Drawing.Size(241, 20);
      this.txtDomain.TabIndex = 10;
      this.txtDomain.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtDomain.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(3, 107);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(53, 13);
      this.lblPassword.TabIndex = 7;
      this.lblPassword.Text = "Password";
      // 
      // lblDomain
      // 
      this.lblDomain.AutoSize = true;
      this.lblDomain.Location = new System.Drawing.Point(3, 133);
      this.lblDomain.Name = "lblDomain";
      this.lblDomain.Size = new System.Drawing.Size(43, 13);
      this.lblDomain.TabIndex = 9;
      this.lblDomain.Text = "Domain";
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(3, 32);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 2;
      this.lblPort.Text = "Port";
      // 
      // txtServer
      // 
      this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtServer.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtServer.Location = new System.Drawing.Point(66, 3);
      this.txtServer.Name = "txtServer";
      this.txtServer.Size = new System.Drawing.Size(241, 20);
      this.txtServer.TabIndex = 1;
      this.txtServer.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtServer.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // lblServer
      // 
      this.lblServer.AutoSize = true;
      this.lblServer.Location = new System.Drawing.Point(3, 6);
      this.lblServer.Name = "lblServer";
      this.lblServer.Size = new System.Drawing.Size(38, 13);
      this.lblServer.TabIndex = 0;
      this.lblServer.Text = "Server";
      // 
      // txtPort
      // 
      this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtPort.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtPort.HidePromptOnLeave = true;
      this.txtPort.Location = new System.Drawing.Point(66, 29);
      this.txtPort.Mask = "99999";
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(241, 20);
      this.txtPort.TabIndex = 3;
      this.txtPort.TextChanged += new System.EventHandler(this.maskedTextBox_TextChanged);
      this.txtPort.GotFocus += new System.EventHandler(this.maskedTextBox_GotFocus);
      // 
      // chkSecureConnection
      // 
      this.chkSecureConnection.AutoSize = true;
      this.chkSecureConnection.Location = new System.Drawing.Point(3, 156);
      this.chkSecureConnection.Name = "chkSecureConnection";
      this.chkSecureConnection.Size = new System.Drawing.Size(168, 17);
      this.chkSecureConnection.TabIndex = 11;
      this.chkSecureConnection.Text = "Use secure connection (https)";
      this.chkSecureConnection.UseVisualStyleBackColor = true;
      this.chkSecureConnection.CheckedChanged += new System.EventHandler(this.chkSecureConnection_CheckedChanged);
      // 
      // OnPremiseAuthenticationConfigurationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.Controls.Add(this.chkSecureConnection);
      this.Controls.Add(this.txtPort);
      this.Controls.Add(this.lblPort);
      this.Controls.Add(this.txtServer);
      this.Controls.Add(this.lblServer);
      this.Controls.Add(this.lblDomain);
      this.Controls.Add(this.lblPassword);
      this.Controls.Add(this.txtDomain);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.txtUserName);
      this.Controls.Add(this.lblUserName);
      this.Controls.Add(this.chkUseDefaultCredentials);
      this.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.Name = "OnPremiseAuthenticationConfigurationControl";
      this.Size = new System.Drawing.Size(310, 187);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox chkUseDefaultCredentials;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtDomain;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblDomain;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.TextBox txtServer;
    private System.Windows.Forms.Label lblServer;
    private System.Windows.Forms.MaskedTextBox txtPort;
    private System.Windows.Forms.CheckBox chkSecureConnection;
  }
}
