using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.IO;
using QSS.Components.Windows.Forms;
using QSS.Components;

using DevAge.Windows.Forms;

using easiplan.domain;

using my.domain.lib.core.Domain;
using System.Runtime.InteropServices;
using PresentationControls;

namespace Finx.App.UserControls
{
    public partial class xInput : UserControl
    {
        Binding binding;
        BindingSource bindingSource;
        BindingList<object> bindinglist = new BindingList<object>();
        BindingSource bSource = new BindingSource();

        BaseEntity<int> _model;

        EditBalloonToolTip editToolTip = new EditBalloonToolTip();
        BalloonToolTip validationTooltip = new BalloonToolTip();

        new bool GotFocus = false;

        public DevAgeTextBox textBox = new DevAgeTextBox();
        public DevAgeComboBox comboBox = new DevAgeComboBox();
        public CheckBoxComboBox chkboxComboBox = new CheckBoxComboBox();
        public CheckBox chkBox= new CheckBox();

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;


        public event EventHandler EnterKeyPressed;
        public event EventHandler KeyPressed;
        public BaseEntity<int> Model
        {
            get { return _model; }
            set
            {
                this.label1.Font = Global.LableFont;
              
                if (MappedField != null && value != null)
                {
                    try
                    {
                        _model = value;
                        _model.ModelCalculated += _model_ModelCalculated;

                        InitialiseControl(value);


                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message);
                    }
                    finally
                    {
                        this.panel1.Width = this.Width - this.panel_Cntrl.Width - 5;
                        this.panel_Cntrl.ResumeLayout();
                        this.panel_Cntrl.Refresh();
                        this.ResumeLayout();
                    }


                }
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            //if (binding != null)
            //    binding.WriteValue();


        }

        private void ShowTextBox(object value)
        {

            #region bindings
            binding = new Binding("Text", value, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);

            textBox.DataBindings.Clear();
            textBox.DataBindings.Add(binding);
            #endregion

            bindingSource = new BindingSource();
            bindingSource.DataSource = value;
            textBox.DataBindings.Clear();
            textBox.DataBindings.Add("Text", bindingSource, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);


            #region Font
            textBox.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
            #endregion

            #region Style
            // textBox.BorderStyle = BorderStyle.FixedSingle;
            // textBox.Location = new Point(-1, -1);
            // textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Dock = DockStyle.Fill;
            textBox.Width = this.panel_Cntrl.Width - 1;
            #endregion

            #region ControlType
            if (_ControlTypes == UserControls.ControlTypes.AmountBox)
            {
                textBox.DataBindings[0].FormatString = "c";
                textBox.TextAlign = HorizontalAlignment.Right;
            }
            if (_ControlTypes == UserControls.ControlTypes.PercentBox)
            {
                binding.Parse += new ConvertEventHandler(StringToPercent);

                textBox.DataBindings[0].FormatString = "P2";
                textBox.TextAlign = HorizontalAlignment.Right;
            }
            if (_ControlTypes == UserControls.ControlTypes.IntBox)
            {
                // textBox.DataBindings[0].FormatString = "P";
                textBox.TextAlign = HorizontalAlignment.Right;
            }
            if (_ControlTypes == UserControls.ControlTypes.MultiLineTextBox)
            {
                textBox.Multiline = true;


            }
            #endregion

            #region Password Image
            PictureBox picBox = new PictureBox();
            picBox.Image = easiplan.app.Properties.Resources.eye_32;
            picBox.SizeMode = PictureBoxSizeMode.CenterImage;
            picBox.Width = 26;
            picBox.Height = 26;
            picBox.Dock = DockStyle.Right;

            picBox.MouseDown += picBox_MouseDown;
            picBox.MouseUp += picBox_MouseUp;

            if (_ControlTypes == UserControls.ControlTypes.PasswordBox)
            {
                textBox.UseSystemPasswordChar = true;
                // textBox.Dock = DockStyle.None;
                //  textBox.Width = ControlWidth - 30;
                // this.Width += 30;
                textBox.Width = this.panel_Cntrl.Width - 30;

                this.panel_Cntrl.Controls.Add(picBox);
            }

            if (_ControlTypes == UserControls.ControlTypes.PasswordBoxNoPreview)
            {
                textBox.UseSystemPasswordChar = true;
            }
            #endregion

            this.textBox.ReadOnly = ReadOnly;
            if (ReadOnly)
            {
                this.textBox.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.textBox.BackColor = Color.White;
            }


            this.panel_Cntrl.Controls.Add(textBox);
        }

        //protected override void SetBoundsCore(int x, int y,
        //int width, int height, BoundsSpecified specified)
        //{
        //    //base.SetBoundsCore(x, y, width, textBox.PreferredHeight, specified);
        //}

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            //  this.panel_Cntrl.BackColor = SystemColors.ControlDark;
            GotFocus = true;
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tB = (TextBox)sender;

            try
            {
                validationTooltip = this.Tag as BalloonToolTip;
                if (null != validationTooltip)
                {
                    validationTooltip.RemoveAll();
                }

                if (GotFocus && !ReadOnly)
                {
                    if (MappedField != null)
                        _model.Validate(MappedField.ToLower());

                    this.BackColor = Color.Transparent;

                    GotFocus = false;
                }

            }
            catch (WarningException x)
            {
                this.SetEditTooltip("Warning", x.Message, "Warning");
                // this.SetValidationTooltip("Warning", x.Message, "Warning");

                this.BackColor = Color.Orange;

                tB.Focus();

            }
            catch (Exception ex)
            {
                this.SetEditTooltip("Error", ex.Message);

                this.BackColor = Color.Red;

                tB.Focus();
            }
            finally
            {
                //  this.panel_Cntrl.BackColor = SystemColors.ButtonFace;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox.Undo();
                editToolTip.Dispose();
                // GotFocus = false;
            }

            if (e.KeyCode == Keys.Enter)
            {
                EnterKeyPressed?.Invoke(sender, e);
                //suppress the beep sound
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                KeyPressed?.Invoke(sender, e);
            }

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    EnterKeyPressed?.Invoke(sender, e);
            //    //suppress the beep sound
            //    e.Handled = true;
            //    e.SuppressKeyPress = true;
            //}
            //else
            //{
            //    KeyPressed?.Invoke(sender, e);
            //}
        }
        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            //TextBox tB = (TextBox)sender;

            //try
            //{
            //    _model.Validate(MappedField);

            //    if (binding != null)
            //        binding.ReadValue();

            //    e.Cancel = false;
            //}
            //catch (WarningException x)
            //{

            //    e.Cancel = true;
            //    this.SetTooltip("Warning", x.Message, "Warning");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    this.SetTooltip("Error", ex.Message);
            //}


        }

        private void StringToPercent(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only. 
            if (cevent.DesiredType != typeof(double)) return;
            try
            {
                // Converts the string back to decimal using the static Parse method.
                cevent.Value = Double.Parse(cevent.Value.ToString()) / 100;
            }
            catch (Exception x) 
            {
                Program.Logger.Error(x);
            }
        }

        void fileBtn_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.panel_Cntrl.Controls[0] as TextBox;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Word Files (*.docx)|*.docx|Excel Files (*.xlsx)|*.xlsx|Pdf Files (*.pdf)|*pdf";
            try
            {
                openFileDialog.InitialDirectory = new FileInfo(txtBox.Text).DirectoryName;
            }
            catch (Exception x)
            { }


            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string FileName = openFileDialog.FileName;
                    FileInfo fInfo = new FileInfo(FileName);


                    txtBox.Text = fInfo.FullName;
                    this.Model.InvokePropertyChanged(MappedField);


                }
                catch (Exception x)
                {

                }
            }
        }

        void folderBtn_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.panel_Cntrl.Controls[0] as TextBox;

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            openFileDialog.ShowNewFolderButton = true;

            openFileDialog.Description = "Choose the directory for the client files ...";

            try
            {
                openFileDialog.SelectedPath = txtBox.Text;
            }
            catch (Exception x)
            { }


            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string FileName = openFileDialog.SelectedPath;
                    DirectoryInfo fInfo = new DirectoryInfo(FileName);


                    txtBox.Text = fInfo.FullName;
                    this.Model.InvokePropertyChanged(MappedField);


                }
                catch (Exception x)
                {

                }
            }
        }

        void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            UsePasswordChar(true);
        }

        void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            UsePasswordChar(false);
        }

        public void UsePasswordChar(bool IsPasswordChar)
        {
            textBox.UseSystemPasswordChar = IsPasswordChar;
        }

        void _model_ModelCalculated(object sender, EntityEventArgs e)
        {
            if (binding != null)
                binding.ReadValue();
        }

        public string LableText { get { return this.label1.Text; } set { this.label1.Text = value; } }

        ControlTypes _ControlTypes = ControlTypes.TextBox;
        public ControlTypes ControlTypes { get { return _ControlTypes; } set { _ControlTypes = value; } }

        LablePosition _LablePosition = LablePosition.Left;
        public LablePosition LablePosition
        {
            get { return _LablePosition; }
            set
            {
                _LablePosition = value;
                switch (value)
                {
                    case UserControls.LablePosition.Top:
                        this.panel1.Dock = DockStyle.Top;
                        this.panel1.Width = this.Width;

                        this.panel_Cntrl.Dock = DockStyle.Fill;
                        this.Height += 25;
                        break;
                    default:
                        this.panel1.Dock = DockStyle.Left;
                        this.panel_Cntrl.Dock = DockStyle.Right;
                        break;
                };

            }
        }

        public string MappedField
        {
            get;
            set;
        }

        public xInput()
        {

            InitializeComponent();
            // this.panel1.Width = this.Width - this.panel_Cntrl.Width - 20;

            #region TextBox

            textBox.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
            this.label1.Font = Global.LableFont;
            this.label1.Width = 100;

            textBox.Validating += new CancelEventHandler(TextBox_Validating);
            textBox.KeyDown += TextBox_KeyDown;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.KeyUp += TextBox_KeyUp;
            textBox.KeyPress += TextBox_KeyPress;

            textBox.Disposed += TextBox_Disposed;
            #endregion

            //this.ResizeRedraw = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this.AutoSize = false;



            chkboxComboBox.DataBindings.DefaultDataSourceUpdateMode
                                   = DataSourceUpdateMode.OnPropertyChanged;

            comboBox.TextChanged += ComboBox_TextChanged;
            comboBox.SelectionChangeCommitted += ComboBox_SelectionChangeCommitted;

        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //suppress the beep sound
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        public void InitialiseControl(object value = null)
        {
            this.panel_Cntrl.SuspendLayout();
            this.SuspendLayout();

            this.panel_Cntrl.Controls.Clear();

            switch (_ControlTypes)
            {
                case UserControls.ControlTypes.ComboList:
                case UserControls.ControlTypes.ComboBox:
                case UserControls.ControlTypes.ComboBoxText:
                    if (ReadOnly)
                        ShowTextBox(value);
                    else
                    {
                        #region ComboBox
                        comboBox.Font = Global.TextFont;
                        comboBox.Width = _ControlWidth;
                        comboBox.Dock = DockStyle.Fill;

                        comboBox.FlatStyle = FlatStyle.Standard;
                       

                        #endregion



                        if (_ControlTypes == UserControls.ControlTypes.ComboList)
                        {
                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                            if (value != null)
                            {

                                bindingSource = new BindingSource();
                                bindingSource.DataSource = value;
                                comboBox.DataBindings.Clear();
                                comboBox.DataBindings.Add("SelectedValue", bindingSource, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);
                            }
                        }
                        else
                        {
                            //binding = new Binding("Text", value, MappedField, false, DataSourceUpdateMode.OnPropertyChanged);

                            //comboBox.DataBindings.Clear();
                            //comboBox.DataBindings.Add(binding);
                            if (value != null)
                            {
                                bindingSource = new BindingSource();
                                bindingSource.DataSource = value;
                                comboBox.DataBindings.Clear();
                                if (_ControlTypes == UserControls.ControlTypes.ComboBoxText)
                                {
                                    comboBox.DataBindings.Add("Text", bindingSource, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);
                                }
                                else
                                {
                                    comboBox.DataBindings.Add("SelectedValue", bindingSource, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);
                                    comboBox.DataBindings.Add("Text", bindingSource, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);
                                }
                            }
                        }



                        this.panel_Cntrl.Controls.Add(comboBox);
                    }
                    break;
                case UserControls.ControlTypes.DatePicker:

                    if (ReadOnly)
                        ShowTextBox(value);
                    else
                    {

                        DateTimePicker dt = new DateTimePicker();
                        if (!string.IsNullOrEmpty(MappedField))
                        {
                            binding = new Binding("Text", value, MappedField, false, DataSourceUpdateMode.OnPropertyChanged);
                            dt.DataBindings.Clear();
                            dt.DataBindings.Add(binding);
                        }
                        dt.Format = DateTimePickerFormat.Custom;
                        dt.CustomFormat = "dd MMM yyyy";

                        dt.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
                                                  //dt.Width = ControlWidth;
                        dt.Dock = DockStyle.Fill;

                        dt.ValueChanged += Dt_ValueChanged;

                        this.panel_Cntrl.Controls.Add(dt);
                    }

                    break;
                case UserControls.ControlTypes.CheckBox:

                    if (!string.IsNullOrEmpty(MappedField))
                    {
                        binding = new Binding("Checked", value, MappedField, false, DataSourceUpdateMode.OnPropertyChanged);
                        chkBox.DataBindings.Clear();
                        chkBox.DataBindings.Add(binding);
                    }

                    chkBox.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
                    chkBox.Width = 20;
                    chkBox.Dock = DockStyle.Left;

                    chkBox.Enabled = !ReadOnly;

                    this.panel_Cntrl.Controls.Add(chkBox);

                    break;
                case UserControls.ControlTypes.MultiSelectCombo:

                    binding = new Binding("SelectedValues", value, MappedField, false, DataSourceUpdateMode.OnPropertyChanged);
                    chkboxComboBox.DataBindings.Clear();
                    chkboxComboBox.DataBindings.Add(binding);

                    chkboxComboBox.Width = _ControlWidth;
                    chkboxComboBox.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
                    chkboxComboBox.Dock = DockStyle.Fill;

                    chkboxComboBox.Enabled = !ReadOnly;

                    this.panel_Cntrl.Controls.Add(chkboxComboBox);

                    break;
                case UserControls.ControlTypes.FileBox:
                case UserControls.ControlTypes.FolderBox:

                    if (ReadOnly)
                        ShowTextBox(value);
                    else
                    {

                        binding = new Binding("Text", value, MappedField, true, DataSourceUpdateMode.OnPropertyChanged);
                        textBox.DataBindings.Clear();
                        textBox.DataBindings.Add(binding);

                        textBox.Font = Global.TextFont;// new System.Drawing.Font("Calibri Light", 11);
                        textBox.Dock = DockStyle.Fill;
                        textBox.ReadOnly = true;

                        this.panel_Cntrl.Controls.Add(textBox);

                        Button fileBtn = new Button();
                        if (_ControlTypes == ControlTypes.FolderBox)
                        {
                            fileBtn.Click += folderBtn_Click;
                            fileBtn.Text = "...";
                        }
                        else
                        {
                            fileBtn.Click += fileBtn_Click;
                            fileBtn.Text = "...";
                        }
                        fileBtn.Dock = DockStyle.Right;
                        fileBtn.Width = 28;
                        fileBtn.Height = 20;
                        //fileBtn.MaximumSize = new Size(25, this.Height - 5);
                        fileBtn.FlatStyle = FlatStyle.System;

                        fileBtn.Cursor = Cursors.Hand;


                        this.panel_Cntrl.Controls.Add(fileBtn);
                    }
                    break;
                case UserControls.ControlTypes.AmountBox:
                case UserControls.ControlTypes.PercentBox:
                case UserControls.ControlTypes.IntBox:
                case UserControls.ControlTypes.MultiLineTextBox:
                default:

                    ShowTextBox(value);
                    break;


            }

            //this.ResumeLayout();
            //this.panel_Cntrl.ResumeLayout();
        }

        private void Dt_ValueChanged(object sender, EventArgs e)
        {

            KeyPressed?.Invoke(sender, e);
        }


        private void TextBox_Disposed(object sender, EventArgs e)
        {
            int iHandle = this.textBox.Handle.ToInt32();

            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }


        }

        public IEnumerable DataSource
        {
            set
            {
                if (_ControlTypes == UserControls.ControlTypes.ComboList
                    | _ControlTypes == UserControls.ControlTypes.ComboBox
                    | _ControlTypes == UserControls.ControlTypes.ComboBoxText
                    )
                {
                    if (value != null)
                    {
                        comboBox.ValueMember = ValueMember;
                        comboBox.DisplayMember = DisplayMember;

                        bindinglist.Clear();

                        foreach (object val in value)
                            bindinglist.Add(val);

                        bSource.DataSource = bindinglist;
                        comboBox.DataSource = bSource;

                    }
                }
                if (_ControlTypes == UserControls.ControlTypes.MultiSelectCombo
                    )
                {
                    if (value != null)
                    {

                        chkboxComboBox.DataSource = new ListSelectionWrapper<ListDataItem>(value, "Text", "Value");
                        chkboxComboBox.ValueMember = "Selected";
                        chkboxComboBox.DisplayMember = "NameConcatenated";
                        chkboxComboBox.DisplayMemberSingleItem = "Name";
                    }
                }

            }
            get
            { return null; }
        }


        string _ValueMember = "Value";
        public string ValueMember { get { return _ValueMember; } set { _ValueMember = value; } }

        string _DisplayMember = "Text";
        public string DisplayMember { get { return _DisplayMember; } set { _DisplayMember = value; } }

        bool _ReadOnly = false;
        public bool ReadOnly { get { return _ReadOnly; } set { _ReadOnly = value; } }

        int _ControlWidth = 200;
        public int ControlWidth
        {
            get { return _ControlWidth; }
            set
            {
                _ControlWidth = value;
                this.panel_Cntrl.Width = value;
                this.panel1.Width = this.Width - this.panel_Cntrl.Width - 20;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void SetEditTooltip(string Title, string Tooltip, string Icon = "Error")
        {
            //Add tooltips for InputBoxes
            editToolTip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), Icon);
            editToolTip.Title = Title;

            this.textBox.Tag = "a";
            editToolTip.SetToolTip(this.textBox, Tooltip);//control must either be a text box or editable combobox

        }

        public void SetValidationTooltip(string Title, string Tooltip, string Icon = "Error")
        {
            validationTooltip = this.Tag as BalloonToolTip;
            if (null != validationTooltip)
            {
                validationTooltip.RemoveAll();
            }

            validationTooltip = new BalloonToolTip();

            //Add tooltips for the buttons
            validationTooltip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), Icon);
            validationTooltip.ShowAlways = false;
            validationTooltip.Absolute = true;
            validationTooltip.Title = Title;
            // validationTooltip.SetToolTip(this.label1, Tooltip);
            validationTooltip.SetToolTip(this.textBox, Tooltip);
            validationTooltip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            this.Tag = validationTooltip;

            this.textBox.Tag = "a";
        }

        public BindingSource BindingSource { get { return bindingSource; } }

        public BindingList<object> BindingList { get { return bindinglist; } }

        public void ResetBindings(BaseEntity<int> model)
        {
            if (bindingSource != null)
            {
                //Reset object bindings
                bindingSource.DataSource = model;
                bindingSource.ResetBindings(false);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        public override string Text
        {
            get
            {
                return textBox.Text;
            }

            set
            {
                textBox.Text = value;
            }
        }

        public object Value
        {
            get
            {
                try
                {
                    switch (_ControlTypes)
                    {
                        case UserControls.ControlTypes.ComboList:
                        case UserControls.ControlTypes.ComboBox:
                        case UserControls.ControlTypes.ComboBoxText:

                            return this.comboBox.SelectedValue;
                        case UserControls.ControlTypes.DatePicker:

                            DateTimePicker dt = this.panel_Cntrl.Controls[0] as DateTimePicker;

                            return dt.Value;

                        case UserControls.ControlTypes.CheckBox:

                            CheckBox chk = this.panel_Cntrl.Controls[0] as CheckBox;

                            return chk.Checked;
                        case UserControls.ControlTypes.MultiSelectCombo:


                            return this.chkboxComboBox.SelectedValues;

                        case UserControls.ControlTypes.FileBox:
                        case UserControls.ControlTypes.FolderBox:
                            return textBox.Text;
                        case UserControls.ControlTypes.AmountBox:
                        case UserControls.ControlTypes.PercentBox:
                        case UserControls.ControlTypes.IntBox:
                        case UserControls.ControlTypes.MultiLineTextBox:
                        default:

                            return textBox.Text;


                    }
                }
                catch (Exception x)
                {

                }
                return null;
            }

            set
            {
                try
                {
                    switch (_ControlTypes)
                    {
                        case UserControls.ControlTypes.ComboList:
                        case UserControls.ControlTypes.ComboBox:
                        case UserControls.ControlTypes.ComboBoxText:

                            this.comboBox.SelectedValue = value;
                            break;
                        case UserControls.ControlTypes.DatePicker:

                            DateTimePicker dt = this.panel_Cntrl.Controls[0] as DateTimePicker;
                            try
                            {
                                if(dt != null)
                                    dt.Value = (DateTime)value;
                            }
                            catch (Exception x)
                            {
                            }
                            break;

                        case UserControls.ControlTypes.CheckBox:

                            CheckBox chk = this.panel_Cntrl.Controls[0] as CheckBox;

                            if (chk != null)
                                chk.Checked = (bool)value;
                            break;
                        case UserControls.ControlTypes.MultiSelectCombo:


                            // this.chkboxComboBox.SelectedValues=(string)value;
                            break;
                        case UserControls.ControlTypes.FileBox:
                        case UserControls.ControlTypes.FolderBox:
                            textBox.Text = (string)value;
                            break;
                        case UserControls.ControlTypes.AmountBox:
                        case UserControls.ControlTypes.PercentBox:
                        case UserControls.ControlTypes.IntBox:
                        case UserControls.ControlTypes.MultiLineTextBox:
                        default:

                            textBox.Text = (string)value;
                            break;


                    }
                }
                catch (Exception x)
                {

                }
            }
        }

        public Label Label
        {
            get { return this.label1; }
        }
    }
    public enum ControlTypes
    {
        AmountBox,
        TextBox,
        PercentBox,
        ComboList,
        ComboBox,
        DatePicker,
        Image,
        PasswordBox,
        PasswordBoxNoPreview,
        CheckBox,
        FileBox,
        FolderBox,
        IntBox,
        MultiLineTextBox,
        ComboBoxText,
        MultiSelectCombo
    }

    public enum LablePosition
    {
        Top,
        Left
    }
    /// <summary>
    /// Class used for demo purposes. A list of "Status". 
    /// This represents the custom "IList" datasource of anything listed in a CheckBoxComboBox.
    /// </summary>
    public class CheckBoxListItemList : List<CheckBoxListItem>
    {
    }
    /// <summary>
    /// Class used for demo purposes. This could be anything listed in a CheckBoxComboBox.
    /// </summary>
    public class CheckBoxListItem
    {
        public CheckBoxListItem(int id, string name, object value) { _Id = id; _Name = name; _Value = value; }

        private int _Id;
        private string _Name;
        private object _Value;

        public int Id { get { return _Id; } set { _Id = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public object Value { get { return _Value; } set { _Value = value; } }

        /// <summary>
        /// Now used to return the Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Name; }

       
    }
}
