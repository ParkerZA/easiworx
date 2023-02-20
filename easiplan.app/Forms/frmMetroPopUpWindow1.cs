using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Finx.App.UserControls;
using MetroFramework.Controls;

namespace Finx.App.Forms
{
    public partial class MetroPopUpWindow : MetroForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>

        public delegate void SetTextInvoker(String text);
        public delegate void IncrementInvoker(int val);
        public delegate void StepToInvoker(int val);
        public delegate void RangeInvoker(int minimum, int maximum);

        private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
        private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
        

        public object DataObject { get; set; }

        public MetroPopUpWindow()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            this.Text = string.Format("     {0} v{1}", Application.ProductName, Application.ProductVersion);

            this.BackImage = global::easiplan.app.Properties.Resources.finworks_b_42;
            this.BackImagePadding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.BackMaxSize = 50;

            this.ForeColor = Global.DFLT_PRIM_CLR;
        }

        public void SetCaption(String text)
        {
            Invoke(new SetTextInvoker(DoSetCaption), new object[] { text });
        }

        private void DoSetCaption(String text)
        {
            metroLabel_Caption.Text = text;
        }

        private void metroButton_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


}
