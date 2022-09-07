using DevAge.ComponentModel;
using Finx.App.Enums;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Controls;
using MetroFramework.Forms;
using my.domain.lib.core.Domain;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmMetroPolicyNotes : MetroForm
    {

        #region Delegates / Events
        public delegate Instruction GetInstructionEventHandler(object sender, EventArgs args);
        public event GetInstructionEventHandler GetInstructionEvent;
        public event EventHandler AddClientInstructionEvent;
        public event EventHandler UpdateClientInstructionEvent;
        public event EventHandler DeleteClientInstructionEvent;
        #endregion

        #region Locals
        bool ReadOnly = false;

        Retirement Retirement = null;
        Investment Investment = null;
        Education Education = null;
        Medical Medical = null;
        Life Life = null;
        IncomeAsset IncomeAsset = null;

        Instruction Instruction = null;//Reference to an Admin Task

        Need Need = null;
        EducationNeed EducationNeed = null;
        InvestmentNeed InvestmentNeed = null;

        Note selectedNote = null;

        PolicyAction Action = PolicyAction.AmendPolicy;
        #endregion

        #region Public Variables
        public Client Client { get; set; }
        #endregion

        #region Constructors
        //Base Constructor
        public frmMetroPolicyNotes(string Title, bool readOnly, PolicyAction action)
        {
            InitializeComponent();

            ReadOnly = readOnly;
            Action = action;

            this.Text = string.Empty;
            this.SubTitle = string.Format("{0}", "Policy Notes");

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.Text = string.Empty;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            #region xToolBarMenu1
            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", Title);
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.notes_32;

            xToolBarMenu1.tbRefresh.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbRefresh.Text = "Delete";
            xToolBarMenu1.RefreshClicked += toolStripButton_Delete_Click;
            xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;

            xToolBarMenu1.tbSave.Visible = !ReadOnly;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;

            xToolBarMenu1.tbEdit.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbEdit.Text = "New Note";
            xToolBarMenu1.EditClicked += toolStripButton_Add_Click;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            #endregion
        }

        public frmMetroPolicyNotes(Retirement model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;

            Initialise_SelectPanel(model.Notes);
        }

        public frmMetroPolicyNotes(Investment model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Investment = model;

            Initialise_SelectPanel(model.Notes);


        }

        public frmMetroPolicyNotes(Education model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(Medical model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(Life model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(IncomeAsset model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(Need model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            Need = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(EducationNeed model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            EducationNeed = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(InvestmentNeed model, bool readOnly, PolicyAction action) : this(model.Description, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            InvestmentNeed = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroPolicyNotes(Instruction model, bool readOnly, PolicyAction action) : this(model.Name, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            Instruction = model;

            if (Instruction.InstructionType == InstructionType.UNKNOWN)
                Instruction.InstructionType = InstructionTypeExt.ToType(Instruction.Type);

            switch (Instruction.InstructionType)
            {

                case InstructionType.ASSET_POLICY_NOTE:
                    IncomeAsset = Program.Repository.Get<IncomeAsset, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(IncomeAsset.Notes);
                    break;
                case InstructionType.EDU_POLICY_NOTE:
                    Education = Program.Repository.Get<Education, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Education.Notes);
                    break;
                case InstructionType.INVEST_POLICY_NOTE:
                    Investment = Program.Repository.Get<Investment, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Investment.Notes);
                    break;
                case InstructionType.LIFE_POLICY_NOTE:
                    Life = Program.Repository.Get<Life, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Life.Notes);
                    break;
                case InstructionType.MEDICAL_POLICY_NOTE:
                    Medical = Program.Repository.Get<Medical, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Medical.Notes);
                    break;
                case InstructionType.RETIRE_POLICY_NOTE:
                    Retirement = Program.Repository.Get<Retirement, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Retirement.Notes);
                    break;
                default:
                    throw new MyValidationException("Unknown Instruction Type");

            }




        }
        #endregion

        #region Initialisation

        private void frmMetroAdminTaskAdd_Load(object sender, EventArgs e)
        {

        }
        void Initialise_SelectPanel(IList<Note> notes)
        {
            if (selectedNote == null)
            {
                if (notes.Count > 0)
                    selectedNote = notes.LastOrDefault();
                else
                    selectedNote = new Note();
            }

            this.metroPanel_Select.Controls.Clear();

            var cboCreateDate = new MetroComboListEditor(Width: 280).DataSourceList(notes.ToList(), "CreateDate", "CreateDate");
            cboCreateDate.controlSelectedIndexChanged += CboCreateDate_controlSelectedIndexChanged;

            this.metroPanel_Select.Initialise(selectedNote, cntr =>
             {
                 cntr.For(x => x.CreateDate, "Created on ...", cboCreateDate);
             }, left: 10, top: 5, labelWidth: 120, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.Never).Format();

            Initialise_PolicyNotePanel();
        }

        private void CboCreateDate_controlSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MetroComboListEditor cboEditor = sender as MetroComboListEditor;
                MetroComboBox cbo = cboEditor.Control as MetroComboBox;

                selectedNote = cbo.SelectedItem as Note;

                Initialise_PolicyNotePanel();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        void Initialise_PolicyNotePanel()
        {
            if (selectedNote == null)
                return;

            selectedNote.IsLoading = true;

            this.metroPanel_PolicyNote.Controls.Clear();

            this.metroPanel_PolicyNote.Initialise(selectedNote, cntr =>
            {
                cntr.For(x => x.UpdateDate, "Updated on", new MetroTextBoxEditor(160).ReadOnly(true));//185
            }, left: 10, top: 0, labelWidth: 120, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            this.metroPanel_PolicyNote.Initialise(selectedNote, cntr =>
            {
                cntr.For(x => x.UpdateBy, "By", new MetroTextBoxEditor(100).ReadOnly(true));
            }, left: 300, top: 0, labelWidth: 30, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            this.metroPanel_PolicyNote.Initialise(selectedNote, cntr =>
            {
                cntr.For(x => x.IsCompleted, "Completed", new MetroCheckBoxEditor(100).ReadOnly(ReadOnly));
            }, left: 440, top: 0, labelWidth: 80, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();


            this.metroPanel_PolicyNote.Initialise<Note>(selectedNote, cntr =>
            {

                //cntr.For(x => x.CreateDate, "Create Date", new MetroTextBoxEditor().ReadOnly(true));
                cntr.For(x => x.Text, "Note", new MetroMultiLineTextBoxEditor(Width: this.metroPanel_PolicyNote.Width - 20, Height: this.metroPanel_PolicyNote.Height - 50).ReadOnly(ReadOnly));
                //cntr.For(x => x.UpdateDate, "Updated On", new MetroTextBoxEditor().ReadOnly(true));//185
                //cntr.For(x => x.UpdateBy, "By", new MetroTextBoxEditor().ReadOnly(true));

            }, left: 10, top: 35, labelWidth: 0, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            selectedNote.IsLoading = false;
        }

        #endregion

        #region toolStripButton Events
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try {
                if (selectedNote.Id == 0)
                {
                    InstructionType instructionType = InstructionType.UNKNOWN;
                    int referenceId = 0;
                    string referenceNumber = "";
                    string comment = "";

                    if (this.Retirement != null)
                    {
                        this.Retirement.Notes.Add(selectedNote);

                        instructionType = InstructionType.RETIRE_POLICY_NOTE;
                        referenceId = this.Retirement.Id;
                        referenceNumber = this.Retirement.ReferenceNo;
                        comment = this.Retirement.Description;
                    }
                    if (this.Investment != null)
                    {
                        this.Investment.Notes.Add(selectedNote);

                        instructionType = InstructionType.INVEST_POLICY_NOTE; ;
                        referenceId = this.Investment.Id;
                        referenceNumber = this.Investment.ReferenceNo;
                        comment = this.Investment.Description;
                    }
                    if (this.Education != null)
                    {
                        this.Education.Notes.Add(selectedNote);

                        instructionType = InstructionType.EDU_POLICY_NOTE;
                        referenceId = this.Education.Id;
                        referenceNumber = this.Education.ReferenceNo;
                        comment = this.Education.Description;

                    }
                    if (this.Medical != null)
                    {
                        this.Medical.Notes.Add(selectedNote);

                        instructionType = InstructionType.MEDICAL_POLICY_NOTE;
                        referenceId = this.Medical.Id;
                        referenceNumber = this.Medical.ReferenceNo;
                        comment = this.Medical.Description;
                    }
                    if (this.Life != null)
                    {
                        this.Life.Notes.Add(selectedNote);

                        instructionType = InstructionType.LIFE_POLICY_NOTE;
                        referenceId = this.Life.Id;
                        referenceNumber = this.Life.ReferenceNo;
                        comment = this.Life.Description;

                    }
                    if (this.IncomeAsset != null)
                    {
                        this.IncomeAsset.Notes.Add(selectedNote);

                        instructionType = InstructionType.ASSET_POLICY_NOTE;
                        referenceId = this.IncomeAsset.Id;
                        referenceNumber = this.IncomeAsset.ReferenceNo;
                        comment = this.IncomeAsset.Description;

                    }

                    if (this.Need != null)
                    {
                        this.Need.Notes.Add(selectedNote);

                        instructionType = InstructionType.RETIRE_POLICY_NOTE;
                        referenceId = this.Need.Id;
                        referenceNumber = this.Need.ReferenceNo;
                        comment = this.Need.Description;
                    }

                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.Notes.Add(selectedNote);

                        instructionType = InstructionType.EDU_POLICY_NOTE;
                        referenceId = this.EducationNeed.Id;
                        referenceNumber = this.EducationNeed.ReferenceNo;
                        comment = this.EducationNeed.Description;
                    }

                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.Notes.Add(selectedNote);

                        instructionType = InstructionType.INVEST_POLICY_NOTE;
                        referenceId = this.InvestmentNeed.Id;
                        referenceNumber = this.InvestmentNeed.ReferenceNo;
                        comment = this.InvestmentNeed.Description;
                    }


                    if (MessageBoxExt.ShowQuestion("Do you wish to create a Task for this note?"))
                    {
                        Instruction = new Instruction()
                        {
                            Status = InstructionStatus.UpdatePending.ToText(),
                            InstructionType = instructionType,
                            Type = instructionType.ToText(),
                            Comment = comment,
                            ReferenceId = referenceId,
                            ReferenceNo = referenceNumber,

                            UpdateDate = DateTime.Now,
                            UpdateBy = Program.User.Username
                        };

                        //Add the Admin Task
                        AddClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                        selectedNote.InstructionId = Instruction.Id;
                    }
                }

                UpdateNote();

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);
            }
        }
        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedNote.Id == 0)
                    return;

                selectedNote = new Note();

                Initialise_PolicyNotePanel();

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);
            }
        }
        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedNote.Id != 0)
                {
                    if (!Program.User.IsAdministrator)
                    {
                        MessageBoxExt.ShowWarning("You must have Administrator role to remove this Note");
                        return;
                    }

                    if (!MessageBoxExt.ShowQuestion("Are you sure you wish to delete this note ?"))
                        return;

                    //Update the Admin Task
                    if (selectedNote.InstructionId > 0)
                    {
                        Instruction = GetInstructionEvent(selectedNote.InstructionId, new EventArgs());
                        if (Instruction != null)
                        {
                            //Instruction.Status = InstructionStatus.Cancelled.ToText();
                            DeleteClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                        }
                    }

                    if (this.Retirement != null)
                    {
                        this.Retirement.Notes.Remove(selectedNote);
                        Program.Repository.Update<Retirement, int>(this.Retirement);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Retirement.Notes);
                    }
                    if (this.Investment != null)
                    {
                        this.Investment.Notes.Remove(selectedNote);
                        Program.Repository.Update<Investment, int>(this.Investment);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Investment.Notes);

                    }
                    if (this.Education != null)
                    {
                        this.Education.Notes.Remove(selectedNote);
                        Program.Repository.Update<Education, int>(this.Education);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Education.Notes);

                    }
                    if (this.Medical != null)
                    {
                        this.Medical.Notes.Remove(selectedNote);
                        Program.Repository.Update<Medical, int>(this.Medical);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Medical.Notes);
                    }
                    if (this.Life != null)
                    {
                        this.Life.Notes.Remove(selectedNote);
                        Program.Repository.Update<Life, int>(this.Life);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Life.Notes);

                    }
                    if (this.IncomeAsset != null)
                    {
                        this.IncomeAsset.Notes.Remove(selectedNote);
                        Program.Repository.Update<IncomeAsset, int>(this.IncomeAsset);

                        selectedNote = null;
                        Initialise_SelectPanel(this.IncomeAsset.Notes);
                    }
                    if (this.Need != null)
                    {
                        this.Need.Notes.Remove(selectedNote);
                        Program.Repository.Update<Need, int>(this.Need);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Need.Notes);
                    }
                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.Notes.Remove(selectedNote);
                        Program.Repository.Update<EducationNeed, int>(this.EducationNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.EducationNeed.Notes);
                    }
                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.Notes.Remove(selectedNote);
                        Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.InvestmentNeed.Notes);
                    }
                }


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);
                xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
            }
        }

        private void UpdateNote()
        {
            if (this.Retirement != null)
            {
                Program.Repository.Update<Retirement, int>(this.Retirement);
                Initialise_SelectPanel(this.Retirement.Notes);
            }
            if (this.Investment != null)
            {
                Program.Repository.Update<Investment, int>(this.Investment);
                Initialise_SelectPanel(this.Investment.Notes);
            }
            if (this.Education != null)
            {
                Program.Repository.Update<Education, int>(this.Education);
                Initialise_SelectPanel(this.Education.Notes);
            }
            if (this.Medical != null)
            {
                Program.Repository.Update<Medical, int>(this.Medical);
                Initialise_SelectPanel(this.Medical.Notes);
            }
            if (this.Life != null)
            {
                Program.Repository.Update<Life, int>(this.Life);
                Initialise_SelectPanel(this.Life.Notes);
            }
            if (this.IncomeAsset != null)
            {
                Program.Repository.Update<IncomeAsset, int>(this.IncomeAsset);
                Initialise_SelectPanel(this.IncomeAsset.Notes);
            }
            if (this.Need != null)
            {
                Program.Repository.Update<Need, int>(this.Need);
                Initialise_SelectPanel(this.Need.Notes);
            }
            if (this.EducationNeed != null)
            {
                Program.Repository.Update<EducationNeed, int>(this.EducationNeed);
                Initialise_SelectPanel(this.EducationNeed.Notes);
            }
            if (this.InvestmentNeed != null)
            {
                Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);
                Initialise_SelectPanel(this.InvestmentNeed.Notes);
            }

            //Update the Admin Task
            if (Instruction != null)
            {
                if (selectedNote.IsCompleted)
                    Instruction.Status = InstructionStatus.Completed.ToText();

                UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
            }
            else
            {
                if (selectedNote.InstructionId > 0)
                {
                    Instruction = GetInstructionEvent(selectedNote.InstructionId, new EventArgs());
                    if (Instruction != null)
                    {
                        if (selectedNote.IsCompleted)
                            Instruction.Status = InstructionStatus.Completed.ToText();

                        UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                    }
                }
            }

        }
        #endregion

        #region Event handlers
        private void Note_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetEditMode(true);

                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        //Show the Note
        private void DataGridRowHeaderSelect_EventHandler(object sender, EventArgs e)
        {
            CellContext context = (CellContext)sender;

            try
            {
                SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                BoundList<Note> bList = dGrid.DataSource as BoundList<Note>;
                Note note = bList[context.Position.Row - 1] as Note;

                if (note == null)
                    return;

                selectedNote = note;

                Initialise_PolicyNotePanel();
            }
            catch (Exception x)
            {

            }
        }
        private void DataGridViewSelect_EventHandler(object sender, EventArgs e)
        {
            try
            {
                MetroGrid grid = sender as MetroGrid;

                int i = grid.CurrentCell.RowIndex;
                Note note = Retirement.Notes[i] as Note;

                if (note == null)
                    return;

                selectedNote = note;

                Initialise_PolicyNotePanel();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
        }
        private void DataGridViewCellSelect_EventHandler(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                Note note = Retirement.Notes[e.RowIndex] as Note;

                if (note == null)
                    return;

                selectedNote = note;

                Initialise_PolicyNotePanel();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
        }
        private void DataGridViewRowHeaderSelect_EventHandler(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                Note note = Retirement.Notes[e.RowIndex] as Note;

                if (note == null)
                    return;

                selectedNote = note;

                Initialise_PolicyNotePanel();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }
        #endregion

        #region Form Events
        protected override void OnResizeEnd(EventArgs e)
        {
            Initialise_PolicyNotePanel();

            base.OnResizeEnd(e);
        }

        #endregion

        private IList<TreeViewModel> GroupByYear(IList<Note> notes)
        {
            // Sort and group the dataList
            var groups = notes
                .OrderBy(x => x.CreateDate.Year).ThenBy(x => x.CreateDate.Month).ThenBy(x=>x.CreateDate.Day)
                .ToLookup(x => x.CreateDate.ToString(), x => new TreeViewModel
                {
                    Key = x.CreateDate.ToShortDateString(),
                    Name = x.Name,

                });

            // Assign children
            foreach (var item in groups.SelectMany(x => x))
            {
                item.childCategories = groups[item.Key].ToList();
            }

           return groups[null].ToList();
        }
    }

    public class TreeViewModel
    {
        public string Name { get; set; }
        public string Key { get; set; }

        public string ImageName { get; set; }

        public IList<TreeViewModel> childCategories { get; set; }
    }
}
