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
using Finx.App.UserControls;
using easiplan.domain;

namespace Finx.App.Forms
{
    public partial class frmMetroClientAdviceRecord : MetroForm
    {
        ClientSearchModel searchModel = new ClientSearchModel();

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

        IList<Note> Notes = new List<Note>();
        IList<Note> filteredNotes = new List<Note>();
        Note selectedNote = null;

        PolicyAction Action = PolicyAction.AmendPolicy;
        #endregion

        #region Public Variables
        public Client Client { get; set; }
        public object SelectedItem { get; set; }
        #endregion

        #region Constructors
        //Base Constructor
        public frmMetroClientAdviceRecord(string Title, bool readOnly, PolicyAction action)
        {
            InitializeComponent();

            ReadOnly = readOnly;
            Action = action;

            this.Text = Title;// string.Empty;
            //this.SubTitle = string.Format("{0}", "Policy Notes");

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            #region xToolBarMenu1
            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Client Advice Record");
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.notes_32;

            xToolBarMenu1.tbRefresh.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbRefresh.Text = "Delete";
            xToolBarMenu1.RefreshClicked += toolStripButton_Delete_Click;
            xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;

            xToolBarMenu1.tbSave.Visible = !ReadOnly;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;



            //This is where we are messing

            xToolBarMenu1.tbEdit.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbEdit.Text = "New Note";
            xToolBarMenu1.EditClicked += toolStripButton_Add_Click;


            //Toolstrip menu items to possibly be moved to the repository or something
            /*
            ToolStripMenuItem newNote = new ToolStripMenuItem()
            {
                Text = "Blank template",
                //Tag = ,
            };

            newNote.Click += new EventHandler(toolStripButton_Add_Click);

            ToolStripMenuItem riskNote = new ToolStripMenuItem()
            {
                Text = "Risk",
                //Tag = ,
            };
            riskNote.Click += new EventHandler(toolStripButton_InvestmentCompliance_Click);
            //tM.Click += new EventHandler(DocumentTemplate_OnClick);

            xToolBarMenu1.tbNewNote.DropDownItems.Add(newNote);
            xToolBarMenu1.tbNewNote.DropDownItems.Add(riskNote);


            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbNewNote.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy;
            */

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            #endregion

            this.xInput_ShowCompletedTasks.ControlTypes = ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.MappedField = "ShowCompletedTask";
            this.xInput_ShowCompletedTasks.LableText = "Show Completed notes";
            this.xInput_ShowCompletedTasks.Model = searchModel;
            this.xInput_ShowCompletedTasks.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_ShowCompletedTasks.chkBox.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;
        }

        public frmMetroClientAdviceRecord(Retirement model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;


            Initialise_SelectPanel(model.Notes);
        }

        public frmMetroClientAdviceRecord(Investment model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Investment = model;

            Initialise_SelectPanel(model.Notes);


        }

        public frmMetroClientAdviceRecord(Education model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(Medical model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(Life model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(IncomeAsset model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(Need model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            Need = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(EducationNeed model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            EducationNeed = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(InvestmentNeed model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            InvestmentNeed = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(Instruction model, bool readOnly, PolicyAction action) : this(model.Name, readOnly, action)
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

        void Initialise_SelectPanel(IList<Note> notes = null)
        {
            if (notes != null)
                Notes = notes;

            if (Notes == null)
                filteredNotes = new List<Note>();
            else
                filteredNotes = Notes.Where(x => x.IsCompleted == searchModel.ShowCompletedTask).ToList();

            #region Notes Grid
            this.dataGrid_Notes.Initialise1(filteredNotes, column =>
            {

                column.For(c => c.NoteDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.Text, "Note", new StringEditor());
                column.For(c => c.IsCompleted, "Completed");
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updt By", new StringEditor());

            },
            RowSelectEventHandler: Notes_RowSelectEventHandlerChanged,
            ReadOnly: true,
            AllowDelete: false)
            .Format1(true, fixedCols: 1);
            #endregion

            if (Notes.Count > 0)
                selectedNote = filteredNotes.LastOrDefault();
            else
                selectedNote = new Note();

            Initialise_PolicyNotePanel();
        }

        void Initialise_PolicyNotePanel()
        {
            this.metroPanel_Select.Controls.Clear();

            this.metroPanel_PolicyNote.Controls.Clear();

            if (selectedNote == null)
                return;

            selectedNote.IsLoading = true;

            this.metroPanel_Select.Initialise(selectedNote, cntr =>
            {
                cntr.For(x => x.NoteDate, "Note Date ...", new MetroTextBoxEditor(160).ReadOnly(true));
                cntr.For(x => x.IsCompleted, "Completed", new MetroCheckBoxEditor().ReadOnly(ReadOnly));
            }, left: 10, top: 5, labelWidth: 120, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            this.metroPanel_PolicyNote.Initialise<Note>(selectedNote, cntr =>
            {

                cntr.For(x => x.Text, "Note", new MetroMultiLineTextBoxEditor(Width: this.metroPanel_PolicyNote.Width - 20, Height: this.metroPanel_PolicyNote.Height - 30).ReadOnly(ReadOnly));


            }, left: 10, top: 0, labelWidth: 100, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

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
            try
            {
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

        //Method to be worked on if we end up doing the new note drop down
        /*
        private void toolStripButton_InvestmentCompliance_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedNote.Id == 0)
                    return;

                selectedNote = new Note();
                selectedNote.Text = "High I am a selected not";
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
        */

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
            searchModel.ShowCompletedTask = false;

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

        private void Notes_RowSelectEventHandlerChanged(object sender, SourceGrid.RowEventArgs e)
        {

            SourceGrid.Selection.RowSelection selection = sender as SourceGrid.Selection.RowSelection;
            if (selection == null)
                return;

            if (filteredNotes.Count == 0 || e.Row > filteredNotes.Count)
                selectedNote = null;
            else
                selectedNote = filteredNotes[e.Row - 1];


            Initialise_PolicyNotePanel();
        }

        private void XInput_ShowCompletedTask_KeyPressed(object sender, EventArgs e)
        {
            searchModel.ShowCompletedTask = !searchModel.ShowCompletedTask;

            Initialise_SelectPanel();
        }
        #endregion

        #region Form Events
        protected override void OnResizeEnd(EventArgs e)
        {
            Initialise_PolicyNotePanel();

            base.OnResizeEnd(e);
        }

        #endregion

        public class ClientSearchModel : BaseEntity<int>
        {
            public bool ShowCompletedTask { get; set; }
        }

        private void metroPanel_PolicyNote_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMetroClientAdviceRecord_Load(object sender, EventArgs e)
        {

        }
    }

}
