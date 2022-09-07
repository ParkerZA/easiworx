using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using DevAge.Windows.Forms;

namespace Finx.App.UserControls
{
    public class MyLinkLabel : LinkLabel
    {
        public string Link { get; set; }

        protected override void OnClick(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Link))
                Process.Start(Link);

            base.OnClick(e);
        }
    }

    public class DevAgeCheckBox: System.Windows.Forms.CheckBox
    {
       
        bool _ReadOnly;

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hwnd, Int32 wCmd);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hwnd, Int32 wMsg, bool wParam, Int32 lParam);

        const int EM_SETREADONLY = 0x00CF;
        const int EM_EMPTYUNDOBUFFER = 0x00CD;
        const int CB_SHOWDROPDOWN = 0x014F;
        const int GW_CHILD = 5;


        #region Generic validation methods
        //protected override void OnValidating(CancelEventArgs e)
        //{
        //    base.OnValidating(e);

        //    object val;
        //    if (IsValidValue(out val) == false)
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        if (FormatValue && Validator != null)
        //        {
        //            Text = Validator.ValueToDisplayString(val);
        //        }
        //    }
        //}

        private bool mFormatValue = true;
        /// <summary>
        /// Gets or sets a property to enable or disable the automatic format of the Text when validating the control.
        /// Default false.
        /// </summary>
        [DefaultValue(false)]
        public bool FormatValue
        {
            get { return mFormatValue; }
            set { mFormatValue = value; }
        }

        private DevAge.ComponentModel.Validator.IValidator mValidator = null;
        /// <summary>
        /// Gets or sets the Validator class useded to validate the value and convert the text when using the Value property.
        /// You can use the ApplyValidatorRules method to apply the settings of the Validator directly to the ComboBox, for example the list of values.
        /// </summary>
        [DefaultValue(null)]
        public DevAge.ComponentModel.Validator.IValidator Validator
        {
            get { return mValidator; }
            set
            {
                if (mValidator != value)
                {
                    if (mValidator != null)
                        mValidator.Changed -= mValidator_Changed;

                    mValidator = value;
                    mValidator.Changed += mValidator_Changed;
                    ApplyValidatorRules();
                }
            }
        }

        void mValidator_Changed(object sender, EventArgs e)
        {
            ApplyValidatorRules();
        }

        ///// <summary>
        ///// Apply the current Validator rules. This method is automatically fired when the Validator change.
        ///// </summary>
        //protected virtual void ApplyValidatorRules()
        //{

        //}

        /// <summary>
        /// Check if the selected value is valid based on the current validator and returns the value.
        /// </summary>
        /// <param name="convertedValue"></param>
        /// <returns></returns>
        public bool IsValidValue(out object convertedValue)
        {
            //Note:
            // SelectedValue is only valid when data binding is active otherwise
            // you must use SelectedItem

            object valToCheck;
            if (this.Checked != null)
                valToCheck = this.Checked;
            else if (this.Checked != null)
                valToCheck = this.Checked;//this.SelectedItem;
            else
                valToCheck = this.Checked;

            if (Validator != null)
            {
                if (Validator.IsValidObject(valToCheck, out convertedValue))
                    return true;
                else
                    return false;
            }
            else
            {
                convertedValue = valToCheck;
                return true;
            }
        }

        /// <summary>
        /// Gets or sets the typed value for the control, using the Validator class.
        /// If the Validator is ull the Text property is used.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get
            {
                object val;
                if (IsValidValue(out val))
                    return val;
                else
                    throw new ArgumentOutOfRangeException("Text");
            }
            set
            {
                if (Validator != null)
                {
                    Text = Validator.ValueToDisplayString(value);
                }
                else
                {
                    if (value == null)
                        Text = "";
                    else
                        Text = value.ToString();
                }
            }
        }

        #endregion

        /// <summary>
        /// Loads the Items from the StandardValues and the DropDownStyle based on the parameters of the validator.
        /// Apply the current Validator rules. This method is automatically fired when the Validator change.
        /// </summary>
        protected virtual void ApplyValidatorRules()
        {
           
        }

        //protected override void OnFormat(ListControlConvertEventArgs e)
        //{
        //    base.OnFormat(e);

        //    // The method converts only to string type. 
        //    if (e.DesiredType != typeof(string) || Validator == null)
        //        return;

        //    e.Value = Validator.ValueToDisplayString(e.ListItem);
        //}

        //protected override void OnPaint(PaintEventArgs e)

        //{
        //    base.OnPaint(e);

        //    e.Graphics.FillRectangle(

        //    Brushes.Blue, this.ClientRectangle);

        //    e.Graphics.DrawString(

        //    this.Text, this.Font, Brushes.White, this.Location);

        //}

        //protected override void WndProc(ref Message m)
        //{
        //    if (_ReadOnly && _DroppedDown)
        //        if (m.Msg == 273)
        //        {
        //            _DroppedDown = false;
        //            SendMessage(this.Handle, CB_SHOWDROPDOWN, false, 0);
        //        }


        //    base.WndProc(ref m);
        //}

        public bool ReadOnly
        {
            get
            { return _ReadOnly; }
            set
            {
                if (value == _ReadOnly)
                    return;
                if (!DesignMode)
                {
                    //if (value)
                    //{
                    //    _DropdownStyle = base.DropDownStyle;
                    //    base.DropDownStyle = ComboBoxStyle.DropDown;

                    //    _AutoCompleteMode = base.AutoCompleteMode;
                    //    base.AutoCompleteMode = AutoCompleteMode.None;

                    //}
                    //else
                    //{
                    //    base.DropDownStyle = _DropdownStyle;
                    //    base.AutoCompleteMode = _AutoCompleteMode;
                    //}
                }

                _ReadOnly = value;
                base.TabStop = !value;
                //base.SelectionLength = 0;

                //SendMessage(GetWindow(this.Handle, GW_CHILD), EM_SETREADONLY, value, 0);
                //SendMessage(GetWindow(this.Handle, GW_CHILD), EM_EMPTYUNDOBUFFER, value, 0);

                //_DroppedDown = false;
                ReadOnly = _ReadOnly;

                this.Refresh();
            }
        }
    }
}
