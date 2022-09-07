
namespace Finx.App.Forms
{
    partial class frmMetroPolicyNotes
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
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.metroPanel_PolicyNote = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Select = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel_Select.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(716, 46);
            this.xToolBarMenu1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 106);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_PolicyNote);
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_Select);
            this.splitContainer1.Size = new System.Drawing.Size(716, 485);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 7;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(25, 460);
            this.treeView1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(25, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // metroPanel_PolicyNote
            // 
            this.metroPanel_PolicyNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_PolicyNote.HorizontalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.HorizontalScrollbarSize = 10;
            this.metroPanel_PolicyNote.Location = new System.Drawing.Point(0, 47);
            this.metroPanel_PolicyNote.Name = "metroPanel_PolicyNote";
            this.metroPanel_PolicyNote.Size = new System.Drawing.Size(687, 438);
            this.metroPanel_PolicyNote.TabIndex = 9;
            this.metroPanel_PolicyNote.VerticalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.VerticalScrollbarSize = 10;
            // 
            // metroPanel_Select
            // 
            this.metroPanel_Select.Controls.Add(this.metroPanel1);
            this.metroPanel_Select.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Select.HorizontalScrollbarBarColor = true;
            this.metroPanel_Select.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.HorizontalScrollbarSize = 10;
            this.metroPanel_Select.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Select.Name = "metroPanel_Select";
            this.metroPanel_Select.Size = new System.Drawing.Size(687, 47);
            this.metroPanel_Select.TabIndex = 8;
            this.metroPanel_Select.VerticalScrollbarBarColor = true;
            this.metroPanel_Select.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(687, 10);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // frmMetroPolicyNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 611);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroPolicyNotes";
            this.Text = "frmMetroPolicyNotes";
            this.Load += new System.EventHandler(this.frmMetroAdminTaskAdd_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.metroPanel_Select.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MetroFramework.Controls.MetroPanel metroPanel_PolicyNote;
        private MetroFramework.Controls.MetroPanel metroPanel_Select;
        private MetroFramework.Controls.MetroPanel metroPanel1;
    }
}