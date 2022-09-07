namespace Finx.App.Forms
{
    partial class frmMetroPolicyFunds
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.dataGrid_PolicyDetails = new SourceGrid.DataGrid();
            this.metroTabControl_PolicyMain = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.dataGrid_PolicyFunds = new SourceGrid.DataGrid();
            this.metroTabPage2 = new System.Windows.Forms.TabPage();
            this.dataGrid_Beneficiaries = new SourceGrid.DataGrid();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.dataGrid_Comments = new SourceGrid.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroTabControl_PolicyMain.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
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
            this.xToolBarMenu1.Size = new System.Drawing.Size(1083, 46);
            this.xToolBarMenu1.TabIndex = 0;
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
            this.splitContainer1.Size = new System.Drawing.Size(1083, 319);
            this.splitContainer1.SplitterDistance = 85;
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
            this.metroPanel1.Size = new System.Drawing.Size(1083, 85);
            this.metroPanel1.TabIndex = 6;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
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
            this.dataGrid_PolicyDetails.Size = new System.Drawing.Size(1083, 80);
            this.dataGrid_PolicyDetails.TabIndex = 10;
            this.dataGrid_PolicyDetails.TabStop = true;
            this.dataGrid_PolicyDetails.ToolTipText = "";
            // 
            // metroTabControl_PolicyMain
            // 
            this.metroTabControl_PolicyMain.Controls.Add(this.metroTabPage1);
            this.metroTabControl_PolicyMain.Controls.Add(this.metroTabPage2);
            this.metroTabControl_PolicyMain.Controls.Add(this.metroTabPage3);
            this.metroTabControl_PolicyMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl_PolicyMain.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl_PolicyMain.Name = "metroTabControl_PolicyMain";
            this.metroTabControl_PolicyMain.SelectedIndex = 0;
            this.metroTabControl_PolicyMain.Size = new System.Drawing.Size(1083, 230);
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
            this.metroTabPage1.Size = new System.Drawing.Size(1075, 188);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Funds";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
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
            this.dataGrid_PolicyFunds.Size = new System.Drawing.Size(1075, 188);
            this.dataGrid_PolicyFunds.TabIndex = 11;
            this.dataGrid_PolicyFunds.TabStop = true;
            this.dataGrid_PolicyFunds.ToolTipText = "";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.dataGrid_Beneficiaries);
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1075, 217);
            this.metroTabPage2.TabIndex = 2;
            this.metroTabPage2.Text = "Beneficiaries";
            // 
            // dataGrid_Beneficiaries
            // 
            this.dataGrid_Beneficiaries.BackColor = System.Drawing.Color.White;
            this.dataGrid_Beneficiaries.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Beneficiaries.DeleteRowsWithDeleteKey = false;
            this.dataGrid_Beneficiaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Beneficiaries.EnableSort = false;
            this.dataGrid_Beneficiaries.FixedColumns = 1;
            this.dataGrid_Beneficiaries.FixedRows = 1;
            this.dataGrid_Beneficiaries.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Beneficiaries.Name = "dataGrid_Beneficiaries";
            this.dataGrid_Beneficiaries.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Beneficiaries.Size = new System.Drawing.Size(1075, 217);
            this.dataGrid_Beneficiaries.TabIndex = 12;
            this.dataGrid_Beneficiaries.TabStop = true;
            this.dataGrid_Beneficiaries.ToolTipText = "";
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.dataGrid_Comments);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1075, 217);
            this.metroTabPage3.TabIndex = 1;
            this.metroTabPage3.Text = "Comments";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // dataGrid_Comments
            // 
            this.dataGrid_Comments.BackColor = System.Drawing.Color.White;
            this.dataGrid_Comments.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Comments.DeleteRowsWithDeleteKey = false;
            this.dataGrid_Comments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Comments.EnableSort = false;
            this.dataGrid_Comments.FixedColumns = 1;
            this.dataGrid_Comments.FixedRows = 1;
            this.dataGrid_Comments.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Comments.Name = "dataGrid_Comments";
            this.dataGrid_Comments.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Comments.Size = new System.Drawing.Size(1075, 217);
            this.dataGrid_Comments.TabIndex = 11;
            this.dataGrid_Comments.TabStop = true;
            this.dataGrid_Comments.ToolTipText = "";
            // 
            // frmMetroPolicyFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 445);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroPolicyFunds";
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
            this.metroTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private SourceGrid.DataGrid dataGrid_PolicyDetails;
        private MetroFramework.Controls.MetroTabControl metroTabControl_PolicyMain;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private SourceGrid.DataGrid dataGrid_PolicyFunds;
        private System.Windows.Forms.TabPage metroTabPage2;
        private SourceGrid.DataGrid dataGrid_Beneficiaries;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private SourceGrid.DataGrid dataGrid_Comments;
    }
}