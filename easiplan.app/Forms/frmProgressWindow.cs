using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Finx.App.UserControls;

namespace Finx.App.Forms
{
	/// <summary>
	/// Summary description for ProgressWindow.
	/// </summary>
	public class ProgressWindow : Form, IProgressCallback
	{
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.ProgressBar progressBar;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public delegate void SetTextInvoker(String text);
		public delegate void IncrementInvoker( int val );
		public delegate void StepToInvoker( int val );
		public delegate void RangeInvoker( int minimum, int maximum );

		private String titleRoot = "";
		private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
		private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
        public Label Caption;
        private Label label1;
        private bool requiresClose = true;

		public ProgressWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		#region Implementation of IProgressCallback
		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress meter.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		public void Begin( int minimum, int maximum )
		{
			initEvent.WaitOne();
			Invoke( new RangeInvoker( DoBegin ), new object[] { minimum, maximum } );
		}

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback, without setting the range
		/// </summary>
		public void Begin()
		{
			initEvent.WaitOne();
			Invoke( new MethodInvoker( DoBegin ) );
		}

		/// <summary>
		/// Call this method from the worker thread to reset the range in the progress callback
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		public void SetRange( int minimum, int maximum )
		{
			initEvent.WaitOne();
			Invoke( new RangeInvoker( DoSetRange ), new object[] { minimum, maximum } );
		}

		/// <summary>
		/// Call this method from the worker thread to update the progress text.
		/// </summary>
		/// <param name="text">The progress text to display</param>
		public void SetText( String text )
		{
            Invoke( new SetTextInvoker(DoSetText), new object[] { text } );

            //MethodInvoker inv = delegate
            //{
            //    this.label.Text = text;
            //};

            //this.Invoke(inv);

        }

		/// <summary>
		/// Call this method from the worker thread to increase the progress counter by a specified value.
		/// </summary>
		/// <param name="val">The amount by which to increment the progress indicator</param>
		public void Increment( int val )
		{
			Invoke( new IncrementInvoker( DoIncrement ), new object[] { val } );
		}

		/// <summary>
		/// Call this method from the worker thread to step the progress meter to a particular value.
		/// </summary>
		/// <param name="val"></param>
		public void StepTo( int val )
		{
			Invoke( new StepToInvoker( DoStepTo ), new object[] { val } );
		}

		
		/// <summary>
		/// If this property is true, then you should abort work
		/// </summary>
		public bool IsAborting
		{
			get
			{
				return abortEvent.WaitOne( 0, false );
			}
		}

		/// <summary>
		/// Call this method from the worker thread to finalize the progress meter
		/// </summary>
		public void End()
		{
			if( requiresClose )
			{
				Invoke( new MethodInvoker( DoEnd ) );
			}
		}

		public object DataObject { get; set; }
		#endregion

		#region Implementation members invoked on the owner thread
		private void DoSetText( String text )
		{
			label.Text = text;
        }

		private void DoIncrement( int val )
		{
			progressBar.Increment( val );
			UpdateStatusText();
		}

		private void DoStepTo( int val )
		{
			progressBar.Value = val;
			UpdateStatusText();
		}

		private void DoBegin( int minimum, int maximum )
		{
			DoBegin();
			DoSetRange( minimum, maximum );
		}

		private void DoBegin()
		{
			cancelButton.Enabled = true;
			ControlBox = true;
		}

		private void DoSetRange( int minimum, int maximum )
		{
			progressBar.Minimum = minimum;
			progressBar.Maximum = maximum;
			progressBar.Value = minimum;
			
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
			base.OnLoad( e );
			ControlBox = false;
			initEvent.Set();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Handler for 'Close' clicking
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			requiresClose = false;
			AbortWork();
			base.OnClosing( e );
		}
		#endregion
		
		#region Implementation Utilities
		/// <summary>
		/// Utility function that formats and updates the title bar text
		/// </summary>
		private void UpdateStatusText()
		{
			Text = titleRoot + String.Format( " - {0}% complete", (progressBar.Value * 100 ) / (progressBar.Maximum - progressBar.Minimum) );
		}
		
		/// <summary>
		/// Utility function to terminate the thread
		/// </summary>
		private void AbortWork()
		{
			abortEvent.Set();
		}
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressWindow));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.Caption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(8, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(194, 23);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(55, 42);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(223, 34);
            this.label.TabIndex = 0;
            this.label.Text = "Starting operation...";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(208, 79);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Visible = false;
            // 
            // Caption
            // 
            this.Caption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Caption.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Caption.Location = new System.Drawing.Point(55, 9);
            this.Caption.Margin = new System.Windows.Forms.Padding(0);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(225, 29);
            this.Caption.TabIndex = 3;
            this.Caption.Text = "Starting operation...";
            // 
            // label1
            // 
            this.label1.Image = global::easiplan.app.Properties.Resources.progress_icon;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 50);
            this.label1.TabIndex = 4;
            // 
            // ProgressWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(290, 113);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Caption);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressWindow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProgressWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void ProgressWindow_Load(object sender, EventArgs e)
        {
			
			this.Text = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
        }
    }
}
