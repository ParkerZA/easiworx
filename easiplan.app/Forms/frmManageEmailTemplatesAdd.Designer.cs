namespace Finx.App.Forms
{
    partial class frmManageEmailTemplatesAdd
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
            this.button_Preview = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.xHtmlEditor1 = new Finx.App.UserControls.xHtmlEditor();
            this.xInput_IsActive = new Finx.App.UserControls.xInput();
            this.xInput_TemplateSubject = new Finx.App.UserControls.xInput();
            this.xInput_TemplateName = new Finx.App.UserControls.xInput();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xHtmlEditor1);
            this.groupBox1.Controls.Add(this.button_Preview);
            this.groupBox1.Controls.Add(this.button_Delete);
            this.groupBox1.Controls.Add(this.xInput_IsActive);
            this.groupBox1.Controls.Add(this.xInput_TemplateSubject);
            this.groupBox1.Controls.Add(this.button_Cancel);
            this.groupBox1.Controls.Add(this.button_Save);
            this.groupBox1.Controls.Add(this.xInput_TemplateName);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(566, 474);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button_Preview
            // 
            this.button_Preview.Location = new System.Drawing.Point(320, 420);
            this.button_Preview.Name = "button_Preview";
            this.button_Preview.Size = new System.Drawing.Size(75, 41);
            this.button_Preview.TabIndex = 11;
            this.button_Preview.Text = "Preview";
            this.button_Preview.UseVisualStyleBackColor = true;
            this.button_Preview.Click += new System.EventHandler(this.button_Preview_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Delete.Location = new System.Drawing.Point(182, 420);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 41);
            this.button_Delete.TabIndex = 10;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(401, 420);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 41);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(482, 420);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 41);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // xHtmlEditor1
            // 
            this.xHtmlEditor1.Changed = false;
            this.xHtmlEditor1.InnerHtml = null;
            this.xHtmlEditor1.Location = new System.Drawing.Point(20, 118);
            this.xHtmlEditor1.Name = "xHtmlEditor1";
            this.xHtmlEditor1.ShowAlignCenterButton = true;
            this.xHtmlEditor1.ShowAlignLeftButton = true;
            this.xHtmlEditor1.ShowAlignRightButton = true;
            this.xHtmlEditor1.ShowBackColorButton = true;
            this.xHtmlEditor1.ShowBoldButton = true;
            this.xHtmlEditor1.ShowBulletButton = true;
            this.xHtmlEditor1.ShowCopyButton = true;
            this.xHtmlEditor1.ShowCutButton = true;
            this.xHtmlEditor1.ShowFontFamilyButton = false;
            this.xHtmlEditor1.ShowFontSizeButton = false;
            this.xHtmlEditor1.ShowIndentButton = true;
            this.xHtmlEditor1.ShowItalicButton = true;
            this.xHtmlEditor1.ShowJustifyButton = true;
            this.xHtmlEditor1.ShowLinkButton = true;
            this.xHtmlEditor1.ShowNewButton = true;
            this.xHtmlEditor1.ShowOrderedListButton = true;
            this.xHtmlEditor1.ShowOutdentButton = true;
            this.xHtmlEditor1.ShowPasteButton = true;
            this.xHtmlEditor1.ShowPrintButton = true;
            this.xHtmlEditor1.ShowTxtBGButton = true;
            this.xHtmlEditor1.ShowTxtColorButton = true;
            this.xHtmlEditor1.ShowUnderlineButton = true;
            this.xHtmlEditor1.ShowUnlinkButton = true;
            this.xHtmlEditor1.Size = new System.Drawing.Size(536, 262);
            this.xHtmlEditor1.TabIndex = 12;
            // 
            // xInput_IsActive
            // 
            this.xInput_IsActive.BackColor = System.Drawing.Color.Transparent;
            this.xInput_IsActive.ControlTypes = Finx.App.UserControls.ControlTypes.CheckBox;
            this.xInput_IsActive.ControlWidth = 20;
            this.xInput_IsActive.DataSource = null;
            this.xInput_IsActive.DisplayMember = "Text";
            this.xInput_IsActive.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_IsActive.LableText = "Is Active";
            this.xInput_IsActive.Location = new System.Drawing.Point(20, 387);
            this.xInput_IsActive.MappedField = "Status";
            this.xInput_IsActive.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_IsActive.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_IsActive.Model = null;
            this.xInput_IsActive.Name = "xInput_IsActive";
            this.xInput_IsActive.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_IsActive.ReadOnly = false;
            this.xInput_IsActive.Size = new System.Drawing.Size(113, 30);
            this.xInput_IsActive.TabIndex = 9;
            this.xInput_IsActive.ValueMember = "Value";
            // 
            // xInput_TemplateSubject
            // 
            this.xInput_TemplateSubject.BackColor = System.Drawing.Color.Transparent;
            this.xInput_TemplateSubject.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_TemplateSubject.ControlWidth = 200;
            this.xInput_TemplateSubject.DataSource = null;
            this.xInput_TemplateSubject.DisplayMember = "Text";
            this.xInput_TemplateSubject.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_TemplateSubject.LableText = "Email Subject";
            this.xInput_TemplateSubject.Location = new System.Drawing.Point(20, 53);
            this.xInput_TemplateSubject.MappedField = "TemplateSubject";
            this.xInput_TemplateSubject.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_TemplateSubject.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_TemplateSubject.Model = null;
            this.xInput_TemplateSubject.Name = "xInput_TemplateSubject";
            this.xInput_TemplateSubject.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_TemplateSubject.ReadOnly = false;
            this.xInput_TemplateSubject.Size = new System.Drawing.Size(536, 62);
            this.xInput_TemplateSubject.TabIndex = 5;
            this.xInput_TemplateSubject.ValueMember = "Value";
            // 
            // xInput_TemplateName
            // 
            this.xInput_TemplateName.BackColor = System.Drawing.Color.Transparent;
            this.xInput_TemplateName.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_TemplateName.ControlWidth = 400;
            this.xInput_TemplateName.DataSource = null;
            this.xInput_TemplateName.DisplayMember = "Text";
            this.xInput_TemplateName.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_TemplateName.LableText = "Template Name";
            this.xInput_TemplateName.Location = new System.Drawing.Point(20, 23);
            this.xInput_TemplateName.MappedField = "TemplateName";
            this.xInput_TemplateName.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_TemplateName.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_TemplateName.Model = null;
            this.xInput_TemplateName.Name = "xInput_TemplateName";
            this.xInput_TemplateName.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_TemplateName.ReadOnly = false;
            this.xInput_TemplateName.Size = new System.Drawing.Size(536, 30);
            this.xInput_TemplateName.TabIndex = 2;
            this.xInput_TemplateName.ValueMember = "Value";
            // 
            // frmManageEmailTemplatesAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(582, 490);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageEmailTemplatesAdd";
            this.Text = "Email Template";
            this.Load += new System.EventHandler(this.frmManageTemplatesAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Save;
        private UserControls.xInput xInput_TemplateName;
        private UserControls.xInput xInput_TemplateSubject;
        private UserControls.xInput xInput_IsActive;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Preview;
        private UserControls.xHtmlEditor xHtmlEditor1;
    }
}