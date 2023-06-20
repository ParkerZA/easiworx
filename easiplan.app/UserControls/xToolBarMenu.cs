using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finx.App.Extensions;
using System.Runtime.InteropServices;

namespace Finx.App.UserControls
{
    public partial class xToolBarMenu : UserControl
    {
        public bool IsInEditMode;

        public virtual event EventHandler<EventArgs> CloseClicked;
        public virtual event EventHandler<EventArgs> EditClicked;
        public virtual event EventHandler<EventArgs> SaveClicked;
        public virtual event EventHandler<EventArgs> RefreshClicked;
        public virtual event EventHandler<EventArgs> NotesClicked;
        public xToolBarMenu()
        {
            InitializeComponent();

            this.tbClose.DoubleClickEnabled = true;
            this.tbEdit.DoubleClickEnabled = true;
            this.tbNotes.DoubleClickEnabled = true;
            this.tbRefresh.DoubleClickEnabled = true;
            this.tbSave.DoubleClickEnabled = true;
            
            SetEditMode(false);

            //this.tbCaption.Visible = false;
            this.tbCaption.Text = "";
            this.tbCaptionImage.Visible = false;
        }

        public virtual void tbClose_Click(object sender, EventArgs e)
        {
            if (IsInEditMode )
                if (!MessageBoxExt.ShowQuestion("Are you sure to close without saving ?"))
                    return;

            CloseClicked?.Invoke(this, e);

       
        }

        public virtual void tbEdit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SetEditMode(!IsInEditMode);
            EditClicked?.Invoke(this, e);

            this.Cursor = Cursors.Default;
        }

        public virtual void tbRefresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SetEditMode(false);
            RefreshClicked?.Invoke(this, e);

            this.Cursor = Cursors.Default;
        }

        public virtual void tbSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SetEditMode(false);
            SaveClicked?.Invoke(this, e);

            this.Cursor = Cursors.Default;
        }

        public void SetEditMode(bool Value)
        {
            IsInEditMode = Value;

            this.tbRefresh.Enabled = !IsInEditMode;
            this.tbEdit.Enabled = !IsInEditMode;
            this.tbSave.Enabled = IsInEditMode;
        }

        #region CAR Edit Modes
        public void SetCAREditBeforeSave(bool Value)
        {
            IsInEditMode = Value;

            this.tbRefresh.Enabled = !IsInEditMode;
            this.tbEdit.Enabled = !IsInEditMode;
            this.tbSave.Enabled = IsInEditMode;
        }

        public void SetCAREditAfterSave(bool Value)
        {
            IsInEditMode = !Value;

            this.tbRefresh.Enabled = !IsInEditMode;
            this.tbEdit.Enabled = IsInEditMode;
            this.tbSave.Enabled = IsInEditMode;
        }

        public void SetCAREdit(bool Value)
        {
            IsInEditMode = Value;

            this.tbRefresh.Enabled = IsInEditMode;
            this.tbEdit.Enabled = !IsInEditMode;
            this.tbSave.Enabled = IsInEditMode;
        }

        public void CarModeAddRecord(bool Value)
        {
            IsInEditMode = Value;

            this.tbRefresh.Enabled = IsInEditMode;
            this.tbEdit.Enabled = IsInEditMode;
            this.tbSave.Enabled = !IsInEditMode;
        }

        #endregion

        private void tbNotes_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SetEditMode(false);
            NotesClicked?.Invoke(this, e);

            this.Cursor = Cursors.Default;
        }
    }

    /// <summary>
    /// This class adds on to the functionality provided in System.Windows.Forms.ToolStrip.
    /// </summary>
    public class ToolStripEx
    : ToolStrip
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private const int WM_PARENTNOTIFY = 0x210;
        private const int WM_LBUTTONDOWN = 0x201;

        private bool clickThrough = true;

        /// <summary>
        /// Gets or sets whether the ToolStripEx honors item clicks when its containing form does
        /// not have input focus.
        /// </summary>
        /// <remarks>
        /// Default value is false, which is the same behavior provided by the base ToolStrip class.
        /// </remarks>
        public bool ClickThrough
        {
            get
            {
                return this.clickThrough;
            }

            set
            {
                this.clickThrough = value;
            }
        }

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);

        //    if (this.clickThrough &&
        //        m.Msg == NativeConstants.WM_MOUSEACTIVATE &&
        //        m.Result == (IntPtr)NativeConstants.MA_ACTIVATEANDEAT)
        //    {
        //        m.Result = (IntPtr)NativeConstants.MA_ACTIVATE;
        //    }
        //}

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PARENTNOTIFY)
            {
                if (m.WParam.ToInt32() == WM_LBUTTONDOWN )
                {
                    Point p = PointToClient(Cursor.Position);
                    if (GetChildAtPoint(p) is ToolStrip)
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, 0);
                }
            }
            base.WndProc(ref m);
        }
    }

    internal sealed class NativeConstants
    {
        private NativeConstants()
        {
        }

        internal const uint WM_MOUSEACTIVATE = 0x21;
        internal const uint MA_ACTIVATE = 1;
        internal const uint MA_ACTIVATEANDEAT = 2;
        internal const uint MA_NOACTIVATE = 3;
        internal const uint MA_NOACTIVATEANDEAT = 4;

      
    }

   
}
