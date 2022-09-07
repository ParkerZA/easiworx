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

namespace Finx.App.UserControls
{
    public partial class xSearchList : UserControl
    {

        public event KeyEventHandler KeyDownPressed;
        public event EventHandler KeyRefreshPressed;

        Font _font = new Font("Verdana", 10);
        public xSearchList()
        {
            InitializeComponent();

            InitialiseControls();
        }

        private void InitialiseControls()
        {
            this.devAgeTextBoxButton1.Font = _font;
            this.listView1.Font = _font;
            this.devAgeTextBoxButton1.TextBox.KeyDown += TextBox_KeyDown;
            this.devAgeTextBoxButton1.Button.Click += Button_Click;

           
        }

        private void Button_Click(object sender, EventArgs e)
        {

            KeyRefreshPressed?.Invoke(sender, e);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownPressed?.Invoke(sender, e);
        }

        public Font font {
            get { return _font; }
            set { _font = value;
                InitialiseControls();
            }
        }

        public ListView ListView
        {
            get
            { return this.listView1; }
        }
        public TextBox TextBox {
            get
            { return this.devAgeTextBoxButton1.TextBox; }
        }

        public Button Button
        {
            get
            { return this.devAgeTextBoxButton1.Button; }
        }

    }
}
