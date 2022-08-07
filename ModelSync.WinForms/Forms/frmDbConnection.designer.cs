namespace WinFormsApp
{
	partial class frmDbConnection
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cbDatabase = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.rbAuthenticationWindows = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.rbAuthenticationDb = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnTest = new System.Windows.Forms.Button();
			this.cbServer = new System.Windows.Forms.ComboBox();
			this.cbUser = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.llCreateDb = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(383, 238);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 23);
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(302, 238);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(65, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			// 
			// cbDatabase
			// 
			this.cbDatabase.FormattingEnabled = true;
			this.cbDatabase.Location = new System.Drawing.Point(122, 178);
			this.cbDatabase.Name = "cbDatabase";
			this.cbDatabase.Size = new System.Drawing.Size(348, 21);
			this.cbDatabase.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(50, 181);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Database:";
			// 
			// rbAuthenticationWindows
			// 
			this.rbAuthenticationWindows.AutoSize = true;
			this.rbAuthenticationWindows.Location = new System.Drawing.Point(122, 39);
			this.rbAuthenticationWindows.Name = "rbAuthenticationWindows";
			this.rbAuthenticationWindows.Size = new System.Drawing.Size(75, 17);
			this.rbAuthenticationWindows.TabIndex = 3;
			this.rbAuthenticationWindows.TabStop = true;
			this.rbAuthenticationWindows.Text = "Windows";
			this.rbAuthenticationWindows.UseVisualStyleBackColor = true;
			this.rbAuthenticationWindows.CheckedChanged += new System.EventHandler(this.rbAuthenticationWindows_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Authentication:";
			// 
			// rbAuthenticationDb
			// 
			this.rbAuthenticationDb.AutoSize = true;
			this.rbAuthenticationDb.Location = new System.Drawing.Point(122, 62);
			this.rbAuthenticationDb.Name = "rbAuthenticationDb";
			this.rbAuthenticationDb.Size = new System.Drawing.Size(79, 17);
			this.rbAuthenticationDb.TabIndex = 4;
			this.rbAuthenticationDb.TabStop = true;
			this.rbAuthenticationDb.Text = "Database";
			this.rbAuthenticationDb.UseVisualStyleBackColor = true;
			this.rbAuthenticationDb.CheckedChanged += new System.EventHandler(this.rbAuthenticationDb_CheckedChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(41, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "User Name:";
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(122, 112);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(168, 21);
			this.tbPassword.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(50, 115);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Password:";
			// 
			// btnTest
			// 
			this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTest.Location = new System.Drawing.Point(221, 238);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 12;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// cbServer
			// 
			this.cbServer.FormattingEnabled = true;
			this.cbServer.Location = new System.Drawing.Point(122, 12);
			this.cbServer.Name = "cbServer";
			this.cbServer.Size = new System.Drawing.Size(348, 21);
			this.cbServer.TabIndex = 1;
			this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
			// 
			// cbUser
			// 
			this.cbUser.FormattingEnabled = true;
			this.cbUser.Location = new System.Drawing.Point(122, 85);
			this.cbUser.Name = "cbUser";
			this.cbUser.Size = new System.Drawing.Size(168, 21);
			this.cbUser.TabIndex = 6;
			this.cbUser.SelectedIndexChanged += new System.EventHandler(this.cbUser_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(125, 141);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(345, 27);
			this.label6.TabIndex = 15;
			this.label6.Text = "Connection strings with passwords are always encrypted and hidden from display.";
			// 
			// llCreateDb
			// 
			this.llCreateDb.AutoSize = true;
			this.llCreateDb.Location = new System.Drawing.Point(119, 202);
			this.llCreateDb.Name = "llCreateDb";
			this.llCreateDb.Size = new System.Drawing.Size(116, 13);
			this.llCreateDb.TabIndex = 16;
			this.llCreateDb.TabStop = true;
			this.llCreateDb.Text = "Create Database...";
			this.llCreateDb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCreateDb_LinkClicked);
			// 
			// frmDbConnection
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(482, 273);
			this.ControlBox = false;
			this.Controls.Add(this.llCreateDb);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cbUser);
			this.Controls.Add(this.cbServer);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbPassword);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.rbAuthenticationDb);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.rbAuthenticationWindows);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cbDatabase);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDbConnection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Database Connection";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDbConnection_FormClosing);
			this.Load += new System.EventHandler(this.frmDbConnection_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbDatabase;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton rbAuthenticationWindows;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton rbAuthenticationDb;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.ComboBox cbServer;
		private System.Windows.Forms.ComboBox cbUser;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel llCreateDb;
	}
}