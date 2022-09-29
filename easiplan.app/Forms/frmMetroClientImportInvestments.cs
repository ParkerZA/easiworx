using CsvHelper;
using CsvHelper.Configuration;
using easiplan.domain.Entities;
using easiplan.domain.Views;
using Finx.App.Extensions;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using Finx.App.Models;
using MetroFramework.Forms;
using MoreLinq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmMetroClientImportInvestments : MetroForm
    {
        #region PrivateVars

        private List<ICsvRecord> _csvRecordList = null;
        private IEnumerable<ClientDetails> _existingClientDetails = null;
        private IEnumerable<ICsvRecord> _matchedClientsFromCsv = null;
        private IEnumerable<ICsvRecord> _csvErrorRecords = null;
        private string _filepath;
        private static int _errRecsCnt = 0;
        private string _selectedLisp;
        private int _noOfExistingClients = 0;
        private int _noOfNewClients = 0;
        private ProgressBar _pbImportFile = null;
        private BackgroundWorker _getExistingClientWorker = null;
        private IEnumerable<ICsvRecord> _distinctFileClients = null;
        private ConcurrentDictionary<string, IEnumerable<ICsvRecord>> _fileClientInvestments = null;
        private static int percCompleted = 0;
        private static int _recCnt = 0;
        private int _importBatchSize = 10;
        private IEnumerable<ClientRetirementPortfolio_View> _clientRetirementPortfolio_View = null;
        private static object _lockObject = new object();
        private static int dgvFileContentsRowCnt = 0;
        private static bool? _importCompleted = null;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private string _detectedFileDelimiter;
        private string _selectedFilename;
        private byte[] _selectedFileHash;

        #endregion

        #region PrivateClasses

        private sealed class FilePolicyFund
        {
            public string Lisp { get; set; }
            public string PolicyNo { get; set; }
            public FileFundDetails FileFundDetails { get; set; }
        }

        private sealed class FileFundDetails
        {
            public string FundCode { get; set; }
            public string FundName { get; set; }
            public double FundValue { get; set; }
            public DateTime FundValueDate { get; set; }
        }

        private sealed class FileSettings
        {
            public CsvConfiguration CsvConfiguration { get; set; }
            public string FilePath { get; set; }
        }

        #endregion

        #region Ctors

        public frmMetroClientImportInvestments()
        {
            InitializeComponent();
            Initialize();

        }

        #endregion

        #region Dtors
        ~frmMetroClientImportInvestments()
        {
            NullifyFormLevelRefTypes();
        }

        private void NullifyFormLevelRefTypes()
        {
            _csvRecordList = null;
            _existingClientDetails = null;
            _matchedClientsFromCsv = null;
            _csvErrorRecords = null;
            _pbImportFile = null;
            _getExistingClientWorker = null;
            _distinctFileClients = null;
            _fileClientInvestments = null;
            _clientRetirementPortfolio_View = null;
            _lockObject = null;
            _cancellationTokenSource = null;
            _selectedFileHash = null;
            GC.Collect();
        }
        #endregion

        #region FormEvents

        private void kbtnOpenFile_Click(object sender, EventArgs e)
        {

            if (cmbSelectLisp.SelectedIndex == 0 || cmbSelectLisp.SelectedItem.ToString().ToLower() == "please select")
            {
                cmbSelectLisp.Focus();
                return;
            }
            splitContainer1.Panel1.Visible = false;
            splitContainer1.Panel2.Visible = false;
            
            using (new AppWaitCursor(sender))
            {
                var csvHelperConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8,
                    DetectDelimiterValues = new string[] { ",", ";", "|", "\t" },
                    //DetectDelimiter = true,
                    HeaderValidated = new HeaderValidated(ValidateCsvFileHeadings),
                    TrimOptions = TrimOptions.Trim,
                    AllowComments = false,
                    //DetectColumnCountChanges = true,
                    HasHeaderRecord = true,
                    ShouldSkipRecord = new ShouldSkipRecord(shouldSkipRecord),
                    IgnoreBlankLines = true
                };

                try
                {
                    openFileDialog1.Multiselect = false;
                    openFileDialog1.Title = "Please select a file.";
                    openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) +
                                                        "\\Easiworx\\" + _selectedLisp; //System.Configuration.ConfigurationManager.AppSettings["CsvFileImportInitialDir"] + _selectedLisp; 
                    openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
                    DialogResult dialogResult;

                    try
                    {
                        dialogResult = openFileDialog1.ShowDialog(this);
                    }
                    catch (Exception)
                    {
                        openFileDialog1.InitialDirectory = System.Configuration.ConfigurationManager.AppSettings["CsvFileImportInitialDir"];
                        openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
                        dialogResult = openFileDialog1.ShowDialog(this);
                    }

                    if (dialogResult == DialogResult.OK)
                    {
                        _selectedFilename = openFileDialog1.SafeFileName;
                        _filepath = openFileDialog1.FileName;
                        //var importedCsvFiles = metroMdiMain.ImportedCsvFiles;

                        //check if file already has been imported

                        //get hash of key:filename
                        //importedCsvFiles.TryGetValue(_selectedFilename, out byte[] importedFileHash);

                        //create hash of newly selected file
                        //CreateFileHash();

                        //if (importedFileHash != null && importedFileHash.Length > 0 && _selectedFileHash.SequenceEqual(importedFileHash))
                        //{
                        //    MessageBox.Show("This file has already been imported! ", "Import Client Investments File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    cmbSelectLisp.Focus();
                        //    return;
                        //}

                        dgvFileContents.DataSource = null;
                        var fileProps = CsvFileHelper.GetFileProperties(_filepath);
                        lblSelectedFile.Text = _selectedFilename;
                        lblSelectedFile1.Visible = true;
                        lblFileDate.Text = fileProps.FileDate.ToString("dd MMM yyyy hh:mm");
                        lblFileSize.Text = string.Format("{0} KB", (fileProps.FileSize / 1024).ToString());

                        var _loadFileWorker = new BackgroundWorker() { WorkerReportsProgress = false };
                        _loadFileWorker.DoWork += LoadFileWorker_DoWork;
                        var fileSettings = new FileSettings() { FilePath = _filepath, CsvConfiguration = csvHelperConfiguration };
                        _loadFileWorker.RunWorkerCompleted += LoadFileWorker_RunWorkerCompleted;
                        _loadFileWorker.RunWorkerAsync(fileSettings);
                    }
                    else
                    {
                        splitContainer1.Panel1.Visible = true;
                        splitContainer1.Panel2.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CreateFileHash()
        {
            var fileBytes = File.ReadAllBytes(_filepath);
            _selectedFileHash = MD5.Create().ComputeHash(fileBytes);
        }

        private bool shouldSkipRecord(ShouldSkipRecordArgs args)
        {
            var record = args.Record;
            switch (_selectedLisp.ToLower())
            {
                case "camissa":
                    if (record.Any(r => r.ToUpper().StartsWith("CAMISSA COLLECTIVE INVESTMENTS LTD")) ||
                        (string.IsNullOrEmpty(record[0]) && string.IsNullOrEmpty(record[1]) && string.IsNullOrEmpty(record[2])))
                        return true;
                    break;
                case "momentum":
                    if (record.Any(r => r.ToUpper().StartsWith("NUMBER OF ROWS")) ||
                        (string.IsNullOrEmpty(record[1]) && string.IsNullOrEmpty(record[2])))
                        return true;
                    break;
            }

            return false;
        }

        private void ValidateCsvFileHeadings(HeaderValidatedArgs args)
        {
            var invalidHeaders = args.InvalidHeaders;
            if (invalidHeaders.Length > 0)
                MessageBox.Show("Invalid file headers detected. Please correct the file before trying to import again." + " " + invalidHeaders.ToDelimitedString("|"));
        }

        private async void btnImportFile_Click(object sender, EventArgs e)
        {
            var handle = this.Handle;
            var dialogResult = new DialogResult();

            await Task.Run(() =>
            {
                var win32Parent = new NativeWindow();
                win32Parent.AssignHandle(handle);
                dialogResult = MessageBox.Show(win32Parent, "Are you sure you want to Import this File into Easiworx ? Nb! Validation error records will be ignored.", "Import Client Investments File", MessageBoxButtons.YesNo);
            });

            if (dialogResult == DialogResult.No) return;

            _recCnt = 0;
            percCompleted = 0;
            _importCompleted = false;

            var totClients = _fileClientInvestments.Count();
            cancelImport.Enabled = true;

            using (new AppWaitCursor(sender))
            {
                try
                {
                    kbtnOpenFile.Enabled = false;
                    var clientKeys = _fileClientInvestments.Keys.ToList();

                    ThreadPool.SetMinThreads(38, 38);
                    _cancellationToken = _cancellationTokenSource.Token;

                    var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = -1, 
                                                                  CancellationToken = _cancellationToken };

                    if (clientKeys.Count() >= _importBatchSize)
                    {
                        var batchedClientInvestments = await Task.Run(() => MoreEnumerable.Batch(clientKeys, _importBatchSize));
                        var recordImportProgress = new Progress<ClientInvestmentRecordImportAudit>();
                        recordImportProgress.ProgressChanged += RecordImportProgress_ProgressChanged;

                        foreach (var batchedClientInvestment in batchedClientInvestments)
                        {
                            var loopResults = await ImportClientInvestmentsInParallel(parallelOptions, batchedClientInvestment, recordImportProgress, _cancellationToken);
                        }
                    }
                    else
                    {
                        _recCnt = 0;
                        foreach (var key in clientKeys)
                        {
                            await ImportClientInvestments(key, _fileClientInvestments[key]);
                            _recCnt++;
                            percCompleted = (int)Math.Round((double)(100 * _recCnt) / totClients);
                            await UpdateProgressBar(_pbImportFile, percCompleted, handle);

                            if (percCompleted == 100)
                            {
                                _importCompleted = true;

                                //RecordCsvFileImport();

                                await Task.Run(() =>
                                {
                                    var win32Parent = new NativeWindow();
                                    win32Parent.AssignHandle(handle);
                                    MessageBox.Show(win32Parent, "Client Investment Portfolios successfully imported!", "Import Client Investments File", MessageBoxButtons.OK);
                                    ValidateDataGridRecords().Wait();
                                });
                                await Task.Run(() =>
                                {
                                    kbtnOpenFile.BeginInvoke((Action)delegate
                                    {
                                        if (!kbtnOpenFile.Enabled)
                                            kbtnOpenFile.Enabled = true;
                                    });
                                });

                            }
                        }

                    }
                }
                catch (OperationCanceledException)
                {
                    await Task.Run(() =>
                    {
                        var win32Parent = new NativeWindow();
                        win32Parent.AssignHandle(handle);
                        MessageBox.Show(win32Parent,"Import operation has been cancelled!", "Easiworx Error", MessageBoxButtons.OK);
                    });
                    
                }
                catch (OperationAbortedException)
                {
                    await Task.Run(() =>
                    {
                        var win32Parent = new NativeWindow();
                        win32Parent.AssignHandle(handle);
                        MessageBox.Show(win32Parent, "Import operation has been aborted!", "Easiworx Error", MessageBoxButtons.OK);
                    });
                    
                }
                catch (AggregateException ex)
                {
                    foreach (var error in ex.Flatten().InnerExceptions)
                    {
                        Program.Logger.Error(error.Message);
                    }

                    await Task.Run(() =>
                    {
                        var win32Parent = new NativeWindow();
                        win32Parent.AssignHandle(handle);
                        MessageBox.Show(win32Parent, ex.Message, "Easiworx Error", MessageBoxButtons.OK);
                    });
                }
                catch (Exception ex)
                {
                    Program.Logger.Error(ex);
                    await Task.Run(() =>
                    {
                        var win32Parent = new NativeWindow();
                        win32Parent.AssignHandle(handle);
                        MessageBox.Show(win32Parent, ex.Message, "Easiworx Error", MessageBoxButtons.OK);
                    });
                }

            }
        }

        //private void RecordCsvFileImport()
        //{
        //    //newly imported file name is same as an existing imported file but file contents are diff, go and change the file name so that we can add it to dict
        //    if (metroMdiMain.ImportedCsvFiles.ContainsKey(_selectedFilename))
        //        metroMdiMain.ImportedCsvFiles.Add(_selectedFilename + "_" + DateTime.Now.ToString("ddMMyyyy:hhmmss"), _selectedFileHash);
        //    else
        //        metroMdiMain.ImportedCsvFiles.Add(_selectedFilename, _selectedFileHash);
        //}

        private async void RecordImportProgress_ProgressChanged(object sender, ClientInvestmentRecordImportAudit e)
        {
            var handle = this.Handle;
            var percCompleted = e.PercentageCompleted.Value;
            await UpdateProgressBar(_pbImportFile, percCompleted, handle);

            if (percCompleted == 100)
            {

                //RecordCsvFileImport();

                percCompleted = 0;
                _importCompleted = true;

                await Task.Run(() =>
                {
                var win32Parent = new NativeWindow();
                win32Parent.AssignHandle(handle);
                MessageBox.Show(win32Parent, "Client Investment Portfolios successfully imported!", "Import Client Investments File", MessageBoxButtons.OK);
                ValidateDataGridRecords().Wait();
                });

                kbtnOpenFile.BeginInvoke((Action)delegate
                {
                    if (!kbtnOpenFile.Enabled)
                        kbtnOpenFile.Enabled = true;
                });
            }
        }
        
        private void openClientToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var mdiForm = Application.OpenForms["metroMdiMain"];
            var cForm = mdiForm.MdiChildren.Where(x => x.Name == "frmMetroClient1").FirstOrDefault();
            var selectedRow = dgvFileContents.SelectedRows[0];
            var idno = selectedRow.Cells["IDNumber"].Value.ToString();
            var passportNo = selectedRow.Cells["PassportNo"].Value.ToString();
            int? clientId = null;

            if (_existingClientDetails != null && _existingClientDetails.Count() > 0)
                clientId = _existingClientDetails.Where(ec => ec.IdentificationNo == idno).FirstOrDefault().ClientId;

            if (clientId == 0) //try find client on passport no
                clientId = _existingClientDetails.Where(ec => ec.PassportNo == passportNo).FirstOrDefault().ClientId;

            if (clientId == 0)
            {
                MessageBox.Show("Client with Id: " + clientId.ToString() + " does not exist! Please contact your system administrator.");
                return;
            }

            if (cForm == null)
            {
                var childForm = new frmMetroClient1(clientId.Value);
                childForm.MdiParent = mdiForm;
                childForm.StartPosition = FormStartPosition.CenterParent;
                childForm.Size = childForm.MaximumSize;

                try
                {
                    childForm.Show();
                }
                catch (NullReferenceException)
                { } // vs does not catch this excep!
                catch (Exception x)
                {
                    MessageBoxExt.ShowWarning(x.Message);
                }
            }
            else
            {
                var frmMetroClient = cForm as frmMetroClient1;
                if (frmMetroClient != null && frmMetroClient.clientId != clientId)
                {
                    cForm.Close();
                    var childForm = new frmMetroClient1(clientId.Value);
                    childForm.MdiParent = mdiForm;
                    childForm.StartPosition = FormStartPosition.CenterParent;
                    childForm.Size = childForm.MaximumSize;
                    childForm.Show();
                }
                else
                    cForm.BringToFront();
            }

        }

        private void CopyCellContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvFileContents.CurrentCell.Value != null)
            {
                Clipboard.SetDataObject(dgvFileContents.CurrentCell.Value.ToString(), false);
            }
        }

        private void cmClientRecords_Opening(object sender, CancelEventArgs e)
        {

            var selectedRow = dgvFileContents.SelectedRows[0];
            var rowBackColor = selectedRow.DefaultCellStyle.BackColor;

            if (rowBackColor == Color.LightBlue && _existingClientDetails != null) //existing client
            {
                openClientToolStripMenuItem.Enabled = true;
                //importClientRecordToolStripMenuItem.Enabled = false;

                //var fundValueBackColor = selectedRow.Cells["FundValue"].Style.BackColor;
                //if (fundValueBackColor == Color.Orange)
                //    importClientRecordToolStripMenuItem.Enabled = true;
                //else
                //    importClientRecordToolStripMenuItem.Enabled = false;

            }
            else //new client
            {
                openClientToolStripMenuItem.Enabled = false;
                //importClientRecordToolStripMenuItem.Enabled = true;
            }

            //if (_hasErrorRecords)
            exportErrorRecordsToolStripMenuItem.Enabled = true;
            //else
            //    exportErrorRecordsToolStripMenuItem.Enabled = false;
        }

        private void cmbSelectLisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedLisp = cmbSelectLisp.SelectedItem.ToString().ToLower();
        }

        private async void exportErrorRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var handle = this.Handle;
                var fileName = await ExportErrorRecordsToCsvFile();
                await Task.Run(() =>
                {
                    var win32Owner = new NativeWindow();
                    win32Owner.AssignHandle(handle);
                    MessageBox.Show(win32Owner, string.Format("Error records successfully exported to file: {0}", fileName), "Import Client Investments File", MessageBoxButtons.OK);
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void FrmMetroClientImportInvestments_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_importCompleted != null && !_importCompleted.Value)
            {
                DialogResult dialogResult = MessageBox.Show("File Import still in progress! Are you sure you want to close this form?", "Import Client Investments", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
            }
            cancelImport_Click(this, null);

            NullifyFormLevelRefTypes();

            GC.Collect();
        }

        private void dgvFileContents_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                dgvFileContents.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = cmClientRecords;
                dgvFileContents.CurrentCell = dgvFileContents.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private async void dgvFileContents_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                var totRecs = _csvRecordList.Count();
                lblRecCnt.Text = totRecs.ToString();

                if (_existingClientDetails != null && _existingClientDetails.Count() > 0)
                {
                    await ValidateDataGridRecords();
                    _matchedClientsFromCsv = _csvRecordList.Where(csvList => _existingClientDetails.Any(ec => !string.IsNullOrEmpty(ec.IdentificationNo) && ec.IdentificationNo == csvList.IDNumber && string.IsNullOrEmpty(ec.PassportNo)))
                                                            .Concat(_csvRecordList.Where(csvList2 => _existingClientDetails.Any(ec2 => !string.IsNullOrEmpty(ec2.PassportNo) && ec2.PassportNo == csvList2.PassportNo && string.IsNullOrEmpty(ec2.IdentificationNo))));
                    _noOfExistingClients = _matchedClientsFromCsv.Count();

                    _csvErrorRecords = _csvRecordList.Where(r => r.HasErrors);
                    var errCnt = _csvErrorRecords.Count();
                    lblTotValErrors.Text = errCnt.ToString();

                    _noOfNewClients = totRecs - _noOfExistingClients;

                    lblExistingClientCnt.Text = _noOfExistingClients.ToString();
                    lblNewClientCnt.Text = (totRecs - _noOfExistingClients).ToString();

                    lblExistingClientCnt.Visible = true;
                    lblNewClientCnt.Visible = true;
                    cmClientRecords.Enabled = true;
                    splitContainer1.Panel1.Visible = true;
                    splitContainer1.Panel2.Visible = true;
                }
            }
        }

        private void cancelImport_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }
        #endregion

        #region BackgroundWorkers

        private async void GetExistingClientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _existingClientDetails = await GetExistingClientDetails();
        }

       
        private void GetExistingClientWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnImportFile.Enabled = true;
        }

        private void LoadFileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadFile((FileSettings)e.Argument);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void LoadFileWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!metroPanel4.Visible)
                metroPanel4.Visible = true;

            try
            {
                switch (_selectedLisp.ToLower())
                {
                    case "alangray":
                    case "alan gray":
                        dgvFileContents.DataSource = _csvRecordList.Cast<AlanGrayRecord>().ToList();
                        dgvFileContents.Columns["Product"].Visible = true;
                        dgvFileContents.Columns["ProductType"].Visible = true;
                        dgvFileContents.Columns["Title"].Visible = false;
                        dgvFileContents.Columns["ProductType"].Visible = false;
                        dgvFileContents.Columns["Lastname"].Visible = true;
                        break;
                    case "camissa":
                        dgvFileContents.DataSource = _csvRecordList.Cast<CamissaRecord>().ToList();
                        dgvFileContents.Columns["Product"].Visible = false;
                        dgvFileContents.Columns["ProductType"].Visible = false;
                        dgvFileContents.Columns["Title"].Visible = true;
                        dgvFileContents.Columns["Lastname"].Visible = true;
                        break;
                    case "momentum":
                        switch (_detectedFileDelimiter)
                        {
                            case "\t":
                                dgvFileContents.DataSource = _csvRecordList.Cast<MomentumRecord_TabDelimited>().ToList();
                                break;
                            default:
                                dgvFileContents.DataSource = _csvRecordList.Cast<MomentumRecord>().ToList();
                                break;
                        }

                        dgvFileContents.Columns["Lastname"].Visible = false;
                        dgvFileContents.Columns["Product"].Visible = true;
                        dgvFileContents.Columns["ProductType"].Visible = true;
                        dgvFileContents.Columns["Title"].Visible = true;
                        break;
                    //case "oasis":
                    //break;
                    default:
                        MessageBox.Show(string.Format("Selected Service Provider Not Supported: {0}", _selectedLisp.ToUpper()), "Import Client Investments File", MessageBoxButtons.OK);
                        break;
                }
            }
            catch (FieldValidationException ex)
            {
                var fieldIndex = ex.Context.Reader.CurrentIndex;
                var field = ex.Context.Reader[fieldIndex];
                MessageBox.Show(ex.Message + " " + fieldIndex.ToString() + "-" + field, "Import Client Investments File", MessageBoxButtons.OK);
            }

            dgvFileContents.CausesValidation = false;
            
            await LoadClientInvestmentsFromFile();

            await RefreshClientRetirementPortfolioView();

        }
        #endregion

        #region FormMethods
        private void Initialize()
        {

            splitContainer1.Panel1.Visible = false;
            splitContainer1.Panel2.Visible = false;
            cmClientRecords.Enabled = false;
            cmbSelectLisp.SelectedIndex = 0;

            _getExistingClientWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _getExistingClientWorker.DoWork += GetExistingClientWorker_DoWork;
            _getExistingClientWorker.WorkerReportsProgress = true;
            _getExistingClientWorker.RunWorkerCompleted += GetExistingClientWorker_RunWorkerCompleted;
            _getExistingClientWorker.RunWorkerAsync();

            metroPanel8.Controls.Add(new Label() { Dock = DockStyle.Fill });
            metroPanel9.Controls.Add(new Label() { Dock = DockStyle.Fill });
            metroPanel10.Controls.Add(new Label() { Dock = DockStyle.Fill });

            _pbImportFile = new ProgressBar() { Dock = DockStyle.Bottom, Style = ProgressBarStyle.Blocks };
            panelFileInfo.Controls.Add(_pbImportFile);
            panelFileInfo.PerformLayout();
            _pbImportFile.Visible = true;

            dgvFileContents.DataBindingComplete += dgvFileContents_DataBindingComplete;
            this.FormClosing += FrmMetroClientImportInvestments_FormClosing;
            this.copyCellContentToolStripMenuItem.Click += CopyCellContentToolStripMenuItem_Click;
            _cancellationTokenSource = new CancellationTokenSource();
            if (_lockObject == null)
                _lockObject = new object();

        }

        private async Task<List<ParallelLoopResult>> ImportClientInvestmentsInParallel(ParallelOptions parallelOptions, IEnumerable<string> batchedClientInvestment, IProgress<ClientInvestmentRecordImportAudit> Progress, CancellationToken cancellationToken)
        {
            var loopResults = new List<ParallelLoopResult>(1);
            await Task.Run(() =>
            {
                loopResults.Add(Parallel.ForEach(batchedClientInvestment, parallelOptions, async (clientIdentificationNo, loopState) =>
                {
                    if (parallelOptions.CancellationToken.IsCancellationRequested)
                    {
                        parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        //loopState.Break(); 
                    }
                    var clientInvestmentRecordImportAudit = new ClientInvestmentRecordImportAudit();

                    try
                    {

                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Pending);

                        await ImportClientInvestments(clientIdentificationNo, _fileClientInvestments[clientIdentificationNo]);
                        _recCnt++;
                        percCompleted = (int)Math.Round((double)(100 * _recCnt) / _fileClientInvestments.Keys.Count);
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Imported);
                        var message = "Records with identification number: " + clientIdentificationNo + " successfully imported!";
                        clientInvestmentRecordImportAudit.SetMessage(message);
                        clientInvestmentRecordImportAudit.SetPercentageCompleted(percCompleted);
                        Progress.Report(clientInvestmentRecordImportAudit);
                    }
                    catch (OperationCanceledException ex)
                    {
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Error);
                        clientInvestmentRecordImportAudit.SetMessage(ex.Message);
                        Program.Logger.Error(clientIdentificationNo + ": " + ex);
                        Progress.Report(clientInvestmentRecordImportAudit);
                    }
                    catch (NullReferenceException ex)
                    {
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Error);
                        clientInvestmentRecordImportAudit.SetMessage(ex.Message);
                        Program.Logger.Error(clientIdentificationNo + ": " + ex);
                        Progress.Report(clientInvestmentRecordImportAudit);
                    }
                    catch (AggregateException ex)
                    {
                        if (ex.InnerExceptions != null && ex.InnerExceptions.Count > 0)
                        {
                            foreach (var error in ex.Flatten().InnerExceptions)
                            {
                                Program.Logger.Error(error.Message);
                            }
                        }
                        else
                        {
                            Program.Logger.Error(clientIdentificationNo + ": " + ex);
                        }
                        clientInvestmentRecordImportAudit.SetMessage(ex.Message);
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Error);
                        Progress.Report(clientInvestmentRecordImportAudit);
                    }
                }));
            });
            return loopResults;
        }

        private async Task RefreshClientRetirementPortfolioView()
        {
            if (Program.ClientRetirementPortfolioService != null)
                await Task.Run(() => _clientRetirementPortfolio_View = Program.ClientRetirementPortfolioService.ListView(lv => lv.ClientId > 0));
        }

        private async Task LoadClientInvestmentsFromFile()
        {

            try
            {
                _distinctFileClients = await Task.Run(() => _csvRecordList.Where(r => !string.IsNullOrEmpty(r.IDNumber)).GroupBy(x => x.IDNumber).Select(x => x.FirstOrDefault())
                                                            .Concat(_csvRecordList.Where(r2 => string.IsNullOrEmpty(r2.IDNumber) && !string.IsNullOrEmpty(r2.PassportNo))
                                                                                            .GroupBy(x2 => x2.PassportNo).Select(x2 => x2.FirstOrDefault())));

                if (_distinctFileClients != null && _distinctFileClients.Count() > 0)
                {
                    _fileClientInvestments = new ConcurrentDictionary<string, IEnumerable<ICsvRecord>>();
                    IEnumerable<ICsvRecord> clientInvestments = null;
                    foreach (var record in _distinctFileClients)
                    {
                        clientInvestments = _csvRecordList.Where(r => !string.IsNullOrEmpty(r.IDNumber) &&
                                                                     string.IsNullOrEmpty(r.PassportNo) &&
                                                                     r.IDNumber.Trim() == record.IDNumber.Trim()
                                                                     && !r.HasErrors);
                        if (clientInvestments.Count() == 0)
                            clientInvestments = _csvRecordList.Where(r2 => !string.IsNullOrEmpty(r2.PassportNo) &&
                                                        string.IsNullOrEmpty(r2.IDNumber) && record.PassportNo != null &&
                                                        r2.PassportNo.Trim() == record.PassportNo.Trim()
                                                        && !r2.HasErrors);

                        var key = !string.IsNullOrEmpty(record.IDNumber) ? record.IDNumber.Trim() : record.PassportNo.Trim();
                        _fileClientInvestments.TryAdd(key, clientInvestments);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task ImportClientInvestments(string ClientUniqueId, IEnumerable<ICsvRecord> Investments)
        {

            try
            {
                //get client, if client portfolio exists on easiworx, return it otherwise create new client & return it
                var clientPortfolio = await GetClientPortfolio(ClientUniqueId);
                Client client = null;

                if (clientPortfolio == null) //create new client & return it
                {
                    //1st check if client exists

                    var matchedClientDetails = await Task.Run(() => _existingClientDetails.Where(cd => cd.IdentificationNo.Trim() == ClientUniqueId).FirstOrDefault());

                    if (matchedClientDetails == null) //try passport no
                        matchedClientDetails = await Task.Run(() => _existingClientDetails.Where(cd => cd.PassportNo != string.Empty && cd.PassportNo.Trim() == ClientUniqueId).FirstOrDefault());

                    if (matchedClientDetails == null)
                        client = await Task.Run(() => CreateNewClient(Investments.FirstOrDefault()));
                    else
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                lock (_lockObject)
                                {
                                    client = Program.ClientService.Get(matchedClientDetails.ClientId);
                                    if (client.ClientPortfolio == null)
                                    {
                                        client.ClientPortfolio = new ClientPortfolio() { CreateDate = DateTime.Now };
                                        Program.ClientService.Update(client);
                                    }
                                }
                            }
                            catch (AggregateException x)
                            {
                                var innerExceptions = x.InnerExceptions;
                                if (innerExceptions != null && innerExceptions.Count > 0)
                                {
                                    foreach (var ix in innerExceptions)
                                    {
                                        Program.Logger.Error(ix.Message);
                                    }
                                }
                                else
                                    Program.Logger.Error(x.ToString());
                            }
                            catch (KeyNotFoundException x)
                            {
                                Program.Logger.Error(x.Message);
                            }

                        });
                    }

                    if (client == null) return;
                }

                //get all distinct policies for this client
                var distinctRetirementPolicies = await Task.Run(() => Investments.GroupBy(i => i.AccountNo).Select(i => i.FirstOrDefault()));

                Retirement retirement = null;

                foreach (var policy in distinctRetirementPolicies)
                {

                    //check if retirement policy exists for this client
                    if (clientPortfolio != null && clientPortfolio.Retirements != null && clientPortfolio.Retirements.Count() > 0)
                        retirement = clientPortfolio.Retirements.Where(r => r.Description.Trim().ToLower() == policy.LISP.Trim().ToLower() &&
                                                                                     r.ReferenceNo.Trim().ToLower() == policy.AccountNo.Trim().ToLower())
                                                                                        .FirstOrDefault();
                    if (retirement == null)
                        await Task.Run(() =>
                        {
                            try
                            {
                                retirement = CreateRetirement(policy);
                            }
                            catch (AggregateException ex)
                            {
                                if (ex.InnerExceptions != null && ex.InnerExceptions.Count > 0)
                                {
                                    foreach (var ix in ex.InnerExceptions)
                                    {
                                        Program.Logger.Error(ix);
                                    }
                                }
                                else
                                    Program.Logger.Error(ex);
                            }

                        });

                    //get all funds per policy
                    var policyFunds = await Task.Run(() => Investments.Where(i => i.AccountNo.Trim() == policy.AccountNo.Trim()));

                    //add or update policy funds
                    var updatedretirement = AddRetirementFunds(retirement, policyFunds);//await Task.Run(() => AddRetirementFunds(retirement, policyFunds));

                    //get existing client retirements
                    List<Retirement> existingRetirements = null;

                    if (clientPortfolio != null)
                        existingRetirements = clientPortfolio.Retirements.ToList();
                    else
                    {
                        existingRetirements = client.ClientPortfolio.Retirements.ToList(); //new client retirement collection
                    }

                    //update existing retirement collection
                    existingRetirements.Add(updatedretirement);

                    //update existing client's portfolio
                    if (clientPortfolio != null)
                    {
                        clientPortfolio.Retirements = existingRetirements;
                        lock (_lockObject)
                        {
                            Program.ClientRetirementPortfolioService.Update(clientPortfolio);
                        }
                    }
                    else //update new client's retirement portfolio 
                    {
                        client.ClientPortfolio.Retirements = existingRetirements;
                        lock (_lockObject)
                        {
                            Program.ClientService.Update(client);
                        }
                    }
                    retirement = null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task<ClientPortfolio> GetClientPortfolio(string ClientUniqueId)
        {
    
            if (_existingClientDetails == null) return null;

            ClientPortfolio clientPortfolio = null;
            var clientPortfolioId = 0;

            try
            {

                //if (ClientUniqueId == "6110265160086")
                //    Debugger.Break();

                int clientId = -1;
                //get easiworx client id, 1st try rsa id no else passport no
                var matchedClientDetails = await Task.Run(() => _existingClientDetails?.Where(cd => cd.IdentificationNo.Trim() == ClientUniqueId).FirstOrDefault());

                if (matchedClientDetails == null) //try passport no
                    matchedClientDetails = await Task.Run(() => _existingClientDetails?.Where(cd => cd.PassportNo != string.Empty && cd.PassportNo.Trim() == ClientUniqueId).FirstOrDefault());

                if (matchedClientDetails != null)
                    clientId = matchedClientDetails.ClientId;

                if (clientId == 0 || clientId == -1)
                    return null;
                IEnumerable<ClientRetirementPortfolio_View> clientRetirementPortfolioList = null;

                //1st try on id no
                clientRetirementPortfolioList = await Task.Run(() => _clientRetirementPortfolio_View.Where(lv => lv.IdentificationNo == ClientUniqueId));
                //clientRetirementPortfolioList = await Task.Run(() => Program.ClientRetirementPortfolioService.ListView(lv => lv.IdentificationNo == ClientUniqueId));

                //else try on passport no
                if (clientRetirementPortfolioList == null || clientRetirementPortfolioList.Count() == 0)
                    clientRetirementPortfolioList = await Task.Run(() => _clientRetirementPortfolio_View.Where(lv => lv.PassportNo == ClientUniqueId));
                //clientRetirementPortfolioList =  await Task.Run(() => Program.ClientRetirementPortfolioService.ListView(lv => lv.PassportNo == ClientUniqueId));

                if (clientRetirementPortfolioList != null && clientRetirementPortfolioList.Count() > 0)
                    clientPortfolioId = clientRetirementPortfolioList.FirstOrDefault().ClientPortfolioId; //assuming a client can ever only have 1 portfolio? 

                //existing easiworx client portfolio
                if (clientPortfolioId > 0)
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            clientPortfolio = Program.ClientRetirementPortfolioService.Get(clientPortfolioId);
                        }
                        catch (DataException ex)
                        {
                            if (ex.InnerException != null)
                            {
                                if (ex.InnerException.InnerException != null)
                                {
                                    if (ex.InnerException.InnerException.GetType() == typeof(TimeoutException))
                                    {
                                        try
                                        {
                                            clientPortfolio = Program.ClientRetirementPortfolioService.Get(clientPortfolioId);
                                        }
                                        catch (Exception)
                                        {
                                            clientPortfolio = Program.ClientRetirementPortfolioService.Get(clientPortfolioId);
                                        }
                                    }
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

            return clientPortfolio;
        }

        private Client CreateNewClient(ICsvRecord csvRecord, string UpdateBy = "System")
        {

            if (csvRecord == null) return null;

            Client client = null;

            lock (_lockObject)
            {
                var title = "";
                var firstname = "";
                var lastname = "";

                var physicalAddress1 = "";
                var physicalAddress2 = "";
                var physicalAddress3 = "";
                var physicalAddress4 = "";
                var physicalAddress5 = "";
                var physicalAddress6 = "";
                var physicalAddressCode = 0;

                var postalAddress1 = "";
                var postalAddress2 = "";
                var postalAddress3 = "";
                var postalAddress4 = "";
                var postalAddress5 = "";
                var postalAddress6 = "";
                var postalCode = 0;

                var taxNo = "";
                var hometel = "";
                var worktel = "";
                var faxno = "";
                var cellno = "";
                var email = "";
                var dob = "";

                try
                {
                    //if (csvRecord.PassportNo == "ZP004396")
                    //  Debugger.Break();

                    //dob is a required field
                    if (!string.IsNullOrEmpty(csvRecord.IDNumber) && csvRecord.IDNumber.Length >= 9 && csvRecord.IDNumber.Length <= 13)
                    {
                        var datePart = csvRecord.IDNumber.Substring(0, 6);
                        var year = int.Parse(datePart.Substring(0, 2));
                        var month = int.Parse(datePart.Substring(2, 2));
                        var day = int.Parse(datePart.Substring(4, 2));

                        var strdt = year + "-" + month + "-" + day;

                        if (DateTime.TryParseExact(strdt, "yy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _) ||
                            DateTime.TryParseExact(strdt, "yy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _) ||
                            DateTime.TryParseExact(strdt, "y-M-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
                            dob = new DateTime(year, month, day).ToString("dd MMM yyyy");
                    }

                    switch (csvRecord.LISP.ToLower())
                    {
                        case "camissa":
                            var camissaRecord = csvRecord as CamissaRecord;

                            //check if dob field is populated in file
                            if (string.IsNullOrEmpty(dob) && string.IsNullOrEmpty(camissaRecord.DateOfBirth))
                                return null;

                            // date format: dd/MM/yyyy
                            DateTime dtDob;
                            if (DateTime.TryParseExact(camissaRecord.DateOfBirth, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob))
                                dob = dtDob.ToString("dd MMM yyyy");

                            title = camissaRecord.Title;
                            firstname = camissaRecord.Firstname;
                            lastname = camissaRecord.Lastname;
                            physicalAddress1 = camissaRecord.PhysicalAddress1;
                            physicalAddress2 = camissaRecord.PhysicalAddress2;
                            physicalAddress3 = camissaRecord.PhysicalAddress3;
                            physicalAddress4 = camissaRecord.PhysicalAddress4;
                            physicalAddress5 = camissaRecord.PhysicalAddress5;
                            physicalAddress6 = camissaRecord.PhysicalAddress6;
                            int.TryParse(camissaRecord.PhysicalAddressPostalCode, out physicalAddressCode);

                            postalAddress1 = camissaRecord.PostalAddress1;
                            postalAddress2 = camissaRecord.PostalAddress2;
                            postalAddress3 = camissaRecord.PostalAddress3;
                            postalAddress4 = camissaRecord.PostalAddress4;
                            postalAddress5 = camissaRecord.PostalAddress5;
                            postalAddress6 = camissaRecord.PostalAddress6;
                            int.TryParse(camissaRecord.PostalCode, out postalCode);

                            taxNo = camissaRecord.TaxNo;

                            hometel = camissaRecord.HomeTelephone;
                            worktel = camissaRecord.WorkTelephone;
                            cellno = camissaRecord.Cellphone;
                            faxno = camissaRecord.FaxNumber;
                            email = camissaRecord.EmailAddress;

                            break;

                        case "momentum":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var momentumRecord = csvRecord as MomentumRecord;

                            title = momentumRecord.Title;
                            firstname = momentumRecord.Firstname;
                            lastname = momentumRecord.Firstname; //momentum file does not give me the surname, and this is a required field in easiworx. defaulting to firstname until further notice

                            break;
                        case "alangray":
                        case "alan gray":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var alanGrayRecord = csvRecord as AlanGrayRecord;

                            firstname = alanGrayRecord.Firstname.Trim();
                            lastname = alanGrayRecord.Lastname.Trim();

                            break;
                        //case "oasis":
                        //  csvRecord = csvRecord as OasisRecord;
                        //break;
                        default:
                            throw new ApplicationException("Invalid Lisp!");
                    }


                    client = new Client()
                    {
                        AgentDetails = Program.User,
                        ClientDetails = new ClientDetails()
                        {
                            ClientTitle = title,
                            FirstName = firstname,
                            LastName = lastname,
                            IdentificationNo = csvRecord.IDNumber,
                            PassportNo = csvRecord.PassportNo,
                            UpdateBy = UpdateBy,
                            CreateDate = DateTime.Now

                        },
                        ClientPortfolio = new ClientPortfolio() { CreateDate = DateTime.Now }
                    };
                    if (!string.IsNullOrEmpty(physicalAddress1))
                    {
                        client.ClientDetails.RecipientAddress = physicalAddress1 + " " +
                                                 physicalAddress2 + " " +
                                                 physicalAddress3 + " " +
                                                 physicalAddress4 + " " +
                                                 physicalAddress5 + " " +
                                                 physicalAddress6 + " " +
                                                 physicalAddressCode;
                    }

                    if (!string.IsNullOrEmpty(taxNo))
                        client.ClientDetails.TaxNumber = taxNo;

                    if (!string.IsNullOrEmpty(cellno))
                        client.ClientDetails.RecipientCell = cellno;

                    if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(faxno) || !string.IsNullOrEmpty(hometel) || !string.IsNullOrEmpty(worktel) || !string.IsNullOrEmpty(cellno))
                    {
                        client.ClientContacts = new ClientContacts()
                        {
                            EMailAddr = email,
                            FaxNo = faxno,
                            HomeTel = hometel,
                            BussTel = worktel,
                            CellNo = cellno,
                            CreateDate = DateTime.Now,
                            UpdateBy = UpdateBy
                        };
                    }
                    if (!string.IsNullOrEmpty(physicalAddress1))
                    {
                        client.PhysicalAddress = new AddressDetail()
                        {
                            Line1 = physicalAddress1,
                            Line2 = physicalAddress2,
                            Line3 = physicalAddress3,
                            Line4 = physicalAddress4 + " " +
                                       physicalAddress5 + " " +
                                       physicalAddress6,
                            Code = physicalAddressCode
                        };
                    }

                    if (!string.IsNullOrEmpty(postalAddress1))
                    {
                        client.PostalAddress = new AddressDetail()
                        {
                            Line1 = postalAddress1,
                            Line2 = postalAddress2,
                            Line3 = postalAddress3,
                            Line4 = postalAddress4 + " " +
                                postalAddress5 + " " +
                                postalAddress6,
                            Code = postalCode
                        };
                    }

                    if (!string.IsNullOrEmpty(dob))
                    {
                        DateTime.TryParse(dob, out DateTime dtDob);
                        client.ClientDetails.DateOfBirth = dtDob;
                    }
                    try
                    {
                        Program.ClientService.Add(client);
                    }
                    catch (Exception)
                    {
                        //client already exists so do nothing here                    
                    }

                    if (client.Id > 0)
                    {
                        client.ClientDetails.ClientId = client.Id;

                        if (client.ClientContacts != null)
                        {
                            client.ClientContacts.ClientId = client.Id;
                            client.ClientContacts.ClientDetailsId = client.ClientDetails.Id;
                            Program.ClientService.Update(client);
                        }
                    }
                    else
                        client = null;

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return client;
        }

        private void LoadFile(FileSettings fileSettings)
        {

            try
            {

                var filepath = fileSettings.FilePath;
                var csvHelperConfiguration = fileSettings.CsvConfiguration;
                _detectedFileDelimiter = CsvFileHelper.DetectDelimiter(File.OpenText(filepath), csvHelperConfiguration.DetectDelimiterValues);
                csvHelperConfiguration.Delimiter = _detectedFileDelimiter;

                if (_selectedLisp.ToLower().Contains("camissa"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<CamissaRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("alan"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<AlanGrayRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("nedgroup"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<NedgroupRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("bullion"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<SABullionRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("momentum"))
                {
                    switch (_detectedFileDelimiter)
                    {
                        case ",":
                            _csvRecordList = CsvFileHelper.GetRecords<MomentumRecord>(filepath, csvHelperConfiguration);
                            break;
                        case "\t":
                            csvHelperConfiguration.Mode = CsvMode.NoEscape;
                            _csvRecordList = CsvFileHelper.GetRecords<MomentumRecord_TabDelimited>(filepath, csvHelperConfiguration);
                            break;
                        default:
                            throw new ApplicationException("Invalid file delimiter detected!");
                    }
                }

                dgvFileContents.AutoGenerateColumns = false;

                if (_existingClientDetails != null && _existingClientDetails.Count() > 0)
                    _matchedClientsFromCsv = _csvRecordList.Where(csvList => _existingClientDetails.Any(ec => ec.IdentificationNo == csvList.IDNumber && csvList.HasErrors == false && ec.ClientId != 0));// && ec.PassportNo == csvList.PassportNo));
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task UpdateProgressBar(ProgressBar progressBar, int value, IntPtr handle)
        {
            try
            {
                if (progressBar.InvokeRequired)
                {
                    progressBar.BeginInvoke(new Action(async delegate ()
                    {
                        await UpdateProgressBar(progressBar, value, handle);


                    }));
                }
                else
                {
                    progressBar.Value = value;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private Retirement AddRetirementFunds(Retirement retirement, IEnumerable<ICsvRecord> funds)
        {

            double fundAllocPerc = 0;
            try
            {

                if (retirement.Funds == null)
                    retirement.Funds = new List<Fund>(1);

                Fund newfund = null;
                foreach (var fund in funds)
                {

                    //if (fund.IDNumber == "9903145082083")
                    //    Debugger.Break();

                    switch (fund.LISP.ToLower())
                    {
                        case "alan gray":
                        case "alangray":
                            Double.TryParse(((AlanGrayRecord)fund).FundAllocationPercentage, out fundAllocPerc);
                            break;
                        case "momentum":
                            Double.TryParse(((MomentumRecord)fund).FundPerc, out fundAllocPerc);
                            break;
                    }

                    if (retirement.Funds.Count() > 0)
                    {
                        var existingFund = retirement.Funds.Where(f => f.Description.Trim().ToLower() == fund.FundName.Trim().ToLower()).FirstOrDefault();
                        if (existingFund == null)
                        {
                            newfund = CreateFund(fund, fundAllocPerc);
                            var retirementFunds = retirement.Funds;
                            retirementFunds.Add(newfund);
                            retirement.Funds = retirementFunds;
                        }
                        else
                        {
                            //check if new fund value & split perc is diff to original & if so add as new fund else update exist fund
                            Double.TryParse(fund.FundValue, out double newFundValue);
                            DateTime.TryParse(fund.FundValueDate, out DateTime newFundValDate);

                            if (newFundValue != existingFund.CurrentAmount && newFundValDate == existingFund.UpdateDate && fundAllocPerc != 0 && fundAllocPerc != existingFund.SplitPerc)
                            {
                                newfund = CreateFund(fund, fundAllocPerc);
                                var retirementFunds = retirement.Funds;
                                retirementFunds.Add(newfund);
                                retirement.Funds = retirementFunds;
                            }
                            else
                                UpdateFund(existingFund, fund);
                        }
                    }
                    else
                    {
                        newfund = CreateFund(fund, fundAllocPerc);
                        var retirementFunds = retirement.Funds;
                        retirementFunds.Add(newfund);
                        retirement.Funds = retirementFunds;
                    }
                    newfund = null;
                }
                retirement.Calculate();

                return retirement;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private Retirement CreateRetirement(ICsvRecord csvRecord, string updateBy = "System", string RetirementType = "Unit Trusts")
        {
            var insured = "";
            double fundAllocPerc = 0;
            string identificationNo;
            ClientDetails soughtClient = null;

            identificationNo = !string.IsNullOrEmpty(csvRecord.IDNumber) ? csvRecord.IDNumber : csvRecord.PassportNo;

            if (!string.IsNullOrEmpty(identificationNo))
            {
                soughtClient = _existingClientDetails.Where(x => x.IdentificationNo == identificationNo)
                                  .MinBy(x => x.CreateDate).FirstOrDefault();
                if (soughtClient == null)
                    soughtClient = _existingClientDetails.Where(x => x.PassportNo == identificationNo)
                                  .MinBy(x => x.CreateDate).FirstOrDefault();
                if (soughtClient == null)
                {
                    //refresh the existing client details list only if the current list does not have the client
                    GetExistingClientDetails().Wait();

                    soughtClient = _existingClientDetails.Where(x => x.IdentificationNo == identificationNo)
                                 .MinBy(x => x.CreateDate).FirstOrDefault();
                    if (soughtClient == null)
                        soughtClient = _existingClientDetails.Where(x => x.PassportNo == identificationNo)
                                      .MinBy(x => x.CreateDate).FirstOrDefault();

                }
            }

            switch (csvRecord.LISP.ToLower())
            {
                case "camissa":
                    insured = soughtClient is null ? ((CamissaRecord)csvRecord).Firstname + " " + ((CamissaRecord)csvRecord).Lastname : soughtClient.FirstName + " " + soughtClient.LastName;
                    break;
                case "momentum":
                    insured = soughtClient is null ? ((MomentumRecord)csvRecord).Firstname : soughtClient.FirstName;
                    break;
                case "alan gray":
                case "alangray":
                    insured = soughtClient is null ? ((AlanGrayRecord)csvRecord).Firstname : soughtClient.FirstName;
                    //Double.TryParse(((AlanGrayRecord)csvRecord).FundAllocationPercentage, out fundAllocPerc);
                    break;
                //case "oasis":
                //    break;
                default:
                    throw new ApplicationException("Invalid Lisp!");
            }

            lock (_lockObject)
            {
                return new Retirement()
                {
                    Type = RetirementType,
                    Description = csvRecord.LISP,
                    ReferenceNo = csvRecord.AccountNo,
                    CreateDate = DateTime.Now,
                    Insured = insured,
                    CurrentAmount = 0,
                    FundsSplitPerc = 0,
                    GrowthPercentage = 0,
                    InflationPercentage = 0,
                    InitialAmount = 0,
                    UpdateBy = updateBy
                };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Fund CreateFund(ICsvRecord csvRecord, double splitPercentage, string updateBy = "System")
        {

            Double.TryParse(csvRecord.FundValue, out double dblFundValue);
            DateTime.TryParse(csvRecord.FundValueDate, out DateTime fundValDate);

            var fund = new Fund()
            {
                Description = csvRecord.FundName,
                CreateDate = fundValDate,
                CurrentAmount = dblFundValue,
                SplitPerc = splitPercentage,
                UpdateBy = updateBy
            };

            if (fundValDate != new DateTime(0001, 1, 1))
                fund.UpdateDate = fundValDate;

            return fund;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Fund UpdateFund(Fund fund, ICsvRecord csvRecord, string UpdateBy = "System")
        {
            try
            {
                //if (csvRecord.IDNumber == "6103105116087")
                //    Debugger.Break();

                Double.TryParse(csvRecord.FundValue, out double dblFundValue);
                DateTime.TryParse(csvRecord.FundValueDate, out DateTime fundValDate);

                if (csvRecord.FundValueDate != null && fundValDate > fund.UpdateDate)
                {
                    fund.CurrentAmount = dblFundValue;
                    fund.UpdateBy = UpdateBy;
                    fund.UpdateDate = fundValDate;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fund;
        }

        private async Task<string> ExportErrorRecordsToCsvFile()
        {
            string fileName = "";

            try
            {
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) +
                                                        "\\Easiworx\\ErrorFiles";
                fileName = filePath;

                if (_selectedLisp.ToLower().Contains("alan"))
                    fileName += "AlanGray_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("atwork"))
                    fileName += "AtWork_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("camissa"))
                    fileName += "Camissa_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("discovery"))
                    fileName += "Discovery_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("marriot"))
                    fileName += "Marriot_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("franklyn"))
                    fileName += "FranklynTempleton_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("momentum"))
                    fileName += "Momentum_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("bullion"))
                    fileName += "SABullion_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("sanlam"))
                    fileName += "SanlamGlacier_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("oasis"))
                    fileName += "Oasis_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("liberty"))
                    fileName += "Liberty_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("nedgroup"))
                    fileName += "Nedgroup_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                await Task.Run(() =>
                {
                    var csvRecordsOut = _csvRecordList.Where(r => r.HasErrors);
                    var errorRows = dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => csvRecordsOut.Any(o => o.RowNo.ToString() == r.Cells[0].Value.ToString()));

                    foreach (var errorRow in errorRows)
                    {
                        if (errorRow.Cells.Cast<DataGridViewCell>().Any(c => c.ErrorText != string.Empty))
                        {
                            var errorCells = errorRow.Cells.Cast<DataGridViewCell>().Where(c => c.ErrorText != string.Empty);
                            var validationErrors = "";
                            foreach (DataGridViewCell errorCell in errorCells)
                            {
                                validationErrors += errorCell.ErrorText + " ";
                            }
                            if (csvRecordsOut != null && csvRecordsOut.Count() > 0)
                            {
                                var csvRecOut = csvRecordsOut.Where(r => r.RowNo == int.Parse(errorRow.Cells["RowNo"].Value.ToString())).FirstOrDefault();
                                csvRecOut.ValidationErrors = validationErrors;
                            }
                        }
                    }

                    var csvHelperConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8, Delimiter = ",", HeaderValidated = null, TrimOptions = TrimOptions.Trim };


                    using (var textWriter = new StreamWriter(fileName, true, Encoding.UTF8) { AutoFlush = true })
                    {
                        using (CsvHelper.CsvWriter csvWriter = new CsvWriter(textWriter, csvHelperConfiguration))
                        {
                            csvWriter.WriteRecords(csvRecordsOut);
                            csvWriter.NextRecord();
                            csvWriter.WriteComment("Record Count: " + csvRecordsOut.Count().ToString());
                        }
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
            return fileName;
        }

        private async Task ValidateDataGridRecords()
        {
            var totRowCnt = dgvFileContents.RowCount;
            var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = -1 };

            Parallel.For(dgvFileContentsRowCnt = 0, totRowCnt, parallelOptions, t =>
             {
                 var dataRow = dgvFileContents.Rows[t];
                 var dgvRowIndex = t + 1;
                 var csvRecord = _csvRecordList.Where(r => r.RowNo == dgvRowIndex).FirstOrDefault();

                 //validate idno cell
                 var idNoCell = dataRow.Cells["IDNumber"];
                 var idno = idNoCell.Value.ToString();

                 if (!string.IsNullOrEmpty(idno) && idno.Length < 13)
                 {
                     idNoCell.ErrorText = "Invalid ID No. Length < 13!";
                     idNoCell.ToolTipText = "Invalid ID No. Length < 13!";

                     dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                     csvRecord.HasErrors = true;
                 }
                 /*
                  YYMMDDGSSSCAZ

                    YYMMDD : Date of birth (DOB)
                    G : Gender. 0-4 Female; 5-9 Male
                    SSS : Sequence No. for DOB/G combination
                    C : Citizenship. 0 SA; 1 Other
                    A : Usually 8, or 9 but can be other values
                    Z : Calculated control (check) digit

                  * */

                 if (!string.IsNullOrEmpty(idno) && !Regex.IsMatch(idno, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
                 {
                     idNoCell.ErrorText = "Invalid ID No. Regex validation failure!";
                     idNoCell.ToolTipText = "Invalid ID No. Regex validation failure!";

                     dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                     csvRecord.HasErrors = true;
                 }

                 //validate fundValue cell
                 var fundValueCell = dataRow.Cells["FundValue"];
                 var fundValue = fundValueCell.Value.ToString();

                 if (string.IsNullOrEmpty(fundValue))
                 {
                     fundValueCell.ErrorText = "Invalid Fund Value!";
                     fundValueCell.ToolTipText = "Invalid Fund Value!";
                     dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                     csvRecord.HasErrors = true;
                 }
                 _ = decimal.TryParse(fundValue, out decimal outFundValue);
                 if (outFundValue == 0)
                 {
                     fundValueCell.ErrorText = "Invalid Fund Value!";
                     fundValueCell.ToolTipText = "Invalid Fund Value!";
                     dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                     csvRecord.HasErrors = true;
                 }
                 DataGridViewCell fundValueDateCell;
                 var fundValueDate = "";

                 //validate fundValueDate cell
                 DateTime dtFundValueDate;
                 if (!_selectedLisp.Contains("nedgroup"))
                 {
                     fundValueDateCell = dataRow.Cells["FundValueDate"];
                     fundValueDate = fundValueDateCell.Value.ToString();

                     if (string.IsNullOrEmpty(fundValueDate))
                     {
                         fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                         fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                         dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                         csvRecord.HasErrors = true;
                     }

                     if (!DateTime.TryParseExact(fundValueDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                     {
                         fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                         fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                         dataRow.DefaultCellStyle.BackColor = Color.LightPink;
                         csvRecord.HasErrors = true;
                     }
                     else
                     {
                         DateTime.TryParse(fundValueDate, out dtFundValueDate);
                         if (dataRow.DefaultCellStyle.BackColor != Color.LightPink)
                         {
                             fundValueDateCell.ErrorText = string.Empty;
                             fundValueDateCell.ToolTipText = string.Empty;
                             dataRow.DefaultCellStyle.BackColor = Color.White;
                         }
                     }
                 }

                 var rowNoCell = dataRow.Cells["RowNo"];

                 if (_matchedClientsFromCsv != null && _matchedClientsFromCsv.Count() > 0)
                 {
                     if (_matchedClientsFromCsv.Any(r => r.IDNumber == idno))
                     {
                         if (dataRow.DefaultCellStyle.BackColor != Color.LightPink)
                             dataRow.DefaultCellStyle.BackColor = Color.LightBlue;
                     }
                     else
                     {
                         if (dataRow.DefaultCellStyle.BackColor != Color.LightPink)
                             dataRow.DefaultCellStyle.BackColor = Color.LightGreen;
                     }
                 }
                 else
                 {
                     if (dataRow.DefaultCellStyle.BackColor != Color.LightPink)
                         dataRow.DefaultCellStyle.BackColor = Color.LightGreen;
                 }

                 if (csvRecord.HasErrors)
                     _errRecsCnt++;
             });
        }

        private async Task<IEnumerable<ClientDetails>> GetExistingClientDetails()
        {
            IEnumerable<ClientDetails> existingClientDetails = null;
            try
            {
                if (Program.ClientDetailsService == null)
                    throw new ApplicationException("ClientDetails service is null!");

                existingClientDetails = await Task.Run(() => _existingClientDetails = Program.ClientDetailsService.List(null));
            }
            catch (Exception)
            {
                throw;
            }
            return existingClientDetails;
        }
        #endregion

        
    }
}

