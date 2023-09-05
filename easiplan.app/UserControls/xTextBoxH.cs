using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevAge.Windows.Forms;

using QSS.Components.Windows.Forms;
using Microsoft.Extensions.Options;
using MetroFramework.Controls;
using System.Globalization;
using easiplan.domain.Entities;

namespace Finx.App.UserControls
{
    public partial class xTextBoxH : bBoundUserControl
    {
        MetroTextBox textBox = new MetroTextBox();
        public xTextBoxH()
        {
            InitializeComponent();

            this.Height = 120;
        }
                
        public override void InitialiseControls()
        {
           
            base.InitialiseControls();

            textBox = new MetroTextBox();
            textBox.Height = this.Height;
            textBox.Width = this.Width;
            textBox.Multiline=true;
            //textBox.ForeColor = Color.DarkSlateGray;
            //textBox.UseCustomForeColor = true;
            //textBox.BackColor = Color.WhiteSmoke;
            //textBox.UseCustomBackColor = true;            
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ShortcutsEnabled = true;
            textBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            textBox.Top = top;
            textBox.WaterMark = "Press [Ctrl and D] keys to add the current date on a new line ...";

            textBox.KeyDown += textBox_AppendNewLineDate;

            textBox.DataBindings.Add("Text", this, "SelectedValue", true, DataSourceUpdateMode.OnPropertyChanged);

            this.MainPanel.Controls.Add(textBox);

            top += textBox.Height + 5;

            this.Height =top;
        }

        private void textBox_AppendNewLineDate(object sender, KeyEventArgs e)
        {
            MetroTextBox tb = (MetroTextBox)sender;
            if (e.Control && e.KeyCode == Keys.D)
            {

                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.AppendText(DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - ");
                }
                else
                {
                    tb.AppendText(Environment.NewLine + DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - ");
                }

                e.SuppressKeyPress = true; // Prevents the 'D' character from being entered into the text box
            }
        }

    }

}
