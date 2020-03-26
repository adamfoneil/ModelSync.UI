namespace ModelSync.App.Forms
{
    partial class frmSaveTestCase
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
            this.label2 = new System.Windows.Forms.Label();
            this.rbCorrect = new System.Windows.Forms.RadioButton();
            this.rbIncorrect = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIncorrectNotes = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "This will save the source and destination data models along with the generated sc" +
    "ript actions as a .zip file. No connection strings, data, or identifying info ar" +
    "e included.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please indicate whether the generated script is correct or not.";
            // 
            // rbCorrect
            // 
            this.rbCorrect.AutoSize = true;
            this.rbCorrect.Location = new System.Drawing.Point(125, 100);
            this.rbCorrect.Name = "rbCorrect";
            this.rbCorrect.Size = new System.Drawing.Size(68, 17);
            this.rbCorrect.TabIndex = 2;
            this.rbCorrect.TabStop = true;
            this.rbCorrect.Text = "Correct";
            this.rbCorrect.UseVisualStyleBackColor = true;
            this.rbCorrect.CheckedChanged += new System.EventHandler(this.rbCorrect_CheckedChanged);
            // 
            // rbIncorrect
            // 
            this.rbIncorrect.AutoSize = true;
            this.rbIncorrect.Location = new System.Drawing.Point(199, 100);
            this.rbIncorrect.Name = "rbIncorrect";
            this.rbIncorrect.Size = new System.Drawing.Size(172, 17);
            this.rbIncorrect.TabIndex = 3;
            this.rbIncorrect.TabStop = true;
            this.rbIncorrect.Text = "Incorrect, please explain:";
            this.rbIncorrect.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Generated SQL is";
            // 
            // tbIncorrectNotes
            // 
            this.tbIncorrectNotes.AcceptsReturn = true;
            this.tbIncorrectNotes.AcceptsTab = true;
            this.tbIncorrectNotes.Location = new System.Drawing.Point(12, 133);
            this.tbIncorrectNotes.MaxLength = 500;
            this.tbIncorrectNotes.Multiline = true;
            this.tbIncorrectNotes.Name = "tbIncorrectNotes";
            this.tbIncorrectNotes.Size = new System.Drawing.Size(375, 121);
            this.tbIncorrectNotes.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(167, 323);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Save as...";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 34);
            this.label4.TabIndex = 8;
            this.label4.Text = "Please send test case zip files to adam@aosoftware.net. Thank you!";
            // 
            // frmSaveTestCase
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(399, 358);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbIncorrectNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbIncorrect);
            this.Controls.Add(this.rbCorrect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSaveTestCase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save Test Case";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbCorrect;
        private System.Windows.Forms.RadioButton rbIncorrect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIncorrectNotes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
    }
}