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

namespace Finx.App.UserControls
{
    public partial class xCheckBoxListH : bBoundUserControl
    {
        Panel panelOptions;
        IList<CheckBoxHOption> _options = new List<CheckBoxHOption>();

        public xCheckBoxListH()
        {
            InitializeComponent();
        }
                
        public override void InitialiseControls()
        {
           
           base.InitialiseControls();

            panelOptions = new Panel(){ Left=left,Top=top,Width=this.Width, AutoSize=true, AutoSizeMode= AutoSizeMode.GrowAndShrink, BorderStyle=BorderStyle.None};
            int i = 0;
            foreach (CheckBoxHOption option in this._options) {
                xCheckBoxHOption opt = new xCheckBoxHOption() { Label = option.Label, Value = option.Value, Name = option.Label + i, Left = left, Width = option.Width };
                opt.CheckEvent += Opt_CheckEvent;               
                this.panelOptions.Controls.Add(opt);
                left += option.Width-1;                
                i++;
            }
           
            this.MainPanel.Controls.Add(panelOptions);

            top += panelOptions.Height + 5;

            this.Height =top;
        }

        private void Opt_CheckEvent(object sender, EventArgs e)
        {
            xCheckBoxHOption cntrlSelect = sender as xCheckBoxHOption;
            //Uncheck all the other checkboxes
            foreach (var cntrl in this.panelOptions.Controls){
                xCheckBoxHOption chk = cntrl as xCheckBoxHOption;
                
                if (!chk.Equals(cntrlSelect))
                {
                    chk.Checked = false;

                }
                
            }
            
            //Update Bindings
            SelectedValue = cntrlSelect.Value;

        }

        public IList<CheckBoxHOption> Options
        {
            get { return _options; }
            set
            {
                _options = value;
         
            }
        }

        public override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SelectedValue != null)
            {
                foreach (var cntrl in this.panelOptions.Controls)
                {
                    xCheckBoxHOption chk = cntrl as xCheckBoxHOption;
                    if (chk != null)
                    {
                        if (chk.Value.Equals(SelectedValue))
                        {
                            chk.Checked = true;
                        }
                        else { chk.Checked = false; }
                    }
                }
            }
            base.OnPropertyChanged(e);
        }

    }

    public class CheckBoxHOption{
     public string Label{ get; set; }
     public object Value{ get; set; }
     public int Width { get; set; } = 65;

    }
}
