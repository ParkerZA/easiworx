namespace Finx.App.Forms
{
    partial class frmClientDocuments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientDocuments));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xInput_ClientDocFolder = new Finx.App.UserControls.xInput();
            this.Search = new System.Windows.Forms.Label();
            this.tbFilter = new DevAge.Windows.Forms.DevAgeTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.xFileView1 = new Finx.App.UserControls.xFileView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.clientDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xInput_ClientDocFolder);
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Search);
            this.splitContainer1.Panel2.Controls.Add(this.tbFilter);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // xInput_ClientDocFolder
            // 
            this.xInput_ClientDocFolder.BackColor = System.Drawing.Color.Transparent;
            this.xInput_ClientDocFolder.ControlTypes = Finx.App.UserControls.ControlTypes.FolderBox;
            this.xInput_ClientDocFolder.ControlWidth = 400;
            this.xInput_ClientDocFolder.DataSource = null;
            this.xInput_ClientDocFolder.DisplayMember = "ListText";
            resources.ApplyResources(this.xInput_ClientDocFolder, "xInput_ClientDocFolder");
            this.xInput_ClientDocFolder.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_ClientDocFolder.LableText = "Client\'s document folder ...";
            this.xInput_ClientDocFolder.MappedField = null;
            this.xInput_ClientDocFolder.Model = null;
            this.xInput_ClientDocFolder.Name = "xInput_ClientDocFolder";
            this.xInput_ClientDocFolder.ReadOnly = false;
            this.xInput_ClientDocFolder.Value = "";
            this.xInput_ClientDocFolder.ValueMember = "ListValue";
            // 
            // Search
            // 
            resources.ApplyResources(this.Search, "Search");
            this.Search.Name = "Search";
            // 
            // tbFilter
            // 
            this.tbFilter.AcceptsReturn = true;
            resources.ApplyResources(this.tbFilter, "tbFilter");
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xInput_Filename_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Menu;
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.xFileView1);
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            // 
            // xFileView1
            // 
            resources.ApplyResources(this.xFileView1, "xFileView1");
            this.xFileView1.Name = "xFileView1";
            this.xFileView1.Load += new System.EventHandler(this.xFileView1_Load);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.clientDocumentsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromScannerToolStripMenuItem,
            this.fromFolderToolStripMenuItem});
            this.importToolStripMenuItem.Image = global::easiplan.app.Properties.Resources.upload_file;
            resources.ApplyResources(this.importToolStripMenuItem, "importToolStripMenuItem");
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            // 
            // fromScannerToolStripMenuItem
            // 
            this.fromScannerToolStripMenuItem.Name = "fromScannerToolStripMenuItem";
            resources.ApplyResources(this.fromScannerToolStripMenuItem, "fromScannerToolStripMenuItem");
            // 
            // fromFolderToolStripMenuItem
            // 
            this.fromFolderToolStripMenuItem.Name = "fromFolderToolStripMenuItem";
            resources.ApplyResources(this.fromFolderToolStripMenuItem, "fromFolderToolStripMenuItem");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeToolStripMenuItem.Image = global::easiplan.app.Properties.Resources.exit_member_icon;
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // clientDocumentsToolStripMenuItem
            // 
            this.clientDocumentsToolStripMenuItem.Name = "clientDocumentsToolStripMenuItem";
            resources.ApplyResources(this.clientDocumentsToolStripMenuItem, "clientDocumentsToolStripMenuItem");
            // 
            // frmClientDocuments
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClientDocuments";
            this.Load += new System.EventHandler(this.frmClientDocuments_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFolderToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.xInput xInput_ClientDocFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBox1;
        private UserControls.xFileView xFileView1;
        private System.Windows.Forms.Label Search;
        private DevAge.Windows.Forms.DevAgeTextBox tbFilter;
        private System.Windows.Forms.ToolStripMenuItem clientDocumentsToolStripMenuItem;
    }
}