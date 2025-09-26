using easiplan.app.Repository;
using easiplan.app.Services;
using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.estate.Services;
using easiplan.domain.Services;
using Finx.App.DBMigrations;
using Finx.App.Extensions;
using Finx.App.Forms;
using Finx.App.Models;
using Finx.App.Sms;
using Finx.App.UserControls;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using my.domain.lib.core;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Registry;
using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using za.co.easiworx.office365.net;
using za.co.easiworx.office365.net.models;

namespace Finx.App
{
	static class Program
    {
        #region Static models used in the Application

        /// <summary>
        /// The logged on User
        /// </summary>
        internal static User User;
        internal static UserService UserServices;
        internal static UserAuthenticationService UserAuthenticationService = new UserAuthenticationService();

        internal static Provider ServiceProviders;
        internal static IEnumerable<DocumentTemplate> DocumentTemplatesList;
        internal static ManualsTemplateService ManualsTemplateService;

        //Domain Aggregate Services
        internal static ClientService ClientService;
        internal static ClientDetailsService ClientDetailsService;
        public static ClientRetirementPortfolioService ClientRetirementPortfolioService;
        internal static ClientFnaRiskService ClientFnaRiskService;

        internal static Company Company;

        /// <summary>
        /// The licensing model
        /// </summary>
        internal static LicenseModel Licensing;

        /// <summary>
        /// MySQL ConnectionInfo
        /// </summary>
        internal static MySqlConnectionInfo ConnectionInfo;

        /// <summary>
        /// The domain repository
        /// </summary>
        internal static EasiworxRepository Repository;

        /// <summary>
        /// static data repository
        /// </summary>
        internal static ListDataRepository listData = new ListDataRepository();

        /// <summary>
        /// Global application error logging
        /// </summary>
        internal static ILog Logger;

        /// <summary>
        /// SMS Configuraton
        /// </summary>
        internal static SmsConfiguration smsConfiguration;

        /// <summary>
        /// Proxy for Office365 Integration - ENHANCED
        /// Now managed by OutlookAuthenticationService but kept for backward compatibility
        /// </summary> 
        internal static OutlookProxy OutlookProxy
        {
            get { return easiplan.app.Services.OutlookAuthenticationService.OutlookProxy; }
            set { /* Managed by OutlookAuthenticationService */ }
        }

        /// <summary>
        /// Track Outlook authentication status - ENHANCED
        /// </summary>
        internal static bool IsOutlookAuthenticated
        {
            get { return easiplan.app.Services.OutlookAuthenticationService.IsAuthenticated; }
            set { /* Managed by OutlookAuthenticationService */ }
        }

        /// <summary>
        /// Current Outlook user email - ENHANCED
        /// </summary>
        internal static string OutlookUserEmail
        {
            get { return easiplan.app.Services.OutlookAuthenticationService.UserEmail; }
            set { /* Managed by OutlookAuthenticationService */ }
        }

        /// <summary>
        /// Estate and Risk Planning Domain Aggregate Services        
        /// </summary>       
        internal static EstateAnalysisService EstateAnalysisService;

        /// <summary>
        /// Initialize Outlook integration after user login
        /// </summary>
        public static async Task<bool> InitializeOutlookIntegration()
        {
            try
            {
                if (OutlookProxy == null)
                {
                    Program.Logger.Info("Initializing Outlook integration...");

                    // Create the proxy with authentication
                    OutlookProxy = new OutlookProxy(new AuthenticationModel());

                    // Test the connection
                    var profile = await OutlookProxy.GetMyProfile();

                    if (profile != null)
                    {
                        Program.Logger.Info($"Outlook integration successful for: {profile.DisplayName}");
                        return true;
                    }
                }
                return OutlookProxy != null;
            }
            catch (Exception ex)
            {
                Program.Logger.Error("Failed to initialize Outlook integration", ex);
                OutlookProxy = null;
                return false;
            }
        }

        /// <summary>
        /// Clean up Outlook resources on application exit
        /// </summary>
        public static void CleanupOutlookIntegration()
        {
            try
            {
                if (OutlookProxy != null)
                {
                    OutlookProxy.Dispose();
                    OutlookProxy = null;
                    Program.Logger.Info("Outlook integration cleaned up");
                }
            }
            catch (Exception ex)
            {
                Program.Logger.Error("Error cleaning up Outlook integration", ex);
            }
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                //Set Culture Info
                SetCultureInfo();

                //Initialise logger configuration
                ConfigureLogger(null);

                //Ensure only a single instance of the application is running
                using (new SingleAppMutexControl(Global.AppGuid))
                {

                    //Initialise Application
                    MetroProgressWindow progress = new MetroProgressWindow();
                    progress.SetCaption("Initialising Application ... Please wait. ");
                    //run in a separate thread
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Initialise), progress);
                    progress.ShowDialog();
                    progress.Close();

                    //Initialise Database
                    MetroProgressWindow progress2 = new MetroProgressWindow();
                    progress2.SetCaption("Initialising Database ... Please wait.");
                    //run in a separate thread
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ConfigureDatabase), progress2);
                    progress2.ShowDialog();
                    progress2.Close();


                    //Check Repository initiation successful
                   
                    if (Program.Repository.IsInError)
                        Application.Exit();
                    else
                    {
                        if (Program.Repository.IsConfigured)
                        {
                            if (Program.Repository.IsOutOfDate)
                            {
                                frmMetroUpdateDatabase frmUpdateDatabase = new frmMetroUpdateDatabase();
                                frmUpdateDatabase.ShowDialog();
                                frmUpdateDatabase.Close();

                            }

                            //TO DO : Use a DI container to inject services
                            Program.ClientService = new ClientService(Program.Repository);
                            Program.ClientDetailsService = new ClientDetailsService(Program.Repository,Program.User);
                            Program.ClientRetirementPortfolioService = new ClientRetirementPortfolioService(Program.Repository);
                            Program.ClientFnaRiskService = new ClientFnaRiskService(Program.Repository);

                            if (Program.UserServices == null)
                                Program.UserServices = new UserService(Program.Repository);

                            Program.EstateAnalysisService = new EstateAnalysisService(Program.Repository);
                        }
                        Program.Logger.Info("Initialisation completed. Opening Logon form with Outlook integration.");

                        // Show login form with integrated Outlook authentication  
                        if (ShowLoginWithOutlookIntegration())
                        {
                            // Login successful, continue with application
                            Program.Logger.Info("Login completed successfully");

                            // THIS IS CRITICAL - Start the main application message loop
                            // The main form is created and shown inside the login form
                            Application.Run();
                        }
                        else
                        {
                            // Login failed or cancelled
                            Program.Logger.Info("Login cancelled or failed");
                            Application.Exit();
                        }

                    }

                }

            }
            catch (System.TimeoutException)
            {
                //MessageBoxExt.ShowWarning("This Application has already been started.");

            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowException(ex, "There was a fatal error in this application. Please report the error below to the vendor.");
            }
            finally
            {
                //TO DO - Clean up resources
                GC.Collect();
            }
        }

        /// <summary>
        /// Set culture specific configuration
        /// </summary>
        private static void SetCultureInfo()
        {
            //Set Current Culture
            CultureInfo CInfo = new CultureInfo("en-ZA", true);
            CInfo.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
            CInfo.DateTimeFormat.LongDatePattern = "dd-MMM-yyyy hh:mm:ss";
            CInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            CInfo.NumberFormat.CurrencyDecimalDigits = 2;
            CInfo.NumberFormat.NaNSymbol = "0";
            CInfo.NumberFormat.NumberDecimalSeparator = ".";
            CInfo.NumberFormat.PercentDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = CInfo;
            Thread.CurrentThread.CurrentUICulture = CInfo;
        }



        /// <summary>
        /// Set Services providers
        /// </summary>
        public static void SetServiceProviders() {

            if (Program.Repository.IsConfigured)
            {
                ServiceProviders = (Provider)Program.Repository.List<Provider, int>(null).FirstOrDefault();
            }
        }

        /// <summary>
        /// Set document Templates List
        /// </summary>
        public static void SetDocumentTemplatesList() {
            if (Program.Repository.IsConfigured)
            {
                DocumentTemplatesList = Program.Repository.List<DocumentTemplate, int>(x => x.Status == "True").ToList();

                ManualsTemplateService = new ManualsTemplateService(Program.Repository);
            }
        }

        /// <summary>
        /// Log4Net Configuration
        /// </summary>
        private static void ConfigureLogger(object status)
        {
           // IProgressCallback callback = status as IProgressCallback;
            try
            {
                //configure logget from app.config file
                //  log4net.Config.BasicConfigurator.Configure();

               // callback.SetText(string.Format("Setting log path {0} ...", Environment.GetFolderPath(Environment.SpecialFolder.Personal)));

                DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                if (!dir.Exists)
                    dir.Create();

                var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "EasiworxLog.txt");

                //Configure logger in code
                Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                // Remove any other appenders
                hierarchy.Root.RemoveAllAppenders();
                // define some basic settings for the root
                Logger rootLogger = hierarchy.Root;
                rootLogger.Level = Level.Debug;

                // declare a RollingFileAppender for NHibernate logging with 5MB per file and max. 10 files
                RollingFileAppender appenderNH = new RollingFileAppender();
                appenderNH.Name = "RollingLogFileAppenderNHibernate";
                appenderNH.AppendToFile = true;
                appenderNH.MaximumFileSize = "5MB";
                appenderNH.MaxSizeRollBackups = 2;
                appenderNH.RollingStyle = RollingFileAppender.RollingMode.Size;
                appenderNH.StaticLogFileName = true;
                appenderNH.LockingModel = new FileAppender.MinimalLock();
                appenderNH.File = outputPath;

                appenderNH.Layout = new PatternLayout("%date - %message%newline"); //new XmlLayout(true);//
                                                                                   // this activates the FileAppender (without it, nothing would be written)
                appenderNH.ActivateOptions();

                // This is required, so that we can access the Logger by using 
                // LogManager.GetLogger("NHibernate") and it can used by NHibernate
                Logger loggerNH = hierarchy.GetLogger("NHibernate") as Logger;
                loggerNH.Level = Level.Error;
                loggerNH.AddAppender(appenderNH);

                // This is required, so that we can access the Logger by using 
                // LogManager.GetLogger("NHibernate.SQL") and it can used by NHibernate
                Logger loggerNHSql = hierarchy.GetLogger("NHibernate.SQL") as Logger;
                loggerNHSql.Level = Level.Error;
                //loggerNHSql.Level = Level.All;
                loggerNHSql.AddAppender(appenderNH);

                // declare RollingFileAppender for EasiworksWorks logging with 5MB per file and max. 10 files
                RollingFileAppender appenderMain = new RollingFileAppender();
                appenderMain.Name = "RollingLogFileAppenderMyProgram";
                appenderMain.AppendToFile = true;
                appenderMain.MaximumFileSize = "5MB";
                appenderMain.MaxSizeRollBackups = 10;
                appenderMain.RollingStyle = RollingFileAppender.RollingMode.Size;
                appenderMain.StaticLogFileName = true;
                appenderMain.LockingModel = new FileAppender.MinimalLock();

                appenderMain.File = outputPath;
                appenderMain.Layout = new PatternLayout("%date [%thread] %-5level %logger [%ndc] - %message%newline");//new XmlLayout(true);//

                // this activates the FileAppender (without it, nothing would be written)
                appenderMain.ActivateOptions();

                // This is required, so that we can access the Logger by using 
                // LogManager.GetLogger("Finx") 
                Logger logger = hierarchy.GetLogger("Finx") as Logger;
                logger.Level = Level.Debug;
                logger.AddAppender(appenderMain);

                // this is required to tell log4net that we're done 
                // with the configuration, so the logging can start
                hierarchy.Configured = true;

                Program.Logger = LogManager.GetLogger("Finx");
            }
            catch (Exception x)
            {
               // callback.SetText(string.Format("Configure Logger Error ...{0}", x.Message));
            }

        }

        /// <summary>
        /// Shows the log file to the user
        /// </summary>
        public static void ShowLog()
        {
            //Log the users system information for debug purposes
            Program.Logger.Info(SystemInfo.SystemInformation(string.Empty));

            var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "EasiworxLog.txt");

            //Launch doc in associated app
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = true;
            startInfo.FileName = outputPath;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            Process.Start(startInfo);
        }

        /// <summary>
        /// Checks for application updates
        /// </summary>
        /// <param name="status"></param>       
        private static void CheckUpdates(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            try
            {
                callback.Begin(0, 100);

                //if (callback.IsAborting)
                //      return;

                Global.CurrentAppVersion = Application.ProductVersion.Replace(".", "");

                // Check if the application was deployed via ClickOnce.
                if (!ApplicationDeployment.IsNetworkDeployed)
                {
                    callback.SetText("This application cannot be updated as it is not deployed via ClickOnce.");
                    //Thread.Sleep(10000);
                    return;
                }

                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;

                UpdateCheckInfo info = null;
                try
                {
                    info = updateCheck.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    callback.SetText(string.Format("ERROR OCURRED:{0}", dde.Message));

                    Thread.Sleep(10000);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    callback.SetText(string.Format("ERROR OCURRED:{0}", ide.Message));

                    Thread.Sleep(10000);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    callback.SetText(string.Format("ERROR OCURRED:{0}",ioe.Message));

                    Thread.Sleep(10000);
                    return;
                }

                if (info.UpdateAvailable)
                    if (info.IsUpdateRequired)
                    {
                        updateCheck.Update();

                        callback.SetText("The application has been upgraded, and will now restart.");

                        Thread.Sleep(10000);

                        Application.Restart();
                    }
                    else
                    {
                        callback.SetText("Updates are available.");
                    }
                else
                    callback.SetText("No updates available.");


            


                //if (callback.IsAborting)
                //    return;


            }
            catch (System.Threading.ThreadAbortException)
            {
                // We want to exit gracefully here (if we're lucky)
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                // And here, if we can
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        /// <summary>
        /// Get Outlook connection status for display
        /// </summary>
        public static string GetOutlookStatus()
        {
            if (easiplan.app.Services.OutlookAuthenticationService.IsAuthenticated)
            {
                return $"Connected: {easiplan.app.Services.OutlookAuthenticationService.UserName} ({easiplan.app.Services.OutlookAuthenticationService.UserEmail})";
            }
            return "Not connected";
        }

        /// <summary>
        /// Quick method to check if Outlook integration is available
        /// </summary>
        public static bool IsOutlookIntegrationAvailable()
        {
            return easiplan.app.Services.OutlookAuthenticationService.IsAuthenticated;
        }

        /// <summary>
        /// Get a user-friendly status message for Outlook integration
        /// </summary>
        public static string GetOutlookIntegrationStatus()
        {
            if (easiplan.app.Services.OutlookAuthenticationService.IsAuthenticated)
            {
                return $"✅ Outlook connected as {easiplan.app.Services.OutlookAuthenticationService.UserName}";
            }
            return "❌ Outlook not connected";
        }

        /// <summary>
        /// Initialising the application
        /// </summary>
        /// <param name="status"></param>
        private static void Initialise(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            try
            {
                callback.Begin(0, 100);

                //if (callback.IsAborting)
                //      return;

                Global.CurrentAppVersion = Application.ProductVersion.Replace(".", "");

                callback.SetText("Set Current Version to " + Global.CurrentAppVersion);

                //Initialise static models
                callback.SetText(string.Format("Reading registry with key {0} ...", Global.RegistryKey));
                Program.User = new User() { Username = RegistryWrapper.ReadRegistry(Global.RegistryKey, "Username", "Admin") };

                //TO DO : Support other types of connections
                ConnectionTypes connType = (ConnectionTypes)Enum.Parse(typeof(ConnectionTypes), RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbType", "MySQL"), true);

                Program.ConnectionInfo = new MySqlConnectionInfo(connType)
                {
                    ServerName = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbServer"),
                    DbName = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbCatalog"),
                    DbSchema = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbSchema"),
                    PortNumber = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbPort"),
                    UserName = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbAdminUser").Decrypt(Global.RegistryKey),
                    UserPwd = RegistryWrapper.ReadRegistry(Global.RegistryKey, "DbAdminPwd").Decrypt(Global.RegistryKey),
                    AssemblyType = typeof(DomainServices)
                     
                    
                };

                Program.smsConfiguration = new SmsConfiguration()
                {
                    // baseRestUri = RegistryWrapper.ReadRegistry(Global.RegistryKey, "SmsUri"),
                    ClientKey = RegistryWrapper.ReadRegistry(Global.RegistryKey, "ClientKey"),
                    SecretKey = RegistryWrapper.ReadRegistry(Global.RegistryKey, "Secret").Decrypt(Global.RegistryKey),
                };

                callback.SetText(string.Format("Done Reading registry."));
                               
                //Check Licensing
                callback.SetText(string.Format("Checking Licensing model ..."));

                //Set licensing model
                Program.Licensing = new LicenseModel(typeof(LicenseModel)) { Status = "Active" };

               // Program.Licensing.Validate().Wait();

               //  Program.Logger.Info($"MachineKey: {Program.Licensing.MachineKey}");

                callback.SetText(string.Format("Done checking Licensing model ..."));

                //if (callback.IsAborting)
                //    return;


            }
            catch (System.Threading.ThreadAbortException)
            {
                // We want to exit gracefully here (if we're lucky)
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                // And here, if we can
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
                //MessageBoxExt.ShowException(x);
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        /// <summary>
        /// Show login form with integrated Outlook authentication
        /// </summary>
        /// <returns>True if login successful (both EasiWorx and Outlook handled)</returns>
        private static bool ShowLoginWithOutlookIntegration()
        {
            try
            {
                // Show the login form
                var loginForm = new frmMetroLogin();
                var result = loginForm.ShowDialog();

                loginForm.Dispose();

                // Return true if login was successful (DialogResult.OK)
                return result == DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logger?.Error("Error during login process", ex);
                MessageBoxExt.ShowException(ex, "Error during login");
                return false;
            }
        }

        /// <summary>
        /// Configure the database
        /// </summary>
        /// <param name="status"></param>
        public static void ConfigureDatabase(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            try
            {
                callback.Begin(0, 100);

                //if (callback.IsAborting)
                //    return;

                callback.SetText(string.Format("Connecting to database ..."));

                Program.Repository = new EasiworxRepository(ConnectionInfo) { UserName = Program.User.Username };                

                Program.Repository.PreLoadEvent += Repository_PreLoadEvent;
                Program.Repository.PostLoadEvent += Repository_PostLoadEvent;
                Program.Repository.PreUpdateEvent += Repository_PreUpdateEvent;
                Program.Repository.PreInsertEvent += Repository_PreInsertEvent;

                //Try Configure the repository
                try
                {

                    callback.SetText(string.Format("Configuring database ..."));
                    Program.Repository.Configure();

                    if (Program.Repository.IsConfigured)
                    {
                        try
                        {
                            // check the admin user for first time installation
                            User _user = Program.Repository.List<User, int>(x => x.Username == "Admin" && x.IsAdministrator == true).FirstOrDefault();
                            if (_user == null)
                                Program.Repository.Add<User, int>(new User() { Username = "Admin", Password = "admin", Designation = "administrator", IsAdministrator = true, IsActive = true });
                        }
                        catch(NHibernate.ADOException adoX)
                        {
                            // This is potentially a new database

                            ConfigureDatabase(true, true);

                            Program.Repository.Add<User, int>(new User() { Username = "Admin", Password = "admin", Designation = "administrator", IsAdministrator = true, IsActive = true });

                        }
                        catch (Exception x2)
                        {
                            // unknow error
                            Program.Logger.Error(x2);
                            throw;
                        }
                        //TO DO: Run any Migrations
                        callback.SetText(string.Format("Running database updates ..."));
                        DoDBMigrations();

                        //Check if the application and database is up-to-date
                        try
                        {
                            DbVersion dbVersion = Program.Repository.List<DbVersion, int>(null).FirstOrDefault();

                            //if (int.Parse(dbVersion.Version) > int.Parse(Global.CurrentAppVersion))
                            //    throw new WarningException();

                            //Do once off Identification/Passport No decryption - YJ 2022-01-01
                            if (int.Parse(dbVersion.Version)<1071)
                            {
                                var clientDetails = Program.Repository.List<ClientDetails,int>(null);
                                foreach (ClientDetails clientDetail in clientDetails)
                                {
                                    clientDetail.IdentificationNo = clientDetail.IdentificationNo.Decrypt("key1");
                                    clientDetail.PassportNo = clientDetail.PassportNo.Decrypt("key1");

                                    Program.Repository.Update<ClientDetails, int>(clientDetail);
                                }

                            }

                            if (int.Parse(dbVersion.Version) < int.Parse(Global.CurrentAppVersion))
                                throw new OutOfDateException(null);

                        }
                        catch (WarningException)
                        {
                            Repository.IsInError = true;
                            MessageBoxExt.ShowWarning("Your application is out of date... Please connect to the internet and restart the application to download the latest version.");
                            return;
                        }
                        catch (OutOfDateException)
                        {
                            callback.SetText("Your database is out of date... updating your database. Please wait ...");

                            ConfigureDatabase(false, true);
                        }
                        catch (Exception x2)
                        {
                            Program.Logger.Error(x2);
                            Repository.IsInError = true;
                            MessageBoxExt.ShowException(x2, "Error checking database version.");
                            return;
                        }
                    }
                }
                catch (ConnectionException cnxe)
                {
                    Program.Logger.Error(cnxe);
                    callback.SetText("Database is not configured or Connection failed.");
                    Repository.IsInError = false;
                    Repository.IsConfigured = false;
                    return;

                }
                catch (SchemaException cnxe)
                {
                    MessageBoxExt.ShowException(cnxe, "Error in database configuration.");
                    callback.SetText("A schema configuration error has occured. Please call your vendor with the log file !");
                    Repository.IsInError = true;    
                    return;
                }
              
                catch (Exception x)
                {
                    if (!Program.Repository.IsConfigured)
                    {
                        //The repository has not been configured - initial setup
                        callback.SetText("Your database has not been configured yet !");
                        return;
                    }
                }

                //if (callback.IsAborting)
                //    return;

            }
            catch (System.Threading.ThreadAbortException)
            {
                // We want to exit gracefully here (if we're lucky)
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                // And here, if we can
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }
        public static bool ConfigureDatabase(bool NewSchema, bool UpdateSchema)
        {
            try
            {

                Program.Repository.Configure(NewSchema, UpdateSchema);

                if (Program.Repository.IsConfigured)
                {
                    if (UpdateSchema)
                    {
                        //update the db version
                        DbVersion dbVersion = Program.Repository.List<DbVersion, int>(null).FirstOrDefault();
                        if (dbVersion == null)
                        {
                            dbVersion = new DbVersion();

                            dbVersion.Version = Global.CurrentAppVersion;
                            dbVersion.Username = Program.User.Username;

                            Program.Repository.Add<DbVersion, int>(dbVersion);
                        }
                        else
                        {
                            dbVersion.Version = Global.CurrentAppVersion;
                            dbVersion.Username = Program.User.Username;

                            Program.Repository.Update<DbVersion, int>(dbVersion);
                        }

                    }
                    return true;
                }

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

            return false;
        }

        #region Repository Events
        private static void Repository_PreInsertEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;

            if (entity.CreateDate == entity.MinDateTime)
                entity.CreateDate = DateTime.Now;

            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = Program.User.Username;
        }
        private static void Repository_PreUpdateEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;

            if (entity.IsModified)
            {
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = Program.User.Username;

            }

        }
        private static void Repository_PreLoadEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            try
            {
                BaseEntity<int> entity = (BaseEntity<int>)sender;
                entity.IsLoading = true;
            }
            catch (Exception x) { }
        }
        private static void Repository_PostLoadEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            try
            {
                BaseEntity<int> entity = (BaseEntity<int>)sender;

                entity.IsLoading = false;
            }
            catch (Exception x) { }

        }
        #endregion

        /// <summary>
        /// Perform Database Updates/Migrations
        /// </summary>
        private static void DoDBMigrations()
        {
            Program.Repository.DBMigrateUp(typeof(_2017JAN_400).Assembly);
            Program.Repository.DBMigrateUp(typeof(_2017JAN_401).Assembly);
            Program.Repository.DBMigrateUp(typeof(_2017JAN_402).Assembly);
            Program.Repository.DBMigrateUp(typeof(_2017APR_405).Assembly);
            Program.Repository.DBMigrateUp(typeof(_2017AUG_406).Assembly);
            Program.Repository.DBMigrateUp(typeof(_2017OCT_407).Assembly);
            //22-12-2021
            Program.Repository.DBMigrateUp(typeof(_20211222_408).Assembly);
            Program.Repository.DBMigrateUp(typeof(_20211222_409).Assembly);
            Program.Repository.DBMigrateUp(typeof(_20211222_410).Assembly);

            Program.Repository.DBMigrateUp(typeof(_20211222_501).Assembly);

            Program.Repository.DBMigrateUp(typeof(_20211222_502).Assembly);
            Program.Repository.DBMigrateUp(typeof(_20230417_505).Assembly);

        }  

        /// <summary>
        /// Creates the CSV from a generic list.
        /// </summary>;
        /// <typeparam name="T"></typeparam>;
        /// <param name="list">The list.</param>;
        /// <param name="csvNameWithExt">Name of CSV (w/ path) w/ file ext.</param>;
        public static void CreateCSVFromGenericList<T>(List<T> list, string csvCompletePath)
        {
            if (list == null || list.Count == 0) return;

            //get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            if (!Directory.Exists(Path.GetDirectoryName(csvCompletePath))) Directory.CreateDirectory(Path.GetDirectoryName(csvCompletePath));

            using (var sw = new StreamWriter(csvCompletePath))
            {
                //make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] props = o.GetType().GetProperties();

                //foreach of the properties in class above, write out properties
                //this is the header row
                sw.Write(string.Join(",", props.Select(d => d.Name).ToArray()) + newLine);

                //this acts as datarow
                foreach (T item in list)
                {
                    //this acts as datacolumn
                    var row = string.Join(",", props.Select(d => $"\"{item.GetType().GetProperty(d.Name).GetValue(item, null)?.ToString()}\"")
                                                            .ToArray());
                    sw.Write(row + newLine);

                }
            }
        }

        public static string ApplicationVersion()
        {
            Version v = new Version(Application.ProductVersion);
            if(ApplicationDeployment.IsNetworkDeployed)
            {
                v = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            return string.Format("     {0} {1}", Application.ProductName, v.ToString());
        }
    }

    /// <summary>
    /// An IDisposable class to simulate wait on a control click event
    /// </summary>
    public class AppWaitCursor : IDisposable
    {
        private readonly Control _eventControl;

        public AppWaitCursor(object eventSender = null)
        {
            _eventControl = eventSender as Control;

            if (_eventControl != null)
                _eventControl.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

        }

        public void Dispose()
        {
            if (_eventControl != null)
                _eventControl.Enabled = true;

            Cursor.Current = Cursors.Default;

            Application.DoEvents();
        }
    }
}

