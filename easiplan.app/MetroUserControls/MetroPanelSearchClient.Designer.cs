namespace Finx.App.MetroControls
{
    partial class MetroPanelSearchClient
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
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_ClientSearch = new MetroFramework.Controls.MetroTextBox();
            this.metroRadioButton_BySurname = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton_ById = new MetroFramework.Controls.MetroRadioButton();
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(290, 45);
            this.metroTile1.TabIndex = 3;
            this.metroTile1.Text = "Client Search";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.metroTile1.UseSelectable = true;
            this.metroTile1.Click += new System.EventHandler(this.metroTile1_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.metroListView1);
            this.metroPanel1.Controls.Add(this.metroTextBox_ClientSearch);
            this.metroPanel1.Controls.Add(this.metroRadioButton_BySurname);
            this.metroPanel1.Controls.Add(this.metroRadioButton_ById);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 45);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(290, 281);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_ClientSearch
            // 
            // 
            // 
            // 
            this.metroTextBox_ClientSearch.CustomButton.Image = null;
            this.metroTextBox_ClientSearch.CustomButton.Location = new System.Drawing.Point(226, 2);
            this.metroTextBox_ClientSearch.CustomButton.Name = "";
            this.metroTextBox_ClientSearch.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox_ClientSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_ClientSearch.CustomButton.TabIndex = 1;
            this.metroTextBox_ClientSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_ClientSearch.CustomButton.UseSelectable = true;
            this.metroTextBox_ClientSearch.DisplayIcon = true;
            this.metroTextBox_ClientSearch.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBox_ClientSearch.Lines = new string[0];
            this.metroTextBox_ClientSearch.Location = new System.Drawing.Point(15, 52);
            this.metroTextBox_ClientSearch.MaxLength = 32767;
            this.metroTextBox_ClientSearch.Name = "metroTextBox_ClientSearch";
            this.metroTextBox_ClientSearch.PasswordChar = '\0';
            this.metroTextBox_ClientSearch.WaterMark = "Enter Search value";
            this.metroTextBox_ClientSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_ClientSearch.SelectedText = "";
            this.metroTextBox_ClientSearch.SelectionLength = 0;
            this.metroTextBox_ClientSearch.SelectionStart = 0;
            this.metroTextBox_ClientSearch.ShortcutsEnabled = true;
            this.metroTextBox_ClientSearch.ShowButton = true;
            this.metroTextBox_ClientSearch.ShowClearButton = true;
            this.metroTextBox_ClientSearch.Size = new System.Drawing.Size(254, 30);
            this.metroTextBox_ClientSearch.TabIndex = 8;
            this.metroTextBox_ClientSearch.UseSelectable = true;
            this.metroTextBox_ClientSearch.WaterMark = "Enter Search value";
            this.metroTextBox_ClientSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_ClientSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroRadioButton_BySurname
            // 
            this.metroRadioButton_BySurname.AutoSize = true;
            this.metroRadioButton_BySurname.Location = new System.Drawing.Point(137, 15);
            this.metroRadioButton_BySurname.Name = "metroRadioButton_BySurname";
            this.metroRadioButton_BySurname.Size = new System.Drawing.Size(86, 15);
            this.metroRadioButton_BySurname.TabIndex = 7;
            this.metroRadioButton_BySurname.Text = "by Surname";
            this.metroRadioButton_BySurname.UseSelectable = true;
            // 
            // metroRadioButton_ById
            // 
            this.metroRadioButton_ById.AutoSize = true;
            this.metroRadioButton_ById.Location = new System.Drawing.Point(15, 15);
            this.metroRadioButton_ById.Name = "metroRadioButton_ById";
            this.metroRadioButton_ById.Size = new System.Drawing.Size(96, 15);
            this.metroRadioButton_ById.TabIndex = 6;
            this.metroRadioButton_ById.Text = "by Id Number";
            this.metroRadioButton_ById.UseSelectable = true;
            // 
            // metroListView1
            // 
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Location = new System.Drawing.Point(15, 88);
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(254, 179);
            this.metroListView1.TabIndex = 9;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            // 
            // MetroPanelSearchClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroTile1);
            this.Name = "MetroPanelSearchClient";
            this.Size = new System.Drawing.Size(290, 326);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTextBox metroTextBox_ClientSearch;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_BySurname;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_ById;
        private MetroFramework.Controls.MetroListView metroListView1;
    }
}
