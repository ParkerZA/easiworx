using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.UserControls
{
    public partial class xCheckBoxHOption : UserControl
    {
        public event EventHandler CheckEvent;
        bool _invokeCheckEvent = true;

        public xCheckBoxHOption()
        {
            InitializeComponent();

            label1.ForeColor = Color.SlateGray;

            this.checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if(chk.Checked){              
                label1.ForeColor = Color.Black;
                checkBox1.Image = easiplan.app.Properties.Resources.checkmark_1;
            }
            else{                
                label1.ForeColor = Color.SlateGray;
                checkBox1.Image = null;
            }

            if(_invokeCheckEvent)
                CheckEvent?.Invoke(this, EventArgs.Empty);
        }

        public bool Checked
        {
            get { return checkBox1.Checked; }
            set {
                _invokeCheckEvent = false; 
                checkBox1.Checked = value;             
                _invokeCheckEvent = true; 
            }
        }

        public object Value{ get;set; }
    }

   
}
