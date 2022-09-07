namespace Finx.App.Forms
{
    partial class frmAdminClientCommunication
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
            this.xInput_BirthDate = new Finx.App.UserControls.xInput();
            this.labelTime = new System.Windows.Forms.Label();
            this.xInputFilterSurname = new Finx.App.UserControls.xInput();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.xCheckBoxList1 = new Finx.App.UserControls.xCheckBoxList();
            this.panelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelFilter.Controls.Add(this.xInput_BirthDate);
            this.panelFilter.Controls.Add(this.labelTime);
            this.panelFilter.Controls.Add(this.xInputFilterSurname);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(20, 106);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(829, 31);
            this.panelFilter.TabIndex = 2;
            // 
            // xInput_BirthDate
            // 
            this.xInput_BirthDate.BackColor = System.Drawing.Color.Transparent;
            this.xInput_BirthDate.ControlTypes = Finx.App.UserControls.ControlTypes.DatePicker;
            this.xInput_BirthDate.ControlWidth = 200;
            this.xInput_BirthDate.DataSource = null;
            this.xInput_BirthDate.DisplayMember = "Text";
            this.xInput_BirthDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.xInput_BirthDate.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_BirthDate.LableText = "Birth Date";
            this.xInput_BirthDate.Location = new System.Drawing.Point(271, 0);
            this.xInput_BirthDate.MappedField = null;
            this.xInput_BirthDate.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_BirthDate.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_BirthDate.Model = null;
            this.xInput_BirthDate.Name = "xInput_BirthDate";
            this.xInput_BirthDate.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_BirthDate.ReadOnly = false;
            this.xInput_BirthDate.Size = new System.Drawing.Size(278, 31);
            this.xInput_BirthDate.TabIndex = 5;
            this.xInput_BirthDate.Value = null;
            this.xInput_BirthDate.ValueMember = "Value";
            // 
            // labelTime
            // 
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTime.Location = new System.Drawing.Point(0, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(110, 31);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "Filter by :";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.xInputFilterSurname.LableText = "Surname";
            this.xInputFilterSurname.Location = new System.Drawing.Point(549, 0);
            this.xInputFilterSurname.MappedField = null;
            this.xInputFilterSurname.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterSurname.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterSurname.Model = null;
            this.xInputFilterSurname.Name = "xInputFilterSurname";
            this.xInputFilterSurname.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterSurname.ReadOnly = false;
            this.xInputFilterSurname.Size = new System.Drawing.Size(280, 31);
            this.xInputFilterSurname.TabIndex = 1;
            this.xInputFilterSurname.Value = "";
            this.xInputFilterSurname.ValueMember = "Value";
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
            // xCheckBoxList1
            // 
            this.xCheckBoxList1.Caption = "Caption";
            this.xCheckBoxList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xCheckBoxList1.font = new System.Drawing.Font("Verdana", 10F);
            this.xCheckBoxList1.Location = new System.Drawing.Point(20, 137);
            this.xCheckBoxList1.Name = "xCheckBoxList1";
            this.xCheckBoxList1.Size = new System.Drawing.Size(829, 283);
            this.xCheckBoxList1.TabIndex = 3;
            // 
            // frmAdminClientCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 440);
            this.Controls.Add(this.xCheckBoxList1);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmAdminClientCommunication";
            this.Text = "Client Communications";
            this.Load += new System.EventHandler(this.frmAdminClientCommunication_Load);
            this.panelFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panelFilter;
        private UserControls.xInput xInputFilterSurname;
        private System.Windows.Forms.Label labelTime;
        private UserControls.xInput xInput_BirthDate;
        private UserControls.xCheckBoxList xCheckBoxList1;
    }
}