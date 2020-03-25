namespace ModelSync.App.Controls
{
    partial class SyncUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbDest = new WinForms.Library.Controls.BuilderTextBox();
            this.tbSource = new WinForms.Library.Controls.BuilderTextBox();
            this.cmAssemblies = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSourceType = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvObjects = new System.Windows.Forms.TreeView();
            this.cmDiff = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.includeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excludeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tbScriptOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGenerateScript = new System.Windows.Forms.ToolStripButton();
            this.btnExecute = new System.Windows.Forms.ToolStripButton();
            this.ddbSave = new System.Windows.Forms.ToolStripSplitButton();
            this.testCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.cmConnections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            this.cmAssemblies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmDiff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbScriptOutput)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbDest);
            this.panel1.Controls.Add(this.tbSource);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbSourceType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 73);
            this.panel1.TabIndex = 0;
            // 
            // tbDest
            // 
            this.tbDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDest.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDest.Location = new System.Drawing.Point(164, 41);
            this.tbDest.Name = "tbDest";
            this.tbDest.Size = new System.Drawing.Size(507, 26);
            this.tbDest.Suggestions = null;
            this.tbDest.TabIndex = 5;
            this.tbDest.Value = "";
            // 
            // tbSource
            // 
            this.tbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSource.ContextMenuStrip = this.cmAssemblies;
            this.tbSource.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSource.Location = new System.Drawing.Point(164, 9);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(507, 26);
            this.tbSource.Suggestions = null;
            this.tbSource.TabIndex = 4;
            this.tbSource.Value = "";
            this.tbSource.BuilderClicked += new WinForms.Library.Controls.BuilderEventHandler(this.tbSource_BuilderClicked);
            // 
            // cmAssemblies
            // 
            this.cmAssemblies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAssemblyToolStripMenuItem});
            this.cmAssemblies.Name = "cmAssemblies";
            this.cmAssemblies.Size = new System.Drawing.Size(169, 26);
            // 
            // selectAssemblyToolStripMenuItem
            // 
            this.selectAssemblyToolStripMenuItem.Name = "selectAssemblyToolStripMenuItem";
            this.selectAssemblyToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.selectAssemblyToolStripMenuItem.Text = "Select Assembly...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "To Connection:";
            // 
            // cbSourceType
            // 
            this.cbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceType.FormattingEnabled = true;
            this.cbSourceType.Location = new System.Drawing.Point(17, 12);
            this.cbSourceType.Name = "cbSourceType";
            this.cbSourceType.Size = new System.Drawing.Size(141, 21);
            this.cbSourceType.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvObjects);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbScriptOutput);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(682, 268);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvObjects
            // 
            this.tvObjects.ContextMenuStrip = this.cmDiff;
            this.tvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObjects.ImageIndex = 0;
            this.tvObjects.ImageList = this.imageList1;
            this.tvObjects.Location = new System.Drawing.Point(0, 25);
            this.tvObjects.Name = "tvObjects";
            this.tvObjects.SelectedImageIndex = 0;
            this.tvObjects.Size = new System.Drawing.Size(227, 243);
            this.tvObjects.TabIndex = 0;
            this.tvObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjects_AfterSelect);
            this.tvObjects.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvObjects_NodeMouseClick);
            // 
            // cmDiff
            // 
            this.cmDiff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.includeToolStripMenuItem,
            this.excludeToolStripMenuItem,
            this.toolStripSeparator1,
            this.setDefaultToolStripMenuItem});
            this.cmDiff.Name = "cmDiff";
            this.cmDiff.Size = new System.Drawing.Size(181, 98);
            this.cmDiff.Opening += new System.ComponentModel.CancelEventHandler(this.cmDiff_Opening);
            // 
            // includeToolStripMenuItem
            // 
            this.includeToolStripMenuItem.Name = "includeToolStripMenuItem";
            this.includeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.includeToolStripMenuItem.Text = "Include";
            this.includeToolStripMenuItem.Click += new System.EventHandler(this.includeToolStripMenuItem_Click);
            // 
            // excludeToolStripMenuItem
            // 
            this.excludeToolStripMenuItem.Name = "excludeToolStripMenuItem";
            this.excludeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.excludeToolStripMenuItem.Text = "Exclude";
            this.excludeToolStripMenuItem.Click += new System.EventHandler(this.excludeToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "create");
            this.imageList1.Images.SetKeyName(1, "delete");
            this.imageList1.Images.SetKeyName(2, "update");
            this.imageList1.Images.SetKeyName(3, "database");
            this.imageList1.Images.SetKeyName(4, "script");
            this.imageList1.Images.SetKeyName(5, "table");
            this.imageList1.Images.SetKeyName(6, "schema");
            this.imageList1.Images.SetKeyName(7, "shortcut");
            this.imageList1.Images.SetKeyName(8, "column");
            this.imageList1.Images.SetKeyName(9, "key");
            this.imageList1.Images.SetKeyName(10, "exclude");
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(227, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tbScriptOutput
            // 
            this.tbScriptOutput.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.tbScriptOutput.AutoIndentCharsPatterns = "";
            this.tbScriptOutput.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.tbScriptOutput.BackBrush = null;
            this.tbScriptOutput.CharHeight = 14;
            this.tbScriptOutput.CharWidth = 8;
            this.tbScriptOutput.CommentPrefix = "--";
            this.tbScriptOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbScriptOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbScriptOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScriptOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbScriptOutput.IsReplaceMode = false;
            this.tbScriptOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbScriptOutput.LeftBracket = '(';
            this.tbScriptOutput.Location = new System.Drawing.Point(0, 25);
            this.tbScriptOutput.Name = "tbScriptOutput";
            this.tbScriptOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.tbScriptOutput.RightBracket = ')';
            this.tbScriptOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbScriptOutput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbScriptOutput.ServiceColors")));
            this.tbScriptOutput.Size = new System.Drawing.Size(450, 243);
            this.tbScriptOutput.TabIndex = 0;
            this.tbScriptOutput.WordWrap = true;
            this.tbScriptOutput.Zoom = 100;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerateScript,
            this.btnExecute,
            this.ddbSave,
            this.btnCopy});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(450, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGenerateScript
            // 
            this.btnGenerateScript.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateScript.Image")));
            this.btnGenerateScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateScript.Name = "btnGenerateScript";
            this.btnGenerateScript.Size = new System.Drawing.Size(107, 22);
            this.btnGenerateScript.Text = "Generate Script";
            this.btnGenerateScript.Click += new System.EventHandler(this.btnGenerateScript_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(68, 22);
            this.btnExecute.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // ddbSave
            // 
            this.ddbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testCaseToolStripMenuItem});
            this.ddbSave.Image = ((System.Drawing.Image)(resources.GetObject("ddbSave.Image")));
            this.ddbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSave.Name = "ddbSave";
            this.ddbSave.Size = new System.Drawing.Size(32, 22);
            this.ddbSave.Text = "toolStripSplitButton1";
            this.ddbSave.ButtonClick += new System.EventHandler(this.ddbSave_ButtonClick);
            // 
            // testCaseToolStripMenuItem
            // 
            this.testCaseToolStripMenuItem.Name = "testCaseToolStripMenuItem";
            this.testCaseToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.testCaseToolStripMenuItem.Text = "Save Test Case...";
            this.testCaseToolStripMenuItem.Click += new System.EventHandler(this.testCaseToolStripMenuItem_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "toolStripButton1";
            this.btnCopy.ToolTipText = "Copy SQL";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cmConnections
            // 
            this.cmConnections.Name = "cmConnections";
            this.cmConnections.Size = new System.Drawing.Size(61, 4);
            // 
            // setDefaultToolStripMenuItem
            // 
            this.setDefaultToolStripMenuItem.Name = "setDefaultToolStripMenuItem";
            this.setDefaultToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setDefaultToolStripMenuItem.Text = "Set Default...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // SyncUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SyncUI";
            this.Size = new System.Drawing.Size(682, 341);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmAssemblies.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmDiff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbScriptOutput)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSourceType;
        private System.Windows.Forms.TreeView tvObjects;
        private FastColoredTextBoxNS.FastColoredTextBox tbScriptOutput;
        private WinForms.Library.Controls.BuilderTextBox tbDest;
        private WinForms.Library.Controls.BuilderTextBox tbSource;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGenerateScript;
        private System.Windows.Forms.ToolStripButton btnExecute;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip cmAssemblies;
        private System.Windows.Forms.ToolStripMenuItem selectAssemblyToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmConnections;
        private System.Windows.Forms.ContextMenuStrip cmDiff;
        private System.Windows.Forms.ToolStripMenuItem includeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excludeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripSplitButton ddbSave;
        private System.Windows.Forms.ToolStripMenuItem testCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem setDefaultToolStripMenuItem;
    }
}
