using DevAge.ComponentModel;
using Finx.App.Enums;
using Finx.App.Extensions;
using easiplan.domain;
using easiplan.domain.Entities;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using easiplan.app.ContextMenus;
using easiplan.app.Extensions;

namespace Finx.App.Forms
{

    public partial class frmMetroAdminTasks : MetroForm
    {
        AdminTaskSearchModel _searchModel = new AdminTaskSearchModel();
        IEnumerable<Instruction> _instructionList = new List<Instruction>();

        Timer _Timer = new Timer();
        bool IsRefreshing = false;

        public frmMetroAdminTasks()
        {
            InitializeComponent();

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            this.Text = "      Admin Tasks";
            this.BackImage = global::easiplan.app.Properties.Resources.Tasks;
            this.BackImagePadding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.BackMaxSize = 40;

            this.KeyPreview = true;

            #endregion

            #region ToolBarMenu
            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Admin Tasks");
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.cog_32;

            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Text = "Extract"; xToolBarMenu1.tbSave.Enabled = true;
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Add Custom Task";
            xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = true;

            xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
            xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
            xToolBarMenu1.SaveClicked += toolStripButton_Extract_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            #endregion

            #region Search Panel
            var Users = new List<ListDataItem>();
            Users.Add(new ListDataItem(ListDataItemType.User, "", ""));
            Users.Add(new ListDataItem(ListDataItemType.User, "UnAllocated", "UnAllocated"));
            foreach (var user in Program.Repository.List<User, int>(null))
                Users.Add(new ListDataItem(ListDataItemType.User, user.Firstname, user.Firstname));

            this.metroPanel1.Height = 80;
            int leftSide = 5;

            MetroTextBoxEditor metroTxtClientName = new MetroTextBoxEditor(Width: 200);
            metroTxtClientName.EnterKeyClicked += MetroTxtClientName_EnterKeyClicked;

            this.metroPanel_SearchTasks.Initialise(_searchModel, cntr =>
            {
                cntr.For(x => x.Clientname, "Client Name", metroTxtClientName);
            }, left: leftSide, labelWidth: 100,
             controlsLayout: ControlsLayout.Vertical
            );

            leftSide += 210;
            this.metroPanel_SearchTasks.Initialise(_searchModel, cntr =>
            {
                cntr.For(x => x.TaskType, "Task Type", new MetroComboListEditor(Width: 200).DataSourceList(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientInstructionType).ToList()));
            }, left: leftSide, labelWidth: 100, PropertyChangedHandler: TaskType_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);

            leftSide += 210;
            this.metroPanel_SearchTasks.Initialise(_searchModel, cntr =>
            {
                cntr.For(x => x.Username, "Allocated To", new MetroComboListEditor(Width: 200).DataSourceList(Users));
            }, left: leftSide, labelWidth: 100, PropertyChangedHandler: User_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);

            //leftSide += 210;
            //this.metroPanel_SearchTasks.Initialise(searchModel, cntr =>
            //{
            //    cntr.For(x => x.InstructionStatus, "Status", new MetroComboListEditor(Width: 200).DataSourceList(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientInstructionStatus).ToList()));
            //}, left: leftSide, labelWidth: 60, PropertyChangedHandler: Status_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);


            leftSide += 210;
            this.metroPanel_SearchTasks.Initialise(_searchModel, cntr =>
            {
                cntr.For(x => x.SearchView, "View", new MetroComboListEditor(Width: 200).DataSourceList(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientInstructionView).ToList()));
            }, left: leftSide, labelWidth: 60, PropertyChangedHandler: View_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);

            leftSide += 210;
            //this.metroPanel_SearchTasks.Initialise(searchModel, cntr =>
            //{
            //    cntr.For(x => x.FilterDate, "Filter Date", new MetroDateEditor(Width: 150));
            //}, left: leftSide, labelWidth: 80, PropertyChangedHandler: Date_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);
            #endregion

            #region ListView
            this.dataGrid_AdminTasks.Initialise<Instruction>(_instructionList.OrderByDescending(x => x.DaysLastUpdated).ToList(), column =>
            {
                column.For(x => x.Description, "Client Name", new MetroTextBoxEditor(201).ReadOnly(true));
                column.For(x => x.Type, "Task Type", new MetroTextBoxEditor(150).ReadOnly(true));
                column.For(c => c.TaskName, "Task Name / LISP", new MetroStringEditor(160).ReadOnly(true));
                column.For(c => c.ReferenceNo, "Policy#", new MetroTextBoxEditor(120).ReadOnly(true));
                //column.For(c => c.ReferenceOwner, "Owner", new MetroTextBoxEditor(210).ReadOnly(true));
                column.For(x => x.AllocatedTo, "Allocate To", new MetroComboBoxEditor(110).DataSourceList(Program.UserServices.ListActiveUsers().ToListDataItem<User>("Firstname", "Firstname")).ReadOnly(false));
                column.For(c => c.Status, "Status", new MetroTextBoxEditor(120).ReadOnly(true));
                column.For(c => c.CreateDate, "Created On", new MetroDateEditor(100).ReadOnly(true));
                column.For(c => c.UpdateDate, "Updated On", new MetroDateEditor(100).ReadOnly(true));
                column.For(c => c.DaysCreated, "Open", new MetroStringNumberEditor(50).ReadOnly(true));
                column.For(c => c.DaysLastUpdated, "Last", new MetroStringNumberEditor(50).ReadOnly(true));
            },
                  ListChangedEventHandler: UpdateEvent_Click,
                  AllowDelete: false,//Program.User.IsAdministrator,                    
                  ContextMenu: new DataGridContextMenu(this, ContextMenuType.AdminTask, false, null),//, OnCompleted: toolStripButton_Refresh_Click
                  ReadOnly: false
                  )
              .Formatt(false);
            this.dataGrid_AdminTasks.EnableSort = false;
            this.dataGrid_AdminTasks.SortingRangeRows += DataGrid_AdminTasks_SortingRangeRows;


            //Format cells
            this.dataGrid_AdminTasks.Columns[6].DataCell.View = new InstructionsStatusView();
            this.dataGrid_AdminTasks.Columns[9].DataCell.View = new InstructionsDaysView();
            this.dataGrid_AdminTasks.Columns[10].DataCell.View = new InstructionsDaysView();

            #endregion

            #region  Timer
            this.SubTitle = string.Format("{0}", DateTime.Now);
            _Timer.Interval = (60 * 1000);//60 secs
                                          //  _Timer.Tick += new EventHandler(_Timer_Tick);
                                          //  _Timer.Start();
            #endregion

        }

        private void DataGrid_AdminTasks_SortingRangeRows(object sender, SortRangeRowsEventArgs e)
        {



        }

        private void frmMetroAdminTasks_Load(object sender, EventArgs e)
        {
            //Populate the grid
            ShowListView();
        }

        #region SearchModel Property Changed Events
        /// <summary>
        /// Search for client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroTxtClientName_EnterKeyClicked(object sender, KeyEventArgs e)
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;
                ShowListView();
                IsRefreshing = false;
            }
        }
        private void TaskType_propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!IsRefreshing && e.PropertyName == "TaskType")
            {
                IsRefreshing = true;
                ShowListView();
                IsRefreshing = false;
            }
        }
        private void View_propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!IsRefreshing && e.PropertyName == "SearchView")
            {
                IsRefreshing = true;
                ShowListView();
                IsRefreshing = false;
            }
        }
        private void Date_propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!IsRefreshing && e.PropertyName == "FilterDate")
            {
                IsRefreshing = true;
                ShowListView();
                IsRefreshing = false;
            }
        }
        private void User_propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!IsRefreshing && e.PropertyName == "Username")
            {
                IsRefreshing = true;
                ShowListView();
                IsRefreshing = false;
            }
        }

        #endregion

        #region Toolbar Event Handlers
        private void toolStripButton_Extract_Click(object sender, EventArgs e)
        {
            if (_instructionList.Count() > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string FileName = saveFileDialog.FileName;

                        Program.CreateCSVFromGenericList<Instruction>(_instructionList.ToList(), FileName);

                        Process.Start(FileName);
                    }
                    catch (Exception x)
                    {
                        MessageBoxExt.ShowException(x);
                    }
                }

            }

            xToolBarMenu1.SetEditMode(true);
            xToolBarMenu1.tbEdit.Enabled = true;
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            IsRefreshing = true;

            ShowListView();

            IsRefreshing = false;
        }

        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            var res = MessageBoxExt.ShowQuestion("NB. This task will not be linked to a Client, instead use the 'Create Client Task' in the Client Management form to create a task linked to a client.\r\n\r\n Do you wish to continue?");
            if (res)
            {
                frmMetroAdminTaskAdd frm = new frmMetroAdminTaskAdd();
                frm.ShowDialog(this);

                ShowListView();
            }

            xToolBarMenu1.tbEdit.Enabled = true;

        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            _Timer.Stop();
            _Timer = null;

            this.Close();
        }
        #endregion

        #region Grid Event Handlers

        void UpdateEvent_Click(object sender, ListChangedEventArgs e)
        {
            if (IsRefreshing)
                return;

            if (e.ListChangedType != ListChangedType.ItemChanged)
                return;

            var _BoundList = sender as BoundList<Instruction>;
            try
            {
                if (_BoundList != null)
                {
                    if (_BoundList.EditedObject != null)
                    {


                        Instruction _instruction = _BoundList.EditedObject as Instruction;

                        Program.Repository.Update<Instruction, int>(_instruction);
                    }

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning("Could not update Task. " + x1.Message);
            }
        }

        #endregion

        private void ShowListView()
        {
            this.dataGrid_AdminTasks.SuspendLayout();
            using (new AppWaitCursor(this))
            {
                try
                {

                    this.dataGrid_AdminTasks.Enabled = false;

                    switch (_searchModel.SearchView)
                    {
                        case "Authoriser":
                            _instructionList = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
                                                         && x.Status != InstructionStatus.AddCompleted.ToString()
                                                         && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                         && x.Status != InstructionStatus.Cancelled.ToString()
                                                         && x.Status != InstructionStatus.AddCancelled.ToString()
                                                         && x.Status != InstructionStatus.UpdateCancelled.ToString()
                                                         && x.Status == InstructionStatus.AddAuthorised.ToString()
                                                         || x.Status == InstructionStatus.ReadyForAuth.ToString()
                                                         || x.Status == InstructionStatus.AddReadyForAuth.ToString()
                                                         || x.Status == InstructionStatus.UpdateAuthorised.ToString()
                                                         || x.Status == InstructionStatus.UpdateReadyForAuth.ToString()
                                                         || x.Status == InstructionStatus.Authorised.ToString()
                     );
                            break;
                        case "Admin":
                            _instructionList = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
                                                        && x.Status != InstructionStatus.AddCompleted.ToString()
                                                        && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                        && x.Status != InstructionStatus.Cancelled.ToString()
                                                        && x.Status != InstructionStatus.AddCancelled.ToString()
                                                        && x.Status != InstructionStatus.UpdateCancelled.ToString()
                                                        && x.Status == InstructionStatus.AddInProgress.ToString()
                                                        || x.Status == InstructionStatus.AddPending.ToString()
                                                        || x.Status == InstructionStatus.InProgress.ToString()
                                                        || x.Status == InstructionStatus.Submitted.ToString()
                                                        || x.Status == InstructionStatus.AddSubmitted.ToString()
                                                        || x.Status == InstructionStatus.UpdateSubmitted.ToString()
                                                        || x.Status == InstructionStatus.Pending.ToString()
                                                        || x.Status == InstructionStatus.UpdateInProgress.ToString()
                                                        || x.Status == InstructionStatus.UpdatePending.ToString()
                    );
                            break;
                        default:
                            _instructionList = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
                                                        && x.Status != InstructionStatus.AddCompleted.ToString()
                                                        && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                        && x.Status != InstructionStatus.Cancelled.ToString()
                                                        && x.Status != InstructionStatus.AddCancelled.ToString()
                                                        && x.Status != InstructionStatus.UpdateCancelled.ToString()

                    );
                            break;

                    }

                    if (!string.IsNullOrEmpty(_searchModel.Clientname))
                        _instructionList = _instructionList.Where(x => x.Description.ToLower().StartsWith(_searchModel.Clientname.ToLower()));

                    if (!string.IsNullOrEmpty(_searchModel.TaskType))
                        _instructionList = _instructionList.Where<Instruction>(x => x.Type == _searchModel.TaskType);

                    if (!string.IsNullOrEmpty(_searchModel.Username))
                        _instructionList = _instructionList.Where<Instruction>(x => x.AllocatedTo == _searchModel.Username);

                    if (_searchModel.FilterDate > _searchModel.MinDateTime)
                        _instructionList = _instructionList.Where(x => x.CreateDate >= _searchModel.FilterDate);

                }
                catch (Exception x)
                {
                }
                finally
                {
                    this.dataGrid_AdminTasks.Rebind<Instruction>(_instructionList.ToList(), UpdateEvent_Click);

                    xToolBarMenu1.tbRefresh.Enabled = true;
                    this.SubTitle = string.Format("{0}", DateTime.Now);

                    this.dataGrid_AdminTasks.ResumeLayout();
                    this.dataGrid_AdminTasks.Enabled = true;

                    //Refresh the Graphics View
                    ShowGraphicsView();

                    this.xToolBarMenu1.tbCaption.Text = string.Format("{0}: {1}", "Admin Tasks", _instructionList.Count());
                    xToolBarMenu1.SetEditMode(false);
                    xToolBarMenu1.tbEdit.Enabled = true;
                    xToolBarMenu1.tbSave.Enabled = true;
                }
            };
        }

        private void ShowGraphicsView()
        {
            this.splitContainer2.Panel1Collapsed = true;

            try
            {
                #region Group By Status
                chartTaskByStatus.Format("Tasks by Status");

                var chartAreaByStatus = new ChartArea("TaskByStatusArea");
                chartAreaByStatus.Format();

                chartTaskByStatus.ChartAreas.Add(chartAreaByStatus);

                Series seriesByStatus = new Series();
                seriesByStatus.Format(SeriesChartType.Column, "TaskByStatusArea");
                seriesByStatus.ChartData<string, Instruction>(_instructionList.GroupBy(x => x.Status).OrderBy(o => o.Key), InstructionStatusExt.ColourScheme());

                chartTaskByStatus.Series.Add(seriesByStatus);

                #endregion

            }
            catch (Exception)
            { }
            try
            {

                #region Group By Allocation
                chartByAllocation.Format("Tasks by Allocation");

                var chartAreaByAllocation = new ChartArea("TaskByAllocationArea");
                chartAreaByAllocation.Format();

                chartByAllocation.ChartAreas.Add(chartAreaByAllocation);

                Series seriesByAllocation = new Series();
                seriesByAllocation.Format(SeriesChartType.Bar, "TaskByAllocationArea");
                seriesByAllocation.ChartData<string, Instruction>(_instructionList.GroupBy(x => x.AllocatedTo));

                chartByAllocation.Series.Add(seriesByAllocation);

                #endregion


            }
            catch (Exception)
            { }
            try
            {


                #region Group By Days Outstanding
                chartByDaysOutstanding.Format("Tasks by Days Last Updated");

                var chartAreaByDaysOut = new ChartArea("TaskByDaysOutArea");
                chartAreaByDaysOut.Format("Days Last Updated");

                chartByDaysOutstanding.ChartAreas.Add(chartAreaByDaysOut);

                Series seriesDaysOut = new Series("Days");
                seriesDaysOut.Format(SeriesChartType.Column, "TaskByDaysOutArea");
                seriesDaysOut.ChartData<int, Instruction>(_instructionList.GroupBy(x => x.DaysLastUpdated).OrderBy(x => x.Key));

                chartByDaysOutstanding.Series.Add(seriesDaysOut);

                #endregion
            }
            catch (Exception)
            { }
        }

    }


    public class AdminTaskSearchModel : BaseEntity<int>
    {
        string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (!value.Equals(_Username))
                {
                    _Username = value;
                    InvokePropertyChanged("Username");
                }
            }
        }

        string _Clientname;
        public string Clientname
        {
            get { return _Clientname; }
            set
            {
                if (!value.Equals(_Clientname))
                {
                    _Clientname = value;
                    InvokePropertyChanged("Clientname");
                }
            }
        }

        string _SearchView;
        public string SearchView
        {
            get { return _SearchView; }
            set
            {
                if (!value.Equals(_SearchView))
                {
                    _SearchView = value;
                    InvokePropertyChanged("SearchView");
                }
            }
        }

        string _TaskType;
        public string TaskType
        {
            get { return _TaskType; }
            set
            {
                if (!value.Equals(_TaskType))
                {
                    _TaskType = value;
                    InvokePropertyChanged("TaskType");
                }
            }
        }


        string _InstructionStatus;
        public string InstructionStatus
        {
            get { return _InstructionStatus; }
            set
            {

                if (!value.Equals(_InstructionStatus))
                {
                    _InstructionStatus = value;
                    InvokePropertyChanged("InstructionStatus");
                }
            }
        }

        DateTime _FilterDate;
        public DateTime FilterDate
        {
            get { return _FilterDate; }
            set
            {

                if (!value.Equals(_FilterDate))
                {
                    _FilterDate = value;
                    InvokePropertyChanged("FilterDate");
                }
            }
        }

        public AdminTaskSearchModel()
        {
            this.FilterDate = this.MinDateTime;

            IsLoading = false;
        }
    }
}
