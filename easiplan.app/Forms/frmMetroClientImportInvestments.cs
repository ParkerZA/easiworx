using AngleSharp.Text;
using CsvFileImporter.CsvFile.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using easiplan.app.Extensions;
using easiplan.domain.Entities;
using easiplan.domain.Views;
//using EnvDTE;
using Finx.App.Extensions;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using Finx.App.Models;
using MetroFramework;
using MetroFramework.Forms;
using MoreLinq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using my.domain.lib.core.Validation;
using EnvDTE;


namespace Finx.App.Forms
{
    public partial class frmMetroClientImportInvestments : MetroForm
    {
        #region PrivateVars

        private List<ICsvRecord> _csvRecordList = null;
        //private IEnumerable<ClientDetails> _existingClientDetails = null;
        private List<ClientDetails> _existingClientDetails = null;
        //private IEnumerable<ICsvRecord> _matchedClientsFromCsv = null;
        private List<ICsvRecord> _matchedClientsFromCsv = null;
        //private IEnumerable<ICsvRecord> _csvErrorRecords = null;
        private List<ICsvRecord> _csvErrorRecords = null;
        private string _filepath;
        //private static int _errRecsCnt = 0;
        private static string _selectedLisp;
        private int _noOfExistingClients = 0;
        private int _noOfNewClients = 0;
        //private ProgressBar _pbImportFile = null;
        private BackgroundWorker _getExistingClientWorker = null;
        //private IEnumerable<ICsvRecord> _distinctFileClients = null;
        private List<ICsvRecord> _distinctFileClients = null;
        //private ConcurrentDictionary<string, IEnumerable<ICsvRecord>> _fileClientInvestments = null;
        private ConcurrentDictionary<string, List<ICsvRecord>> _fileClientInvestments = null;
        private static int _percCompleted = 0;
        private static int _recCnt = 0;
        private int _importBatchSize = 10;
        private IEnumerable<ClientRetirementPortfolio_View> _clientRetirementPortfolio_View = null;
        private static object _lockObject = new object();
        //private static int _dgvFileContentsRowCnt = 0;
        private static bool? _importCompleted = null;
        private static bool? _importCancelled = null;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private string _detectedFileDelimiter;
        private string _selectedFilename;
        private byte[] _selectedFileHash;
        private IntPtr _handle;
        private FileProperties _fileProperties;
        //private static bool _dataBindingCompleteHasRun = false;
        private frmCsvImportProgressWindow _frmCsvImportProgressWindow = null;
        private string _errorFile = "";
        //private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        

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

            this.kbtnOpenFile.Enabled = false;
            this.btnImportFile.Enabled = false;
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
            //_pbImportFile = null;
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

        #region FormEventHandlers

        private void kbtnOpenFile_Click(object sender, EventArgs e)
        {
            //Console.WriteLine($"kbtnOpenFile_Click {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");

            chkViewErrorRecords.Checked = false;
            chkViewNewRecords.Checked = false;
            chkViewExistingRecords.Checked = false;


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
                    HeaderValidated = new HeaderValidated(ValidateCsvFileHeadings),
                    TrimOptions = TrimOptions.Trim,
                    AllowComments = false,
                    HasHeaderRecord = true,
                    ShouldSkipRecord = new ShouldSkipRecord(shouldSkipRecord),
                    IgnoreBlankLines = true 
                };

                try
                {
                    openFileDialog1.Multiselect = false;
                    openFileDialog1.Title = "Please select a file.";
                    //var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) +
                    //                                    "\\Easiworx\\" + _selectedLisp; 

                    //if (!Directory.Exists(dirPath))
                    //    Directory.CreateDirectory(dirPath);

                    //openFileDialog1.InitialDirectory = dirPath;

                    //TODO: STore path in registry so user always access the same dir
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
                        //_errRecsCnt = 0;
                        //_dataBindingCompleteHasRun = false;
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
                        //dgvFileContents.DataBindingComplete -= dgvFileContents_DataBindingComplete;

                        dgvFileContents.DataSource = null;
                        _fileProperties = CsvFileHelper.GetFileProperties(_filepath);

                        lblSelectedFile.Text = _selectedFilename;
                        //lblSelectedFile1.Visible = true;
                        lblFileDate.Text = _fileProperties.FileDate.ToString("dd MMM yyyy hh:mm");
                        lblFileSize.Text = string.Format("{0} KB", (_fileProperties.FileSize / 1024).ToString());


                       


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

        private async void btnImportFile_Click(object sender, EventArgs e)
        {
            _handle = this.Handle;
            var dialogResult = new DialogResult();

            await Task.Run(() =>
            {
                var win32Parent = new NativeWindow();
                win32Parent.AssignHandle(_handle);
                dialogResult = MessageBox.Show(win32Parent, "Are you sure you want to Import this File into Easiworx ? Nb! Validation error records will be ignored.", "Import Client Investments File", MessageBoxButtons.YesNo);
            });

            if (dialogResult == DialogResult.No)
                return;

            _recCnt = 0;
            _percCompleted = 0;
            _importCompleted = false;
            lblImportStatus.Text = "Importing...";

            //todo:get last imported info from db
            //lblLastImportDate.Text = "";
            //lblLastImportUser.Text = "";

            var totClients = _fileClientInvestments.Count;
            cancelImport.Enabled = true;
            kbtnOpenFile.Enabled = false;
            this.ControlBox = false;

            _frmCsvImportProgressWindow = new frmCsvImportProgressWindow();
            _frmCsvImportProgressWindow.SetCaption("Importing Client Investments");
            _frmCsvImportProgressWindow.SetText("Please wait...");

            var backgroundWorker_ImportClientInvestmentsFromFile = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            backgroundWorker_ImportClientInvestmentsFromFile.DoWork += BackgroundWorker_ImportClientInvestmentsFromFile_DoWork;
            backgroundWorker_ImportClientInvestmentsFromFile.RunWorkerCompleted += BackgroundWorker_ImportClientInvestmentsFromFile_RunWorkerCompleted;
            backgroundWorker_ImportClientInvestmentsFromFile.RunWorkerAsync(_frmCsvImportProgressWindow);
           

            _frmCsvImportProgressWindow.ShowDialog(this);
            //Close the progress window
            _frmCsvImportProgressWindow.End();
            //_frmCsvImportProgressWindow.Close();
            MetroPopUpWindow importComplete = new MetroPopUpWindow();
            if ((_importCompleted!= null) && (_importCompleted==true))
            {
                importComplete.SetCaption("Import concluded");
            }
            else if ((_importCancelled != null) && (_importCancelled == true))
            {
                importComplete.SetCaption("Import cancelled");
                _importCancelled= false;
                lblImportStatus.Text = "Pending";
            }
            importComplete.ShowDialog();

            this.ControlBox = true;
        }

        private async void RecordImportProgress_ProgressChanged(object sender, ClientInvestmentRecordImportAudit e)
        {
            //var handle = this.Handle;
            var percCompleted = e.PercentageCompleted.Value;
            //await UpdateProgressBar(_pbImportFile, percCompleted, _handle);
            var frmCsvImportProgressWindow = e.ProgressCallback;
            
            //if (percCompleted == 100)
              //  System.Diagnostics.Debugger.Break();

                frmCsvImportProgressWindow.SetText(e.Message);

            if (percCompleted == 100)
            {
                //RecordCsvFileImport();
                if (this.IsHandleCreated)
                { 
                    frmCsvImportProgressWindow.SetText("Client Investment Portfolios successfully imported!");
                    frmCsvImportProgressWindow.End();
                }

                
                percCompleted = 0;
                _importCompleted = true;

                lblImportStatus.BeginInvoke((Action)delegate
                {
                    lblImportStatus.Text = "Imported";
                });

                //lblLastImportDate.BeginInvoke((Action)delegate
                //{
                //    lblLastImportDate.Text = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss");
                //});

                //lblLastImportUser.BeginInvoke((Action)delegate
                //{
                //    lblLastImportUser.Text = Program.User.Firstname;
                //});

                await Task.Run(() =>
                {
                    ValidateDataGridRecords();
                });




                //await Task.Run(() =>
                //{
                //var win32Parent = new NativeWindow();
                //win32Parent.AssignHandle(_handle);
                //MessageBox.Show(win32Parent, "Client Investment Portfolios successfully imported!", "Import Client Investments File", MessageBoxButtons.OK);
                //ValidateDataGridRecords().Wait();
                //});

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
            var passportNo = "";
            if (selectedRow.Cells["PassportNo"] != null && selectedRow.Cells["PassportNo"].Value != null)
                passportNo = selectedRow.Cells["PassportNo"].Value.ToString();
            
            int? clientId = null;

            if (_existingClientDetails != null && _existingClientDetails.Count > 0)
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
                frmMetroClient1 childForm = new frmMetroClient1(clientId.Value);
                string FormText = string.Format("Client {0}", clientId.Value);
                childForm.Text = FormText;
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

            this.kbtnOpenFile.Enabled = _selectedLisp!="please select";
            this.btnImportFile.Enabled = false;
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

        private void dgvFileContents_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Console.WriteLine(sender.ToString() + " " + e.ListChangedType.ToString());
            //if (_dataBindingCompleteHasRun == true) return;

            using (new AppWaitCursor(sender))
            {
                if (_existingClientDetails != null && _existingClientDetails.Count > 0)
                {
                    Task.Run(async () => { await GetMatchedEasiworxClientsFromCsv(); });
                    
                    ValidateDataGridRecords();

                    Task.Run(async () => {await GetErrorRecords();});

                    Task.Run(async () => {await SetFileImportDetails();});

                    //_dataBindingCompleteHasRun = true;

                }
            }
        }

        private void cancelImport_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationToken = _cancellationTokenSource.Token;
                _cancellationTokenSource.Cancel();
            }
        }

        private void chkViewNewRecords_CheckedChanged(object sender, EventArgs e)
        {
            chkViewExistingRecords.CheckedChanged -= chkViewExistingRecords_CheckedChanged;
            chkViewExistingRecords.Checked = false;
            chkViewErrorRecords.CheckedChanged -= chkViewErrorRecords_CheckedChanged;
            chkViewErrorRecords.Checked = false;

            if (dgvFileContents.DataSource == null) return;

            if (chkViewNewRecords.Checked)
            {
                SetDgvFileContentsDataSource(_csvRecordList);
                var newRecordList = dgvFileContents.Rows.Cast<DataGridViewRow>().ToList().Where(r => r.DefaultCellStyle.BackColor == Color.Chartreuse).ToList();
                var originalListCopy = _csvRecordList;
                var newRecords = originalListCopy.Where(l => newRecordList.Any(n => n.Cells[0].Value.ToString() == l.RowNo.ToString())).ToList();
                SetDgvFileContentsDataSource(newRecords); 
            }
            else
                SetDgvFileContentsDataSource(_csvRecordList);

            chkViewExistingRecords.CheckedChanged += chkViewExistingRecords_CheckedChanged;
            chkViewErrorRecords.CheckedChanged += chkViewErrorRecords_CheckedChanged;
        }

        private void chkViewExistingRecords_CheckedChanged(object sender, EventArgs e)
        {

            chkViewNewRecords.CheckedChanged -= chkViewNewRecords_CheckedChanged;
            chkViewNewRecords.Checked = false;
            chkViewErrorRecords.CheckedChanged -= chkViewErrorRecords_CheckedChanged;
            chkViewErrorRecords.Checked = false;

            if (dgvFileContents.DataSource == null) return;
            if (_matchedClientsFromCsv == null) return;

            if (chkViewExistingRecords.Checked)
                SetDgvFileContentsDataSource(_matchedClientsFromCsv);
            else
                SetDgvFileContentsDataSource(_csvRecordList);

            chkViewNewRecords.CheckedChanged += chkViewNewRecords_CheckedChanged;
            chkViewErrorRecords.CheckedChanged += chkViewErrorRecords_CheckedChanged;
        }

        private void chkViewErrorRecords_CheckedChanged(object sender, EventArgs e)
        {
            chkViewNewRecords.CheckedChanged -= chkViewNewRecords_CheckedChanged;
            chkViewNewRecords.Checked = false;
            chkViewExistingRecords.CheckedChanged -= chkViewExistingRecords_CheckedChanged;
            chkViewExistingRecords.Checked = false;

            if (dgvFileContents.DataSource == null) return;
            if (_csvErrorRecords == null) return;

            if (chkViewErrorRecords.Checked)
            {
                SetDgvFileContentsDataSource(_csvRecordList);
                var newRecordList = dgvFileContents.Rows.Cast<DataGridViewRow>().ToList().Where(r => r.DefaultCellStyle.BackColor == Color.FromArgb(230,7,7)).ToList();
                var originalListCopy = _csvRecordList;
                var newRecords = originalListCopy.Where(l => newRecordList.Any(n => n.Cells[0].Value.ToString() == l.RowNo.ToString())).ToList();
                SetDgvFileContentsDataSource(newRecords);
            }
            else
                SetDgvFileContentsDataSource(_csvRecordList);

            chkViewNewRecords.CheckedChanged += chkViewNewRecords_CheckedChanged;
            chkViewExistingRecords.CheckedChanged += chkViewExistingRecords_CheckedChanged;

        }

        //private async void exportErrorRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var handle = this.Handle;
        //        var fileName = await ExportErrorRecordsToCsvFile();
        //        await Task.Run(() =>
        //        {
        //            var win32Owner = new NativeWindow();
        //            win32Owner.AssignHandle(handle);
        //            MessageBox.Show(win32Owner, string.Format("Error records successfully exported to file: {0}", fileName), "Import Client Investments File", MessageBoxButtons.OK);
        //        });
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        #endregion

        #region BackgroundWorkers
        private async void GetExistingClientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _existingClientDetails = await GetExistingClientDetails();
        }
        private void GetExistingClientWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // btnImportFile.Enabled = true;
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
        private void LoadFileWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                

                if (_csvRecordList == null) return;

                dgvFileContents.DataBindingComplete += dgvFileContents_DataBindingComplete;

                SetDgvFileContentsDataSource(_csvRecordList);
                chkViewErrorRecords.Enabled = true;
                chkViewNewRecords.Enabled = true;
                chkViewExistingRecords.Enabled = true;
            }
            catch (FieldValidationException ex)
            {
                var fieldIndex = ex.Context.Reader.CurrentIndex;
                var field = ex.Context.Reader[fieldIndex];
                MessageBox.Show(ex.Message + " " + fieldIndex.ToString() + "-" + field, "Import Client Investments File", MessageBoxButtons.OK);
            }

            dgvFileContents.CausesValidation = false;

            Task.Run(async () =>
            {
                await LoadClientInvestmentsFromFile();
                RefreshClientRetirementPortfolioView().Wait();
                await ExportErrorRecordsToCsvFile();
            });



            this.btnImportFile.Enabled = true;

        }
        private void SetDgvFileContentsDataSource(List<ICsvRecord> csvRecords)
        {
            
            
            dgvFileContents.DataSource = null;

               
            switch (_selectedLisp.ToLower())
            {
                case "allangray":
                case "allan gray":
                case "alan gray":
                    dgvFileContents.DataSource = csvRecords.Cast<AllanGrayRecord>().ToList();
                    dgvFileContents.Columns["Product"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = true;
                    dgvFileContents.Columns["Title"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = false;
                    dgvFileContents.Columns["Lastname"].Visible = true;
                    dgvFileContents.Columns["BirthDate"].Visible = false;
                    dgvFileContents.Columns["RegistrationNo"].Visible = false;
                    dgvFileContents.Columns["ClientNo"].Visible = true;
                    dgvFileContents.Columns["AccountFundAllocation"].Visible = true;

                    break;

                case "camissa":
                    dgvFileContents.DataSource = csvRecords.Cast<CamissaRecord>().ToList();
                    dgvFileContents.Columns["Product"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = true;
                    dgvFileContents.Columns["Title"].Visible = true;
                    dgvFileContents.Columns["Lastname"].Visible = true;
                    dgvFileContents.Columns["Premium"].Visible = true;
                    dgvFileContents.Columns["BirthDate"].Visible = true;
                    dgvFileContents.Columns["RegistrationNo"].Visible = false;
                    dgvFileContents.Columns["ClientNo"].Visible = false;
                    dgvFileContents.Columns["AccountFundAllocation"].Visible = true;

                    break;

                case "momentum":
                case "mtab":
                    switch (_detectedFileDelimiter)
                    {
                        case "\t":
                            dgvFileContents.DataSource = csvRecords.Cast<MomentumRecord_TabDelimited>().ToList();
                            _selectedLisp = "mTab"; //Setting selected lisp to mTab to indicate that this is the Tab Delimited Momentum CSV 
                           
                            break;

                        default:
                            dgvFileContents.DataSource = csvRecords.Cast<MomentumRecord>().ToList();
                            
                            break;
                    }

                    dgvFileContents.Columns["Firstname"].Visible = false;
                    dgvFileContents.Columns["Product"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = true;
                    dgvFileContents.Columns["Title"].Visible = true;
                    dgvFileContents.Columns["BirthDate"].Visible = false;
                    dgvFileContents.Columns["RegistrationNo"].Visible = false;
                    dgvFileContents.Columns["ClientNo"].Visible = false;
                    dgvFileContents.Columns["Premium"].Visible = false;
                    dgvFileContents.Columns["AccountFundAllocation"].Visible = true;
                    break;
                case "easiworx":
                case "easiworxtemplate":
                    dgvFileContents.DataSource = csvRecords.Cast<EasiworxRecord>().ToList();
                    
                    dgvFileContents.Columns["Product"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = true;
                    dgvFileContents.Columns["Title"].Visible = false;
                    dgvFileContents.Columns["Lastname"].Visible = true;
                    dgvFileContents.Columns["BirthDate"].Visible = true;
                    dgvFileContents.Columns["RegistrationNo"].Visible = false;
                    dgvFileContents.Columns["AccountFundAllocation"].Visible = true;
                    dgvFileContents.Columns["ClientNo"].Visible = true;
                    dgvFileContents.Columns["Premium"].Visible = true;
 
                    break;

                case "astutetemplate":
                    dgvFileContents.DataSource = csvRecords.Cast<AstuteRecord>().ToList();

                    dgvFileContents.Columns["PassportNo"].Visible=false;
                    dgvFileContents.Columns["Product"].Visible = false;
                    dgvFileContents.Columns["ProductType"].Visible = true;
                    dgvFileContents.Columns["Title"].Visible = false;
                    dgvFileContents.Columns["Lastname"].Visible = true;
                    dgvFileContents.Columns["BirthDate"].Visible = false;
                    dgvFileContents.Columns["RegistrationNo"].Visible = false;
                    dgvFileContents.Columns["AccountFundAllocation"].Visible = false;
                    dgvFileContents.Columns["ClientNo"].Visible = false;
                    dgvFileContents.Columns["Premium"].Visible = false;
                    break;
                default:
                    MessageBox.Show(string.Format("Selected Service Provider Not Supported: {0}", _selectedLisp.ToUpper()), "Import Client Investments File", MessageBoxButtons.OK);
                    break;
            }
        }
        private void BackgroundWorker_ImportClientInvestmentsFromFile_DoWork(object sender, DoWorkEventArgs e)
        {
            var frmCsvImportProgressWindow = e.Argument as frmCsvImportProgressWindow;
            if (frmCsvImportProgressWindow == null)
                frmCsvImportProgressWindow = _frmCsvImportProgressWindow;
            frmCsvImportProgressWindow.CancellationTokenSource = _cancellationTokenSource;
            
            ImportClientInvestmentsFromFile(frmCsvImportProgressWindow).Wait();
        }
        private void BackgroundWorker_ImportClientInvestmentsFromFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //what to do here?
            //MessageBox.Show("You clicked cancel");
            
        }
                
        #endregion

        #region FormMethods
        private void Initialize()
        {

            splitContainer1.Panel1.Visible = false;
            splitContainer1.Panel2.Visible = false;
            cmClientRecords.Enabled = false;
            cmbSelectLisp.SelectedIndex = 0;

            var primaryMonitorSizeWidth = SystemInformation.PrimaryMonitorSize.Width;
            var splitterDistance = 0.60 * primaryMonitorSizeWidth;

            Program.Logger.Info("PrimaryMonitorSizeWidth: " + primaryMonitorSizeWidth.ToString());
            Program.Logger.Info("SplitterDistance: " + splitterDistance.ToString());

            int.TryParse(splitterDistance.ToString(), out int intSplitterDistance);
            if(intSplitterDistance > 50)
                splitContainer1.SplitterDistance = intSplitterDistance;

            _getExistingClientWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _getExistingClientWorker.DoWork += GetExistingClientWorker_DoWork;
            _getExistingClientWorker.WorkerReportsProgress = true;
            _getExistingClientWorker.RunWorkerCompleted += GetExistingClientWorker_RunWorkerCompleted;
            _getExistingClientWorker.RunWorkerAsync();

            //_pbImportFile = new ProgressBar() { Dock = DockStyle.Bottom, Style = ProgressBarStyle.Blocks };
            //panelFileInfo.Controls.Add(_pbImportFile);
            //panelFileInfo.PerformLayout();
            //_pbImportFile.Visible = true;

            this.FormClosing += FrmMetroClientImportInvestments_FormClosing;
            this.copyCellContentToolStripMenuItem.Click += CopyCellContentToolStripMenuItem_Click;
            _cancellationTokenSource = new CancellationTokenSource();
            //_cancellationToken  = _cancellationTokenSource.Token;
            if (_lockObject == null)
                _lockObject = new object();

        }

        private async Task<List<ParallelLoopResult>> ImportClientInvestmentsInParallel(ParallelOptions parallelOptions, IEnumerable<string> batchedClientInvestment, IProgress<ClientInvestmentRecordImportAudit> Progress, frmCsvImportProgressWindow frmCsvImportProgressWindow)
        {
            var loopResults = new List<ParallelLoopResult>(1);
            await Task.Run(() =>
            {
                loopResults.Add(Parallel.ForEach(batchedClientInvestment, parallelOptions, async (clientIdentificationNo, loopState) =>
                {
                    //Console.WriteLine("A new one");
                    var clientInvestmentRecordImportAudit = new ClientInvestmentRecordImportAudit();

                    clientInvestmentRecordImportAudit.SetPercentageCompleted(_percCompleted);
                    //frmCsvImportProgressWindow.CancellationTokenSource = parallelOptions.CancellationToken
                    var progressCallback = frmCsvImportProgressWindow;
                    clientInvestmentRecordImportAudit.SetProgressCallback(progressCallback);
                    try
                    {

                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Pending);

                        //here
                        if (parallelOptions.CancellationToken.IsCancellationRequested)
                        {
                            loopState.Stop(); 
                            parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        }
                        await ImportClientInvestments(clientIdentificationNo, _fileClientInvestments[clientIdentificationNo]);
                        _recCnt++;
                        _percCompleted = (int)Math.Round((double)(100 * _recCnt) / _fileClientInvestments.Keys.Count);
                        clientInvestmentRecordImportAudit.SetPercentageCompleted(_percCompleted);
                       
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Imported);
                        
                        var message = "Records with identification number: " + clientIdentificationNo + " successfully imported!";
                        
                        clientInvestmentRecordImportAudit.SetMessage(message);
                        
                        
                        Progress.Report(clientInvestmentRecordImportAudit);
                        
                        //Check if cancel button has been clicked
                        if (parallelOptions.CancellationToken.IsCancellationRequested)
                        {
                            //Console.WriteLine("Hi, still going");
                            loopState.Stop();
                            parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Error);
                        clientInvestmentRecordImportAudit.SetMessage(ex.Message);
                        Program.Logger.Error(clientIdentificationNo + ": " + ex);
                        Progress.Report(clientInvestmentRecordImportAudit);
                        _importCancelled= true;
                        _importCompleted = false;


                        //loopState.Stop();
                        //frmCsvImportProgressWindow.End();

                        //return;
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
                }
                
                ));
                //Close the progress window
                //frmCsvImportProgressWindow.End();
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
                                                                                            .GroupBy(x2 => x2.PassportNo).Select(x2 => x2.FirstOrDefault())).ToList());

                if (_distinctFileClients != null && _distinctFileClients.Count > 0)
                {
                    //_fileClientInvestments = new ConcurrentDictionary<string, IEnumerable<ICsvRecord>>();
                    _fileClientInvestments = new ConcurrentDictionary<string, List<ICsvRecord>>();
                    //IEnumerable<ICsvRecord> clientInvestments = null;
                    List<ICsvRecord> clientInvestments = null;
                    foreach (var record in _distinctFileClients)
                    {
                        clientInvestments = _csvRecordList.Where(r => !string.IsNullOrEmpty(r.IDNumber) &&
                                                                     string.IsNullOrEmpty(r.PassportNo) &&
                                                                     r.IDNumber.Trim() == record.IDNumber.Trim()
                                                                     && !r.HasErrors).ToList();
                        if (clientInvestments.Count == 0)
                            clientInvestments = _csvRecordList.Where(r2 => !string.IsNullOrEmpty(r2.PassportNo) &&
                                                        string.IsNullOrEmpty(r2.IDNumber) && record.PassportNo != null &&
                                                        r2.PassportNo.Trim() == record.PassportNo.Trim()
                                                        && !r2.HasErrors).ToList();

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

        private async Task ImportClientInvestments(string ClientUniqueId, List<ICsvRecord> Investments)
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
                    {
                        client = await Task.Run(() => CreateNewClient(Investments.FirstOrDefault()));

                        if (!(client == null))
                        {
                            lock (_lockObject)
                            {
                                UpdateClientDetails(client, Investments.FirstOrDefault());
                                Program.ClientService.Update(client);
                            }
                        }
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                lock (_lockObject)
                                {
                                    client = Program.ClientService.Get(matchedClientDetails.ClientId);

                                    if (!(client == null))
                                    { 
                                        if (client.ClientPortfolio == null)
                                        {
                                            client.ClientPortfolio = new ClientPortfolio() { CreateDate = DateTime.Now };
                                            Program.ClientService.Update(client);
                                        }

                                        if (client.PhysicalAddress == null)
                                        {
                                            client.PhysicalAddress = new AddressDetail() { CreateDate = DateTime.Now };
                                            Program.ClientService.Update(client);
                                        }

                                        if (client.PostalAddress == null)
                                        {
                                            client.PostalAddress = new AddressDetail() { CreateDate = DateTime.Now };
                                            Program.ClientService.Update(client);
                                        }


                                        if (client.BankDetails == null)
                                        {
                                            client.BankDetails = new BankDetail() { CreateDate = DateTime.Now };
                                            Program.ClientService.Update(client);
                                        }

                                        UpdateClientDetails(client, Investments.FirstOrDefault());
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
                var distinctRetirementPolicies = await Task.Run(() => Investments.GroupBy(i => i.AccountNo).Select(i => i.FirstOrDefault()).ToList());

                Retirement retirement = null;
                foreach (var policy in distinctRetirementPolicies)
                {

                    //check if retirement policy exists for this client
                    if (clientPortfolio != null && clientPortfolio.Retirements != null && clientPortfolio.Retirements.Count > 0)
                    {
                        retirement = clientPortfolio.Retirements.Where(r => r.Description.Trim().ToLower() == policy.LISP.Trim().ToLower() &&
                                                                                     r.ReferenceNo.Trim().ToLower() == policy.AccountNo.Trim().ToLower())
                                                                                        .FirstOrDefault();
                        //This is where you need to operate, need to make it check if the model portfolio is not the same as well (Should say if lisp is the same, and if (reference num or (model portfolio is the same and model p))
                    
                    }                                                                                                                  

                    if (retirement == null)
                    {
                        Task.Run(() =>
                        {
                            try
                            {
                                retirement = CreateRetirement(policy, "System", policy.ProductType);
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

                        }).Wait();
                    }
                    //get all funds per policy
                    var policyFunds = await Task.Run(() => Investments.Where(i => i.AccountNo.Trim() == policy.AccountNo.Trim()).ToList());

                    //add or update policy funds
                    
                    
                    var updatedretirement = await Task.Run(() => AddRetirementFunds(retirement, policyFunds));
                    
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

                int clientId = -1;
                //get easiworx client id, 1st try rsa id no else passport no
                var matchedClientDetails = await Task.Run(() => _existingClientDetails?.Where(cd => cd.IdentificationNo.Trim() == ClientUniqueId).FirstOrDefault());

                if (matchedClientDetails == null) //try passport no
                    matchedClientDetails = await Task.Run(() => _existingClientDetails?.Where(cd => cd.PassportNo != string.Empty && cd.PassportNo.Trim() == ClientUniqueId).FirstOrDefault());

                if (matchedClientDetails != null)
                    clientId = matchedClientDetails.ClientId;

                if (clientId == 0 || clientId == -1)
                    return null;
                //IEnumerable<ClientRetirementPortfolio_View> clientRetirementPortfolioList = null;
                List<ClientRetirementPortfolio_View> clientRetirementPortfolioList = null;

                //1st try on id no
                if(_clientRetirementPortfolio_View != null)
                    clientRetirementPortfolioList = await Task.Run(() => _clientRetirementPortfolio_View.Where(lv => lv.IdentificationNo == ClientUniqueId).ToList());
                //clientRetirementPortfolioList = await Task.Run(() => Program.ClientRetirementPortfolioService.ListView(lv => lv.IdentificationNo == ClientUniqueId));

                //else try on passport no
                if (clientRetirementPortfolioList == null || clientRetirementPortfolioList.Count == 0)
                {
                    if(_clientRetirementPortfolio_View != null)
                        clientRetirementPortfolioList = await Task.Run(() => _clientRetirementPortfolio_View.Where(lv => lv.PassportNo == ClientUniqueId).ToList());
                }
                //clientRetirementPortfolioList =  await Task.Run(() => Program.ClientRetirementPortfolioService.ListView(lv => lv.PassportNo == ClientUniqueId));

                if (clientRetirementPortfolioList != null && clientRetirementPortfolioList.Count > 0)
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
            DateTime now = DateTime.Now;
            if (csvRecord == null) return null;

            Client client = null;

            lock (_lockObject)
            {
                var title = "";
                var firstname = "";
                var lastname = "";
                var dob = "";
                var taxNo = "";
                var accName = "";
                //DateTime dtDob = new DateTime(0001, 1, 1);
                try
                {
                    //if (csvRecord.PassportNo == "ZP004396")
                    //  Debugger.Break();

                    //dob is a required field
                    if (!string.IsNullOrEmpty(csvRecord.IDNumber) && csvRecord.IDNumber.Length >= 9 && csvRecord.IDNumber.Length <= 13)
                    {
                        DateTime dtDob;
                        //Here I have put in easiworx id to do testing on saadiqas side
                        var easiworx = csvRecord as EasiworxRecord;

                        //Messagebox to check for difference in ID
                        MessageBox.Show("The id number is *" + csvRecord.IDNumber + "*");

                        //These easiworx ids are to be switched out for csvRecord.IDNumber once problem is solved
                        dtDob = DateTime.Parse(string.Format("{0}/{1}/20{2}", (object)easiworx.IDNumber.Substring(4, 2), (object)easiworx.IDNumber.Substring(2, 2), (object)easiworx.IDNumber.Substring(0, 2)));
                        if (dtDob.CompareTo(DateTime.Now) > 0)
                        {
                            dtDob = dtDob.AddYears(-100);
                        }
                        /*var datePart = csvRecord.IDNumber.Substring(0, 6);
                        var year = int.Parse(datePart.Substring(0, 2));
                        var month = int.Parse(datePart.Substring(2, 2));
                        var day = int.Parse(datePart.Substring(4, 2));

                        if (year > 49)
                        {
                            year = year + 1900;
                        }
                        else 
                        {
                            year = year + 2000;
                        }

                        var strdt = year + "-" + month + "-" + day;
                        
                        if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob) ||
                            DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob))
                            // || DateTime.TryParseExact(strdt, "y-M-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob))
                        {
                            if (dtDob > now)
                            {
                                dtDob= dtDob.AddYears(-100);
                                
                            }

                            dob = dtDob.ToString("dd MMM yyyy");
                            //dob = new DateTime(year, month, day).ToString("dd MMM yyyy");
                        }*/

                        dob = dtDob.ToString("dd MMM yyyy");

                    }

                    //switch (csvRecord.LISP.ToLower())
                    switch (_selectedLisp.ToLower())
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

                            break;

                        case "momentum":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var momentumRecord = csvRecord as MomentumRecord;

                            title = momentumRecord.Title;
                            firstname = momentumRecord.Initials; //Momentum only gives an initial, I am storing this as the first name for now
                            lastname = momentumRecord.Lastname; 

                            break;

                        case "mtab":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var momentumTabRecord = csvRecord as MomentumRecord_TabDelimited;

                            title = momentumTabRecord.Title;
                            firstname = momentumTabRecord.Initials; //Momentum does not give first name, only Initial. I am storing this as the first name for now
                            lastname = momentumTabRecord.Lastname; 
                            break;
                        
                        case "alangray":
                        case "alan gray":
                        case "allangray":
                        case "allan gray":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var allanGrayRecord = csvRecord as AllanGrayRecord;

                            firstname = allanGrayRecord.Firstname.Trim();
                            lastname = allanGrayRecord.Lastname.Trim();
  

                            break;

                        case "astutetemplate":
                            //Nb! no dob field provided in csv file, therefor clients with passport nos wont get added to easiworx as dob is a required field
                            if (string.IsNullOrEmpty(dob))
                                return null;

                            var astuteRecord = csvRecord as AstuteRecord;

                            firstname = astuteRecord.Firstname.Trim();
                            lastname = astuteRecord.Lastname.Trim();


                            break;

                        case "easiworx":
                        case "easiworxtemplate":
                            
                            var easiworxRecord = csvRecord as EasiworxRecord;
                            firstname = easiworxRecord.Firstname.Trim();
                            lastname = easiworxRecord.Lastname.Trim();
                            accName = easiworxRecord.AccountName.Trim();
                            
                            taxNo = easiworxRecord.TaxNo.Trim();


                            // date format: dd/MM/yyyy
                            DateTime ewx_dtDob;
                            if (DateTime.TryParseExact(easiworxRecord.DateOfBirth, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ewx_dtDob))
                            { 
                                dob = ewx_dtDob.ToString("dd MMM yyyy"); 
                            }
                            /*if (easiworxRecord.getBirthday != null)
                            {
                                dtDob = easiworxRecord.getBirthday;
                            }*/
                            //Console.WriteLine("First one " + dob);
                            break;
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
                            TaxNumber = taxNo,
                            UpdateBy = UpdateBy,
                            CreateDate = DateTime.Now

                        },
                        ClientPortfolio = new ClientPortfolio() { CreateDate = DateTime.Now }
                    };
                    

                    client.ClientContacts = new ClientContacts()
                    {
                        EMailAddr = "",
                        FaxNo = "",
                        HomeTel = "",
                        BussTel = "",
                        CellNo = "",
                        CreateDate = DateTime.Now,
                        UpdateBy = UpdateBy
                    };

                    client.PhysicalAddress = new AddressDetail()
                    {
                        Line1 = "",
                        Line2 = "",
                        Line3 = "",
                        Line4 = "",
                        Code = 0,
                        CreateDate = DateTime.Now,
                        UpdateBy = UpdateBy
                    };

                    client.PostalAddress = new AddressDetail()
                    {
                        Line1 = "",
                        Line2 = "",
                        Line3 = "",
                        Line4 = "",
                        Code = 0,
                        CreateDate = DateTime.Now,
                        UpdateBy = UpdateBy
                    };

                    client.BankDetails = new BankDetail()
                    {
                        BnkName = "",
                        BrnchName = "",
                        BrnchCode = "",
                        AcctNumber = "",
                        AcctType = "",
                        AcctName = accName,
                        CreateDate = DateTime.Now,
                        UpdateBy = UpdateBy
                    };

                    if (!string.IsNullOrEmpty(dob))
                    {
                        if (DateTime.TryParseExact(dob, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtDob))
                        {
                            client.ClientDetails.DateOfBirth = dtDob;
                        }
                    }

                    /*if ((dtDob != (new DateTime(0001, 1, 1))))
                    {
                        client.ClientDetails.DateOfBirth = dtDob;
                    }*/
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
                        }

                        Program.ClientService.Update(client);
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


        

        private Client UpdateClientDetails(Client client, ICsvRecord csvRecord, string UpdateBy = "System")
        {
            
            try
            {

                DateTime now = DateTime.Now;
                if (csvRecord == null) return null;


                lock (_lockObject)
                {
                    //DateTime ewx_dtDob= new DateTime(0001, 1, 1);

                    var dob = "";
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

                    var bankName = "";
                    var branchName= "";
                    var branchCode = "";
                    var bankAccNo = "";
                    var bankAccType = "";
                    //var accName = "";

                    //switch (csvRecord.LISP.ToLower())
                    switch (_selectedLisp.ToLower())
                    {
                        case "camissa":
                            var camissaRecord = csvRecord as CamissaRecord;


                            //birthdate

                            DateTime cm_dtDob;
                            if (DateTime.TryParseExact(camissaRecord.DateOfBirth, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out cm_dtDob))
                                dob = cm_dtDob.ToString("dd MMM yyyy");

                            //Camissa Physical Address

                            //Below I am splitting the first line of the Camissa address format into a street number and road name
                            string streetNumber = "";
                            string roadName = "";

                            int spaceIndex = camissaRecord.PhysicalAddress1.IndexOf(' '); //Gets index of first space in address

                            streetNumber = camissaRecord.PhysicalAddress1.Substring(0, spaceIndex); //Sets street number
                            roadName = camissaRecord.PhysicalAddress1.Substring(spaceIndex).Trim(); //Sets road name

                            physicalAddress1 = streetNumber; //Street number
                            physicalAddress2 = roadName; //Road name
                            physicalAddress3 = camissaRecord.PhysicalAddress2; // Suburb (Possibly refactor, especially if the other lisps have a very different address structure)
                            physicalAddress4 = camissaRecord.PhysicalAddress4;
                            physicalAddress5 = camissaRecord.PhysicalAddress5;
                            physicalAddress6 = camissaRecord.PhysicalAddress6;
                            int.TryParse(camissaRecord.PhysicalAddressPostalCode, out physicalAddressCode);

                            //Camissa Postal Address

                            //Splitting postal road number from the street name
                            string posStreetNumber = "";
                            string posRoadName = "";

                            int posSpaceIndex = camissaRecord.PostalAddress1.IndexOf(' '); //Gets index of first space in address

                            posStreetNumber = camissaRecord.PhysicalAddress1.Substring(0, posSpaceIndex); //Sets street number
                            posRoadName = camissaRecord.PhysicalAddress1.Substring(posSpaceIndex).Trim(); //Sets road name

                            postalAddress1 = posStreetNumber; //Street number
                            postalAddress2 = posRoadName; //Road name
                            postalAddress3 = camissaRecord.PostalAddress2; //Suburb (This is really dumb... might have to refactor)
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


                            var momentumRecord = csvRecord as MomentumRecord;
                            //Currently momentum does not provide address and contact details

                            break;

                        case "mtab":


                            var momentumTabRecord = csvRecord as MomentumRecord_TabDelimited;

                            break;
                        case "astutetemplate":


                            var astuteRecord = csvRecord as AstuteRecord;

                            break;

                        case "alangray":
                        case "alan gray":
                        case "allangray":
                        case "allan gray":

                            var allanGrayRecord = csvRecord as AllanGrayRecord;

                            //firstname = allanGrayRecord.Firstname.Trim();
                            //lastname = allanGrayRecord.Lastname.Trim();


                            break;
                        case "easiworx":
                        case "easiworxtemplate":

                            var easiworxRecord = csvRecord as EasiworxRecord;
                            //firstname = easiworxRecord.Firstname.Trim();
                            //lastname = easiworxRecord.Lastname.Trim();

                            //Tax number 
                            taxNo = easiworxRecord.TaxNo.Trim();


                            //birthdate

                            //ewx_dtDob = easiworxRecord.getBirthday;
                            
                            //Easiworx Physical Address

                            physicalAddress1 = easiworxRecord.PhysicalAddressStreetNo;
                            physicalAddress2 = easiworxRecord.PhysicalAddress;
                            physicalAddress3 = easiworxRecord.PhysicalAddressSuburb;
                            //physicalAddress4 = easiworxRecord.PhysicalAddress4; //This will be the physical address city
                            int.TryParse(easiworxRecord.PhysicalAddressPostalCode, out physicalAddressCode); //Sets the physical address postal code

                            //Easiworx Postal Address

                            postalAddress1 = easiworxRecord.PostalAddressStreetNo;
                            postalAddress2 = easiworxRecord.PostalAddress;
                            postalAddress3 = easiworxRecord.PostalSuburb;
                            //postalAddress4 = easiworxRecord.PhysicalAddress4; //This will be the postal city
                            int.TryParse(easiworxRecord.PostalCode, out postalCode); //Sets the postal address postal code

                            //Easiworx Contact Details

                            //taxNo = easiworxRecord.TaxNo; //No tax number has been specified in easiworx csv
                            hometel = easiworxRecord.HomeTel.Replace("'", string.Empty);
                            worktel = easiworxRecord.OfficeTel.Replace("'", string.Empty);
                            cellno = easiworxRecord.CellNo.Replace("'", string.Empty);
                            //faxno = easiworxRecord.FaxNumber; //No fax number has been specified in easiworx csv
                            email = easiworxRecord.EmailAddress;


                            //Easiworx Banking Details

                            //Match Bank name

                            string bnkName = easiworxRecord.BankName.ToLower();

                            if (bnkName.Contains("absa"))
                            {
                                bankName = "Absa";
                            }
                            else if (bnkName.Contains("albaraka"))
                            {
                                bankName = "Albaraka";
                            }
                            else if (bnkName.Contains("capitec"))
                            {
                                bankName = "Capitec";
                            }
                            else if (bnkName.Contains("fnb")||bnkName.Contains("first national bank"))
                            {
                                bankName = "FNB";
                            }
                            else if (bnkName.Contains("nedbank"))
                            {
                                bankName = "Nedbank";
                            }
                            else if (bnkName.Contains("standard"))
                            {
                                bankName = "Standard Bank";
                            }
                            else if (bnkName.Contains("discovery"))
                            {
                                bankName = "Discovery Bank";
                            }
                            else if (bnkName.Contains("bidvest"))
                            {
                                bankName = "Bidvest";
                            }
                            else if (bnkName.Contains("tyme"))
                            {
                                bankName = "TymeBank";
                            }
                            else if (bnkName.Contains("mercantile"))
                            {
                                bankName = "Mercantile";
                            }
                            else if (bnkName.Contains("zero"))
                            {
                                bankName = "Bank Zero";
                            }
                            else if (bnkName.Contains("investec"))
                            {
                                bankName = "Investec";
                            }
                            
                            
                            branchName = easiworxRecord.BranchName;
                            branchCode = easiworxRecord.BranchCode;
                            bankAccNo = easiworxRecord.BankAccNo;
                            //accName = easiworxRecord.AccountName;

                            //Matching account types


                            string bnkAccType = easiworxRecord.BankAccType.ToLower();

                            if (bnkAccType.Contains("current"))
                            {
                                bankAccType = "Current";
                            }
                            else if (bnkAccType.Contains("savings"))
                            {
                                bankAccType = "Saving";
                            }
                            else
                            {
                                bankAccType = "Other";
                            }

                                

                            break;
                        default:
                            throw new ApplicationException("Invalid Lisp!");
                    }

                    if (!string.IsNullOrWhiteSpace(taxNo))
                    {
                        client.ClientDetails.TaxNumber = taxNo;
                    }

                    /*if (!string.IsNullOrWhiteSpace(dob))
                    {
                        if (DateTime.TryParseExact(dob, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtDob))
                        {
                            client.ClientDetails.DateOfBirth = dtDob;
                            Console.WriteLine("Here " + dtDob.ToString("dd MMM yyyy"));
                        }
                    }*/

                    /*
                    if ((ewx_dtDob != (new DateTime(0001, 1, 1))))
                    {
                        client.ClientDetails.DateOfBirth = ewx_dtDob;
                    }
                    if (!string.IsNullOrEmpty(physicalAddress1) || !string.IsNullOrEmpty(physicalAddress2) || !string.IsNullOrEmpty(physicalAddress3) || !(physicalAddressCode == 0))
                    {
                        client.ClientDetails.RecipientAddress = physicalAddress1 + " " +
                                                 physicalAddress2 + " " +
                                                 physicalAddress3 + " " +
                                                 physicalAddress4 + " " +
                                                 physicalAddress5 + " " +
                                                 physicalAddress6 + " " +
                                                 physicalAddressCode;
                    }
                    */
                    if (!string.IsNullOrWhiteSpace(taxNo))
                        client.ClientDetails.TaxNumber = taxNo;

                    if (!string.IsNullOrWhiteSpace(cellno))
                        client.ClientDetails.RecipientCell = cellno;
                    
                    if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrWhiteSpace(faxno) || !string.IsNullOrWhiteSpace(hometel) || !string.IsNullOrWhiteSpace(worktel) || !string.IsNullOrWhiteSpace(cellno))
                    {


                        client.ClientContacts.EMailAddr = email;
                        client.ClientContacts.FaxNo = faxno;
                        client.ClientContacts.HomeTel = hometel;
                        client.ClientContacts.BussTel = worktel;
                        client.ClientContacts.CellNo = cellno;
                        client.ClientContacts.UpdateBy = UpdateBy;
                    }
                    
                    
                    if (!string.IsNullOrWhiteSpace(physicalAddress1) || !string.IsNullOrWhiteSpace(physicalAddress2) || !string.IsNullOrWhiteSpace(physicalAddress3) || !(physicalAddressCode == 0))
                    {
                        client.PhysicalAddress.Line1 = physicalAddress1;
                        client.PhysicalAddress.Line2 = physicalAddress2;
                        client.PhysicalAddress.Line3 = physicalAddress3;
                        client.PhysicalAddress.Line4 = physicalAddress4 + " " +
                                   physicalAddress5 + " " +
                                   physicalAddress6;
                        client.PhysicalAddress.Code = physicalAddressCode;
                        client.PhysicalAddress.UpdateBy = UpdateBy;
                    }

                    if (!string.IsNullOrWhiteSpace(postalAddress1)|| !string.IsNullOrWhiteSpace(postalAddress2) || !string.IsNullOrWhiteSpace(postalAddress3) || !(postalCode==0))
                    {
                        client.PostalAddress.Line1 = postalAddress1;
                        client.PostalAddress.Line2 = postalAddress2;
                        client.PostalAddress.Line3 = postalAddress3;
                        client.PostalAddress.Line4 = postalAddress4 + " " +
                            postalAddress5 + " " +
                            postalAddress6;
                        client.PostalAddress.Code = postalCode;
                        client.PostalAddress.UpdateBy = UpdateBy;
                    }

                    
                    if (!string.IsNullOrWhiteSpace(bankName) || !string.IsNullOrWhiteSpace(branchName) || !string.IsNullOrWhiteSpace(branchCode) || !string.IsNullOrWhiteSpace(bankAccType) || !string.IsNullOrWhiteSpace(bankAccNo))
                    {
                        client.BankDetails.BnkName = bankName;
                        client.BankDetails.BrnchName = branchName;
                        client.BankDetails.BrnchCode = branchCode;
                        client.BankDetails.AcctNumber = bankAccNo;
                        client.BankDetails.AcctType = bankAccType;
                       //client.BankDetails.AcctName = accName;
                        
                       
                        
                    }


                }
            }
                catch (Exception)
                {
                    throw;
                }
            

            return client;
        }



        

        private void LoadFile(FileSettings fileSettings)
        {

            ApplicationException ex = new ApplicationException("Cannot access file");
            try
            {
                var filepath = fileSettings.FilePath;
                var csvHelperConfiguration = fileSettings.CsvConfiguration;

                //This try catch block is to detect if there is an exception when trying to open the CSV file
                try
                {
                    _detectedFileDelimiter = CsvFileHelper.DetectDelimiter(File.OpenText(filepath), csvHelperConfiguration.DetectDelimiterValues);
                }
                catch (IOException)
                {
                    throw new IOException("Cannot Access File");
                }
                csvHelperConfiguration.Delimiter = _detectedFileDelimiter;


                if (_selectedLisp.ToLower().Contains("astute"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<AstuteRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("camissa"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<CamissaRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("allan"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<AllanGrayRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("nedgroup"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<NedgroupRecord>(filepath, csvHelperConfiguration);
                }
                if (_selectedLisp.ToLower().Contains("bullion"))
                {
                    _csvRecordList = CsvFileHelper.GetRecords<SABullionRecord>(filepath, csvHelperConfiguration);
                }
                if ((_selectedLisp.ToLower().Contains("momentum")) || (_selectedLisp.ToLower().Contains("mtab")))
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

                if (_selectedLisp.ToLower().Contains("easiworx"))
                {
                    var fileName = fileSettings.FilePath;
                    var lisp = "";

                    if (fileName.ToLower().Contains("allan"))
                        lisp = "Allan Gray";
                    if (fileName.ToLower().Contains("camissa"))
                        lisp = "Camissa";
                    if (fileName.ToLower().Contains("momentum"))
                        lisp = "Momentum";

                    _csvRecordList = CsvFileHelper.GetRecords<EasiworxRecord>(filepath, csvHelperConfiguration, lisp);

                }
                dgvFileContents.AutoGenerateColumns = false;

                if (_existingClientDetails != null && _existingClientDetails.Count > 0)
                {
                    _matchedClientsFromCsv = _csvRecordList.Where(csvList => _existingClientDetails.Any(ec => ec.IdentificationNo == csvList.IDNumber &&
                                                                                                        string.IsNullOrEmpty(ec.PassportNo) &&
                                                                                                        csvList.HasErrors == false &&
                                                                                                        ec.ClientId != 0))
                                                           .Concat(_csvRecordList.Where(csvList2 => _existingClientDetails.Any(ec2 => ec2.PassportNo == csvList2.PassportNo &&
                                                                                                                               string.IsNullOrEmpty(ec2.IdentificationNo) &&
                                                                                                                              csvList2.HasErrors == false &&
                                                                                                                               ec2.ClientId != 0))).ToList();
                }
            }
            catch (IOException) //Catches exception when the CSV file is being used by another program
            {
                MessageBox.Show("Please ensure that the CSV file is not being used by another application", "Cannot Access CSV File");
            }
            catch (ApplicationException) //Catches exception when CSV file has incorrect Delimiter
            {
                MessageBox.Show("Please ensure that the CSV file is delimited only using commas or tabs", "Invalid File Delimiter");
            }
            catch (CsvHelper.MissingFieldException mfEx) //Catches exception when CSV file contains incorrect columns
            {
                var msg = mfEx.ToString();
                MessageBox.Show(msg, "There is a problem with the CSV file");
            }
            catch (CsvHelper.BadDataException bdEx) //Catches exception when CSV file contains bad data
            {
                var msg = bdEx.ToString();
                MessageBox.Show(msg, "The CSV file contains bad data");
            }
            catch (Exception x) //Catches any further exceptions
            {
                MessageBox.Show(x.Message,"Invalid CSV File!");
            }
        }

        private async Task ImportClientInvestmentsFromFile(frmCsvImportProgressWindow frmCsvImportProgressWindow)
        {
            try
            {
                var totClients = _fileClientInvestments.Count;
                var clientKeys = _fileClientInvestments.Keys.ToList();

                ThreadPool.SetMinThreads(38, 38);

                var parallelOptions = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = -1,
                    CancellationToken = frmCsvImportProgressWindow.cancelTk
                };

                var recordImportProgress = new Progress<ClientInvestmentRecordImportAudit>();
                recordImportProgress.ProgressChanged += RecordImportProgress_ProgressChanged;

                if (clientKeys.Count >= _importBatchSize)
                {
                    var batchedClientInvestments = await Task.Run(() => MoreEnumerable.Batch(clientKeys, _importBatchSize));

                    foreach (var batchedClientInvestment in batchedClientInvestments)
                    {
                        
                            var loopResults = await ImportClientInvestmentsInParallel(parallelOptions, batchedClientInvestment, recordImportProgress, frmCsvImportProgressWindow);
                        
                    }
                    //frmCsvImportProgressWindow.End();
                }
                else
                {
                    _recCnt = 0;
                    var clientInvestmentRecordImportAudit = new ClientInvestmentRecordImportAudit();
                    var progressCallback = frmCsvImportProgressWindow;
                    clientInvestmentRecordImportAudit.SetProgressCallback(progressCallback);
                    try
                    {
                        foreach (var clientIdentificationNo in clientKeys)
                        {

                            //Check if cancel button has been clicked
                            if (parallelOptions.CancellationToken.IsCancellationRequested)
                            {
                                parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                                break;
                            }

                            _recCnt++;


                            clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Pending);

                            await ImportClientInvestments(clientIdentificationNo, _fileClientInvestments[clientIdentificationNo]);

                            _percCompleted = (int)Math.Round((double)(100 * _recCnt) / totClients);

                            clientInvestmentRecordImportAudit.SetPercentageCompleted(_percCompleted);
                            var message = "Records with identification number: " + clientIdentificationNo + " successfully imported!";
                            clientInvestmentRecordImportAudit.SetMessage(message);

                            ((IProgress<ClientInvestmentRecordImportAudit>)(recordImportProgress)).Report(clientInvestmentRecordImportAudit);

                            //await UpdateProgressBar(_pbImportFile, _percCompleted, _handle);

                            if (_percCompleted == 100)
                            {
                                _importCompleted = true;
                                clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Imported);
                                progressCallback.End();

                                //RecordCsvFileImport();

                                await Task.Run(() =>
                                {
                                    //var win32Parent = new NativeWindow();
                                    //win32Parent.AssignHandle(_handle);
                                    //MessageBox.Show(win32Parent, "Client Investment Portfolios successfully imported!", "Import Client Investments File", MessageBoxButtons.OK);
                                    ValidateDataGridRecords();
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
                    catch (OperationCanceledException ex)
                    {
                        clientInvestmentRecordImportAudit.SetImportStatus(Enums.ClientInvestmentRecordImportStatus.Error);
                        clientInvestmentRecordImportAudit.SetMessage(ex.Message);
                        _importCancelled = true;
                        _importCompleted = false;

                        //Close progress window
                        frmCsvImportProgressWindow.End();
                    }
                }
            }
            /*catch (OperationCanceledException)
            {
                await Task.Run(() =>
                {
                    var win32Parent = new NativeWindow();
                    win32Parent.AssignHandle(_handle);
                    MessageBox.Show(win32Parent, "Import operation has been cancelled!", "Easiworx Error", MessageBoxButtons.OK);
                });

            }*/
            catch (OperationAbortedException)
            {
                await Task.Run(() =>
                {
                    var win32Parent = new NativeWindow();
                    win32Parent.AssignHandle(_handle);
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
                    win32Parent.AssignHandle(_handle);
                    MessageBox.Show(win32Parent, ex.Message, "Easiworx Error", MessageBoxButtons.OK);
                });
            }
            catch (Exception ex)
            {
                Program.Logger.Error(ex);
                await Task.Run(() =>
                {
                    var win32Parent = new NativeWindow();
                    win32Parent.AssignHandle(_handle);
                    MessageBox.Show(win32Parent, ex.Message, "Easiworx Error", MessageBoxButtons.OK);
                });
            }
        }

        //private async Task UpdateProgressBar(ProgressBar progressBar, int value, IntPtr handle)
        //{
        //    try
        //    {
        //        if (progressBar.InvokeRequired)
        //        {
        //            progressBar.BeginInvoke(new Action(async delegate ()
        //            {
        //                await UpdateProgressBar(progressBar, value, handle);


        //            }));
        //        }
        //        else
        //        {
        //            progressBar.Value = value;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        private Retirement AddRetirementFunds(Retirement retirement, List<ICsvRecord> funds)
        {

            double fundAllocPerc = 0;
            string modelPortfolio = "";
            


            try
            {


                if (retirement.Funds == null)
                    retirement.Funds = new List<Fund>(1);

                Fund newfund = null;
                
                List<ICsvRecord> mpFunds = new List<ICsvRecord>(1); //List of all model portfolio funds assigned to this retirement record
               
                foreach (var fund in funds)
                {
                   
                    //if (fund.IDNumber == "9903145082083")
                    //    Debugger.Break();

                    //switch (fund.LISP.ToLower())
                    switch (_selectedLisp.ToLower())
                    {
                        case "allan gray":
                        case "allangray":
                            //Console.WriteLine("Check this one: " + ((AllanGrayRecord)fund).FundAllocationPercentage);
                           // Double.TryParse(((AllanGrayRecord)fund).FundAllocationPercentage, out fundAllocPerc);
                            //Console.WriteLine(fundAllocPerc);
                            fundAllocPerc = ((AllanGrayRecord)fund).AccountFundAllocation.AsDouble();
                            break;
                        case "easiworx":
                        case "easiworxtemplate":
                           // Double.TryParse(((EasiworxRecord)fund).AccountFundAllocation, out fundAllocPerc);
                            fundAllocPerc = ((EasiworxRecord)fund).AccountFundAllocation.AsDouble();
                            modelPortfolio = ((EasiworxRecord)fund).ModelPortfolio;
                            break;
                        case "momentum":
                           // Double.TryParse(((MomentumRecord)fund).FundPerc, out fundAllocPerc);
                            fundAllocPerc = ((MomentumRecord)fund).FundPerc.AsDouble();
                            break;
                        case "mtab":
                           // Double.TryParse(((MomentumRecord_TabDelimited)fund).FundPerc, out fundAllocPerc);
                            fundAllocPerc = ((MomentumRecord_TabDelimited)fund).AccountFundAllocation.AsDouble();
                            break;
                        case "astutetemplate":
                            // Double.TryParse(((MomentumRecord_TabDelimited)fund).FundPerc, out fundAllocPerc);
                            fundAllocPerc = ((AstuteRecord)fund).AccountFundAllocation.AsDouble();
                            break;
                    }

                    //If this fund belongs to a model portfolio, then add it to the list and skip this loop iteration
                    if (!string.IsNullOrEmpty(modelPortfolio))
                    {
                        mpFunds.Add(fund);
                        continue;
                    }


                    if (retirement.Funds.Count > 0)
                    {
                        //Check if fund codes are the same indicating that the fund is already present in the list
                        var existingFund = retirement.Funds.Where(f => (f.FundCode.Trim().ToLower() == fund.FundCode.Trim().ToLower() && fund.FundCode!= "N/A" && !(string.IsNullOrWhiteSpace(fund.FundCode))) || (f.Description.Trim().ToLower() == fund.FundName.Trim().ToLower())).FirstOrDefault();
                        if (existingFund == null)
                        {
                            newfund = CreateFund(fund, fundAllocPerc);
                            var retirementFunds = retirement.Funds;
                            retirementFunds.Add(newfund);
                            retirement.Funds = retirementFunds;
                            retirement.MonthlyContribution += newfund.PolicyPremium;
                        }
                        
                        else
                        {
                            //check if new fund value & split perc is diff to original & if so add as new fund else update exist fund
                            //Double.TryParse(fund.FundValue, out double newFundValue);

                            double newFundValue = fund.FundValue.AsDouble();
                            DateTime.TryParse(fund.FundValueDate, out DateTime newFundValDate);

                            if (newFundValue != existingFund.CurrentAmount && newFundValDate == existingFund.FundValueDate && fundAllocPerc != 0 && fundAllocPerc != existingFund.SplitPerc)
                            {
                                newfund = CreateFund(fund, fundAllocPerc);
                                var retirementFunds = retirement.Funds;
                                retirementFunds.Add(newfund);
                                retirement.Funds = retirementFunds;
                                retirement.MonthlyContribution += newfund.PolicyPremium;
                            }
                            else
                                UpdateFund(existingFund, fund, fundAllocPerc);
                        }
                    }
                    else
                    {
                        newfund = CreateFund(fund, fundAllocPerc);
                        var retirementFunds = retirement.Funds;
                        retirementFunds.Add(newfund);
                        retirement.Funds = retirementFunds;
                        retirement.MonthlyContribution += newfund.PolicyPremium;
                    }
                    newfund = null;
                }
                
                if (mpFunds.Count > 0)
                {
                    string mpName = "";
                    double mpFundValue=0;
                    double mpSplitPerc = 100;
                    double mpPolicyPremium = 0;

                    DateTime mpFundValDate= new DateTime(0001, 1, 1);
                    DateTime mpStartDate = new DateTime(0001, 1, 1);

                    //Add together the values of the model portfolio
                    foreach (var fund in mpFunds)
                    {
                        EasiworxRecord esFund = ((EasiworxRecord)fund);
                        mpName = esFund.ModelPortfolio;
                        mpFundValue += esFund.FundValue.AsDouble();
                        //mpSplitPerc += esFund.AccountFundAllocation.AsDouble();
                        mpPolicyPremium += esFund.MonthlyPremium.AsDouble();

                        if((mpFundValDate == new DateTime(0001, 1, 1)) || (mpStartDate == new DateTime(0001, 1, 1)))
                        DateTime.TryParse(esFund.FundValueDate, out mpFundValDate);
                        DateTime.TryParse(esFund.InceptionDate, out mpStartDate);
                    }

                    //Create new fund object for the model portfolio
                    var modelPortfolioFund = new Fund()
                    {
                        FundCode = "N/A",
                        Description = mpName,
                        CreateDate = DateTime.Now,
                        CurrentAmount = mpFundValue,
                        SplitPerc = Math.Round(mpSplitPerc, 2, MidpointRounding.AwayFromZero),
                        PolicyPremium = mpPolicyPremium,
                        UpdateBy = "System",
                        UpdateDate = DateTime.Now

                    };

                    if (mpStartDate != new DateTime(0001, 1, 1))
                    {
                        modelPortfolioFund.StartDate = mpStartDate;
                    }
                    if (mpFundValDate != new DateTime(0001, 1, 1))
                    {
                        modelPortfolioFund.FundValueDate = mpFundValDate;
                    }




                    //Add model portfolio funds to retirement portfolio
                 
                    
                        if (retirement.Funds.Count > 0)
                        {
                        //Check if model portfolio names are the same indicating that the fund is already present in the list
                            var existingFund = retirement.Funds.Where(f => f.Description.Trim().ToLower() == modelPortfolioFund.Description.Trim().ToLower()).FirstOrDefault();
                            if (existingFund == null)
                            {
                                var retirementFunds = retirement.Funds;
                                retirementFunds.Add(modelPortfolioFund);
                                retirement.Funds = retirementFunds;
                                retirement.MonthlyContribution += modelPortfolioFund.PolicyPremium;
                            }

                            else
                            {
                                //check if new fund value & split perc is diff to original & if so add as new fund else update exist fund
                                //Double.TryParse(fund.FundValue, out double newFundValue);

                                double newFundValue = modelPortfolioFund.CurrentAmount;
                                //DateTime.TryParse(modelPortfolioFund.FundValueDate, out DateTime newFundValDate);
                                DateTime newFundValDate = modelPortfolioFund.FundValueDate;

                                if (newFundValue != existingFund.CurrentAmount && newFundValDate == existingFund.FundValueDate && modelPortfolioFund.SplitPerc != 0 && modelPortfolioFund.SplitPerc != existingFund.SplitPerc)
                                {
                                    var retirementFunds = retirement.Funds;
                                    retirementFunds.Add(modelPortfolioFund);
                                    retirement.Funds = retirementFunds;
                                    retirement.MonthlyContribution += modelPortfolioFund.PolicyPremium;
                                }
                                else
                                {

                                    existingFund.FundCode = modelPortfolioFund.FundCode;
                                    existingFund.Description = modelPortfolioFund.Description;
                                    existingFund.CreateDate = modelPortfolioFund.CreateDate;
                                    existingFund.CurrentAmount = modelPortfolioFund.CurrentAmount;
                                    existingFund.SplitPerc = modelPortfolioFund.SplitPerc;
                                    existingFund.PolicyPremium = modelPortfolioFund.PolicyPremium;
                                    existingFund.UpdateBy = "System";
                                    existingFund.UpdateDate = DateTime.Now;

                                }
                            }
                        }
                        else
                        {
                            var retirementFunds = retirement.Funds;
                            retirementFunds.Add(modelPortfolioFund);
                            retirement.Funds = retirementFunds;
                            retirement.MonthlyContribution += modelPortfolioFund.PolicyPremium;
                            //Console.WriteLine("'" + modelPortfolioFund.Description + "'");
                            
                        }
                    
                    //End
                     /*var retirementFunds = retirement.Funds;
                    retirementFunds.Add(modelPortfolioFund);
                    retirement.Funds = retirementFunds;
                    retirement.MonthlyContribution += modelPortfolioFund.PolicyPremium;*/

                }
                retirement.Calculate();
                

                return retirement;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private Retirement CreateRetirement(ICsvRecord csvRecord, string updateBy = "System", string RetirementType= "Error")
        {
            var insured = "";
            
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

            //switch (csvRecord.LISP.ToLower())
            switch (_selectedLisp.ToLower())
            {
                case "camissa":
                    insured = soughtClient is null ? ((CamissaRecord)csvRecord).Firstname + " " + ((CamissaRecord)csvRecord).Lastname : soughtClient.FirstName + " " + soughtClient.LastName;
                    
                    break;
                case "momentum":
                    insured = soughtClient is null ? ((MomentumRecord)csvRecord).Firstname : soughtClient.FirstName;
                    break;
                case "mtab":
                    insured = soughtClient is null ? ((MomentumRecord_TabDelimited)csvRecord).Lastname : soughtClient.LastName;
                    //Momentum doesnt give monthly premium
                    break;
                case "allangray":
                case "allan gray":
                    insured = soughtClient is null ? ((AllanGrayRecord)csvRecord).Firstname : soughtClient.FirstName;
                    
                    break;
                case "easiworx":
                case "easiworxtemplate":
                    insured = soughtClient is null ? ((EasiworxRecord)csvRecord).Firstname : soughtClient.FirstName;
                    
                    break;
                case "astutetemplate":
                    insured = soughtClient is null ? ((AstuteRecord)csvRecord).Firstname : soughtClient.FirstName;

                    break;

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
                    Status = "Implemented",
                    UpdateBy = updateBy
                };
                
            }
            
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        private Fund CreateFund(ICsvRecord csvRecord, double splitPercentage, string updateBy = "System")
        {
            /*var fundValue = csvRecord.FundValue.Replace(".", ",");
            Double.TryParse(fundValue, out double dblFundValue);*/

            var fundValue = csvRecord.FundValue.Replace(",", ""); //Removes comma if it exists, this is a potential area of contention

           // Double.TryParse(csvRecord.FundValue, out double dblFundValue);

            double dblFundValue = csvRecord.FundValue.AsDouble();

            DateTime.TryParse(csvRecord.FundValueDate, out DateTime fundValDate);
            DateTime fundStartDate = new DateTime(0001,1,1);

            //todo: check csvRecord Type & cast to appropriate type
            Double dblMonthlyPremium=0;

            //Initialise model portfolio for easiworx imports
            string modelPortfolio="";


            switch (_selectedLisp.ToUpper())
            {
                case "ALLANGRAY":
                    DateTime.TryParse(((AllanGrayRecord)csvRecord).StartDate, out fundStartDate);
                    //Double.TryParse(((AllanGrayRecord)csvRecord).MonthlyPremium, out dblMonthlyPremium);

                    dblMonthlyPremium = ((AllanGrayRecord)csvRecord).MonthlyPremium.AsDouble();

                    break;
                case "MOMENTUM":
                    DateTime.TryParse(((MomentumRecord)csvRecord).StartDate, out fundStartDate);
                    //Console.WriteLine("Wrong one");
                    //Double.TryParse(((MomentumRecord)csvRecord).MonthlyPremium, out dblMonthlyPremium); //Momentum CSV does not provide premiums
                    break;
                case "MTAB":
                    DateTime.TryParse(((MomentumRecord_TabDelimited)csvRecord).StartDate, out fundStartDate);
                   
                    //Double.TryParse(((MomentumRecord_TabDelimited)csvRecord).MonthlyPremium, out dblMonthlyPremium); //Momentum CSV does not provide premium
                    break;
                case "CAMISSA":
                    DateTime.TryParse(((CamissaRecord)csvRecord).InvestmentStartDate, out fundStartDate);
                    //Double.TryParse(((CamissaRecord)csvRecord).MonthlyPremium, out dblMonthlyPremium);

                    dblMonthlyPremium = ((CamissaRecord)csvRecord).MonthlyPremium.AsDouble();
                    break;
                case "EASIWORXTEMPLATE":
                case "EASIWORX":
                    DateTime.TryParse(((EasiworxRecord)csvRecord).InceptionDate, out fundStartDate);
                    //Double.TryParse(((EasiworxRecord)csvRecord).MonthlyPremium, out dblMonthlyPremium);

                    dblMonthlyPremium = ((EasiworxRecord)csvRecord).MonthlyPremium.AsDouble();
                    modelPortfolio = ((EasiworxRecord)csvRecord).ModelPortfolio;
                    break;
                case "ASTUTETEMPLATE":
                    DateTime.TryParse(((AstuteRecord)csvRecord).InceptionDate, out fundStartDate);
                    break;

            }


            //Program.Logger.Info("TT checking the funds details on Catherines machine Start");
            //Program.Logger.Info("From Csv Record: " + csvRecord.FundValue + ", After Parsing to double: " + dblFundValue.ToString());
            //Program.Logger.Info("Fund Alloc Perc: " + splitPercentage.ToString());
            //Program.Logger.Info("TT checking the funds details on Catherines machine End");


            var fund = new Fund()
            {
                FundCode = csvRecord.FundCode,
                Description = csvRecord.FundName,
                CreateDate = DateTime.Now,
                CurrentAmount = dblFundValue,
                SplitPerc = splitPercentage,
                PolicyPremium = dblMonthlyPremium,
                UpdateBy = updateBy,
                UpdateDate = fundValDate
                
            };
            //Console.WriteLine(fund.FundCode);
            //if (fundValDate != new DateTime(0001, 1, 1))
            //fund.UpdateDate = fundValDate;
            //Console.WriteLine(fund.UpdateDate);
            //Set model portfolio if the value is not empty
            if (!string.IsNullOrEmpty(modelPortfolio))
            {
                fund.ModelPortfolio = modelPortfolio;
            }

            //Set start dates and fund value dates if the values are not empty
            if (fundStartDate != new DateTime(0001,1,1))
            {
                fund.StartDate = fundStartDate;
            }
            if (fundValDate != new DateTime(0001, 1, 1))
            {
                fund.FundValueDate = fundValDate;
            }

            return fund;
        }

        


        [MethodImpl(MethodImplOptions.Synchronized)]
        private Fund UpdateFund(Fund fund, ICsvRecord csvRecord, double dblSplitPerc, string UpdateBy = "System" )
        {
            try
            {
                //if (csvRecord.IDNumber == "1102120396083")
                //Debugger.Break();
              //  var fundValue = csvRecord.FundValue.Replace(".", ",");

                var fundValue = csvRecord.FundValue.Replace(",", "");
                //var splitPerc = csvRecord.
                //Double.TryParse(fundValue, out double dblFundValue);

                double dblFundValue = fundValue.AsDouble();
                DateTime.TryParse(csvRecord.FundValueDate, out DateTime fundValDate);

                if (csvRecord.FundValueDate != null && fundValDate > fund.FundValueDate)
                {
                    fund.CurrentAmount = dblFundValue;
                    fund.SplitPerc = dblSplitPerc; 
                    fund.UpdateBy = UpdateBy;
                    fund.UpdateDate = DateTime.Now;
                    fund.FundValueDate = fundValDate;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fund;
        }
        private async Task ExportErrorRecordsToCsvFile()
        {
            string fileName = "";

            try
            {
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) +
                                                        "\\Easiworx\\ErrorFiles";
                fileName = filePath;

                if (_selectedLisp.ToLower().Contains("allan"))
                    fileName += "AllanGray_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                if (_selectedLisp.ToLower().Contains("astute"))
                    fileName += "Astute_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

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
                if (_selectedLisp.ToLower().Contains("mtab"))
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

                if (_selectedLisp.ToLower().Contains("easiworx"))
                    fileName += "Easiworx_ErrorFile_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";

                await GetErrorRecords();
                var errorRows = await GetErrorRowsByRowNo();

                if (errorRows == null || errorRows.Count == 0)
                    return;

                await Task.Run(() =>
                {

                    foreach (var errorRow in errorRows)
                    {
                        if (errorRow.Cells.Cast<DataGridViewCell>().Any(c => c.ErrorText != string.Empty))
                        {
                            var errorCells = errorRow.Cells.Cast<DataGridViewCell>().Where(c => c.ErrorText != string.Empty);
                            var validationErrors = "";

                            foreach (var errorCell in errorCells)
                            {
                                validationErrors += errorCell.ErrorText + " ";
                            }

                            var csvRecOut = _csvErrorRecords.Where(r => r.RowNo == int.Parse(errorRow.Cells["RowNo"].Value.ToString())).FirstOrDefault();
                            csvRecOut.ValidationErrors = validationErrors;

                        }
                    }

                    var csvHelperConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8, Delimiter = ",", HeaderValidated = null, TrimOptions = TrimOptions.Trim };

                    using (var textWriter = new StreamWriter(fileName, true, Encoding.UTF8) { AutoFlush = true })
                    {
                        using (CsvHelper.CsvWriter csvWriter = new CsvWriter(textWriter, csvHelperConfiguration))
                        {
                            csvWriter.WriteRecords(_csvErrorRecords);
                            csvWriter.NextRecord();
                            csvWriter.WriteComment("Record Count: " + _csvErrorRecords.Count.ToString());
                        }
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                _errorFile = fileName;
            }
            
        }
        private async Task<List<DataGridViewRow>> GetErrorRowsByRowNo()
        {
            return await Task.Run(()=> dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => _csvErrorRecords.Any(o => o.RowNo.ToString() == r.Cells[0].Value.ToString())).ToList());
        }
        private void ValidateDataGridRecords()
        {
            try
            {
                var totRowCnt = dgvFileContents.RowCount + 1;
                //var tokenSource = new CancellationTokenSource();
                //var cancellationToken = tokenSource.Token;
                //var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = -1, CancellationToken = cancellationToken };
                
                //Console.WriteLine($"Before ParrallelFor {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");
                //await Task.Run(() =>
                //{

                //Parallel.For(_dgvFileContentsRowCnt = 0, totRowCnt, parallelOptions,rowIndex =>
                for (var rowIndex = 0; rowIndex < dgvFileContents.RowCount; rowIndex++)
                {
                    //DataGridViewRow datagridViewRow = null;
                    try
                    {
                        //Console.WriteLine($"Before WaitAsync {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");

                        //await semaphore.WaitAsync();
                        //_semaphore.Wait();
                        var dgvFileContents_RowNo = rowIndex + 1;

                        //Console.WriteLine($"After WaitAsync {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");

                        //if (dgvFileContents_RowNo == totRowCnt)
                        //{
                        //    tokenSource.Cancel();
                        //    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        //}

                        //if (dgvFileContents.InvokeRequired)
                        //    dgvFileContents.BeginInvoke((Action)delegate
                        //    {
                        //        datagridViewRow = dgvFileContents.Rows[rowIndex];
                        //    });
                        //else
                        var datagridViewRow = dgvFileContents.Rows[rowIndex];
                        var csvRecord = GetCsvRecordByRowIndex(dgvFileContents_RowNo);

                        ValidateSAIDNo(rowIndex, csvRecord);
                        ValidateFundValue(rowIndex, csvRecord);
                        ValidateFundValueDate(rowIndex, csvRecord);
                        ValidateBirthDate(rowIndex, csvRecord);
                        ColourRow(rowIndex);

                        /*await ValidateSAIDNo(datagridViewRow, csvRecord);
                        await ValidateFundValue(datagridViewRow, csvRecord);
                        await ValidateFundValueDate(datagridViewRow, csvRecord);
                        await ColourRow(datagridViewRow);*/

                        //if (csvRecord.HasErrors)
                          //  Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}. Record with index {dgvFileContents_RowNo} has validation errors!");

                    }
                    catch (OperationCanceledException)
                    {
                    }
                    //finally
                    //{
                    //    _semaphore.Release();
                    //}

                    //});
                    //});
                }
            }
            catch (Exception)
            {
            }
            //finally
            //{
            //    //_semaphore.Release();
            //    //Console.WriteLine("ErrorRecCount: " + _csvRecordList.Where(l => l.HasErrors == true).Count());
            //}
        }
        private ICsvRecord GetCsvRecordByRowIndex(int rowIndex)
        {
            return _csvRecordList.Where(r => r.RowNo == rowIndex).FirstOrDefault();
        }
        private void ColourRow(int RowIndex)
        {
            DataGridViewCell idNoCell = null;

            //Console.WriteLine($"ColourRow {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");

            if (dgvFileContents.InvokeRequired == true)
                dgvFileContents.BeginInvoke((Action)delegate
                {
                    idNoCell = dgvFileContents.Rows[RowIndex].Cells["IDNumber"];
                    var idno = idNoCell.Value.ToString();

                    if (_matchedClientsFromCsv != null && _matchedClientsFromCsv.Count > 0)
                    {
                        if (_matchedClientsFromCsv.Any(r => r.IDNumber == idno))
                        {
                            if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                            {
                                dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                                dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                            }
                        }
                        else
                        {
                            if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                            {
                                dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Chartreuse;
                                dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                            }
                        }
                    }
                    else
                    {
                        if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                        {
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Chartreuse;
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                        }
                    }
                });
            else
            {
                idNoCell = dgvFileContents.Rows[RowIndex].Cells["IDNumber"];
                var idno = idNoCell.Value.ToString();

                if (_matchedClientsFromCsv != null && _matchedClientsFromCsv.Count > 0)
                {
                    if (_matchedClientsFromCsv.Any(r => r.IDNumber == idno))
                    {
                        if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                        {
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                        }
                    }
                    else
                    {
                        if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                        {
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Chartreuse;
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                        }
                    }
                }
                else
                {
                    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                    {
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Chartreuse;
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                    }
                }
            }


        }
        private void ValidateSAIDNo(int RowIndex, ICsvRecord csvRecord)
        {
            //Console.WriteLine($"ValidateSAIDNo {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");
            #region SAIDNoComposition
            /*
                YYMMDDGSSSCAZ

                YYMMDD : Date of birth (DOB)
                G : Gender. 0-4 Female; 5-9 Male
                SSS : Sequence No. for DOB/G combination
                C : Citizenship. 0 SA; 1 Other
                A : Usually 8, or 9 but can be other values
                Z : Calculated control (check) digit

                * */
            #endregion

            //await Task.Run(() =>
            //{
            var idno = "";
            var ppno = "";
            DataGridViewCell idNoCell = null;
            DataGridViewCell ppNoCell = null;




            if (dgvFileContents.InvokeRequired == true)
                dgvFileContents.BeginInvoke((Action)delegate
                {
                    ppNoCell = dgvFileContents.Rows[RowIndex].Cells["PassportNo"];
                    ppno = ppNoCell.Value.ToString();
                    idNoCell = dgvFileContents.Rows[RowIndex].Cells["IDNumber"];
                    idno = idNoCell.Value.ToString();

                    if (string.IsNullOrEmpty(idno) && !(string.IsNullOrEmpty(ppno)))
                    {
                        //Do nothing, there is a passport number instead of ID
                    }

                    else if (!string.IsNullOrEmpty(idno) && idno.Length < 13)
                    {
                        idNoCell.ErrorText = "Invalid ID No. Length < 13!";
                        idNoCell.ToolTipText = "Invalid ID No. Length < 13!";

                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255,46,46);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        
                        csvRecord.HasErrors = true;
                    }
                    else
                    {
                        //Validate ID number
                        SaIdValidator validator = new SaIdValidator();
                        if (!(validator.Validate(idno)))
                        {
                            idNoCell.ErrorText = "Invalid SA ID No!";
                            idNoCell.ToolTipText = "Invalid SA ID No!";

                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;
                        }
                    }
                });
            else
            {
                idNoCell = dgvFileContents.Rows[RowIndex].Cells["IDNumber"];
                idno = idNoCell.Value.ToString();

                ppNoCell = dgvFileContents.Rows[RowIndex].Cells["PassportNo"];
                if (ppNoCell.Value!= null)
                {
                    ppno = ppNoCell.Value.ToString();
                }

                if (string.IsNullOrEmpty(idno) && !(string.IsNullOrEmpty(ppno)))
                {
                    //Do nothing, there is a passport number instead of ID
                }

                else if (!string.IsNullOrEmpty(idno) && idno.Length < 13)
                {
                    idNoCell.ErrorText = "Invalid ID No. Length < 13!";
                    idNoCell.ToolTipText = "Invalid ID No. Length < 13!";

                    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                    dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    csvRecord.HasErrors = true;
                }
                else
                {
                    //Validate ID number
                    SaIdValidator validator = new SaIdValidator();
                    if (!(validator.Validate(idno)))
                    {
                        idNoCell.ErrorText = "Invalid SA ID No!";
                        idNoCell.ToolTipText = "Invalid SA ID No!";

                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }
                }
            }


            // });
        }
        private void ValidateFundValue(int RowIndex, ICsvRecord csvRecord)
        {
            //Console.WriteLine($"ValidateFundValue {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");
            DataGridViewCell fundValueCell = null;

            //await Task.Run(() =>
            //{
            if (dgvFileContents.InvokeRequired == true)
                dgvFileContents.BeginInvoke((Action)delegate
                {
                    fundValueCell = dgvFileContents.Rows[RowIndex].Cells["FundValue"];
                    var fundValue = fundValueCell.Value.ToString();

                    if (string.IsNullOrEmpty(fundValue))
                    {
                        fundValueCell.ErrorText = "Invalid Fund Value!";
                        fundValueCell.ToolTipText = "Invalid Fund Value!";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }
                    else
                    {
                       // _ = double.TryParse(fundValue, out double outFundValue);
                       double outFundValue = fundValue.AsDouble();
                        if (outFundValue <= 0)
                        {
                            fundValueCell.ErrorText = "Invalid Fund Value!";
                            fundValueCell.ToolTipText = "Invalid Fund Value!";
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;
                        }
                        else
                        {
                            fundValueCell.ErrorText = "";
                            fundValueCell.ToolTipText = "";
                        }
                    }
                });
            else
            {
                fundValueCell = dgvFileContents.Rows[RowIndex].Cells["FundValue"];
                var fundValue = fundValueCell.Value.ToString();

                if (string.IsNullOrEmpty(fundValue))
                {
                    fundValueCell.ErrorText = "Invalid Fund Value!";
                    fundValueCell.ToolTipText = "Invalid Fund Value!";
                    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                    dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    csvRecord.HasErrors = true;
                }
                else
                {
                   // _ = double.TryParse(fundValue, out double outFundValue);
                    double outFundValue = fundValue.AsDouble();
                    if (outFundValue <= 0)
                    {
                        fundValueCell.ErrorText = "Invalid Fund Value!";
                        fundValueCell.ToolTipText = "Invalid Fund Value!";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }
                    else
                    {
                        fundValueCell.ErrorText = "";
                        fundValueCell.ToolTipText = "";
                    }
                }
            }

            //});
        }



        private void ValidateBirthDate(int RowIndex, ICsvRecord csvRecord)
        {
            DataGridViewCell birthDateCell = null;
            var birthDate = "";


                if (dgvFileContents.InvokeRequired == true)
                    dgvFileContents.BeginInvoke((Action)delegate
                    {
                        birthDateCell = dgvFileContents.Rows[RowIndex].Cells["BirthDate"];
                        birthDate = birthDateCell.Value.ToString();

                        if (birthDate == "-")
                        {
                            birthDateCell.ErrorText = "Invalid Birthdate!";
                            birthDateCell.ToolTipText = "Please ensure that Birthdate is in the format: 'dd/mm/yyyy'";
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;
                        }


                        
                        
                    });
                else
                {
                    birthDateCell = dgvFileContents.Rows[RowIndex].Cells["BirthDate"];

                if (birthDateCell.Value != null)
                {
                    birthDate = birthDateCell.Value.ToString();
                }

                    if (birthDate == "-")
                    {
                        birthDateCell.ErrorText = "Invalid Birthdate!";
                        birthDateCell.ToolTipText = "Please ensure that Birthdate is in the format: 'dd/mm/yyyy'";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }   
                }
    }





        private void ValidateFundValueDate(int RowIndex, ICsvRecord csvRecord)
        {
            //Console.WriteLine($"ValidateFundValueDate {Thread.CurrentThread.ManagedThreadId} Backround Thread: {Thread.CurrentThread.IsBackground}");

            DataGridViewCell fundValueDateCell = null;
            var fundValueDate = "";


            //await Task.Run(() =>
            //{
            if (!_selectedLisp.Contains("nedgroup"))
            {
                if (dgvFileContents.InvokeRequired == true)
                    dgvFileContents.BeginInvoke((Action)delegate
                    {
                        fundValueDateCell = dgvFileContents.Rows[RowIndex].Cells["FundValueDate"];
                        fundValueDate = fundValueDateCell.Value.ToString();

                        if (fundValueDate == "-")
                        {
                            fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                            fundValueDateCell.ToolTipText = "Please ensure that Fund Value Date is in the correct format!";
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;
                        }

                        if (string.IsNullOrEmpty(fundValueDate))
                        {
                            fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                            fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;
                        }

                        if (DateTime.TryParse(fundValueDate, out _))
                        {
                            if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                            {
                                fundValueDateCell.ErrorText = string.Empty;
                                fundValueDateCell.ToolTipText = string.Empty;
                                dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                            fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            csvRecord.HasErrors = true;                        
                        }
                        //else if (!DateTime.TryParseExact(fundValueDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) )
                        //{
                        //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                        //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                        //    csvRecord.HasErrors = true;
                        //}
                        //else
                        //{
                        //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                        //    {
                        //        fundValueDateCell.ErrorText = string.Empty;
                        //        fundValueDateCell.ToolTipText = string.Empty;
                        //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                        //    }
                        //}
                        //if (fundValueDate.Contains("/") && fundValueDate.Length == 10 && !DateTime.TryParseExact(fundValueDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        //{
                        //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                        //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                        //    csvRecord.HasErrors = true;
                        //}
                        //else
                        //{
                        //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                        //    {
                        //        fundValueDateCell.ErrorText = string.Empty;
                        //        fundValueDateCell.ToolTipText = string.Empty;
                        //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                        //    }
                        //}
                        //if (fundValueDate.Contains("/") && fundValueDate.Length == 8 && !DateTime.TryParseExact(fundValueDate, "dd/mm/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        //{
                        //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                        //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                        //    csvRecord.HasErrors = true;
                        //}
                        //else
                        //{
                        //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                        //    {
                        //        fundValueDateCell.ErrorText = string.Empty;
                        //        fundValueDateCell.ToolTipText = string.Empty;
                        //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                        //    }
                        //}
                    });
                else
                {
                    fundValueDateCell = dgvFileContents.Rows[RowIndex].Cells["FundValueDate"];
                    fundValueDate = fundValueDateCell.Value.ToString();


                    if (fundValueDate == "-")
                    {
                        fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        fundValueDateCell.ToolTipText = "Please ensure that Fund Value Date is in the correct format!";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }

                    if (string.IsNullOrEmpty(fundValueDate))
                    {
                        fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }

                    if (DateTime.TryParse(fundValueDate, out _))
                    {
                        if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.FromArgb(230, 7, 7))
                        {
                            fundValueDateCell.ErrorText = string.Empty;
                            fundValueDateCell.ToolTipText = string.Empty;
                            dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                        fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 7, 7);
                        dgvFileContents.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        csvRecord.HasErrors = true;
                    }

                    //else if (!DateTime.TryParseExact(fundValueDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    //{
                    //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                    //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                    //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    //    csvRecord.HasErrors = true;
                    //}
                    //else
                    //{
                    //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                    //    {
                    //        fundValueDateCell.ErrorText = string.Empty;
                    //        fundValueDateCell.ToolTipText = string.Empty;
                    //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                    //    }
                    //}

                    //if (fundValueDate.Contains("/") && fundValueDate.Length == 10 && !DateTime.TryParseExact(fundValueDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    //{
                    //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                    //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                    //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    //    csvRecord.HasErrors = true;
                    //} 
                    //else 
                    //{ 
                    //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                    //    {
                    //        fundValueDateCell.ErrorText = string.Empty;
                    //        fundValueDateCell.ToolTipText = string.Empty;
                    //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                    //    }
                    //}

                    //if (fundValueDate.Contains("/") && fundValueDate.Length == 8 && !DateTime.TryParseExact(fundValueDate, "dd/mm/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    //{
                    //    fundValueDateCell.ErrorText = "Invalid Fund Value Date!";
                    //    fundValueDateCell.ToolTipText = "Invalid Fund Value Date!";
                    //    dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    //    csvRecord.HasErrors = true;
                    //}
                    //else
                    //{
                    //    if (dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor != Color.LightPink)
                    //    {
                    //        fundValueDateCell.ErrorText = string.Empty;
                    //        fundValueDateCell.ToolTipText = string.Empty;
                    //        dgvFileContents.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                    //    }
                    //}
                }
            }
            //});
        }
        private async Task<List<ClientDetails>> GetExistingClientDetails()
        {
            List<ClientDetails> existingClientDetails = null;
            try
            {
                if (Program.ClientDetailsService == null)
                    throw new ApplicationException("ClientDetails service is null!");

                existingClientDetails = await Task.Run(() => _existingClientDetails = Program.ClientDetailsService.List(null).ToList());
            }
            catch (Exception)
            {
                throw;
            }
            return existingClientDetails;
        }
        private async Task SetFileImportDetails()
        {
            

            await Task.Run(() =>
            {

                var totRecs = _csvRecordList.Count;
                var errCnt = _csvErrorRecords == null ? 0 : _csvErrorRecords.Count;


                _noOfExistingClients = _matchedClientsFromCsv.Count;
                _noOfNewClients = totRecs - _noOfExistingClients;
                
                

                if (InvokeRequired)
                    BeginInvoke(new Action(() =>
                    {
                        lblRecCnt.Text = totRecs.ToString();

                        //Set onscreen error count
                        if (!chkViewErrorRecords.Checked && !chkViewNewRecords.Checked && !chkViewExistingRecords.Checked)
                        {
                            lblTotValErrors.Text = dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => r.DefaultCellStyle.BackColor == Color.FromArgb(230, 7, 7)).ToList().Count.ToString(); ;
                        }
                        
                        lblExistingClientCnt.Text = _noOfExistingClients.ToString();

                        //Set onscreen new client count
                        if (!chkViewErrorRecords.Checked && !chkViewNewRecords.Checked && !chkViewExistingRecords.Checked)
                        {
                            lblNewClientCnt.Text = dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => r.DefaultCellStyle.BackColor == Color.Chartreuse).ToList().Count.ToString(); //_noOfNewClients.ToString();
                        }

                        lblExistingClientCnt.Visible = true;
                        lblNewClientCnt.Visible = true;
                        cmClientRecords.Enabled = true;

                        splitContainer1.Panel1.Visible = true;
                        splitContainer1.Panel2.Visible = true;
                    }));
                else
                {
                    lblRecCnt.Text = totRecs.ToString();

                    //Set onscreen new client count
                    if (!chkViewErrorRecords.Checked && !chkViewNewRecords.Checked && !chkViewExistingRecords.Checked)
                    {
                        lblTotValErrors.Text = dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => r.DefaultCellStyle.BackColor == Color.FromArgb(230, 7, 7)).ToList().Count.ToString();
                    }
                    lblExistingClientCnt.Text = _noOfExistingClients.ToString();

                    //Set onscreen new client count
                    if (!chkViewErrorRecords.Checked && !chkViewNewRecords.Checked && !chkViewExistingRecords.Checked)
                    {
                        lblNewClientCnt.Text = dgvFileContents.Rows.Cast<DataGridViewRow>().Where(r => r.DefaultCellStyle.BackColor == Color.Chartreuse).ToList().Count.ToString(); //_noOfNewClients.ToString();
                    }
                    lblExistingClientCnt.Visible = true;
                    lblNewClientCnt.Visible = true;
                    cmClientRecords.Enabled = true;

                    splitContainer1.Panel1.Visible = true;
                    splitContainer1.Panel2.Visible = true;

                }
            });
        }
        private async Task GetErrorRecords()
        {
            _csvErrorRecords = await Task.Run(() => _csvRecordList.Where(r => r.HasErrors).ToList());
        }
        private async Task GetMatchedEasiworxClientsFromCsv()
        {
            _matchedClientsFromCsv = await Task.Run(() => _csvRecordList.Where(csvList => _existingClientDetails.Any(ec => !string.IsNullOrEmpty(ec.IdentificationNo) &&
                                                          ec.IdentificationNo == csvList.IDNumber &&
                                                          string.IsNullOrEmpty(ec.PassportNo))).Concat(_csvRecordList.Where(csvList2 => _existingClientDetails.Any(ec2 => !string.IsNullOrEmpty(ec2.PassportNo) &&
                                                                                                       ec2.PassportNo == csvList2.PassportNo &&
                                                                                                       string.IsNullOrEmpty(ec2.IdentificationNo)))).ToList());
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
                case "mtab":
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



        //private void RecordCsvFileImport()
        //{
        //    //newly imported file name is same as an existing imported file but file contents are diff, go and change the file name so that we can add it to dict
        //    if (metroMdiMain.ImportedCsvFiles.ContainsKey(_selectedFilename))
        //        metroMdiMain.ImportedCsvFiles.Add(_selectedFilename + "_" + DateTime.Now.ToString("ddMMyyyy:hhmmss"), _selectedFileHash);
        //    else
        //        metroMdiMain.ImportedCsvFiles.Add(_selectedFilename, _selectedFileHash);
        //}
        #endregion

        private void lblNewClientCnt_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblSelectedFile_Click(object sender, EventArgs e)
        {

        }

        private void dgvFileContents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblExistingClientCnt_Click(object sender, EventArgs e)
        {

        }
    }
}

