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
using System.Threading;

namespace Finx.App.Forms
{
    public partial class frmCsvImportProgressWindow : MetroForm, IProgressCallback
    {
        private string titleRoot = "";
        public delegate void SetTextInvoker(string text);
        public delegate void IncrementInvoker(int val);
        public delegate void StepToInvoker(int val);
        public delegate void RangeInvoker(int minimum, int maximum);
        private ManualResetEvent manualResetEventInit = new ManualResetEvent(false);
        private ManualResetEvent manualResetEventAbort = new ManualResetEvent(false);
        private bool requiresClose = true;
        
        public frmCsvImportProgressWindow()
        {
            InitializeComponent();
            InitialiseFormProperties();
        }

        private void InitialiseFormProperties()
        {
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
            manualResetEventInit.WaitOne();
            Invoke(new RangeInvoker(Initialise), new object[2] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        public void Begin()
        {
            manualResetEventInit.WaitOne();
            Invoke(new MethodInvoker(EnableCancelAndControlBox));
        }

        /// <summary>
        /// Call this method from the worker thread to reset the range in the progress callback
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void SetRange(int minimum, int maximum)
        {
            manualResetEventInit.WaitOne();
            Invoke(new RangeInvoker(SetProgressBarRange), new object[2] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        public void SetText(String text)
        {
            Invoke(new SetTextInvoker(DoSetText), new object[1] { text });
        }

        public void SetCaption(string text)
        {
            Invoke(new SetTextInvoker(DoSetCaption), new object[] { text });
        }
        /// <summary>
        /// Call this method from the worker thread to increase the progress counter by a specified value.
        /// </summary>
        /// <param name="val">The amount by which to increment the progress indicator</param>
        public void Increment(int val)
        {
            Invoke(new IncrementInvoker(IncrementProgressBarAndSpinnerValues), new object[1] { val });
        }

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a particular value.
        /// </summary>
        /// <param name="val"></param>
        public void StepTo(int val)
        {
            Invoke(new StepToInvoker(SetProgressBarAndSpinnerValuesAndUpdateStatusText), new object[1] { val });
        }


        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        public bool IsAborting
        {
            get
            {
                return manualResetEventAbort.WaitOne(0, false);
            }
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        public void End()
        {
            if (requiresClose)
            {
                Invoke(new MethodInvoker(CloseForm));
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
        private void IncrementProgressBarAndSpinnerValues(int val)
        {
            try
            {
                metroProgressBar1.Increment(val);
                metroProgressSpinner1.Value += val;
            }
            catch (Exception) { };

            UpdateStatusText();
        }

        private void SetProgressBarAndSpinnerValuesAndUpdateStatusText(int val)
        {
            try
            {
                metroProgressBar1.Value = val;
                metroProgressSpinner1.Value = val;
            }
            catch (Exception) { };

            UpdateStatusText();
        }

        private void Initialise(int minimum, int maximum)
        {
            EnableCancelAndControlBox();
            SetProgressBarRange(minimum, maximum);
        }

        private void EnableCancelAndControlBox()
        {
            metroButton_Cancel.Enabled = true;
            ControlBox = true;
        }

        private void SetProgressBarRange(int minimum, int maximum)
        {
            metroProgressBar1.Minimum = minimum;
            metroProgressBar1.Maximum = maximum;
            metroProgressBar1.Value = minimum;

            metroProgressSpinner1.Minimum = minimum;
            metroProgressSpinner1.Maximum = maximum;
            metroProgressSpinner1.Value = minimum;

        }

        private void CloseForm()
        {
            this.Close();
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
            manualResetEventInit.Set();
        }

    

        /// <summary>
        /// Handler for 'Close' clicking
        /// </summary>
        /// <param name="args"></param>
        protected override void OnClosing(CancelEventArgs args)
        {
            requiresClose = false;
            AbortWork();
            base.OnClosing(args);

        }
        #endregion

        #region Implementation Utilities
        /// <summary>
        /// Utility function that formats and updates the title bar text
        /// </summary>
        private void UpdateStatusText()
        {
            Text = titleRoot + string.Format(" - {0}% complete", (metroProgressBar1.Value * 100) / (metroProgressBar1.Maximum - metroProgressBar1.Minimum));
        }

        /// <summary>
        /// Utility function to terminate the thread
        /// </summary>
        private void AbortWork()
        {
            manualResetEventAbort.Set();
        }
        #endregion

        private void metroButton_Cancel_Click(object sender, EventArgs e)
        {
            AbortWork();
        }
    }

    
}
