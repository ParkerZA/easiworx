using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.Domain.Entities;
using System.Drawing;

namespace Finx.App.Forms
{
    public partial class frmAdminTasks : Form
    {
        IList<Instruction> model;
        Timer _Timer = new Timer();

        public frmAdminTasks()
        {
            InitializeComponent();

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Admin Tasks");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.cog_32;

            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

            // xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
            // xToolBarMenu1.SaveClicked += toolStripButton_AddToPortfolio_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            var Users = new List<ListDataItem>();
            Users.Add(new ListDataItem(ListDataItemType.User, "", ""));
            Users.Add(new ListDataItem(ListDataItemType.User,"UnAllocated", "UnAllocated"));
            foreach (var user in Program.Repository.List<User, int>(null))
                Users.Add(new ListDataItem(ListDataItemType.User, user.Firstname, user.Firstname));

            this.xInputFilterAllocation.ControlTypes = UserControls.ControlTypes.ComboBox;
            this.xInputFilterAllocation.DataSource = Users;
            this.xInputFilterAllocation.InitialiseControl();
            this.xInputFilterAllocation.comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            IEnumerable<ListDataItem> views = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientInstructionView);

            this.xInputFilterStatus.ControlTypes = UserControls.ControlTypes.ComboList;
            this.xInputFilterStatus.DataSource = views; 
            this.xInputFilterStatus.InitialiseControl();
            this.xInputFilterStatus.comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            //Timer
            this.labelTime.Text = string.Format("{0}", DateTime.Now);
            _Timer.Interval = (30 * 1000);//30 secs
            _Timer.Tick += new EventHandler(_Timer_Tick);
            _Timer.Start();
        }

        private void frmAdminTasks_Load(object sender, EventArgs e)
        {
           

            this.dataGrid_AdminTasks.Initialise();

            var Users = new List<ListDataItem>();
            foreach (var user in Program.Repository.List<User, int>(null))
                Users.Add(new ListDataItem(ListDataItemType.User, user.Firstname, user.Firstname));
            ComboBoxEditor cboUsers = new ComboBoxEditor(Users);
            cboUsers.Control.SelectedValueChanged += cboUsers_SelectedValueChanged;


            this.dataGrid_AdminTasks.Columns.Add("Description", "Client Name", typeof(string));
           // this.dataGrid_AdminTasks.Columns.Add("Type", "Type", typeof(string));
            this.dataGrid_AdminTasks.Columns.Add("Comment", "Comment", typeof(string));
            this.dataGrid_AdminTasks.Columns.Add("AllocatedTo", "Allocated To", typeof(string));
            //this.dataGrid_AdminTasks.Columns.Add("AllocatedTo", "Allocated To", cboUsers).Width = 350;
            //this.dataGrid_AdminTasks.Columns.Add("Comment", "Comment", typeof(string));
            //this.dataGrid_AdminTasks.Columns.Add("ReferenceNo", "Reference", typeof(string));
            this.dataGrid_AdminTasks.Columns.Add("Status", "Status", typeof(string)).DataCell.View = new InstructionsStatusView(); ;
            this.dataGrid_AdminTasks.Columns.Add("CreateDate", "Date Started", typeof(DateTime));
            this.dataGrid_AdminTasks.Columns.Add("UpdateDate", "Date Updated", typeof(DateTime));
            this.dataGrid_AdminTasks.Columns.Add("DaysLastUpdated", "Days", typeof(int));

            if (model == null)
                model = new List<Instruction>();

            //  Client click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
                clickEvent2.Click += ShowEvent_Click;

            var btnFunds = new SourceGrid.Cells.RowHeader("");
                btnFunds.Image = Properties.Resources.save;
                btnFunds.ToolTipText = "Click to open client";
                btnFunds.AddController(clickEvent2);

            var col = dataGrid_AdminTasks.Columns.Add("", "", btnFunds);
                col.Width = 22;
                col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                col.MaximalWidth = 22;
                col.MinimalWidth = 22;

            if (Program.User.IsAdministrator)
            {
                //Add a delete button
                SourceGrid.Cells.Controllers.CustomEvents deleteEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                deleteEvent.Click += DeleteEvent_Click;

                var btnDel = new SourceGrid.Cells.RowHeader("");
                btnDel.Image = Properties.Resources.delete_button;
                btnDel.ToolTipText = "Click to delete row";
                btnDel.AddController(deleteEvent);

                var colDel = dataGrid_AdminTasks.Columns.Add("", "", btnDel);
                colDel.Width = 22;
                colDel.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                colDel.MaximalWidth = 22;
                colDel.MinimalWidth = 22;
            }

            this.dataGrid_AdminTasks.DataSource = new DevAge.ComponentModel.BoundList<Instruction>(model);
            this.dataGrid_AdminTasks.DataSource.ListChanged += DataSource_ListChanged<Instruction>;

            this.dataGrid_AdminTasks.Format(true);//ReadOnly

            //Populate the grid
            toolStripButton_Refresh_Click(sender, e);

        }

        private void cboUsers_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
            //try
            //{

            //    DevAge.ComponentModel.BoundList<Instruction> list = sender as DevAge.ComponentModel.BoundList<Instruction>;

            //    Program.Repository.Update<Instruction, int>(list.EditedObject as Instruction);
            //}
            //catch (Exception x)
            //{
            //}
        }

        void ShowEvent_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0)
                {
                    Instruction instruction = model[context.Position.Row - 1];

                    frmClient childForm = new frmClient(Program.Repository.Get<Client, int>(instruction.ClientId));
                    childForm.WindowState = FormWindowState.Normal;

                    childForm.ShowDialog(this);

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning("Could not show Client Form. " + x1.Message);
            }
            finally
            {
                this.dataGrid_AdminTasks.Refresh();
            }


        }
        void DeleteEvent_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0)
                {
                    Instruction instruction = model[context.Position.Row - 1];

                    if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Task?"))
                    {
                        Program.Repository.Remove<Instruction, int>(instruction.Id);

                        model.Remove(instruction);
                    }

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning("Could not delete Task. " + x1.Message);
            }
            finally
            {
                this.dataGrid_AdminTasks.Refresh();
            }


        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            _Timer.Stop();
            _Timer = null;

            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            try
            {

                    switch (xInputFilterStatus.comboBox.SelectedValue.ToString()) {
                        case "Authoriser":
                            model = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
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
                         ).ToList();
                            break;
                        case "Admin":
                            model = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
                                                            && x.Status != InstructionStatus.AddCompleted.ToString()
                                                            && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                            && x.Status != InstructionStatus.Cancelled.ToString()
                                                            && x.Status != InstructionStatus.AddCancelled.ToString()
                                                            && x.Status != InstructionStatus.UpdateCancelled.ToString()
                                                            && x.Status == InstructionStatus.AddInProgress.ToString()
                                                            || x.Status == InstructionStatus.AddPending.ToString()
                                                            || x.Status == InstructionStatus.InProgress.ToString()
                                                            || x.Status == InstructionStatus.Pending.ToString()
                                                            || x.Status == InstructionStatus.UpdateInProgress.ToString()
                                                            || x.Status == InstructionStatus.UpdatePending.ToString()
                        ).ToList();
                            break;
                        default:
                            model = Program.Repository.List<Instruction, int>(x => x.Status != InstructionStatus.Completed.ToString()
                                                            && x.Status != InstructionStatus.AddCompleted.ToString()
                                                            && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                            && x.Status != InstructionStatus.Cancelled.ToString()
                                                            && x.Status != InstructionStatus.AddCancelled.ToString()
                                                            && x.Status != InstructionStatus.UpdateCancelled.ToString()
                                                           
                        ).ToList();
                            break;

                        }
               
                if (!xInputFilterAllocation.comboBox.Text.Equals(string.Empty))
                    model = model.Where<Instruction>(x => x.AllocatedTo == xInputFilterAllocation.comboBox.Text.ToString()).ToList();
            }
            catch (Exception x)
            {
            }
            finally
            {

                this.dataGrid_AdminTasks.DataSource = new DevAge.ComponentModel.BoundList<Instruction>(model);
                this.dataGrid_AdminTasks.Refresh();

                xToolBarMenu1.tbRefresh.Enabled = true;
                this.labelTime.Text = string.Format("{0}", DateTime.Now);

                //Refresh the Graphics View
                ShowGraphicsView();
            }
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void ShowGraphicsView()
        {
            this.splitContainer2.Panel1Collapsed = true;

            #region Group By Status
            chartTaskByStatus.Format("Tasks by Status");

             var chartAreaByStatus = new ChartArea("TaskByStatusArea");
            chartAreaByStatus.Format();

            chartTaskByStatus.ChartAreas.Add(chartAreaByStatus);

            Series seriesByStatus = new Series();
            seriesByStatus.Format(SeriesChartType.Column, "TaskByStatusArea");
            seriesByStatus.ChartData<string,Instruction>(model.GroupBy(x=>x.Status).OrderBy(o=>o.Key),InstructionStatusExt.ColourScheme());

            chartTaskByStatus.Series.Add(seriesByStatus);

            #endregion

            #region Group By Allocation
            chartByAllocation.Format("Tasks by Allocation");
           
            var chartAreaByAllocation = new ChartArea("TaskByAllocationArea");
            chartAreaByAllocation.Format();

            chartByAllocation.ChartAreas.Add(chartAreaByAllocation);

            Series seriesByAllocation = new Series();
            seriesByAllocation.Format(SeriesChartType.Bar, "TaskByAllocationArea");
            seriesByAllocation.ChartData<string, Instruction>(model.GroupBy(x => x.AllocatedTo));
             
            chartByAllocation.Series.Add(seriesByAllocation);

            #endregion

            #region Group By Days Outstanding
            chartByDaysOutstanding.Format("Tasks by Days Last Updated");
           
            var chartAreaByDaysOut = new ChartArea("TaskByDaysOutArea");
            chartAreaByDaysOut.Format("Days Last Updated");

            chartByDaysOutstanding.ChartAreas.Add(chartAreaByDaysOut);

            Series seriesDaysOut = new Series("Days");
            seriesDaysOut.Format(SeriesChartType.Column, "TaskByDaysOutArea");
            seriesDaysOut.ChartData<int, Instruction>(model.GroupBy(x => x.DaysLastUpdated).OrderBy(x=>x.Key));

            chartByDaysOutstanding.Series.Add(seriesDaysOut);

            #endregion

        }

        private void frmAdminTasks_Deactivate(object sender, EventArgs e)
        {

        }
    }
}
