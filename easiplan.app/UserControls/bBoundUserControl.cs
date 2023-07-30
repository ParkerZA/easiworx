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
    [System.ComponentModel.DefaultBindingProperty("SelectedValue")]
    public partial class bBoundUserControl : UserControl,INotifyPropertyChanged
    {
        internal int left = 0;
        internal int top = 0;

        public bBoundUserControl()
        {
            InitializeComponent();
        }

        public virtual void InitialiseControls(){

            if (!string.IsNullOrEmpty(Title))
            {
                MetroLabel lblTitle = new MetroLabel();
                lblTitle.ForeColor = Color.Black;
                lblTitle.UseCustomForeColor = true;
                lblTitle.AutoSize = true;
                lblTitle.Text = Title;
                lblTitle.Top = top;
                lblTitle.Width = this.Width;

                this.MainPanel.Controls.Add(lblTitle);
           
                top += lblTitle.Height;
            }

            if (!string.IsNullOrEmpty(Hint))
            {
                MetroLabel lblHint = new MetroLabel();
                lblHint.ForeColor = Color.Gray;
                lblHint.UseCustomForeColor = true;
                lblHint.Text = Hint;
                lblHint.Top = top;
                lblHint.Width = this.Width;
                lblHint.WrapToLine = true;
                lblHint.Height = lblHint.Height * NoOfHintLines;

                this.MainPanel.Controls.Add(lblHint);

                top += lblHint.Height;
            }
        }

        public virtual void AddDataBinding<T>(T obj, string propertyName)
        {
            InitialiseControls();
            //this.DataBindings.Clear();
            this.DataBindings.Add("SelectedValue", obj, propertyName, true, DataSourceUpdateMode.OnPropertyChanged);

        }

        object _selectedValue;
        [Bindable(true)]
        public object SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue!=value)
                    { _selectedValue = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedValue")); }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public string Hint { get; set; }
        public string Title { get; set; }
        public int NoOfHintLines { get; set; } = 1;
    }

}
