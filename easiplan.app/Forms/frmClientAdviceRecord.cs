/* Client Advice Record and Notes
 * Author: Yusuf Jattiem
 * Create Date: 14=JuL-2023
 * REVISION : 
 *  21-JUL-2023 : Add bindable custom UserControls
 *              : Add extension to Clone lists
 *              : Add Reset bindings methods
 *              : Checke Equals in reset methods
 * 
 */
using DevAge.ComponentModel;
using easiplan.app.Models;
using easiplan.domain.Entities;
using Finx.App;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.UserControls;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Controls.Ext;
using my.domain.lib.core.Domain;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace easiplan.app.Forms
{
    public partial class frmClientAdviceRecord : Form
    {
        readonly string lblHeading = "";
        readonly string Title = "";
        bool ReadOnly = true;
        readonly PolicyAction Action;

        static int left = 10;
        static int top = 10;

        #region Entities
        readonly Retirement Retirement = null;
        readonly Investment Investment = null;
        readonly Education Education = null;
        readonly Medical Medical = null;
        readonly Life Life = null;
        readonly IncomeAsset IncomeAsset = null;
        readonly Need RetirementNeed = null;
        readonly EducationNeed EducationNeed = null;
        readonly InvestmentNeed InvestmentNeed = null;
        readonly RiskCoverNeed RiskCoverNeed = null;
        #endregion

        #region Binding Sources
        readonly IList<ClientAdviceRecord> clientAdviceRecords = null;
        ClientAdviceRecord clientAdviceRecord = null;
        readonly BindingSource bsClientAdviceRecord = new BindingSource();

        readonly IList<MedicalAidAdviceRecord> medicalAidAdviceRecords = null;
        MedicalAidAdviceRecord medicalAidAdviceRecord = null;
        readonly BindingSource bsMedicalAidAdviceRecord = new BindingSource();

        readonly IList<RiskAdviceRecord> riskAdviceRecords = null;
        RiskAdviceRecord riskAdviceRecord = null;
        readonly BindingSource bsRiskAdviceRecord = new BindingSource();

        readonly IList<Note> archiveNotes = null;
        Note archiveNote = null;
        readonly BindingSource bsArchiveNote = new BindingSource();

        readonly IList<Fund> fundsList = new List<Fund>();

        #endregion

        #region Shared UserControls
        /// <summary>
        /// Custom UserControls that are used to populate the panels at run-time
        /// </summary>

        readonly Label lblSubHeading = new Label()
        {
            Top = top,
            Left = left,
            Width = 660,
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = System.Drawing.Color.White,
        };

        readonly xCheckBoxListH cbhProductKnowledge = new xCheckBoxListH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Product Knowledge and Experience:",
            Hint = "(Describe the client's level of knowledge and experience of the product purchased.)",
            Options = new List<CheckBoxHOption>
                {
                new CheckBoxHOption() { Label = "1 \r\nLow", Value = "1", Width=65 },
                new CheckBoxHOption() { Label = "2 ", Value = "2",Width=65 },
                new CheckBoxHOption() { Label = "3 ", Value = "3",Width=65 },
                new CheckBoxHOption() { Label = "4 ", Value = "4",Width=65 },
                new CheckBoxHOption() { Label = "5 \r\nModerate", Value = "5", Width=65 },
                new CheckBoxHOption() { Label = "6 ", Value = "6",Width=65 },
                new CheckBoxHOption() { Label = "7 ", Value = "7",Width=65 },
                new CheckBoxHOption() { Label = "8 ", Value = "8",Width=65 },
                new CheckBoxHOption() { Label = "9 ", Value = "9", Width=65 },
                new CheckBoxHOption() { Label = "10 \r\nHigh", Value = "10",Width=65 }
                }
        };

        readonly xCheckBoxListH cbhInvestmentHorizon = new xCheckBoxListH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Investment Horizon:",
            Hint = "(For how long the client expects his/her money to be invested before he/she would like to cash it in ?)",
            Options = new List<CheckBoxHOption>
                {
                new CheckBoxHOption() { Label = "0-2 \r\nYears", Value = "0-2", Width=65 },
                 new CheckBoxHOption() { Label = "2-5 \r\nYears", Value = "2-5", Width=65 },
                  new CheckBoxHOption() { Label = "5-9 \r\nYears", Value = "5-9", Width=65 },
                   new CheckBoxHOption() { Label = "10+ \r\nYears", Value = "10+", Width=65 },
                }
        };

        readonly xCheckBoxListH cbhAccessToCapital = new xCheckBoxListH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Access to Capital:",
            Hint = "(The accessibility and liquidity of the investment)",
            Options = new List<CheckBoxHOption>
                {
                new CheckBoxHOption() { Label = "Need to draw an income", Value = "1", Width=150 },
                 new CheckBoxHOption() { Label = "Always require access to capital", Value = "2", Width=150 },
                  new CheckBoxHOption() { Label = "Do not require access to capital for at least 5 years", Value = "3", Width=150 },

                }
        };

        readonly xTextBoxH tbhNeedsAndOjectives = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Needs and Objectives:",
            Hint = "(What are the client's needs and what does the client wish to achieve by purchasing this financial product ?)",


        };

        readonly xTextBoxH tbhFinancialSituation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Financial Situation:",
            Hint = "(Set out summary of the client's current financial situation.)",

        };

        readonly xTextBoxH tbhOtherInformation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Other Information:",
            Hint = "(The client's retirement age, premium increases, expected growth rates, tax considerations, specific goals etc.)",

        };

        readonly xTextBoxH tbhInitialRecommendation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Initial Recommendation / Advice:",
            Hint = "(Ensure all needs identified are addressed) \r\nProducts \\Funds recommended",
            NoOfHintLines = 2

        };

        readonly xTextBoxH tbhMotivationForRecommendation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Motivation for Recommendation:",
            Hint = "(State why the product recommended will suit the client. When noting reasons, make reference to relevant information above to make your case why the product is suitable. Where this is a replacement, provide an overview of the reasons why the replacement product was concidered to be more sutable to the clients needs.)",
            NoOfHintLines = 3
        };

        readonly xTextBoxH tbhImplentationForRecommendation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Implementation of Recommendation/Advise:",
            Hint = "(Explain what was finally implemented and the reasons thereof.)",

        };

        readonly xTextBoxH tbhReasonsForRecommendation = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width = 660,
            Title = "Final Recommendation/Advice:",
            Hint = "(State what product was purchased, contribution patterns and whether the need or requirement was fully or partially implemented. Where the client elected not to follow the recommendations above, ensure this is documented together with the risks and warnings addressed with the client.)",
            NoOfHintLines = 3
        };

        readonly xTextBoxH tbhArchiveNote = new xTextBoxH
        {
            Top = top,
            Left = left,
            Width =840,
            Height=330,
            Title = "Note",
           
        };

        readonly SourceGrid.DataGrid historyGrid = new SourceGrid.DataGrid()
        {
            Dock = DockStyle.Fill,
            AutoScroll = false,
            BackColor = Color.White,
        };

        readonly SourceGrid.DataGrid currentRecordGrid = new SourceGrid.DataGrid()
        {
            Dock = DockStyle.Fill,
            AutoScroll = false,
        };

        readonly SourceGrid.DataGrid historyNotesGrid = new SourceGrid.DataGrid()
        {
            Dock = DockStyle.Fill,
            AutoScroll = false,
            BackColor = Color.White,
        };

        readonly SourceGrid.DataGrid currentNoteRecordGrid = new SourceGrid.DataGrid()
        {
            Dock = DockStyle.Fill,
            AutoScroll = false,
        };

        #endregion

        #region Constructors
        public frmClientAdviceRecord(Retirement model, bool readOnly, PolicyAction action) :this(model,action){

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;       

            lblHeading = "Retirement Advice Record";            
            Title = $"{Retirement.Description} [{Retirement.ReferenceNo}]";

            clientAdviceRecords = Retirement.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            Retirement.Calculate();
            fundsList = Retirement.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = Retirement.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);

        }
        public frmClientAdviceRecord(Investment model,bool readOnly, PolicyAction action) : this(model,action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Investment = model;

            lblHeading = "Investment Advice Record";
            Title = $"{Investment.Description} [{Investment.ReferenceNo}]";

            clientAdviceRecords = Investment.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            Investment.Calculate();
            fundsList = Investment.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = Investment.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(Education model, bool readOnly, PolicyAction action) : this(model,action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;

            lblHeading = "Education Advice Record";
            Title = $"{Education.Description} [{Education.ReferenceNo}]";

            clientAdviceRecords = Education.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            Education.Calculate();
            fundsList = Education.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = Education.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(Medical model, bool readOnly, PolicyAction action)     : this(model, action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;

            lblHeading = "Medical Advice Record";
            Title = $"{Medical.Description} [{Medical.ReferenceNo}]";

            medicalAidAdviceRecords = Medical.AdviceRecords.Clone();
            medicalAidAdviceRecord = medicalAidAdviceRecords.LastOrDefault();
            bsMedicalAidAdviceRecord.DataSource = medicalAidAdviceRecord;

            Initialise_CarPanel(readOnly);

            archiveNotes = Medical.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(Life model, bool readOnly, PolicyAction action) : this(model,action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;

            lblHeading = "Risk/Life Advice Record";
            Title = $"{Life.Description} [{Life.ReferenceNo}]";

            riskAdviceRecords = Life.AdviceRecords.Clone();
            riskAdviceRecord = riskAdviceRecords.LastOrDefault();
            bsRiskAdviceRecord.DataSource = riskAdviceRecord;

            Initialise_CarPanel(readOnly);

            archiveNotes = Life.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(IncomeAsset model, bool readOnly, PolicyAction action) : this(model,action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;

            lblHeading = "IncomeAsset Advice Record";
            Title = $"{IncomeAsset.Description} [{IncomeAsset.ReferenceNo}]";

            clientAdviceRecords = IncomeAsset.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();      
            bsClientAdviceRecord.DataSource = IncomeAsset.CurrentAdviceRecord;

            Initialise_CarPanel(readOnly);

            archiveNotes = IncomeAsset.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }        
        public frmClientAdviceRecord(Need model, bool readOnly, PolicyAction action) : this(model,action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            RetirementNeed = model;

            lblHeading = "Retirement Need Advice Record";
            Title = $"{RetirementNeed.Description} [{RetirementNeed.ReferenceNo}]";

            clientAdviceRecords = RetirementNeed.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            RetirementNeed.Calculate();
            fundsList = RetirementNeed.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = RetirementNeed.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);

        }
        public frmClientAdviceRecord(EducationNeed model, bool readOnly, PolicyAction action) : this(model,action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            EducationNeed = model;

            lblHeading = "Education Need Advice Record";
            Title = $"{EducationNeed.Description} [{EducationNeed.ReferenceNo}]";

            clientAdviceRecords = EducationNeed.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            EducationNeed.Calculate();
            fundsList = EducationNeed.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = EducationNeed.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(InvestmentNeed model, bool readOnly, PolicyAction action) : this(model, action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            InvestmentNeed = model;

            lblHeading = "Investment Need Advice Record";
            Title = $"{InvestmentNeed.Description} [{InvestmentNeed.ReferenceNo}]";

            clientAdviceRecords = InvestmentNeed.AdviceRecords.Clone();
            clientAdviceRecord = clientAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = clientAdviceRecord;

            InvestmentNeed.Calculate();
            fundsList = InvestmentNeed.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = InvestmentNeed.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        public frmClientAdviceRecord(RiskCoverNeed model, bool readOnly, PolicyAction action) : this(model, action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            RiskCoverNeed = model;

            lblHeading = "RiskCover Need Advice Record";
            Title = $"{RiskCoverNeed.Description} [{RiskCoverNeed.ReferenceNo}]";

            riskAdviceRecords = RiskCoverNeed.RiskAdviceRecords.Clone();
            riskAdviceRecord = riskAdviceRecords.LastOrDefault();
            bsClientAdviceRecord.DataSource = riskAdviceRecord ;

            RiskCoverNeed.Calculate();
            fundsList = RiskCoverNeed.Funds;

            Initialise_CarPanel(readOnly);

            archiveNotes = RiskCoverNeed.Notes.Clone();
            archiveNote = archiveNotes.LastOrDefault();
            bsArchiveNote.DataSource = archiveNote;

            Initialise_NotesPanel(readOnly);
        }
        #endregion

        #region Private
        private frmClientAdviceRecord(object model,PolicyAction action)
        {
            InitializeComponent();

            SelectedItem = model;
            Action = action;

            this.Text = "Client Advice Record [CAR]";
            this.Width =1140;

            this.metroTabPage2.Text = "Advice Records |";
            this.splitContainer2.FixedPanel = FixedPanel.Panel1;            
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.SplitterDistance = 245;
            this.metroPanel_Car_History.BorderStyle = BorderStyle.FixedSingle;
            this.metroPanel_CAR_header.BorderStyle = BorderStyle.FixedSingle;
            this.metroPanel_CAR_header.BackColor = Color.WhiteSmoke;
            this.metroPanel_CAR_header.Height = 60;
            this.metroPanel_CAR.BorderStyle = BorderStyle.FixedSingle;

            this.metroTabPage1.Text = "Notes";
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.SplitterDistance = 245;

            this.metroTabControl1.SelectedIndex = 0;

            this.metroPanel_Notes_History.BorderStyle= BorderStyle.FixedSingle;
            this.metroPanel_Notes_Header.BorderStyle = BorderStyle.FixedSingle;
            this.metroPanel_Notes_Header.BackColor = Color.WhiteSmoke;
            this.metroPanel_Notes_Header.Height = 60;
            this.metroPanel_Notes.BorderStyle = BorderStyle.FixedSingle;

            #region xToolBarMenu1 Event Handlers
            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);            
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.notes_32;

            xToolBarMenu1.RefreshClicked += toolStripButton_Delete_Click;           
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;
            xToolBarMenu1.EditClicked += toolStripButton_Add_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            #endregion

            #region Binding Source Event Handlers

            bsClientAdviceRecord.ListChanged += BsClientAdviceRecord_ListChanged;
            bsMedicalAidAdviceRecord.ListChanged += BsClientAdviceRecord_ListChanged;
            bsRiskAdviceRecord.ListChanged += BsClientAdviceRecord_ListChanged;
            bsArchiveNote.ListChanged += BsClientAdviceRecord_ListChanged;

            #endregion

        }

        private void ClientAdviceRecord_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            xToolBarMenu1.SetCAREdit(true);
        }
        private void BsClientAdviceRecord_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if(e.PropertyDescriptor!=null)
                xToolBarMenu1.SetCAREdit(true);
        }
        private void clientAdviceRecords_RowSelectEventHandler(object sender, RowEventArgs e)
        {
            if (e.Row <= clientAdviceRecords.Count)
            {
                ResetClientAdviceRecordBindings(e.Row);              
            }

        }
        private void medicalAdviceRecords_RowSelectEventHandler(object sender, RowEventArgs e)
        {
            if (e.Row <=medicalAidAdviceRecords.Count)
            {
                ResetMedicalAidAdviceRecordBindings(e.Row);
            }

        }
        private void riskAdviceRecords_RowSelectEventHandler(object sender, RowEventArgs e)
        {
            if (e.Row <= riskAdviceRecords.Count)
            {
                ResetRiskAdviceRecordBindings(e.Row);
            }

        }
        private void archiveNotes_RowSelectEventHandler(object sender, RowEventArgs e)
        {
            if (e.Row <= archiveNotes.Count)
            {
                ResetNoteRecordBindings(e.Row);
            }

        }
        
        private void ResetClientAdviceRecordBindings(int row){
            try
            {
                clientAdviceRecord = clientAdviceRecords[row-1];

                //rebind binding source
                if (!clientAdviceRecord.Equals(bsClientAdviceRecord.DataSource))
                {
                    bsClientAdviceRecord.Clear();
                    bsClientAdviceRecord.DataSource = clientAdviceRecord;
                    bsClientAdviceRecord.ResetBindings(true);

                    //current Advice Record  
                    currentRecordGrid.DataSource = new BoundList<ClientAdviceRecord>(new List<ClientAdviceRecord> { clientAdviceRecord });
                }
            }catch{

            }
        }
        private void ResetMedicalAidAdviceRecordBindings(int row)
        {
            try
            {
                medicalAidAdviceRecord = medicalAidAdviceRecords[row-1];

                //rebind binding source
                if (!medicalAidAdviceRecord.Equals(bsMedicalAidAdviceRecord.DataSource))
                {
                    bsMedicalAidAdviceRecord.Clear();
                    bsMedicalAidAdviceRecord.DataSource = medicalAidAdviceRecord;
                    bsMedicalAidAdviceRecord.ResetBindings(true);

                    //current Advice Record                
                    medicalAidAdviceRecord.Initialise_Names();
                    currentRecordGrid.DataSource = new BoundList<MedicalAidAdviceRecord>(new List<MedicalAidAdviceRecord> { medicalAidAdviceRecord });
                }
            }catch{ }
        }
        private void ResetRiskAdviceRecordBindings(int row)
        {
            try
            {
               
                riskAdviceRecord = riskAdviceRecords[row-1];

                //rebind binding source
                if (!riskAdviceRecord.Equals(bsRiskAdviceRecord.DataSource))
                {
                    bsRiskAdviceRecord.Clear();
                    bsRiskAdviceRecord.DataSource = riskAdviceRecord;
                    bsRiskAdviceRecord.ResetBindings(true);

                    //current Advice Record
                    riskAdviceRecord.Initialise_Names();
                    currentRecordGrid.DataSource = new BoundList<RiskAdviceRecord>(new List<RiskAdviceRecord> { riskAdviceRecord });
                }
            }
            catch { }
        }
        private void ResetNoteRecordBindings(int row)
        {
            try
            {
                archiveNote = archiveNotes[row-1];

                //rebind binding source
                if (!archiveNote.Equals(bsArchiveNote.DataSource))
                {
                    bsArchiveNote.Clear();
                    bsArchiveNote.DataSource = archiveNote;
                    bsArchiveNote.ResetBindings(true);

                    currentNoteRecordGrid.DataSource = new BoundList<Note>(new List<Note> { archiveNote });
                }
            }
            catch { }
        }

        private void Initialise_CarPanel(bool readOnly){

            ReadOnly = readOnly;

            Initialise_ToolStrip();

            left = 10;
            top = 10;

            #region Caption
            if (!string.IsNullOrEmpty(lblHeading))
            {
                this.metroPanel_CAR.Controls.Add(new Label()
                {
                    Text = lblHeading,
                    Top = top,
                    Left = left,
                    Width = 660,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = System.Drawing.Color.White,
                });
                top += 30;
            }
            #endregion
            
            #region ClientAdviceRecord            
            if(clientAdviceRecords!=null)
            {
                #region History
                               
                historyGrid.Initialise1(clientAdviceRecords, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new DateEditor(true), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);

                },
                 AllowDelete: false, AllowAddNew: false, RowSelectEventHandler: clientAdviceRecords_RowSelectEventHandler)
                .Format1(true, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

                this.metroPanel_Car_History.Controls.Add(historyGrid);

                historyGrid.Selection.SelectRow(clientAdviceRecords.Count, true);
                #endregion

                #region Current Record

                currentRecordGrid.Initialise1((IList<ClientAdviceRecord>)bsClientAdviceRecord.List, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new StringEditor(true), Width: 200);
                    column.For(c => c.InitialFee, "Initial fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.OngoingFee, "Ongoing fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);
                    column.For(c => c.UpdateDate, "Last Update", new DateEditor(true), Width: 115);
                    column.For(c => c.UpdateBy, "Update By", new StringEditor(true), Width: 200);

                },
                 AllowDelete: false, AllowAddNew: false)
                .Format1(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

                this.metroPanel_CAR_header.Controls.Add(currentRecordGrid);

                #endregion

                //Heading
                lblSubHeading.Top = top;
                lblSubHeading.DataBindings.Add("Text", bsClientAdviceRecord, "AdviceDate");
                this.metroPanel_CAR.Controls.Add(lblSubHeading);
                top += 30;

                //Funds
                this.metroPanel_CAR.Controls.Add(new MetroLabel() { Text = "Funds", Top = top, Left = left, Width = 660 });
                top += 30;
                SourceGrid.DataGrid fundsGrid = new SourceGrid.DataGrid()
                {
                    Top = top,
                    Left = left,
                    Width=640,
                    BackColor = Color.White,
                };
                fundsGrid.Initialise1(fundsList, column =>
                {
                    column.For(c => c.FundCode, "Fund Code", new StringEditor(true), Width: 100);
                    column.For(c => c.Description, "Fund Name", new StringEditor(true), Width: 300);
                    column.For(c => c.MonthlyContribution, "Premium", new CurrencyEditor(true), Width: 100);
                    column.For(c => c.SplitPerc, "Split %", new PercentageEditor(true), Width: 80);                    
                },
                 AllowDelete: false, AllowAddNew: false)
                .Format1(true, fixedCols: 0, format: MetroControlExt.GridFormats.Format1);
                this.metroPanel_CAR.Controls.Add(fundsGrid);
                top += fundsGrid.Height + 5;

                //1. Product Knowledge and Experience
                cbhProductKnowledge.Top = top;
                cbhProductKnowledge.AddDataBinding(bsClientAdviceRecord, "ProductKnowledge");
                this.metroPanel_CAR.Controls.Add(cbhProductKnowledge);
                top += cbhProductKnowledge.Height;

                //2. Investment Horizon
                cbhInvestmentHorizon.Top = top;
                cbhInvestmentHorizon.AddDataBinding(bsClientAdviceRecord, "InvestmentHorizen");
                this.metroPanel_CAR.Controls.Add(cbhInvestmentHorizon);
                top += cbhInvestmentHorizon.Height;

                //3. Access to Capital
                cbhAccessToCapital.Top = top;
                cbhAccessToCapital.AddDataBinding(bsClientAdviceRecord, "AccessToCapital");
                this.metroPanel_CAR.Controls.Add(cbhAccessToCapital);
                top += cbhAccessToCapital.Height;

                //4. Needs and Objectives  
                tbhNeedsAndOjectives.Top = top;
                tbhNeedsAndOjectives.AddDataBinding(bsClientAdviceRecord, "NeedsAndObjectives");
                this.metroPanel_CAR.Controls.Add(tbhNeedsAndOjectives);
                top += tbhNeedsAndOjectives.Height;

                //5. Financial Situation
                tbhFinancialSituation.Top = top;
                tbhFinancialSituation.AddDataBinding(bsClientAdviceRecord, "FinancialSituation");
                this.metroPanel_CAR.Controls.Add(tbhFinancialSituation);
                top += tbhFinancialSituation.Height;

                //6. Other Information
                tbhOtherInformation.Top = top;
                tbhOtherInformation.AddDataBinding(bsClientAdviceRecord, "OtherInformation");
                this.metroPanel_CAR.Controls.Add(tbhOtherInformation);
                top += tbhOtherInformation.Height;

                //7. Initial Recommendation
                tbhInitialRecommendation.Top = top;
                tbhInitialRecommendation.AddDataBinding(bsClientAdviceRecord, "RecommendedFunds");
                this.metroPanel_CAR.Controls.Add(tbhInitialRecommendation);
                top += tbhInitialRecommendation.Height;

                //8. Motivation for Recommendation
                tbhMotivationForRecommendation.Top = top;
                tbhMotivationForRecommendation.AddDataBinding(bsClientAdviceRecord, "ImplementedMotivation");
                this.metroPanel_CAR.Controls.Add(tbhMotivationForRecommendation);
                top += tbhMotivationForRecommendation.Height;

                //9. Implementation of Recommendation
                tbhImplentationForRecommendation.Top = top;
                tbhImplentationForRecommendation.AddDataBinding(bsClientAdviceRecord, "ImplementedProduct");
                this.metroPanel_CAR.Controls.Add(tbhImplentationForRecommendation);
                top += tbhImplentationForRecommendation.Height;

                //10. Reasons of Recommendation
                tbhReasonsForRecommendation.Top = top;
                tbhReasonsForRecommendation.AddDataBinding(bsClientAdviceRecord, "Motivation");
                this.metroPanel_CAR.Controls.Add(tbhReasonsForRecommendation);
                top += tbhReasonsForRecommendation.Height;

            }
            #endregion

            #region MedicalAdviceRecord
            if (medicalAidAdviceRecords != null)
            {
                #region History

                historyGrid.Initialise1(medicalAidAdviceRecords, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new DateEditor(true), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);

                },
                 AllowDelete: false, AllowAddNew: false, RowSelectEventHandler: medicalAdviceRecords_RowSelectEventHandler)
                .Format1(true, fixedCols: 1, format: MetroControlExt.GridFormats.Format2); ;

                this.metroPanel_Car_History.Controls.Add(historyGrid);

                historyGrid.Selection.SelectRow(medicalAidAdviceRecords.Count, true);
                #endregion

                #region Current Record

                currentRecordGrid.Initialise1((IList<MedicalAidAdviceRecord>)bsMedicalAidAdviceRecord.List, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new StringEditor(true), Width: 200);
                    column.For(c => c.InitialFee, "Initial fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.OngoingFee, "Ongoing fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);
                    column.For(c => c.UpdateDate, "Last Update", new DateEditor(true), Width: 115);
                    column.For(c => c.UpdateBy, "Update By", new StringEditor(true), Width: 200);

                },
                 AllowDelete: false, AllowAddNew: false)
                .Format1(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

                this.metroPanel_CAR_header.Controls.Add(currentRecordGrid);

                #endregion

                //Heading
                lblSubHeading.Top = top;
                lblSubHeading.DataBindings.Add("Text", bsMedicalAidAdviceRecord, "AdviceDate");
                this.metroPanel_CAR.Controls.Add(lblSubHeading);
                top += 30;

                cbhProductKnowledge.Top = top;
                cbhProductKnowledge.AddDataBinding(bsMedicalAidAdviceRecord, "ProductKnowledge");
                this.metroPanel_CAR.Controls.Add(cbhProductKnowledge);
                top += cbhProductKnowledge.Height;

                xTextBoxH tbhMedicalConditions = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Medical Conditions:",
                    Hint = "(Chronic conditions, chronic medications, other medical conditions and medication etc.)",

                };
                tbhMedicalConditions.AddDataBinding(bsMedicalAidAdviceRecord, "MedicalConditions");
                this.metroPanel_CAR.Controls.Add(tbhMedicalConditions);
                top += tbhMedicalConditions.Height;

                xTextBoxH tbhCurrentMedicalCover = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Current Medical Cover:",
                    Hint = "(Information on the client's current medical scheme and benefits, period of uninterupted cover, period of any breakage in cover etc.)",
                    NoOfHintLines = 2

                };
                tbhCurrentMedicalCover.AddDataBinding(bsMedicalAidAdviceRecord, "MedicalCover");
                this.metroPanel_CAR.Controls.Add(tbhCurrentMedicalCover);
                top += tbhCurrentMedicalCover.Height;

                //6. Other Information
                tbhOtherInformation.Top = top;
                tbhOtherInformation.AddDataBinding(bsMedicalAidAdviceRecord, "OtherInformation");
                this.metroPanel_CAR.Controls.Add(tbhOtherInformation);
                top += tbhOtherInformation.Height;

                //Hospitalisation
                xTextBoxH tbhHospitalisation = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Hospitalisation:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhHospitalisation.AddDataBinding(bsMedicalAidAdviceRecord, "Hospitalisation");
                this.metroPanel_CAR.Controls.Add(tbhHospitalisation);
                top += tbhHospitalisation.Height;

                //Needs and Goals Identified
                this.metroPanel_CAR.Controls.Add(new MetroLabel() { Text = "Needs and Goals Identified", Top = top, Left = left, Width = 660 });
                top += 30;

                medicalAidAdviceRecord.Initialise_Names();

                IList<MedicalAidNeedsAndGoalsTableRow> needs = new List<MedicalAidNeedsAndGoalsTableRow>
                {
                    medicalAidAdviceRecord.HospitalCoverInfo,
                    medicalAidAdviceRecord.DayToDayBenefitInfo,
                    medicalAidAdviceRecord.ThresholdBenefitInfo,
                    medicalAidAdviceRecord.ChronicBenefitInfo,
                    medicalAidAdviceRecord.SavingsAccountInfo,
                    medicalAidAdviceRecord.HospitalPreferenceInfo,
                    medicalAidAdviceRecord.GapCoverInfo,
                    medicalAidAdviceRecord.OtherInfo
                };

                SourceGrid.DataGrid needsAndGoalsGrid = new SourceGrid.DataGrid()
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Height = 9 * 40 + 5
                };
                needsAndGoalsGrid.Initialise1(needs, column =>
                {
                    column.For(c => c.Name, "Cover", new StringEditor(true), Width: 200);
                    column.For(x => x.CoverDiscussed, "Cover Discussed", new ComboListEditor(ListDataItemType.YesNoString), Width: 100);
                    column.For(c => c.CoverTaken, "Cover Taken", new ComboListEditor(ListDataItemType.YesNoString), Width: 100);
                    column.For(c => c.Comments, "Comments", new MultiLineEditor(), Width: 230);
                },
                 AllowDelete: false, AllowAddNew: false)
                .Format2(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format1);
                this.metroPanel_CAR.Controls.Add(needsAndGoalsGrid);
                top += needsAndGoalsGrid.Height + 10;

                //Chronic conditions
                xTextBoxH tbhChronicConditions = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Chronic Conditions:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhChronicConditions.AddDataBinding(bsMedicalAidAdviceRecord, "ChronicConditions");
                this.metroPanel_CAR.Controls.Add(tbhChronicConditions);
                top += tbhChronicConditions.Height;

                //WaitingPeriods
                xTextBoxH tbhWaitingPeriods = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Waiting Periods:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhWaitingPeriods.AddDataBinding(bsMedicalAidAdviceRecord, "WaitingPeriods");
                this.metroPanel_CAR.Controls.Add(tbhWaitingPeriods);
                top += tbhWaitingPeriods.Height;

                //LateJoinerPenalty
                xTextBoxH tbhLateJoinerPenalty = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Late Joiner Penalty:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhLateJoinerPenalty.AddDataBinding(bsMedicalAidAdviceRecord, "LateJoynerPenalty");
                this.metroPanel_CAR.Controls.Add(tbhLateJoinerPenalty);
                top += tbhLateJoinerPenalty.Height;

                //CoPayments
                xTextBoxH tbhCoPayments = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Co-Payments:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhCoPayments.AddDataBinding(bsMedicalAidAdviceRecord, "CoPayments");
                this.metroPanel_CAR.Controls.Add(tbhCoPayments);
                top += tbhCoPayments.Height;

                //OtherInformation
                xTextBoxH tbhOtherInfo = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Other Important Information:",
                    Hint = "(Whether the client or any dependant has been admitted to hospital in the last 12 months, any planned procedures etc..)",
                    NoOfHintLines = 2,
                };
                tbhOtherInfo.AddDataBinding(bsMedicalAidAdviceRecord, "OtherImportantInformation");
                this.metroPanel_CAR.Controls.Add(tbhOtherInfo);
                top += tbhOtherInfo.Height;

                //Notes
                xTextBoxH tbhNotes = new xTextBoxH
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Title = "Notes:",
                    Hint = "(Add any other notes to self)",

                };
                tbhNotes.AddDataBinding(bsMedicalAidAdviceRecord, "OtherImportantInformation");
                this.metroPanel_CAR.Controls.Add(tbhNotes);
                top += tbhNotes.Height;

                //Comparison for replacement of Medical Schemes
                this.metroPanel_CAR.Controls.Add(new MetroLabel() { Text = "Comparison for Replacement of Medical Scheme", Top = top, Left = left, Width = 660 });
                top += 30;

                IList<MedicalSchemeComparison> comparisons = new List<MedicalSchemeComparison>
                {
                    medicalAidAdviceRecord.PolicyNumberComparison,
                    medicalAidAdviceRecord.InsurerComparison,
                    medicalAidAdviceRecord.ProductNameComparison,
                    medicalAidAdviceRecord.PremiumComparison,
                    medicalAidAdviceRecord.BenefitsComparison,
                    medicalAidAdviceRecord.SavingsAccountComparison,
                    medicalAidAdviceRecord.ChronicBenefitComparison,
                    medicalAidAdviceRecord.HospitalCoverComparison,
                    medicalAidAdviceRecord.LimitsOnCoverComparison,
                    medicalAidAdviceRecord.OtherComparison
                };

                SourceGrid.DataGrid compareMedicalSchemesGrid = new SourceGrid.DataGrid()
                {
                    Top = top,
                    Left = left,
                    Width = 660,
                    Height = 11 * 40 + 5
                };
                compareMedicalSchemesGrid.Initialise1(comparisons, column =>
                {
                    column.For(c => c.Name, "Detail", new StringEditor(true), Width: 200);
                    column.For(x => x.CurrentMedicalScheme, "Current \r\nOR\r\n Proposed Medical Scheme", new MultiLineEditor(), Width: 210);
                    column.For(c => c.ReplacedMedicalScheme, "Replaced \r\nOR\r\n Proposed Medical Scheme", new MultiLineEditor(), Width: 210);

                },
                 AllowDelete: false, AllowAddNew: false)
                .Format2(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format1);
                this.metroPanel_CAR.Controls.Add(compareMedicalSchemesGrid);
                top += compareMedicalSchemesGrid.Height + 10;

                //7. Initial Recommendation
                tbhInitialRecommendation.Top = top;
                tbhInitialRecommendation.AddDataBinding(bsMedicalAidAdviceRecord, "RecommendedFunds");
                this.metroPanel_CAR.Controls.Add(tbhInitialRecommendation);
                top += tbhInitialRecommendation.Height;

                //8. Motivation for Recommendation
                tbhMotivationForRecommendation.Top = top;
                tbhMotivationForRecommendation.AddDataBinding(bsMedicalAidAdviceRecord, "ImplementedMotivation");
                this.metroPanel_CAR.Controls.Add(tbhMotivationForRecommendation);
                top += tbhMotivationForRecommendation.Height;

                //9. Implementation of Recommendation
                tbhImplentationForRecommendation.Top = top;
                tbhImplentationForRecommendation.AddDataBinding(bsMedicalAidAdviceRecord, "ImplementedProduct");
                this.metroPanel_CAR.Controls.Add(tbhImplentationForRecommendation);
                top += tbhImplentationForRecommendation.Height;

                //10. Reasons of Recommendation
                tbhReasonsForRecommendation.Top = top;
                tbhReasonsForRecommendation.AddDataBinding(bsMedicalAidAdviceRecord, "Motivation");
                this.metroPanel_CAR.Controls.Add(tbhReasonsForRecommendation);
                top += tbhReasonsForRecommendation.Height;


            }
            #endregion

            #region Life/RiskAdviceRecord            
            if (riskAdviceRecords!=null)
            {
                #region History

                historyGrid.Initialise1(riskAdviceRecords, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new DateEditor(true), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);

                },
                 AllowDelete: false, AllowAddNew: false, RowSelectEventHandler: riskAdviceRecords_RowSelectEventHandler)
                .Format1(true, fixedCols: 1, format: MetroControlExt.GridFormats.Format2); ;

                this.metroPanel_Car_History.Controls.Add(historyGrid);

                historyGrid.Selection.SelectRow(riskAdviceRecords.Count, true);
                #endregion

                #region Current Record

                currentRecordGrid.Initialise1((IList<RiskAdviceRecord>)bsRiskAdviceRecord.List, column =>
                {
                    column.For(c => c.AdviceDate, "Advice Date", new StringEditor(true), Width: 200);
                    column.For(c => c.InitialFee, "Initial fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.OngoingFee, "Ongoing fee", new CurrencyEditor(), Width: 100);
                    column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);
                    column.For(c => c.UpdateDate, "Last Update", new DateEditor(true), Width: 115);
                    column.For(c => c.UpdateBy, "Update By", new StringEditor(true), Width: 200);

                },
                 AllowDelete: false, AllowAddNew: false)
                .Format1(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

                this.metroPanel_CAR_header.Controls.Add(currentRecordGrid);

                #endregion

                //bsRiskAdviceRecord.ListChanged += BsClientAdviceRecord_ListChanged;

                //Heading
                lblSubHeading.Top = top;
                lblSubHeading.DataBindings.Add("Text", bsRiskAdviceRecord, "AdviceDate");
                this.metroPanel_CAR.Controls.Add(lblSubHeading);
                top += 30;

                cbhProductKnowledge.Top = top;
                cbhProductKnowledge.AddDataBinding(bsRiskAdviceRecord, "ProductKnowledge");
                this.metroPanel_CAR.Controls.Add(cbhProductKnowledge);
                top += cbhProductKnowledge.Height;

                tbhNeedsAndOjectives.Top = top;
                tbhNeedsAndOjectives.AddDataBinding(bsRiskAdviceRecord, "NeedsAndObjectives");
                this.metroPanel_CAR.Controls.Add(tbhNeedsAndOjectives);
                top += tbhNeedsAndOjectives.Height;

                //5. Financial Situation
                tbhFinancialSituation.Top = top;
                tbhFinancialSituation.AddDataBinding(bsRiskAdviceRecord, "FinancialSituation");
                this.metroPanel_CAR.Controls.Add(tbhFinancialSituation);
                top += tbhFinancialSituation.Height;

                //6. Other Information
                tbhOtherInformation.Top = top;
                tbhOtherInformation.AddDataBinding(bsRiskAdviceRecord, "OtherInformation");
                this.metroPanel_CAR.Controls.Add(tbhOtherInformation);
                top += tbhOtherInformation.Height;

                //Needs and Goals Identified
                this.metroPanel_CAR.Controls.Add(new MetroLabel() { Text = "Needs and Goals Identified", Top = top, Left = left, Width = 660 });
                top += 30;

                riskAdviceRecord.Initialise_Names();

                IList<RiskNeedsAndGoalsTableRow> needs = new List<RiskNeedsAndGoalsTableRow>
                {
                    riskAdviceRecord.LifeInfo,
                    riskAdviceRecord.IncomeProtectionInfo,
                    riskAdviceRecord.LumpSumInfo,
                    riskAdviceRecord.TemporaryDisabilityInfo,
                    riskAdviceRecord.TraumaAndIllnessInfo,
                    riskAdviceRecord.FuneralCoverInfo,
                    riskAdviceRecord.OtherInfo
                };

                SourceGrid.DataGrid needsAndGoalsGrid = new SourceGrid.DataGrid()
                {
                    Top = top,
                    Left = left,
                    Width = 930,
                    Height = 8 * 40 + 10
                };
                needsAndGoalsGrid.Initialise1(needs, column =>
                {
                    column.For(c => c.Name, "Financial Need", new StringEditor(true), Width: 200);
                    column.For(x => x.NeedsQuantifiedAsDouble, "Need Quantified", new CurrencyEditor(), Width: 150);
                    column.For(c => c.NeedsPriority, "Priority of need to be addressed", new ComboListEditor(ListDataItemType.NeedPriority), Width: 100);
                    column.For(c => c.NeedAddressed, "Was the need fully addressed ?", new ComboListEditor(ListDataItemType.NeedAdressed), Width: 100);
                    column.For(c => c.ShortfallAsDouble, "Shortfall", new CurrencyEditor(), Width: 150);
                    column.For(c => c.ReviewDateAsDate, "Review Date\r\n(If the need is to be addressed partially or later).", new DateEditor(), Width: 200);

                },
                 AllowDelete: false, AllowAddNew: false)
                .Format2(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format1);
                this.metroPanel_CAR.Controls.Add(needsAndGoalsGrid);
                top += needsAndGoalsGrid.Height + 10;

                //7. Initial Recommendation
                tbhInitialRecommendation.Top = top;
                tbhInitialRecommendation.AddDataBinding(bsRiskAdviceRecord, "RecommendedFunds");
                this.metroPanel_CAR.Controls.Add(tbhInitialRecommendation);
                top += tbhInitialRecommendation.Height;

                //8. Motivation for Recommendation
                tbhMotivationForRecommendation.Top = top;
                tbhMotivationForRecommendation.AddDataBinding(bsRiskAdviceRecord, "ImplementedMotivation");
                this.metroPanel_CAR.Controls.Add(tbhMotivationForRecommendation);
                top += tbhMotivationForRecommendation.Height;

                //9. Implementation of Recommendation
                tbhImplentationForRecommendation.Top = top;
                tbhImplentationForRecommendation.AddDataBinding(bsRiskAdviceRecord, "ImplementedProduct");
                this.metroPanel_CAR.Controls.Add(tbhImplentationForRecommendation);
                top += tbhImplentationForRecommendation.Height;

                //10. Reasons of Recommendation
                tbhReasonsForRecommendation.Top = top;
                tbhReasonsForRecommendation.AddDataBinding(bsRiskAdviceRecord, "Motivation");
                this.metroPanel_CAR.Controls.Add(tbhReasonsForRecommendation);
                top += tbhReasonsForRecommendation.Height;

            }
            #endregion

        }        
        private void Initialise_NotesPanel(bool readOnly)
        {
            ReadOnly = readOnly;

            left = 10;
            top = 5;

            #region Notes History

            historyNotesGrid.Initialise1(archiveNotes, column =>
            {
                column.For(c => c.NoteDate, "Note Date", new DateEditor(true), Width: 100);
                column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);

            },
             AllowDelete: false, AllowAddNew: false, RowSelectEventHandler: archiveNotes_RowSelectEventHandler)
            .Format1(true, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

            this.metroPanel_Notes_History.Controls.Add(historyNotesGrid);

            historyNotesGrid.Selection.SelectRow(archiveNotes.Count, true);

            #endregion

            #region Current Record

            currentNoteRecordGrid.Initialise1((IList<Note>)bsArchiveNote.List, column =>
            {
                column.For(c => c.NoteDate, "Note Date", new StringEditor(true), Width: 200);
               // column.For(c => c.InitialFee, "Initial fee", new CurrencyEditor(), Width: 100);
               // column.For(c => c.OngoingFee, "Ongoing fee", new CurrencyEditor(), Width: 100);
                column.For(c => c.IsCompleted, "Is Completed ?", Width: 115);
                column.For(c => c.UpdateDate, "Last Update", new DateEditor(true), Width: 115);
                column.For(c => c.UpdateBy, "Update By", new StringEditor(true), Width: 200);

            },
             AllowDelete: false, AllowAddNew: false)
            .Format1(false, fixedCols: 1, format: MetroControlExt.GridFormats.Format2);

            this.metroPanel_Notes_Header.Controls.Add(currentNoteRecordGrid);

            #endregion

            #region Note 

            tbhArchiveNote.Top = top; tbhArchiveNote.Left = left;
            tbhArchiveNote.AddDataBinding(bsArchiveNote, "Text");
            this.metroPanel_Notes.Controls.Add(tbhArchiveNote);
            top += tbhArchiveNote.Height;


            #endregion

        }

        #endregion

        #region Public 
        public Client Client { get; set; }
        public object SelectedItem { get; set; }

        #endregion

        #region Toolstrip Events

        private void Initialise_ToolStrip()
        {

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", Title);

            xToolBarMenu1.tbRefresh.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbRefresh.Text = "Delete";
            xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;

            xToolBarMenu1.tbSave.Visible = !ReadOnly;

            xToolBarMenu1.tbEdit.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbEdit.Text = "Add New";


        }
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Retirement != null)
                {
                    Retirement.AdviceRecords = clientAdviceRecords;
                    Retirement.Notes = archiveNotes;

                    Program.Repository.Update<Retirement, int>(Retirement);
                }
                if (Investment != null)
                {
                    Investment.AdviceRecords = clientAdviceRecords;
                    Investment.Notes = archiveNotes;

                    Program.Repository.Update<Investment, int>(Investment);
                }
                if (Education != null)
                {
                    Education.AdviceRecords = clientAdviceRecords;
                    Education.Notes = archiveNotes;

                    Program.Repository.Update<Education, int>(Education);
                }
                if (Medical != null)
                {
                    Medical.AdviceRecords = medicalAidAdviceRecords;
                    Medical.Notes = archiveNotes;

                    Program.Repository.Update<Medical, int>(Medical);
                }
                if (Life != null)
                {
                    Life.AdviceRecords = riskAdviceRecords;
                    Life.Notes = archiveNotes;

                    Program.Repository.Update<Life, int>(Life);
                }
                if (IncomeAsset != null)
                {
                    IncomeAsset.AdviceRecords = clientAdviceRecords;
                    IncomeAsset.Notes = archiveNotes;

                    Program.Repository.Update<IncomeAsset, int>(IncomeAsset);
                }
                if (RetirementNeed != null)
                {
                    RetirementNeed.AdviceRecords = clientAdviceRecords;
                    RetirementNeed.Notes = archiveNotes;

                    Program.Repository.Update<Need, int>(RetirementNeed);
                }
                if (EducationNeed != null)
                {
                    EducationNeed.AdviceRecords = clientAdviceRecords;
                    EducationNeed.Notes = archiveNotes;

                    Program.Repository.Update<EducationNeed, int>(EducationNeed);
                }
                if (InvestmentNeed != null)
                {
                    InvestmentNeed.AdviceRecords = clientAdviceRecords;
                    InvestmentNeed.Notes = archiveNotes;

                    Program.Repository.Update<InvestmentNeed, int>(InvestmentNeed);
                }
                if (RiskCoverNeed != null)
                {
                    RiskCoverNeed.RiskAdviceRecords = riskAdviceRecords;
                    RiskCoverNeed.Notes = archiveNotes;

                    Program.Repository.Update<RiskCoverNeed, int>(RiskCoverNeed);
                }

                MessageBoxExt.ShowInformation("Save completed successfully");
            }
            catch(Exception x){
                MessageBoxExt.ShowException(x, "Could not save updates");
            }
        }
        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //remove car
                if (this.metroTabControl1.SelectedIndex == 0)
                {
                    //TO DO: only if saved note
                    if (!Program.User.IsAdministrator && clientAdviceRecord.Id!=0)
                    {
                        MessageBoxExt.ShowWarning("You must have Administrator role to remove this Advice record");
                        return;
                    }

                    if (!MessageBoxExt.ShowQuestion("Are you sure you wish to delete this advice record ?"))
                        return;

                    if(clientAdviceRecord!=null)
                    {
                        clientAdviceRecords.Remove(clientAdviceRecord);

                        if (clientAdviceRecords.Count == 0)
                            clientAdviceRecords.Add(new ClientAdviceRecord());

                        int r = clientAdviceRecords.Count;

                        historyGrid.Rebind(clientAdviceRecords);
                        ResetClientAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);
                    }

                    if (medicalAidAdviceRecord != null)
                    {
                        medicalAidAdviceRecords.Remove(medicalAidAdviceRecord);

                        if (medicalAidAdviceRecords.Count == 0)
                            medicalAidAdviceRecords.Add(new MedicalAidAdviceRecord());

                        int r = medicalAidAdviceRecords.Count;

                        historyGrid.Rebind(medicalAidAdviceRecords);
                        ResetMedicalAidAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);

                    }

                    if (riskAdviceRecord != null)
                    {
                        riskAdviceRecords.Remove(riskAdviceRecord);

                        if (riskAdviceRecords.Count == 0)
                            riskAdviceRecords.Add(new RiskAdviceRecord());

                        int r = riskAdviceRecords.Count;

                        historyGrid.Rebind(riskAdviceRecords);
                        ResetRiskAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);

                    }
                }

                //remove note
                if (this.metroTabControl1.SelectedIndex == 1)
                {
                    if (!MessageBoxExt.ShowQuestion("Are you sure you wish to delete this note ?"))
                        return;

                    archiveNotes.Remove(archiveNote);

                    if (archiveNotes.Count == 0)
                        archiveNotes.Add(new Note());

                    int r = archiveNotes.Count;
                    historyNotesGrid.Rebind(archiveNotes);                    
                    ResetNoteRecordBindings(r);
                    historyNotesGrid.Selection.SelectRow(r, true);
                   
                }

                //activate save button
                xToolBarMenu1.SetCAREdit(true);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
            }
        }
        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            try
            {
                //add car
                if (this.metroTabControl1.SelectedIndex == 0)
                {
                    if(clientAdviceRecords!=null)
                    {

                        clientAdviceRecords.Add(new ClientAdviceRecord());

                        int r = clientAdviceRecords.Count;

                        historyGrid.Rebind(clientAdviceRecords);
                        ResetClientAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);
                    }

                    if (medicalAidAdviceRecords != null)
                    {
                        medicalAidAdviceRecords.Add(new MedicalAidAdviceRecord());

                        int r = medicalAidAdviceRecords.Count;

                        historyGrid.Rebind(medicalAidAdviceRecords);
                        ResetMedicalAidAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);

                    }

                    if (riskAdviceRecords != null)
                    {
                        riskAdviceRecords.Add(new RiskAdviceRecord());

                        int r = riskAdviceRecords.Count;

                        historyGrid.Rebind(riskAdviceRecords);
                        ResetRiskAdviceRecordBindings(r);
                        historyGrid.Selection.SelectRow(r, true);

                    }
                }

                //add note
                if (this.metroTabControl1.SelectedIndex == 1)
                {
                    archiveNotes.Add(new Note());

                    int r = archiveNotes.Count;
                    historyNotesGrid.Rebind(archiveNotes);
                    ResetNoteRecordBindings(r);
                    historyNotesGrid.Selection.SelectRow(r, true);

                }

                //activate save button
                xToolBarMenu1.SetCAREditBeforeSave(true);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                
            }
        }
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
