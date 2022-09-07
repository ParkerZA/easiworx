using Finx.App.Enums;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Controls.Ext;
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
using easiplan.app.Extensions;

namespace Finx.App
{
    public partial class frmMetroAdminTaskAdd : MetroForm
    {
        Instruction Instruction = new Instruction() { InstructionType = InstructionType.CUSTOMTASK };

        public frmMetroAdminTaskAdd(Instruction model = null)
        {
            InitializeComponent();

            if (model != null)
                Instruction = model;

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            //this.ResizeRedraw = true;

            this.Text = string.Empty;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", Instruction.InstructionType.ToText());
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.cog_32;

            xToolBarMenu1.tbRefresh.Visible = false;
            //xToolBarMenu1.tbRefresh.Enabled = false;
            //xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;

            xToolBarMenu1.tbSave.Visible = true;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;

            xToolBarMenu1.tbEdit.Visible = Instruction.Status == InstructionStatus.InProgress.ToText(); xToolBarMenu1.tbEdit.Text = "Complete";
            xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

        }

        private void frmMetroAdminTaskAdd_Load(object sender, EventArgs e)
        {

            this.metroPanel_AdminTaskAdd.Controls.Clear();

            this.metroPanel_AdminTaskAdd.Initialise(Instruction, cntr =>
            {
                cntr.For(x => x.Type, "Task Type", new MetroComboBoxEditor(Width: this.Width - 180, Height: 80)
                    .DataSourceList(Program.listData.List(ListDataItemType.CustomTaskType)));
                cntr.For(x => x.Description, "Client Name", new MetroTextBoxEditor(Width: this.Width - 180));
                cntr.For(c => c.TaskName, "Task Name", new MetroTextBoxEditor(Width: this.Width - 180));
                cntr.For(x => x.Comment, "Task Comments", new MetroMultiLineTextBoxEditor(Width: this.Width - 180, Height: 120));
                cntr.For(x => x.AllocatedTo, "Allocated To", new MetroComboBoxEditor().DataSourceList(Program.UserServices.ListActiveUsers().ToListDataItem<User>("Firstname", "Firstname")));
                cntr.For(x => x.Status, "Status", new MetroComboBoxEditor()
                    .DataSourceList(Program.listData.List(ListDataItemType.CustomTaskStatus)).ReadOnly(false));
                cntr.For(x => x.CreateDate, "Created On", new MetroTextBoxEditor().ReadOnly(true));
                cntr.For(x => x.UpdateDate, "Updated On", new MetroTextBoxEditor().ReadOnly(true));
                cntr.For(x => x.UpdateBy, "By", new MetroTextBoxEditor().ReadOnly(true));

            }, left: 5, top: 5, labelWidth: 120,
            PropertyChangedHandler: propertyChanged_EventHandler,
            controlsLayout: ControlsLayout.Horizontal,
            dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Instruction.Id == 0)
                {
                    Instruction.Status = InstructionStatus.Pending.ToText();

                    Instruction.InstructionType = InstructionType.CUSTOMTASK;

                    Instruction.CreateDate = DateTime.Now;
                    Instruction.UpdateBy = Program.User.Username;
                    Instruction.UpdateDate = DateTime.Now;

                    Program.Repository.Add<Instruction, int>(Instruction);

                }
                else
                {
                    Instruction.Status = InstructionStatus.InProgress.ToText();

                    Instruction.UpdateBy = Program.User.Username;
                    Instruction.UpdateDate = DateTime.Now;

                    Program.Repository.Update<Instruction, int>(Instruction);
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);

                xToolBarMenu1.tbEdit.Visible = Instruction.Status == InstructionStatus.InProgress.ToText(); xToolBarMenu1.tbEdit.Text = "Complete";
            }
        }
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Instruction.Id == 0)
                {
                    Instruction.Status = InstructionStatus.Pending.ToText();

                    Instruction.CreateDate = DateTime.Now;
                    Instruction.UpdateBy = Program.User.Username;
                    Instruction.UpdateDate = DateTime.Now;

                    Program.Repository.Add<Instruction, int>(Instruction);

                }
                else
                {
                    Instruction.Status = InstructionStatus.Completed.ToText();

                    Instruction.UpdateBy = Program.User.Username;
                    Instruction.UpdateDate = DateTime.Now;

                    Program.Repository.Update<Instruction, int>(Instruction);

                    toolStripButton_Close_Click(sender, e);
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);

                xToolBarMenu1.tbEdit.Visible = Instruction.Status == InstructionStatus.InProgress.ToText(); xToolBarMenu1.tbEdit.Text = "Complete";
            }
        }
        private void propertyChanged_EventHandler(object sender, EventArgs e)
        {
            this.xToolBarMenu1.SetEditMode(true);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            frmMetroAdminTaskAdd_Load(this, e);

            base.OnResizeEnd(e);
        }
    }
}
