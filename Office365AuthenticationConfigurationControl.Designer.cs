namespace MicrosoftDynamicsCRMPlugin
{
  partial class Office365AuthenticationConfigurationControl
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
      this.lblPassword = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.lblUserName = new System.Windows.Forms.Label();
      this.lblLocation = new System.Windows.Forms.Label();
      this.comboLocation = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(3, 32);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(53, 13);
      this.lblPassword.TabIndex = 2;
      this.lblPassword.Text = "Password";
      // 
      // txtPassword
      // 
      this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtPassword.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtPassword.Location = new System.Drawing.Point(66, 29);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(193, 20);
      this.txtPassword.TabIndex = 3;
      this.txtPassword.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtPassword.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // txtUserName
      // 
      this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.txtUserName.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.txtUserName.Location = new System.Drawing.Point(66, 3);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(193, 20);
      this.txtUserName.TabIndex = 1;
      this.txtUserName.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      this.txtUserName.GotFocus += new System.EventHandler(this.textBox_GotFocus);
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(3, 6);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(57, 13);
      this.lblUserName.TabIndex = 0;
      this.lblUserName.Text = "UserName";
      // 
      // lblLocation
      // 
      this.lblLocation.AutoSize = true;
      this.lblLocation.Location = new System.Drawing.Point(3, 58);
      this.lblLocation.Name = "lblLocation";
      this.lblLocation.Size = new System.Drawing.Size(48, 13);
      this.lblLocation.TabIndex = 4;
      this.lblLocation.Text = "Location";
      // 
      // comboLocation
      // 
      this.comboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.comboLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.comboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboLocation.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.comboLocation.FormattingEnabled = true;
      this.comboLocation.Location = new System.Drawing.Point(66, 55);
      this.comboLocation.Name = "comboLocation";
      this.comboLocation.Size = new System.Drawing.Size(193, 21);
      this.comboLocation.TabIndex = 5;
      this.comboLocation.SelectedIndexChanged += new System.EventHandler(this.comboLocation_SelectedIndexChanged);
      // 
      // Office365AuthenticationConfigurationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
      this.Controls.Add(this.comboLocation);
      this.Controls.Add(this.lblLocation);
      this.Controls.Add(this.lblPassword);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.txtUserName);
      this.Controls.Add(this.lblUserName);
      this.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.Name = "Office365AuthenticationConfigurationControl";
      this.Size = new System.Drawing.Size(262, 91);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.ComboBox comboLocation;
  }
}
