using Finx.App;
using Finx.App.Extensions;
using MetroFramework.Controls;
using QSS.Components.Windows.Forms;
using SourceGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SourceGrid.Cells.Editors;
using DevAge.Windows.Forms;
using PresentationControls;
using System.Runtime.InteropServices;
using Finx.App.UserControls;
using my.domain.lib.core.Validation;
using Finx.App.Forms;
using DevAge.ComponentModel;
using easiplan.domain;
using MetroFramework.Forms;
using ComponentFactory.Krypton.Toolkit;
using easiplan.app.ContextMenus;

namespace MetroFramework.Controls.Ext
{
    #region Old Code
    //public static class MetroControlExt
    //   {
    //       #region MetroPanel Initialise
    //       public static MetroPanel Initialise<T>(this MetroPanel panel, T bindingSource, Action<ControlBuilder<T>> controlBuilder, ControlsLayout controlsLayout = ControlsLayout.Horizontal,int top=5,int left=5,int labelWidth= 120,PropertyChangedEventHandler PropertyChangedHandler = null,DataSourceUpdateMode dataSourceUpdateMode= DataSourceUpdateMode.OnValidation) where T : BaseEntity<int>
    //       {
    //           var builder = new ControlBuilder<T>();
    //           controlBuilder(builder);

    //           panel.SuspendLayout();
    //           panel.AutoScroll = true;

    //           foreach (var control in builder)
    //           {

    //               var _cntrl = panel.Controls.Find("editor_" + control.Editor.GetType().Name + "_" + control.Name, true);

    //               if (_cntrl.Count() > 0)
    //               {
    //                   try
    //                   {
    //                       #region Update Control
    //                       var cntrl = _cntrl[0];
    //                       if (cntrl.GetType() == typeof(DevAgeTextBox))
    //                       {
    //                           DevAgeTextBox txt = cntrl as DevAgeTextBox;
    //                           txt.ReadOnly = !control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(DevAgeComboBox))
    //                       {
    //                           DevAgeComboBox txt = cntrl as DevAgeComboBox;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(ExComboBox))
    //                       {
    //                           ExComboBox txt = cntrl as ExComboBox;
    //                           txt.ReadOnly = !control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(DevAgeNumericUpDown))
    //                       {
    //                           DevAgeNumericUpDown txt = cntrl as DevAgeNumericUpDown;
    //                          // txt.ReadOnly = !control.Editor.Enabled;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(MetroTrackBar))
    //                       {
    //                           MetroTrackBar txt = cntrl as MetroTrackBar;
    //                           // txt.ReadOnly = !control.Editor.Enabled;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(MetroComboBox))
    //                       {
    //                           MetroComboBox txt = cntrl as MetroComboBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(MetroCheckBox))
    //                       {
    //                           MetroCheckBox txt = cntrl as MetroCheckBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(MetroTextBox))
    //                       {
    //                           MetroTextBox txt = cntrl as MetroTextBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
    //                       {
    //                           System.Windows.Forms.DateTimePicker txt = cntrl as System.Windows.Forms.DateTimePicker;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }

    //                       #endregion

    //                       //# region Rebind the dataSource
    //                       //BindingSource gridDataBinder = new BindingSource();
    //                       //gridDataBinder.DataSource = bindingSource;
    //                       //if (PropertyChangedHandler != null)
    //                       //   // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
    //                       //   bindingSource.PropertyChanged += PropertyChangedHandler;

    //                       //cntrl.DataBindings.Clear();

    //                       //string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
    //                       //foreach (string _binding in _bindings.ToList())
    //                       //    cntrl.DataBindings.Add(_binding, gridDataBinder, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string);//OnPropertyChanged

    //                       ////YJ 2021-11-05 Update Databinding Update mode to the control editors datasourceupdatemode
    //                       //#endregion
    //                   }
    //                   catch (Exception x)
    //                   {
    //                       Program.Logger.Error(x);
    //                   }

    //               }
    //               else
    //               {
    //                   #region Add Label
    //                   int lableHeight = 0;
    //                   if (labelWidth > 0)
    //                   {
    //                       MetroLabel label = new MetroLabel() { Text = control.DisplayName };
    //                       label.Left = left;
    //                       label.Top = top;
    //                       label.Width = labelWidth;
    //                       label.BackColor = Color.Transparent;
    //                       label.UseCustomBackColor = true;
    //                       label.Font = Global.LableFont;
    //                       label.FontSize = MetroLabelSize.Medium;
    //                       //  label.FontWeight = MetroLabelWeight.Bold;

    //                       lableHeight += label.Height + 5;

    //                       label.Name = "lbl_" + control.Name;
    //                       panel.Controls.Add(label);
    //                   }
    //                   #endregion

    //                   #region Add Control
    //                   Control cntrl = null;

    //                   if (control.Editor == null)
    //                       control.Editor = new MetroStringEditor() { Enabled = false }; //No editing

    //                   //Get Required Attribute
    //                   var _reqAttr = typeof(T).GetProperty(control.Name).CustomAttributes.Where(x => x.AttributeType == typeof(RequiredAttribute));
    //                   if (_reqAttr != null)
    //                   {
    //                       if (_reqAttr.Count() > 0)
    //                       {
    //                           var _msg = _reqAttr.FirstOrDefault().NamedArguments[0].TypedValue.Value as string;
    //                           control.Editor.Watermark = _msg;
    //                       }
    //                   }

    //                   //Create control
    //                   cntrl = control.Editor.CreateControl();

    //                   if (controlsLayout == ControlsLayout.Vertical && control.Editor.GetType()!= typeof(MetroText))
    //                   {
    //                       cntrl.Left = left;//padding
    //                       cntrl.Top = top + lableHeight;
    //                   }
    //                   else
    //                   {
    //                       cntrl.Left = left + labelWidth + 5;//padding
    //                       cntrl.Top = top;
    //                   }

    //                   cntrl.Name = "editor_" + control.Editor.GetType().Name + "_" + control.Name;

    //                   if (cntrl.GetType() == typeof(MetroTextBox) && cntrl.Name == "editor_MetroPasswordEditor_Password")
    //                   {
    //                       var pwdTextBoxControl = (MetroTextBox)cntrl;
    //                       pwdTextBoxControl.TabIndex = 0;
    //                       pwdTextBoxControl.Focus();
    //                       panel.Controls.Add(pwdTextBoxControl);
    //                   }
    //                   else
    //                       panel.Controls.Add(cntrl);

    //                   #endregion

    //                   #region Bind Control

    //                   BindingSource dataSource = new BindingSource();
    //                   dataSource.DataSource = bindingSource;
    //                   if (PropertyChangedHandler != null)
    //                   {
    //                      // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
    //                       bindingSource.PropertyChanged += PropertyChangedHandler;
    //                   }

    //                   cntrl.DataBindings.Clear();

    //                   string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
    //                   foreach(string binding in _bindings.ToList())
    //                       //cntrl.DataBindings.Add(binding, binding, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string);//OnPropertyChanged
    //                       cntrl.DataBindings.Add(new Binding(binding, dataSource, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string));

    //                   //YJ 2021-11-05 Update Databinding Update mode to the control editors datasourceupdatemode

    //                   if (cntrl.GetType() == typeof(MetroCheckBox))
    //                   {
    //                       cntrl.Text = "";// control.DisplayName;
    //                       cntrl.Left = left;
    //                   }
    //                   #endregion

    //                   top = cntrl.Top + control.Editor.Size.Height + 2; //padding
    //               }

    //           }

    //           panel.ResumeLayout();

    //           return panel;
    //       }
    //       #endregion

    //       #region MetroGrid Initialise
    //       public static MetroGrid Initialise<T>(this MetroGrid grid, IList<T> bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5,bool HasDetails=false,DataGridViewCellEventHandler dgvCellEventHandler=null, DataGridViewCellMouseEventHandler dgvRowHeaderEventHandler = null, EventHandler PropertyChangedHandler = null, MetroContextMenu ContextMenu = null) where T : class
    //       {
    //           var builder = new ControlBuilder<T>();
    //           controlBuilder(builder);

    //           grid.SuspendLayout();

    //           grid.DataError += Grid_DataError;
    //           grid.EditingControlShowing += Grid_EditingControlShowing;
    //           grid.CellValidating += Grid_CellValidating;
    //           grid.CellParsing += Grid_CellParsing;
    //           grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;

    //           grid.AutoGenerateColumns = false;
    //           grid.Columns.Clear();

    //           if (HasDetails)
    //           {
    //               //build a Toggle column
    //               DataGridViewImageColumn dgvShowHideCol = new DataGridViewImageColumn()
    //               {
    //                   Name = "",
    //                   Width = 25,
    //                   Resizable = DataGridViewTriState.False,
    //                   Frozen=true,                    
    //                   Image = global::easiplan.app.Properties.Resources.expand,
    //                   Tag = "Details"
    //               };
    //               grid.Columns.Add(dgvShowHideCol);
    //           }

    //           if (dgvCellEventHandler != null)
    //               grid.CellContentClick += dgvCellEventHandler;

    //           if(dgvRowHeaderEventHandler!=null)
    //               grid.RowHeaderMouseClick += dgvRowHeaderEventHandler;

    //           foreach (var control in builder)
    //           {
    //               if (control.Editor == null)
    //               {
    //                   DataGridViewColumn dgvCol = new DataGridViewTextBoxColumn()
    //                   {
    //                       Name = control.Name,
    //                       DataPropertyName = control.Name,
    //                       HeaderText = control.DisplayName,
    //                       AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
    //                   };

    //                   grid.Columns.Add(dgvCol);
    //               }
    //               else
    //               {
    //                   //Add column to the grid
    //                   grid.Columns.Add(control.Editor.CreateColumn(control.Name, control.DisplayName));
    //               }


    //           }


    //           if (ContextMenu!=null)
    //           {
    //               //build a ContextMenu column
    //               DataGridViewImageColumn dgvContextMenu = new DataGridViewImageColumn()
    //               {
    //                   Name = "",
    //                   Width = 25,
    //                   Resizable = DataGridViewTriState.False,
    //                   Image = global::easiplan.app.Properties.Resources.save,
    //                   Description = "Menu",
    //                   Tag= "ContextMenu"

    //               };
    //               grid.Columns.Add(dgvContextMenu);
    //           }


    //           grid.ResumeLayout();

    //           BindingSource gridDataBinder = new BindingSource();
    //           gridDataBinder.DataSource = bindingSource;
    //           gridDataBinder.AllowNew = true;
    //           grid.DataSource = gridDataBinder;

    //           if (PropertyChangedHandler != null)
    //               gridDataBinder.CurrentItemChanged += PropertyChangedHandler;

    //           //var data = new BindingList<T>(bindingSource);
    //           //grid.DataSource = data;


    //           return grid;
    //       }
    //       public static MetroGrid Initialise<T>(this MetroGrid grid, T bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool HasDetails = false, DataGridViewCellEventHandler dgvCellEventHandler = null, EventHandler PropertyChangedHandler = null, MetroContextMenu ContextMenu = null) where T : class
    //       {
    //           var builder = new ControlBuilder<T>();
    //           controlBuilder(builder);

    //           grid.SuspendLayout();

    //           grid.DataError += Grid_DataError;
    //           grid.EditingControlShowing += Grid_EditingControlShowing;
    //           grid.CellValidating += Grid_CellValidating;
    //           grid.CellParsing += Grid_CellParsing;

    //           grid.AutoGenerateColumns = false;

    //           grid.Columns.Clear();

    //           if (HasDetails)
    //           {
    //               //build a Toggle column
    //               DataGridViewImageColumn dgvShowHideCol = new DataGridViewImageColumn()
    //               {
    //                   Name = "",
    //                   Width = 25,
    //                   Resizable = DataGridViewTriState.False,
    //                   Frozen = true,
    //                   Image = global::easiplan.app.Properties.Resources.expand,
    //                   Tag = "Details"
    //               };
    //               grid.Columns.Add(dgvShowHideCol);
    //           }


    //           if (dgvCellEventHandler != null)
    //               grid.CellContentClick += dgvCellEventHandler;

    //           foreach (var control in builder)
    //           {
    //               if (control.Editor == null)
    //               {
    //                   DataGridViewColumn dgvCol = new DataGridViewTextBoxColumn()
    //                   {
    //                       Name = control.Name,
    //                       DataPropertyName = control.Name,
    //                       HeaderText = control.DisplayName,
    //                       //AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
    //                   };

    //                   grid.Columns.Add(dgvCol);
    //               }
    //               else
    //               {
    //                   //Add column to the grid
    //                   grid.Columns.Add(control.Editor.CreateColumn(control.Name, control.DisplayName));
    //               }
    //           }


    //           if (ContextMenu != null)
    //           {
    //               //build a ContextMenu column
    //               DataGridViewImageColumn dgvContextMenu = new DataGridViewImageColumn()
    //               {
    //                   Name = "",
    //                   Width = 25,
    //                   Resizable = DataGridViewTriState.False,
    //                   Image = global::easiplan.app.Properties.Resources.save,
    //                   Description = "Menu",
    //                   Tag = "ContextMenu"

    //               };
    //               grid.Columns.Add(dgvContextMenu);
    //           }

    //           grid.ResumeLayout();

    //           BindingSource gridDataBinder = new BindingSource();
    //           gridDataBinder.DataSource = bindingSource;
    //           gridDataBinder.AllowNew = true;
    //           grid.DataSource = gridDataBinder;

    //           if (PropertyChangedHandler != null)
    //               gridDataBinder.CurrentItemChanged += PropertyChangedHandler;



    //           //IList<T> _list = new List<T>();
    //           //_list.Add(bindingSource);

    //           //var data = new BindingList<T>(_list);
    //           //grid.DataSource = data;


    //           return grid;
    //       }
    //       public static MetroGrid ResetBindingsExt(this MetroGrid grid)
    //       {
    //           BindingSource gridDataBinder = grid.DataSource as BindingSource;
    //           gridDataBinder.ResetBindings(false);

    //           return grid;
    //       }
    //       public static bool ToggleGridShowHide(this MetroGrid grid,int colIndex,int rowIndex,bool ForceExpand=false)
    //       {
    //           try
    //           {
    //               DataGridViewImageColumn col = grid.Columns[colIndex] as DataGridViewImageColumn;

    //               if (col != null)
    //               {
    //                   if ("toggle" == grid.Rows[rowIndex].Cells[colIndex].Tag as string || ForceExpand)
    //                   {
    //                       grid.Rows[rowIndex].Cells[colIndex].Value = global::easiplan.app.Properties.Resources.expand;
    //                       grid.Rows[rowIndex].Cells[colIndex].Tag = "expand";
    //                       return false;
    //                   }
    //                   else
    //                   {
    //                       grid.Rows[rowIndex].Cells[colIndex].Value = global::easiplan.app.Properties.Resources.toggle;
    //                       grid.Rows[rowIndex].Cells[colIndex].Tag = "toggle";
    //                       return true;
    //                   }
    //               }
    //           }
    //           catch { }
    //           return false;
    //       }
    //       #endregion

    //       #region MetroGrid EventHandlers
    //       private static void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
    //       {
    //           MetroGrid grid = sender as MetroGrid;

    //           switch (e.Context)
    //           {
    //               case DataGridViewDataErrorContexts.Parsing:
    //               case DataGridViewDataErrorContexts.Commit:
    //               case DataGridViewDataErrorContexts.CurrentCellChange:
    //                   grid.CurrentCell.ErrorText = "parsing error";
    //                   grid.CurrentCell.Style.ForeColor = Color.Red;
    //                   break;
    //           }

    //           if (e.Exception.GetType() == typeof(ConstraintException))
    //           {
    //               grid.CurrentCell.ErrorText = "constraint error";
    //               e.ThrowException = false;
    //           }
    //           if (e.Exception.GetType() == typeof(FormatException))
    //           {
    //               grid.CurrentCell.ErrorText = "format error";
    //               e.ThrowException = false;
    //           }
    //           //if (e.Exception.GetType() == typeof(ArgumentException))
    //           //{
    //           //    grid.CurrentCell.ErrorText = "data error";
    //           //    e.ThrowException = false;
    //           //}
    //           // throw new NotImplementedException();
    //       }

    //       private static void Grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
    //       {
    //          // throw new NotImplementedException();
    //       }

    //       private static void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    //       {
    //           try
    //           {
    //               MetroGrid grid = sender as MetroGrid;
    //               grid.CurrentCell.ErrorText = "";

    //               DataGridViewComboBoxCell cCell = grid.CurrentCell as DataGridViewComboBoxCell;
    //               if (cCell != null)
    //               {
    //                   BindingList<ListDataItem> bList = cCell.DataSource as BindingList<ListDataItem>;
    //                   var selItem = bList.Where(x => x.Text.Trim().ToLower() == e.FormattedValue.ToString().Trim().ToLower()).FirstOrDefault();
    //                   if (selItem == null)
    //                   {
    //                    //   bList.RaiseListChangedEvents = true;
    //                       bList.Add(new Finx.App.ListDataItem() { ListType = ListDataItemType.DependentType, Text = e.FormattedValue as string, Value = e.FormattedValue as string });

    //                       cCell.Value = e.FormattedValue;

    //                   }
    //               }

    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       private static void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    //       {
    //           MetroGrid grid = sender as MetroGrid;
    //           if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
    //           {
    //               DataGridViewComboBoxEditingControl eC = e.Control as DataGridViewComboBoxEditingControl;

    //               System.Windows.Forms.ComboBox combo = e.Control as System.Windows.Forms.ComboBox;
    //               if (combo != null)
    //               {
    //                   combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
    //                   combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);

    //                   combo.DropDownStyle = ComboBoxStyle.DropDown;
    //               }
    //           }
    //       }

    //       private static void Grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    //       {
    //          // throw new NotImplementedException();
    //       }

    //       private static void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    //       {
    //           System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
    //           var item = cb.SelectedValue;
    //           if (item != null)
    //           {


    //           }
    //       }

    //       //public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, IList<T> dataSource, Action<ControlBuilder<T>> controlBuilder, ListChangedEventHandler handler = null, int top = 5, int left = 5) where T : class
    //       //{

    //       //    grid.SuspendLayout();

    //       //    grid.DataSource = new DevAge.ComponentModel.BoundList<T>(dataSource);

    //       //    if (handler != null)
    //       //        grid.DataSource.ListChanged += handler;

    //       //    var builder = new ControlBuilder<T>();
    //       //    controlBuilder(builder);

    //       //    grid.Columns.Clear();

    //       //    foreach (var column in builder)
    //       //    {

    //       //        //Control cntrlEditor = null;
    //       //        //if (control.Editor == null)
    //       //        //    control.Editor = new MetroStringEditor() { ReadOnly = true }; //No editing



    //       //        //Add column to the grid
    //       //        //DataGridViewColumn dgvCol = new DataGridViewColumn()
    //       //        //{
    //       //        //    Name = control.Name,
    //       //        //    //DataPropertyName = "Value",
    //       //        //    HeaderText = control.DisplayName,

    //       //        //};
    //       //        //grid.Columns.Add(dgvCol);

    //       //        grid.Columns.Add(column.Name, column.DisplayName, new StringEditor());
    //       //    }



    //       //    grid.ResumeLayout();

    //       //    return grid;
    //       //}

    //       #endregion

    //       #region MetroControls Format
    //       public static MetroForm Format(this MetroForm control, string SubTitle = "")
    //       {
    //           try
    //           {


    //               #region Form Format
    //               control.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
    //               control.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;

    //               control.Text = string.Empty;
    //               control.SubTitle = string.Format("{0}", SubTitle);


    //               control.DisplayHeader = false;
    //               #endregion

    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }

    //           return control;

    //       }
    //       public static MetroTextBox Format(this MetroTextBox control)
    //       {
    //           try
    //           {
    //               control.Font = new Font(FontFamily.GenericSansSerif, 12);

    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }

    //           return control;

    //       }
    //       public static MetroTabControl Format(this MetroTabControl control, ImageList images = null)
    //       {

    //           if (images != null)
    //               control.ImageList = images;



    //           return control;
    //       }
    //       public static TabPage Format(this TabPage control, string text = "", Image image = null, int padding = 2, EventHandler ClickEventHandler = null)
    //       {
    //           control.Text = text;
    //           control.Padding = new Padding(padding);
    //           control.ImageIndex = 1;

    //           control.BackColor = Color.WhiteSmoke;


    //           if (ClickEventHandler != null)
    //           {
    //               control.Click += ClickEventHandler;

    //               control.GotFocus += ClickEventHandler;
    //           }

    //           return control;
    //       }
    //       public static MetroPanel Format(this MetroPanel control, string text = "", Image image = null, int padding = 5, int height = 0)
    //       {
    //           control.SuspendLayout();

    //           control.Text = text;
    //           control.Padding = new Padding(padding);

    //           control.BackColor = Color.White;
    //           control.UseCustomBackColor = true;

    //           // control.BorderStyle = BorderStyle.FixedSingle;

    //           if (height > 0)
    //           {
    //               control.Height = height;
    //               control.VerticalScrollbar = true;
    //               control.AutoScroll = true;
    //               control.VerticalScrollbarBarColor = true;
    //           }
    //           control.ResumeLayout();

    //           return control;
    //       }
    //       public static MetroGrid Format(this MetroGrid control, bool ReadOnly, GridFormats format = GridFormats.Default)
    //       {
    //           try
    //           {
    //               switch (format)
    //               {
    //                   case GridFormats.Default:
    //                       control.BorderStyle = BorderStyle.FixedSingle;

    //                       control.UseCustomBackColor = true;
    //                       control.UseCustomForeColor = true;

    //                       control.ScrollBars = ScrollBars.Both;

    //                       control.AllowUserToAddRows = !ReadOnly;
    //                       control.AllowUserToDeleteRows = false;

    //                       //control.Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Pixel);
    //                       control.Font = MetroFonts.Default(13.5f);

    //                       control.BackgroundColor = Color.White;
    //                       control.ForeColor = Color.Black;

    //                       //control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       //control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.DefaultBold(13f);
    //                       control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


    //                       //control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       //control.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       //control.RowHeadersDefaultCellStyle.Font = MetroFonts.Default(12f);

    //                       control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
    //                       control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

    //                       control.CellBorderStyle = DataGridViewCellBorderStyle.Single;
    //                       control.BorderStyle = BorderStyle.FixedSingle;
    //                       control.GridColor = Color.WhiteSmoke;

    //                       //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    //                       foreach (DataGridViewColumn col in control.Columns)
    //                       {
    //                           if (!col.Frozen)
    //                               col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    //                       }

    //                       control.Refresh();

    //                       break;
    //                   case GridFormats.Format1:
    //                       control.Font = MetroFonts.Default(13f);

    //                       control.AllowUserToAddRows = !ReadOnly;
    //                       control.AllowUserToDeleteRows = false;

    //                       control.BackgroundColor = Color.White;
    //                       control.ForeColor = Color.Black;

    //                       control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
    //                       control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

    //                       control.RowsDefaultCellStyle.SelectionBackColor = Color.White;
    //                       control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

    //                       //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
    //                       //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                       foreach (DataGridViewColumn col in control.Columns)
    //                       {
    //                           if (!col.Frozen)
    //                               col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    //                       }

    //                       control.RowHeadersVisible = false;
    //                       control.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
    //                       control.ScrollBars = ScrollBars.None;

    //                       control.Refresh();
    //                       break;
    //                   case GridFormats.Format11:
    //                       control.Font = MetroFonts.Default(13f);

    //                       control.AllowUserToAddRows = !ReadOnly;
    //                       control.AllowUserToDeleteRows = false;

    //                       control.BackgroundColor = Color.White;
    //                       control.ForeColor = Color.Black;

    //                       control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
    //                       control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

    //                       control.RowsDefaultCellStyle.SelectionBackColor = Color.White;
    //                       control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

    //                       //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
    //                       //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                       foreach (DataGridViewColumn col in control.Columns)
    //                       {
    //                           if (!col.Frozen)
    //                               col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    //                       }

    //                       control.RowHeadersVisible = true;
    //                       control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    //                       control.ScrollBars = ScrollBars.Both;

    //                       control.Refresh();
    //                       break;
    //                   case GridFormats.Format2:
    //                       control.Font = MetroFonts.Default(14f);

    //                       control.AllowUserToAddRows = !ReadOnly;
    //                       control.AllowUserToDeleteRows = false;

    //                       control.BackgroundColor = Color.White;
    //                       control.ForeColor = Color.Black;

    //                       control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
    //                       control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

    //                       control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.RowHeadersDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;

    //                       control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
    //                       control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

    //                       //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
    //                       //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                       foreach (DataGridViewColumn col in control.Columns)
    //                       {
    //                           if (!col.Frozen)
    //                               col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    //                       }

    //                       control.RowHeadersVisible = true;
    //                       control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

    //                       control.Refresh();
    //                       break;
    //                   case GridFormats.Format3:
    //                       control.Font = MetroFonts.Default(14f);

    //                       control.AllowUserToAddRows = !ReadOnly;
    //                       control.AllowUserToDeleteRows = false;

    //                       control.BackgroundColor = Color.White;
    //                       control.ForeColor = Color.Black;

    //                       control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
    //                       control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
    //                       control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

    //                       control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //                       control.RowHeadersDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;

    //                       control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
    //                       control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

    //                       control.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
    //                       control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                       foreach (DataGridViewColumn col in control.Columns)
    //                       {
    //                           if (!col.Frozen)
    //                               col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    //                       }

    //                       control.RowHeadersVisible = true;
    //                       control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

    //                       control.Refresh();
    //                       break;
    //                   default:
    //                       break;
    //               };




    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }

    //           return control;

    //       }
    //       public static TabPage Format(this TabPage control,  int PageIndex, string text = "", Image image = null, int padding = 2, EventHandler ClickEventHandler = null)
    //       {
    //           control = control.Format(text, image, padding, ClickEventHandler);

    //           control.Tag = PageIndex;

    //           return control;
    //       }
    //       #endregion

    //       #region SourceGrid.DataGrid Initialise

    //       public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, IList<T> bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null, EventHandler ItemEditEventHandler = null) where T : class
    //       {

    //           grid.SuspendLayout();

    //           grid.Columns.Clear();

    //           grid.SelectionMode = GridSelectionMode.Row;
    //           //grid.DataError += Grid_DataError;
    //           //grid.EditingControlShowing += Grid_EditingControlShowing;
    //           //grid.CellValidating += Grid_CellValidating;
    //           //grid.CellParsing += Grid_CellParsing;
    //           //grid.Validated += Grid_CurrentCellDirtyStateChanged;           


    //           grid.DeleteRowsWithDeleteKey = false;
    //           grid.CancelEditingWithEscapeKey = true;
    //           //grid.EndEditingRowOnValidate = true;

    //           grid.AutoSize = false;
    //           grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    //           grid.AutoStretchColumnsToFitWidth = true;

    //           grid.FixedRows = 1;
    //           grid.FixedColumns = 1;

    //           grid.EnableSort = false;

    //           #region Row Header
    //           //Event Controller
    //           SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           if (RowHeaderSelectEventHandler != null)
    //           {
    //               RowHeaderSelectEvent.Click -= RowHeaderSelectEventHandler;
    //               RowHeaderSelectEvent.Click += RowHeaderSelectEventHandler;
    //           }
    //           else
    //           {
    //               RowHeaderSelectEvent.Click -= RowHeaderSelectEvent_Click;
    //               RowHeaderSelectEvent.Click += RowHeaderSelectEvent_Click;
    //           }


    //           var rowHeader = new SourceGrid.Cells.RowHeader("");
    //           if (RowHeaderSelectEventHandler != null)
    //           {
    //               rowHeader.Image = global::easiplan.app.Properties.Resources.arrow_r_128.ToBitmap();
    //               rowHeader.ToolTipText = "Click to show";
    //           }
    //           rowHeader.AddController(RowHeaderSelectEvent);

    //           var colHeader = grid.Columns.Add("", "", rowHeader);
    //           colHeader.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           colHeader.MaximalWidth = 25;
    //           colHeader.Width = 25;

    //           //Row Header
    //           // var dgCol = DataGridColumn.CreateRowHeader(grid);
    //           // dgCol.DataCell.AddController(RowHeaderSelectEvent);

    //           // grid.Columns.Insert(0,dgCol);
    //           //grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           //grid.Columns[0].MaximalWidth = 25;
    //           //grid.Columns[0].Width = 25;
    //           //var RowHeaderEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string))
    //           //{ EditableMode = SourceGrid.EditableMode.None, EnableEdit = false };
    //           //RowHeaderEditor.Control.Cursor = Cursors.Hand;
    //           //grid.Columns[0].DataCell.Editor = RowHeaderEditor;

    //           //Delete Controller
    //           //grid.Controller.AddController(new DataGridCellController());
    //           //grid.Controller.AddController(new KeyDeleteController());

    //           #endregion

    //           #region Row Click
    //           grid.Selection.FocusRowEntered += RowSelectEventHandler;
    //           grid.Selection.FocusStyle = FocusStyle.FocusFirstCellOnEnter;
    //           grid.MouseClick += SourceGrid_MouseClick;
    //           #endregion

    //           #region Build Grid Columns
    //           var builder = new ControlBuilder<T>();
    //           controlBuilder(builder);

    //           foreach (var control in builder)
    //           {
    //               DataGridColumn dataGridColumn = null;
    //               if (control.Editor == null)
    //               {
    //                   dataGridColumn= grid.Columns.Add(control.Name, control.DisplayName, typeof(String)); //default editor
    //               }
    //               else
    //               {
    //                   dataGridColumn = control.Editor.CreateDataGridColumn(control.Name, control.DisplayName,grid);

    //                   if (ReadOnly)
    //                       control.Editor.DataGridEditor.EditableMode = EditableMode.None;

    //               }
    //               // Add a custom view for the cell e.g
    //               // dataGridColumn.DataCell.View = new InstructionsStatusView();

    //               if(control.Editor._width!=200)
    //                   dataGridColumn.MinimalWidth = control.Editor._width;

    //               if (!string.IsNullOrEmpty(control.ToolTip))
    //               {

    //                   DataGridColumnTooltip dgct = new DataGridColumnTooltip(control.ToolTip);
    //                   dataGridColumn.HeaderCell.AddController(dgct);

    //                   dataGridColumn.HeaderCell.View = new ColumnInfoView();
    //               }


    //           }
    //           #endregion

    //           #region ContextMenuButton
    //           if (ContextMenu != null)
    //           {

    //               DataGridContextMenu dgct = ContextMenu as DataGridContextMenu;

    //               if (dgct.Visible)
    //               {
    //                   var btn = new SourceGrid.Cells.RowHeader("");
    //                   btn.Image = global::easiplan.app.Properties.Resources.dots_3_1281;
    //                   btn.ToolTipText = "Click to show";

    //                   btn.AddController(ContextMenu);

    //                   var col = grid.Columns.Add("", "", btn);
    //                   col.Width = 22;
    //                   col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //                   col.MaximalWidth = 22;
    //                   col.MinimalWidth = 22;
    //               }

    //               //override the default contextmenu property
    //               // grid.ContextMenu = dgct.ContextMenu;
    //               grid.Tag = dgct.ContextMenu;


    //           }
    //           #endregion

    //           #region  Edit Button

    //           if (!ReadOnly && AllowEdit)
    //           {
    //               //Add a delete button
    //               SourceGrid.Cells.Controllers.CustomEvents editEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //               if (ItemEditEventHandler != null)
    //               {
    //                   editEvent.Click -= ItemEditEventHandler;
    //                   editEvent.Click += ItemEditEventHandler;
    //               }


    //               var btnEdit = new SourceGrid.Cells.RowHeader("");
    //               btnEdit.Image = global::easiplan.app.Properties.Resources.dots_3_128.ToBitmap();
    //               btnEdit.ToolTipText = "Click to delete row";
    //               btnEdit.AddController(editEvent);

    //               var col = grid.Columns.Add("", "", btnEdit);
    //               col.Width = 22;
    //               col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //               col.MaximalWidth = 22;
    //               col.MinimalWidth = 22;
    //           }
    //           #endregion

    //           #region  Delete Button
    //           grid.DeleteRowsWithDeleteKey = AllowDelete;// !ReadOnly;
    //           if (!ReadOnly && AllowDelete)
    //           {
    //               //Add a delete button
    //               SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();

    //               if (ItemDeleteEventHandler == null)
    //               {
    //                   deleteEvent.Click -= DeleteEvent_Click;
    //                   deleteEvent.Click += DeleteEvent_Click;
    //               }
    //               else
    //               {
    //                   deleteEvent.Click -= ItemDeleteEventHandler;
    //                   deleteEvent.Click += ItemDeleteEventHandler;
    //               }


    //               var btn = new SourceGrid.Cells.RowHeader("");
    //               btn.Image = global::easiplan.app.Properties.Resources.delete;
    //               btn.ToolTipText = "Click to delete row";
    //               btn.AddController(deleteEvent);

    //               var col = grid.Columns.Add("", "", btn);
    //               col.Width = 22;
    //               col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //               col.MaximalWidth = 22;
    //               col.MinimalWidth = 22;
    //           }
    //           #endregion

    //           //Reset the datasource
    //           grid.DataBindings.Clear();
    //           grid.DataSource = null;

    //           DevAge.ComponentModel.BoundList<T> boundList = new DevAge.ComponentModel.BoundList<T>(bindingSource);

    //           boundList.ItemDeleted -= ItemDeletedEventHandler;
    //           boundList.ItemDeleted += ItemDeletedEventHandler;

    //           if (PropertyChangedHandler != null) boundList.ListChanged -= PropertyChangedHandler;
    //           if (PropertyChangedHandler != null) boundList.ListChanged += PropertyChangedHandler;

    //           //if (ListChangedEventHandler != null) boundList.ListChanged -= ListChangedEventHandler;// PropertyChangedHandler;
    //           //if (ListChangedEventHandler != null) boundList.ListChanged += ListChangedEventHandler;// PropertyChangedHandler;


    //           grid.DataSource = boundList;

    //           if (grid.DataSource != null)
    //               grid.DataSource.AllowNew = AllowAddNew;

    //           grid.ResumeLayout();

    //           grid.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;           


    //           grid.Refresh();

    //           return grid;
    //       }


    //       public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, T bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null) where T : class
    //       {
    //           IList<T> _bindingSource = new List<T>();
    //           _bindingSource.Add(bindingSource);

    //           return grid.Initialise<T>(_bindingSource, controlBuilder, top, left, AllowDelete, AllowAddNew, AllowEdit, ReadOnly, PropertyChangedHandler, ItemDeleteEventHandler, ItemDeletedEventHandler, RowHeaderSelectEventHandler, RowSelectEventHandler, ListChangedEventHandler, ContextMenu);
    //       }
    //       public static void InitialiseRectangle(this SourceGrid.DataGrid grid, CellContext context)
    //       {

    //           context.Grid.Selection.SelectRow(context.Position.Row, true);

    //           var _cntrl = context.Grid.Controls.Find(grid.Name, true);
    //           if (_cntrl.Count() > 0)
    //           {
    //               context.Grid.Controls.RemoveByKey(grid.Name);
    //               throw new Exception();
    //           }

    //           grid.Tag = null;

    //           if (context.Position.Row < 1)
    //               throw new Exception();
    //       }
    //       public static void DisplayRectangle(this SourceGrid.DataGrid grid, CellContext context)
    //       {
    //           int rectHeight = 150;

    //           context.Grid.Controls.Add(grid);

    //           var xPos = context.Grid.Columns.GetWidth(context.Position.Column);
    //           var yPos = 0;

    //           for (int i = 0; i <= context.Position.Row; i++)
    //               yPos += context.Grid.Rows.GetHeight(i);

    //           if (yPos + rectHeight > (context.Grid.Bounds.Height+ context.Grid.Rows.GetHeight(context.Position.Row) + 10))
    //               yPos =  yPos-(rectHeight + context.Grid.Rows.GetHeight(context.Position.Row) + 10);

    //           Rectangle dgvRectangle = new Rectangle(xPos + 5, yPos + 5, 0, 0);

    //           grid.Size = new Size(context.Grid.Width - (dgvRectangle.X + 5), rectHeight);
    //           grid.Location = new Point(dgvRectangle.X, dgvRectangle.Y);

    //           grid.Refresh();
    //       }
    //       #endregion

    //       #region SourceGrid.DataGrid EventHandlers
    //       private static void ContextMenu_Popup(object sender, EventArgs e)
    //       {

    //           //throw new NotImplementedException();
    //       }

    //       private static void SourceGrid_MouseClick(object sender, MouseEventArgs e)
    //       {

    //           SourceGrid.DataGrid dg = sender as SourceGrid.DataGrid;

    //           if(e.Button == MouseButtons.Right && dg.Visible)
    //           {
    //               if (dg.MouseDownPosition.Row <= 0)
    //                   return;

    //               dg.Selection.FocusRow(dg.MouseDownPosition.Row);

    //               ContextMenu ctxMenu = dg.Tag as ContextMenu;

    //               if(ctxMenu != null)
    //                   ctxMenu.Show(dg, new Point(e.X, e.Y));

    //           }
    //       }

    //       private static void Grid_Paint(object sender, PaintEventArgs e)
    //       {

    //           SourceGrid.DataGrid grid = sender as SourceGrid.DataGrid;

    //           Rectangle rect = grid.DisplayRectangle;
    //           //using (Brush b = new SolidBrush(Color.Red))
    //           //{
    //           //    e.Graphics.FillRectangle(b, e.CellBounds);
    //           //}
    //           using (Pen pen = new Pen(Brushes.DarkGreen))
    //           {
    //               pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
    //               e.Graphics.DrawRectangle(pen, rect);
    //           }

    //           // throw new NotImplementedException();
    //       }

    //       public static void RowHeaderSelectEvent_Click(object sender, EventArgs e)
    //       {
    //           try
    //           {
    //               CellContext context = (CellContext)sender;
    //              // context.Grid.Selection.SelectRow(context.Position.Row, true);

    //               context.Grid.Selection.FocusRow(context.Position.Row);
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       public static void DeleteEvent_Click(object sender, EventArgs e)
    //       {
    //           CellContext context = (CellContext)sender;
    //           try
    //           {
    //               SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;

    //               if (context.Position.Row > 0)
    //               {
    //                   if (context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow || dg.DataSource.AllowNew == false)
    //                   {
    //                       context.Grid.Selection.FocusRow(context.Position.Row);

    //                       if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
    //                       {
    //                           //SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
    //                           // dg.DeleteSelectedRows();

    //                           int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
    //                           if (dataIndex < dg.DataSource.Count)
    //                           {
    //                               dg.DataSource.RemoveAt(dataIndex);
    //                           }
    //                       }
    //                   }
    //               }
    //           }
    //           catch (Exception x1)
    //           {
    //               MessageBoxExt.ShowWarning(x1.Message);
    //           }

    //       }
    //       public static void ContextMenuEvent_Click(object sender, EventArgs e)
    //       {
    //           CellContext context = (CellContext)sender;
    //           try
    //           {
    //               if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
    //               {
    //                   context.Grid.Selection.FocusRow(context.Position.Row);


    //                   //if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
    //                   //{
    //                   //    SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
    //                   //    // dg.DeleteSelectedRows();

    //                   //    int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
    //                   //    if (dataIndex < dg.DataSource.Count)
    //                   //        dg.DataSource.RemoveAt(dataIndex);
    //                   //}
    //               }
    //           }
    //           catch (Exception x1)
    //           {
    //               MessageBoxExt.ShowWarning(x1.Message);
    //           }

    //       }
    //       static void dataGrid_UserException(object sender, SourceGrid.ExceptionEventArgs e)
    //       {
    //           MessageBoxExt.ShowException(e.Exception);
    //       }

    //       public static void AddColumnButton(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
    //       {
    //           if (ReadOnly)
    //               return;

    //           SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           clickEvent.Click += eventHandler;

    //           var btn = new SourceGrid.Cells.RowHeader("");
    //           //btn.Image = easiplan.app.Properties.Resources;
    //           btn.ToolTipText = "Click to add Funds";
    //           btn.AddController(clickEvent);

    //           var col = grid.Columns.Add("", "", btn);
    //           col.Width = 22;
    //           col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           col.MaximalWidth = 22;
    //           col.MinimalWidth = 22;
    //       }
    //       public static void AddRowClickEvent(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
    //       {
    //           //  row header click event
    //           SourceGrid.Cells.Controllers.CustomEvents ClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           ClickEvent.FocusEntered += eventHandler;
    //           grid.Controller.AddController(ClickEvent); //Event fired when any column is clicked

    //       }

    //       public static SourceGrid.DataGrid Rebind<T>(this SourceGrid.DataGrid grid, IList<T> bindingSource,ListChangedEventHandler PropertyChangedHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null)
    //       {
    //           grid.DataSource = new DevAge.ComponentModel.BoundList<T>(bindingSource);

    //           grid.DataSource.ListChanged -= PropertyChangedHandler;
    //           grid.DataSource.ListChanged += PropertyChangedHandler;
    //           grid.DataSource.ItemDeleted -= ItemDeletedEventHandler;
    //           grid.DataSource.ItemDeleted += ItemDeletedEventHandler;

    //           grid.Refresh();

    //           return grid;
    //       }

    //       #endregion

    //       #region SourceGrid.DataGrid Formatt
    //       public static SourceGrid.DataGrid Formatt(this SourceGrid.DataGrid grid, bool ReadOnly = false, bool AllowDelete = true, bool AllowAddNew = true, GridFormats format = GridFormats.Default, bool AlternateBackground = false)
    //       {
    //           grid.SuspendLayout();

    //           #region Header Cell Format
    //           DevAge.Drawing.VisualElements.ColumnHeader bheader = new DevAge.Drawing.VisualElements.ColumnHeader();
    //           bheader.BackColor = Color.WhiteSmoke;
    //           //bheader.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1, Color.Gainsboro, Color.Gainsboro);
    //           bheader.BackgroundColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid;


    //           SourceGrid.Cells.Views.Header header = new SourceGrid.Cells.Views.Header();
    //           header.Background = bheader;
    //           header.ForeColor = Color.DarkSlateGray;
    //           header.Font = Global.GridFont;// new Font("Verdana", 8, FontStyle.Regular);
    //           header.WordWrap = true;
    //           //header.TrimmingMode = TrimmingMode.Word;

    //           for (int i = 0; i < grid.Columns.Count; i++)
    //           {
    //               //grid.Columns[i].AutoSizeMode = SourceGrid.AutoSizeMode.EnableAutoSizeView;
    //               grid.Columns[i].HeaderCell.View = header;

    //               if (grid.Columns[i].DataCell.Editor != null)
    //               {
    //                   if (grid.Columns[i].DataCell.Editor.EditableMode != SourceGrid.EditableMode.None)
    //                   {
    //                       grid.Columns[i].DataCell.Editor.EnableEdit = !ReadOnly;

    //                   }
    //               }
    //           }
    //           #endregion

    //           #region Editor Cell Formats
    //           SourceGrid.Cells.Views.Cell mView_Amount = new SourceGrid.Cells.Views.Cell();
    //           mView_Amount.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;

    //           SourceGrid.Cells.Views.Cell mView_Amountdisabled = new SourceGrid.Cells.Views.Cell();
    //           mView_Amountdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
    //           mView_Amountdisabled.BackColor = Color.GhostWhite;
    //           mView_Amountdisabled.ForeColor = Color.DarkSlateGray;

    //           SourceGrid.Cells.Views.Cell mView_Textdisabled = new SourceGrid.Cells.Views.Cell();
    //           mView_Textdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
    //           mView_Textdisabled.BackColor = Color.GhostWhite;// Color.WhiteSmoke;
    //           mView_Textdisabled.ForeColor = Color.DarkSlateGray;

    //           //set editor formats/views
    //           for (int i = 0; i < grid.Columns.Count; i++)
    //           {
    //               if (grid.Columns[i].DataCell.Editor != null)
    //               {
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(CurrencyEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Amountdisabled;
    //                       else
    //                           grid.Columns[i].DataCell.View = mView_Amount;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(NumericEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Amountdisabled;
    //                       else
    //                           grid.Columns[i].DataCell.View = mView_Amount;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(DecimalEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Amountdisabled;
    //                       else
    //                           grid.Columns[i].DataCell.View = mView_Amount;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(StringEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Textdisabled;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(MultiLineEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Textdisabled;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(ComboBoxEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Textdisabled;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(DateEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Textdisabled;
    //                   }
    //                   if (grid.Columns[i].DataCell.Editor.GetType() == typeof(CheckBoxEditor))
    //                   {
    //                       if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
    //                           grid.Columns[i].DataCell.View = mView_Textdisabled;
    //                   }
    //               }
    //               else
    //               {
    //                   grid.Columns[i].DataCell.View = mView_Textdisabled;
    //               }

    //           }
    //           #endregion

    //           #region Alternate Background View
    //           if (AlternateBackground)
    //               foreach (SourceGrid.DataGridColumn colu in grid.Columns)
    //               {
    //                   SourceGrid.Conditions.ICondition condition =
    //                       SourceGrid.Conditions.ConditionBuilder.AlternateView(colu.DataCell.View,
    //                                                                            Color.WhiteSmoke, Color.Black);
    //                   colu.Conditions.Add(condition);
    //               }
    //           #endregion

    //           #region Selection Mode
    //           //grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
    //           //grid.Selection.EnableMultiSelection = false;

    //           SourceGrid.Selection.SelectionBase SelectionBase = grid.Selection as SourceGrid.Selection.SelectionBase;

    //           SelectionBase.BackColor = Color.FromArgb(75, Color.FromKnownColor(KnownColor.Highlight));

    //           DevAge.Drawing.RectangleBorder border = SelectionBase.Border;
    //           border.SetWidth(1);
    //           border.SetColor(Color.DarkGray);
    //           SelectionBase.Border = border;

    //           //SourceGrid.GridSpecialKeys specialKeys = SourceGrid.GridSpecialKeys.None;
    //           //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Tab;
    //           //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Arrows;
    //           //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Enter;
    //           //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Escape;

    //           //grid.SpecialKeys = specialKeys;

    //           //grid.Selection.FocusStyle = grid.Selection.FocusStyle | SourceGrid.FocusStyle.FocusFirstCellOnEnter;
    //           //grid.Selection.FocusStyle = grid.Selection.FocusStyle | SourceGrid.FocusStyle.RemoveFocusCellOnLeave;

    //           #endregion

    //           grid.UserException += dataGrid_UserException;

    //           #region Tooltip
    //           SourceGrid.Cells.Controllers.ToolTipText toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
    //           toolTipController.ToolTipTitle = "ToolTip example";
    //           toolTipController.ToolTipIcon = ToolTipIcon.Info;
    //           toolTipController.IsBalloon = true;


    //           #endregion

    //           FormatGrid(grid, format);

    //           grid.ResumeLayout(true);

    //           return grid;
    //       }

    //       internal static void FormatGrid(SourceGrid.DataGrid grid, GridFormats format = GridFormats.Default)
    //       {
    //           try
    //           {
    //               switch (format)
    //               {
    //                   case GridFormats.Default:
    //                        //control.Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Pixel);
    //                       grid.Font = Global.GridFont;

    //                       grid.BorderStyle = BorderStyle.FixedSingle;
    //                       grid.AutoStretchColumnsToFitWidth = true;
    //                       //grid.Paint -= Grid_Paint;
    //                       //grid.Paint += Grid_Paint;

    //                       // grid.AutoSizeCells();
    //                       grid.Columns.AutoSize(true);
    //                       grid.Columns.StretchToFit();

    //                       grid.EnableSort = false;

    //                       break;
    //                   case GridFormats.Format1:
    //                       grid.Font = MetroFonts.Default(12f);
    //                       grid.BorderStyle = BorderStyle.FixedSingle;
    //                       grid.AutoStretchColumnsToFitWidth = true;
    //                       grid.Columns.AutoSize(true);
    //                       grid.Columns.StretchToFit();

    //                       grid.EnableSort = false;
    //                       break;
    //                   case GridFormats.Format11:
    //                       grid.Font = MetroFonts.Default(13f);
    //                       grid.EnableSort = false;
    //                       break;
    //                   case GridFormats.Format2:
    //                       grid.Font = MetroFonts.Default(13f);
    //                       grid.EnableSort = false;
    //                       break;
    //                   case GridFormats.Format3:
    //                       grid.Font = MetroFonts.Default(14f);
    //                       grid.EnableSort = false;
    //                       break;
    //                   default:
    //                       break;
    //               };




    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }
    //       #endregion

    //       #region Tooltips
    //       public static void SetTooltip(this MetroFramework.Controls.MetroButton button, string Icon, string Title, string Tooltip, bool ShowAlways = false)
    //       {
    //           BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
    //           if (null != btnToolTip)
    //           {
    //               btnToolTip.RemoveAll();
    //           }

    //           btnToolTip = new BalloonToolTip();

    //           //Add tooltips for the buttons
    //           btnToolTip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), Icon);
    //           btnToolTip.ShowAlways = ShowAlways;
    //           btnToolTip.Absolute = true;
    //           btnToolTip.Title = Title;
    //           btnToolTip.SetToolTip(button, Tooltip);
    //           btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

    //           button.Tag = btnToolTip;
    //       }

    //       public static void SetTooltip(this MetroFramework.Controls.MetroButton button, BalloonIcon Icon, string Title, string Tooltip, bool ShowAlways = false)
    //       {
    //           BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
    //           if (null != btnToolTip)
    //           {
    //               btnToolTip.RemoveAll();
    //           }

    //           btnToolTip = new BalloonToolTip();

    //           //Add tooltips for the buttons
    //           btnToolTip.Icon = Icon;
    //           btnToolTip.ShowAlways = ShowAlways;
    //           btnToolTip.Absolute = true;
    //           btnToolTip.Title = Title;
    //           btnToolTip.SetToolTip(button, Tooltip);
    //           btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

    //           button.Tag = btnToolTip;
    //       }

    //       public static void HideTooltip(this MetroFramework.Controls.MetroButton button)
    //       {
    //           try
    //           {
    //               BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
    //               btnToolTip.ShowAlways = false;
    //               btnToolTip.RemoveAll();
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }

    //       }

    //       #endregion

    //       public enum GridFormats
    //       {
    //           Default,
    //           Format1,
    //           Format11,
    //           Format2,
    //           Format3

    //       }
    //   }

    //   #region Panel ControlsBuilder
    //   public enum ControlsLayout {
    //       Horizontal,
    //       Vertical
    //   }
    //   public class ControlBuilder<T> : IList<PanelControl<T>> where T : class
    //   {
    //       //  private readonly ModelMetadataProvider _metadataProvider;
    //       private readonly List<PanelControl<T>> _Controls = new List<PanelControl<T>>();

    //       public PanelControl<T> this[int index]
    //       {
    //           get
    //           {
    //               return _Controls[index];
    //           }

    //           set
    //           {
    //               _Controls[index] = value;
    //           }
    //       }

    //       public int Count
    //       {
    //           get
    //           {
    //               return _Controls.Count();
    //           }
    //       }

    //       public bool IsReadOnly
    //       {
    //           get
    //           {
    //               throw new NotImplementedException();
    //           }
    //       }

    //       public void Add(PanelControl<T> item)
    //       {
    //           _Controls.Add(item);
    //       }

    //       public void Clear()
    //       {
    //           _Controls.Clear();
    //       }

    //       public bool Contains(PanelControl<T> item)
    //       {
    //           return _Controls.Contains(item);
    //       }

    //       public void CopyTo(PanelControl<T>[] array, int arrayIndex)
    //       {
    //           _Controls.CopyTo(array, arrayIndex);
    //       }

    //       public IEnumerator<PanelControl<T>> GetEnumerator()
    //       {
    //           return _Controls.GetEnumerator();
    //       }

    //       public int IndexOf(PanelControl<T> item)
    //       {
    //           return _Controls.IndexOf(item);
    //       }

    //       public void Insert(int index, PanelControl<T> item)
    //       {
    //           _Controls.Insert(index, item);
    //       }

    //       public bool Remove(PanelControl<T> item)
    //       {
    //           return _Controls.Remove(item);
    //       }

    //       public void RemoveAt(int index)
    //       {
    //           _Controls.RemoveAt(index);
    //       }

    //       IEnumerator IEnumerable.GetEnumerator()
    //       {
    //           return GetEnumerator();
    //       }

    //       PanelControl<T> IList<PanelControl<T>>.this[int index]
    //       {
    //           get { return _Controls[index]; }
    //           set { _Controls[index] = value; }
    //       }

    //       /// <summary>
    //       /// Specifies a control should be constructed for the specified property.
    //       /// </summary>
    //       /// <param name="propertySpecifier">Lambda that specifies the property for which a column should be constructed</param>
    //       public PanelControl<T> For(Expression<Func<T, object>> propertySpecifier, string labelName = "", BaseMetroEditor editor = null,string toolTip="")
    //       {

    //               var memberExpression = propertySpecifier==null?null: GetMemberExpression(propertySpecifier);
    //               var propertyType = propertySpecifier == null ? null : GetTypeFromMemberExpression(memberExpression);
    //               var declaringType = memberExpression == null ? null : memberExpression.Expression.Type;
    //               var inferredName = memberExpression == null ? null : memberExpression.Member.Name;
    //               var control = new PanelControl<T>(propertySpecifier==null?null:propertySpecifier.Compile(), inferredName, propertyType, editor);


    //           if (declaringType != null)
    //           {
    //               //    var metadata = _metadataProvider.GetMetadataForProperty(null, declaringType, inferredName);

    //               //    if (!string.IsNullOrEmpty(metadata.DisplayName))
    //               //    {
    //               //        column.Named(metadata.DisplayName);
    //               //    }

    //               //    if (!string.IsNullOrEmpty(metadata.DisplayFormatString))
    //               //    {
    //               //        column.Format(metadata.DisplayFormatString);
    //               //    }
    //           }

    //           if (!string.IsNullOrEmpty(labelName))
    //           {
    //               control.Named(labelName);
    //           }
    //           if (!string.IsNullOrEmpty(toolTip))
    //           {
    //               control.ToolTiped(toolTip);
    //           }

    //           Add(control);

    //           return control;
    //       }

    //       public static MemberExpression GetMemberExpression(LambdaExpression expression)
    //       {
    //           return RemoveUnary(expression.Body) as MemberExpression;
    //       }

    //       private static Type GetTypeFromMemberExpression(MemberExpression memberExpression)
    //       {
    //           if (memberExpression == null) return null;

    //           var dataType = GetTypeFromMemberInfo(memberExpression.Member, (PropertyInfo p) => p.PropertyType);
    //           if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (MethodInfo m) => m.ReturnType);
    //           if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (FieldInfo f) => f.FieldType);

    //           return dataType;
    //       }

    //       private static Type GetTypeFromMemberInfo<TMember>(MemberInfo member, Func<TMember, Type> func) where TMember : MemberInfo
    //       {
    //           if (member is TMember)
    //           {
    //               return func((TMember)member);
    //           }
    //           return null;
    //       }

    //       private static Expression RemoveUnary(Expression body)
    //       {
    //           var unary = body as UnaryExpression;
    //           if (unary != null)
    //           {
    //               return unary.Operand;
    //           }
    //           return body;
    //       }
    //   }
    //   public class PanelControl<T> : IPanelControl<T> where T : class
    //   {
    //       private readonly string _name;
    //       private string _displayName;
    //       private bool _doNotSplit;
    //       private readonly Func<T, object> _columnValueFunc;
    //       private readonly Type _dataType;
    //       private Func<T, bool> _cellCondition = x => true;
    //       private string _format;
    //       private bool _visible = true;
    //       private bool _htmlEncode = true;
    //       private readonly IDictionary<string, object> _headerAttributes = new Dictionary<string, object>();
    //       // private List<Func<GridRowViewData<T>, IDictionary<string, object>>> _attributes = new List<Func<GridRowViewData<T>, IDictionary<string, object>>>();
    //       private bool _sortable = true;
    //       private string _sortColumnName = null;
    //       private SortDirection? _initialDirection;
    //       private int? _position;
    //       private Func<object, object> _headerRenderer = x => null;

    //       private string _toolTip;

    //       private BaseMetroEditor _editor = null;

    //       public BaseMetroEditor Editor
    //       {
    //           get { return _editor; }
    //           set { _editor = value; }
    //       }

    //       /// <summary>
    //       /// Creates a new instance of the GridColumn class
    //       /// </summary>
    //       public PanelControl(Func<T, object> columnValueFunc, string name, Type type, BaseMetroEditor editor = null)
    //       {
    //           _name = name;
    //           _displayName = name;
    //           _dataType = type;
    //           _columnValueFunc = columnValueFunc;
    //           _editor = editor;


    //       }

    //       public bool Sortable
    //       {
    //           get { return _sortable; }
    //       }

    //       public bool Visible
    //       {
    //           get { return _visible; }
    //       }

    //       public string SortColumnName
    //       {
    //           get { return _sortColumnName; }
    //       }

    //       public SortDirection? InitialDirection
    //       {
    //           get { return _initialDirection; }
    //       }

    //       /// <summary>
    //       /// Name of the column
    //       /// </summary>
    //       public string Name
    //       {
    //           get { return _name; }
    //       }

    //       /// <summary>
    //       /// Display name for the column
    //       /// </summary>
    //       public string DisplayName
    //       {
    //           get
    //           {
    //               if (_doNotSplit)
    //               {
    //                   return _displayName;
    //               }
    //               return SplitPascalCase(_displayName);
    //           }
    //       }

    //       /// <summary>
    //       /// ToolTip for the column
    //       /// </summary>
    //       public string ToolTip
    //       {
    //           get
    //           {
    //               if (_doNotSplit)
    //               {
    //                   return _toolTip;
    //               }
    //               return SplitPascalCase(_toolTip);
    //           }
    //       }

    //       /// <summary>
    //       /// The type of the object being rendered for thsi column. 
    //       /// Note: this will return null if the type cannot be inferred.
    //       /// </summary>
    //       public Type ColumnType
    //       {
    //           get { return _dataType; }
    //       }

    //       public int? Position
    //       {
    //           get { return _position; }
    //       }

    //       //IGridColumn<T> IGridColumn<T>.Attributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes)
    //       //{
    //       //    _attributes.Add(attributes);
    //       //    return this;
    //       //}

    //       IPanelControl<T> IPanelControl<T>.Sortable(bool isColumnSortable)
    //       {
    //           _sortable = isColumnSortable;
    //           return this;
    //       }

    //       IPanelControl<T> IPanelControl<T>.SortColumnName(string name)
    //       {
    //           _sortColumnName = name;
    //           return this;
    //       }

    //       IPanelControl<T> IPanelControl<T>.SortInitialDirection(SortDirection initialDirection)
    //       {
    //           _initialDirection = initialDirection;
    //           return this;
    //       }


    //       IPanelControl<T> IPanelControl<T>.InsertAt(int index)
    //       {
    //           _position = index;
    //           return this;
    //       }

    //       /// <summary>
    //       /// Additional attributes for the column header
    //       /// </summary>
    //       public IDictionary<string, object> HeaderAttributes
    //       {
    //           get { return _headerAttributes; }
    //       }

    //       /// <summary>
    //       /// Additional attributes for the cell
    //       /// </summary>
    //       //public Func<GridRowViewData<T>, IDictionary<string, object>> Attributes
    //       //{
    //       //    get { return GetAttributesFromRow; }
    //       //}

    //       //private IDictionary<string, object> GetAttributesFromRow(GridRowViewData<T> row)
    //       //{
    //       //    var dictionary = new Dictionary<string, object>();
    //       //    var pairs = _attributes.SelectMany(attributeFunc => attributeFunc(row));

    //       //    foreach (var pair in pairs)
    //       //    {
    //       //        dictionary[pair.Key] = pair.Value;
    //       //    }

    //       //    return dictionary;
    //       //}

    //       public IPanelControl<T> Named(string name)
    //       {
    //           _displayName = name;
    //           _doNotSplit = true;
    //           return this;
    //       }

    //       public IPanelControl<T> ToolTiped(string tooltip)
    //       {
    //           _toolTip = tooltip;
    //           _doNotSplit = true;
    //           return this;
    //       }

    //       public IPanelControl<T> DoNotSplit()
    //       {
    //           _doNotSplit = true;
    //           return this;
    //       }

    //       public IPanelControl<T> Format(string format)
    //       {
    //           _format = format;
    //           return this;
    //       }

    //       public IPanelControl<T> CellCondition(Func<T, bool> func)
    //       {
    //           _cellCondition = func;
    //           return this;
    //       }

    //       IPanelControl<T> IPanelControl<T>.Visible(bool isVisible)
    //       {
    //           _visible = isVisible;
    //           return this;
    //       }

    //       public IPanelControl<T> Label(Func<object, object> headerRenderer)
    //       {
    //           _headerRenderer = headerRenderer;
    //           return this;
    //       }

    //       public IPanelControl<T> Encode(bool shouldEncode)
    //       {
    //           _htmlEncode = shouldEncode;
    //           return this;
    //       }



    //       IPanelControl<T> IPanelControl<T>.LabelAttributes(IDictionary<string, object> attributes)
    //       {
    //           foreach (var attribute in attributes)
    //           {
    //               _headerAttributes.Add(attribute);
    //           }

    //           return this;
    //       }

    //       private string SplitPascalCase(string input)
    //       {
    //           if (string.IsNullOrEmpty(input))
    //           {
    //               return input;
    //           }
    //           return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
    //       }

    //       /// <summary>
    //       /// Gets the value for a particular cell in this column
    //       /// </summary>
    //       /// <param name="instance">Instance from which the value should be obtained</param>
    //       /// <returns>Item to be rendered</returns>
    //       public object GetValue(T instance)
    //       {
    //           if (!_cellCondition(instance) || instance == null)
    //           {
    //               return null;
    //           }

    //           var value = _columnValueFunc(instance);

    //           if (!string.IsNullOrEmpty(_format))
    //           {
    //               value = string.Format(_format, value);
    //           }

    //           //if (_htmlEncode && value != null && !(value is IHtmlString))
    //           //{
    //           //    value = HttpUtility.HtmlEncode(value.ToString());
    //           //}

    //           return value;
    //       }

    //       public string GetHeader()
    //       {
    //           var header = _headerRenderer(null);
    //           return header == null ? null : header.ToString();
    //       }

    //   }
    //   public interface IPanelControl<T>
    //   {
    //       /// <summary>
    //       /// Specified an explicit name for the column.
    //       /// </summary>
    //       /// <param name="name">Name of column</param>
    //       /// <returns></returns>
    //       IPanelControl<T> Named(string name);
    //       /// <summary>
    //       /// If the property name is PascalCased, it should not be split part.
    //       /// </summary>
    //       /// <returns></returns>
    //       IPanelControl<T> DoNotSplit();
    //       /// <summary>
    //       /// A custom format to use when building the cell's value
    //       /// </summary>
    //       /// <param name="format">Format to use</param>
    //       /// <returns></returns>
    //       IPanelControl<T> Format(string format);
    //       /// <summary>
    //       /// Delegate used to hide the contents of the cells in a column.
    //       /// </summary>
    //       IPanelControl<T> CellCondition(Func<T, bool> func);

    //       /// <summary>
    //       /// Determines whether the column should be displayed
    //       /// </summary>
    //       /// <param name="isVisible"></param>
    //       /// <returns></returns>
    //       IPanelControl<T> Visible(bool isVisible);

    //       IPanelControl<T> Label(Func<object, object> customHeaderRenderer);

    //       /// <summary>
    //       /// Determines whether or not the column should be encoded. Default is true.
    //       /// </summary>
    //       IPanelControl<T> Encode(bool shouldEncode);



    //       /// <summary>
    //       /// Defines additional attributes for the column heading.
    //       /// </summary>
    //       /// <param name="attributes"></param>
    //       /// <returns></returns>
    //       IPanelControl<T> LabelAttributes(IDictionary<string, object> attributes);

    //       /// <summary>
    //       /// Defines additional attributes for the cell. 
    //       /// </summary>
    //       /// <param name="attributes">Lambda expression that should return a dictionary containing the attributes for the cell</param>
    //       /// <returns></returns>
    //       //IGridColumn<T> Attributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes);

    //       /// <summary>
    //       /// Specifies whether or not this column should be sortable. 
    //       /// The default is true. 
    //       /// </summary>
    //       /// <param name="isColumnSortable"></param>
    //       /// <returns></returns>
    //       IPanelControl<T> Sortable(bool isColumnSortable);

    //       /// <summary>
    //       /// Specifies a custom name that should be used when sorting on this column
    //       /// </summary>
    //       /// <param name="name"></param>
    //       /// <returns></returns>
    //       IPanelControl<T> SortColumnName(string name);

    //       /// <summary>
    //       /// Specifies the direction of the sort link when this column is not currently sorted.  
    //       /// The direction will continue to toggle when it is the currently sorted column. 
    //       /// </summary>
    //       /// <param name="initialDirection"></param>
    //       /// <returns></returns>
    //       IPanelControl<T> SortInitialDirection(SortDirection initialDirection);



    //       /// <summary>
    //       /// Specifies the position of a column. 
    //       /// This is usually used in conjunction with the AutoGenerateColumns method 
    //       /// in order to specify where additional custom columns should be placed.
    //       /// </summary>
    //       /// <param name="index">The index at which the column should be inserted</param>
    //       IPanelControl<T> InsertAt(int index);


    //   }
    //   #endregion

    //   #region Editors
    //   public interface IMetroEditor
    //   {
    //       bool Enabled { get; set; }

    //       //Editor
    //       Type EditorType { get; set; }

    //       EditorBase DataGridEditor { get; set; }

    //       //Style
    //       DataGridViewCellStyle FormatStyle { get; set; }
    //       Size Size { get; set; }

    //       Font Font { get; set; }

    //       //Create Controls
    //       Control CreateControl();

    //       DataGridViewColumn CreateColumn(string name, string headerText);
    //       DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid);

    //       //Validation
    //       IMetroValidator Validator { get; set; }

    //       //DataBinding
    //       string BindingName { get; set; }
    //       DataSourceUpdateMode dataSourceUpdateMode { get; set; } 

    //       //Editor Events
    //       Delegate OnEditorStarting();
    //       Delegate OnEditorFinish();
    //       Delegate OnEditorKeyUp();
    //       Delegate OnEditorKeyDown();
    //       Delegate OnEditorLostFocus();
    //       Delegate OnEditorGetFocus();
    //   }
    //   public abstract class BaseMetroEditor : IMetroEditor
    //   {
    //       ErrorProvider errorProvider = new ErrorProvider();
    //       internal string _valueMember;
    //       internal string _displayMember;
    //       internal int _width;
    //       internal int _height;

    //       #region  Delegate : SelectedIndexChange
    //       public delegate void ControlSelectedIndexChanged(object sender, EventArgs e);
    //       public event ControlSelectedIndexChanged controlSelectedIndexChanged;
    //       public void Control_SelectedIndexChanged(object sender, EventArgs e)
    //       {
    //           controlSelectedIndexChanged?.Invoke(this, e);
    //       }


    //       #endregion

    //       #region Delegate : EnterKey Clicked
    //       public delegate void ControlKeyClicked(object sender, KeyEventArgs e);
    //       public event ControlKeyClicked EnterKeyClicked;
    //       #endregion

    //       #region Default Constructor and Fluent Extentions
    //       public BaseMetroEditor(int Width=200,int Height=0) 
    //       {
    //           _width = Width;
    //           _height = Height;

    //           Watermark = string.Empty;

    //           Enabled = true;

    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleLeft
    //           };
    //       }
    //       public BaseMetroEditor ReadOnly(bool readOnly)
    //       {
    //           Enabled = !readOnly;
    //           return this;

    //       }
    //       public BaseMetroEditor FontSize(Font font,bool IsBold=false)
    //       {
    //           Font = font;

    //           return this;
    //       }
    //       public virtual BaseMetroEditor ControlValueChanged(ControlSelectedIndexChanged indexChanged)
    //       {
    //           controlSelectedIndexChanged = indexChanged;

    //           return this;
    //       }
    //       #endregion

    //       #region  Interface Properties
    //       public bool Enabled
    //       {
    //           get; set;
    //       }
    //       public Type EditorType
    //       {
    //           get; set;
    //       }
    //       public Control Control { get; set; }
    //       public DataGridViewColumn Column { get; set; }
    //       public DataGridColumn dataGridColumn { get; set; }
    //       public DataGridViewCellStyle FormatStyle { get; set; }
    //       public string ErrorMessage { get; set; }
    //       public string Watermark { get; set; }
    //       Size _size = new Size(100, 20);
    //       public Size Size { get { return _size; } set { _size = value; } }
    //       public Font Font { get; set; } = Global.TextFont;
    //       #endregion

    //       //Interface Methods
    //       public virtual Control CreateControl()
    //       {

    //           Control.Paint += Control_Paint;
    //           Control.TextChanged += Control_TextChanged;
    //           Control.KeyDown += Control_KeyDown;
    //           Control.KeyUp += Control_KeyUp;
    //           Control.KeyPress += Control_KeyPress;
    //           Control.LostFocus += Control_LostFocus;
    //           Control.GotFocus += Control_GotFocus;

    //           Control.CausesValidation = true; //Enables DataSourceUpdate.OnValidation event to fire

    //           Control.Validating += Control_Validating;
    //           Control.Validated += Control_Validated;

    //           Control.Width = _width;

    //           if (_height > 0)
    //               Control.Height = _height;
    //           else
    //               _height = Control.Height;

    //           Size = new Size(_width, _height);


    //           return Control;
    //       }

    //       private void Control_KeyPress(object sender, KeyPressEventArgs e)
    //       {
    //           //suppress the beep sound
    //           if (e.KeyChar == (char)Keys.Return)
    //           {
    //               e.Handled = true;
    //           }
    //       }

    //       public virtual DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column.ReadOnly = !Enabled;

    //           Column.DefaultCellStyle = new DataGridViewCellStyle(FormatStyle);

    //           if (Column.ReadOnly)
    //           {
    //               //Column.DefaultCellStyle.ForeColor = Color.Gray;
    //               //Column.DefaultCellStyle.SelectionForeColor = Color.Gray;
    //           }

    //           return Column;
    //       }

    //       //DataSource List for Dropdown controls
    //       internal IList _DataSource;
    //       public BaseMetroEditor DataSourceList(IList dataSource, string displayMember = "Text", string valueMember = "Value")
    //       {

    //           _DataSource = dataSource;
    //           _valueMember = valueMember;
    //           _displayMember = displayMember;

    //           return this;

    //       }

    //       //DataBinding
    //       public string BindingName { get; set; }
    //       public DataSourceUpdateMode dataSourceUpdateMode { get; set; }

    //       //Validator
    //       public IMetroValidator Validator { get; set; }

    //       # region DataGrid Editor
    //       public EditorBase DataGridEditor
    //       {
    //           get; set;
    //       }

    //       public virtual DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {

    //           this.DataGridEditor.EditableMode = !Enabled ? EditableMode.None : EditableMode.SingleClick;

    //           this.DataGridEditor.Changed += DataGridEditor_Changed;
    //           this.DataGridEditor.ConvertingObjectToValue += DataGridEditor_ConvertingObjectToValue;
    //           this.DataGridEditor.ConvertingValueToDisplayString += DataGridEditor_ConvertingValueToDisplayString;
    //           this.DataGridEditor.ConvertingValueToObject += DataGridEditor_ConvertingValueToObject;
    //           this.DataGridEditor.EditException += DataGridEditor_EditException;
    //           //this.DataGridEditor.Validated += DataGridEditor_Validated;
    //           //this.DataGridEditor.Validating += DataGridEditor_Validating;

    //           //this.DataGridEditor.Control.Validating += Control_Validating1;
    //           //this.DataGridEditor.Control.Validated += Control_Validated1;

    //           this.dataGridColumn = grid.Columns.Add(name, headerText, this.DataGridEditor);

    //           //SourceGrid.Cells.Views.Cell mView_Amount = new SourceGrid.Cells.Views.Cell();
    //           //mView_Amount.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;

    //           SourceGrid.Cells.Views.Cell mView_Amountdisabled = new SourceGrid.Cells.Views.Cell();
    //           //  mView_Amountdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
    //           mView_Amountdisabled.BackColor = Color.GhostWhite.GetPastelShade();

    //           //SourceGrid.Cells.Views.Cell mView_Textdisabled = new SourceGrid.Cells.Views.Cell();
    //           //mView_Textdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
    //           //mView_Textdisabled.BackColor = Color.WhiteSmoke;

    //           //if (!Enabled)
    //           //{
    //           //    this.dataGridColumn.DataCell.View = mView_Amountdisabled;
    //           //}
    //           if (this._width > 200)
    //           {
    //               this.dataGridColumn.MinimalWidth = this._width;
    //               this.dataGridColumn.Width = this._width;
    //           }
    //           else if (this._width < 200)
    //           {
    //               this.dataGridColumn.MaximalWidth = this._width;
    //           }


    //           return this.dataGridColumn;
    //       }

    //       private void Control_Validated1(object sender, EventArgs e)
    //       {
    //           throw new NotImplementedException();
    //       }

    //       private void Control_Validating1(object sender, CancelEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //           //errorProvider.SetError(cntrl, "");
    //           try
    //           {
    //               if (Validator != null)
    //                   Validator.Validate(cntrl.GetEditedValue());

    //           }
    //           catch (Exception)
    //           {
    //               //errorProvider.SetError(cntrl, this.ErrorMessage);
    //               e.Cancel = true;
    //               cntrl.ClearCell(cntrl.EditCellContext);
    //           }
    //       }

    //       private void DataGridEditor_Validated(object sender, CellContextEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_EditException(object sender, ExceptionEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //           //cntrl.ApplyEdit();

    //           e.Handled = true;

    //       }

    //       private void DataGridEditor_ConvertingValueToObject(object sender, ConvertingObjectEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_ConvertingValueToDisplayString(object sender, ConvertingObjectEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_ConvertingObjectToValue(object sender, ConvertingObjectEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_Changed(object sender, EventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_Validating1(object sender, ValidatingCellEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //       }

    //       private void DataGridEditor_Validating(object sender, ValidatingCellEventArgs e)
    //       {
    //           var cntrl = sender as EditorBase;
    //           //errorProvider.SetError(cntrl, "");
    //           try
    //           {
    //               if (Validator != null)
    //                   Validator.Validate(e.NewValue);

    //           }
    //           catch (Exception)
    //           {
    //               //errorProvider.SetError(cntrl, this.ErrorMessage);
    //               e.Cancel = true;

    //               cntrl.ApplyEdit();
    //           }
    //       }

    //       //Editor Delegates
    //       public Delegate OnEditorFinish()
    //       {
    //           return null;
    //       }

    //       public Delegate OnEditorGetFocus()
    //       {
    //           return null;
    //       }

    //       public Delegate OnEditorKeyDown()
    //       {
    //           return null;
    //       }

    //       public Delegate OnEditorKeyUp()
    //       {
    //           return null;
    //       }

    //       public Delegate OnEditorLostFocus()
    //       {
    //           return null;
    //       }

    //       public Delegate OnEditorStarting()
    //       {
    //           return null;
    //       }

    //       public virtual BaseMetroEditor OnEditorClick(EventHandler eventHandler)
    //       {

    //           return this;
    //       }
    //       #endregion

    //       #region Control Events
    //       public virtual void Control_TextChanged(object sender, EventArgs e)
    //       {

    //       }
    //       private void Control_Paint(object sender, PaintEventArgs e)
    //       {

    //       }

    //       private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    //       {
    //           var cntrl = sender as Control;
    //           errorProvider.SetError(cntrl, "");
    //           try
    //           {
    //                if (Validator != null && this.Enabled)
    //                   Validator.Validate(cntrl.Text);               
    //           }
    //           catch (Exception)
    //           {
    //               errorProvider.SetError(cntrl, this.ErrorMessage);
    //               e.Cancel = true;
    //           }
    //       }

    //       public virtual void Control_Validated(object sender, EventArgs e)
    //       {

    //       }

    //       private void Control_GotFocus(object sender, EventArgs e)
    //       {

    //       }
    //       private void Control_LostFocus(object sender, EventArgs e)
    //       {
    //          // return;

    //           var cntrl = sender as Control;
    //           errorProvider.SetError(cntrl, "");

    //           try
    //           {
    //               //using (new AppWaitCursor(sender))
    //               //{

    //                   if (Validator != null && this.Enabled)
    //                     if (!Validator.IsValidated) 
    //                       Validator.Validate(cntrl.Text);

    //               //YJ 2021-11-05
    //               UpdateDataBindings(cntrl.Text);
    //               //}
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);

    //               errorProvider.SetError(cntrl, this.ErrorMessage);

    //               var restoreFocus = (System.Threading.ThreadStart)delegate { Control.Focus(); };
    //               Control.BeginInvoke(restoreFocus);
    //           }
    //       }

    //       private void Control_KeyUp(object sender, KeyEventArgs e)
    //       {
    //           e.Handled = false;
    //           e.SuppressKeyPress = false;
    //       }

    //       private void Control_KeyDown(object sender, KeyEventArgs e)
    //       {

    //           if (e.KeyCode == Keys.Escape)
    //           {
    //               DevAgeTextBox textBox = sender as DevAgeTextBox;
    //               if (textBox != null)
    //               {
    //                   textBox.Undo();
    //                   textBox.ClearUndo();
    //               }

    //           }
    //           if (e.KeyCode == Keys.Enter)
    //           {

    //               UpdateDataBindings(sender);

    //               EnterKeyClicked?.Invoke(this.Control, e);
    //               //suppress the beep sound
    //               e.Handled = true;
    //               e.SuppressKeyPress = true;

    //           }
    //       }
    //       #endregion


    //       private void UpdateDataBindings(object value)
    //       {
    //           if (this.dataSourceUpdateMode == DataSourceUpdateMode.OnValidation)
    //           {
    //               foreach(Binding db in this.Control.DataBindings)
    //               {
    //                       db.WriteValue();
    //               }
    //           }
    //       }
    //   }

    //   public class MetroTextBoxEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroTextBoxEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           DevAgeTextBox control = new DevAgeTextBox();
    //           control.Font = Global.TextFont;
    //           control.ReadOnlyChanged += Control_ReadOnlyChanged;

    //           control.SelectedText = this.Watermark;
    //           control.ReadOnly = !Enabled;

    //           control.Height += 3;

    //           this.Control = control;

    //           if(!string.IsNullOrEmpty(RegExpression))
    //               this.Validator = new RegExValidator(RegExpression);

    //           return base.CreateControl();
    //       }

    //       private void Control_ReadOnlyChanged(object sender, EventArgs e)
    //       {
    //           DevAgeTextBox textBox = sender as DevAgeTextBox;
    //           if (textBox != null)
    //               if (textBox.ReadOnly)
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
    //                   this.Enabled = false;
    //               }
    //               else
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
    //                   this.Enabled = true;
    //               }
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };
    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           if(this.DataGridEditor==null)
    //               this.DataGridEditor = new StringEditor();

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }

    //   }
    //   public class MetroMultiLineTextBoxEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroMultiLineTextBoxEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           //MetroTextBox control = new MetroTextBox();
    //           //control.FontSize = MetroFramework.MetroTextBoxSize.Medium;
    //           //control.WaterMark = this.Watermark;
    //           //control.ReadOnly = !Enabled;

    //           //_height = (control.Height*2) + 3;
    //           //control.Multiline = true;
    //           ////control.AcceptsReturn = true;
    //           ////control.WordWrap = true;
    //           //control.MaxLength = 4000;
    //           //control.ScrollBars = ScrollBars.Vertical;

    //           DevAgeTextBox control = new DevAgeTextBox();
    //           control.ReadOnlyChanged += Control_ReadOnlyChanged;
    //           control.AcceptsReturn = true;
    //           control.Multiline = true;
    //           control.TextAlign = HorizontalAlignment.Left;

    //           control.Font = Global.TextFont;

    //           control.SelectedText = this.Watermark;

    //           control.ReadOnly = !Enabled;
    //           control.Height = (control.Height * 2) + 3;
    //           control.MaxLength = 4000;
    //           control.ScrollBars = ScrollBars.Vertical;

    //           this.Control = control;
    //           this.Validator = new RegExValidator(RegExpression);

    //           //this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

    //           //this.Control.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

    //           Control.CausesValidation = true; //Enables DataSourceUpdate.OnValidation event to fire

    //           //Control.Validating += Control_Validating;
    //           //Control.Validated += Control_Validated;

    //           Control.Width = _width;

    //           if (_height > 0)
    //               Control.Height = _height;
    //           else
    //               _height = Control.Height;

    //           Size = new Size(_width, _height);

    //           return this.Control;// base.CreateControl();
    //       }

    //       private void Control_ReadOnlyChanged(object sender, EventArgs e)
    //       {
    //           DevAgeTextBox textBox = sender as DevAgeTextBox;
    //           if (textBox != null)
    //               if (textBox.ReadOnly)
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    //               }
    //               else
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
    //               }
    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,
    //           };
    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new MultiLineEditor();

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }

    //       public override void Control_TextChanged(object sender, EventArgs e)
    //       {
    //           DevAgeTextBox textBox = sender as DevAgeTextBox;
    //           try
    //           {
    //               textBox.DataBindings[0].WriteValue();
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }

    //           base.Control_TextChanged(sender, e);
    //       }

    //   }
    //   public class MetroStringEditor : MetroTextBoxEditor
    //   {
    //       public MetroStringEditor(int Width=200, int Height = 0) : base(Width,Height)
    //       {
    //       }
    //       public override Control CreateControl()
    //       {
    //           RegExpression = RegExpressions.CharactersAscii();
    //           this.ErrorMessage = string.Format("Only Characters are allowed");

    //           return base.CreateControl();
    //       }
    //   }
    //   public class MetroNumberEditor : MetroTextBoxEditor
    //   {
    //       public MetroNumberEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           RegExpression = RegExpressions.Numbers();
    //           this.ErrorMessage = string.Format("Only Numbers are allowed");

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }
    //       public override Control CreateControl()
    //       {

    //           return base.CreateControl();
    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //             //  Format = "c"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new NumericEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }
    //   public class MetroStringNumberEditor : MetroTextBoxEditor
    //   {
    //       public MetroStringNumberEditor(int Width = 200, int Height = 0) : base(Width,Height)
    //       {
    //           RegExpression = RegExpressions.CharacterNumbersAscii();
    //           this.ErrorMessage = string.Format("Only Characters and Numbers are allowed");
    //       }

    //       public override Control CreateControl()
    //       {
    //           return base.CreateControl();
    //       }

    //   }
    //   public class MetroNumberUpDownEditor : BaseMetroEditor
    //   {
    //       int _min = 0;
    //       int _max = 100;

    //       public string RegExpression = string.Empty;
    //       public MetroNumberUpDownEditor(int min = 0,int max=100, int Width = 200, int Height = 0) : base(Width,Height)
    //       {
    //           RegExpression = RegExpressions.Numbers();
    //           this.ErrorMessage = string.Format("Only Numbers are allowed");

    //           this.BindingName = "Text;Value";

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

    //           _min = min;
    //           _max = max;
    //       }
    //       public override Control CreateControl()
    //       {
    //           //DevAgeNumericUpDown control = new DevAgeNumericUpDown();
    //           // MetroTrackBar control = new MetroTrackBar();
    //           KryptonTrackBar control = new KryptonTrackBar();

    //           control.BackStyle = PaletteBackStyle.ContextMenuItemSplit;
    //           control.TickStyle = TickStyle.BottomRight;           


    //           control.Enabled = Enabled;

    //           //control.Height += 5;
    //           //_height = control.Height + 3;

    //           control.Height += 20;
    //           _height = control.Height + 5;

    //           control.Minimum = _min;
    //           control.Maximum = _max;
    //           control.SmallChange = 1;
    //           control.LargeChange = 5;          

    //           this.Control = control;


    //           this.Validator = new RegExValidator(RegExpression);


    //           return base.CreateControl();
    //       }

    //       //private void Control_ValueChanged(object sender, EventArgs e)
    //       //{
    //       //    try
    //       //    {
    //       //        //MetroTrackBar cntrl = sender as MetroTrackBar;

    //       //        //cntrl.DataBindings[0].WriteValue();

    //       //    }
    //       //    catch (Exception x)
    //       //    {
    //       //        Program.Logger.Error(x);
    //       //    }
    //       //}

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "D"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new NumericEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }

    //   public class MetroPercentageUpDownEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroPercentageUpDownEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           RegExpression = RegExpressions.Decimals();
    //           this.ErrorMessage = string.Format("Only Decimals are allowed");

    //           this.BindingName = "Text;Value";

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }
    //       public override Control CreateControl()
    //       {
    //           //DevAgeNumericUpDown control = new DevAgeNumericUpDown();
    //           MetroTrackBar control = new MetroTrackBar();


    //           control.Enabled = Enabled;

    //           //control.Height += 5;
    //           //_height = control.Height + 3;

    //           control.Height += 20;
    //           _height = control.Height + 5;

    //           control.Minimum = 1;
    //           control.Maximum = 100;
    //           control.SmallChange = 1;
    //           control.LargeChange = 1;

    //           this.Control = control;

    //           this.Validator = new RegExValidator(RegExpression);


    //           return base.CreateControl();
    //       }

    //       //private void Control_ValueChanged(object sender, EventArgs e)
    //       //{
    //       //    try
    //       //    {
    //       //        //MetroTrackBar cntrl = sender as MetroTrackBar;

    //       //        //cntrl.DataBindings[0].WriteValue();

    //       //    }
    //       //    catch (Exception x)
    //       //    {
    //       //        Program.Logger.Error(x);
    //       //    }
    //       //}

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "D"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new NumericEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }
    //   public class MetroCurrencyEditor : MetroTextBoxEditor
    //   {
    //       public MetroCurrencyEditor(int Width = 200, int Height = 0) : base(Width,Height)
    //       {
    //           RegExpression = RegExpressions.Currency();
    //           this.ErrorMessage = string.Format("Only Currency digits are allowed");

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

    //       }
    //       public override Control CreateControl()
    //       {


    //           base.CreateControl();

    //           Control.Tag = "C";
    //           Control.RightToLeft = RightToLeft.Yes;



    //           return Control;

    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "c"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new CurrencyEditor();

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }

    //       public override void Control_TextChanged(object sender, EventArgs e)
    //       {
    //           Control editor = sender as Control;
    //           Decimal val = 0;
    //           Decimal.TryParse(editor.Text.Replace("R", ""), out val);

    //           if (val < 0)
    //           {
    //               editor.BackColor = Color.FromKnownColor(KnownColor.Yellow);
    //               editor.ForeColor = Color.FromKnownColor(KnownColor.Red);
    //           }
    //           else
    //           {
    //               editor.BackColor = Color.FromKnownColor(KnownColor.White);
    //               editor.ForeColor = Color.FromKnownColor(KnownColor.ControlText); ;
    //           }

    //           base.Control_TextChanged(sender, e);
    //       }
    //   }
    //   public class MetroDecimalEditor : MetroTextBoxEditor
    //   {
    //       public MetroDecimalEditor(int Width = 200, int Height = 0) : base(Width,Height)
    //       {
    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }

    //       public override Control CreateControl()
    //       {
    //           RegExpression = RegExpressions.Decimals();
    //           this.ErrorMessage = string.Format("Only Decimals are allowed");


    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "N3"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new DecimalEditor();

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroNameEditor : MetroTextBoxEditor
    //   {
    //       public MetroNameEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {

    //       }
    //       public override Control CreateControl()
    //       {
    //           RegExpression = RegExpressions.CharactersUnicode(" -'`^");
    //           this.ErrorMessage = string.Format("Not a valid name");

    //           return base.CreateControl();
    //       }
    //   }
    //   public class MetroComboBoxEditor : BaseMetroEditor
    //   {
    //       bool _dropDownList;
    //       public MetroComboBoxEditor(int Width = 200, int Height = 0, bool DropdownList = false) : base(Width,Height)
    //       {
    //           this.BindingName = "SelectedValue";//SelectedValue
    //           this._dropDownList = DropdownList;

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }
    //       public override Control CreateControl()
    //       {
    //           //MetroComboBox control = new MetroComboBox();
    //           //control.DropDownStyle = ComboBoxStyle.DropDown;

    //           //System.Windows.Forms.ComboBox control = new System.Windows.Forms.ComboBox();
    //           //control.DropDownStyle = ComboBoxStyle.DropDown;

    //           //DevAgeComboBox control = new DevAgeComboBox();
    //           //control.EnabledChanged += Control_EnabledChanged;
    //           //control.Enabled = Enabled;

    //           ExComboBox control = new ExComboBox();
    //           control.ReadOnly = !Enabled;

    //           if (_dropDownList == true)
    //           {
    //               control.DropDownStyle = ComboBoxStyle.DropDownList;
    //               control.ReadOnly = true;
    //           }


    //           control.DataSource = _DataSource;
    //           control.ValueMember = _valueMember;
    //           control.DisplayMember = _displayMember;

    //           _height = control.Height + 5;

    //           control.Font = Global.TextFont;
    //           control.FlatStyle = FlatStyle.Standard;

    //           control.SelectedIndexChanged += Control_SelectedIndexChanged;

    //           this.Control = control;

    //           return base.CreateControl();
    //       }

    //       private void Control_EnabledChanged(object sender, EventArgs e)
    //       {
    //           DevAgeComboBox textBox = sender as DevAgeComboBox;
    //           if (textBox != null)
    //               if (!textBox.Enabled)
    //               {
    //                  // 
    //                   //textBox.DropDownStyle = ComboBoxStyle.DropDownList;
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    //                   // U.SetComboBoxReadOnly(textBox, false);
    //               }
    //               else
    //               {
    //                  // textBox.DropDownStyle = ComboBoxStyle.DropDown;
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
    //                   // U.SetComboBoxReadOnly(textBox, true);
    //               }
    //       }

    //       public override DataGridViewColumn CreateColumn(string name,string headerText)
    //       {
    //           try
    //           {
    //               BindingList<ListDataItem> bList = new BindingList<ListDataItem>((List<ListDataItem>)_DataSource);
    //               Column = new DataGridViewComboBoxColumn()
    //               {
    //                   DataSource = bList, 
    //                   ValueMember = _valueMember,
    //                   DisplayMember = _displayMember,

    //                   Name = name,
    //                   DataPropertyName = name,
    //                   HeaderText = headerText,

    //                   DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
    //                   FlatStyle = FlatStyle.Flat,
    //                   DropDownWidth = _width,

    //               };

    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);

    //               BindingSource bSource = new BindingSource();
    //               bSource.DataSource = _DataSource;
    //               Column = new DataGridViewComboBoxColumn()
    //               {
    //                   DataSource = bSource,
    //                   ValueMember = _valueMember,
    //                   DisplayMember = _displayMember,

    //                   Name = name,
    //                   DataPropertyName = name,
    //                   HeaderText = headerText,

    //                   DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
    //                   FlatStyle = FlatStyle.Flat,
    //                   DropDownWidth = _width,

    //               };
    //           }

    //           //  



    //           //myDataGridViewComboBoxColumn cboColumn = new myDataGridViewComboBoxColumn();
    //           //cboColumn.myComboBox.DataSource = _DataSource;
    //           //cboColumn.myComboBox.DisplayMember = _displayMember;
    //           //cboColumn.myComboBox.ValueMember = _valueMember;
    //           //cboColumn.myComboBox.DropDownStyle = ComboBoxStyle.DropDown;

    //           //cboColumn.MappingName = name;
    //           //cboColumn.HeaderText = headerText;

    //           //Column = cboColumn;

    //           return base.CreateColumn(name,headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText,SourceGrid.DataGrid grid)
    //       {
    //           try
    //           {
    //               var _list = (IEnumerable<ListDataItem>)_DataSource;

    //               ICollection values = (ICollection)_list.Select(x => x.Text).ToList();

    //               this.DataGridEditor = new ComboBoxEditor((IEnumerable<ListDataItem>)_DataSource, !Enabled);
    //               this.DataGridEditor.StandardValues = values;
    //               this.DataGridEditor.StandardValuesExclusive = false;
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);

    //           }

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroComboListEditor : BaseMetroEditor
    //   {

    //       public MetroComboListEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {
    //           this.BindingName = "SelectedValue";// "SelectedValue;Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           MetroComboBox control = new MetroComboBox();

    //           control.DataBindings.Clear();
    //           control.DataSource = null;

    //           control.DataSource = _DataSource;
    //           control.ValueMember = _valueMember;
    //           control.DisplayMember = _displayMember;

    //           control.Enabled = Enabled;

    //           _height = control.Height + 8;

    //           control.DropDownStyle = ComboBoxStyle.DropDownList;

    //           control.SelectedIndexChanged += Control_SelectedIndexChanged;
    //           this.Control = control;

    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

    //           return base.CreateControl();
    //       }



    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {

    //           Column = new DataGridViewComboBoxColumn()
    //           {
    //               DataSource = _DataSource,
    //               ValueMember = _valueMember,
    //               DisplayMember = _displayMember,

    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,


    //               DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
    //               FlatStyle = FlatStyle.Flat,
    //               DropDownWidth = _width,


    //       };


    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           var _list = (IEnumerable<ListDataItem>)_DataSource;

    //           ICollection values = (ICollection)_list.Select(x => x.Text).ToList();

    //           this.DataGridEditor = new ComboBoxListEditor((IEnumerable<ListDataItem>)_DataSource, !Enabled);
    //           this.DataGridEditor.StandardValues = values;
    //           this.DataGridEditor.StandardValuesExclusive = false;



    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroDateEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroDateEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //          // MetroDateTime control = new MetroDateTime();
    //           System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

    //           control.Height += 5;

    //           control.Format = DateTimePickerFormat.Custom;
    //           control.CustomFormat = "dd MMM yyyy";
    //           control.MinDate = DateTime.Parse("1/1/1900");

    //           control.Font = Global.TextFont;
    //           control.Enabled = Enabled;

    //           this.Control = control;
    //           this.Validator = new RegExValidator(RegExpression);


    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };

    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "d",

    //           };


    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new DateEditor(!Enabled);


    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroTimeEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroTimeEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           // MetroDateTime control = new MetroDateTime();
    //           System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

    //           control.Height += 5;

    //           control.Format = DateTimePickerFormat.Custom;
    //           control.CustomFormat = "hh:mm:ss";
    //           control.MinDate = DateTime.Parse("1/1/1900");

    //           control.Font = Global.TextFont;
    //           control.Enabled = Enabled;

    //           this.Control = control;
    //           this.Validator = new RegExValidator(RegExpression);


    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };

    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "d",

    //           };


    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new TimeEditor(!Enabled);

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }
    //   public class MetroDateTimeEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroDateTimeEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           // MetroDateTime control = new MetroDateTime();
    //           System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

    //           control.Height += 5;

    //           control.Format = DateTimePickerFormat.Custom;
    //           control.CustomFormat = "dd MMM yyyy hh:mm:ss";
    //           control.MinDate = DateTime.Parse("1/1/1900");

    //           control.Font = Global.TextFont;
    //           control.Enabled = Enabled;

    //           this.Control = control;
    //           this.Validator = new RegExValidator(RegExpression);


    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };

    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "d",

    //           };


    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new DateTimeEditor(!Enabled);


    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }
    //   public class MetroCheckBoxEditor : BaseMetroEditor
    //   {
    //       public MetroCheckBoxEditor(int Width = 200) : base(Width)
    //       {
    //           this.BindingName = "Checked";
    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }
    //       public override Control CreateControl()
    //       {
    //           MetroCheckBox control = new MetroCheckBox();
    //           control.Enabled = Enabled;
    //           control.CheckAlign = ContentAlignment.MiddleRight;
    //           control.CheckedChanged += Control_CheckedChanged;
    //           control.BackColor = Color.Transparent;
    //           control.UseCustomBackColor = true;
    //           control.Text = "";
    //           control.FontSize = MetroCheckBoxSize.Medium;

    //           _height = control.Height + 8;

    //           this.Control = control;

    //           return base.CreateControl();
    //       }

    //       private void Control_CheckedChanged(object sender, EventArgs e)
    //       {
    //           try
    //           {
    //               MetroCheckBox cntrl = sender as MetroCheckBox;

    //               cntrl.DataBindings[0].WriteValue();

    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {

    //           Column = new DataGridViewCheckBoxColumn()
    //           {
    //               //DataSource = _DataSource,
    //               //ValueMember = _valueMember,
    //               //DisplayMember = _displayMember,

    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,


    //           };


    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new CheckBoxEditor(typeof(bool), !Enabled);

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroPercentageEditor : MetroTextBoxEditor
    //   {
    //       public MetroPercentageEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {
    //           this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //       }

    //       public override Control CreateControl()
    //       {
    //           RegExpression = RegExpressions.Decimals();
    //           this.ErrorMessage = string.Format("Only digits are allowed");

    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //               Format = "##.#0"
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new DecimalEditor(!Enabled);

    //           return base.CreateDataGridColumn(name, headerText,grid);
    //       }
    //   }
    //   public class MetroPasswordEditor : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroPasswordEditor(int Width = 200,int Height=0) : base(Width,Height)
    //       {
    //           this.BindingName = "Text";
    //       }

    //       public override Control CreateControl()
    //       {
    //           MetroTextBox control = new MetroTextBox();

    //           control.UseSystemPasswordChar = true;
    //           control.WaterMark =this.Watermark;

    //           _height = control.Height + 3;

    //           this.Control = control;

    //           this.Validator = new RegExValidator(RegExpression);

    //           return base.CreateControl();
    //       }
    //   }

    //   public class MetroButtonEditor : BaseMetroEditor
    //   {
    //       SourceGrid.Cells.Controllers.CustomEvents clickEvent= new SourceGrid.Cells.Controllers.CustomEvents();
    //       MetroButton control = new MetroButton();

    //       public string RegExpression = string.Empty;
    //       public MetroButtonEditor(int Width = 22, int Height = 0) : base(Width, Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {


    //           control.Font = Global.TextFont;

    //           control.Enabled = Enabled;

    //           control.TextAlign = ContentAlignment.MiddleCenter;

    //           //control.ForeColor = Color.Black;

    //           control.Height += 3;

    //           control.CausesValidation = true;

    //           this.Control = control;

    //           if (!string.IsNullOrEmpty(RegExpression))
    //               this.Validator = new RegExValidator(RegExpression);

    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewButtonColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };
    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           if (this.DataGridEditor == null)
    //               this.DataGridEditor = new StringEditor();

    //           var btn = new SourceGrid.Cells.RowHeader("");
    //           btn.Image = global::easiplan.app.Properties.Resources.save;
    //           btn.ToolTipText = "Click to open ";

    //           btn.AddController(clickEvent);

    //           var col = grid.Columns.Add("", "", btn);
    //           col.Width = 22;
    //           col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           col.MaximalWidth = 22;
    //           col.MinimalWidth = 22;

    //           return col;

    //           //return base.CreateDataGridColumn(name, headerText, grid);
    //       }

    //       public override BaseMetroEditor OnEditorClick(EventHandler eventHandler)
    //       {
    //           clickEvent.Click += eventHandler;

    //           control.Click += eventHandler;

    //           return base.OnEditorClick(eventHandler);
    //       }
    //   }

    //   public class MetroSAIDEditor : MetroTextBoxEditor
    //   {
    //       public MetroSAIDEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.Validator = new SAIDValidator();
    //           this.ErrorMessage = string.Format("Invalid South African Id");
    //       }

    //       public override Control CreateControl()
    //       {
    //           return base.CreateControl();
    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //            };

    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new StringEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }
    //   public class MetroTaxNoEditor : MetroTextBoxEditor
    //   {
    //       public MetroTaxNoEditor(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.Validator = new TaxNoValidator();
    //           this.ErrorMessage = string.Format("Invalid South African Tax Number");
    //       }

    //       public override Control CreateControl()
    //       {
    //           return base.CreateControl();
    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           FormatStyle = new DataGridViewCellStyle()
    //           {
    //               Alignment = DataGridViewContentAlignment.MiddleRight,
    //           };

    //           return base.CreateColumn(name, headerText);
    //       }

    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new StringEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }
    //   }

    //   public class MetroHtmlEditor : BaseMetroEditor
    //   {
    //       Type _placeHolderType = null;

    //       public string RegExpression = string.Empty;
    //       public MetroHtmlEditor(int Width = 200, int Height = 0,Type placeHolderType=null) : base(Width, Height)
    //       {
    //           this.BindingName = "InnerHtml;Text";

    //           _placeHolderType = placeHolderType;
    //       }
    //       public override Control CreateControl()
    //       {
    //           xHtmlEditor control = new xHtmlEditor();

    //           control.Font = Global.TextFont;

    //           if(_placeHolderType!=null)
    //               control.SetPlaceholders(_placeHolderType);

    //           this.Control = control;

    //           this.Control = control;
    //           this.Validator = new RegExValidator(RegExpression);

    //           return base.CreateControl();
    //       }

    //       private void Control_ReadOnlyChanged(object sender, EventArgs e)
    //       {
    //           xHtmlEditor textBox = sender as xHtmlEditor;
    //           if (textBox != null)
    //               if (textBox.Enabled)
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    //               }
    //               else
    //               {
    //                   textBox.BackColor = Color.FromKnownColor(KnownColor.White);
    //                   textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
    //               }
    //       }
    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,
    //           };
    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           this.DataGridEditor = new MultiLineEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }

    //   }
    //   #endregion

    //   public class MetroText : BaseMetroEditor
    //   {
    //       public string RegExpression = string.Empty;
    //       public MetroText(int Width = 200, int Height = 0) : base(Width, Height)
    //       {
    //           this.BindingName = "Text";
    //       }
    //       public override Control CreateControl()
    //       {
    //           Label control = new Label();
    //           control.Font = Font;
    //           control.BackColor = Color.Transparent;

    //           control.Height += 3;

    //           this.Control = control;

    //           if (!string.IsNullOrEmpty(RegExpression))
    //               this.Validator = new RegExValidator(RegExpression);

    //           return base.CreateControl();
    //       }

    //       public override DataGridViewColumn CreateColumn(string name, string headerText)
    //       {
    //           Column = new DataGridViewTextBoxColumn()
    //           {
    //               Name = name,
    //               DataPropertyName = name,
    //               HeaderText = headerText,

    //           };
    //           return base.CreateColumn(name, headerText);
    //       }
    //       public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
    //       {
    //           if (this.DataGridEditor == null)
    //               this.DataGridEditor = new StringEditor();

    //           return base.CreateDataGridColumn(name, headerText, grid);
    //       }

    //   }

    //   #region Validators
    //   public interface IMetroValidator
    //   {
    //       void Validate(object Value);

    //       bool IsValidated { get; set; }
    //   }
    //   public abstract class BaseMetroValidator : IMetroValidator
    //   {
    //       public bool IsValidated { get; set; }

    //       public virtual void Validate(object Value)
    //       {
    //           IsValidated = false;
    //       }
    //   }

    //   public class RegExValidator : BaseMetroValidator
    //   {
    //       string _RegEx;
    //       public RegExValidator(string RegEx)
    //       {
    //           _RegEx = RegEx;
    //       }
    //       public override void Validate(object Value)
    //       {
    //           if (!string.IsNullOrEmpty(_RegEx)
    //               && !string.IsNullOrEmpty(Value as string))
    //               if (!Regex.IsMatch(Value as string, _RegEx))
    //                   throw new Exception("Invalid");

    //           base.Validate(Value);
    //       }
    //   }

    //   public class SAIDValidator : BaseMetroValidator
    //   {
    //       public override void Validate(object Value)
    //       {
    //           SaIdValidator validator = new SaIdValidator();

    //           string idNo = Value as string;
    //           if (!string.IsNullOrEmpty(idNo))
    //               if (!validator.Validate(idNo))
    //                   throw new my.domain.lib.core.Domain.MyValidationException(string.Format("Id Number is not valid."));


    //           base.Validate(Value);
    //       }
    //   }

    //   public class TaxNoValidator : BaseMetroValidator
    //   {
    //       public override void Validate(object Value)
    //       {
    //           string taxNo = Value as string;

    //           if(!string.IsNullOrEmpty(taxNo))
    //               if (!F.IsSATaxNumber(taxNo))
    //               throw new my.domain.lib.core.Domain.MyValidationException(string.Format("Tax Number is not valid."));


    //           base.Validate(Value);
    //       }
    //   }
    //   #endregion

    //   public static class RegExpressions
    //   {
    //       public static string CharactersAscii(string Includes = "")
    //       {
    //           return "^[a-zA-Z" + Includes + "]+$";
    //       }
    //       public static string CharactersUnicode(string Includes = "")
    //       {
    //           return "^[a-zA-Z\\p{L}" + Includes + "]+$";
    //       }
    //       public static string Numbers(string Includes = "")
    //       {
    //           return "^[0-9" + Includes + "]+$";
    //       }
    //       public static string CharacterNumbersAscii(string Includes = "")
    //       {
    //           return "^[a-zA-Z0-9" + Includes + "]+$";
    //       }

    //       public static string Decimals(string Includes = ".")
    //       {
    //           return "^[0-9" + Includes + "]+$";
    //       }
    //       public static string Currency(string Includes = ".")
    //       {
    //           return "^R[0-9" + Includes + "]+$";
    //       }
    //   }

    //  public static class KryptoControlExt
    //   {

    //       public static KryptonGroupPanel Initialise<T>(this KryptonGroupPanel panel, T bindingSource, Action<ControlBuilder<T>> controlBuilder, ControlsLayout controlsLayout = ControlsLayout.Horizontal, int top = 5, int left = 5, int labelWidth = 120, PropertyChangedEventHandler PropertyChangedHandler = null, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnValidation, EventHandler ChangedEventHandler = null) where T : BaseEntity<int>
    //       {
    //           var builder = new ControlBuilder<T>();
    //           controlBuilder(builder);

    //           panel.Hide();

    //           panel.SuspendLayout();
    //           panel.AutoScroll = true;

    //           foreach (var control in builder)
    //           {

    //               var _cntrl = panel.Controls.Find("editor_" + control.Editor.GetType().Name + "_" + control.Name, true);

    //               if (_cntrl.Count() > 0)
    //               {
    //                   #region Update Control
    //                   try
    //                   {
    //                       var cntrl = _cntrl[0];
    //                       if (cntrl.GetType() == typeof(DevAgeTextBox))
    //                       {
    //                           DevAgeTextBox txt = cntrl as DevAgeTextBox;
    //                           txt.ReadOnly = !control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(DevAgeComboBox))
    //                       {
    //                           DevAgeComboBox txt = cntrl as DevAgeComboBox;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }
    //                       if (cntrl.GetType() == typeof(ExComboBox))
    //                       {
    //                           ExComboBox txt = cntrl as ExComboBox;
    //                           txt.ReadOnly = !control.Editor.Enabled;
    //                       }

    //                       if (cntrl.GetType() == typeof(DevAgeNumericUpDown))
    //                       {
    //                           DevAgeNumericUpDown txt = cntrl as DevAgeNumericUpDown;
    //                           // txt.ReadOnly = !control.Editor.Enabled;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }


    //                       if (cntrl.GetType() == typeof(MetroTrackBar))
    //                       {
    //                           MetroTrackBar txt = cntrl as MetroTrackBar;
    //                           // txt.ReadOnly = !control.Editor.Enabled;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }



    //                       if (cntrl.GetType() == typeof(MetroComboBox))
    //                       {
    //                           MetroComboBox txt = cntrl as MetroComboBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(MetroCheckBox))
    //                       {
    //                           MetroCheckBox txt = cntrl as MetroCheckBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(MetroTextBox))
    //                       {
    //                           MetroTextBox txt = cntrl as MetroTextBox;
    //                           txt.Enabled = control.Editor.Enabled;

    //                       }
    //                       if (cntrl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
    //                       {
    //                           System.Windows.Forms.DateTimePicker txt = cntrl as System.Windows.Forms.DateTimePicker;
    //                           txt.Enabled = control.Editor.Enabled;
    //                       }
    //                       //Rebind the dataSource
    //                       //BindingSource gridDataBinder = new BindingSource();

    //                       //if (PropertyChangedHandler != null)
    //                       //    // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
    //                       //    bindingSource.PropertyChanged += PropertyChangedHandler;

    //                       //cntrl.DataBindings.Clear();
    //                       //gridDataBinder.DataSource = bindingSource;
    //                       //string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
    //                       //foreach (string _binding in _bindings.ToList())
    //                       //{
    //                       //    Binding b = new Binding(_binding, gridDataBinder, control.Name, true, control.Editor.dataSourceUpdateMode, string.Empty, cntrl.Tag as string);
    //                       //    b.Format += new ConvertEventHandler(formatHandler);
    //                       //    b.Parse += new ConvertEventHandler(parseHandler);
    //                       //    cntrl.DataBindings.Add(b);
    //                       //}
    //                   }
    //                   catch (Exception x)
    //                   {
    //                       Program.Logger.Error(x);
    //                   }
    //                   #endregion
    //               }
    //               else
    //               {
    //                   #region Add Label
    //                   int lableHeight = 0;
    //                   if (labelWidth > 0)
    //                   {
    //                       MetroLabel label = new MetroLabel() { Text = control.DisplayName };
    //                       label.Left = left;
    //                       label.Top = top;
    //                       label.Width = labelWidth;
    //                       label.BackColor = Color.Transparent;
    //                       label.UseCustomBackColor = true;
    //                       label.Font = Global.LableFont;
    //                       label.FontSize = MetroLabelSize.Medium;
    //                       //  label.FontWeight = MetroLabelWeight.Bold;

    //                       lableHeight += label.Height + 5;

    //                       label.Name = "lbl_" + control.Name;
    //                       panel.Controls.Add(label);
    //                   }
    //                   #endregion

    //                   #region Add Editor
    //                   Control cntrlEditor = null;

    //                   if (control.Editor == null)
    //                       control.Editor = new MetroStringEditor() { Enabled = false }; //No editing

    //                   //Get Required Attribute
    //                   var _reqAttr = typeof(T).GetProperty(control.Name).CustomAttributes.Where(x => x.AttributeType == typeof(RequiredAttribute));
    //                   if (_reqAttr.Count() > 0)
    //                   {
    //                       var _msg = _reqAttr.FirstOrDefault().NamedArguments[0].TypedValue.Value as string;
    //                       control.Editor.Watermark = _msg;
    //                   }

    //                   //Create editor control
    //                   cntrlEditor = control.Editor.CreateControl();

    //                   if (controlsLayout == ControlsLayout.Vertical)
    //                   {
    //                       cntrlEditor.Left = left;//padding
    //                       cntrlEditor.Top = top + lableHeight;
    //                   }
    //                   else
    //                   {
    //                       cntrlEditor.Left = left + labelWidth + 5;//padding
    //                       cntrlEditor.Top = top;
    //                   }

    //                   cntrlEditor.Name = "editor_" + control.Editor.GetType().Name + "_" + control.Name;


    //                   #endregion

    //                   #region Bind Control

    //                   BindingSource dataSource = new BindingSource();
    //                   dataSource.DataSource = bindingSource;
    //                   if (PropertyChangedHandler != null)
    //                   {
    //                       dataSource.CurrentItemChanged += ChangedEventHandler;
    //                       // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
    //                       bindingSource.PropertyChanged += PropertyChangedHandler;
    //                   }

    //                   cntrlEditor.DataBindings.Clear();

    //                   string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
    //                   foreach (string binding in _bindings.ToList())
    //                   {
    //                       Binding b = new Binding(binding, dataSource, control.Name, true, control.Editor.dataSourceUpdateMode, string.Empty, cntrlEditor.Tag as string);
    //                       b.Format += new ConvertEventHandler(formatHandler);
    //                       b.Parse += new ConvertEventHandler(parseHandler);

    //                       cntrlEditor.DataBindings.Add(b);
    //                   }

    //                   if (cntrlEditor.GetType() == typeof(MetroCheckBox))
    //                   {
    //                       cntrlEditor.Text = "";// control.DisplayName;
    //                       cntrlEditor.Left = left;
    //                   }
    //                   #endregion

    //                   top = cntrlEditor.Top + control.Editor.Size.Height + 2; //padding

    //                   panel.Controls.Add(cntrlEditor);
    //               }

    //           }

    //           panel.ResumeLayout();

    //           panel.Show();

    //           return panel;
    //       }


    //       public static KryptonGroupPanel Format(this KryptonGroupPanel control, string text = "", Image image = null, int padding = 5, int height = 0)
    //       {
    //           control.Text = text;
    //           control.Padding = new Padding(padding);

    //           control.BackColor = Color.White;
    //           //control.UseCustomBackColor = true;

    //           // control.BorderStyle = BorderStyle.FixedSingle;

    //           if (height > 0)
    //           {
    //               control.Height = height;
    //               //control.VerticalScrollbar = true;
    //               control.AutoScroll = true;
    //               //control.VerticalScrollbarBarColor = true;
    //           }

    //           return control;
    //       }

    //       private static void formatHandler(object sender, ConvertEventArgs e)
    //       {
    //           //put code and breakpoint here to inspect e.Value
    //       }

    //       private static void parseHandler(object sender, ConvertEventArgs e)
    //       {
    //           //put code and breakpoint here to inspect e.Value
    //       }
    //   }
    #endregion

    public static class MetroControlExt
    {
        #region MetroPanel Initialise
        public static MetroPanel Initialise<T>(this MetroPanel panel, T bindingSource, Action<ControlBuilder<T>> controlBuilder, ControlsLayout controlsLayout = ControlsLayout.Horizontal, int top = 5, int left = 5, int labelWidth = 120, PropertyChangedEventHandler PropertyChangedHandler = null, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnValidation) where T : BaseEntity<int>
        {
            var builder = new ControlBuilder<T>();
            controlBuilder(builder);

            panel.SuspendLayout();
            panel.AutoScroll = true;

            foreach (var control in builder)
            {

                var _cntrl = panel.Controls.Find("editor_" + control.Editor.GetType().Name + "_" + control.Name, true);

                if (_cntrl.Count() > 0)
                {
                    try
                    {
                        #region Update Control
                        var cntrl = _cntrl[0];
                        if (cntrl.GetType() == typeof(DevAgeTextBox))
                        {
                            DevAgeTextBox txt = cntrl as DevAgeTextBox;
                            txt.ReadOnly = !control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(DevAgeComboBox))
                        {
                            DevAgeComboBox txt = cntrl as DevAgeComboBox;
                            txt.Enabled = control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(ExComboBox))
                        {
                            ExComboBox txt = cntrl as ExComboBox;
                            txt.ReadOnly = !control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(DevAgeNumericUpDown))
                        {
                            DevAgeNumericUpDown txt = cntrl as DevAgeNumericUpDown;
                            // txt.ReadOnly = !control.Editor.Enabled;
                            txt.Enabled = control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(MetroTrackBar))
                        {
                            MetroTrackBar txt = cntrl as MetroTrackBar;
                            // txt.ReadOnly = !control.Editor.Enabled;
                            txt.Enabled = control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(MetroComboBox))
                        {
                            MetroComboBox txt = cntrl as MetroComboBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(MetroCheckBox))
                        {
                            MetroCheckBox txt = cntrl as MetroCheckBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(MetroTextBox))
                        {
                            MetroTextBox txt = cntrl as MetroTextBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
                        {
                            System.Windows.Forms.DateTimePicker txt = cntrl as System.Windows.Forms.DateTimePicker;
                            txt.Enabled = control.Editor.Enabled;
                        }

                        #endregion

                        //# region Rebind the dataSource
                        //BindingSource gridDataBinder = new BindingSource();
                        //gridDataBinder.DataSource = bindingSource;
                        //if (PropertyChangedHandler != null)
                        //   // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
                        //   bindingSource.PropertyChanged += PropertyChangedHandler;

                        //cntrl.DataBindings.Clear();

                        //string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
                        //foreach (string _binding in _bindings.ToList())
                        //    cntrl.DataBindings.Add(_binding, gridDataBinder, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string);//OnPropertyChanged

                        ////YJ 2021-11-05 Update Databinding Update mode to the control editors datasourceupdatemode
                        //#endregion
                    }
                    catch (Exception x)
                    {

                    }

                }
                else
                {
                    #region Add Label
                    int lableHeight = 0;
                    if (labelWidth > 0)
                    {
                        MetroLabel label = new MetroLabel() { Text = control.DisplayName };
                        label.Left = left;
                        label.Top = top;
                        label.Width = labelWidth;
                        label.BackColor = Color.Transparent;
                        label.UseCustomBackColor = true;
                        label.Font = Global.LableFont;
                        label.FontSize = MetroLabelSize.Medium;
                        //  label.FontWeight = MetroLabelWeight.Bold;

                        lableHeight += label.Height + 5;

                        label.Name = "lbl_" + control.Name;
                        panel.Controls.Add(label);
                    }
                    #endregion

                    #region Add Control
                    Control cntrl = null;

                    if (control.Editor == null)
                        control.Editor = new MetroStringEditor() { Enabled = false }; //No editing

                    //Get Required Attribute
                    var _reqAttr = typeof(T).GetProperty(control.Name).CustomAttributes.Where(x => x.AttributeType == typeof(RequiredAttribute));
                    if (_reqAttr != null)
                    {
                        if (_reqAttr.Count() > 0)
                        {
                            var _msg = _reqAttr.FirstOrDefault().NamedArguments[0].TypedValue.Value as string;
                            control.Editor.Watermark = _msg;
                        }
                    }

                    //Create control
                    cntrl = control.Editor.CreateControl();

                    if (controlsLayout == ControlsLayout.Vertical && control.Editor.GetType() != typeof(MetroText))
                    {
                        cntrl.Left = left;//padding
                        cntrl.Top = top + lableHeight;
                    }
                    else
                    {
                        cntrl.Left = left + labelWidth + 5;//padding
                        cntrl.Top = top;
                    }

                    cntrl.Name = "editor_" + control.Editor.GetType().Name + "_" + control.Name;
                    panel.Controls.Add(cntrl);

                    #endregion

                    #region Bind Control

                    BindingSource dataSource = new BindingSource();
                    dataSource.DataSource = bindingSource;


                    cntrl.DataBindings.Clear();

                    string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
                    foreach (string binding in _bindings.ToList())
                        //cntrl.DataBindings.Add(binding, binding, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string);//OnPropertyChanged
                        cntrl.DataBindings.Add(new Binding(binding, dataSource, control.Name, true, control.Editor.dataSourceUpdateMode, null, cntrl.Tag as string));

                    //YJ 2021-11-05 Update Databinding Update mode to the control editors datasourceupdatemode

                    if (cntrl.GetType() == typeof(MetroCheckBox))
                    {
                        cntrl.Text = "";// control.DisplayName;
                        cntrl.Left = left;
                    }
                    #endregion

                    top = cntrl.Top + control.Editor.Size.Height + 2; //padding
                }

            }

            if (PropertyChangedHandler != null)
            {
                // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
                bindingSource.PropertyChanged -= PropertyChangedHandler;
                bindingSource.PropertyChanged += PropertyChangedHandler;
            }

            panel.ResumeLayout();

            return panel;
        }
        #endregion

        #region MetroGrid Initialise
        public static MetroGrid Initialise<T>(this MetroGrid grid, IList<T> bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool HasDetails = false, DataGridViewCellEventHandler dgvCellEventHandler = null, DataGridViewCellMouseEventHandler dgvRowHeaderEventHandler = null, EventHandler PropertyChangedHandler = null, MetroContextMenu ContextMenu = null) where T : class
        {
            var builder = new ControlBuilder<T>();
            controlBuilder(builder);

            grid.SuspendLayout();

            grid.DataError += Grid_DataError;
            grid.EditingControlShowing += Grid_EditingControlShowing;
            grid.CellValidating += Grid_CellValidating;
            grid.CellParsing += Grid_CellParsing;
            grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;

            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            if (HasDetails)
            {
                //build a Toggle column
                DataGridViewImageColumn dgvShowHideCol = new DataGridViewImageColumn()
                {
                    Name = "",
                    Width = 25,
                    Resizable = DataGridViewTriState.False,
                    Frozen = true,
                    Image = global::easiplan.app.Properties.Resources.expand,
                    Tag = "Details"
                };
                grid.Columns.Add(dgvShowHideCol);
            }

            if (dgvCellEventHandler != null)
                grid.CellContentClick += dgvCellEventHandler;

            if (dgvRowHeaderEventHandler != null)
                grid.RowHeaderMouseClick += dgvRowHeaderEventHandler;

            foreach (var control in builder)
            {
                if (control.Editor == null)
                {
                    DataGridViewColumn dgvCol = new DataGridViewTextBoxColumn()
                    {
                        Name = control.Name,
                        DataPropertyName = control.Name,
                        HeaderText = control.DisplayName,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    };

                    grid.Columns.Add(dgvCol);
                }
                else
                {
                    //Add column to the grid
                    grid.Columns.Add(control.Editor.CreateColumn(control.Name, control.DisplayName));
                }


            }


            if (ContextMenu != null)
            {
                //build a ContextMenu column
                DataGridViewImageColumn dgvContextMenu = new DataGridViewImageColumn()
                {
                    Name = "",
                    Width = 25,
                    Resizable = DataGridViewTriState.False,
                    Image = global::easiplan.app.Properties.Resources.save,
                    Description = "Menu",
                    Tag = "ContextMenu"

                };
                grid.Columns.Add(dgvContextMenu);
            }


            grid.ResumeLayout();

            BindingSource gridDataBinder = new BindingSource();
            gridDataBinder.DataSource = bindingSource;
            gridDataBinder.AllowNew = true;
            grid.DataSource = gridDataBinder;

            if (PropertyChangedHandler != null)
                gridDataBinder.CurrentItemChanged += PropertyChangedHandler;

            //var data = new BindingList<T>(bindingSource);
            //grid.DataSource = data;


            return grid;
        }
        public static MetroGrid Initialise<T>(this MetroGrid grid, T bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool HasDetails = false, DataGridViewCellEventHandler dgvCellEventHandler = null, EventHandler PropertyChangedHandler = null, MetroContextMenu ContextMenu = null) where T : class
        {
            var builder = new ControlBuilder<T>();
            controlBuilder(builder);

            grid.SuspendLayout();

            grid.DataError += Grid_DataError;
            grid.EditingControlShowing += Grid_EditingControlShowing;
            grid.CellValidating += Grid_CellValidating;
            grid.CellParsing += Grid_CellParsing;

            grid.AutoGenerateColumns = false;

            grid.Columns.Clear();

            if (HasDetails)
            {
                //build a Toggle column
                DataGridViewImageColumn dgvShowHideCol = new DataGridViewImageColumn()
                {
                    Name = "",
                    Width = 25,
                    Resizable = DataGridViewTriState.False,
                    Frozen = true,
                    Image = global::easiplan.app.Properties.Resources.expand,
                    Tag = "Details"
                };
                grid.Columns.Add(dgvShowHideCol);
            }


            if (dgvCellEventHandler != null)
                grid.CellContentClick += dgvCellEventHandler;

            foreach (var control in builder)
            {
                if (control.Editor == null)
                {
                    DataGridViewColumn dgvCol = new DataGridViewTextBoxColumn()
                    {
                        Name = control.Name,
                        DataPropertyName = control.Name,
                        HeaderText = control.DisplayName,
                        //AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    };

                    grid.Columns.Add(dgvCol);
                }
                else
                {
                    //Add column to the grid
                    grid.Columns.Add(control.Editor.CreateColumn(control.Name, control.DisplayName));
                }
            }


            if (ContextMenu != null)
            {
                //build a ContextMenu column
                DataGridViewImageColumn dgvContextMenu = new DataGridViewImageColumn()
                {
                    Name = "",
                    Width = 25,
                    Resizable = DataGridViewTriState.False,
                    Image = global::easiplan.app.Properties.Resources.save,
                    Description = "Menu",
                    Tag = "ContextMenu"

                };
                grid.Columns.Add(dgvContextMenu);
            }

            grid.ResumeLayout();

            BindingSource gridDataBinder = new BindingSource();
            gridDataBinder.DataSource = bindingSource;
            gridDataBinder.AllowNew = true;
            grid.DataSource = gridDataBinder;

            if (PropertyChangedHandler != null)
                gridDataBinder.CurrentItemChanged += PropertyChangedHandler;



            //IList<T> _list = new List<T>();
            //_list.Add(bindingSource);

            //var data = new BindingList<T>(_list);
            //grid.DataSource = data;


            return grid;
        }
        public static MetroGrid ResetBindingsExt(this MetroGrid grid)
        {
            BindingSource gridDataBinder = grid.DataSource as BindingSource;
            gridDataBinder.ResetBindings(false);

            return grid;
        }
        public static bool ToggleGridShowHide(this MetroGrid grid, int colIndex, int rowIndex, bool ForceExpand = false)
        {
            try
            {
                DataGridViewImageColumn col = grid.Columns[colIndex] as DataGridViewImageColumn;

                if (col != null)
                {
                    if ("toggle" == grid.Rows[rowIndex].Cells[colIndex].Tag as string || ForceExpand)
                    {
                        grid.Rows[rowIndex].Cells[colIndex].Value = global::easiplan.app.Properties.Resources.expand;
                        grid.Rows[rowIndex].Cells[colIndex].Tag = "expand";
                        return false;
                    }
                    else
                    {
                        grid.Rows[rowIndex].Cells[colIndex].Value = global::easiplan.app.Properties.Resources.toggle;
                        grid.Rows[rowIndex].Cells[colIndex].Tag = "toggle";
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
        #endregion

        #region MetroGrid EventHandlers
        private static void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MetroGrid grid = sender as MetroGrid;

            switch (e.Context)
            {
                case DataGridViewDataErrorContexts.Parsing:
                case DataGridViewDataErrorContexts.Commit:
                case DataGridViewDataErrorContexts.CurrentCellChange:
                    grid.CurrentCell.ErrorText = "parsing error";
                    grid.CurrentCell.Style.ForeColor = Color.Red;
                    break;
            }

            if (e.Exception.GetType() == typeof(ConstraintException))
            {
                grid.CurrentCell.ErrorText = "constraint error";
                e.ThrowException = false;
            }
            if (e.Exception.GetType() == typeof(FormatException))
            {
                grid.CurrentCell.ErrorText = "format error";
                e.ThrowException = false;
            }
            //if (e.Exception.GetType() == typeof(ArgumentException))
            //{
            //    grid.CurrentCell.ErrorText = "data error";
            //    e.ThrowException = false;
            //}
            // throw new NotImplementedException();
        }

        private static void Grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private static void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                MetroGrid grid = sender as MetroGrid;
                grid.CurrentCell.ErrorText = "";

                DataGridViewComboBoxCell cCell = grid.CurrentCell as DataGridViewComboBoxCell;
                if (cCell != null)
                {
                    BindingList<ListDataItem> bList = cCell.DataSource as BindingList<ListDataItem>;
                    var selItem = bList.Where(x => x.Text.Trim().ToLower() == e.FormattedValue.ToString().Trim().ToLower()).FirstOrDefault();
                    if (selItem == null)
                    {
                        //   bList.RaiseListChangedEvents = true;
                        bList.Add(new Finx.App.ListDataItem() { ListType = ListDataItemType.DependentType, Text = e.FormattedValue as string, Value = e.FormattedValue as string });

                        cCell.Value = e.FormattedValue;

                    }
                }

            }
            catch (Exception x)
            { }
        }

        private static void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            MetroGrid grid = sender as MetroGrid;
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                DataGridViewComboBoxEditingControl eC = e.Control as DataGridViewComboBoxEditingControl;

                System.Windows.Forms.ComboBox combo = e.Control as System.Windows.Forms.ComboBox;
                if (combo != null)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);

                    combo.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        private static void Grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private static void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            var item = cb.SelectedValue;
            if (item != null)
            {


            }
        }

        //public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, IList<T> dataSource, Action<ControlBuilder<T>> controlBuilder, ListChangedEventHandler handler = null, int top = 5, int left = 5) where T : class
        //{

        //    grid.SuspendLayout();

        //    grid.DataSource = new DevAge.ComponentModel.BoundList<T>(dataSource);

        //    if (handler != null)
        //        grid.DataSource.ListChanged += handler;

        //    var builder = new ControlBuilder<T>();
        //    controlBuilder(builder);

        //    grid.Columns.Clear();

        //    foreach (var column in builder)
        //    {

        //        //Control cntrlEditor = null;
        //        //if (control.Editor == null)
        //        //    control.Editor = new MetroStringEditor() { ReadOnly = true }; //No editing



        //        //Add column to the grid
        //        //DataGridViewColumn dgvCol = new DataGridViewColumn()
        //        //{
        //        //    Name = control.Name,
        //        //    //DataPropertyName = "Value",
        //        //    HeaderText = control.DisplayName,

        //        //};
        //        //grid.Columns.Add(dgvCol);

        //        grid.Columns.Add(column.Name, column.DisplayName, new StringEditor());
        //    }



        //    grid.ResumeLayout();

        //    return grid;
        //}

        #endregion

        #region MetroControls Format
        public static MetroForm Format(this MetroForm control, string SubTitle = "")
        {
            try
            {


                #region Form Format
                control.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
                control.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;

                control.Text = string.Empty;
                control.SubTitle = string.Format("{0}", SubTitle);


                control.DisplayHeader = false;
                #endregion

            }
            catch (Exception x)
            {
            }

            return control;

        }
        public static MetroTextBox Format(this MetroTextBox control)
        {
            try
            {
                control.Font = new Font(FontFamily.GenericSansSerif, 12);

            }
            catch (Exception x)
            {
            }

            return control;

        }
        public static MetroTabControl Format(this MetroTabControl control, ImageList images = null)
        {

            if (images != null)
                control.ImageList = images;



            return control;
        }
        public static TabPage Format(this TabPage control, string text = "", Image image = null, int padding = 2, EventHandler ClickEventHandler = null)
        {
            control.Text = text;
            control.Padding = new Padding(padding);
            control.ImageIndex = 1;

            control.BackColor = Color.White;
            control.HorizontalScroll.Visible = false;
            control.VerticalScroll.Visible = false;


            if (ClickEventHandler != null)
            {
                control.Click += ClickEventHandler;

                control.GotFocus += ClickEventHandler;
            }

            return control;
        }
        public static MetroPanel Format(this MetroPanel control, string text = "", Image image = null, int padding = 5, int height = 0)
        {
            control.SuspendLayout();

            control.Text = text;
            control.Padding = new Padding(padding);

            control.BackColor = Color.White;
            control.UseCustomBackColor = true;

            // control.BorderStyle = BorderStyle.FixedSingle;

            if (height > 0)
            {
                control.Height = height;
                control.VerticalScrollbar = true;
                control.AutoScroll = true;
                control.VerticalScrollbarBarColor = true;
            }
            control.ResumeLayout();

            return control;
        }
        public static MetroGrid Format(this MetroGrid control, bool ReadOnly, GridFormats format = GridFormats.Default)
        {
            try
            {
                switch (format)
                {
                    case GridFormats.Default:
                        control.BorderStyle = BorderStyle.FixedSingle;

                        control.UseCustomBackColor = true;
                        control.UseCustomForeColor = true;

                        control.ScrollBars = ScrollBars.Both;

                        control.AllowUserToAddRows = !ReadOnly;
                        control.AllowUserToDeleteRows = false;

                        //control.Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Pixel);
                        control.Font = MetroFonts.Default(13.5f);

                        control.BackgroundColor = Color.White;
                        control.ForeColor = Color.Black;

                        //control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        //control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.DefaultBold(13f);
                        control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        //control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        //control.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
                        //control.RowHeadersDefaultCellStyle.Font = MetroFonts.Default(12f);

                        control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                        control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                        control.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                        control.BorderStyle = BorderStyle.FixedSingle;
                        control.GridColor = Color.WhiteSmoke;

                        //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        foreach (DataGridViewColumn col in control.Columns)
                        {
                            if (!col.Frozen)
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        control.Refresh();

                        break;
                    case GridFormats.Format1:
                        control.Font = MetroFonts.Default(13f);

                        control.AllowUserToAddRows = !ReadOnly;
                        control.AllowUserToDeleteRows = false;

                        control.BackgroundColor = Color.White;
                        control.ForeColor = Color.Black;

                        control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
                        control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        control.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                        control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                        //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        foreach (DataGridViewColumn col in control.Columns)
                        {
                            if (!col.Frozen)
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        control.RowHeadersVisible = false;
                        control.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        control.ScrollBars = ScrollBars.None;

                        control.Refresh();
                        break;
                    case GridFormats.Format11:
                        control.Font = MetroFonts.Default(13f);

                        control.AllowUserToAddRows = !ReadOnly;
                        control.AllowUserToDeleteRows = false;

                        control.BackgroundColor = Color.White;
                        control.ForeColor = Color.Black;

                        control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
                        control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        control.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                        control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                        //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        foreach (DataGridViewColumn col in control.Columns)
                        {
                            if (!col.Frozen)
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        control.RowHeadersVisible = true;
                        control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        control.ScrollBars = ScrollBars.Both;

                        control.Refresh();
                        break;
                    case GridFormats.Format2:
                        control.Font = MetroFonts.Default(14f);

                        control.AllowUserToAddRows = !ReadOnly;
                        control.AllowUserToDeleteRows = false;

                        control.BackgroundColor = Color.White;
                        control.ForeColor = Color.Black;

                        control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
                        control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.RowHeadersDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;

                        control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                        control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                        //control.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        //control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        foreach (DataGridViewColumn col in control.Columns)
                        {
                            if (!col.Frozen)
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        control.RowHeadersVisible = true;
                        control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        control.Refresh();
                        break;
                    case GridFormats.Format3:
                        control.Font = MetroFonts.Default(14f);

                        control.AllowUserToAddRows = !ReadOnly;
                        control.AllowUserToDeleteRows = false;

                        control.BackgroundColor = Color.White;
                        control.ForeColor = Color.Black;

                        control.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        control.ColumnHeadersDefaultCellStyle.Font = MetroFonts.Default(14f);
                        control.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        control.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        control.RowHeadersDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;

                        control.RowsDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                        control.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                        control.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        control.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        foreach (DataGridViewColumn col in control.Columns)
                        {
                            if (!col.Frozen)
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        control.RowHeadersVisible = true;
                        control.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        control.Refresh();
                        break;
                    default:
                        break;
                };




            }
            catch (Exception x)
            {
            }

            return control;

        }
        public static TabPage Format(this TabPage control, int PageIndex, string text = "", Image image = null, int padding = 2, EventHandler ClickEventHandler = null)
        {
            control = control.Format(text, image, padding, ClickEventHandler);

            control.Tag = PageIndex;

            return control;
        }
        #endregion

        #region SourceGrid.DataGrid Initialise

        public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, IList<T> bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null, EventHandler ItemEditEventHandler = null) where T : class
        {

            grid.SuspendLayout();
            grid.Columns.Clear();

            grid.SelectionMode = GridSelectionMode.Row;
            grid.AutoSize = false;
            grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            grid.AutoStretchColumnsToFitWidth = true;

            //grid.DataError += Grid_DataError;
            //grid.EditingControlShowing += Grid_EditingControlShowing;
            //grid.CellValidating += Grid_CellValidating;
            //grid.CellParsing += Grid_CellParsing;
            //grid.Validated += Grid_CurrentCellDirtyStateChanged;           


            grid.DeleteRowsWithDeleteKey = false;
            grid.CancelEditingWithEscapeKey = true;
            //grid.EndEditingRowOnValidate = true;




            grid.FixedRows = 1;
            grid.FixedColumns = 1;

            grid.EnableSort = false;

            #region Row Header
            //Event Controller
            SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            if (RowHeaderSelectEventHandler != null)
            {
                RowHeaderSelectEvent.Click -= RowHeaderSelectEventHandler;
                RowHeaderSelectEvent.Click += RowHeaderSelectEventHandler;
            }
            else
            {
                RowHeaderSelectEvent.Click -= RowHeaderSelectEvent_Click;
                RowHeaderSelectEvent.Click += RowHeaderSelectEvent_Click;
            }


            var rowHeader = new SourceGrid.Cells.RowHeader("");
            if (RowHeaderSelectEventHandler != null)
            {
                rowHeader.Image = global::easiplan.app.Properties.Resources.arrow_r_128.ToBitmap();
                rowHeader.ToolTipText = "Click to show";
            }
            rowHeader.AddController(RowHeaderSelectEvent);

            var colHeader = grid.Columns.Add("", "", rowHeader);
            colHeader.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            colHeader.MaximalWidth = 25;
            colHeader.Width = 25;

            //Row Header
            // var dgCol = DataGridColumn.CreateRowHeader(grid);
            // dgCol.DataCell.AddController(RowHeaderSelectEvent);

            // grid.Columns.Insert(0,dgCol);
            //grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            //grid.Columns[0].MaximalWidth = 25;
            //grid.Columns[0].Width = 25;
            //var RowHeaderEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string))
            //{ EditableMode = SourceGrid.EditableMode.None, EnableEdit = false };
            //RowHeaderEditor.Control.Cursor = Cursors.Hand;
            //grid.Columns[0].DataCell.Editor = RowHeaderEditor;

            //Delete Controller
            //grid.Controller.AddController(new DataGridCellController());
            //grid.Controller.AddController(new KeyDeleteController());

            #endregion

            #region Row Click
            grid.Selection.FocusRowEntered += RowSelectEventHandler;
            grid.Selection.FocusStyle = FocusStyle.FocusFirstCellOnEnter;
            grid.MouseClick += SourceGrid_MouseClick;
            #endregion

            #region Build Grid Columns
            var builder = new ControlBuilder<T>();
            controlBuilder(builder);

            foreach (var control in builder)
            {
                DataGridColumn dataGridColumn = null;
                if (control.Editor == null)
                {
                    dataGridColumn = grid.Columns.Add(control.Name, control.DisplayName, typeof(String)); //default editor
                }
                else
                {
                    dataGridColumn = control.Editor.CreateDataGridColumn(control.Name, control.DisplayName, grid);

                    if (ReadOnly)
                        control.Editor.DataGridEditor.EditableMode = EditableMode.None;

                }
                // Add a custom view for the cell e.g
                // dataGridColumn.DataCell.View = new InstructionsStatusView();

                if (control.Editor._width != 200)
                    dataGridColumn.MinimalWidth = control.Editor._width;

                if (!string.IsNullOrEmpty(control.ToolTip))
                {

                    DataGridColumnTooltip dgct = new DataGridColumnTooltip(control.ToolTip);
                    dataGridColumn.HeaderCell.AddController(dgct);

                    dataGridColumn.HeaderCell.View = new ColumnInfoView();
                }

                dataGridColumn.HeaderCell.View.WordWrap = true;
            }
            #endregion

            #region ContextMenuButton
            if (ContextMenu != null)
            {

                DataGridContextMenu dgct = ContextMenu as DataGridContextMenu;

                if (dgct.Visible)
                {
                    var btn = new SourceGrid.Cells.RowHeader("");
                    btn.Image = global::easiplan.app.Properties.Resources.dots_3_1281;
                    btn.ToolTipText = "Click to show";

                    btn.AddController(ContextMenu);

                    var col = grid.Columns.Add("", "", btn);
                    col.Width = 22;
                    col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                    col.MaximalWidth = 22;
                    col.MinimalWidth = 22;
                }

                //override the default contextmenu property
                // grid.ContextMenu = dgct.ContextMenu;
                grid.Tag = dgct.ContextMenu;


            }
            #endregion

            #region  Edit Button

            if (!ReadOnly && AllowEdit)
            {
                //Add a delete button
                SourceGrid.Cells.Controllers.CustomEvents editEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                if (ItemEditEventHandler != null)
                {
                    editEvent.Click -= ItemEditEventHandler;
                    editEvent.Click += ItemEditEventHandler;
                }


                var btnEdit = new SourceGrid.Cells.RowHeader("");
                btnEdit.Image = global::easiplan.app.Properties.Resources.dots_3_128.ToBitmap();
                btnEdit.ToolTipText = "Click to delete row";
                btnEdit.AddController(editEvent);

                var col = grid.Columns.Add("", "", btnEdit);
                col.Width = 22;
                col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                col.MaximalWidth = 22;
                col.MinimalWidth = 22;
            }
            #endregion

            #region  Delete Button
            grid.DeleteRowsWithDeleteKey = AllowDelete;// !ReadOnly;
            if (!ReadOnly && AllowDelete)
            {
                //Add a delete button
                SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();

                if (ItemDeleteEventHandler == null)
                {
                    deleteEvent.Click -= DeleteEvent_Click;
                    deleteEvent.Click += DeleteEvent_Click;
                }
                else
                {
                    deleteEvent.Click -= ItemDeleteEventHandler;
                    deleteEvent.Click += ItemDeleteEventHandler;
                }


                var btn = new SourceGrid.Cells.RowHeader("");
                btn.Image = global::easiplan.app.Properties.Resources.delete;
                btn.ToolTipText = "Click to delete row";
                btn.AddController(deleteEvent);

                var col = grid.Columns.Add("", "", btn);
                col.Width = 22;
                col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                col.MaximalWidth = 22;
                col.MinimalWidth = 22;
            }
            #endregion

            //Reset the datasource
            grid.DataBindings.Clear();
            grid.DataSource = null;

            DevAge.ComponentModel.BoundList<T> boundList = new DevAge.ComponentModel.BoundList<T>(bindingSource);

            boundList.ItemDeleted -= ItemDeletedEventHandler;
            boundList.ItemDeleted += ItemDeletedEventHandler;

            if (PropertyChangedHandler != null) boundList.ListChanged -= PropertyChangedHandler;
            if (PropertyChangedHandler != null) boundList.ListChanged += PropertyChangedHandler;

            //if (ListChangedEventHandler != null) boundList.ListChanged -= ListChangedEventHandler;// PropertyChangedHandler;
            //if (ListChangedEventHandler != null) boundList.ListChanged += ListChangedEventHandler;// PropertyChangedHandler;


            grid.DataSource = boundList;

            if (grid.DataSource != null)
                grid.DataSource.AllowNew = AllowAddNew;

            grid.ResumeLayout();

            grid.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;


            grid.Refresh();

            return grid;
        }
        public static SourceGrid.DataGrid Initialise<T>(this SourceGrid.DataGrid grid, T bindingSource, Action<ControlBuilder<T>> controlBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null) where T : class
        {
            IList<T> _bindingSource = new List<T>();
            _bindingSource.Add(bindingSource);

            return grid.Initialise<T>(_bindingSource, controlBuilder, top, left, AllowDelete, AllowAddNew, AllowEdit, ReadOnly, PropertyChangedHandler, ItemDeleteEventHandler, ItemDeletedEventHandler, RowHeaderSelectEventHandler, RowSelectEventHandler, ListChangedEventHandler, ContextMenu);
        }
        public static void InitialiseRectangle(this SourceGrid.DataGrid grid, CellContext context)
        {

            context.Grid.Selection.SelectRow(context.Position.Row, true);

            var _cntrl = context.Grid.Controls.Find(grid.Name, true);
            if (_cntrl.Count() > 0)
            {
                context.Grid.Controls.RemoveByKey(grid.Name);
                throw new Exception();
            }

            grid.Tag = null;

            if (context.Position.Row < 1)
                throw new Exception();
        }
        public static void DisplayRectangle(this SourceGrid.DataGrid grid, CellContext context)
        {
            int rectHeight = 150;

            context.Grid.Controls.Add(grid);

            var xPos = context.Grid.Columns.GetWidth(context.Position.Column);
            var yPos = 0;

            for (int i = 0; i <= context.Position.Row; i++)
                yPos += context.Grid.Rows.GetHeight(i);

            if (yPos + rectHeight > (context.Grid.Bounds.Height + context.Grid.Rows.GetHeight(context.Position.Row) + 10))
                yPos = yPos - (rectHeight + context.Grid.Rows.GetHeight(context.Position.Row) + 10);

            Rectangle dgvRectangle = new Rectangle(xPos + 5, yPos + 5, 0, 0);

            grid.Size = new Size(context.Grid.Width - (dgvRectangle.X + 5), rectHeight);
            grid.Location = new Point(dgvRectangle.X, dgvRectangle.Y);

            grid.BorderStyle = BorderStyle.FixedSingle;

            grid.Refresh();
        }
        #endregion

        #region SourceGrid.DataGrid EventHandlers
        private static void ContextMenu_Popup(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
        }

        private static void SourceGrid_MouseClick(object sender, MouseEventArgs e)
        {

            SourceGrid.DataGrid dg = sender as SourceGrid.DataGrid;

            if (e.Button == MouseButtons.Right && dg.Visible)
            {
                if (dg.MouseDownPosition.Row <= 0)
                    return;

                dg.Selection.FocusRow(dg.MouseDownPosition.Row);

                ContextMenuStrip ctxMenu = dg.Tag as ContextMenuStrip;

                if (ctxMenu != null)
                    ctxMenu.Show(dg, new Point(e.X, e.Y));

            }
        }

        private static void Grid_Paint(object sender, PaintEventArgs e)
        {

            SourceGrid.DataGrid grid = sender as SourceGrid.DataGrid;

            Rectangle rect = grid.DisplayRectangle;
            //using (Brush b = new SolidBrush(Color.Red))
            //{
            //    e.Graphics.FillRectangle(b, e.CellBounds);
            //}
            using (Pen pen = new Pen(Brushes.DarkGreen))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawRectangle(pen, rect);
            }

            // throw new NotImplementedException();
        }

        public static void RowHeaderSelectEvent_Click(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;
                // context.Grid.Selection.SelectRow(context.Position.Row, true);

                context.Grid.Selection.FocusRow(context.Position.Row);
            }
            catch (Exception x)
            {
            }
        }

        public static void DeleteEvent_Click(object sender, EventArgs e)
        {
            CellContext context = (CellContext)sender;
            try
            {
                SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;

                if (context.Position.Row > 0)
                {
                    if (context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow || dg.DataSource.AllowNew == false)
                    {
                        context.Grid.Selection.FocusRow(context.Position.Row);

                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
                        {
                            //SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
                            // dg.DeleteSelectedRows();

                            int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
                            if (dataIndex < dg.DataSource.Count)
                            {
                                dg.DataSource.RemoveAt(dataIndex);
                            }
                        }
                    }
                }
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }

        }
        public static void ContextMenuEvent_Click(object sender, EventArgs e)
        {
            CellContext context = (CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);


                    //if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
                    //{
                    //    SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
                    //    // dg.DeleteSelectedRows();

                    //    int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
                    //    if (dataIndex < dg.DataSource.Count)
                    //        dg.DataSource.RemoveAt(dataIndex);
                    //}
                }
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }

        }
        static void dataGrid_UserException(object sender, SourceGrid.ExceptionEventArgs e)
        {
            MessageBoxExt.ShowException(e.Exception);
        }

        public static void AddColumnButton(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
        {
            if (ReadOnly)
                return;

            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.Click += eventHandler;

            var btn = new SourceGrid.Cells.RowHeader("");
            //btn.Image = easiplan.app.Properties.Resources;
            btn.ToolTipText = "Click to add Funds";
            btn.AddController(clickEvent);

            var col = grid.Columns.Add("", "", btn);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;
        }
        public static void AddRowClickEvent(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
        {
            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents ClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            ClickEvent.FocusEntered += eventHandler;
            grid.Controller.AddController(ClickEvent); //Event fired when any column is clicked

        }

        public static SourceGrid.DataGrid Rebind<T>(this SourceGrid.DataGrid grid, IList<T> bindingSource, ListChangedEventHandler PropertyChangedHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null)
        {
            grid.DataSource = new DevAge.ComponentModel.BoundList<T>(bindingSource);

            grid.DataSource.ListChanged -= PropertyChangedHandler;
            grid.DataSource.ListChanged += PropertyChangedHandler;
            grid.DataSource.ItemDeleted -= ItemDeletedEventHandler;
            grid.DataSource.ItemDeleted += ItemDeletedEventHandler;

            grid.Refresh();

            return grid;
        }

        #endregion

        #region SourceGrid.DataGrid Formatt
        public static SourceGrid.DataGrid Formatt(this SourceGrid.DataGrid grid, bool ReadOnly = false, bool AllowDelete = true, bool AllowAddNew = true, GridFormats format = GridFormats.Default, bool AlternateBackground = false)
        {
            grid.SuspendLayout();

            #region Header Cell Format
            DevAge.Drawing.VisualElements.ColumnHeader bheader = new DevAge.Drawing.VisualElements.ColumnHeader();
            bheader.BackColor = Color.WhiteSmoke;
            //bheader.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1, Color.Gainsboro, Color.Gainsboro);
            bheader.BackgroundColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid;


            SourceGrid.Cells.Views.Header header = new SourceGrid.Cells.Views.Header();
            header.Background = bheader;
            header.ForeColor = Color.DarkSlateGray;
            header.Font = Global.GridFont;// new Font("Verdana", 8, FontStyle.Regular);
            header.WordWrap = true;
            //header.TrimmingMode = TrimmingMode.Word;

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                //grid.Columns[i].AutoSizeMode = SourceGrid.AutoSizeMode.EnableAutoSizeView;
                grid.Columns[i].HeaderCell.View = header;

                if (grid.Columns[i].DataCell.Editor != null)
                {
                    if (grid.Columns[i].DataCell.Editor.EditableMode != SourceGrid.EditableMode.None)
                    {
                        grid.Columns[i].DataCell.Editor.EnableEdit = !ReadOnly;

                    }
                }
            }
            #endregion

            #region Editor Cell Formats
            SourceGrid.Cells.Views.Cell mView_Amount = new SourceGrid.Cells.Views.Cell();
            mView_Amount.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;

            SourceGrid.Cells.Views.Cell mView_Amountdisabled = new SourceGrid.Cells.Views.Cell();
            mView_Amountdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            mView_Amountdisabled.BackColor = Color.GhostWhite;
            mView_Amountdisabled.ForeColor = Color.DarkSlateGray;

            SourceGrid.Cells.Views.Cell mView_Textdisabled = new SourceGrid.Cells.Views.Cell();
            mView_Textdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
            mView_Textdisabled.BackColor = Color.GhostWhite;// Color.WhiteSmoke;
            mView_Textdisabled.ForeColor = Color.DarkSlateGray;

            //set editor formats/views
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].DataCell.Editor != null)
                {
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(CurrencyEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Amountdisabled;
                        else
                            grid.Columns[i].DataCell.View = mView_Amount;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(NumericEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Amountdisabled;
                        else
                            grid.Columns[i].DataCell.View = mView_Amount;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(DecimalEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Amountdisabled;
                        else
                            grid.Columns[i].DataCell.View = mView_Amount;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(StringEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Textdisabled;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(MultiLineEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Textdisabled;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(ComboBoxEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Textdisabled;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(DateEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Textdisabled;
                    }
                    if (grid.Columns[i].DataCell.Editor.GetType() == typeof(CheckBoxEditor))
                    {
                        if (grid.Columns[i].DataCell.Editor.EditableMode == EditableMode.None)
                            grid.Columns[i].DataCell.View = mView_Textdisabled;
                    }
                }
                else
                {
                    grid.Columns[i].DataCell.View = mView_Textdisabled;
                }

            }
            #endregion

            #region Alternate Background View
            if (AlternateBackground)
                foreach (SourceGrid.DataGridColumn colu in grid.Columns)
                {
                    SourceGrid.Conditions.ICondition condition =
                        SourceGrid.Conditions.ConditionBuilder.AlternateView(colu.DataCell.View,
                                                                             Color.WhiteSmoke, Color.Black);
                    colu.Conditions.Add(condition);
                }
            #endregion

            #region Selection Mode
            //grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            //grid.Selection.EnableMultiSelection = false;

            SourceGrid.Selection.SelectionBase SelectionBase = grid.Selection as SourceGrid.Selection.SelectionBase;

            SelectionBase.BackColor = Color.FromArgb(75, Color.FromKnownColor(KnownColor.Highlight));

            //DevAge.Drawing.RectangleBorder border = SelectionBase.Border;
            //border.SetWidth(1);
            //border.SetColor(Color.DarkGray);
            //SelectionBase.Border = border;

            //SourceGrid.GridSpecialKeys specialKeys = SourceGrid.GridSpecialKeys.None;
            //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Tab;
            //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Arrows;
            //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Enter;
            //specialKeys = specialKeys | SourceGrid.GridSpecialKeys.Escape;

            //grid.SpecialKeys = specialKeys;

            //grid.Selection.FocusStyle = grid.Selection.FocusStyle | SourceGrid.FocusStyle.FocusFirstCellOnEnter;
            //grid.Selection.FocusStyle = grid.Selection.FocusStyle | SourceGrid.FocusStyle.RemoveFocusCellOnLeave;

            #endregion

            grid.UserException += dataGrid_UserException;

            #region Tooltip
            SourceGrid.Cells.Controllers.ToolTipText toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
            toolTipController.ToolTipTitle = "ToolTip example";
            toolTipController.ToolTipIcon = ToolTipIcon.Info;
            toolTipController.IsBalloon = true;


            #endregion

            FormatGrid(grid, format);

            grid.ResumeLayout(true);

            return grid;
        }

        internal static void FormatGrid(SourceGrid.DataGrid grid, GridFormats format = GridFormats.Default)
        {
            try
            {
                switch (format)
                {
                    case GridFormats.Default:
                        //control.Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Pixel);
                        grid.Font = Global.GridFont;

                        grid.BorderStyle = BorderStyle.FixedSingle;
                        grid.AutoStretchColumnsToFitWidth = true;
                        //grid.Paint -= Grid_Paint;
                        //grid.Paint += Grid_Paint;

                        // grid.AutoSizeCells();
                        grid.Columns.AutoSize(true);
                        grid.Columns.StretchToFit();

                        grid.EnableSort = false;

                        break;
                    case GridFormats.Format1:
                        grid.Font = MetroFonts.Default(12f);
                        grid.BorderStyle = BorderStyle.FixedSingle;
                        grid.AutoStretchColumnsToFitWidth = true;
                        grid.Columns.AutoSize(true);
                        grid.Columns.StretchToFit();

                        grid.EnableSort = false;
                        break;
                    case GridFormats.Format11:
                        grid.Font = MetroFonts.Default(13f);
                        grid.EnableSort = false;
                        break;
                    case GridFormats.Format2:
                        grid.Font = MetroFonts.Default(13f);
                        grid.EnableSort = false;
                        break;
                    case GridFormats.Format3:
                        grid.Font = MetroFonts.Default(14f);
                        grid.EnableSort = false;
                        break;
                    default:
                        break;
                };




            }
            catch (Exception x)
            {
            }
        }
        #endregion

        #region Tooltips
        public static void SetTooltip(this MetroFramework.Controls.MetroButton button, string Icon, string Title, string Tooltip, bool ShowAlways = false)
        {
            BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
            if (null != btnToolTip)
            {
                btnToolTip.RemoveAll();
            }

            btnToolTip = new BalloonToolTip();

            //Add tooltips for the buttons
            btnToolTip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), Icon);
            btnToolTip.ShowAlways = ShowAlways;
            btnToolTip.Absolute = true;
            btnToolTip.Title = Title;
            btnToolTip.SetToolTip(button, Tooltip);
            btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            button.Tag = btnToolTip;
        }

        public static void SetTooltip(this MetroFramework.Controls.MetroButton button, BalloonIcon Icon, string Title, string Tooltip, bool ShowAlways = false)
        {
            BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
            if (null != btnToolTip)
            {
                btnToolTip.RemoveAll();
            }

            btnToolTip = new BalloonToolTip();

            //Add tooltips for the buttons
            btnToolTip.Icon = Icon;
            btnToolTip.ShowAlways = ShowAlways;
            btnToolTip.Absolute = true;
            btnToolTip.Title = Title;
            btnToolTip.SetToolTip(button, Tooltip);
            btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            button.Tag = btnToolTip;
        }

        public static void HideTooltip(this MetroFramework.Controls.MetroButton button)
        {
            try
            {
                BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
                btnToolTip.ShowAlways = false;
                btnToolTip.RemoveAll();
            }
            catch (Exception x)
            {
            }

        }

        #endregion

        public enum GridFormats
        {
            Default,
            Format1,
            Format11,
            Format2,
            Format3

        }
    }

    #region Panel ControlsBuilder
    public enum ControlsLayout
    {
        Horizontal,
        Vertical
    }
    public class ControlBuilder<T> : IList<PanelControl<T>> where T : class
    {
        //  private readonly ModelMetadataProvider _metadataProvider;
        private readonly List<PanelControl<T>> _Controls = new List<PanelControl<T>>();

        public PanelControl<T> this[int index]
        {
            get
            {
                return _Controls[index];
            }

            set
            {
                _Controls[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _Controls.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(PanelControl<T> item)
        {
            _Controls.Add(item);
        }

        public void Clear()
        {
            _Controls.Clear();
        }

        public bool Contains(PanelControl<T> item)
        {
            return _Controls.Contains(item);
        }

        public void CopyTo(PanelControl<T>[] array, int arrayIndex)
        {
            _Controls.CopyTo(array, arrayIndex);
        }

        public IEnumerator<PanelControl<T>> GetEnumerator()
        {
            return _Controls.GetEnumerator();
        }

        public int IndexOf(PanelControl<T> item)
        {
            return _Controls.IndexOf(item);
        }

        public void Insert(int index, PanelControl<T> item)
        {
            _Controls.Insert(index, item);
        }

        public bool Remove(PanelControl<T> item)
        {
            return _Controls.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _Controls.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        PanelControl<T> IList<PanelControl<T>>.this[int index]
        {
            get { return _Controls[index]; }
            set { _Controls[index] = value; }
        }

        /// <summary>
        /// Specifies a control should be constructed for the specified property.
        /// </summary>
        /// <param name="propertySpecifier">Lambda that specifies the property for which a column should be constructed</param>
        public PanelControl<T> For(Expression<Func<T, object>> propertySpecifier, string labelName = "", BaseMetroEditor editor = null, string toolTip = "")
        {

            var memberExpression = propertySpecifier == null ? null : GetMemberExpression(propertySpecifier);
            var propertyType = propertySpecifier == null ? null : GetTypeFromMemberExpression(memberExpression);
            var declaringType = memberExpression == null ? null : memberExpression.Expression.Type;
            var inferredName = memberExpression == null ? null : memberExpression.Member.Name;
            var control = new PanelControl<T>(propertySpecifier == null ? null : propertySpecifier.Compile(), inferredName, propertyType, editor);


            if (declaringType != null)
            {
                //    var metadata = _metadataProvider.GetMetadataForProperty(null, declaringType, inferredName);

                //    if (!string.IsNullOrEmpty(metadata.DisplayName))
                //    {
                //        column.Named(metadata.DisplayName);
                //    }

                //    if (!string.IsNullOrEmpty(metadata.DisplayFormatString))
                //    {
                //        column.Format(metadata.DisplayFormatString);
                //    }
            }

            if (!string.IsNullOrEmpty(labelName))
            {
                control.Named(labelName);
            }
            if (!string.IsNullOrEmpty(toolTip))
            {
                control.ToolTiped(toolTip);
            }

            Add(control);

            return control;
        }

        public static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            return RemoveUnary(expression.Body) as MemberExpression;
        }

        private static Type GetTypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null) return null;

            var dataType = GetTypeFromMemberInfo(memberExpression.Member, (PropertyInfo p) => p.PropertyType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (MethodInfo m) => m.ReturnType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (FieldInfo f) => f.FieldType);

            return dataType;
        }

        private static Type GetTypeFromMemberInfo<TMember>(MemberInfo member, Func<TMember, Type> func) where TMember : MemberInfo
        {
            if (member is TMember)
            {
                return func((TMember)member);
            }
            return null;
        }

        private static Expression RemoveUnary(Expression body)
        {
            var unary = body as UnaryExpression;
            if (unary != null)
            {
                return unary.Operand;
            }
            return body;
        }
    }
    public class PanelControl<T> : IPanelControl<T> where T : class
    {
        private readonly string _name;
        private string _displayName;
        private bool _doNotSplit;
        private readonly Func<T, object> _columnValueFunc;
        private readonly Type _dataType;
        private Func<T, bool> _cellCondition = x => true;
        private string _format;
        private bool _visible = true;
        private bool _htmlEncode = true;
        private readonly IDictionary<string, object> _headerAttributes = new Dictionary<string, object>();
        // private List<Func<GridRowViewData<T>, IDictionary<string, object>>> _attributes = new List<Func<GridRowViewData<T>, IDictionary<string, object>>>();
        private bool _sortable = true;
        private string _sortColumnName = null;
        private SortDirection? _initialDirection;
        private int? _position;
        private Func<object, object> _headerRenderer = x => null;

        private string _toolTip;

        private BaseMetroEditor _editor = null;

        public BaseMetroEditor Editor
        {
            get { return _editor; }
            set { _editor = value; }
        }

        /// <summary>
        /// Creates a new instance of the GridColumn class
        /// </summary>
        public PanelControl(Func<T, object> columnValueFunc, string name, Type type, BaseMetroEditor editor = null)
        {
            _name = name;
            _displayName = name;
            _dataType = type;
            _columnValueFunc = columnValueFunc;
            _editor = editor;


        }

        public bool Sortable
        {
            get { return _sortable; }
        }

        public bool Visible
        {
            get { return _visible; }
        }

        public string SortColumnName
        {
            get { return _sortColumnName; }
        }

        public SortDirection? InitialDirection
        {
            get { return _initialDirection; }
        }

        /// <summary>
        /// Name of the column
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Display name for the column
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (_doNotSplit)
                {
                    return _displayName;
                }
                return SplitPascalCase(_displayName);
            }
        }

        /// <summary>
        /// ToolTip for the column
        /// </summary>
        public string ToolTip
        {
            get
            {
                if (_doNotSplit)
                {
                    return _toolTip;
                }
                return SplitPascalCase(_toolTip);
            }
        }

        /// <summary>
        /// The type of the object being rendered for thsi column. 
        /// Note: this will return null if the type cannot be inferred.
        /// </summary>
        public Type ColumnType
        {
            get { return _dataType; }
        }

        public int? Position
        {
            get { return _position; }
        }

        //IGridColumn<T> IGridColumn<T>.Attributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes)
        //{
        //    _attributes.Add(attributes);
        //    return this;
        //}

        IPanelControl<T> IPanelControl<T>.Sortable(bool isColumnSortable)
        {
            _sortable = isColumnSortable;
            return this;
        }

        IPanelControl<T> IPanelControl<T>.SortColumnName(string name)
        {
            _sortColumnName = name;
            return this;
        }

        IPanelControl<T> IPanelControl<T>.SortInitialDirection(SortDirection initialDirection)
        {
            _initialDirection = initialDirection;
            return this;
        }


        IPanelControl<T> IPanelControl<T>.InsertAt(int index)
        {
            _position = index;
            return this;
        }

        /// <summary>
        /// Additional attributes for the column header
        /// </summary>
        public IDictionary<string, object> HeaderAttributes
        {
            get { return _headerAttributes; }
        }

        /// <summary>
        /// Additional attributes for the cell
        /// </summary>
        //public Func<GridRowViewData<T>, IDictionary<string, object>> Attributes
        //{
        //    get { return GetAttributesFromRow; }
        //}

        //private IDictionary<string, object> GetAttributesFromRow(GridRowViewData<T> row)
        //{
        //    var dictionary = new Dictionary<string, object>();
        //    var pairs = _attributes.SelectMany(attributeFunc => attributeFunc(row));

        //    foreach (var pair in pairs)
        //    {
        //        dictionary[pair.Key] = pair.Value;
        //    }

        //    return dictionary;
        //}

        public IPanelControl<T> Named(string name)
        {
            _displayName = name;
            _doNotSplit = true;
            return this;
        }

        public IPanelControl<T> ToolTiped(string tooltip)
        {
            _toolTip = tooltip;
            _doNotSplit = true;
            return this;
        }

        public IPanelControl<T> DoNotSplit()
        {
            _doNotSplit = true;
            return this;
        }

        public IPanelControl<T> Format(string format)
        {
            _format = format;
            return this;
        }

        public IPanelControl<T> CellCondition(Func<T, bool> func)
        {
            _cellCondition = func;
            return this;
        }

        IPanelControl<T> IPanelControl<T>.Visible(bool isVisible)
        {
            _visible = isVisible;
            return this;
        }

        public IPanelControl<T> Label(Func<object, object> headerRenderer)
        {
            _headerRenderer = headerRenderer;
            return this;
        }

        public IPanelControl<T> Encode(bool shouldEncode)
        {
            _htmlEncode = shouldEncode;
            return this;
        }



        IPanelControl<T> IPanelControl<T>.LabelAttributes(IDictionary<string, object> attributes)
        {
            foreach (var attribute in attributes)
            {
                _headerAttributes.Add(attribute);
            }

            return this;
        }

        private string SplitPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        /// <summary>
        /// Gets the value for a particular cell in this column
        /// </summary>
        /// <param name="instance">Instance from which the value should be obtained</param>
        /// <returns>Item to be rendered</returns>
        public object GetValue(T instance)
        {
            if (!_cellCondition(instance) || instance == null)
            {
                return null;
            }

            var value = _columnValueFunc(instance);

            if (!string.IsNullOrEmpty(_format))
            {
                value = string.Format(_format, value);
            }

            //if (_htmlEncode && value != null && !(value is IHtmlString))
            //{
            //    value = HttpUtility.HtmlEncode(value.ToString());
            //}

            return value;
        }

        public string GetHeader()
        {
            var header = _headerRenderer(null);
            return header == null ? null : header.ToString();
        }

    }
    public interface IPanelControl<T>
    {
        /// <summary>
        /// Specified an explicit name for the column.
        /// </summary>
        /// <param name="name">Name of column</param>
        /// <returns></returns>
        IPanelControl<T> Named(string name);
        /// <summary>
        /// If the property name is PascalCased, it should not be split part.
        /// </summary>
        /// <returns></returns>
        IPanelControl<T> DoNotSplit();
        /// <summary>
        /// A custom format to use when building the cell's value
        /// </summary>
        /// <param name="format">Format to use</param>
        /// <returns></returns>
        IPanelControl<T> Format(string format);
        /// <summary>
        /// Delegate used to hide the contents of the cells in a column.
        /// </summary>
        IPanelControl<T> CellCondition(Func<T, bool> func);

        /// <summary>
        /// Determines whether the column should be displayed
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        IPanelControl<T> Visible(bool isVisible);

        IPanelControl<T> Label(Func<object, object> customHeaderRenderer);

        /// <summary>
        /// Determines whether or not the column should be encoded. Default is true.
        /// </summary>
        IPanelControl<T> Encode(bool shouldEncode);



        /// <summary>
        /// Defines additional attributes for the column heading.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IPanelControl<T> LabelAttributes(IDictionary<string, object> attributes);

        /// <summary>
        /// Defines additional attributes for the cell. 
        /// </summary>
        /// <param name="attributes">Lambda expression that should return a dictionary containing the attributes for the cell</param>
        /// <returns></returns>
        //IGridColumn<T> Attributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes);

        /// <summary>
        /// Specifies whether or not this column should be sortable. 
        /// The default is true. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IPanelControl<T> Sortable(bool isColumnSortable);

        /// <summary>
        /// Specifies a custom name that should be used when sorting on this column
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPanelControl<T> SortColumnName(string name);

        /// <summary>
        /// Specifies the direction of the sort link when this column is not currently sorted.  
        /// The direction will continue to toggle when it is the currently sorted column. 
        /// </summary>
        /// <param name="initialDirection"></param>
        /// <returns></returns>
        IPanelControl<T> SortInitialDirection(SortDirection initialDirection);



        /// <summary>
        /// Specifies the position of a column. 
        /// This is usually used in conjunction with the AutoGenerateColumns method 
        /// in order to specify where additional custom columns should be placed.
        /// </summary>
        /// <param name="index">The index at which the column should be inserted</param>
        IPanelControl<T> InsertAt(int index);


    }
    #endregion

    #region Editors
    public interface IMetroEditor
    {
        bool Enabled { get; set; }

        //Editor
        Type EditorType { get; set; }

        EditorBase DataGridEditor { get; set; }

        //Style
        DataGridViewCellStyle FormatStyle { get; set; }
        Size Size { get; set; }

        Font Font { get; set; }

        //Create Controls
        Control CreateControl();

        DataGridViewColumn CreateColumn(string name, string headerText);
        DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid);

        //Validation
        IMetroValidator Validator { get; set; }

        //DataBinding
        string BindingName { get; set; }
        DataSourceUpdateMode dataSourceUpdateMode { get; set; }

        //Editor Events
        Delegate OnEditorStarting();
        Delegate OnEditorFinish();
        Delegate OnEditorKeyUp();
        Delegate OnEditorKeyDown();
        Delegate OnEditorLostFocus();
        Delegate OnEditorGetFocus();
    }
    public abstract class BaseMetroEditor : IMetroEditor
    {
        ErrorProvider errorProvider = new ErrorProvider();
        internal string _valueMember = "Value";
        internal string _displayMember = "Text";
        internal int _width;
        internal int _height;

        #region  Delegate : SelectedIndexChange
        public delegate void ControlSelectedIndexChanged(object sender, EventArgs e);
        public event ControlSelectedIndexChanged controlSelectedIndexChanged;
        public void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            controlSelectedIndexChanged?.Invoke(this, e);
        }
        #endregion

        #region Delegate : EnterKey Clicked
        public delegate void ControlKeyClicked(object sender, KeyEventArgs e);
        public event ControlKeyClicked EnterKeyClicked;
        #endregion

        #region Default Constructor and Fluent Extentions
        public BaseMetroEditor(int Width = 200, int Height = 0)
        {
            _width = Width;
            _height = Height;

            Watermark = string.Empty;

            Enabled = true;

            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
        }
        public BaseMetroEditor ReadOnly(bool readOnly)
        {
            Enabled = !readOnly;
            return this;

        }
        public BaseMetroEditor FontSize(Font font, bool IsBold = false)
        {
            Font = font;

            return this;
        }
        public virtual BaseMetroEditor ControlValueChanged(ControlSelectedIndexChanged indexChanged)
        {
            controlSelectedIndexChanged = indexChanged;

            return this;
        }
        #endregion

        #region  Interface Properties
        public bool Enabled
        {
            get; set;
        }
        public Type EditorType
        {
            get; set;
        }
        public Control Control { get; set; }
        public DataGridViewColumn Column { get; set; }
        public DataGridColumn dataGridColumn { get; set; }
        public DataGridViewCellStyle FormatStyle { get; set; }
        public string ErrorMessage { get; set; }
        public string Watermark { get; set; }
        Size _size = new Size(100, 20);
        public Size Size { get { return _size; } set { _size = value; } }
        public Font Font { get; set; } = Global.TextFont;
        #endregion

        //Interface Methods
        public virtual Control CreateControl()
        {

            Control.Paint += Control_Paint;
            Control.TextChanged += Control_TextChanged;
            Control.KeyDown += Control_KeyDown;
            Control.KeyUp += Control_KeyUp;
            Control.KeyPress += Control_KeyPress;
            Control.LostFocus += Control_LostFocus;
            Control.GotFocus += Control_GotFocus;

            Control.CausesValidation = true; //Enables DataSourceUpdate.OnValidation event to fire

            Control.Validating += Control_Validating;
            Control.Validated += Control_Validated;

            Control.Width = _width;

            if (_height > 0)
                Control.Height = _height;
            else
                _height = Control.Height;

            Size = new Size(_width, _height);


            return Control;
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //suppress the beep sound
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        public virtual DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column.ReadOnly = !Enabled;

            Column.DefaultCellStyle = new DataGridViewCellStyle(FormatStyle);

            if (Column.ReadOnly)
            {
                //Column.DefaultCellStyle.ForeColor = Color.Gray;
                //Column.DefaultCellStyle.SelectionForeColor = Color.Gray;
            }

            return Column;
        }

        //DataSource List for Dropdown controls
        internal IList _DataSource;
        public BaseMetroEditor DataSourceList(IList dataSource, string displayMember = "Text", string valueMember = "Value")
        {

            _DataSource = dataSource;
            _valueMember = valueMember;
            _displayMember = displayMember;

            return this;

        }

        //DataBinding
        public string BindingName { get; set; }
        public DataSourceUpdateMode dataSourceUpdateMode { get; set; }

        //Validator
        public IMetroValidator Validator { get; set; }

        # region DataGrid Editor
        public EditorBase DataGridEditor
        {
            get; set;
        }

        public virtual DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {

            this.DataGridEditor.EditableMode = !Enabled ? EditableMode.None : EditableMode.SingleClick;

            this.DataGridEditor.Changed += DataGridEditor_Changed;
            this.DataGridEditor.ConvertingObjectToValue += DataGridEditor_ConvertingObjectToValue;
            this.DataGridEditor.ConvertingValueToDisplayString += DataGridEditor_ConvertingValueToDisplayString;
            this.DataGridEditor.ConvertingValueToObject += DataGridEditor_ConvertingValueToObject;
            this.DataGridEditor.EditException += DataGridEditor_EditException;
            //this.DataGridEditor.Validated += DataGridEditor_Validated;
            //this.DataGridEditor.Validating += DataGridEditor_Validating;

            this.dataGridColumn = grid.Columns.Add(name, headerText, this.DataGridEditor);

            SourceGrid.Cells.Views.Cell mView_Amountdisabled = new SourceGrid.Cells.Views.Cell();
            mView_Amountdisabled.BackColor = Color.GhostWhite.GetPastelShade();

            if (this._width > 200)
            {
                this.dataGridColumn.MinimalWidth = this._width;
                this.dataGridColumn.Width = this._width;
            }
            else if (this._width < 200)
            {
                this.dataGridColumn.MaximalWidth = this._width;
            }


            return this.dataGridColumn;
        }

        private void Control_Validating1(object sender, CancelEventArgs e)
        {
            var cntrl = sender as EditorBase;
            //errorProvider.SetError(cntrl, "");
            try
            {
                if (Validator != null)
                    Validator.Validate(cntrl.GetEditedValue(), "Validation Failed");

            }
            catch (Exception x)
            {
                //errorProvider.SetError(cntrl, this.ErrorMessage);
                e.Cancel = true;
                cntrl.ClearCell(cntrl.EditCellContext);
                this.DataGridEditor.ClearCell(cntrl.EditCellContext);
            }
        }

        private void DataGridEditor_Validated(object sender, CellContextEventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_EditException(object sender, ExceptionEventArgs e)
        {
            var cntrl = sender as EditorBase;

            this.DataGridEditor.ClearCell(cntrl.EditCellContext);

            e.Handled = true;

        }

        private void DataGridEditor_ConvertingValueToObject(object sender, ConvertingObjectEventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_ConvertingValueToDisplayString(object sender, ConvertingObjectEventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_ConvertingObjectToValue(object sender, ConvertingObjectEventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_Changed(object sender, EventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_Validating1(object sender, ValidatingCellEventArgs e)
        {
            var cntrl = sender as EditorBase;
        }

        private void DataGridEditor_Validating(object sender, ValidatingCellEventArgs e)
        {
            var cntrl = sender as EditorBase;
            //errorProvider.SetError(cntrl, "");
            try
            {
                if (Validator != null)
                    Validator.Validate(e.NewValue, "Validation Failed");

            }
            catch (Exception x)
            {
                //errorProvider.SetError(cntrl, this.ErrorMessage);
                e.Cancel = true;

                this.DataGridEditor.ClearCell(cntrl.EditCellContext);

            }
        }

        //Editor Delegates
        public Delegate OnEditorFinish()
        {
            return null;
        }

        public Delegate OnEditorGetFocus()
        {
            return null;
        }

        public Delegate OnEditorKeyDown()
        {
            return null;
        }

        public Delegate OnEditorKeyUp()
        {
            return null;
        }

        public Delegate OnEditorLostFocus()
        {
            return null;
        }

        public Delegate OnEditorStarting()
        {
            return null;
        }

        public virtual BaseMetroEditor OnEditorClick(EventHandler eventHandler)
        {

            return this;
        }
        #endregion

        #region Control Events
        public virtual void Control_TextChanged(object sender, EventArgs e)
        {

        }
        private void Control_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var cntrl = sender as Control;
            errorProvider.SetError(cntrl, "");
            try
            {
                if (Validator != null && this.Enabled)
                    Validator.Validate(cntrl.Text);
            }
            catch (Exception x)
            {
                errorProvider.SetError(cntrl, this.ErrorMessage);
                e.Cancel = true;
            }
        }

        public virtual void Control_Validated(object sender, EventArgs e)
        {

        }

        private void Control_GotFocus(object sender, EventArgs e)
        {

        }
        private void Control_LostFocus(object sender, EventArgs e)
        {
            // return;

            var cntrl = sender as Control;
            errorProvider.SetError(cntrl, "");

            try
            {
                //using (new AppWaitCursor(sender))
                //{

                if (Validator != null && this.Enabled)
                    if (!Validator.IsValidated)
                        Validator.Validate(cntrl.Text);

                //YJ 2021-11-05
                UpdateDataBindings(cntrl.Text);
                //}
            }
            catch (Exception x)
            {


                errorProvider.SetError(cntrl, this.ErrorMessage);

                var restoreFocus = (System.Threading.ThreadStart)delegate { Control.Focus(); };
                Control.BeginInvoke(restoreFocus);
            }
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            e.SuppressKeyPress = false;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                DevAgeTextBox textBox = sender as DevAgeTextBox;
                if (textBox != null)
                {
                    textBox.Undo();
                    textBox.ClearUndo();
                }

            }
            if (e.KeyCode == Keys.Enter)
            {

                UpdateDataBindings(sender);

                EnterKeyClicked?.Invoke(this.Control, e);
                //suppress the beep sound
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }
        #endregion


        private void UpdateDataBindings(object value)
        {
            if (this.dataSourceUpdateMode == DataSourceUpdateMode.OnValidation)
            {
                foreach (Binding db in this.Control.DataBindings)
                {
                    db.WriteValue();
                }
            }
        }
    }

    public class MetroTextBoxEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroTextBoxEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            DevAgeTextBox control = new DevAgeTextBox();
            control.Font = Global.TextFont;
            control.ReadOnlyChanged += Control_ReadOnlyChanged;

            control.SelectedText = this.Watermark;
            control.ReadOnly = !Enabled;

            control.Height += 3;

            this.Control = control;

            if (!string.IsNullOrEmpty(RegExpression))
                this.Validator = new RegExValidator(RegExpression);

            return base.CreateControl();
        }

        private void Control_ReadOnlyChanged(object sender, EventArgs e)
        {
            DevAgeTextBox textBox = sender as DevAgeTextBox;
            if (textBox != null)
                if (textBox.ReadOnly)
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
                    this.Enabled = false;
                }
                else
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    this.Enabled = true;
                }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };
            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            if (this.DataGridEditor == null)
                this.DataGridEditor = new StringEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }

    }
    public class MetroMultiLineTextBoxEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroMultiLineTextBoxEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            //MetroTextBox control = new MetroTextBox();
            //control.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            //control.WaterMark = this.Watermark;
            //control.ReadOnly = !Enabled;

            //_height = (control.Height*2) + 3;
            //control.Multiline = true;
            ////control.AcceptsReturn = true;
            ////control.WordWrap = true;
            //control.MaxLength = 4000;
            //control.ScrollBars = ScrollBars.Vertical;

            DevAgeTextBox control = new DevAgeTextBox();
            control.ReadOnlyChanged += Control_ReadOnlyChanged;
            control.AcceptsReturn = true;
            control.Multiline = true;
            control.TextAlign = HorizontalAlignment.Left;

            control.Font = Global.TextFont;

            control.SelectedText = this.Watermark;

            control.ReadOnly = !Enabled;
            control.Height = (control.Height * 2) + 3;
            control.MaxLength = 4000;
            control.ScrollBars = ScrollBars.Vertical;

            this.Control = control;
            this.Validator = new RegExValidator(RegExpression);

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            this.Control.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            Control.CausesValidation = true; //Enables DataSourceUpdate.OnValidation event to fire

            //Control.Validating += Control_Validating;
            //Control.Validated += Control_Validated;

            Control.Width = _width;

            if (_height > 0)
                Control.Height = _height;
            else
                _height = Control.Height;

            Size = new Size(_width, _height);

            return this.Control;// base.CreateControl();
        }

        private void Control_ReadOnlyChanged(object sender, EventArgs e)
        {
            DevAgeTextBox textBox = sender as DevAgeTextBox;
            if (textBox != null)
                if (textBox.ReadOnly)
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
                }
                else
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                }
        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,
            };
            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new MultiLineEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }

        public override void Control_TextChanged(object sender, EventArgs e)
        {
            DevAgeTextBox textBox = sender as DevAgeTextBox;
            try
            {
                textBox.DataBindings[0].WriteValue();
            }
            catch (Exception x)
            {
            }

            base.Control_TextChanged(sender, e);
        }

    }
    public class MetroStringEditor : MetroTextBoxEditor
    {
        public MetroStringEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
        }
        public override Control CreateControl()
        {
            RegExpression = RegExpressions.CharactersAscii();
            this.ErrorMessage = string.Format("Only Characters are allowed");

            return base.CreateControl();
        }
    }
    public class MetroNumberEditor : MetroTextBoxEditor
    {
        public MetroNumberEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            RegExpression = RegExpressions.Numbers();
            this.ErrorMessage = string.Format("Only Numbers are allowed");

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }
        public override Control CreateControl()
        {

            return base.CreateControl();
        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                //  Format = "c"
            };

            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new NumericEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroStringNumberEditor : MetroTextBoxEditor
    {
        public MetroStringNumberEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            RegExpression = RegExpressions.CharacterNumbersAscii();
            this.ErrorMessage = string.Format("Only Characters and Numbers are allowed");
        }

        public override Control CreateControl()
        {
            return base.CreateControl();
        }

    }
    public class MetroNumberUpDownEditor : BaseMetroEditor
    {
        int _min = 0;
        int _max = 100;

        public string RegExpression = string.Empty;
        public MetroNumberUpDownEditor(int min = 0, int max = 100, int Width = 200, int Height = 0) : base(Width, Height)
        {
            RegExpression = RegExpressions.Numbers();
            this.ErrorMessage = string.Format("Only Numbers are allowed");

            this.BindingName = "Text;Value";

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            _min = min;
            _max = max;
        }
        public override Control CreateControl()
        {
            //DevAgeNumericUpDown control = new DevAgeNumericUpDown();
            // MetroTrackBar control = new MetroTrackBar();
            KryptonTrackBar control = new KryptonTrackBar();

            control.BackStyle = PaletteBackStyle.ContextMenuItemSplit;
            control.TickStyle = TickStyle.BottomRight;

            control.ValueChanged += Control_ValueChanged;
            control.Enabled = Enabled;

            //control.Height += 5;
            //_height = control.Height + 3;

            control.Height += 20;
            _height = control.Height + 5;

            control.Minimum = _min;
            control.Maximum = _max;
            control.SmallChange = 1;
            control.LargeChange = 5;

            this.Control = control;


            this.Validator = new RegExValidator(RegExpression);


            return base.CreateControl();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //MetroTrackBar cntrl = sender as MetroTrackBar;

                //cntrl.DataBindings[0].WriteValue();

            }
            catch (Exception x)
            {
            }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "D"
            };

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new NumericEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }

    public class MetroPercentageUpDownEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroPercentageUpDownEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            RegExpression = RegExpressions.Decimals();
            this.ErrorMessage = string.Format("Only Decimals are allowed");

            this.BindingName = "Text;Value";

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }
        public override Control CreateControl()
        {
            //DevAgeNumericUpDown control = new DevAgeNumericUpDown();
            MetroTrackBar control = new MetroTrackBar();

            control.ValueChanged += Control_ValueChanged;
            control.Enabled = Enabled;

            //control.Height += 5;
            //_height = control.Height + 3;

            control.Height += 20;
            _height = control.Height + 5;

            control.Minimum = 1;
            control.Maximum = 100;
            control.SmallChange = 1;
            control.LargeChange = 1;

            this.Control = control;

            this.Validator = new RegExValidator(RegExpression);


            return base.CreateControl();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //MetroTrackBar cntrl = sender as MetroTrackBar;

                //cntrl.DataBindings[0].WriteValue();

            }
            catch (Exception x)
            {
            }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "D"
            };

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new NumericEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroCurrencyEditor : MetroTextBoxEditor
    {
        public MetroCurrencyEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            RegExpression = RegExpressions.Currency();
            this.ErrorMessage = string.Format("Only Currency digits are allowed");

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

        }
        public override Control CreateControl()
        {


            base.CreateControl();

            Control.Tag = "C";
            Control.RightToLeft = RightToLeft.Yes;



            return Control;

        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "c"
            };

            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new CurrencyEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }

        public override void Control_TextChanged(object sender, EventArgs e)
        {
            Control editor = sender as Control;
            Decimal val = 0;
            Decimal.TryParse(editor.Text.Replace("R", ""), out val);

            if (val < 0)
            {
                editor.BackColor = Color.FromKnownColor(KnownColor.Yellow);
                editor.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }
            else
            {
                editor.BackColor = Color.FromKnownColor(KnownColor.White);
                editor.ForeColor = Color.FromKnownColor(KnownColor.ControlText); ;
            }

            base.Control_TextChanged(sender, e);
        }
    }
    public class MetroDecimalEditor : MetroTextBoxEditor
    {
        public MetroDecimalEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }

        public override Control CreateControl()
        {
            RegExpression = RegExpressions.Decimals();
            this.ErrorMessage = string.Format("Only Decimals are allowed");


            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "N3"
            };

            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new DecimalEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroNameEditor : MetroTextBoxEditor
    {
        public MetroNameEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {

        }
        public override Control CreateControl()
        {
            RegExpression = RegExpressions.CharactersUnicode(" -'`^");
            this.ErrorMessage = string.Format("Not a valid name");

            return base.CreateControl();
        }
    }
    public class MetroComboBoxEditor : BaseMetroEditor
    {
        bool _dropDownList;
        public MetroComboBoxEditor(int Width = 200, int Height = 0, bool DropdownList = false) : base(Width, Height)
        {
            this.BindingName = "SelectedValue";//SelectedValue
            this._dropDownList = DropdownList;
            
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }
        public override Control CreateControl()
        {
            //MetroComboBox control = new MetroComboBox();
            //control.DropDownStyle = ComboBoxStyle.DropDown;

            //System.Windows.Forms.ComboBox control = new System.Windows.Forms.ComboBox();
            //control.DropDownStyle = ComboBoxStyle.DropDown;

            //DevAgeComboBox control = new DevAgeComboBox();
            //control.EnabledChanged += Control_EnabledChanged;
            //control.Enabled = Enabled;

            ExComboBox control = new ExComboBox();
            control.ReadOnly = !Enabled;

            if (_dropDownList == true)
            {
                control.DropDownStyle = ComboBoxStyle.DropDownList;
                control.ReadOnly = true;
            }


            control.DataSource = _DataSource;
            control.ValueMember = _valueMember;
            control.DisplayMember = _displayMember;

            _height = control.Height + 5;

            control.Font = Global.TextFont;
            control.FlatStyle = FlatStyle.Standard;

            control.SelectedIndexChanged += Control_SelectedIndexChanged;

            this.Control = control;

            return base.CreateControl();
        }

        private void Control_EnabledChanged(object sender, EventArgs e)
        {
            DevAgeComboBox textBox = sender as DevAgeComboBox;
            if (textBox != null)
                if (!textBox.Enabled)
                {
                    // 
                    //textBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    textBox.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
                    // U.SetComboBoxReadOnly(textBox, false);
                }
                else
                {
                    // textBox.DropDownStyle = ComboBoxStyle.DropDown;
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    // U.SetComboBoxReadOnly(textBox, true);
                }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            try
            {
                BindingList<ListDataItem> bList = new BindingList<ListDataItem>((List<ListDataItem>)_DataSource);
                Column = new DataGridViewComboBoxColumn()
                {
                    DataSource = bList,
                    ValueMember = _valueMember,
                    DisplayMember = _displayMember,

                    ValueType = bList[0].Value.GetType(),

                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText,

                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                    FlatStyle = FlatStyle.Flat,
                    DropDownWidth = _width,

                    
                };
                
            }
            catch (Exception x)
            {
                BindingSource bSource = new BindingSource();
                bSource.DataSource = _DataSource;
                Column = new DataGridViewComboBoxColumn()
                {
                    DataSource = bSource,
                    ValueMember = _valueMember,
                    DisplayMember = _displayMember,

                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText,

                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                    FlatStyle = FlatStyle.Flat,
                    DropDownWidth = _width,

                };
            }

            //  



            //myDataGridViewComboBoxColumn cboColumn = new myDataGridViewComboBoxColumn();
            //cboColumn.myComboBox.DataSource = _DataSource;
            //cboColumn.myComboBox.DisplayMember = _displayMember;
            //cboColumn.myComboBox.ValueMember = _valueMember;
            //cboColumn.myComboBox.DropDownStyle = ComboBoxStyle.DropDown;

            //cboColumn.MappingName = name;
            //cboColumn.HeaderText = headerText;

            //Column = cboColumn;

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            try
            {
                var _list = (IEnumerable<ListDataItem>)_DataSource;

                ICollection values = (ICollection)_list.Select(x => x.Text).ToList();

                this.DataGridEditor = new ComboBoxEditor((IEnumerable<ListDataItem>)_DataSource, !Enabled);
                //this.DataGridEditor.StandardValues = values;
                //this.DataGridEditor.StandardValuesExclusive = false;

            }
            catch (Exception x)
            {


            }

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }

    public class MetroComboBoxEditor<T> : BaseMetroEditor
    {
        bool _dropDownList;
        public MetroComboBoxEditor(int Width = 200, int Height = 0, bool DropdownList = false) : base(Width, Height)
        {
            this.BindingName = "Value";//SelectedValue
            this._dropDownList = DropdownList;

            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }
        public override Control CreateControl()
        {
            MetroComboBox control = new MetroComboBox();
            control.DropDownStyle = ComboBoxStyle.DropDown;

            //System.Windows.Forms.ComboBox control = new System.Windows.Forms.ComboBox();
            //control.DropDownStyle = ComboBoxStyle.DropDown;

            //DevAgeComboBox control = new DevAgeComboBox();
            //control.EnabledChanged += Control_EnabledChanged;
            //control.Enabled = Enabled;

            //ExComboBox control = new ExComboBox();
            //control.ReadOnly = !Enabled;

            if (_dropDownList == true)
            {
                control.DropDownStyle = ComboBoxStyle.DropDownList;
                //control.ReadOnly = true;
            }


            control.DataSource = _DataSource;
            control.ValueMember = _valueMember;
            control.DisplayMember = _displayMember;

            _height = control.Height + 5;

            control.Font = Global.TextFont;
            control.FlatStyle = FlatStyle.Standard;

            control.SelectedIndexChanged += Control_SelectedIndexChanged;

            this.Control = control;

            return base.CreateControl();
        }

        private void Control_EnabledChanged(object sender, EventArgs e)
        {
            DevAgeComboBox textBox = sender as DevAgeComboBox;
            if (textBox != null)
                if (!textBox.Enabled)
                {
                    // 
                    //textBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    textBox.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
                    // U.SetComboBoxReadOnly(textBox, false);
                }
                else
                {
                    // textBox.DropDownStyle = ComboBoxStyle.DropDown;
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    // U.SetComboBoxReadOnly(textBox, true);
                }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            try
            {
                BindingList<ListDataItem> bList = new BindingList<ListDataItem>((List<ListDataItem>)_DataSource);
                Column = new DataGridViewComboBoxColumn()
                {
                    DataSource = bList,
                    ValueMember = _valueMember,
                    DisplayMember = _displayMember,

                    ValueType = bList[0].Value.GetType(),

                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText,

                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                    FlatStyle = FlatStyle.Flat,
                    DropDownWidth = _width,

                };

            }
            catch (Exception x)
            {
                BindingSource bSource = new BindingSource();
                bSource.DataSource = _DataSource;
                Column = new DataGridViewComboBoxColumn()
                {
                    DataSource = bSource,
                    ValueMember = _valueMember,
                    DisplayMember = _displayMember,

                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText,

                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                    FlatStyle = FlatStyle.Flat,
                    DropDownWidth = _width,

                };
            }

            //  



            //myDataGridViewComboBoxColumn cboColumn = new myDataGridViewComboBoxColumn();
            //cboColumn.myComboBox.DataSource = _DataSource;
            //cboColumn.myComboBox.DisplayMember = _displayMember;
            //cboColumn.myComboBox.ValueMember = _valueMember;
            //cboColumn.myComboBox.DropDownStyle = ComboBoxStyle.DropDown;

            //cboColumn.MappingName = name;
            //cboColumn.HeaderText = headerText;

            //Column = cboColumn;

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            try
            {
                var _list = (IEnumerable<ListDataItem>)_DataSource;

                this.DataGridEditor = new ComboBoxEditor<ListDataItem, T>(_list, !Enabled);
                //this.DataGridEditor.StandardValues = (ICollection)_list;
                //this.DataGridEditor.StandardValuesExclusive = false;
            }
            catch (Exception x)
            {


            }

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroComboListEditor : BaseMetroEditor
    {

        public MetroComboListEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "SelectedValue";// "SelectedValue;Text";
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }

        public override Control CreateControl()
        {
            MetroComboBox control = new MetroComboBox();

            control.DataBindings.Clear();
            control.DataSource = null;

            control.DataSource = _DataSource;
            control.ValueMember = _valueMember;
            control.DisplayMember = _displayMember;

            control.Enabled = Enabled;

            _height = control.Height + 8;

            control.DropDownStyle = ComboBoxStyle.DropDownList;

            control.SelectedIndexChanged += Control_SelectedIndexChanged;

            this.Control = control;

            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {

            Column = new DataGridViewComboBoxColumn()
            {
                DataSource = _DataSource,
                ValueMember = _valueMember,
                DisplayMember = _displayMember,

                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat,
                DropDownWidth = _width,

            };

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            var _list = (IEnumerable<ListDataItem>)_DataSource;

            ICollection values = (ICollection)_list.Select(x => x.Text).ToList();

            ComboBoxListEditor cbo = new ComboBoxListEditor((IEnumerable<ListDataItem>)_DataSource, !Enabled);
            cbo.SelectedIndexChanged -= Cbo_SelectedIndexChanged;
            cbo.SelectedIndexChanged += Cbo_SelectedIndexChanged;

            this.DataGridEditor = cbo;
            this.DataGridEditor.StandardValues = values;
            this.DataGridEditor.StandardValuesExclusive = false;

            return base.CreateDataGridColumn(name, headerText, grid);
        }

        private void Cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = sender as DevAgeComboBox;

            Control_SelectedIndexChanged(sender, e);
        }
    }
    public class MetroDateEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroDateEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            // MetroDateTime control = new MetroDateTime();
            System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

            control.Height += 5;

            control.Format = DateTimePickerFormat.Custom;
            control.CustomFormat = "dd MMM yyyy";
            control.MinDate = DateTime.Parse("1/1/1900");

            control.Font = Global.TextFont;
            control.Enabled = Enabled;

            this.Control = control;
            this.Validator = new RegExValidator(RegExpression);


            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };

            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "d",

            };


            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new DateEditor(!Enabled);


            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroTimeEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroTimeEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            // MetroDateTime control = new MetroDateTime();
            System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

            control.Height += 5;

            control.Format = DateTimePickerFormat.Custom;
            control.CustomFormat = "hh:mm:ss";
            control.MinDate = DateTime.Parse("1/1/1900");

            control.Font = Global.TextFont;
            control.Enabled = Enabled;

            this.Control = control;
            this.Validator = new RegExValidator(RegExpression);


            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };

            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "d",

            };


            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new TimeEditor(!Enabled);

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroDateTimeEditor : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroDateTimeEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            // MetroDateTime control = new MetroDateTime();
            System.Windows.Forms.DateTimePicker control = new System.Windows.Forms.DateTimePicker();

            control.Height += 5;

            control.Format = DateTimePickerFormat.Custom;
            control.CustomFormat = "dd MMM yyyy hh:mm:ss";
            control.MinDate = DateTime.Parse("1/1/1900");

            control.Font = Global.TextFont;
            control.Enabled = Enabled;

            this.Control = control;
            this.Validator = new RegExValidator(RegExpression);


            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };

            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "d",

            };


            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new DateTimeEditor(!Enabled);


            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroCheckBoxEditor : BaseMetroEditor
    {
        public MetroCheckBoxEditor(int Width = 200) : base(Width)
        {
            this.BindingName = "Checked";
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }
        public override Control CreateControl()
        {
            MetroCheckBox control = new MetroCheckBox();
            control.Enabled = Enabled;
            control.CheckAlign = ContentAlignment.MiddleRight;
            control.CheckedChanged += Control_CheckedChanged;
            control.BackColor = Color.Transparent;
            control.UseCustomBackColor = true;
            control.Text = "";
            control.FontSize = MetroCheckBoxSize.Medium;

            _height = control.Height + 8;

            this.Control = control;

            return base.CreateControl();
        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MetroCheckBox cntrl = sender as MetroCheckBox;

                cntrl.DataBindings[0].WriteValue();

            }
            catch (Exception x)
            {
            }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {

            Column = new DataGridViewCheckBoxColumn()
            {
                //DataSource = _DataSource,
                //ValueMember = _valueMember,
                //DisplayMember = _displayMember,

                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,


            };


            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new CheckBoxEditor(!Enabled);

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroPercentageEditor : MetroTextBoxEditor
    {
        public MetroPercentageEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }

        public override Control CreateControl()
        {
            RegExpression = RegExpressions.Decimals();
            this.ErrorMessage = string.Format("Only digits are allowed");

            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "##.#0"
            };

            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new DecimalEditor(!Enabled);

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroPasswordEditor : BaseMetroEditor
    {
        MetroTextBox control = new MetroTextBox();

        public string RegExpression = string.Empty;
        public MetroPasswordEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }

        public override Control CreateControl()
        {
            

            control.UseSystemPasswordChar = true;
            control.WaterMark = this.Watermark;

            _height = control.Height + 3;

            this.Control = control;

            this.Validator = new RegExValidator(RegExpression);

            return base.CreateControl();
        }

        public void ShowPassword(bool visible=true){

            control.UseSystemPasswordChar = visible;
        }
    }
    public class MetroTrueFalseEditor : BaseMetroEditor
    {
        public MetroTrueFalseEditor(int Width = 200) : base(Width)
        {
            this.BindingName = "Value";
            this.dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;



        }
        public override Control CreateControl()
        {
            MetroCheckBox control = new MetroCheckBox();
            control.Enabled = Enabled;
            control.CheckAlign = ContentAlignment.MiddleRight;
            control.CheckedChanged += Control_CheckedChanged;
            control.BackColor = Color.Transparent;
            control.UseCustomBackColor = true;
            control.Text = "";
            control.FontSize = MetroCheckBoxSize.Medium;

            _height = control.Height + 8;

            this.Control = control;

            return base.CreateControl();
        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MetroCheckBox cntrl = sender as MetroCheckBox;

                cntrl.DataBindings[0].WriteValue();

            }
            catch (Exception x)
            {
            }
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {

            Column = new DataGridViewComboBoxColumn()
            {
                DataSource = _DataSource,
                ValueMember = _valueMember,
                DisplayMember = _displayMember,

                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat,
                DropDownWidth = _width,

            };


            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            var _list = (IEnumerable<ListDataItem>)_DataSource;

            ICollection values = (ICollection)_list.Select(x => x.Value).ToList();

            ComboBoxListEditor cbo = new ComboBoxListEditor((IEnumerable<ListDataItem>)_DataSource, !Enabled);
            cbo.SelectedIndexChanged -= Cbo_SelectedIndexChanged;
            cbo.SelectedIndexChanged += Cbo_SelectedIndexChanged;

            cbo.ValueType = typeof(bool);

            this.DataGridEditor = cbo;
            this.DataGridEditor.StandardValues = values;
            this.DataGridEditor.StandardValuesExclusive = true;

            return base.CreateDataGridColumn(name, headerText, grid);
        }
        private void Cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = sender as DevAgeComboBox;

            Control_SelectedIndexChanged(sender, e);
        }
    }
    public class MetroButtonEditor : BaseMetroEditor
    {
        SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
        MetroButton control = new MetroButton();

        public string RegExpression = string.Empty;
        public MetroButtonEditor(int Width = 22, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {


            control.Font = Global.TextFont;

            control.Enabled = Enabled;

            control.TextAlign = ContentAlignment.MiddleCenter;

            //control.ForeColor = Color.Black;

            control.Height += 3;

            control.CausesValidation = true;

            this.Control = control;

            if (!string.IsNullOrEmpty(RegExpression))
                this.Validator = new RegExValidator(RegExpression);

            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewButtonColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };
            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            if (this.DataGridEditor == null)
                this.DataGridEditor = new StringEditor();

            var btn = new SourceGrid.Cells.RowHeader("");
            btn.Image = global::easiplan.app.Properties.Resources.save;
            btn.ToolTipText = "Click to open ";

            btn.AddController(clickEvent);

            var col = grid.Columns.Add("", "", btn);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

            return col;

            //return base.CreateDataGridColumn(name, headerText, grid);
        }

        public override BaseMetroEditor OnEditorClick(EventHandler eventHandler)
        {
            clickEvent.Click += eventHandler;

            control.Click += eventHandler;

            return base.OnEditorClick(eventHandler);
        }
    }

    public class MetroSAIDEditor : MetroTextBoxEditor
    {
        public MetroSAIDEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.Validator = new SAIDValidator();
            this.ErrorMessage = string.Format("Invalid South African Id");
        }

        public override Control CreateControl()
        {
            return base.CreateControl();
        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
            };

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new StringEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }
    public class MetroTaxNoEditor : MetroTextBoxEditor
    {
        public MetroTaxNoEditor(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.Validator = new TaxNoValidator();
            this.ErrorMessage = string.Format("Invalid South African Tax Number");
        }

        public override Control CreateControl()
        {
            return base.CreateControl();
        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            FormatStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
            };

            return base.CreateColumn(name, headerText);
        }

        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new StringEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }
    }

    public class MetroHtmlEditor : BaseMetroEditor
    {
        Type _placeHolderType = null;

        public string RegExpression = string.Empty;
        public MetroHtmlEditor(int Width = 200, int Height = 0, Type placeHolderType = null) : base(Width, Height)
        {
            this.BindingName = "InnerHtml;Text";

            _placeHolderType = placeHolderType;
        }
        public override Control CreateControl()
        {
            xHtmlEditor control = new xHtmlEditor();

            control.Font = Global.TextFont;

            if (_placeHolderType != null)
                control.SetPlaceholders(_placeHolderType);

            this.Control = control;

            this.Control = control;
            this.Validator = new RegExValidator(RegExpression);

            return base.CreateControl();
        }

        private void Control_ReadOnlyChanged(object sender, EventArgs e)
        {
            xHtmlEditor textBox = sender as xHtmlEditor;
            if (textBox != null)
                if (textBox.Enabled)
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
                }
                else
                {
                    textBox.BackColor = Color.FromKnownColor(KnownColor.White);
                    textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                }
        }
        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,
            };
            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            this.DataGridEditor = new MultiLineEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }

    }
    #endregion

    public class MetroText : BaseMetroEditor
    {
        public string RegExpression = string.Empty;
        public MetroText(int Width = 200, int Height = 0) : base(Width, Height)
        {
            this.BindingName = "Text";
        }
        public override Control CreateControl()
        {
            Label control = new Label();
            control.Font = Font;
            control.BackColor = Color.Transparent;

            control.Height += 3;

            this.Control = control;

            if (!string.IsNullOrEmpty(RegExpression))
                this.Validator = new RegExValidator(RegExpression);

            return base.CreateControl();
        }

        public override DataGridViewColumn CreateColumn(string name, string headerText)
        {
            Column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,

            };
            return base.CreateColumn(name, headerText);
        }
        public override DataGridColumn CreateDataGridColumn(string name, string headerText, SourceGrid.DataGrid grid)
        {
            if (this.DataGridEditor == null)
                this.DataGridEditor = new StringEditor();

            return base.CreateDataGridColumn(name, headerText, grid);
        }

    }

    #region Validators
    public interface IMetroValidator
    {
        void Validate(object Value, string Message = "");

        bool IsValidated { get; set; }
    }
    public abstract class BaseMetroValidator : IMetroValidator
    {
        public bool IsValidated { get; set; }

        public virtual void Validate(object Value, string Message = "")
        {
            IsValidated = false;
        }
    }

    public class RegExValidator : BaseMetroValidator
    {
        string _RegEx;
        public RegExValidator(string RegEx)
        {
            _RegEx = RegEx;
        }
        public override void Validate(object Value, string Message = "Invalid Value")
        {
            if (!string.IsNullOrEmpty(_RegEx)
                && !string.IsNullOrEmpty(Value as string))
                if (!Regex.IsMatch(Value as string, _RegEx))
                    throw new Exception(Message);

            base.Validate(Value);
        }
    }

    public class SAIDValidator : BaseMetroValidator
    {
        public override void Validate(object Value, string Message = "Id Number is not valid.")
        {
            SaIdValidator validator = new SaIdValidator();

            string idNo = Value as string;
            if (!string.IsNullOrEmpty(idNo))
                if (!validator.Validate(idNo))
                    throw new my.domain.lib.core.Domain.MyValidationException(Message);


            base.Validate(Value);
        }
    }

    public class TaxNoValidator : BaseMetroValidator
    {
        public override void Validate(object Value, string Message = "Tax Number is not valid.")
        {
            string taxNo = Value as string;

            if (!string.IsNullOrEmpty(taxNo))
                if (!F.IsSATaxNumber(taxNo))
                    throw new my.domain.lib.core.Domain.MyValidationException(Message);


            base.Validate(Value);
        }
    }
    #endregion

    public static class RegExpressions
    {
        public static string CharactersAscii(string Includes = "")
        {
            return "^[a-zA-Z" + Includes + "]+$";
        }
        public static string CharactersUnicode(string Includes = "")
        {
            return "^[a-zA-Z\\p{L}" + Includes + "]+$";
        }
        public static string Numbers(string Includes = "")
        {
            return "^[0-9" + Includes + "]+$";
        }
        public static string CharacterNumbersAscii(string Includes = "")
        {
            return "^[a-zA-Z0-9" + Includes + "]+$";
        }

        public static string Decimals(string Includes = ".")
        {
            return "^[0-9" + Includes + "]+$";
        }
        public static string Currency(string Includes = ".")
        {
            return "^R[0-9" + Includes + "]+$";
        }
    }

    public static class KryptoControlExt
    {

        public static KryptonGroupPanel Initialise<T>(this KryptonGroupPanel panel, T bindingSource, Action<ControlBuilder<T>> controlBuilder, ControlsLayout controlsLayout = ControlsLayout.Horizontal, int top = 5, int left = 5, int labelWidth = 120, PropertyChangedEventHandler PropertyChangedHandler = null, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnValidation, EventHandler ChangedEventHandler = null, bool IsLoading = false) where T : BaseEntity<int>
        {
            var builder = new ControlBuilder<T>();
            controlBuilder(builder);

            panel.Hide();

            panel.SuspendLayout();
            panel.AutoScroll = true;

            foreach (var control in builder)
            {
                //Find the Editor Control
                var _cntrl = panel.Controls.Find("editor_" + control.Editor.GetType().Name + "_" + control.Name, true);

                if (_cntrl.Count() > 0)
                {
                    var cntrl = _cntrl[0];

                    #region Update Control Readonly field
                    try
                    {

                        if (cntrl.GetType() == typeof(DevAgeTextBox))
                        {
                            DevAgeTextBox txt = cntrl as DevAgeTextBox;
                            txt.ReadOnly = !control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(DevAgeComboBox))
                        {
                            DevAgeComboBox txt = cntrl as DevAgeComboBox;
                            txt.Enabled = control.Editor.Enabled;
                        }
                        if (cntrl.GetType() == typeof(ExComboBox))
                        {
                            ExComboBox txt = cntrl as ExComboBox;
                            txt.ReadOnly = !control.Editor.Enabled;
                        }

                        if (cntrl.GetType() == typeof(DevAgeNumericUpDown))
                        {
                            DevAgeNumericUpDown txt = cntrl as DevAgeNumericUpDown;
                            // txt.ReadOnly = !control.Editor.Enabled;
                            txt.Enabled = control.Editor.Enabled;
                        }


                        if (cntrl.GetType() == typeof(MetroTrackBar))
                        {
                            MetroTrackBar txt = cntrl as MetroTrackBar;
                            // txt.ReadOnly = !control.Editor.Enabled;
                            txt.Enabled = control.Editor.Enabled;
                        }



                        if (cntrl.GetType() == typeof(MetroComboBox))
                        {
                            MetroComboBox txt = cntrl as MetroComboBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(MetroCheckBox))
                        {
                            MetroCheckBox txt = cntrl as MetroCheckBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(MetroTextBox))
                        {
                            MetroTextBox txt = cntrl as MetroTextBox;
                            txt.Enabled = control.Editor.Enabled;

                        }
                        if (cntrl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
                        {
                            System.Windows.Forms.DateTimePicker txt = cntrl as System.Windows.Forms.DateTimePicker;
                            txt.Enabled = control.Editor.Enabled;
                        }

                    }
                    catch (Exception x)
                    {

                    }
                    #endregion

                    #region Rebind Control
                    if (bindingSource.IsLoading)
                    {
                        BindingSource gridDataBinder = new BindingSource();
                        if (PropertyChangedHandler != null)
                            // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
                            bindingSource.PropertyChanged += PropertyChangedHandler;
                        cntrl.DataBindings.Clear();
                        gridDataBinder.DataSource = bindingSource;
                        string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
                        foreach (string _binding in _bindings.ToList())
                        {
                            Binding b = new Binding(_binding, gridDataBinder, control.Name, true, control.Editor.dataSourceUpdateMode, string.Empty, cntrl.Tag as string);
                            b.Format += new ConvertEventHandler(formatHandler);
                            b.Parse += new ConvertEventHandler(parseHandler);
                            cntrl.DataBindings.Add(b);
                        }


                    }
                    #endregion
                }
                else
                {
                    #region Add Label
                    int lableHeight = 0;
                    if (labelWidth > 0)
                    {
                        MetroLabel label = new MetroLabel() { Text = control.DisplayName };
                        label.Left = left;
                        label.Top = top;
                        label.Width = labelWidth;
                        label.BackColor = Color.Transparent;
                        label.UseCustomBackColor = true;
                        label.Font = Global.LableFont;
                        label.FontSize = MetroLabelSize.Medium;
                        //  label.FontWeight = MetroLabelWeight.Bold;

                        lableHeight += label.Height + 5;

                        label.Name = "lbl_" + control.Name;
                        panel.Controls.Add(label);
                    }
                    #endregion

                    #region Add Editor
                    Control cntrlEditor = null;

                    if (control.Editor == null)
                        control.Editor = new MetroStringEditor() { Enabled = false }; //No editing

                    //Get Required Attribute
                    var _reqAttr = typeof(T).GetProperty(control.Name).CustomAttributes.Where(x => x.AttributeType == typeof(RequiredAttribute));
                    if (_reqAttr.Count() > 0)
                    {
                        var _msg = _reqAttr.FirstOrDefault().NamedArguments[0].TypedValue.Value as string;
                        control.Editor.Watermark = _msg;
                    }

                    //Create editor control
                    cntrlEditor = control.Editor.CreateControl();

                    if (controlsLayout == ControlsLayout.Vertical)
                    {
                        cntrlEditor.Left = left;//padding
                        cntrlEditor.Top = top + lableHeight;
                    }
                    else
                    {
                        cntrlEditor.Left = left + labelWidth + 5;//padding
                        cntrlEditor.Top = top;
                    }

                    cntrlEditor.Name = "editor_" + control.Editor.GetType().Name + "_" + control.Name;


                    #endregion

                    #region Bind Control

                    BindingSource dataSource = new BindingSource();
                    dataSource.DataSource = bindingSource;
                    if (PropertyChangedHandler != null)
                    {
                        dataSource.CurrentItemChanged += ChangedEventHandler;
                        // gridDataBinder.CurrentItemChanged += PropertyChangedHandler;
                        bindingSource.PropertyChanged += PropertyChangedHandler;
                    }

                    cntrlEditor.DataBindings.Clear();

                    string[] _bindings = control.Editor.BindingName.Split(";".ToCharArray()[0]);
                    foreach (string binding in _bindings.ToList())
                    {
                        Binding b = new Binding(binding, dataSource, control.Name, true, control.Editor.dataSourceUpdateMode, string.Empty, cntrlEditor.Tag as string);
                        b.Format += new ConvertEventHandler(formatHandler);
                        b.Parse += new ConvertEventHandler(parseHandler);

                        cntrlEditor.DataBindings.Add(b);
                    }

                    if (cntrlEditor.GetType() == typeof(MetroCheckBox))
                    {
                        cntrlEditor.Text = "";// control.DisplayName;
                        cntrlEditor.Left = left;
                    }
                    #endregion

                    top = cntrlEditor.Top + control.Editor.Size.Height + 2; //padding

                    panel.Controls.Add(cntrlEditor);
                }

            }

            //Set binding source loading to false to prevent rebinding
            bindingSource.IsLoading = IsLoading;

            panel.ResumeLayout();

            panel.Show();

            return panel;
        }


        public static KryptonGroupPanel Format(this KryptonGroupPanel control, string text = "", Image image = null, int padding = 5, int height = 0)
        {
            control.Text = text;
            control.Padding = new Padding(padding);

            control.BackColor = Color.White;
            //control.UseCustomBackColor = true;

            // control.BorderStyle = BorderStyle.FixedSingle;

            if (height > 0)
            {
                control.Height = height;
                //control.VerticalScrollbar = true;
                control.AutoScroll = true;
                //control.VerticalScrollbarBarColor = true;
            }

            return control;
        }

        private static void formatHandler(object sender, ConvertEventArgs e)
        {
            //put code and breakpoint here to inspect e.Value
        }

        private static void parseHandler(object sender, ConvertEventArgs e)
        {
            //put code and breakpoint here to inspect e.Value
        }
    }
}

