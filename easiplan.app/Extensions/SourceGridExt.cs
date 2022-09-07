
//using SourceGrid;
//using SourceGrid.Cells.Editors;
//using SourceGrid.Selection;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using Finx.App.Enums;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.ComponentModel;
//using System.Text.RegularExpressions;
//using DevAge.ComponentModel;
//using DevAge.Drawing;
//using DevAge.Windows.Forms;

using SourceGrid;
using SourceGrid.Cells.Editors;
using SourceGrid.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Finx.App.Enums;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DevAge.ComponentModel;
using DevAge.Drawing;
using DevAge.Windows.Forms;
using Finx.App.UserControls;
using SourceGrid.Cells;
using static MetroFramework.Controls.Ext.MetroControlExt;
using MetroFramework;
using MetroFramework.Controls.Ext;
using DevAge.ComponentModel.Validator;
using my.domain.lib.core.Validation;
using QSS.Components.Windows.Forms;
using easiplan.app.Extensions;
using easiplan.app.ContextMenus;
using SourceGrid.Cells.Controllers;

namespace Finx.App.Extensions
{
    #region OldCode
    //public static class SourceGridExt
    //   {
    //       public static SourceGrid.DataGrid Initialise(this SourceGrid.DataGrid grid, bool ReadOnly=false,bool AllowDelete=true)
    //       {
    //           grid.SuspendLayout();

    //           #region Events
    //           SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           RowHeaderSelectEvent.Click += RowHeaderSelectEvent_Click;

    //           if (AllowDelete)
    //           {
    //               SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //               deleteEvent.Click += DeleteEvent_Click;
    //           }
    //           #endregion

    //           var RowHeaderEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string))
    //           { EditableMode = SourceGrid.EditableMode.None, EnableEdit = false };
    //           RowHeaderEditor.Control.Cursor = Cursors.Hand;

    //           var RowHeader = new SourceGrid.Cells.Cell("");
    //           RowHeader.Editor = RowHeaderEditor;
    //           RowHeader.AddController(RowHeaderSelectEvent);


    //           #region Default Columns
    //           grid.Columns.Clear();

    //           grid.FixedRows = 1;
    //           grid.FixedColumns = 1;

    //           grid.Columns.Insert(0, DataGridColumn.CreateRowHeader(grid));
    //           grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           grid.Columns[0].MaximalWidth = 25;
    //           grid.Columns[0].Width = 25;
    //           grid.Columns[0].DataCell.Editor = RowHeaderEditor;

    //           grid.Controller.AddController(new DataGridCellController());
    //           //grid.Controller.AddController(new KeyDeleteController());

    //           grid.SelectionMode = GridSelectionMode.Row;

    //           #endregion

    //           grid.DeleteRowsWithDeleteKey = false;
    //           grid.CancelEditingWithEscapeKey = true;
    //           grid.EndEditingRowOnValidate = true;

    //           grid.AutoSize = false;
    //           grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    //           grid.AutoStretchColumnsToFitWidth = true;


    //           //Reset the datasource
    //           grid.DataSource = null;

    //           grid.ResumeLayout(true);

    //           return grid;
    //       }

    //       public static void Format(this SourceGrid.DataGrid grid,bool ReadOnly=false, bool AllowDelete = true, bool AllowAddNew = true,bool AlternateBackground = false)
    //       {
    //           grid.SuspendLayout();

    //           #region Header Cell Format
    //           DevAge.Drawing.VisualElements.ColumnHeader bheader = new DevAge.Drawing.VisualElements.ColumnHeader();
    //           bheader.BackColor = Color.GhostWhite;
    //           bheader.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1,Color.GhostWhite, Color.GhostWhite);

    //           SourceGrid.Cells.Views.Header header = new SourceGrid.Cells.Views.Header();
    //           header.Background = bheader;
    //           header.ForeColor = Color.Black;
    //           header.Font = Global.GridFont;// new Font("Verdana", 8, FontStyle.Regular);
    //           header.WordWrap = true;
    //           header.TrimmingMode = TrimmingMode.Word;


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
    //           mView_Amountdisabled.BackColor = Color.WhiteSmoke;

    //           SourceGrid.Cells.Views.Cell mView_Textdisabled = new SourceGrid.Cells.Views.Cell();
    //           mView_Textdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft ;
    //           mView_Textdisabled.BackColor = Color.WhiteSmoke;

    //           //set editor formats/views
    //           for (int i=0;i<grid.Columns.Count;i++)
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
    //           if(AlternateBackground)
    //           foreach (SourceGrid.DataGridColumn colu in grid.Columns)
    //           {
    //               SourceGrid.Conditions.ICondition condition =
    //                   SourceGrid.Conditions.ConditionBuilder.AlternateView(colu.DataCell.View,
    //                                                                        Color.WhiteSmoke, Color.Black);
    //               colu.Conditions.Add(condition);
    //           }
    //           #endregion

    //           grid.Font = Global.GridFont;// new System.Drawing.Font("Verdana", 8);

    //           #region Selection Mode
    //           //grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
    //           //grid.Selection.EnableMultiSelection = false;

    //           SelectionBase SelectionBase = grid.Selection as SelectionBase;

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

    //           #region PopupMenu
    //           PopupMenu menuController = new PopupMenu();

    //           #endregion

    //           #region  Delete Button

    //           if (!ReadOnly && AllowDelete)
    //           {
    //               //Add a delete button
    //               SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //               deleteEvent.Click += DeleteEvent_Click;

    //               var btn = new SourceGrid.Cells.RowHeader("");
    //               btn.Image = easiplan.app.Properties.Resources.delete_button;
    //               btn.ToolTipText = "Click to delete row";
    //               btn.AddController(deleteEvent);

    //               var col = grid.Columns.Add("", "", btn);
    //               col.Width = 22;
    //               col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //               col.MaximalWidth = 22;
    //               col.MinimalWidth = 22;
    //           }
    //           #endregion

    //           grid.DeleteRowsWithDeleteKey = AllowDelete;// !ReadOnly;

    //           if (grid.DataSource != null)
    //           {
    //               grid.DataSource.AllowNew = AllowAddNew;

    //           }


    //           grid.Refresh();

    //           // grid.AutoSizeCells();
    //           grid.Columns.AutoSize(true);
    //           grid.Columns.StretchToFit();

    //           grid.ResumeLayout(true);
    //       }

    //       public static void DeleteEvent_Click(object sender, EventArgs e)
    //       {
    //           CellContext context = (CellContext)sender;
    //           try
    //           {
    //               if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
    //               {
    //                   context.Grid.Selection.FocusRow(context.Position.Row);
    //                   if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
    //                   {
    //                       SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
    //                       // dg.DeleteSelectedRows();

    //                       int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
    //                       if (dataIndex < dg.DataSource.Count)
    //                           dg.DataSource.RemoveAt(dataIndex);
    //                   }
    //               }
    //           }
    //           catch (Exception x1)
    //           {
    //               MessageBoxExt.ShowWarning(x1.Message);
    //           }

    //       }

    //       public static void RowHeaderSelectEvent_Click(object sender, EventArgs e)
    //       {
    //           try
    //           {
    //               CellContext context = (CellContext)sender;
    //               context.Grid.Selection.SelectRow(context.Position.Row, true);
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       static void dataGrid_UserException(object sender, SourceGrid.ExceptionEventArgs e)
    //       {
    //           MessageBoxExt.ShowException(e.Exception);
    //       }

    //       public class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
    //       {
    //           public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
    //           {
    //               base.OnClick(sender, e);

    //             //  MessageBox.Show(sender.Grid, sender.DisplayText);
    //           }
    //       }

    //       public class PopupMenu : SourceGrid.Cells.Controllers.ControllerBase
    //       {
    //           ContextMenu menu = new ContextMenu();
    //           public PopupMenu()
    //           {
    //               menu.MenuItems.Add("Menu 1", new EventHandler(Menu1_Click));
    //               menu.MenuItems.Add("Menu 2", new EventHandler(Menu2_Click));
    //           }

    //           public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
    //           {
    //               base.OnMouseUp(sender, e);

    //               if (e.Button == MouseButtons.Right)
    //                   menu.Show(sender.Grid, new Point(e.X, e.Y));
    //           }

    //           private void Menu1_Click(object sender, EventArgs e)
    //           {
    //               //TODO Your code here
    //           }
    //           private void Menu2_Click(object sender, EventArgs e)
    //           {
    //               //TODO Your code here
    //           }
    //       }

    //       public static void AddColumnButton(this SourceGrid.DataGrid grid,EventHandler eventHandler, bool ReadOnly = false)
    //       {
    //           if (ReadOnly)
    //               return;

    //           SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           clickEvent.Click += eventHandler;

    //           var btn = new SourceGrid.Cells.RowHeader("");
    //           btn.Image = easiplan.app.Properties.Resources.save;
    //           btn.ToolTipText = "Click to add Funds";
    //           btn.AddController(clickEvent);

    //           var col = grid.Columns.Add("", "", btn);
    //           col.Width = 22;
    //           col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
    //           col.MaximalWidth = 22;
    //           col.MinimalWidth = 22;
    //       }

    //       public static void AddButtonColumn(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
    //       {
    //           if (ReadOnly)
    //               return;

    //           SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
    //           clickEvent.Click += eventHandler;

    //           var btn = new SourceGrid.Cells.RowHeader("");
    //           btn.Image = easiplan.app.Properties.Resources.dots_3_128.ToBitmap();
    //           btn.ToolTipText = "Click";

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

    //       public static SourceGrid.DataGrid DataSource<T>(this SourceGrid.DataGrid grid, IList<T> dataSource,ListChangedEventHandler handler=null) where T : class
    //       {

    //           grid.DataSource = new DevAge.ComponentModel.BoundList<T>(dataSource);

    //           if(handler!=null)
    //               grid.DataSource.ListChanged += handler;

    //           return grid;
    //       }

    //       public static SourceGrid.DataGrid Columns<T>(this SourceGrid.DataGrid grid, Action<ColumnBuilder<T>> columnBuilder) where T:class
    //       {
    //           var builder = new ColumnBuilder<T>();
    //           columnBuilder(builder);

    //           foreach (var column in builder)
    //           {
    //               grid.Columns.Add(column.Name, column.DisplayName,column.Editor);
    //           }
    //           return grid;
    //       }

    //       public static DataGridColumn Editor(this DataGridColumn column, EditorControlBase editor)
    //       {
    //           column.DataCell.Editor = editor;

    //           return column;
    //       }

    //       public static List<ListDataItem> ToListDataItem<T>(this IList<T> list, string text, string value)
    //       {
    //           List<ListDataItem> _list = new List<ListDataItem>();

    //           foreach (T l in list)
    //               _list.Add(new ListDataItem() { Text = l.GetType().GetProperty(text).GetValue(l) as string, Value = l.GetType().GetProperty(value).GetValue(l) as string });

    //           return _list;
    //       }
    //       public static List<ListDataItem> ToListDataItem<T>(this IEnumerable<T> list, string text, string value)
    //       {
    //           List<ListDataItem> _list = new List<ListDataItem>();

    //           foreach (T l in list)
    //               _list.Add(new ListDataItem() { Text = l.GetType().GetProperty(text).GetValue(l) as string, Value = l.GetType().GetProperty(value).GetValue(l) as string });

    //           return _list;
    //       }
    //   }
    //   public class ColumnBuilder<T> : IList<GridColumn<T>> where T:class
    //   {
    //     //  private readonly ModelMetadataProvider _metadataProvider;
    //       private readonly List<GridColumn<T>> _Columns = new List<GridColumn<T>>();

    //       public GridColumn<T> this[int index]
    //       {
    //           get
    //           {
    //               return _Columns[index];
    //           }

    //           set
    //           {
    //               _Columns[index] = value;
    //           }
    //       }

    //       public int Count
    //       {
    //           get
    //           {
    //               return _Columns.Count();
    //           }
    //       }

    //       public bool IsReadOnly
    //       {
    //           get
    //           {
    //               throw new NotImplementedException();
    //           }
    //       }

    //       public void Add(GridColumn<T> item)
    //       {
    //           _Columns.Add(item);
    //       }

    //       public void Clear()
    //       {
    //           _Columns.Clear();
    //       }

    //       public bool Contains(GridColumn<T> item)
    //       {
    //           return _Columns.Contains(item);
    //       }

    //       public void CopyTo(GridColumn<T>[] array, int arrayIndex)
    //       {
    //           _Columns.CopyTo(array, arrayIndex);
    //       }

    //       public IEnumerator<GridColumn<T>> GetEnumerator()
    //       {
    //           return _Columns.GetEnumerator();
    //       }

    //       public int IndexOf(GridColumn<T> item)
    //       {
    //           return _Columns.IndexOf(item);
    //       }

    //       public void Insert(int index, GridColumn<T> item)
    //       {
    //           _Columns.Insert(index, item);
    //       }

    //       public bool Remove(GridColumn<T> item)
    //       {
    //          return _Columns.Remove(item);
    //       }

    //       public void RemoveAt(int index)
    //       {
    //           _Columns.RemoveAt(index);
    //       }

    //       IEnumerator IEnumerable.GetEnumerator()
    //       {
    //           return GetEnumerator();
    //       }

    //       GridColumn<T> IList<GridColumn<T>>.this[int index]
    //       {
    //           get { return _Columns[index]; }
    //           set { _Columns[index] = value; }
    //       }

    //       /// <summary>
    //       /// Specifies a column should be constructed for the specified property.
    //       /// </summary>
    //       /// <param name="propertySpecifier">Lambda that specifies the property for which a column should be constructed</param>
    //       public GridColumn<T> For(Expression<Func<T, object>> propertySpecifier,string displayName="",EditorBase editor=null)
    //       {
    //           var memberExpression = GetMemberExpression(propertySpecifier);
    //           var propertyType = GetTypeFromMemberExpression(memberExpression);
    //           var declaringType = memberExpression == null ? null : memberExpression.Expression.Type;
    //           var inferredName = memberExpression == null ? null : memberExpression.Member.Name;
    //           var column = new GridColumn<T>(propertySpecifier.Compile(), inferredName, propertyType,editor);


    //               if (declaringType != null)
    //               {
    //               //    var metadata = _metadataProvider.GetMetadataForProperty(null, declaringType, inferredName);

    //               //    if (!string.IsNullOrEmpty(metadata.DisplayName))
    //               //    {
    //               //        column.Named(metadata.DisplayName);
    //               //    }

    //               //    if (!string.IsNullOrEmpty(metadata.DisplayFormatString))
    //               //    {
    //               //        column.Format(metadata.DisplayFormatString);
    //               //    }
    //               }

    //           if (!string.IsNullOrEmpty(displayName))
    //           {
    //               column.Named(displayName);
    //           }


    //           Add(column);

    //           return column;
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
    //   /// <summary>
    ///// Column for the grid
    ///// </summary>
    //public class GridColumn<T> : IGridColumn<T> where T : class
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
    //      // private List<Func<GridRowViewData<T>, IDictionary<string, object>>> _attributes = new List<Func<GridRowViewData<T>, IDictionary<string, object>>>();
    //       private bool _sortable = true;
    //       private string _sortColumnName = null;
    //       private SortDirection? _initialDirection;
    //       private int? _position;
    //       private Func<object, object> _headerRenderer = x => null;

    //       private EditorBase _editor = null;
    //       /// <summary>
    //       /// Name of the column
    //       /// </summary>
    //       public EditorBase Editor
    //       {
    //           get { return _editor; }
    //       }
    //       /// <summary>
    //       /// Creates a new instance of the GridColumn class
    //       /// </summary>
    //       public GridColumn(Func<T, object> columnValueFunc, string name, Type type, EditorBase editor=null)
    //       {
    //           _name = name;
    //           _displayName = name;
    //           _dataType = type;
    //           _columnValueFunc = columnValueFunc;

    //           if (editor != null)
    //               _editor = editor;
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

    //       IGridColumn<T> IGridColumn<T>.Sortable(bool isColumnSortable)
    //       {
    //           _sortable = isColumnSortable;
    //           return this;
    //       }

    //       IGridColumn<T> IGridColumn<T>.SortColumnName(string name)
    //       {
    //           _sortColumnName = name;
    //           return this;
    //       }

    //       IGridColumn<T> IGridColumn<T>.SortInitialDirection(SortDirection initialDirection)
    //       {
    //           _initialDirection = initialDirection;
    //           return this;
    //       }


    //       IGridColumn<T> IGridColumn<T>.InsertAt(int index)
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

    //       public IGridColumn<T> Named(string name)
    //       {
    //           _displayName = name;
    //           _doNotSplit = true;
    //           return this;
    //       }

    //       public IGridColumn<T> DoNotSplit()
    //       {
    //           _doNotSplit = true;
    //           return this;
    //       }

    //       public IGridColumn<T> Format(string format)
    //       {
    //           _format = format;
    //           return this;
    //       }

    //       public IGridColumn<T> CellCondition(Func<T, bool> func)
    //       {
    //           _cellCondition = func;
    //           return this;
    //       }

    //       IGridColumn<T> IGridColumn<T>.Visible(bool isVisible)
    //       {
    //           _visible = isVisible;
    //           return this;
    //       }

    //       public IGridColumn<T> Header(Func<object, object> headerRenderer)
    //       {
    //           _headerRenderer = headerRenderer;
    //           return this;
    //       }

    //       public IGridColumn<T> Encode(bool shouldEncode)
    //       {
    //           _htmlEncode = shouldEncode;
    //           return this;
    //       }



    //       IGridColumn<T> IGridColumn<T>.HeaderAttributes(IDictionary<string, object> attributes)
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
    //   public interface IGridColumn<T>
    //   {
    //       /// <summary>
    //       /// Specified an explicit name for the column.
    //       /// </summary>
    //       /// <param name="name">Name of column</param>
    //       /// <returns></returns>
    //       IGridColumn<T> Named(string name);
    //       /// <summary>
    //       /// If the property name is PascalCased, it should not be split part.
    //       /// </summary>
    //       /// <returns></returns>
    //       IGridColumn<T> DoNotSplit();
    //       /// <summary>
    //       /// A custom format to use when building the cell's value
    //       /// </summary>
    //       /// <param name="format">Format to use</param>
    //       /// <returns></returns>
    //       IGridColumn<T> Format(string format);
    //       /// <summary>
    //       /// Delegate used to hide the contents of the cells in a column.
    //       /// </summary>
    //       IGridColumn<T> CellCondition(Func<T, bool> func);

    //       /// <summary>
    //       /// Determines whether the column should be displayed
    //       /// </summary>
    //       /// <param name="isVisible"></param>
    //       /// <returns></returns>
    //       IGridColumn<T> Visible(bool isVisible);

    //       IGridColumn<T> Header(Func<object, object> customHeaderRenderer);

    //       /// <summary>
    //       /// Determines whether or not the column should be encoded. Default is true.
    //       /// </summary>
    //       IGridColumn<T> Encode(bool shouldEncode);



    //       /// <summary>
    //       /// Defines additional attributes for the column heading.
    //       /// </summary>
    //       /// <param name="attributes"></param>
    //       /// <returns></returns>
    //       IGridColumn<T> HeaderAttributes(IDictionary<string, object> attributes);

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
    //       IGridColumn<T> Sortable(bool isColumnSortable);

    //       /// <summary>
    //       /// Specifies a custom name that should be used when sorting on this column
    //       /// </summary>
    //       /// <param name="name"></param>
    //       /// <returns></returns>
    //       IGridColumn<T> SortColumnName(string name);

    //       /// <summary>
    //       /// Specifies the direction of the sort link when this column is not currently sorted.  
    //       /// The direction will continue to toggle when it is the currently sorted column. 
    //       /// </summary>
    //       /// <param name="initialDirection"></param>
    //       /// <returns></returns>
    //       IGridColumn<T> SortInitialDirection(SortDirection initialDirection);



    //       /// <summary>
    //       /// Specifies the position of a column. 
    //       /// This is usually used in conjunction with the AutoGenerateColumns method 
    //       /// in order to specify where additional custom columns should be placed.
    //       /// </summary>
    //       /// <param name="index">The index at which the column should be inserted</param>
    //       IGridColumn<T> InsertAt(int index);
    //   }
    //   public enum SortDirection
    //   {
    //       Ascending, Descending
    //   }

    //   public enum FilterOperation
    //   {
    //       Equal, Like, GreaterThan, LessThan
    //   }

    //   #region Editors

    //   public class StringEditor : SourceGrid.Cells.Editors.TextBox
    //  {
    //       public StringEditor(bool ReadOnly=false) :base(typeof(string))
    //	{
    //           if(ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }
    //   }
    //   public class NumericEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    //   {
    //       public NumericEditor(bool ReadOnly = false) : base(typeof(int))
    //       {
    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }
    //   }
    //   public class DecimalEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    //   {
    //       public DecimalEditor(bool ReadOnly = false) : base(typeof(double))
    //       {
    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }
    //   }
    //   public class CurrencyEditor : SourceGrid.Cells.Editors.TextBoxCurrency
    //   {
    //       public CurrencyEditor(bool ReadOnly = false) : base(typeof(double))
    //       {
    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }
    //   }
    //   public class MultiLineEditor : SourceGrid.Cells.Editors.TextBox
    //   {
    //       public MultiLineEditor(bool ReadOnly = false) : base(typeof(string))
    //       {
    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

    //           Control.Multiline = true;
    //           Control.AcceptsReturn = true;
    //           Control.WordWrap = true;
    //           Control.MaxLength = 4000;
    //           Control.ScrollBars = ScrollBars.Vertical;
    //       }
    //   }
    //   public class DateEditor : SourceGrid.Cells.Editors.DateTimePicker //TextBoxDate
    //   {
    //       public DateEditor(bool ReadOnly = false) // base(typeof(DateTime))
    //       {

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }

    //       protected override void OnConvertingValueToDisplayString(ConvertingObjectEventArgs e)
    //       {
    //           if (e.Value != null)
    //           {
    //               DateTime.TryParse(e.Value.ToString(), out DateTime dateTime);

    //               if (dateTime.Date == new DateTime(0001, 1, 1))
    //                   e.Value = "";
    //               else
    //                   e.Value = dateTime.ToString("dd MMM yyyy");

    //               base.OnConvertingValueToDisplayString(e);
    //           }
    //       }
    //   }
    //   public class TimeEditor : SourceGrid.Cells.Editors.TimePicker
    //   {
    //       public TimeEditor(bool ReadOnly = false) 
    //       {
    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }
    //   }
    //   public class DateTimeEditor : SourceGrid.Cells.Editors.DateTimePicker //TextBoxDate
    //   {
    //       public DateTimeEditor(bool ReadOnly = false) // base(typeof(DateTime))
    //       {

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
    //       }

    //       protected override void OnConvertingValueToDisplayString(ConvertingObjectEventArgs e)
    //       {
    //           //try
    //           //{
    //           //    e.Value = DateTime.Parse(e.Value.ToString()).ToString("dd MMM yyyy hh:mm:ss");
    //           //}
    //           //catch (Exception x) { };

    //           base.OnConvertingValueToDisplayString(e);
    //       }
    //   }
    //   public class CheckBoxEditor : EditorControlBase
    //   {
    //       public CheckBoxEditor():base(typeof(bool))
    //       {

    //       }
    //       public CheckBoxEditor(Type p_Type):base(p_Type)
    //	{
    //       }

    //       public CheckBoxEditor(Type p_Type, bool ReadOnly=false) : base(p_Type)
    //       {
    //           Control.Checked = false;
    //           EnableEdit = !ReadOnly;
    //       }
    //       //
    //       // Summary:
    //       //     Gets the control used for editing the cell.
    //       public new  CheckBox Control {
    //           get
    //           {
    //               return (CheckBox)base.Control;
    //           }
    //       }

    //       //
    //       // Summary:
    //       //     Returns the value inserted with the current editor control
    //       public override object GetEditedValue()
    //       {
    //           return Control.Checked;
    //       }
    //       //
    //       // Summary:
    //       //     Set the specified value in the current editor control.
    //       //
    //       // Parameters:
    //       //   editValue:
    //       public override void SetEditValue(object editValue)
    //       {
    //           Control.Checked = (bool)editValue;
    //       }
    //       //
    //       // Summary:
    //       //     Create the editor control
    //       protected override Control CreateControl()
    //       {
    //           CheckBox editor = new CheckBox();

    //           //editor.FlatStyle = FlatStyle.System;
    //          // editor.Validator = this;

    //           //NOTE: I have changed a little the ArrangeLinkedControls to support ComboBox control

    //           return editor;
    //       }
    //       protected override void OnSendCharToEditor(char key)
    //       {
    //       }
    //   }

    //   public class ComboBoxEditor : SourceGrid.Cells.Editors.ComboBox
    //   {
    //       public ComboBoxEditor(bool ReadOnly = false) : base(typeof(string))
    //       {
    //           EnableEdit = !ReadOnly;

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.SingleClick;// SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

    //       }

    //       public ComboBoxEditor(ListDataItemType listDataItemType,bool ReadOnly = false) : this(ReadOnly)
    //       {

    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           ICollection values = (ICollection)Program.listData.Items.Where<ListDataItem>(x => x.ListType == listDataItemType).Select(x=>x.Text).ToList();

    //           this.StandardValues = values;
    //           this.StandardValuesExclusive = false;

    //       }

    //       public ComboBoxEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : this(ReadOnly)
    //       {

    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           //this.StandardValues = standardValues;

    //           //ICollection values = (ICollection)standardValues.Select(x => x.Text).ToList();
    //           //this.StandardValues = values;
    //           //this.StandardValuesExclusive = false;

    //           this.Control.DropDownWidth = 200;
    //       }

    //       public ComboBoxEditor(IEnumerable<string> standardValues, bool ReadOnly = false) : this(ReadOnly)
    //       {

    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           //this.StandardValues = standardValues;


    //           this.StandardValues = (ICollection)standardValues;
    //           this.StandardValuesExclusive = false;


    //       }

    //   }

    //   public class ComboBoxEditor<T> : SourceGrid.Cells.Editors.ComboBox
    //   {
    //       public ComboBoxEditor(IEnumerable<T> standardValues, bool ReadOnly = false) : base(typeof(string))
    //       {
    //           EnableEdit = !ReadOnly;

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.SingleClick;// SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

    //           this.Control.ValueMember = "FundName";
    //           this.Control.DisplayMember = "FundName";
    //           this.Control.DataSource= standardValues;


    //       }
    //   }

    //   public class ComboBoxListEditor : SourceGrid.Cells.Editors.ComboBox
    //   {
    //       public ComboBoxListEditor(bool ReadOnly = false) : base(typeof(string))
    //       {
    //           EnableEdit = !ReadOnly;

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

    //          // this.Control.AutoCompleteMode = AutoCompleteMode.Suggest;
    //           this.Control.DropDownStyle = ComboBoxStyle.DropDownList;
    //           this.Control.DropDownWidth = 200;

    //           this.Control.KeyPress += Control_KeyPress;
    //           this.Control.LostFocus += Control_LostFocus;

    //           this.Control.Validator.AllowNull = true;
    //       }

    //       private void Control_LostFocus(object sender, EventArgs e)
    //       {
    //           DevAgeComboBox cbo = sender as DevAgeComboBox;


    //           cbo.EndUpdate();


    //       }
    //       private void Control_KeyPress(object sender, KeyPressEventArgs e)
    //       {
    //           e.Handled = true;
    //       }

    //       public ComboBoxListEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : this(ReadOnly)
    //       {
    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           ICollection values = (ICollection)Program.listData.Items.Where<ListDataItem>(x => x.ListType == listDataItemType).Select(x => x.Text).ToList();

    //           this.StandardValues = values;
    //           this.StandardValuesExclusive = false;
    //       }



    //       public ComboBoxListEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : this(ReadOnly)
    //       {
    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           //this.StandardValues = standardValues;
    //           this.Control.ValueMember = "Value";
    //           this.Control.DisplayMember = "Text";
    //           //this.Control.DataSource = standardValues;

    //           ICollection values = (ICollection)standardValues.Select(x => x.Text).ToList();
    //           this.StandardValues = values;
    //           this.StandardValuesExclusive = true;
    //       }

    //       public ComboBoxListEditor(IEnumerable<string> standardValues, bool ReadOnly = false) : this(ReadOnly)
    //       {

    //           //this.ValueMember = "Value";
    //           //this.DisplayMember = "Text";
    //           //this.StandardValues = standardValues;

    //           this.StandardValues = (ICollection)standardValues;
    //           this.StandardValuesExclusive = false;

    //       }

    //   }

    //   public class ComboBoxListEditor<T> : SourceGrid.Cells.Editors.ComboBox
    //   {
    //       public ComboBoxListEditor(IEnumerable<T> standardValues, bool ReadOnly = false) : base(typeof(string))
    //       {
    //           EnableEdit = !ReadOnly;

    //           if (ReadOnly)
    //               EditableMode = SourceGrid.EditableMode.None;
    //           else
    //               EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

    //           this.Control.ValueMember = "FundName";
    //           this.Control.DisplayMember = "FundName";
    //           this.Control.DataSource = standardValues;

    //           this.Control.DropDownStyle = ComboBoxStyle.DropDownList;
    //           this.Control.DropDownWidth = 200;

    //       }
    //   }
    //   #endregion

    //   #region Views
    //   public class InstructionsStatusView : SourceGrid.Cells.Views.Cell
    //   {
    //       IDictionary<string, Color> colourScheme = InstructionStatusExt.ColourScheme();
    //       public InstructionsStatusView()
    //       {

    //       }
    //       protected override void PrepareView(CellContext context)
    //       {
    //           base.PrepareView(context);

    //           string str = context.Value as string;
    //           if (string.IsNullOrEmpty(str))
    //               return;

    //           try
    //           {
    //               this.BackColor = Color.WhiteSmoke;

    //               this.ElementText.ForeColor = colourScheme[str]; ;
    //               this.ElementText.Value = str;
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       protected override void PrepareVisualElementText(CellContext context)
    //       {
    //           base.PrepareVisualElementText(context);

    //       }

    //   }

    //   public class InstructionsDaysView : SourceGrid.Cells.Views.Cell
    //   {

    //       public InstructionsDaysView()
    //       {

    //       }
    //       protected override void PrepareView(CellContext context)
    //       {
    //           base.PrepareView(context);

    //           BackColor = Color.Transparent;

    //           int? val = context.Value as int?;

    //           try
    //           {
    //               BackColor = InstructionDaysExt.ToColour(val);
    //               //ForeColor = BackColor;

    //               this.ElementText.ForeColor = Color.White;
    //               this.ElementText.Value = val.ToString();
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       protected override void PrepareVisualElementText(CellContext context)
    //       {
    //           base.PrepareVisualElementText(context);
    //           this.ElementText.Value = "";
    //       }

    //   }

    //   public class ColumnInfoView : SourceGrid.Cells.Views.Cell
    //   {
    //       IDictionary<string, Color> colourScheme = InstructionStatusExt.ColourScheme();
    //       public ColumnInfoView()
    //       {
    //           this.ElementImage = new DevAge.Drawing.VisualElements.Image(easiplan.app.Properties.Resources.save);
    //           this.ImageAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
    //           this.ImageStretch = true;


    //       }
    //       protected override void PrepareView(CellContext context)
    //       {
    //           base.PrepareView(context);

    //           string str = context.Value as string;
    //           if (string.IsNullOrEmpty(str))
    //               return;

    //           try
    //           {
    //               this.BackColor = Color.Transparent;

    //               this.ElementText.ForeColor = colourScheme[str]; ;
    //               this.ElementText.Value = str +  "info";
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       protected override void PrepareVisualElementText(CellContext context)
    //       {


    //           base.PrepareVisualElementText(context);

    //       }

    //       protected override void OnDraw(GraphicsCache graphics, RectangleF area)
    //       {
    //           //var img = easiplan.app.Properties.Resources.save;

    //           //int offset = (area.Width - img.ImageSize.Width) / 2;
    //           //pt.X += offset;
    //           //pt.Y += 1;
    //           //img.Draw(graphics, pt, 0);

    //           //graphics.DrawImage(InfoIcon, )

    //           //Draw the Info Icon
    //           base.OnDraw(graphics, area);
    //       }

    //   }
    //   #endregion

    //   #region Controllers
    //   public class InstructionsController : SourceGrid.Cells.Controllers.ControllerBase
    //   {
    //       SourceGrid.Cells.Views.Cell view1 = new SourceGrid.Cells.Views.Cell();
    //       SourceGrid.Cells.Views.Cell view2 = new SourceGrid.Cells.Views.Cell();

    //       public InstructionsController()
    //       {
    //           view1.BackColor = Color.Red;
    //           view2.BackColor = Color.Green;
    //       }

    //       public override void OnValueChanged(CellContext sender, EventArgs e)
    //       {

    //           base.OnValueChanged(sender, e);

    //           sender.Cell.View = view1;
    //           sender.Grid.InvalidateCell(sender.Position);

    //       }


    //   }
    //   #endregion

    //   #region CellFormat
    //   public class CellFormatTypeView : SourceGrid.Cells.Views.Cell
    //   {

    //       public CellFormatTypeView()
    //       {

    //       }


    //       protected override void PrepareView(CellContext context)
    //       {
    //           base.PrepareView(context);


    //           string str = context.Value as string;
    //           if (string.IsNullOrEmpty(str))
    //               return;

    //           try
    //           {
    //               this.BackColor = Color.WhiteSmoke;
    //               this.ElementText.Value = str;
    //           }
    //           catch (Exception x)
    //           {
    //               Program.Logger.Error(x);
    //           }
    //       }

    //       protected override void PrepareVisualElementText(CellContext context)
    //       {
    //           base.PrepareVisualElementText(context);

    //       }

    //   }
    //#endregion
    #endregion

    public static class SourceGridExt
    {
        public static SourceGrid.DataGrid Initialise(this SourceGrid.DataGrid grid, bool ReadOnly = false, bool AllowDelete = true)
        {
            grid.SuspendLayout();

            #region Events
            SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            RowHeaderSelectEvent.Click += RowHeaderSelectEvent_Click;

            if (AllowDelete)
            {
                SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                deleteEvent.Click += DeleteEvent_Click;
            }
            #endregion

            var RowHeaderEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string))
            { EditableMode = SourceGrid.EditableMode.None, EnableEdit = false };
            RowHeaderEditor.Control.Cursor = Cursors.Hand;

            var RowHeader = new SourceGrid.Cells.Cell("");
            RowHeader.Editor = RowHeaderEditor;
            RowHeader.AddController(RowHeaderSelectEvent);


            #region Default Columns
            grid.Columns.Clear();

            grid.FixedRows = 1;
            grid.FixedColumns = 1;

            grid.Columns.Insert(0, DataGridColumn.CreateRowHeader(grid));
            grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            grid.Columns[0].MaximalWidth = 25;
            grid.Columns[0].Width = 25;
            grid.Columns[0].DataCell.Editor = RowHeaderEditor;

            grid.Controller.AddController(new DataGridCellController());
            //grid.Controller.AddController(new KeyDeleteController());

            grid.SelectionMode = GridSelectionMode.Row;

            #endregion

            grid.DeleteRowsWithDeleteKey = false;
            grid.CancelEditingWithEscapeKey = true;
            grid.EndEditingRowOnValidate = true;

            grid.AutoSize = false;
            grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            grid.AutoStretchColumnsToFitWidth = true;


            //Reset the datasource
            grid.DataSource = null;

            grid.ResumeLayout(true);

            return grid;
        }

        //     public static SourceGrid.DataGrid Initialise1<T>(this SourceGrid.DataGrid grid, IBindingList bindingSource, Action<ColumnBuilder<T>> columnsBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null, EventHandler ItemEditEventHandler = null) where T : class
        //     {

        //         grid.SuspendLayout();

        //         #region Grid Init
        //         grid.Columns.Clear();
        //         grid.LinkedControls.Clear();//remove any stuck cell editor controls

        //         grid.SelectionMode = GridSelectionMode.Row;
        //         grid.AutoSize = false;
        //         grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        //         //grid.AutoStretchColumnsToFitWidth = true;
        //         grid.DeleteRowsWithDeleteKey = false;
        //         grid.CancelEditingWithEscapeKey = true;

        //         grid.Rows.AutoSizeMode = SourceGrid.AutoSizeMode.EnableAutoSizeView;//Disable row Resizing

        //         grid.EnableSort = false;



        //         #endregion

        //         #region Old
        //         /*
        //         #region Row Header
        //         //Event Controller
        //         SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();
        //         if (RowHeaderSelectEventHandler != null)
        //         {
        //             RowHeaderSelectEvent.Click -= RowHeaderSelectEventHandler;
        //             RowHeaderSelectEvent.Click += RowHeaderSelectEventHandler;
        //         }
        //         else
        //         {
        //             RowHeaderSelectEvent.Click -= RowHeaderSelectEvent_Click;
        //             RowHeaderSelectEvent.Click += RowHeaderSelectEvent_Click;
        //         }


        //         var rowHeader = new SourceGrid.Cells.RowHeader("");
        //         if (RowHeaderSelectEventHandler != null)
        //         {
        //             rowHeader.Image = global::easiplan.app.Properties.Resources.arrow_r_128.ToBitmap();
        //             rowHeader.ToolTipText = "Click to show";
        //         }
        //         rowHeader.AddController(RowHeaderSelectEvent);

        //         var colHeader = grid.Columns.Add("", "", rowHeader);
        //         colHeader.AutoSizeMode = SourceGrid.AutoSizeMode.None;
        //         colHeader.MaximalWidth = 25;
        //         colHeader.Width = 25;

        //         //Row Header
        //         // var dgCol = DataGridColumn.CreateRowHeader(grid);
        //         // dgCol.DataCell.AddController(RowHeaderSelectEvent);

        //         // grid.Columns.Insert(0,dgCol);
        //         //grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
        //         //grid.Columns[0].MaximalWidth = 25;
        //         //grid.Columns[0].Width = 25;
        //         //var RowHeaderEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string))
        //         //{ EditableMode = SourceGrid.EditableMode.None, EnableEdit = false };
        //         //RowHeaderEditor.Control.Cursor = Cursors.Hand;
        //         //grid.Columns[0].DataCell.Editor = RowHeaderEditor;

        //         //Delete Controller
        //         //grid.Controller.AddController(new DataGridCellController());
        //         //grid.Controller.AddController(new KeyDeleteController());

        //         #endregion

        //         #region Row Click
        //         grid.Selection.FocusRowEntered += RowSelectEventHandler;
        //         grid.Selection.FocusStyle = FocusStyle.FocusFirstCellOnEnter;
        //         grid.MouseClick += SourceGrid_MouseClick;
        //         #endregion

        //         #region Build Grid Columns
        //         var builder = new ControlBuilder<T>();
        //         controlBuilder(builder);

        //         foreach (var control in builder)
        //         {
        //             DataGridColumn dataGridColumn = null;
        //             if (control.Editor == null)
        //             {
        //                 dataGridColumn = grid.Columns.Add(control.Name, control.DisplayName, typeof(String)); //default editor
        //             }
        //             else
        //             {
        //                 dataGridColumn = control.Editor.CreateDataGridColumn(control.Name, control.DisplayName, grid);

        //                 if (ReadOnly)
        //                     control.Editor.DataGridEditor.EditableMode = EditableMode.None;

        //             }
        //             // Add a custom view for the cell e.g
        //             // dataGridColumn.DataCell.View = new InstructionsStatusView();

        //             if (control.Editor._width != 200)
        //                 dataGridColumn.MinimalWidth = control.Editor._width;

        //             if (!string.IsNullOrEmpty(control.ToolTip))
        //             {

        //                 DataGridColumnTooltip dgct = new DataGridColumnTooltip(control.ToolTip);
        //                 dataGridColumn.HeaderCell.AddController(dgct);

        //                 dataGridColumn.HeaderCell.View = new ColumnInfoView();
        //             }

        //             dataGridColumn.HeaderCell.View.WordWrap = true;
        //         }
        //         #endregion

        //         #region ContextMenuButton
        //         if (ContextMenu != null)
        //         {

        //             DataGridContextMenu dgct = ContextMenu as DataGridContextMenu;

        //             if (dgct.Visible)
        //             {
        //                 var btn = new SourceGrid.Cells.RowHeader("");
        //                 btn.Image = global::easiplan.app.Properties.Resources.dots_3_1281;
        //                 btn.ToolTipText = "Click to show";

        //                 btn.AddController(ContextMenu);

        //                 var col = grid.Columns.Add("", "", btn);
        //                 col.Width = 22;
        //                 col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
        //                 col.MaximalWidth = 22;
        //                 col.MinimalWidth = 22;
        //             }

        //             //override the default contextmenu property
        //             // grid.ContextMenu = dgct.ContextMenu;
        //             grid.Tag = dgct.ContextMenu;


        //         }
        //         #endregion

        //         #region  Edit Button

        //         if (!ReadOnly && AllowEdit)
        //         {
        //             //Add a delete button
        //             SourceGrid.Cells.Controllers.CustomEvents editEvent = new SourceGrid.Cells.Controllers.CustomEvents();
        //             if (ItemEditEventHandler != null)
        //             {
        //                 editEvent.Click -= ItemEditEventHandler;
        //                 editEvent.Click += ItemEditEventHandler;
        //             }


        //             var btnEdit = new SourceGrid.Cells.RowHeader("");
        //             btnEdit.Image = global::easiplan.app.Properties.Resources.dots_3_128.ToBitmap();
        //             btnEdit.ToolTipText = "Click to delete row";
        //             btnEdit.AddController(editEvent);

        //             var col = grid.Columns.Add("", "", btnEdit);
        //             col.Width = 22;
        //             col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
        //             col.MaximalWidth = 22;
        //             col.MinimalWidth = 22;
        //         }
        //         #endregion

        //         #region  Delete Button
        //         grid.DeleteRowsWithDeleteKey = AllowDelete;// !ReadOnly;
        //         if (!ReadOnly && AllowDelete)
        //         {
        //             //Add a delete button
        //             SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();

        //             if (ItemDeleteEventHandler == null)
        //             {
        //                 deleteEvent.Click -= DeleteEvent_Click;
        //                 deleteEvent.Click += DeleteEvent_Click;
        //             }
        //             else
        //             {
        //                 deleteEvent.Click -= ItemDeleteEventHandler;
        //                 deleteEvent.Click += ItemDeleteEventHandler;
        //             }


        //             var btn = new SourceGrid.Cells.RowHeader("");
        //             btn.Image = global::easiplan.app.Properties.Resources.delete;
        //             btn.ToolTipText = "Click to delete row";
        //             btn.AddController(deleteEvent);

        //             var col = grid.Columns.Add("", "", btn);
        //             col.Width = 22;
        //             col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
        //             col.MaximalWidth = 22;
        //             col.MinimalWidth = 22;
        //         }
        //         #endregion
        //         */
        //         #endregion

        //         #region  RowHeader with Select Event Handler
        //         grid.AddRowHeader(RowHeaderSelectEventHandler);
        //         #endregion

        //         #region Add the Columns
        //         //Add the delete event handler when not in ReadOnly mode
        //         if (!ReadOnly && AllowDelete)
        //         {
        //             if(ItemDeleteEventHandler == null)
        //                 ItemDeleteEventHandler = DeleteEvent_Click;//default delete event handler
        //         }
        //         else
        //         {
        //             ItemDeleteEventHandler = null;
        //         }

        //         grid.AddColumns<T>(columnsBuilder, ItemDeleteEventHandler);

        //#endregion

        //#region Add Context Menu 
        //if (ContextMenu != null)
        //         {
        //             DataGridContextMenu dgct = ContextMenu as DataGridContextMenu;
        //             if (dgct.Visible)
        //                 grid.AddContextMenu(ContextMenu);

        //             //DO NOT :override the default contextmenu property
        //             // grid.ContextMenu = dgct.ContextMenu;
        //             grid.Tag = dgct.ContextMenu;

        //             //Show Context Menu on mouse clieck event
        //             grid.MouseClick += SourceGrid_MouseClick;
        //         }
        //#endregion

        //#region Bind the Data Source with PropertyChangedhandler
        //grid.DataSource<T>(bindingSource, PropertyChangedHandler);

        //         if (grid.DataSource != null)
        //             grid.DataSource.AllowNew = AllowAddNew;

        //         #endregion

        //         #region Row Select Event Handler
        //         grid.Selection.FocusRowEntered += RowSelectEventHandler;
        //         grid.Selection.FocusStyle = FocusStyle.FocusFirstCellOnEnter;            
        //         #endregion

        //         grid.ResumeLayout();

        //         return grid;
        //     }

        public static SourceGrid.DataGrid Initialise1<T>(this SourceGrid.DataGrid grid, IList<T> bindingSource, Action<ColumnBuilder<T>> columnsBuilder, int top = 5, int left = 5, bool AllowDelete = true, bool AllowAddNew = true, bool AllowEdit = false, bool ReadOnly = true, ListChangedEventHandler PropertyChangedHandler = null, EventHandler ItemDeleteEventHandler = null, ItemDeletedEventHandler ItemDeletedEventHandler = null, EventHandler RowHeaderSelectEventHandler = null, RowEventHandler RowSelectEventHandler = null, ListChangedEventHandler ListChangedEventHandler = null, SourceGrid.Cells.Controllers.ControllerBase ContextMenu = null, EventHandler ItemEditEventHandler = null) where T : class
        {

            grid.SuspendLayout();

            #region Grid Init
            grid.Columns.Clear();
            grid.LinkedControls.Clear();//remove any stuck cell editor controls

            grid.SelectionMode = GridSelectionMode.Row;
            grid.AutoSize = false;
            grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //grid.AutoStretchColumnsToFitWidth = true;
            grid.DeleteRowsWithDeleteKey = false;
            grid.CancelEditingWithEscapeKey = true;

            grid.Rows.AutoSizeMode = SourceGrid.AutoSizeMode.EnableAutoSizeView;//Disable row Resizing

            grid.EnableSort = false;



            #endregion

            #region Old
            /*
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
            */
            #endregion

            #region  RowHeader with Select Event Handler
            grid.AddRowHeader(RowHeaderSelectEventHandler);
            #endregion

            #region Add the Columns
            //Add the delete event handler when not in ReadOnly mode
            if (!ReadOnly && AllowDelete)
            {
                if (ItemDeleteEventHandler == null)
                    ItemDeleteEventHandler = DeleteEvent_Click;//default delete event handler
            }
            else
            {
                ItemDeleteEventHandler = null;
            }

            grid.AddColumns<T>(columnsBuilder, ItemDeleteEventHandler);

            #endregion

            #region Add Context Menu 
            if (ContextMenu != null)
            {
                DataGridContextMenu dgct = ContextMenu as DataGridContextMenu;
                if (dgct.Visible)
                    grid.AddContextMenu(ContextMenu);

                //DO NOT :override the default contextmenu property
                // grid.ContextMenu = dgct.ContextMenu;
                grid.Tag = dgct.ContextMenu;

                //Show Context Menu on mouse clieck event
                grid.MouseClick += SourceGrid_MouseClick;
            }
            #endregion

            #region Bind the Data Source with PropertyChangedhandler
            grid.DataSource<T>(bindingSource, PropertyChangedHandler);

            if (grid.DataSource != null)
                grid.DataSource.AllowNew = AllowAddNew;

            #endregion

            #region Row Select Event Handler
            grid.Selection.FocusRowEntered += RowSelectEventHandler;
            grid.Selection.FocusStyle = FocusStyle.FocusFirstCellOnEnter;
            #endregion

            grid.ResumeLayout();

            return grid;
        }

        public static void Format(this SourceGrid.DataGrid grid, bool ReadOnly = false, bool AllowDelete = true, bool AllowAddNew = true, bool AlternateBackground = false)
        {
            grid.SuspendLayout();

            #region Header Cell Format
            DevAge.Drawing.VisualElements.ColumnHeader bheader = new DevAge.Drawing.VisualElements.ColumnHeader();
            bheader.BackColor = Color.GhostWhite;
            bheader.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1, Color.GhostWhite, Color.GhostWhite);

            SourceGrid.Cells.Views.Header header = new SourceGrid.Cells.Views.Header();
            header.Background = bheader;
            header.ForeColor = Color.Black;
            header.Font = Global.GridFont;// new Font("Verdana", 8, FontStyle.Regular);
            header.WordWrap = true;
            header.TrimmingMode = TrimmingMode.Word;


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
            mView_Amountdisabled.BackColor = Color.WhiteSmoke;

            SourceGrid.Cells.Views.Cell mView_Textdisabled = new SourceGrid.Cells.Views.Cell();
            mView_Textdisabled.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
            mView_Textdisabled.BackColor = Color.WhiteSmoke;

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

            grid.Font = Global.GridFont;// new System.Drawing.Font("Verdana", 8);

            #region Selection Mode
            //grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            //grid.Selection.EnableMultiSelection = false;

            SelectionBase SelectionBase = grid.Selection as SelectionBase;

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


            #region  Delete Button

            if (!ReadOnly && AllowDelete)
            {
                //Add a delete button
                SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                deleteEvent.Click += DeleteEvent_Click;

                var btn = new SourceGrid.Cells.RowHeader("");
                btn.Image = easiplan.app.Properties.Resources.delete_button;
                btn.ToolTipText = "Click to delete row";
                btn.AddController(deleteEvent);

                var col = grid.Columns.Add("", "", btn);
                col.Width = 22;
                col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                col.MaximalWidth = 22;
                col.MinimalWidth = 22;
            }
            #endregion

            grid.DeleteRowsWithDeleteKey = AllowDelete;// !ReadOnly;

            if (grid.DataSource != null)
            {
                grid.DataSource.AllowNew = AllowAddNew;

            }


            grid.Refresh();

            // grid.AutoSizeCells();
            grid.Columns.AutoSize(true);
            grid.Columns.StretchToFit();

            grid.ResumeLayout(true);
        }

        public static SourceGrid.DataGrid Format1(this SourceGrid.DataGrid grid, bool ReadOnly = false, bool AllowDelete = true, bool AllowAddNew = true, GridFormats format = GridFormats.Default, bool AlternateBackground = false, int fixedCols = 0)
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
            header.ImageAlignment = DevAge.Drawing.ContentAlignment.TopRight;

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

            SelectionBase.BackColor = Color.FromArgb(75, Color.FromKnownColor(KnownColor.LightSteelBlue));

            DevAge.Drawing.RectangleBorder border = SelectionBase.Border;
            border.SetWidth(1);
            border.SetColor(Color.DarkGray);
            SelectionBase.Border = border;

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

            FormatGrid(grid, format);

            grid.FixedColumns = fixedCols + 1;

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

                        grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
                        grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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

        public static void DeleteEvent_Click(object sender, EventArgs e)
        {
            CellContext context = (CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);
                    if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
                    {
                        SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;
                        // dg.DeleteSelectedRows();

                        int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);
                        if (dataIndex < dg.DataSource.Count)
                            dg.DataSource.RemoveAt(dataIndex);
                    }
                }
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }

        }

        public static void RowHeaderSelectEvent_Click(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;
                context.Grid.Selection.SelectRow(context.Position.Row, true);
            }
            catch (Exception x)
            {
            }
        }

        static void dataGrid_UserException(object sender, SourceGrid.ExceptionEventArgs e)
        {
            MessageBoxExt.ShowException(e.Exception);
        }
        private static void SourceGrid_MouseClick(object sender, MouseEventArgs e)
        {

            SourceGrid.DataGrid dg = sender as SourceGrid.DataGrid;

            if (e.Button == MouseButtons.Right && dg.Visible)
            {
                if (dg.MouseDownPosition.Row <= 0)
                    return;

                dg.Selection.FocusRow(dg.MouseDownPosition.Row);

                ContextMenu ctxMenu = dg.Tag as ContextMenu;

                if (ctxMenu != null)
                    ctxMenu.Show(dg, new Point(e.X, e.Y));

            }
        }

        public static void AddButtonColumn(this SourceGrid.DataGrid grid, EventHandler eventHandler, System.Drawing.Image image = null, bool ReadOnly = false)
        {
            if (ReadOnly)
                return;

            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.Click += eventHandler;

            var btn = new SourceGrid.Cells.RowHeader("");
            btn.Image = image == null ? easiplan.app.Properties.Resources.dots_3_128.ToBitmap() : image;
            btn.ToolTipText = "Click";

            btn.AddController(clickEvent);

            var col = grid.Columns.Add("", "", btn);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

        }

        public static void AddRowHeader(this SourceGrid.DataGrid grid, EventHandler RowHeaderSelectEventHandler, System.Drawing.Image image = null, bool ReadOnly = false)
        {
            if (ReadOnly)
                return;

            var rowHeader = new SourceGrid.Cells.RowHeader("");

            var col = grid.Columns.Add("", "", rowHeader);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 25;
            col.MinimalWidth = 25;


            if (RowHeaderSelectEventHandler != null)
            {
                SourceGrid.Cells.Controllers.CustomEvents RowHeaderSelectEvent = new SourceGrid.Cells.Controllers.CustomEvents();

                rowHeader.Image = image == null ? global::easiplan.app.Properties.Resources.arrow_r_128.ToBitmap() : image;
                rowHeader.ToolTipText = "Click to show";

                RowHeaderSelectEvent.Click -= RowHeaderSelectEventHandler;
                RowHeaderSelectEvent.Click += RowHeaderSelectEventHandler;

                rowHeader.AddController(RowHeaderSelectEvent);
            }
        }

        public static void AddRowClickEvent(this SourceGrid.DataGrid grid, EventHandler eventHandler, bool ReadOnly = false)
        {
            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents ClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            ClickEvent.FocusEntered += eventHandler;
            grid.Controller.AddController(ClickEvent); //Event fired when any column is clicked

        }

        public static void AddContextMenu(this SourceGrid.DataGrid grid, ControllerBase contextMenu, System.Drawing.Image image = null, bool ReadOnly = false)
        {
            if (ReadOnly)
                return;

            var btn = new SourceGrid.Cells.RowHeader("");
            btn.Image = image == null ? easiplan.app.Properties.Resources.dots_3_128.ToBitmap() : image;
            btn.ToolTipText = "Click";

            btn.AddController(contextMenu);

            var col = grid.Columns.Add("", "", btn);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

        }

        public static SourceGrid.DataGrid DataSource<T>(this SourceGrid.DataGrid grid, IList<T> sourceList, ListChangedEventHandler listChangedHandler = null, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnValidation) where T : class
        {

            DevAge.ComponentModel.BoundList<T> mBoundList = new DevAge.ComponentModel.BoundList<T>(sourceList);

            //mBoundList.ListChanged += new ListChangedEventHandler(listChangedHandler);
            //mBoundList.ItemDeleted -= ItemDeletedEventHandler;
            //mBoundList.ItemDeleted += ItemDeletedEventHandler;

            grid.DataSource = mBoundList;

            if (listChangedHandler != null)
            {
                grid.DataSource.ListChanged -= listChangedHandler;
                grid.DataSource.ListChanged += listChangedHandler;
            }

            grid.DataBindings.DefaultDataSourceUpdateMode = dataSourceUpdateMode;

            return grid;
        }

        public static SourceGrid.DataGrid AddColumns<T>(this SourceGrid.DataGrid grid, Action<ColumnBuilder<T>> columnBuilder, EventHandler ItemDeleteEventHandler = null) where T : class
        {
            var builder = new ColumnBuilder<T>();
            columnBuilder(builder);

            foreach (var column in builder)
            {
                DataGridColumn dgCol = null;

                if (column.Editor == null)
                {
                    ICellVirtual cell = SourceGrid.Cells.DataGrid.Cell.Create(column.ColumnType, editable: column.ColumnFormat.Editable);

                    cell.Editor.EditableMode = !column.ColumnFormat.Editable ? EditableMode.None : EditableMode.SingleClick;

                    cell.Editor.EditException -= Editor_EditException;
                    cell.Editor.EditException += Editor_EditException;

                    dgCol = grid.Columns.Add(column.Name, column.DisplayName, cell);
                }
                else
                {
                    SourceGrid.Cells.DataGrid.Cell cell = new SourceGrid.Cells.DataGrid.Cell();

                    cell.Editor = column.Editor;

                    cell.Editor.EditException -= Editor_EditException;
                    cell.Editor.EditException += Editor_EditException;

                    dgCol = grid.Columns.Add(column.Name, column.DisplayName, cell);
                }

                if (column.ColumnFormat.MinWidth > 0)
                    dgCol.MinimalWidth = column.ColumnFormat.MinWidth;

                if (column.ColumnFormat.Width > 0)
                {
                    dgCol.Width = column.ColumnFormat.Width;
                    dgCol.MaximalWidth = column.ColumnFormat.Width;
                    dgCol.MinimalWidth = column.ColumnFormat.Width;
                }
                if (!string.IsNullOrEmpty(column.ColumnFormat.Tooltip))
                {

                    DataGridColumnTooltip dgct = new DataGridColumnTooltip(column.ColumnFormat.Tooltip);
                    dgCol.HeaderCell.AddController(dgct);

                    dgCol.HeaderCell.View = new ColumnInfoView();
                }


            }

            //Add a delete button
            if (ItemDeleteEventHandler != null)
                grid.AddButtonColumn(ItemDeleteEventHandler, global::easiplan.app.Properties.Resources.delete);


            return grid;
        }

        public static DataGridColumn Editor(this DataGridColumn column, EditorControlBase editor)
        {
            column.DataCell.Editor = editor;

            return column;
        }
        private static void Editor_EditException(object sender, ExceptionEventArgs e)
        {
            EditorControlBase cntrl = sender as EditorControlBase;

            if (cntrl != null)
                if (cntrl.Control != null)
                    cntrl.Control.ShowTooltip(e.Exception.Message, "Error", "Error");

            e.Handled = true;
        }

    }
    public class ColumnBuilder<T> : IList<GridColumn<T>> where T : class
    {
        //  private readonly ModelMetadataProvider _metadataProvider;
        private readonly List<GridColumn<T>> _Columns = new List<GridColumn<T>>();

        public GridColumn<T> this[int index]
        {
            get
            {
                return _Columns[index];
            }

            set
            {
                _Columns[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _Columns.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(GridColumn<T> item)
        {
            _Columns.Add(item);
        }

        public void Clear()
        {
            _Columns.Clear();
        }

        public bool Contains(GridColumn<T> item)
        {
            return _Columns.Contains(item);
        }

        public void CopyTo(GridColumn<T>[] array, int arrayIndex)
        {
            _Columns.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GridColumn<T>> GetEnumerator()
        {
            return _Columns.GetEnumerator();
        }

        public int IndexOf(GridColumn<T> item)
        {
            return _Columns.IndexOf(item);
        }

        public void Insert(int index, GridColumn<T> item)
        {
            _Columns.Insert(index, item);
        }

        public bool Remove(GridColumn<T> item)
        {
            return _Columns.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _Columns.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        GridColumn<T> IList<GridColumn<T>>.this[int index]
        {
            get { return _Columns[index]; }
            set { _Columns[index] = value; }
        }

        /// <summary>
        /// Specifies a column should be constructed for the specified property.
        /// </summary>
        /// <param name="propertySpecifier">Lambda that specifies the property for which a column should be constructed</param>
        public GridColumn<T> For(Expression<Func<T, object>> propertySpecifier, string displayName = "", EditorBase editor = null, bool Editable = true, int Width = 0, int MinWidth = 0, string Tooltip = "")
        {
            var memberExpression = GetMemberExpression(propertySpecifier);
            var propertyType = GetTypeFromMemberExpression(memberExpression);
            var declaringType = memberExpression == null ? null : memberExpression.Expression.Type;
            var inferredName = memberExpression == null ? null : memberExpression.Member.Name;
            var column = new GridColumn<T>(propertySpecifier.Compile(), inferredName, propertyType, editor);


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

            if (!string.IsNullOrEmpty(displayName))
            {
                column.Named(displayName);
            }

            if (Width > 0) column.ColumnFormat.Width = Width;
            if (MinWidth > 0) column.ColumnFormat.MinWidth = MinWidth;
            column.ColumnFormat.Editable = Editable;

            column.ColumnFormat.Tooltip = Tooltip;

            Add(column);

            return column;
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
    /// <summary>
	/// Column for the grid
	/// </summary>
	public class GridColumn<T> : IGridColumn<T> where T : class
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
        private ColumnFormat _columnFormat = new ColumnFormat();

        private EditorBase _editor = null;
        /// <summary>
        /// Editor Control of the column
        /// </summary>
        public EditorBase Editor
        {
            get { return _editor; }
        }
        /// <summary>
        /// Creates a new instance of the GridColumn class
        /// </summary>
        public GridColumn(Func<T, object> columnValueFunc, string name, Type type, EditorBase editor = null, ColumnFormat columnFormat = null)
        {
            _name = name;
            _displayName = name;
            _dataType = type;
            _columnValueFunc = columnValueFunc;

            if (editor != null)
                _editor = editor;

            if (columnFormat != null)
                _columnFormat = columnFormat;
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

        IGridColumn<T> IGridColumn<T>.Sortable(bool isColumnSortable)
        {
            _sortable = isColumnSortable;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.SortColumnName(string name)
        {
            _sortColumnName = name;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.SortInitialDirection(SortDirection initialDirection)
        {
            _initialDirection = initialDirection;
            return this;
        }


        IGridColumn<T> IGridColumn<T>.InsertAt(int index)
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

        public IGridColumn<T> Named(string name)
        {
            _displayName = name;
            _doNotSplit = true;
            return this;
        }

        public IGridColumn<T> DoNotSplit()
        {
            _doNotSplit = true;
            return this;
        }

        public IGridColumn<T> Format(string format)
        {
            _format = format;
            return this;
        }

        public IGridColumn<T> CellCondition(Func<T, bool> func)
        {
            _cellCondition = func;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Visible(bool isVisible)
        {
            _visible = isVisible;
            return this;
        }

        public IGridColumn<T> Header(Func<object, object> headerRenderer)
        {
            _headerRenderer = headerRenderer;
            return this;
        }

        public IGridColumn<T> Encode(bool shouldEncode)
        {
            _htmlEncode = shouldEncode;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.HeaderAttributes(IDictionary<string, object> attributes)
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

        public ColumnFormat ColumnFormat { get { return _columnFormat; } set { _columnFormat = value; } }
    }
    public interface IGridColumn<T>
    {
        /// <summary>
        /// Specified an explicit name for the column.
        /// </summary>
        /// <param name="name">Name of column</param>
        /// <returns></returns>
        IGridColumn<T> Named(string name);
        /// <summary>
        /// If the property name is PascalCased, it should not be split part.
        /// </summary>
        /// <returns></returns>
        IGridColumn<T> DoNotSplit();
        /// <summary>
        /// A custom format to use when building the cell's value
        /// </summary>
        /// <param name="format">Format to use</param>
        /// <returns></returns>
        IGridColumn<T> Format(string format);
        /// <summary>
        /// Delegate used to hide the contents of the cells in a column.
        /// </summary>
        IGridColumn<T> CellCondition(Func<T, bool> func);

        /// <summary>
        /// Determines whether the column should be displayed
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        IGridColumn<T> Visible(bool isVisible);

        IGridColumn<T> Header(Func<object, object> customHeaderRenderer);

        /// <summary>
        /// Determines whether or not the column should be encoded. Default is true.
        /// </summary>
        IGridColumn<T> Encode(bool shouldEncode);



        /// <summary>
        /// Defines additional attributes for the column heading.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IGridColumn<T> HeaderAttributes(IDictionary<string, object> attributes);

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
        IGridColumn<T> Sortable(bool isColumnSortable);

        /// <summary>
        /// Specifies a custom name that should be used when sorting on this column
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IGridColumn<T> SortColumnName(string name);

        /// <summary>
        /// Specifies the direction of the sort link when this column is not currently sorted.  
        /// The direction will continue to toggle when it is the currently sorted column. 
        /// </summary>
        /// <param name="initialDirection"></param>
        /// <returns></returns>
        IGridColumn<T> SortInitialDirection(SortDirection initialDirection);



        /// <summary>
        /// Specifies the position of a column. 
        /// This is usually used in conjunction with the AutoGenerateColumns method 
        /// in order to specify where additional custom columns should be placed.
        /// </summary>
        /// <param name="index">The index at which the column should be inserted</param>
        IGridColumn<T> InsertAt(int index);

        //IGridColumn<T> SetColumnFormat(ColumnFormat columnFormat);
        ColumnFormat ColumnFormat { get; set; }
    }
    public enum SortDirection
    {
        Ascending, Descending
    }

    public enum FilterOperation
    {
        Equal, Like, GreaterThan, LessThan
    }

    public class ColumnFormat
    {
        public int MinWidth { get; set; } = 20;
        public int Width { get; set; }
        public bool Editable { get; set; } = true;
        public string Tooltip { get; set; }

    }
    #region Editors

    public class StringEditor : SourceGrid.Cells.Editors.TextBox
    {
        public StringEditor(bool ReadOnly = false) : base(typeof(string))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

    }
    public class NumericEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    {
        public NumericEditor(bool ReadOnly = false) : base(typeof(int))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class NumberEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    {
        public NumberEditor(bool ReadOnly = false) : base(typeof(int))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class DecimalEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    {
        public DecimalEditor(bool ReadOnly = false) : base(typeof(double))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class PercentageEditor : SourceGrid.Cells.Editors.TextBoxNumeric
    {
        public PercentageEditor(bool ReadOnly = false) : base(typeof(double))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class CurrencyEditor : SourceGrid.Cells.Editors.TextBoxCurrency
    {
        public CurrencyEditor(bool ReadOnly = false) : base(typeof(double))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class MultiLineEditor : SourceGrid.Cells.Editors.TextBox
    {
        public MultiLineEditor(bool ReadOnly = false) : base(typeof(string))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

            Control.Multiline = true;
            Control.AcceptsReturn = true;
            Control.WordWrap = true;
            Control.MaxLength = 4000;
            Control.ScrollBars = ScrollBars.Vertical;
        }
    }
    public class DateEditor : SourceGrid.Cells.Editors.DateTimePicker //TextBoxDate
    {
        public DateEditor(bool ReadOnly = false) // base(typeof(DateTime))
        {

            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        protected override void OnConvertingValueToDisplayString(ConvertingObjectEventArgs e)
        {
            try
            {
                if (e.Value == null)
                    return;

                e.Value = DateTime.Parse(e.Value.ToString()).ToString("dd MMM yyyy");
            }
            catch (Exception x) { };

            base.OnConvertingValueToDisplayString(e);
        }
    }
    public class TimeEditor : SourceGrid.Cells.Editors.TimePicker
    {
        public TimeEditor(bool ReadOnly = false)
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }
    }
    public class DateTimeEditor : SourceGrid.Cells.Editors.DateTimePicker //TextBoxDate
    {
        public DateTimeEditor(bool ReadOnly = false) // base(typeof(DateTime))
        {

            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        protected override void OnConvertingValueToDisplayString(ConvertingObjectEventArgs e)
        {
            //try
            //{
            //    e.Value = DateTime.Parse(e.Value.ToString()).ToString("dd MMM yyyy hh:mm:ss");
            //}
            //catch (Exception x) { };

            base.OnConvertingValueToDisplayString(e);
        }
    }
    public class CheckBoxEditor : EditorControlBase
    {
        public new DevAgeCheckBox Control => (DevAgeCheckBox)base.Control;

        public CheckBoxEditor() : base(typeof(bool))
        {

        }

        public CheckBoxEditor(bool ReadOnly = false) : this()
        {
            EnableEdit = !ReadOnly;
        }

        //
        // Summary:
        //     Returns the value inserted with the current editor control
        public override object GetEditedValue()
        {
            return Control.Checked;
        }
        //
        // Summary:
        //     Set the specified value in the current editor control.
        //
        // Parameters:
        //   editValue:
        public override void SetEditValue(object editValue)
        {
            Control.Checked = (bool)editValue;
        }
        //
        // Summary:
        //     Create the editor control
        protected override Control CreateControl()
        {
            DevAgeCheckBox control = new DevAgeCheckBox();
            control.Validator = this;
            return control;
        }
        protected override void OnSendCharToEditor(char key)
        {
        }
    }

    #region ComboBox Editor
    public class ComboBoxEditor<T, P> : SourceGrid.Cells.Editors.ComboBox
    {
        internal ExComboBox control = new ExComboBox();

        private BindingSource bSource = new BindingSource();

        public ComboBoxEditor(bool ReadOnly = false) : base(typeof(P))
        {
            EnableEdit = !ReadOnly;

            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.Focus;//| SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        public ComboBoxEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : this(ReadOnly)
        {
            var dataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == listDataItemType).ToList();

            this.StandardValues = dataSource.Select(x => x.Value).ToList();

            control.BindingContext = new BindingContext();
            bSource.DataSource = dataSource;

            control.DataSource = bSource;
            control.ValueMember = "Value";
            control.DisplayMember = "Text";

        }

        public ComboBoxEditor(IEnumerable<ListDataItem> dataSource, bool ReadOnly = false) : this(ReadOnly)
        {

            this.StandardValues = dataSource.Select(x => x.Value).ToList();

            control.BindingContext = new BindingContext();
            bSource.DataSource = dataSource;

            control.DataSource = bSource;
            control.ValueMember = "Value";
            control.DisplayMember = "Text";
        }

        //public ComboBoxEditor(IEnumerable<T> dataSource, bool ReadOnly = false, string valueMember = "Value", string displayMember = "Text") :this(ReadOnly)
        //{

        //    // TO DO: this.StandardValues = dataSource.Select(x => x.).ToList();

        //    control.BindingContext = new BindingContext();
        //    bSource.DataSource = dataSource;

        //    control.DataSource = bSource;
        //    control.ValueMember = valueMember;
        //    control.DisplayMember = displayMember;


        //}

        /// <summary>
        /// Create our own custom editor control
        /// </summary>
        /// <returns></returns>
        protected override Control CreateControl()
        {

            control.Font = Global.TextFont;
            control.FlatStyle = FlatStyle.Flat;

            //control.SelectedIndexChanged += Control_SelectedIndexChanged;

            return control;
        }
        public override void SetEditValue(object editValue)
        {
            if (control == null)
                return;

            if (editValue is string && IsStringConversionSupported() &&
                    control.DropDownStyle == ComboBoxStyle.DropDown)
            {
                control.SelectedIndex = -1; //Clear the text
                control.Text = (string)editValue;
                control.SelectionLength = 0;
                if (control.Text != null)
                    control.SelectionStart = control.Text.Length;
                else
                    control.SelectionStart = 0;
            }
            else
            {
                control.SelectedIndex = -1;//clear the text
                if (editValue != null && editValue.ToString() != string.Empty)
                {
                    try
                    {
                        control.SelectedValue = editValue;
                    }
                    catch (Exception x) { };

                    if (control.SelectedValue == null) //not in the select list
                    {
                        //Add the ListDataItem
                        bSource.Add(new ListDataItem() { Value = editValue, Text = editValue.ToString() });
                        bSource.ResetBindings(false);

                        this.StandardValues = bSource.ToList<ListDataItem>().Select(x => x.Value).ToList();

                        control.SelectedValue = editValue;

                    }
                }
            }

        }
        public override object GetEditedValue()
        {
            return control != null ? control.SelectedValue == null ? control.Text : control.SelectedValue : null;

        }
        protected override void OnSendCharToEditor(char key)
        {
            if (control != null && control.DropDownStyle == ComboBoxStyle.DropDown)
            {
                control.Text = key.ToString();
                if (control.Text != null)
                    control.SelectionStart = control.Text.Length;
            }
        }
        public override bool SetCellValue(CellContext cellContext, object p_NewValue)
        {
            try
            {
                return base.SetCellValue(cellContext, p_NewValue);
            }
            catch (Exception x)
            {

            }
            return false;
        }
    }

    public class ComboBoxEditor<P> : ComboBoxEditor<ListDataItem, P>
    {
        public ComboBoxEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : base(listDataItemType, ReadOnly)
        {
        }
        public ComboBoxEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : base(standardValues, ReadOnly)
        {
        }
    }

    public class ComboBoxEditor : ComboBoxEditor<string>
    {
        public ComboBoxEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : base(listDataItemType, ReadOnly)
        {
        }
        public ComboBoxEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : base(standardValues, ReadOnly)
        {
        }
    }

    #endregion

    #region ComboList Editor
    public class ComboListEditor<T, P> : ComboBoxEditor<T, P>
    {

        public ComboListEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : base(listDataItemType, ReadOnly)
        {
            this.control.DropDownStyle = ComboBoxStyle.DropDownList;
            this.StandardValuesExclusive = true;
        }

        public ComboListEditor(IEnumerable<ListDataItem> dataSource, bool ReadOnly = false) : base(dataSource, ReadOnly)
        {
            this.control.DropDownStyle = ComboBoxStyle.DropDownList;
            this.StandardValuesExclusive = true;
        }

        //public ComboListEditor(IEnumerable<T> dataSource, bool ReadOnly = false, string valueMember = "Value", string displayMember = "Text") : base(dataSource,ReadOnly,valueMember,displayMember)
        //{
        //    this.control.DropDownStyle = ComboBoxStyle.DropDownList;
        //    this.StandardValuesExclusive = true;
        //}

    }

    public class ComboListEditor<P> : ComboListEditor<ListDataItem, P>
    {
        public ComboListEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : base(listDataItemType, ReadOnly)
        {
        }
        public ComboListEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : base(standardValues, ReadOnly)
        {
        }
    }

    public class ComboListEditor : ComboListEditor<string>
    {
        public ComboListEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : base(listDataItemType, ReadOnly)
        {
        }
        public ComboListEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : base(standardValues, ReadOnly)
        {
        }
    }

    #endregion

    public class ComboBoxListEditor : SourceGrid.Cells.Editors.ComboBox
    {
        public event EventHandler SelectedIndexChanged;

        public ComboBoxListEditor(bool ReadOnly = false) : base(typeof(string))
        {
            EnableEdit = !ReadOnly;

            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

            // this.Control.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control.DropDownWidth = 200;

            this.Control.KeyPress += Control_KeyPress;
            this.Control.LostFocus += Control_LostFocus;

            this.Control.Validator.AllowNull = true;

            this.Control.SelectedIndexChanged += Control_SelectedIndexChanged;

            this.Control.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;


        }

        private void Control_LostFocus(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = sender as DevAgeComboBox;
            // cbo.EndUpdate();
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //prevent typing in the text box region
            e.Handled = true;
        }

        private void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DevAgeComboBox cbo = sender as DevAgeComboBox;

            SelectedIndexChanged?.Invoke(sender, e);
        }

        public ComboBoxListEditor(ListDataItemType listDataItemType, bool ReadOnly = false) : this(ReadOnly)
        {
            ICollection values = (ICollection)Program.listData.Items.Where<ListDataItem>(x => x.ListType == listDataItemType).Select(x => x.Text).ToList();

            this.StandardValues = values;
            this.StandardValuesExclusive = false;
        }

        public ComboBoxListEditor(IEnumerable<ListDataItem> standardValues, bool ReadOnly = false) : this(ReadOnly)
        {
            this.Control.ValueMember = "Value";
            this.Control.DisplayMember = "Text";

            ICollection values = (ICollection)standardValues.Select(x => x.Text).ToList();

            this.StandardValues = values;
            this.StandardValuesExclusive = true;
        }

        public ComboBoxListEditor(IEnumerable<string> standardValues, bool ReadOnly = false) : this(ReadOnly)
        {
            this.StandardValues = (ICollection)standardValues;
            this.StandardValuesExclusive = false;
        }

    }

    public class ComboBoxListEditor<T> : SourceGrid.Cells.Editors.ComboBox
    {
        public ComboBoxListEditor(IEnumerable<T> standardValues, bool ReadOnly = false) : base(typeof(string))
        {
            EnableEdit = !ReadOnly;

            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;

            this.Control.ValueMember = "FundName";
            this.Control.DisplayMember = "FundName";
            this.Control.DataSource = standardValues;

            this.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control.DropDownWidth = 200;

        }
    }

    public class SAIdEditor : EditorControlBase
    {
        DevAgeTextBox control = new DevAgeTextBox();

        public SAIdEditor(bool ReadOnly = false) : base(typeof(string))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        //
        // Summary:
        //     Returns the value inserted with the current editor control
        public override object GetEditedValue()
        {
            SaIdValidator validator = new SaIdValidator();

            var idNo = control.Text as string;
            if (idNo != null)
            {
                if (!string.IsNullOrEmpty(idNo))
                    if (!validator.Validate(idNo))
                        throw (new Exception("Invalid SA Id Number"));

            }

            return control != null ? control.Text : null;
        }
        //
        // Summary:
        //     Set the specified value in the current editor control.
        //
        // Parameters:
        //   editValue:
        public override void SetEditValue(object editValue)
        {
            control.Text = editValue == null ? "" : editValue.ToString();
            control.SelectAll();

        }
        //
        // Summary:
        //     Create the editor control
        protected override Control CreateControl()
        {
            control.Validator = this;
            return control;
        }
        protected override void OnSendCharToEditor(char key)
        {
        }

    }
    public class SATaxEditor : EditorControlBase
    {
        DevAgeTextBox control = new DevAgeTextBox();

        public SATaxEditor(bool ReadOnly = false) : base(typeof(string))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        //
        // Summary:
        //     Returns the value inserted with the current editor control
        public override object GetEditedValue()
        {
            TaxNoValidator validator = new TaxNoValidator();

            var idNo = control.Text as string;
            if (idNo != null)
            {
                if (!string.IsNullOrEmpty(idNo))
                    validator.Validate(idNo);


            }

            return control != null ? control.Text : null;
        }
        //
        // Summary:
        //     Set the specified value in the current editor control.
        //
        // Parameters:
        //   editValue:
        public override void SetEditValue(object editValue)
        {
            control.Text = editValue == null ? "" : editValue.ToString();
            control.SelectAll();

        }
        //
        // Summary:
        //     Create the editor control
        protected override Control CreateControl()
        {
            control.Validator = this;
            return control;
        }
        protected override void OnSendCharToEditor(char key)
        {
        }

    }

    public class TelephoneEditor : EditorControlBase
    {
        DevAgeTextBox control = new DevAgeTextBox();

        public TelephoneEditor(bool ReadOnly = false) : base(typeof(string))
        {
            if (ReadOnly)
                EditableMode = SourceGrid.EditableMode.None;
            else
                EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
        }

        //
        // Summary:
        //     Returns the value inserted with the current editor control
        public override object GetEditedValue()
        {
            RegExValidator validator = new RegExValidator(RegExpressions.Numbers(" "));

            var txt = control.Text as string;
            if (txt != null)
            {
                if (!string.IsNullOrEmpty(txt))
                    validator.Validate(txt, "Not a valid telephone number.");


            }

            return control != null ? control.Text : null;
        }
        //
        // Summary:
        //     Set the specified value in the current editor control.
        //
        // Parameters:
        //   editValue:
        public override void SetEditValue(object editValue)
        {
            control.Text = editValue == null ? "" : editValue.ToString();
            control.SelectAll();

        }
        //
        // Summary:
        //     Create the editor control
        protected override Control CreateControl()
        {
            control.Validator = this;
            return control;
        }
        protected override void OnSendCharToEditor(char key)
        {
        }

    }
    #endregion

    #region Views
    public class InstructionsStatusView : SourceGrid.Cells.Views.Cell
    {
        IDictionary<string, Color> colourScheme = InstructionStatusExt.ColourScheme();
        public InstructionsStatusView()
        {

        }
        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            string str = context.Value as string;
            if (string.IsNullOrEmpty(str))
                return;

            try
            {
                this.BackColor = Color.GhostWhite;

                this.ElementText.ForeColor = colourScheme[str]; ;
                this.ElementText.Value = str;
            }
            catch (Exception x)
            {

            }
        }

        protected override void PrepareVisualElementText(CellContext context)
        {
            base.PrepareVisualElementText(context);

        }

    }

    public class InstructionsDaysView : SourceGrid.Cells.Views.Cell
    {

        public InstructionsDaysView()
        {

        }
        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            BackColor = Color.Transparent;

            int? val = context.Value as int?;

            try
            {
                BackColor = InstructionDaysExt.ToColour(val);
                //ForeColor = BackColor;

                this.ElementText.ForeColor = Color.White;
                this.ElementText.Value = val.ToString();
            }
            catch (Exception x)
            {

            }
        }

        protected override void PrepareVisualElementText(CellContext context)
        {
            base.PrepareVisualElementText(context);
            this.ElementText.Value = "";
        }

    }

    public class ColumnInfoView : SourceGrid.Cells.Views.Cell
    {
        IDictionary<string, Color> colourScheme = InstructionStatusExt.ColourScheme();
        public ColumnInfoView()
        {
            this.ElementImage = new DevAge.Drawing.VisualElements.Image(easiplan.app.Properties.Resources.save);
            this.ImageAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            this.ImageStretch = true;


        }
        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            string str = context.Value as string;
            if (string.IsNullOrEmpty(str))
                return;

            try
            {
                this.BackColor = Color.Transparent;

                this.ElementText.ForeColor = colourScheme[str]; ;
                this.ElementText.Value = str + "info";
            }
            catch (Exception x)
            {

            }
        }

        protected override void PrepareVisualElementText(CellContext context)
        {


            base.PrepareVisualElementText(context);

        }

        protected override void OnDraw(GraphicsCache graphics, RectangleF area)
        {
            //var img = easiplan.app.Properties.Resources.save;

            //int offset = (area.Width - img.ImageSize.Width) / 2;
            //pt.X += offset;
            //pt.Y += 1;
            //img.Draw(graphics, pt, 0);

            //graphics.DrawImage(InfoIcon, )

            //Draw the Info Icon
            base.OnDraw(graphics, area);
        }

    }
    #endregion

    #region Controllers
    public class InstructionsController : SourceGrid.Cells.Controllers.ControllerBase
    {
        SourceGrid.Cells.Views.Cell view1 = new SourceGrid.Cells.Views.Cell();
        SourceGrid.Cells.Views.Cell view2 = new SourceGrid.Cells.Views.Cell();

        public InstructionsController()
        {
            view1.BackColor = Color.Red;
            view2.BackColor = Color.Green;
        }

        public override void OnValueChanged(CellContext sender, EventArgs e)
        {

            base.OnValueChanged(sender, e);

            sender.Cell.View = view1;
            sender.Grid.InvalidateCell(sender.Position);

        }


    }
    #endregion

    #region CellFormat
    public class CellFormatTypeView : SourceGrid.Cells.Views.Cell
    {

        public CellFormatTypeView()
        {

        }


        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);


            string str = context.Value as string;
            if (string.IsNullOrEmpty(str))
                return;

            try
            {
                this.BackColor = Color.WhiteSmoke;
                this.ElementText.Value = str;
            }
            catch (Exception x)
            {

            }
        }

        protected override void PrepareVisualElementText(CellContext context)
        {
            base.PrepareVisualElementText(context);

        }

    }
    #endregion
}
