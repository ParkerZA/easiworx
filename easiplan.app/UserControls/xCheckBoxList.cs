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
    public partial class xCheckBoxList : UserControl
    {

        //public event KeyEventHandler CheckBoxClicked;
        public event EventHandler ButtonClicked;

        Font _font = new Font("Verdana", 10);
        public xCheckBoxList()
        {
            InitializeComponent();

            InitialiseControls();
        }

        private void InitialiseControls()
        {
            
            this.listView1.Font = _font;

            this.button1.Tag = this.listView1;
            this.button1.Click += Button1_Click;

            this.checkBox1.Click += CheckBox1_Click;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
                ButtonClicked(sender, e);
        }

        private void CheckBox1_Click(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (ListViewItem item in this.listView1.Items)
                item.Checked = chk.Checked;
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

        public Button Button
        {
            get
            { return this.button1; }
        }

        public string Caption {
            get
            { return this.lbl_caption.Text; }
            set
            { this.lbl_caption.Text = value; }
        }
       
    }
}
