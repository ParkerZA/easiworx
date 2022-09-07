namespace Finx.App.Forms
{
    partial class frmManageUsersAdd
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xInput_IsActive = new Finx.App.UserControls.xInput();
            this.xInput_Administrator = new Finx.App.UserControls.xInput();
            this.xInput_Designation = new Finx.App.UserControls.xInput();
            this.xInput_Surname = new Finx.App.UserControls.xInput();
            this.xInput_Firstname = new Finx.App.UserControls.xInput();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.xInput_Title = new Finx.App.UserControls.xInput();
            this.xInput_Password = new Finx.App.UserControls.xInput();
            this.xInput_Username = new Finx.App.UserControls.xInput();
            this.button_Delete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Delete);
            this.groupBox1.Controls.Add(this.xInput_IsActive);
            this.groupBox1.Controls.Add(this.xInput_Administrator);
            this.groupBox1.Controls.Add(this.xInput_Designation);
            this.groupBox1.Controls.Add(this.xInput_Surname);
            this.groupBox1.Controls.Add(this.xInput_Firstname);
            this.groupBox1.Controls.Add(this.button_Cancel);
            this.groupBox1.Controls.Add(this.button_Save);
            this.groupBox1.Controls.Add(this.xInput_Title);
            this.groupBox1.Controls.Add(this.xInput_Password);
            this.groupBox1.Controls.Add(this.xInput_Username);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(402, 355);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // xInput_IsActive
            // 
            this.xInput_IsActive.BackColor = System.Drawing.Color.Transparent;
            this.xInput_IsActive.ControlTypes = Finx.App.UserControls.ControlTypes.CheckBox;
            this.xInput_IsActive.ControlWidth = 200;
            this.xInput_IsActive.DataSource = null;
            this.xInput_IsActive.DisplayMember = "Text";
            this.xInput_IsActive.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_IsActive.LableText = "Is Active";
            this.xInput_IsActive.Location = new System.Drawing.Point(20, 251);
            this.xInput_IsActive.MappedField = "IsActive";
            this.xInput_IsActive.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_IsActive.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_IsActive.Model = null;
            this.xInput_IsActive.Name = "xInput_IsActive";
            this.xInput_IsActive.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_IsActive.ReadOnly = false;
            this.xInput_IsActive.Size = new System.Drawing.Size(375, 30);
            this.xInput_IsActive.TabIndex = 9;
            this.xInput_IsActive.ValueMember = "Value";
            // 
            // xInput_Administrator
            // 
            this.xInput_Administrator.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Administrator.ControlTypes = Finx.App.UserControls.ControlTypes.CheckBox;
            this.xInput_Administrator.ControlWidth = 200;
            this.xInput_Administrator.DataSource = null;
            this.xInput_Administrator.DisplayMember = "Text";
            this.xInput_Administrator.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Administrator.LableText = "Administrator";
            this.xInput_Administrator.Location = new System.Drawing.Point(20, 221);
            this.xInput_Administrator.MappedField = "IsAdministrator";
            this.xInput_Administrator.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Administrator.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Administrator.Model = null;
            this.xInput_Administrator.Name = "xInput_Administrator";
            this.xInput_Administrator.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Administrator.ReadOnly = false;
            this.xInput_Administrator.Size = new System.Drawing.Size(375, 30);
            this.xInput_Administrator.TabIndex = 8;
            this.xInput_Administrator.ValueMember = "Value";
            // 
            // xInput_Designation
            // 
            this.xInput_Designation.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Designation.ControlTypes = Finx.App.UserControls.ControlTypes.MultiSelectCombo;
            this.xInput_Designation.ControlWidth = 200;
            this.xInput_Designation.DataSource = null;
            this.xInput_Designation.DisplayMember = "Text";
            this.xInput_Designation.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Designation.LableText = "Designation";
            this.xInput_Designation.Location = new System.Drawing.Point(20, 191);
            this.xInput_Designation.MappedField = "Designation";
            this.xInput_Designation.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Designation.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Designation.Model = null;
            this.xInput_Designation.Name = "xInput_Designation";
            this.xInput_Designation.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Designation.ReadOnly = false;
            this.xInput_Designation.Size = new System.Drawing.Size(375, 30);
            this.xInput_Designation.TabIndex = 7;
            this.xInput_Designation.ValueMember = "Value";
            // 
            // xInput_Surname
            // 
            this.xInput_Surname.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Surname.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Surname.ControlWidth = 200;
            this.xInput_Surname.DataSource = null;
            this.xInput_Surname.DisplayMember = "Text";
            this.xInput_Surname.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Surname.LableText = "Surname";
            this.xInput_Surname.Location = new System.Drawing.Point(20, 83);
            this.xInput_Surname.MappedField = "Surname";
            this.xInput_Surname.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Surname.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Surname.Model = null;
            this.xInput_Surname.Name = "xInput_Surname";
            this.xInput_Surname.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Surname.ReadOnly = false;
            this.xInput_Surname.Size = new System.Drawing.Size(375, 30);
            this.xInput_Surname.TabIndex = 6;
            this.xInput_Surname.ValueMember = "Value";
            // 
            // xInput_Firstname
            // 
            this.xInput_Firstname.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Firstname.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Firstname.ControlWidth = 200;
            this.xInput_Firstname.DataSource = null;
            this.xInput_Firstname.DisplayMember = "Text";
            this.xInput_Firstname.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Firstname.LableText = "First name";
            this.xInput_Firstname.Location = new System.Drawing.Point(20, 53);
            this.xInput_Firstname.MappedField = "Firstname";
            this.xInput_Firstname.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Firstname.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Firstname.Model = null;
            this.xInput_Firstname.Name = "xInput_Firstname";
            this.xInput_Firstname.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Firstname.ReadOnly = false;
            this.xInput_Firstname.Size = new System.Drawing.Size(375, 30);
            this.xInput_Firstname.TabIndex = 5;
            this.xInput_Firstname.ValueMember = "Value";
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(239, 309);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 41);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(320, 309);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 41);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // xInput_Title
            // 
            this.xInput_Title.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Title.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Title.ControlWidth = 80;
            this.xInput_Title.DataSource = null;
            this.xInput_Title.DisplayMember = "Text";
            this.xInput_Title.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Title.LableText = "Title";
            this.xInput_Title.Location = new System.Drawing.Point(20, 23);
            this.xInput_Title.MappedField = "Title";
            this.xInput_Title.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Title.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Title.Model = null;
            this.xInput_Title.Name = "xInput_Title";
            this.xInput_Title.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Title.ReadOnly = false;
            this.xInput_Title.Size = new System.Drawing.Size(254, 30);
            this.xInput_Title.TabIndex = 2;
            this.xInput_Title.ValueMember = "Value";
            // 
            // xInput_Password
            // 
            this.xInput_Password.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Password.ControlTypes = Finx.App.UserControls.ControlTypes.PasswordBox;
            this.xInput_Password.ControlWidth = 200;
            this.xInput_Password.DataSource = null;
            this.xInput_Password.DisplayMember = "Text";
            this.xInput_Password.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Password.LableText = "Password";
            this.xInput_Password.Location = new System.Drawing.Point(20, 152);
            this.xInput_Password.MappedField = "Password";
            this.xInput_Password.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Password.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Password.Model = null;
            this.xInput_Password.Name = "xInput_Password";
            this.xInput_Password.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Password.ReadOnly = false;
            this.xInput_Password.Size = new System.Drawing.Size(375, 30);
            this.xInput_Password.TabIndex = 1;
            this.xInput_Password.ValueMember = "Value";
            // 
            // xInput_Username
            // 
            this.xInput_Username.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Username.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Username.ControlWidth = 200;
            this.xInput_Username.DataSource = null;
            this.xInput_Username.DisplayMember = "Text";
            this.xInput_Username.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Username.LableText = "Username";
            this.xInput_Username.Location = new System.Drawing.Point(20, 122);
            this.xInput_Username.MappedField = "Username";
            this.xInput_Username.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Username.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Username.Model = null;
            this.xInput_Username.Name = "xInput_Username";
            this.xInput_Username.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Username.ReadOnly = false;
            this.xInput_Username.Size = new System.Drawing.Size(375, 30);
            this.xInput_Username.TabIndex = 0;
            this.xInput_Username.ValueMember = "Value";
            // 
            // button_Delete
            // 
            this.button_Delete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Delete.Location = new System.Drawing.Point(20, 309);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 41);
            this.button_Delete.TabIndex = 10;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // frmManageUsersAdd
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(420, 362);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageUsersAdd";
            this.Text = "Application User";
            this.Load += new System.EventHandler(this.frmManageUsersAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.xInput xInput_Username;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Save;
        private UserControls.xInput xInput_Title;
        private UserControls.xInput xInput_Password;
        private UserControls.xInput xInput_Administrator;
        private UserControls.xInput xInput_Designation;
        private UserControls.xInput xInput_Surname;
        private UserControls.xInput xInput_Firstname;
        private UserControls.xInput xInput_IsActive;
        private System.Windows.Forms.Button button_Delete;
    }
}