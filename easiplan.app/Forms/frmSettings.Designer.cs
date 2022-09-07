namespace Finx.App.Forms
{
    partial class frmSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl_Configuration = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.htmlPanel_BackgroundImg = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.button_loadPicture = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_SmallFont = new System.Windows.Forms.RadioButton();
            this.radioButton_NormalFont = new System.Windows.Forms.RadioButton();
            this.radioButton_LargeFont = new System.Windows.Forms.RadioButton();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl_Configuration.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.htmlPanel_BackgroundImg.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xToolBarMenu1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 50);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl_Configuration);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 110);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(630, 283);
            this.panel2.TabIndex = 17;
            // 
            // tabControl_Configuration
            // 
            this.tabControl_Configuration.Controls.Add(this.tabPage1);
            this.tabControl_Configuration.Controls.Add(this.tabPage2);
            this.tabControl_Configuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Configuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_Configuration.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Configuration.Name = "tabControl_Configuration";
            this.tabControl_Configuration.SelectedIndex = 0;
            this.tabControl_Configuration.Size = new System.Drawing.Size(630, 283);
            this.tabControl_Configuration.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.htmlPanel_BackgroundImg);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(622, 255);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Background Image";
            // 
            // htmlPanel_BackgroundImg
            // 
            this.htmlPanel_BackgroundImg.AutoScroll = true;
            this.htmlPanel_BackgroundImg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.htmlPanel_BackgroundImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.htmlPanel_BackgroundImg.BaseStylesheet = "";
            this.htmlPanel_BackgroundImg.Controls.Add(this.button_loadPicture);
            this.htmlPanel_BackgroundImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel_BackgroundImg.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel_BackgroundImg.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel_BackgroundImg.Name = "htmlPanel_BackgroundImg";
            this.htmlPanel_BackgroundImg.Size = new System.Drawing.Size(622, 255);
            this.htmlPanel_BackgroundImg.TabIndex = 20;
            this.htmlPanel_BackgroundImg.TabStop = false;
            this.htmlPanel_BackgroundImg.Text = null;
            // 
            // button_loadPicture
            // 
            this.button_loadPicture.Enabled = false;
            this.button_loadPicture.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadPicture.Location = new System.Drawing.Point(504, 3);
            this.button_loadPicture.Name = "button_loadPicture";
            this.button_loadPicture.Size = new System.Drawing.Size(115, 34);
            this.button_loadPicture.TabIndex = 20;
            this.button_loadPicture.Text = "Load New Image";
            this.button_loadPicture.UseVisualStyleBackColor = true;
            this.button_loadPicture.Click += new System.EventHandler(this.button_loadPicture_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 255);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Font";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_SmallFont);
            this.groupBox1.Controls.Add(this.radioButton_NormalFont);
            this.groupBox1.Controls.Add(this.radioButton_LargeFont);
            this.groupBox1.Location = new System.Drawing.Point(19, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font Size";
            // 
            // radioButton_SmallFont
            // 
            this.radioButton_SmallFont.AutoSize = true;
            this.radioButton_SmallFont.Location = new System.Drawing.Point(416, 39);
            this.radioButton_SmallFont.Name = "radioButton_SmallFont";
            this.radioButton_SmallFont.Size = new System.Drawing.Size(57, 19);
            this.radioButton_SmallFont.TabIndex = 2;
            this.radioButton_SmallFont.TabStop = true;
            this.radioButton_SmallFont.Text = "Small";
            this.radioButton_SmallFont.UseVisualStyleBackColor = true;
            // 
            // radioButton_NormalFont
            // 
            this.radioButton_NormalFont.AutoSize = true;
            this.radioButton_NormalFont.Location = new System.Drawing.Point(234, 39);
            this.radioButton_NormalFont.Name = "radioButton_NormalFont";
            this.radioButton_NormalFont.Size = new System.Drawing.Size(66, 19);
            this.radioButton_NormalFont.TabIndex = 1;
            this.radioButton_NormalFont.TabStop = true;
            this.radioButton_NormalFont.Text = "Normal";
            this.radioButton_NormalFont.UseVisualStyleBackColor = true;
            // 
            // radioButton_LargeFont
            // 
            this.radioButton_LargeFont.AutoSize = true;
            this.radioButton_LargeFont.Location = new System.Drawing.Point(65, 39);
            this.radioButton_LargeFont.Name = "radioButton_LargeFont";
            this.radioButton_LargeFont.Size = new System.Drawing.Size(57, 19);
            this.radioButton_LargeFont.TabIndex = 0;
            this.radioButton_LargeFont.TabStop = true;
            this.radioButton_LargeFont.Text = "Large";
            this.radioButton_LargeFont.UseVisualStyleBackColor = true;
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoSize = true;
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xToolBarMenu1.Location = new System.Drawing.Point(0, 0);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(630, 50);
            this.xToolBarMenu1.TabIndex = 9;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 413);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl_Configuration.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.htmlPanel_BackgroundImg.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl_Configuration;
        private System.Windows.Forms.TabPage tabPage1;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel_BackgroundImg;
        private System.Windows.Forms.Button button_loadPicture;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_SmallFont;
        private System.Windows.Forms.RadioButton radioButton_NormalFont;
        private System.Windows.Forms.RadioButton radioButton_LargeFont;
    }
}