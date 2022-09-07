namespace Finx.App.Forms
{
    partial class frmMetroPolicyHistory
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroTabControl_PolicyMain = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.dataGrid_PolicyDetails = new SourceGrid.DataGrid();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.dataGrid_PolicyFunds = new SourceGrid.DataGrid();
            this.dataGrid_PolicyNotes = new SourceGrid.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroTabControl_PolicyMain.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 106);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.metroPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroTabControl_PolicyMain);
            this.splitContainer1.Size = new System.Drawing.Size(1035, 319);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 7;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.dataGrid_PolicyDetails);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.metroPanel1.Size = new System.Drawing.Size(1035, 132);
            this.metroPanel1.TabIndex = 6;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroTabControl_PolicyMain
            // 
            this.metroTabControl_PolicyMain.Controls.Add(this.metroTabPage1);
            this.metroTabControl_PolicyMain.Controls.Add(this.metroTabPage2);
            this.metroTabControl_PolicyMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl_PolicyMain.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl_PolicyMain.Name = "metroTabControl_PolicyMain";
            this.metroTabControl_PolicyMain.SelectedIndex = 0;
            this.metroTabControl_PolicyMain.Size = new System.Drawing.Size(1035, 183);
            this.metroTabControl_PolicyMain.TabIndex = 7;
            this.metroTabControl_PolicyMain.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.dataGrid_PolicyFunds);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1027, 141);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Funds";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.dataGrid_PolicyNotes);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1027, 141);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Notes";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // dataGrid_PolicyDetails
            // 
            this.dataGrid_PolicyDetails.BackColor = System.Drawing.Color.White;
            this.dataGrid_PolicyDetails.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_PolicyDetails.DeleteRowsWithDeleteKey = false;
            this.dataGrid_PolicyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_PolicyDetails.EnableSort = false;
            this.dataGrid_PolicyDetails.FixedColumns = 1;
            this.dataGrid_PolicyDetails.FixedRows = 1;
            this.dataGrid_PolicyDetails.Location = new System.Drawing.Point(0, 5);
            this.dataGrid_PolicyDetails.Name = "dataGrid_PolicyDetails";
            this.dataGrid_PolicyDetails.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_PolicyDetails.Size = new System.Drawing.Size(1035, 127);
            this.dataGrid_PolicyDetails.TabIndex = 12;
            this.dataGrid_PolicyDetails.TabStop = true;
            this.dataGrid_PolicyDetails.ToolTipText = "";
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(1035, 46);
            this.xToolBarMenu1.TabIndex = 0;
            // 
            // dataGrid_PolicyFunds
            // 
            this.dataGrid_PolicyFunds.BackColor = System.Drawing.Color.White;
            this.dataGrid_PolicyFunds.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_PolicyFunds.DeleteRowsWithDeleteKey = false;
            this.dataGrid_PolicyFunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_PolicyFunds.EnableSort = false;
            this.dataGrid_PolicyFunds.FixedColumns = 1;
            this.dataGrid_PolicyFunds.FixedRows = 1;
            this.dataGrid_PolicyFunds.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_PolicyFunds.Name = "dataGrid_PolicyFunds";
            this.dataGrid_PolicyFunds.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_PolicyFunds.Size = new System.Drawing.Size(1027, 141);
            this.dataGrid_PolicyFunds.TabIndex = 13;
            this.dataGrid_PolicyFunds.TabStop = true;
            this.dataGrid_PolicyFunds.ToolTipText = "";
            // 
            // dataGrid_PolicyNotes
            // 
            this.dataGrid_PolicyNotes.BackColor = System.Drawing.Color.White;
            this.dataGrid_PolicyNotes.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_PolicyNotes.DeleteRowsWithDeleteKey = false;
            this.dataGrid_PolicyNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_PolicyNotes.EnableSort = false;
            this.dataGrid_PolicyNotes.FixedColumns = 1;
            this.dataGrid_PolicyNotes.FixedRows = 1;
            this.dataGrid_PolicyNotes.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_PolicyNotes.Name = "dataGrid_PolicyNotes";
            this.dataGrid_PolicyNotes.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_PolicyNotes.Size = new System.Drawing.Size(1027, 141);
            this.dataGrid_PolicyNotes.TabIndex = 13;
            this.dataGrid_PolicyNotes.TabStop = true;
            this.dataGrid_PolicyNotes.ToolTipText = "";
            // 
            // frmMetroPolicyHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 445);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroPolicyHistory";
            this.Text = "frmMetroPolicyFunds";
            this.Load += new System.EventHandler(this.frmMetroPolicyFunds_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroTabControl_PolicyMain.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl_PolicyMain;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private SourceGrid.DataGrid dataGrid_PolicyDetails;
        private SourceGrid.DataGrid dataGrid_PolicyFunds;
        private SourceGrid.DataGrid dataGrid_PolicyNotes;
    }
}