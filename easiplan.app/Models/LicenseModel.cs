using easiplan.app.Services;
using easiplan.domain;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.License;
using my.domain.lib.core.Registry;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    [GuidAttribute("2de915e1-df71-3443-9f4d-32259c92ced2")]
    public class LicenseModel : BaseEntity<int>, IDisposable
    {
        readonly static LicenseIntegration _licenseIntegration = new LicenseIntegration();

        public LicenseKeyModel licenseKeyModel { get; internal set; } = new LicenseKeyModel();

        [Required(ErrorMessage = "MachineKey is Required")]
        public string MachineKey { get; internal set; }

        string _LicenseKey;
        [Required(ErrorMessage = "License is Required")]
        public string LicenseKey { get { return _LicenseKey; } set { _LicenseKey = value; InvokePropertyChanged("LicenseKey"); } }

        public string Type { get; internal set; }
        public string IssueDate { get; internal set; }
        public string ExpiryDate { get; internal set; }

        public LicenseModel(Type type)
        {
            MachineKey = MachineKeyGenerator.Value();
            LicenseKey = RegistryWrapper.ReadRegistry(Global.RegistryKey, "license");

            licenseKeyModel = new LicenseKeyModel() { Database = new Database() { ConnectionString = "-" } };
            licenseKeyModel.Type = RegistryWrapper.ReadRegistry(Global.RegistryKey, "type");
            licenseKeyModel.ExpiryDate = RegistryWrapper.ReadRegistry(Global.RegistryKey, "expireDt");
            licenseKeyModel.Database.Hosted = Boolean.Parse(RegistryWrapper.ReadRegistry(Global.RegistryKey, "isHosted","true"));
        }      

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Activates the License key
        /// </summary>
        /// <param name="licenseKey"></param>
        public async Task<bool> Activate()
        {
            try
            {
                if (string.IsNullOrEmpty(MachineKey))
                    throw new MyValidationException("Validation Error : Machine Key is Required.");

                if (string.IsNullOrEmpty(LicenseKey))
                    throw new MyValidationException("Validation Error : License Key is Required.");

                licenseKeyModel = _licenseIntegration.Activate(MachineKey, LicenseKey);

                return await CheckLicenseKeyModel();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                Program.Licensing = this;
            }

        }

        /// <summary>
        /// Validates the license key
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Validate()
        {
            try
            {
                if (string.IsNullOrEmpty(MachineKey))
                    throw new MyValidationException("Validation Error : Machine Key is Required.");

                if (string.IsNullOrEmpty(LicenseKey))
                    throw new MyValidationException("Validation Error : License Key is Required.");

                licenseKeyModel = _licenseIntegration.Validate(MachineKey, LicenseKey);
               
                return await CheckLicenseKeyModel();
            }
            catch(WarningException wx)
            {
                throw wx;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                Program.Licensing = this;
            }
        }

        private async Task<bool> CheckLicenseKeyModel()
        {
            this.Status = string.Empty;

            if (licenseKeyModel == null)
                throw new Exception("License key Verification Failed.");

            //TO DO : Check Expiry Dates
            //InstallDt = DateTime.FromBinary(licenseKeyModel.IssueDate);
            //ExpiryDt = DateTime.FromBinary(licenseKeyModel.ExpiryDate);
            //var expireDays = ExpiryDt.Subtract(DateTime.Now.Date.AddDays(0)).Days;
            //if (expireDays < 0)
            //    throw new WarningException("Warning : The license has expired");

            //Check Active Status
            if (!licenseKeyModel.Status.Equals("Active"))
                throw new WarningException("Warning : The license key is no longer active.");

            //Update the registry
            RegistryWrapper.WriteRegistry(Global.RegistryKey, "license", licenseKeyModel.Id);
            RegistryWrapper.WriteRegistry(Global.RegistryKey, "type", licenseKeyModel.Type);           

            //Check Machine Key
            if (!licenseKeyModel.MachineKey.Equals(MachineKeyGenerator.Value()))
                throw new WarningException("Warning : The machine keys do not match");

            //Update the registry
            RegistryWrapper.WriteRegistry(Global.RegistryKey, "expireDt", licenseKeyModel.ExpiryDate.ToString());
            RegistryWrapper.WriteRegistry(Global.RegistryKey, "isHosted", licenseKeyModel.Database.Hosted.ToString());

            //Check Database configuration
            if (licenseKeyModel.Database.Hosted) {
                if (licenseKeyModel.Database.ConnectionString.Equals("-"))
                    throw new WarningException("Warning : The hosted database has not yet been configured. Please consult your Administrator.");

                if (!licenseKeyModel.Database.Status.Equals("Enabled"))
                    throw new WarningException("Warning : The hosted database has been disabled. Please consult your Administrator.");

               
                Program.ConnectionInfo = new MySqlConnectionInfo(my.domain.lib.core.Repository.ConnectionTypes.MySQL)
                {
                    ServerName = licenseKeyModel.Database.ConnectionString,
                    DbName = licenseKeyModel.Database.DatabaseName,
                    DbSchema = licenseKeyModel.Database.DatabaseName,
                    PortNumber = licenseKeyModel.Database.Port,
                    UserName = licenseKeyModel.Database.Username,
                    UserPwd = licenseKeyModel.Database.Password,
                    AssemblyType = typeof(DomainServices)
                };

                //TO DO : UPDATE Database Configuration in Registry
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbType", Program.ConnectionInfo.ConnectionType);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbServer", Program.ConnectionInfo.ServerName);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbCatalog", Program.ConnectionInfo.DbName);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbSchema", Program.ConnectionInfo.DbSchema);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbPort", Program.ConnectionInfo.PortNumber);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminUser", Program.ConnectionInfo.UserName.Encrypt(Global.RegistryKey));
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminPwd", Program.ConnectionInfo.UserPwd.Encrypt(Global.RegistryKey));
            }

            this.Status = licenseKeyModel.Status;
            this.Type = licenseKeyModel.Type;
            this.IssueDate = licenseKeyModel.IssueDate;
            this.ExpiryDate = licenseKeyModel.ExpiryDate;

            Program.User.Username = licenseKeyModel.Customer.Email;
            Program.User.Firstname = licenseKeyModel.Customer.Name;

            return await Task.FromResult(true);
          
        }
        
        public bool HasFeature(string featureName)
        {
            try { 
            
                if(licenseKeyModel!=null)
                {
                    var feature = licenseKeyModel.ProductFeatures.FeatureModels.Where(x => x.name == featureName).FirstOrDefault();

                    if (feature != null) {
                        if (feature.Status == "yes")
                            return true;
                    }
                }
            }
            catch(Exception x) 
            {
                Program.Logger.Error(x);
            }

            return false;
        }
     }
}
