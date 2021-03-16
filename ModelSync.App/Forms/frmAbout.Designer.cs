namespace ModelSync.App.Forms
{
    partial class frmAbout
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
            this.label1 = new System.Windows.Forms.Label();
            this.webUrlLinkLabel1 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.webUrlLinkLabel2 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llSaveFolder = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.webUrlLinkLabel3 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.webUrlLinkLabel4 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ModelSync";
            // 
            // webUrlLinkLabel1
            // 
            this.webUrlLinkLabel1.AutoSize = true;
            this.webUrlLinkLabel1.Location = new System.Drawing.Point(143, 19);
            this.webUrlLinkLabel1.Name = "webUrlLinkLabel1";
            this.webUrlLinkLabel1.Size = new System.Drawing.Size(147, 13);
            this.webUrlLinkLabel1.TabIndex = 1;
            this.webUrlLinkLabel1.TabStop = true;
            this.webUrlLinkLabel1.Text = "by Adam O\'Neil (GitHub)";
            this.webUrlLinkLabel1.Url = "https://github.com/adamfoneil";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(361, 19);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(41, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Download and Install";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.lblNewVersion);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 41);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(12, 13);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(169, 13);
            this.lblNewVersion.TabIndex = 5;
            this.lblNewVersion.Text = "Version {version} available:";
            // 
            // webUrlLinkLabel2
            // 
            this.webUrlLinkLabel2.AutoSize = true;
            this.webUrlLinkLabel2.Location = new System.Drawing.Point(25, 122);
            this.webUrlLinkLabel2.Name = "webUrlLinkLabel2";
            this.webUrlLinkLabel2.Size = new System.Drawing.Size(247, 13);
            this.webUrlLinkLabel2.TabIndex = 6;
            this.webUrlLinkLabel2.TabStop = true;
            this.webUrlLinkLabel2.Text = "https://github.com/adamfoneil/ModelSync";
            this.webUrlLinkLabel2.Url = "https://github.com/adamfoneil/ModelSync";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Diff capabilities provided by this C# library:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Local save folder:";
            // 
            // llSaveFolder
            // 
            this.llSaveFolder.AutoSize = true;
            this.llSaveFolder.Location = new System.Drawing.Point(25, 170);
            this.llSaveFolder.Name = "llSaveFolder";
            this.llSaveFolder.Size = new System.Drawing.Size(106, 13);
            this.llSaveFolder.TabIndex = 9;
            this.llSaveFolder.TabStop = true;
            this.llSaveFolder.Text = "webUrlLinkLabel3";
            this.llSaveFolder.Url = null;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "WinForm app links:";
            // 
            // webUrlLinkLabel3
            // 
            this.webUrlLinkLabel3.AutoSize = true;
            this.webUrlLinkLabel3.Location = new System.Drawing.Point(25, 70);
            this.webUrlLinkLabel3.Name = "webUrlLinkLabel3";
            this.webUrlLinkLabel3.Size = new System.Drawing.Size(82, 13);
            this.webUrlLinkLabel3.TabIndex = 11;
            this.webUrlLinkLabel3.TabStop = true;
            this.webUrlLinkLabel3.Text = "Product Page";
            this.webUrlLinkLabel3.Url = "https://aosoftware.net/modelsync/";
            // 
            // webUrlLinkLabel4
            // 
            this.webUrlLinkLabel4.AutoSize = true;
            this.webUrlLinkLabel4.Location = new System.Drawing.Point(113, 70);
            this.webUrlLinkLabel4.Name = "webUrlLinkLabel4";
            this.webUrlLinkLabel4.Size = new System.Drawing.Size(78, 13);
            this.webUrlLinkLabel4.TabIndex = 12;
            this.webUrlLinkLabel4.TabStop = true;
            this.webUrlLinkLabel4.Text = "GitHub Repo";
            this.webUrlLinkLabel4.Url = "https://github.com/adamfoneil/ModelSync.WinForms";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 235);
            this.Controls.Add(this.webUrlLinkLabel4);
            this.Controls.Add(this.webUrlLinkLabel3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.llSaveFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.webUrlLinkLabel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.webUrlLinkLabel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About ModelSync";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewVersion;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private WinForms.Library.Controls.WebUrlLinkLabel llSaveFolder;
        private System.Windows.Forms.Label label5;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel3;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel4;
    }
}