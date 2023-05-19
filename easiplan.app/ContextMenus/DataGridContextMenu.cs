using easiplan.app.Models;
using easiplan.domain.Entities;
using easiplan.domain.Views;
using Finx.App;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.Forms;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easiplan.app.ContextMenus
{
    /// <summary>
    /// Class to generate context menu's for SourceGrid data grids
    /// </summary>
    public class DataGridContextMenu : SourceGrid.Cells.Controllers.ControllerBase
    {
        #region Local Variables
        ContextMenuStrip _menu = new ContextMenuStrip();
        ContextMenuType _contextMenuType;
        Form _parent;
        Client _client = null;

        frmMetroPolicyFunds frmPolicyFunds = null;
        frmMetroPolicyNotes frmPolicyNotes = null;
        frmMetroClientAdviceRecord frmClientAdviceRecord = null;

        EventHandler _OnCompleted;

        object _selectedItem;
        bool _readOnly = true;
        bool _visible = true;

        public bool ReadOnly { get { return _readOnly; } }
        public bool Visible { get { return _visible; } }

        int _mouseX = 40;
        int _mouseY = 200;
        
        #endregion

        #region Public Variables
        public ContextMenuStrip ContextMenu { get { return _menu; } }

        #endregion

        #region Constructor
        public DataGridContextMenu(Form parent, ContextMenuType contextMenuType, bool readOnly = true, Client client = null, bool visible = true, EventHandler OnCompleted = null)
        {
            _parent = parent;
            _contextMenuType = contextMenuType;
            _readOnly = readOnly;
            _visible = visible;

            _client = client;

            _OnCompleted = OnCompleted;

           

            switch (contextMenuType)
            {
                case ContextMenuType.RetirementPortfolio:                    
                case ContextMenuType.InvestmentPortfolio:                    
                case ContextMenuType.EducationPortfolio:
                case ContextMenuType.MedicalPortfolio:
                case ContextMenuType.LifePortfolio:
                    
                    _menu.AddMenuItem("Amend Policy", new EventHandler(AmendPortfolio_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Policy History", new EventHandler(PolicyHistory_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Policy Notes", new EventHandler(PolicyNotes_Click)).Enabled = !ReadOnly; 
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Client Advice Record", new EventHandler(ClientAdviceRecord_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Cancel Policy", new EventHandler(RemovePortfolio_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                    if (contextMenuType == ContextMenuType.RetirementPortfolio)
                    {
                        _menu.AddMenuItem("-");
                        _menu.AddMenuItem("Move to Non-Retirement", new EventHandler(MovePolicy_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                        
                    };
                    if (contextMenuType == ContextMenuType.InvestmentPortfolio)
                    {
                        _menu.AddMenuItem("-");
                        _menu.AddMenuItem("Move to Retirement", new EventHandler(MovePolicy_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                        
                    }

                    break;
                case ContextMenuType.AssetPortfolio:
                    _menu.AddMenuItem("Notes", new EventHandler(PolicyNotes_Click)).Enabled = !ReadOnly; ;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Client Advice Record", new EventHandler(ClientAdviceRecord_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Remove", new EventHandler(RemovePortfolio_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                    break;
                case ContextMenuType.RetirementFna:
                case ContextMenuType.EducationFna:
                case ContextMenuType.InvestmentFna:
                case ContextMenuType.RiskCoverFna:
                    _menu.AddMenuItem("Accept Advice", new EventHandler(UpdateFna_Click)).Enabled = !ReadOnly && (Program.User.IsAdministrator || Program.User.IsAdvisor);
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Advice Notes", new EventHandler(PolicyNotes_Click)).Enabled = !ReadOnly; ;
                    //_menu.MenuItems.Add("-");
                    //_menu.MenuItems.Add("Remove Advice", new EventHandler(RemoveFna_Click)).Enabled = !ReadOnly;
                    break;
                case ContextMenuType.ClientInstruction:
                    _menu.AddMenuItem("Open Task", new EventHandler(UpdatePortfolio_Click)).Enabled = !ReadOnly;
                    break;
                case ContextMenuType.ClientTask:
                    _menu.AddMenuItem("Open Client", new EventHandler(OpenClientForm_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Client Rating", new EventHandler(OpenClientRating_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Send Email/Sms", new EventHandler(SendEmailSmsForm_Click)).Enabled = true;
                    break;
                case ContextMenuType.ClientComms:
                    _menu.AddMenuItem("Send Email", new EventHandler(SendEmailForm_Click)).Enabled = true;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Send SMS", new EventHandler(SendSMSForm_Click)).Enabled = true;
                    break;
                case ContextMenuType.MeetingSync:
                    _menu.AddMenuItem("Sync Outlook365", new EventHandler(SyncClientMeeting_Click));
                    break;

                case ContextMenuType.AdminTask:
                    _menu.AddMenuItem("Open Task", new EventHandler(UpdatePortfolio_Click)).Enabled = !ReadOnly;
                    _menu.AddMenuItem("-");
                    _menu.AddMenuItem("Open Client", new EventHandler(OpenClientForm_Click)).Enabled = !ReadOnly;
                    break;

            }

        }
        #endregion

        #region ContextMenu Events
        private object GetSourceGridSelectedItem(object sender, EventArgs e)
        {
            try
            {
                SourceGrid.DataGrid dg = sender as SourceGrid.DataGrid;
                if (dg != null)
                {
                    int dataIndex = dg.Selection.ActivePosition.Row - 1;

                    if (dataIndex < dg.DataSource.Count)
                        return dg.DataSource[dataIndex];
                }
            }
            catch (Exception x)
            {

            }

            return null;
        }

        /// <summary>
        /// Show the Context Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnClick(CellContext sender, EventArgs e)
        {
            _selectedItem = null;
           
            try
            {
                if (sender.Position.Row > 0 && sender.Position.Row <= sender.Grid.Rows.LastVisibleScrollableRow)
                {
                    sender.Grid.Selection.FocusRow(sender.Position.Row);

                    _menu.Show(sender.Grid, new Point(_mouseX, _mouseY));

                }
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }

            base.OnClick(sender, e);
        }
        public override void OnMouseDown(CellContext sender, MouseEventArgs e)
        {
            //set the mouse co-ordinates for the context menu
            _mouseX = e.X;
            _mouseY = e.Y;

            base.OnMouseDown(sender, e);
        }

        private void UpdateFna_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                switch (_contextMenuType)
                {

                    case ContextMenuType.RetirementFna:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Need, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.UpdateFna, _readOnly, _client);
                        break;
                    case ContextMenuType.EducationFna:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as EducationNeed, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.UpdateFna, _readOnly, _client);
                        break;
                    case ContextMenuType.InvestmentFna:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as InvestmentNeed, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.UpdateFna, _readOnly, _client);
                        break;
                    case ContextMenuType.RiskCoverFna:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as RiskCoverNeed, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.UpdateRiskFna, _readOnly, _client);
                        break;
                    default:
                        throw new Exception("Invalid or Unknow ContextMenuType");

                };

                using (new AppWaitCursor(sender))
                {
                    frmPolicyFunds.ShowDialog(_parent);
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }
        private void RemoveFna_Click(object sender, EventArgs e)
        {
            _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

            if (_selectedItem == null)
                return;

            //Check if allowed to remove policy
            var id = _selectedItem.GetPropertyValue("Id");
            if (id.ToInt() > 0)
            {
                if (!Program.User.IsAdministrator)
                {
                    MessageBoxExt.ShowWarning("You must have Administrator role to remove this Policy");
                    return;
                }
            }
            else
            {
                if (Program.User.IsClerk || Program.User.IsAdministrator)
                {
                }
                else
                {

                    MessageBoxExt.ShowWarning("You must have Clerk or Administrator role to remove this Policy");
                    return;
                }
            }

            //Confirm
            if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Remove this Policy ?"))
                return;

            using (new AppWaitCursor(sender))
            {
                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementFna:
                        Need need = _selectedItem as Need;
                        if (need != null)
                        {
                            _client.ClientFna.Needs.Remove(need);
                            //remove any instruction linked to thes need
                            var instruction = _client.ClientInstructions.Instructions.Where(x => x.ReferenceId == need.Id).FirstOrDefault();
                            if (instruction != null)
                                _client.ClientInstructions.Instructions.Remove(instruction);

                            Program.Repository.Update<ClientFna, int>(_client.ClientFna);
                        }
                        break;
                    case ContextMenuType.EducationFna:
                        EducationNeed needEducation = _selectedItem as EducationNeed;
                        if (needEducation != null)
                        {
                            _client.ClientFnaEducation.EducationNeeds.Remove(needEducation);
                            //remove any instruction linked to thes need
                            var instruction = _client.ClientInstructions.Instructions.Where(x => x.ReferenceId == needEducation.Id).FirstOrDefault();
                            if (instruction != null)
                                _client.ClientInstructions.Instructions.Remove(instruction);

                            Program.Repository.Update<ClientFnaEducation, int>(_client.ClientFnaEducation);
                        }
                        break;
                    case ContextMenuType.InvestmentFna:
                        InvestmentNeed needInvestment = _selectedItem as InvestmentNeed;
                        if (needInvestment != null)
                        {
                            _client.ClientFnaInvestment.Needs.Remove(needInvestment);
                            //remove any instruction linked to thes need
                            var instruction = _client.ClientInstructions.Instructions.Where(x => x.ReferenceId == needInvestment.Id).FirstOrDefault();
                            if (instruction != null)
                                _client.ClientInstructions.Instructions.Remove(instruction);

                            Program.Repository.Update<ClientFnaInvestment, int>(_client.ClientFnaInvestment);
                        }
                        break;

                }
            }

        }
        private void UpdatePortfolio_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Retirement, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Investment, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.EducationPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Education, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Medical, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.LifePortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Life, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.AssetPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as IncomeAsset, ReadOnly ? PolicyAction.ViewFunds : PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.ClientInstruction:
                    case ContextMenuType.AdminTask:

                        Instruction Instruction = _selectedItem as Instruction;

                        //FIX: workaround for unknown instruction types
                        //YJ 2021-12-13 This fix breaks custom tasks
                        //if (Instruction.InstructionType == InstructionType.UNKNOWN)
                        //    Instruction.InstructionType = InstructionTypeExt.ToType(Instruction.Type);

                        switch (Instruction.InstructionType)
                        {
                            case InstructionType.POLICY_UPDATE:
                            case InstructionType.POLICY_CANCEL:
                            case InstructionType.POLICY_CREATE:
                            case InstructionType.POLICY_REVIEW:
                                frmPolicyFunds = new frmMetroPolicyFunds(Instruction, PolicyAction.UpdateInstruction, _readOnly, _client);
                                break;

                            case InstructionType.ASSET_POLICY_NOTE:
                            case InstructionType.EDU_POLICY_NOTE:
                            case InstructionType.INVEST_POLICY_NOTE:
                            case InstructionType.LIFE_POLICY_NOTE:
                            case InstructionType.MEDICAL_POLICY_NOTE:
                            case InstructionType.RETIRE_POLICY_NOTE:
                                PolicyNotes_Click(Instruction, new EventArgs());
                                return;
                            case InstructionType.CUSTOMTASK:
                            case InstructionType.UNKNOWN:
                                frmMetroAdminTaskAdd frmTask = new frmMetroAdminTaskAdd(Instruction);
                                frmTask.ShowDialog(_parent);
                                return;
                            default:
                                throw new MyValidationException("This task could not be found and may no longer exist.");
                        }
                        break;
                    default:
                        return;
                }

                using (new AppWaitCursor(sender))
                {
                    frmPolicyFunds.ShowDialog(_parent);
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }
        private void AmendPortfolio_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Retirement, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Investment, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.EducationPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Education, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Medical, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.LifePortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Life, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    case ContextMenuType.AssetPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as IncomeAsset, PolicyAction.AmendPolicy, _readOnly, _client);
                        break;
                    default:
                        return;
                }

                using (new AppWaitCursor(sender))
                {
                    frmPolicyFunds.ShowDialog(_parent);
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);

            }
        }
        private void RemovePortfolio_Click(object sender, EventArgs e)
        {
            _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

            if (_selectedItem == null)
                return;

            //Check if allowed to remove policy
            var id = _selectedItem.GetPropertyValue("Id");
            if (id.ToInt() > 0)
            {
                if (!Program.User.IsAdministrator)
                {
                    MessageBoxExt.ShowWarning("You must have Administrator role to remove this Policy");
                    return;
                }
            }
            else
            {
                if (Program.User.IsClerk || Program.User.IsAdministrator)
                {
                }
                else
                {

                    MessageBoxExt.ShowWarning("You must have Clerk or Administrator role to remove this Policy");
                    return;
                }
            }

            //Confirm
            if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Remove this Policy ?"))
                return;

            using (new AppWaitCursor(sender))
            {
                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        Retirement Retirement = _selectedItem as Retirement;
                        if (Retirement != null)
                        {
                            Retirement.Status = NeedStatus.Cancelled.ToText();

                            if (Retirement.Id > 0)
                                Program.Repository.Update<Retirement, int>(Retirement);
                            else
                                _client.ClientPortfolio.Retirements.Remove(Retirement);
                        }
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        Investment Investment = _selectedItem as Investment;
                        if (Investment != null)
                        {
                            Investment.Status = NeedStatus.Cancelled.ToText();

                            if (Investment.Id > 0)
                                Program.Repository.Update<Investment, int>(Investment);
                            else
                                _client.ClientPortfolio.Investments.Remove(Investment);
                        }
                        break;
                    case ContextMenuType.EducationPortfolio:
                        Education Education = _selectedItem as Education;
                        if (Education != null)
                        {
                            Education.Status = NeedStatus.Cancelled.ToText();

                            if (Education.Id > 0)
                                Program.Repository.Update<Education, int>(Education);
                            else
                                _client.ClientPortfolio.Educations.Remove(Education);

                        }
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        Medical Medical = _selectedItem as Medical;
                        if (Medical != null)
                        {
                            Medical.Status = NeedStatus.Cancelled.ToText();

                            if (Medical.Id > 0)
                                Program.Repository.Update<Medical, int>(Medical);
                            else
                                _client.ClientPortfolio.Medicals.Remove(Medical);

                        }
                        break;
                    case ContextMenuType.LifePortfolio:
                        Life Life = _selectedItem as Life;
                        if (Life != null)
                        {
                            Life.Status = NeedStatus.Cancelled.ToText();

                            if (Life.Id > 0)
                                Program.Repository.Update<Life, int>(Life);
                            else
                                _client.ClientPortfolio.Lifes.Remove(Life);

                        }
                        break;
                    case ContextMenuType.AssetPortfolio:
                        IncomeAsset Asset = _selectedItem as IncomeAsset;
                        if (Asset != null)
                        {
                            Asset.Status = NeedStatus.Cancelled.ToText();

                            if (Asset.Id > 0)
                                Program.Repository.Update<IncomeAsset, int>(Asset);
                            else
                                _client.ClientPortfolio.IncomeAssets.Remove(Asset);

                        }
                        break;

                };

                _OnCompleted?.Invoke(_selectedItem, e);

            }

        }
        private void PolicyHistory_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Retirement, PolicyAction.PolicyHistory, true, _client);
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Investment, PolicyAction.PolicyHistory, true, _client);
                        break;
                    case ContextMenuType.EducationPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Education, PolicyAction.PolicyHistory, true, _client);
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Medical, PolicyAction.PolicyHistory, true, _client);
                        break;
                    case ContextMenuType.LifePortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as Life, PolicyAction.PolicyHistory, true, _client);
                        break;
                    case ContextMenuType.AssetPortfolio:
                        frmPolicyFunds = new frmMetroPolicyFunds(_selectedItem as IncomeAsset, PolicyAction.PolicyHistory, true, _client);
                        break;
                    default:
                        return;
                }

                using (new AppWaitCursor(sender))
                {
                    frmPolicyFunds.ShowDialog(_parent);
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }
        private void PolicyNotes_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                if (frmPolicyNotes != null) {

                    if (_selectedItem == frmPolicyNotes.SelectedItem && !frmPolicyNotes.IsDisposed)
                    {
                        frmPolicyNotes.BringToFront();
                        return;
                    }
                    else
                        frmPolicyNotes.Close();
                }
                   

                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Retirement, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Investment, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.EducationPortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Education, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Medical, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.LifePortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Life, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.AssetPortfolio:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as IncomeAsset, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.RetirementFna:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Need, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.EducationFna:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as EducationNeed, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.InvestmentFna:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as InvestmentNeed, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.ClientInstruction:
                    case ContextMenuType.AdminTask:
                        frmPolicyNotes = new frmMetroPolicyNotes(_selectedItem as Instruction, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    default:
                        return;
                }

                frmPolicyNotes.Client = this._client;
                frmPolicyNotes.SelectedItem = _selectedItem;

                using (new AppWaitCursor(sender))
                {
                    frmPolicyNotes.Show();
                }

            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }


        //Open client advice record

        private void ClientAdviceRecord_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                if (frmClientAdviceRecord != null)
                {

                    if (_selectedItem == frmClientAdviceRecord.SelectedItem && !frmClientAdviceRecord.IsDisposed)
                    {
                        frmClientAdviceRecord.BringToFront();
                        return;
                    }
                    else
                        frmClientAdviceRecord.Close();
                }


                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Retirement, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    /*case ContextMenuType.InvestmentPortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Investment, ReadOnly, PolicyAction.AmendPolicy);
                        break;*/
                    case ContextMenuType.EducationPortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Education, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.MedicalPortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Medical, ReadOnly, PolicyAction.AmendPolicy);
                        break;/*
                    case ContextMenuType.LifePortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Life, ReadOnly, PolicyAction.AmendPolicy);
                        break;*/
                    case ContextMenuType.AssetPortfolio:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as IncomeAsset, ReadOnly, PolicyAction.AmendPolicy);
                        break;/*
                    case ContextMenuType.RetirementFna:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Need, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.EducationFna:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as EducationNeed, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.InvestmentFna:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as InvestmentNeed, ReadOnly, PolicyAction.AmendPolicy);
                        break;
                    case ContextMenuType.ClientInstruction:
                    case ContextMenuType.AdminTask:
                        frmClientAdviceRecord = new frmMetroClientAdviceRecord(_selectedItem as Instruction, ReadOnly, PolicyAction.AmendPolicy);
                        break;*/
                    default:
                        return;
                }

                frmClientAdviceRecord.Client = this._client;
                frmClientAdviceRecord.SelectedItem = _selectedItem;

                using (new AppWaitCursor(sender))
                {
                    frmClientAdviceRecord.WindowState = FormWindowState.Maximized;
                    frmClientAdviceRecord.Show();
                }

            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }




        private void OpenClientForm_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                using (new AppWaitCursor(sender))
                {
                    switch (_contextMenuType)
                    {
                        case ContextMenuType.AdminTask:

                            Instruction instruction = _selectedItem as Instruction;

                            if (instruction.ClientId == 0)
                            {
                                frmMetroAdminTaskAdd frm = new frmMetroAdminTaskAdd(instruction);
                                frm.ShowDialog();
                            }
                            else
                            {
                                string FormText = string.Format("Client No: {0}", instruction.ClientId);
                                Form mdiForm = Application.OpenForms["metroMdiMain"];

                                Form cForm = mdiForm.MdiChildren.Where(x => x.Text == FormText).FirstOrDefault();

                                if (cForm == null)
                                {

                                    frmMetroClient1 childForm = new frmMetroClient1(instruction.ClientId, 8);

                                    childForm.MdiParent = mdiForm;
                                    childForm.Text = FormText;
                                    childForm.Show();
                                }
                                else
                                {
                                    cForm.BringToFront();
                                    //cForm.WindowState = FormWindowState.Maximized;
                                }
                            }
                            break;
                        case ContextMenuType.ClientTask:

                            ClientDetailsView clientDetails = _selectedItem as ClientDetailsView;

                            string FormText1 = string.Format("Client No: {0}", clientDetails.ClientId);
                            Form mdiForm1 = Application.OpenForms["metroMdiMain"];

                            Form cForm1 = mdiForm1.MdiChildren.Where(x => x.Text == FormText1).FirstOrDefault();

                            if (cForm1 == null)
                            {

                                frmMetroClient1 childForm = new frmMetroClient1(clientDetails.ClientId, 0);

                                childForm.MdiParent = mdiForm1;
                                childForm.Text = FormText1;
                                childForm.Show();
                                
                            }
                            else
                            {
                                cForm1.BringToFront();
                                //cForm.WindowState = FormWindowState.Maximized;
                            }
                            break;

                        default:
                            return;
                    }
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }

        private void OpenClientRating_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                using (new AppWaitCursor(sender))
                {
                    switch (_contextMenuType)
                    {

                        case ContextMenuType.ClientTask:

                            ClientDetailsView clientDetailsView = _selectedItem as ClientDetailsView;

                            try
                            {
                                if (clientDetailsView.ClientId > 0)
                                {
                                    frmMetroClientSegmentation frm = new frmMetroClientSegmentation(clientDetailsView.ClientId);
                                    frm.ShowDialog(_parent);
                                }
                                else
                                {
                                    throw new Exception("Invalid client...please select a valid client");
                                }
                            }
                            catch (Exception x)
                            {
                                MessageBoxExt.ShowException(x, "Not a valid client");
                            };
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }

        private void SendEmailForm_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                using (new AppWaitCursor(sender))
                {
                    switch (_contextMenuType)
                    {

                        case ContextMenuType.ClientComms:

                            //ClientDetailsView clientDetails = _selectedItem as ClientDetailsView;

                            try
                            {
                                List<ClientDetailsView> clientDetailsView = Program.ClientDetailsService.ListView(x => x.ClientId == _client.Id).ToList();

                                if (clientDetailsView != null)
                                {
                                    var frm = new frmClientCommunication(clientDetailsView, CommunicationAction.SendEmail);
                                    frm.ShowDialog(_parent);
                                }
                                else
                                {
                                    throw new Exception("Invalid client...please select a valid client");
                                }
                            }
                            catch (Exception x)
                            {
                                MessageBoxExt.ShowException(x, "Not a valid client");
                            };
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }
        private void SendSMSForm_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                using (new AppWaitCursor(sender))
                {
                    switch (_contextMenuType)
                    {

                        case ContextMenuType.ClientComms:
                            //ClientDetailsView clientDetails = _selectedItem as ClientDetailsView; 

                            try
                            {
                                List<ClientDetailsView> clientDetailsView = Program.ClientDetailsService.ListView(x => x.ClientId == _client.Id).ToList();

                                if (clientDetailsView != null)
                                {
                                    var frm = new frmClientCommunication(clientDetailsView, CommunicationAction.SendSMS);
                                    frm.ShowDialog(_parent);
                                }
                                else
                                {
                                    throw new Exception("Invalid client...please select a valid client");
                                }
                            }
                            catch (Exception x)
                            {
                                MessageBoxExt.ShowException(x, "Not a valid client");
                            };
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }
        private void SendEmailSmsForm_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                using (new AppWaitCursor(sender))
                {
                    switch (_contextMenuType)
                    {

                        case ContextMenuType.ClientTask:

                            ClientDetailsView clientDetailsView = _selectedItem as ClientDetailsView;

                            try
                            {
                                if (clientDetailsView != null)
                                {
                                    List<ClientDetailsView> listClientDetailsView = new List<ClientDetailsView>();
                                    listClientDetailsView.Add(clientDetailsView);

                                    var frm = new frmClientCommunication(listClientDetailsView, CommunicationAction.SendAll);
                                    frm.ShowDialog(_parent);
                                }
                                else
                                {
                                    throw new Exception("Invalid client...please select a valid client");
                                }
                            }
                            catch (Exception x)
                            {
                                MessageBoxExt.ShowException(x, "Not a valid client");
                            };
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);
            }
        }

        private void MovePolicy_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

                if (_selectedItem == null)
                    return;

                switch (_contextMenuType)
                {
                    case ContextMenuType.RetirementPortfolio:
                        try{

                            //Confirm
                            if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Move this Policy to Non-Retirement ?"))
                                return;

                            Retirement item = _selectedItem as Retirement;
                            if(item!=null){
                                _client.ClientPortfolio.RetirementsBindingList.Remove(item);
                                _client.ClientPortfolio.InvestmentsBindingList.Add(item.CloneAsInvestment());
                                
                            }
                            
                        }catch(Exception x){
                          //TODO Rollback move
                        }
                        break;
                    case ContextMenuType.InvestmentPortfolio:
                        try
                        {

                            //Confirm
                            if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Move this Policy to Retirement ?"))
                                return;

                            Investment item = _selectedItem as Investment;
                            if (item != null)
                            {
                                _client.ClientPortfolio.InvestmentsBindingList.Remove(item);
                                _client.ClientPortfolio.RetirementsBindingList.Add(item.CloneAsRetirement());

                            }

                        }
                        catch (Exception x)
                        {
                            //TODO Rollback move
                        }
                        break;
                    case ContextMenuType.EducationPortfolio:
                         break;
                    case ContextMenuType.MedicalPortfolio:
                         break;
                    case ContextMenuType.LifePortfolio:
                         break;
                    case ContextMenuType.AssetPortfolio:
                         break;
                    default:
                        return;
                }

            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                _OnCompleted?.Invoke(_selectedItem, e);

            }
        }
        #endregion

        //To Be deleted
        #region Get Need Events

        //private Need GetNeed_EventHandler(object sender, EventArgs args)
        //{
        //    if (_client == null)
        //        return null;

        //    Need need = null;

        //    try
        //    {
        //        int id = (int)sender;
        //        need = Program.Repository.Get<Need, int>(id);
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //    finally
        //    {
        //        //Fix for where the need is implemented, but the ReferenceId has not been reset
        //        if (need != null)
        //        {

        //            need.CurrentAge = _client.ClientDetails.Age;

        //        }
        //    }
        //    return need;
        //}
        //private Need GetFnaNeed_EventHandler(object sender, EventArgs args)
        //{
        //    if (_client == null)
        //        return null;

        //    Need need = null;

        //    try
        //    {
        //        if (sender is Need fnaNeed)
        //        {
        //            need = _client.ClientFna.Needs.Where(x => x.Id == fnaNeed.ReferenceId).FirstOrDefault();
        //        }

        //        if (sender is InvestmentNeed investmentNeed)
        //        {
        //            need = _client.ClientFnaInvestment.Needs.Where(x => x.Id == investmentNeed.ReferenceId).FirstOrDefault();
        //        }

        //        if (sender is EducationNeed educationNeed)
        //        {
        //             need = _client.ClientFnaEducation.EducationNeeds.Where(x => x.Id == educationNeed.ReferenceId).FirstOrDefault();
        //        }

        //        if (sender is Instruction instruction)
        //        {
        //            need = _client.ClientFna.Needs.Where(x => x.Id == instruction.ReferenceId).FirstOrDefault();
        //            if(need==null)
        //                need = _client.ClientFnaInvestment.Needs.Where(x => x.Id == instruction.ReferenceId).FirstOrDefault();
        //                if (need == null)
        //                    need = _client.ClientFnaEducation.EducationNeeds.Where(x => x.Id == instruction.ReferenceId).FirstOrDefault();

        //        }
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //    return need;
        //}
        //private Need GetPolicyNeed_EventHandler(object sender, EventArgs args)
        //{
        //    Need need = null;
        //    try
        //    {

        //        if (sender is Retirement retirement)
        //        {
        //            need = Program.Repository.Get<Need, int>(retirement.ReferenceId);
        //        }

        //        if (sender is Investment investment)
        //        {
        //            //need = investment.Amendments.Where(x => x.Id == investment.ReferenceId).FirstOrDefault();
        //            //if (need == null)
        //            //    need = investment.Amendments.Where(x => x.Status != NeedStatus.Implemented.ToText() && x.Status != NeedStatus.Cancelled.ToText()).FirstOrDefault();

        //            need = Program.Repository.Get<Need, int>(investment.ReferenceId);
        //        }

        //        if (sender is Education education)
        //        {
        //            //need = education.Amendments.Where(x => x.Id == education.ReferenceId).FirstOrDefault();
        //            //if (need == null)
        //            //    need = education.Amendments.Where(x => x.Status != NeedStatus.Implemented.ToText() && x.Status != NeedStatus.Cancelled.ToText()).FirstOrDefault();

        //            need = Program.Repository.Get<Need, int>(education.ReferenceId);
        //        }

        //        if (sender is Medical medical)
        //        {
        //            //need = medical.Amendments.Where(x => x.Id == medical.ReferenceId).FirstOrDefault();
        //            //if (need == null)
        //            //    need = medical.Amendments.Where(x => x.Status != NeedStatus.Implemented.ToText() && x.Status != NeedStatus.Cancelled.ToText()).FirstOrDefault();
        //            need = Program.Repository.Get<Need, int>(medical.ReferenceId);
        //        }

        //        if (sender is Life life)
        //        {
        //            //need = life.Amendments.Where(x => x.Id == life.ReferenceId).FirstOrDefault();
        //            //if (need == null)
        //            //    need = life.Amendments.Where(x => x.Status != NeedStatus.Implemented.ToText() && x.Status != NeedStatus.Cancelled.ToText()).FirstOrDefault();

        //            need = Program.Repository.Get<Need, int>(life.ReferenceId);
        //        }

        //        if (sender is IncomeAsset asset)
        //        {
        //            //need = asset.Amendments.Where(x => x.Id == asset.ReferenceId).FirstOrDefault();
        //            //if (need == null)
        //            //    need = asset.Amendments.Where(x => x.Status != NeedStatus.Implemented.ToText() && x.Status != NeedStatus.Cancelled.ToText()).FirstOrDefault();

        //            need = Program.Repository.Get<Need, int>(asset.ReferenceId);
        //        }

        //        if (sender is Instruction instruction)
        //        {
        //            need = Program.Repository.Get<Need, int>(instruction.ReferenceId);
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //    finally
        //    {
        //        //Fix for where the need is implemented, but the ReferenceId has not been reset
        //        if (need != null)
        //        {
        //            need.CurrentAge = _client.ClientDetails.Age;

        //            need.Initialise();

        //            //if (need.IsImplemented || need.Status == NeedStatus.Implemented.ToText())
        //            //{ need = null; }

        //        }
        //    }

        //    return need;
        //}

        #endregion

        #region Update Need Events
        //private void UpdateNeed_EventHandler(object sender, EventArgs e)
        //{
        //    if (sender is Need need)
        //    {
        //        if (need.Id == 0)
        //        {
        //            Program.Repository.Add<Need, int>(need);
        //        }
        //        else
        //        {
        //            Program.Repository.Update<Need, int>(need);
        //        }


        //    }

        //}
        //private void UpdateFnaNeed_EventHandler(object sender, EventArgs e)
        //{
        //    if (_client == null)
        //        return;

        //    if (sender is InvestmentNeed investmentNeed)
        //    {
        //        investmentNeed.UpdateDate = DateTime.Now;
        //        investmentNeed.UpdateBy = Program.User.Username;

        //        //_client.ClientFnaInvestment.Calculate();
        //        Program.Repository.Update<ClientFnaInvestment, int>(_client.ClientFnaInvestment);

        //        return;
        //    }

        //    if (sender is EducationNeed educationNeed)
        //    {
        //        educationNeed.UpdateDate = DateTime.Now;
        //        educationNeed.UpdateBy = Program.User.Username;

        //        //_client.ClientFnaEducation.Calculate();
        //        Program.Repository.Update<ClientFnaEducation, int>(_client.ClientFnaEducation);

        //        return;
        //    }

        //    if (sender is Need need)
        //    {
        //        need.UpdateDate = DateTime.Now;
        //        need.UpdateBy = Program.User.Username;

        //       // _client.ClientFna.Calculate();
        //        Program.Repository.Update<ClientFna, int>(_client.ClientFna);

        //        return;
        //    }

        //}
        //private void UpdatePolicyNeed_EventHandler(object sender, Need need, EventArgs e)
        //{
        //    try
        //    {
        //        if (need.Id == 0)
        //        {
        //            Program.Repository.Add<Need, int>(need);
        //        }
        //        else
        //        {
        //            Program.Repository.Update<Need, int>(need);
        //        }

        //        if (sender is Retirement retirement)
        //        {
        //            //if (need.Id == 0)
        //            //{
        //            //    retirement.Amendments.Add(need);
        //            //    Program.Repository.Update<Retirement, int>(retirement);
        //            //    //Update the reference Id with the new Need.Id
        //            //    retirement.ReferenceId = need.Id;
        //            //}
        //            retirement.ReferenceId = need.Id;
        //            Program.Repository.Update<Retirement, int>(retirement);
        //            return;
        //        }

        //        if (sender is Investment investment)
        //        {
        //            //if (need.Id == 0)
        //            //{
        //            //    investment.Amendments.Add(need);
        //            //    Program.Repository.Update<Investment, int>(investment);
        //            //    //Update the reference Id with the new Need.Id
        //            //    investment.ReferenceId = need.Id;
        //            //}
        //            investment.ReferenceId = need.Id;
        //            Program.Repository.Update<Investment, int>(investment);
        //            return;
        //        }

        //        if (sender is Education education)
        //        {
        //        //    if (need.Id == 0)
        //        //    {
        //        //        education.Amendments.Add(need);
        //        //        Program.Repository.Update<Education, int>(education);
        //        //        //Update the reference Id with the new Need.Id
        //        //        education.ReferenceId = need.Id;
        //        //    }
        //            education.ReferenceId = need.Id;
        //            Program.Repository.Update<Education, int>(education);
        //            return;
        //        }

        //        if (sender is Medical medical)
        //        {
        //            //if (need.Id == 0)
        //            //{
        //            //    medical.Amendments.Add(need);
        //            //    Program.Repository.Update<Medical, int>(medical);
        //            //    //Update the reference Id with the new Need.Id
        //            //    medical.ReferenceId = need.Id;
        //            //}
        //            medical.ReferenceId = need.Id;
        //            Program.Repository.Update<Medical, int>(medical);
        //            return;

        //        }

        //        if (sender is Life life)
        //        {
        //            //if (need.Id == 0)
        //            //{
        //            //    life.Amendments.Add(need);
        //            //    Program.Repository.Update<Life, int>(life);
        //            //    //Update the reference Id with the new Need.Id
        //            //    life.ReferenceId = need.Id;
        //            //}

        //            life.ReferenceId = need.Id;
        //            Program.Repository.Update<Life, int>(life);
        //            return;
        //        }

        //        if (sender is IncomeAsset asset)
        //        {
        //            //if (need.Id == 0)
        //            //{
        //            //    asset.Amendments.Add(need);
        //            //    Program.Repository.Update<IncomeAsset, int>(asset);
        //            //    //Update the reference Id with the new Need.Id
        //            //    asset.ReferenceId = need.Id;
        //            //}

        //            asset.ReferenceId = need.Id;
        //            Program.Repository.Update<IncomeAsset, int>(asset);
        //            return;
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //}
        //private void UpdatePolicy_EventHandler(object sender, EventArgs e)
        //{
        //    if (sender is Retirement retirement)
        //    {
        //        Program.Repository.Update<Retirement, int>(retirement);
        //    }

        //    if (sender is Investment investment)
        //    {
        //        Program.Repository.Update<Investment, int>(investment);
        //    }

        //    if (sender is Education education)
        //    {
        //        Program.Repository.Update<Education, int>(education);
        //    }

        //    if (sender is Medical medical)
        //    {
        //        Program.Repository.Update<Medical, int>(medical);
        //     }

        //    if (sender is Life life)
        //    {
        //        Program.Repository.Update<Life, int>(life);
        //    }

        //    if (sender is IncomeAsset asset)
        //    {
        //        Program.Repository.Update<IncomeAsset, int>(asset);
        //    }

        //    //Refresh the client
        //    _client = Program.Repository.Get<Client, int>(_client.Id);
        //}
        //private void UpdateClientPortfoilio_EventHandler(object sender, EventArgs e)
        //{
        //    if (_client == null)
        //        return;

        //    ////Retirement
        //    if (sender is Retirement Retirement)
        //    {
        //        var _retirement = _client.ClientPortfolio.Retirements.Where(x => x.Id == Retirement.Id).FirstOrDefault();
        //        if (_retirement == null)
        //        {
        //            Retirement.Id = 0;
        //            _client.ClientPortfolio.Retirements.Add(Retirement);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);
        //    }

        //    // Investment
        //    if (sender is Investment Investment)
        //    {
        //        var _investment = _client.ClientPortfolio.Investments.Where(x => x.Id == Investment.Id).FirstOrDefault();
        //        if (_investment == null)
        //        {
        //            Investment.Id = 0;
        //            _client.ClientPortfolio.Investments.Add(Investment);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);

        //    }

        //    // Education
        //    if (sender is Education Education)
        //    {

        //        var _education = _client.ClientPortfolio.Educations.Where(x => x.Id == Education.Id).FirstOrDefault();
        //        if (_education == null)
        //        {
        //            Education.Id = 0;
        //            _client.ClientPortfolio.Educations.Add(Education);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);

        //    }

        //    // Medical
        //    if (sender is Medical Medical)
        //    {
        //        var _medical = _client.ClientPortfolio.Medicals.Where(x => x.Id == Medical.Id).FirstOrDefault();
        //        if (_medical == null)
        //        {
        //            Medical.Id = 0;
        //            _client.ClientPortfolio.Medicals.Add(Medical);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);

        //    }

        //    // Life
        //    if (sender is Life Life)
        //    {

        //        var _life = _client.ClientPortfolio.Lifes.Where(x => x.Id == Life.Id).FirstOrDefault();
        //        if (_life == null)
        //        {
        //            Life.Id = 0;
        //            _client.ClientPortfolio.Lifes.Add(Life);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);

        //    }

        //    // Assets
        //    if (sender is IncomeAsset Asset)
        //    {
        //        var _assets = _client.ClientPortfolio.IncomeAssets.Where(x => x.Id == Asset.Id).FirstOrDefault();
        //        if (_assets == null)
        //        {
        //            Asset.Id = 0;
        //            _client.ClientPortfolio.IncomeAssets.Add(Asset);
        //        }

        //        Program.Repository.Update<ClientPortfolio, int>(_client.ClientPortfolio);

        //    }

        //    _client.ClientPortfolio.InvokePropertyChanged("propertyName");
        //}
        #endregion

        #region Delete Need Events
        //private void DeleteNeed_EventHandler(object sender, EventArgs e)
        //{
        //    if (sender is Need need)
        //    {
        //        if (need.Id != 0)
        //        {
        //            if (need.InstructionId != 0)
        //                throw new Exception("This item cannot be deleted as it is linked to an Task.");

        //            Program.Repository.Remove<Need, int>(need.Id);
        //        }
        //    }

        //}
        #endregion

        #region Instruction Events

        //private Instruction GetInstruction_EventHandler(object sender, EventArgs args)
        //{
        //    if (_client == null)
        //        return null;

        //    try
        //    {
        //        int id = (int)sender;
        //        return _client.ClientInstructions.Instructions.Where(x => x.Id == id).FirstOrDefault();
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //    return null;
        //}
        //private void AddClientInstruction_EventHandler(object sender, EventArgs e)
        //{
        //    if (_client == null)
        //        return;

        //    if (sender is Instruction instruction)
        //    {
        //        instruction.Description = _client.ClientDetails.Fullname;
        //        instruction.ClientId = _client.Id;
        //        instruction.UpdateDate = DateTime.Now;
        //        instruction.UpdateBy = Program.User.Username;

        //        _client.ClientInstructions.Instructions.Add(instruction);

        //        Program.Repository.Update<ClientInstructions, int>(_client.ClientInstructions);

        //    }
        //}
        //private void UpdateClientInstruction_EventHandler(object sender, EventArgs e)
        //{
        //    if (_client == null)
        //        return;

        //    if (sender is Instruction instruction)
        //    {
        //        Instruction _instruction = _client.ClientInstructions.Instructions.Where(x => x.Id == instruction.Id).FirstOrDefault();

        //        if (_instruction != null)
        //        {

        //            _instruction.Status = instruction.Status;
        //            _instruction.UpdateDate = DateTime.Now;
        //            _instruction.UpdateBy = Program.User.Username;
        //            _instruction.ReferenceOwner = instruction.ReferenceOwner;
        //            _instruction.ReferenceNo = instruction.ReferenceNo;

        //            Program.Repository.Update<Instruction, int>(_instruction);
        //        }
        //    }
        //}
        //private void DeleteClientInstruction_EventHandler(object sender, EventArgs e)
        //{
        //    if (_client == null)
        //        return;

        //    if (sender is Instruction instruction)
        //    {
        //        Instruction _instruction = _client.ClientInstructions.Instructions.Where(x => x.Id == instruction.Id).FirstOrDefault();

        //        if (_instruction != null)
        //        {
        //            _client.ClientInstructions.Instructions.Remove(_instruction);

        //            Program.Repository.Remove<Instruction, int>(instruction.Id);
        //        }
        //    }
        //}

        #endregion

        #region Client GetEvents
        //private IList<ClientDependent> GetClientBeneficiariesEvent(object sender, EventArgs args)
        //{
        //    if (_client == null)
        //        return null;

        //    return _client.ClientDependents.Dependents;
        //}

        //private IList<ClientDependent> GetPolicyOwnersEvent(object sender, EventArgs args)
        //{
        //    if (_client == null)
        //        return null;

        //    return _client.PolicyOwners;
        //}
        #endregion

        #region Outlook Integration
        private void SyncClientMeeting_Click(object sender, EventArgs e)
        {
            if (_client == null)
                return;

            _selectedItem = GetSourceGridSelectedItem(_menu.SourceControl, e);

            if (_selectedItem is ClientMeetings meeting)
            {
                if (string.IsNullOrEmpty(_client.ClientContacts.EMailAddr))
                {
                    MessageBoxExt.ShowWarning("Client Email Address is required for synchronisation");
                    return;
                }
                try
                {
                    using (new AppWaitCursor(sender))
                    {

                        try
                        {
                            var clientMeeting = new za.co.easiworx.office365.net.models.ClientMeeting()
                            {
                                MeetingId = meeting.Status,

                                //ClientId = _client.Id,
                                Notes = meeting.Notes,
                                MeetingType = meeting.MeetingType,
                                Reminder = 15,
                                SetReminder = true,
                                Venue = meeting.Venue,
                                TimeFrom = new DateTime(meeting.ScheduledDate.Year, meeting.ScheduledDate.Month, meeting.ScheduledDate.Day, meeting.TimeFrom.Hour, meeting.TimeFrom.Minute, 0),
                                TimeTo = new DateTime(meeting.ScheduledDate.Year, meeting.ScheduledDate.Month, meeting.ScheduledDate.Day, meeting.TimeTo.Hour, meeting.TimeTo.Minute, 0),
                                Subject = _client.ClientDetails.Fullname,
                                Attendees = new List<String>() { _client.ClientContacts.EMailAddr },
                                Organizer = Program.User.Username

                            };

                            Task.Run(async () => {
                                await Program.OutlookProxy.AddMeetingRequestAsync(clientMeeting);
                                meeting.Status = clientMeeting.MeetingId;
                                meeting.Sync = true;

                                _OnCompleted?.Invoke(meeting, e);
                            }
                           );
                        }
                        catch (Exception x)
                        {
                            MessageBoxExt.ShowException(x);
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
                finally
                {

                }
            }
        }
        #endregion
    }

    public class DataGridContextMenuItem : MenuItem
    {
        Bitmap bmMenuImage = null;
        public DataGridContextMenuItem(string text, EventHandler ClickEventHandler, Bitmap image)
        {
            bmMenuImage = image;

            this.Text = text;
            this.OwnerDraw = true;

            this.MeasureItem += DataGridContextMenuItem_MeasureItem;
            this.DrawItem += DataGridContextMenuItem_DrawItem;
        }

        private void DataGridContextMenuItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;

            // Get standard menu font so that the text in this
            // menu rectangle doesn't look funny with a
            // different font
            Font menuFont = SystemInformation.MenuFont;

            // Get a brush to use for painting
            SolidBrush menuBrush = null;

            // Determine menu brush for painting
            if (mi.Enabled == false)
            {
                // disabled text if menu item not enabled
                menuBrush = new SolidBrush(SystemColors.GrayText);
            }
            else // Normal (enabled) text
            {
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    // Text color when selected (highlighted)
                    menuBrush =
                      new SolidBrush(SystemColors.MenuText);
                }
                else
                {
                    // Text color during normal drawing
                    menuBrush = new SolidBrush(SystemColors.MenuText);
                }
            }

            // Center the text portion (out to side of image portion)
            StringFormat strfmt = new StringFormat();
            strfmt.LineAlignment = System.Drawing.StringAlignment.Near;

            // Get image associated with this menu item
            //Bitmap bmMenuImage =
            //    new Bitmap(typeof(FormMenuImages), "COPY.BMP");
            if (bmMenuImage != null)
            {
                // Rectangle for image portion
                Rectangle rectImage = e.Bounds;

                // Set image rectangle same dimensions as image
                rectImage.Width = bmMenuImage.Width;
                rectImage.Height = bmMenuImage.Height;

                // Rectanble for text portion
                Rectangle rectText = e.Bounds;

                // set wideth to x value of text portion
                rectText.X += rectImage.Width;

                // Start Drawing the menu rectangle

                // Fill rectangle with proper background color
                // [use this instead of e.DrawBackground() ]
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    // Selected color
                    e.Graphics.FillRectangle(SystemBrushes.Menu,
                                             e.Bounds);
                }
                else
                {
                    // Normal background color (when not selected)
                    e.Graphics.FillRectangle(SystemBrushes.Menu,
                                             e.Bounds);
                }

                // Draw image portion
                e.Graphics.DrawImage(bmMenuImage, rectImage);
            }
            // Draw string/text portion
            //
            // text portion
            // using menu font
            // using brush determined earlier
            // Start at offset of image rect already drawn
            // Total height,divided to be centered
            // Formated string
            e.Graphics.DrawString(mi.Text,
                   menuFont,
                   menuBrush,
                   e.Bounds.Left + 20,//bmMenuImage.Width,
                   e.Bounds.Top + ((e.Bounds.Height - menuFont.Height) / 2),
                   strfmt);
        }

        private void DataGridContextMenuItem_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;

            // Get standard menu font so that the text in this
            // menu rectangle doesn't look funny with a
            // different font
            Font menuFont = SystemInformation.MenuFont;

            StringFormat strfmt = new StringFormat();

            SizeF sizef =
                e.Graphics.MeasureString(mi.Text,
                                            menuFont,
                                            1000,
                                            strfmt);

            // Get image so size can be computed
            //Bitmap bmMenuImage =
            //  new Bitmap(typeof(FormMenuImages), "COPY.BMP");
            if (bmMenuImage != null)
            {
                // Add image height and width  to the text height and width when
                // drawn with selected font (got that from measurestring method)
                // to compute the total height and width needed for the rectangle
                e.ItemWidth =
              (int)Math.Ceiling(sizef.Width) + bmMenuImage.Width;
                e.ItemHeight =
                  (int)Math.Ceiling(sizef.Height) + bmMenuImage.Height;
            }
            else
            {
                e.ItemWidth =
              (int)Math.Ceiling(sizef.Width);
                e.ItemHeight =
                  (int)Math.Ceiling(sizef.Height);
            }
        }
    }

    public class DataGridColumnTooltip : SourceGrid.Cells.Controllers.ControllerBase
    {
        ToolTip cntrlToolTip = new ToolTip();

        int x = 0;
        int y = 2;

        string _tooltip;

        public DataGridColumnTooltip(string tooltip)
        {
            _tooltip = tooltip;

            cntrlToolTip.ToolTipTitle = "Tooltip";
            cntrlToolTip.ToolTipIcon = ToolTipIcon.Info;
            cntrlToolTip.IsBalloon = true;
            cntrlToolTip.UseAnimation = false;
            cntrlToolTip.UseFading = false;
        }

        public override void OnMouseDown(CellContext sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            base.OnMouseDown(sender, e);
        }

        public override void OnMouseEnter(CellContext sender, EventArgs e)
        {
            base.OnMouseEnter(sender, e);

            var grid = sender.Grid;

            grid.InvalidateCell(sender.Position);

            //Find the x pos of the selected column
            x = 0;
            for (int i = 0; i < sender.Position.Column; i++)
                x += grid.Columns.GetWidth(i);

            if (string.IsNullOrEmpty(cntrlToolTip.GetToolTip(grid)))
            {
                //cntrlToolTip.Show(_tooltip, grid, x, y - 80, 5000); //show for 5 sec
                cntrlToolTip.Show(_tooltip, grid, x, y - 80); //show until mouse leave
            }
            else
            {
                cntrlToolTip.Hide(grid);
            }

        }

        public override void OnMouseLeave(CellContext sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);

            var grid = sender.Grid;

            grid.InvalidateCell(sender.Position);

            cntrlToolTip.Hide(grid);

        }

        //public override void OnClick(CellContext sender, EventArgs e)
        //      {
        //          var grid = sender.Grid;

        //          if (string.IsNullOrEmpty(cntrlToolTip.GetToolTip(grid)))
        //          {
        //              cntrlToolTip.Show(_tooltip, grid, x, y - 80, 5000); //show for 5 sec
        //          }
        //          else
        //          {
        //              cntrlToolTip.Hide(grid);
        //          }

        //          base.OnClick(sender, e);
        //      }


    }

    public class DataGridColumnCheckbox : SourceGrid.Cells.Controllers.ControllerBase
    {

        int x = 0;
        int y = 0;

        public DataGridColumnCheckbox()
        {
           
        }

        public override void OnMouseDown(CellContext sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            base.OnMouseDown(sender, e);
        }

        public override void OnMouseEnter(CellContext sender, EventArgs e)
        {
            base.OnMouseEnter(sender, e);

            var grid = sender.Grid;

            grid.InvalidateCell(sender.Position);

            

        }

        public override void OnMouseLeave(CellContext sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);

            var grid = sender.Grid;

            grid.InvalidateCell(sender.Position);

            

        }

        public override void OnClick(CellContext sender, EventArgs e)
        {
            var grid = sender.Grid;

            
            base.OnClick(sender, e);
        }


    }

    public static class ContextenuItemsExt{
        public static ToolStripItem AddMenuItem(this ContextMenuStrip menu, string text, EventHandler clickEvent=null)
        {
            ToolStripItem item = menu.Items.Add(text);
            
            if(clickEvent!=null){
                item.Click += clickEvent;
                
            }
            return item;
        }
    }
}
