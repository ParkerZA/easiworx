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
    public partial class MetroProgressWindow : MetroForm, IProgressCallback
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
     
        public delegate void SetTextInvoker(String text);
        public delegate void IncrementInvoker(int val);
        public delegate void StepToInvoker(int val);
        public delegate void RangeInvoker(int minimum, int maximum);

        private String titleRoot = "";
        private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
        private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
        private bool requiresClose = true;

        public object DataObject { get; set; }

        public MetroProgressWindow()
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

        #region Implementation of IProgressCallback
        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress meter.
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        public void Begin(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoBegin), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        public void Begin()
        {
            initEvent.WaitOne();
            Invoke(new MethodInvoker(DoBegin));
        }

        /// <summary>
        /// Call this method from the worker thread to reset the range in the progress callback
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void SetRange(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoSetRange), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        public void SetText(String text)
        {
            Invoke(new SetTextInvoker(DoSetText), new object[] { text });

            //MethodInvoker inv = delegate
            //{
            //    this.label.Text = text;
            //};

            //this.Invoke(inv);

        }

        public void SetCaption(String text)
        {
            Invoke(new SetTextInvoker(DoSetCaption), new object[] { text });
        }
        /// <summary>
        /// Call this method from the worker thread to increase the progress counter by a specified value.
        /// </summary>
        /// <param name="val">The amount by which to increment the progress indicator</param>
        public void Increment(int val)
        {
            Invoke(new IncrementInvoker(DoIncrement), new object[] { val });
        }

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a particular value.
        /// </summary>
        /// <param name="val"></param>
        public void StepTo(int val)
        {
            Invoke(new StepToInvoker(DoStepTo), new object[] { val });
        }


        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        public bool IsAborting
        {
            get
            {
                return abortEvent.WaitOne(0, false);
            }
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        public void End()
        {
            if (requiresClose)
            {
                Invoke(new MethodInvoker(DoEnd));
            }
        }
        #endregion

        #region Implementation members invoked on the owner thread
        private void DoSetText(String text)
        {
            metroLabel_Status.Text = text;
          
        }

        private void DoSetCaption(String text)
        {
            metroLabel_Caption.Text = text;
        }
        private void DoIncrement(int val)
        {
            try
            {
                metroProgressBar1.Increment(val);
                metroProgressSpinner1.Value += val;
            }
            catch (Exception) { };

            UpdateStatusText();
        }

        private void DoStepTo(int val)
        {
            try
            {
                metroProgressBar1.Value = val;
                metroProgressSpinner1.Value = val;
            }
            catch (Exception) { };

            UpdateStatusText();
        }

        private void DoBegin(int minimum, int maximum)
        {
            DoBegin();
            DoSetRange(minimum, maximum);
        }

        private void DoBegin()
        {
            metroButton_Cancel.Enabled = true;
            ControlBox = true;
        }

        private void DoSetRange(int minimum, int maximum)
        {
            metroProgressBar1.Minimum = minimum;
            metroProgressBar1.Maximum = maximum;
            metroProgressBar1.Value = minimum;

            metroProgressSpinner1.Minimum = minimum;
            metroProgressSpinner1.Maximum = maximum;
            metroProgressSpinner1.Value = minimum;

        }

        private void DoEnd()
        {
            Close();
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Handles the form load, and sets an event to ensure that
        /// intialization is synchronized with the appearance of the form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            ControlBox = false;
            initEvent.Set();
        }

    

        /// <summary>
        /// Handler for 'Close' clicking
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            requiresClose = false;
            AbortWork();
            base.OnClosing(e);

        }
        #endregion

        #region Implementation Utilities
        /// <summary>
        /// Utility function that formats and updates the title bar text
        /// </summary>
        private void UpdateStatusText()
        {
            Text = titleRoot + String.Format(" - {0}% complete", (metroProgressBar1.Value * 100) / (metroProgressBar1.Maximum - metroProgressBar1.Minimum));
        }

        /// <summary>
        /// Utility function to terminate the thread
        /// </summary>
        private void AbortWork()
        {
            abortEvent.Set();
        }
        #endregion
             
    }

    
}
