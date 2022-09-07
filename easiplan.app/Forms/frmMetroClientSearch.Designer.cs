namespace Finx.App.Forms
{
    partial class frmMetroClientSearch
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroRadioButton_BySurname = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton_ByIdNumber = new MetroFramework.Controls.MetroRadioButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_Search = new MetroFramework.Controls.MetroTextBox();
            this.mbSearch = new MetroFramework.Controls.MetroButton();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.metroListView_SearchResults = new MetroFramework.Controls.MetroListView();
            this.Surname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.metroCheckBox_CloseOnSelect = new MetroFramework.Controls.MetroCheckBox();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroRadioButton_BySurname);
            this.metroPanel1.Controls.Add(this.metroRadioButton_ByIdNumber);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(320, 25);
            this.metroPanel1.TabIndex = 2;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroRadioButton_BySurname
            // 
            this.metroRadioButton_BySurname.AutoSize = true;
            this.metroRadioButton_BySurname.Checked = true;
            this.metroRadioButton_BySurname.Location = new System.Drawing.Point(5, 4);
            this.metroRadioButton_BySurname.Name = "metroRadioButton_BySurname";
            this.metroRadioButton_BySurname.Size = new System.Drawing.Size(86, 15);
            this.metroRadioButton_BySurname.TabIndex = 3;
            this.metroRadioButton_BySurname.TabStop = true;
            this.metroRadioButton_BySurname.Text = "by Surname";
            this.metroRadioButton_BySurname.UseSelectable = true;
            // 
            // metroRadioButton_ByIdNumber
            // 
            this.metroRadioButton_ByIdNumber.AutoSize = true;
            this.metroRadioButton_ByIdNumber.Location = new System.Drawing.Point(107, 4);
            this.metroRadioButton_ByIdNumber.Name = "metroRadioButton_ByIdNumber";
            this.metroRadioButton_ByIdNumber.Size = new System.Drawing.Size(96, 15);
            this.metroRadioButton_ByIdNumber.TabIndex = 2;
            this.metroRadioButton_ByIdNumber.Text = "by Id Number";
            this.metroRadioButton_ByIdNumber.UseSelectable = true;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.metroTextBox_Search);
            this.metroPanel2.Controls.Add(this.mbSearch);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(20, 85);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.metroPanel2.Size = new System.Drawing.Size(320, 39);
            this.metroPanel2.TabIndex = 3;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_Search
            // 
            // 
            // 
            // 
            this.metroTextBox_Search.CustomButton.Image = null;
            this.metroTextBox_Search.CustomButton.Location = new System.Drawing.Point(200, 1);
            this.metroTextBox_Search.CustomButton.Name = "";
            this.metroTextBox_Search.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.metroTextBox_Search.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Search.CustomButton.TabIndex = 1;
            this.metroTextBox_Search.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Search.CustomButton.UseSelectable = true;
            this.metroTextBox_Search.CustomButton.Visible = false;
            this.metroTextBox_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox_Search.FontSize = MetroFramework.MetroTextBoxSize.Large;
            this.metroTextBox_Search.Lines = new string[0];
            this.metroTextBox_Search.Location = new System.Drawing.Point(5, 5);
            this.metroTextBox_Search.MaxLength = 32767;
            this.metroTextBox_Search.Name = "metroTextBox_Search";
            this.metroTextBox_Search.PasswordChar = '\0';
            this.metroTextBox_Search.PromptText = "Enter Search value";
            this.metroTextBox_Search.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_Search.SelectedText = "";
            this.metroTextBox_Search.SelectionLength = 0;
            this.metroTextBox_Search.SelectionStart = 0;
            this.metroTextBox_Search.ShortcutsEnabled = true;
            this.metroTextBox_Search.ShowClearButton = true;
            this.metroTextBox_Search.Size = new System.Drawing.Size(228, 29);
            this.metroTextBox_Search.TabIndex = 5;
            this.metroTextBox_Search.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Search.UseSelectable = true;
            this.metroTextBox_Search.WaterMark = "Enter Search value";
            this.metroTextBox_Search.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Search.WaterMarkFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbSearch
            // 
            this.mbSearch.BackColor = System.Drawing.Color.GhostWhite;
            this.mbSearch.BackgroundImage = global::easiplan.app.Properties.Resources.Search_32;
            this.mbSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mbSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.mbSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.mbSearch.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.mbSearch.Location = new System.Drawing.Point(233, 5);
            this.mbSearch.Name = "mbSearch";
            this.mbSearch.Size = new System.Drawing.Size(82, 29);
            this.mbSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.mbSearch.TabIndex = 4;
            this.mbSearch.Text = "      Search";
            this.mbSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mbSearch.UseCustomBackColor = true;
            this.mbSearch.UseCustomForeColor = true;
            this.mbSearch.UseSelectable = true;
            this.mbSearch.UseStyleColors = true;
            
            // 
            // metroPanel3
            // 
            this.metroPanel3.BackColor = System.Drawing.SystemColors.Window;
            this.metroPanel3.Controls.Add(this.metroListView_SearchResults);
            this.metroPanel3.Controls.Add(this.metroPanel4);
            this.metroPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(20, 124);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Padding = new System.Windows.Forms.Padding(5);
            this.metroPanel3.Size = new System.Drawing.Size(320, 196);
            this.metroPanel3.TabIndex = 4;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // metroListView_SearchResults
            // 
            this.metroListView_SearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Surname});
            this.metroListView_SearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroListView_SearchResults.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView_SearchResults.FullRowSelect = true;
            this.metroListView_SearchResults.LabelWrap = false;
            this.metroListView_SearchResults.Location = new System.Drawing.Point(5, 5);
            this.metroListView_SearchResults.Name = "metroListView_SearchResults";
            this.metroListView_SearchResults.OwnerDraw = true;
            this.metroListView_SearchResults.Size = new System.Drawing.Size(310, 161);
            this.metroListView_SearchResults.TabIndex = 4;
            this.metroListView_SearchResults.UseCompatibleStateImageBehavior = false;
            this.metroListView_SearchResults.UseSelectable = true;
            this.metroListView_SearchResults.View = System.Windows.Forms.View.List;
            // 
            // Surname
            // 
            this.Surname.Text = "Surname";
            this.Surname.Width = 137;
            // 
            // metroPanel4
            // 
            this.metroPanel4.Controls.Add(this.metroCheckBox_CloseOnSelect);
            this.metroPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 10;
            this.metroPanel4.Location = new System.Drawing.Point(5, 166);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Size = new System.Drawing.Size(310, 25);
            this.metroPanel4.TabIndex = 3;
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 10;
            // 
            // metroCheckBox_CloseOnSelect
            // 
            this.metroCheckBox_CloseOnSelect.AutoSize = true;
            this.metroCheckBox_CloseOnSelect.Checked = true;
            this.metroCheckBox_CloseOnSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metroCheckBox_CloseOnSelect.Location = new System.Drawing.Point(0, 7);
            this.metroCheckBox_CloseOnSelect.Name = "metroCheckBox_CloseOnSelect";
            this.metroCheckBox_CloseOnSelect.Size = new System.Drawing.Size(103, 15);
            this.metroCheckBox_CloseOnSelect.TabIndex = 2;
            this.metroCheckBox_CloseOnSelect.Text = "Close on Select";
            this.metroCheckBox_CloseOnSelect.UseSelectable = true;
            // 
            // frmMetroClientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 340);
            this.Controls.Add(this.metroPanel3);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Name = "frmMetroClientSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmMetroClientSearch";
            this.Load += new System.EventHandler(this.frmMetroClientSearch_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel4.ResumeLayout(false);
            this.metroPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_BySurname;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_ByIdNumber;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroListView metroListView_SearchResults;
        private System.Windows.Forms.ColumnHeader Surname;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox_CloseOnSelect;
        private MetroFramework.Controls.MetroTextBox metroTextBox_Search;
        private MetroFramework.Controls.MetroButton mbSearch;
    }
}