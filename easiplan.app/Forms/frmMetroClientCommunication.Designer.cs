namespace Finx.App.Forms
{
    partial class frmMetroClientCommunication
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
            this.metroPanel_Search = new MetroFramework.Controls.MetroPanel();
            this.dataGrid_ClientSegmentation = new SourceGrid.DataGrid();
            this.SuspendLayout();
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.BackColor = System.Drawing.Color.Transparent;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(895, 46);
            this.xToolBarMenu1.TabIndex = 1;
            // 
            // metroPanel_Search
            // 
            this.metroPanel_Search.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Search.HorizontalScrollbarBarColor = true;
            this.metroPanel_Search.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Search.HorizontalScrollbarSize = 10;
            this.metroPanel_Search.Location = new System.Drawing.Point(20, 106);
            this.metroPanel_Search.Name = "metroPanel_Search";
            this.metroPanel_Search.Size = new System.Drawing.Size(895, 50);
            this.metroPanel_Search.TabIndex = 3;
            this.metroPanel_Search.VerticalScrollbarBarColor = true;
            this.metroPanel_Search.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Search.VerticalScrollbarSize = 10;
            // 
            // dataGrid_ClientSegmentation
            // 
            this.dataGrid_ClientSegmentation.BackColor = System.Drawing.Color.White;
            this.dataGrid_ClientSegmentation.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_ClientSegmentation.DeleteRowsWithDeleteKey = false;
            this.dataGrid_ClientSegmentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_ClientSegmentation.EnableSort = false;
            this.dataGrid_ClientSegmentation.FixedColumns = 1;
            this.dataGrid_ClientSegmentation.FixedRows = 1;
            this.dataGrid_ClientSegmentation.Location = new System.Drawing.Point(20, 156);
            this.dataGrid_ClientSegmentation.Name = "dataGrid_ClientSegmentation";
            this.dataGrid_ClientSegmentation.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_ClientSegmentation.Size = new System.Drawing.Size(895, 201);
            this.dataGrid_ClientSegmentation.TabIndex = 8;
            this.dataGrid_ClientSegmentation.TabStop = true;
            this.dataGrid_ClientSegmentation.ToolTipText = "";
            // 
            // frmMetroClientSegmentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 377);
            this.Controls.Add(this.dataGrid_ClientSegmentation);
            this.Controls.Add(this.metroPanel_Search);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroClientSegmentation";
            this.Text = "frmMetroCLientSegmentation";
            this.Load += new System.EventHandler(this.frmMetroClientCommunication_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private MetroFramework.Controls.MetroPanel metroPanel_Search;
        private SourceGrid.DataGrid dataGrid_ClientSegmentation;
    }
}