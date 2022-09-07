namespace Finx.App.UserControls
{
    partial class xListView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DataGridList = new SourceGrid.DataGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(417, 37);
            this.panel1.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSearch.Font = new System.Drawing.Font("Arial Unicode MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(5, 5);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(15);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(407, 25);
            this.textBoxSearch.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DataGridList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(417, 209);
            this.panel2.TabIndex = 1;
            // 
            // DataGridList
            // 
            this.DataGridList.BackColor = System.Drawing.Color.White;
            this.DataGridList.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.DataGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridList.EnableSort = false;
            this.DataGridList.FixedColumns = 1;
            this.DataGridList.FixedRows = 1;
            this.DataGridList.Location = new System.Drawing.Point(0, 0);
            this.DataGridList.Name = "DataGridList";
            this.DataGridList.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.DataGridList.Size = new System.Drawing.Size(417, 209);
            this.DataGridList.TabIndex = 0;
            this.DataGridList.TabStop = true;
            this.DataGridList.ToolTipText = "";
            // 
            // xListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "xListView";
            this.Size = new System.Drawing.Size(417, 246);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panel2;
        public SourceGrid.DataGrid DataGridList;
    }
}
