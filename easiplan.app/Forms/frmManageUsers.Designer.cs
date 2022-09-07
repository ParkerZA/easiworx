namespace Finx.App.Forms
{
    partial class frmManageUsers
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelFilter = new System.Windows.Forms.Panel();
            this.xInputFilterSurname = new Finx.App.UserControls.xInput();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGrid_Users = new SourceGrid.DataGrid();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panelFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.SystemColors.Window;
            this.panelFilter.Controls.Add(this.xInputFilterSurname);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(20, 106);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(829, 31);
            this.panelFilter.TabIndex = 2;
            // 
            // xInputFilterSurname
            // 
            this.xInputFilterSurname.BackColor = System.Drawing.Color.Transparent;
            this.xInputFilterSurname.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInputFilterSurname.ControlWidth = 200;
            this.xInputFilterSurname.DataSource = null;
            this.xInputFilterSurname.DisplayMember = "Text";
            this.xInputFilterSurname.Dock = System.Windows.Forms.DockStyle.Right;
            this.xInputFilterSurname.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInputFilterSurname.LableText = "Search by Surname";
            this.xInputFilterSurname.Location = new System.Drawing.Point(469, 0);
            this.xInputFilterSurname.MappedField = null;
            this.xInputFilterSurname.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterSurname.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterSurname.Model = null;
            this.xInputFilterSurname.Name = "xInputFilterSurname";
            this.xInputFilterSurname.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterSurname.ReadOnly = false;
            this.xInputFilterSurname.Size = new System.Drawing.Size(360, 31);
            this.xInputFilterSurname.TabIndex = 1;
            this.xInputFilterSurname.Value = "";
            this.xInputFilterSurname.ValueMember = "Value";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGrid_Users);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 95);
            this.panel1.TabIndex = 4;
            // 
            // dataGrid_Users
            // 
            this.dataGrid_Users.BackColor = System.Drawing.Color.White;
            this.dataGrid_Users.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Users.DeleteRowsWithDeleteKey = false;
            this.dataGrid_Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Users.EnableSort = false;
            this.dataGrid_Users.FixedColumns = 1;
            this.dataGrid_Users.FixedRows = 1;
            this.dataGrid_Users.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Users.Name = "dataGrid_Users";
            this.dataGrid_Users.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Users.Size = new System.Drawing.Size(829, 95);
            this.dataGrid_Users.TabIndex = 4;
            this.dataGrid_Users.TabStop = true;
            this.dataGrid_Users.ToolTipText = "";
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.Transparent;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(829, 46);
            this.xToolBarMenu1.TabIndex = 0;
            // 
            // frmManageUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 252);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmManageUsers";
            this.Load += new System.EventHandler(this.frmAdminTasks_Load);
            this.panelFilter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panelFilter;
        private UserControls.xInput xInputFilterSurname;
        private System.Windows.Forms.Panel panel1;
        private SourceGrid.DataGrid dataGrid_Users;
    }
}