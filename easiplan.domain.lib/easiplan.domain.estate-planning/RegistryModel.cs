using Microsoft.Win32;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Finx.App.Models
{
    public class RegistryModel
    {
        public DbDetails dbDetails { get; set; }
        public LicenseModel licDetails{get;set;}
        public bool IsConfigured { get; set; }
        public string AppKey { get; set; }
        public string AppVersion { get; set; }
        public string LastUsername { get { return ReadRegistry("LastUsername"); } set { WriteRegistry("LastUsername",value.ToString()); } }
        public string BackgroundImage { get; set; }
        public bool IsOffline { get; set; }

        public RegistryModel(string applicationKey)
        {

            try
            {
                licDetails = new LicenseModel(typeof(LicenseModel));
                dbDetails = new DbDetails();

                // Check the unique Application Key
                AppKey = applicationKey;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(AppKey, false);

                if (regKey == null)
                    throw new Exception("Registry Key Invalid or not defined");

                //Config
                IsConfigured = regKey.GetValue("IsConfigured", false).ToString().ToUpper().Equals("TRUE") ? true : false;
                //App Version
                AppVersion = (string)regKey.GetValue("AppVersion", null);
                //Background Image
                BackgroundImage = (string)regKey.GetValue("BackgroundImage", null);
                
               //Database Details
                dbDetails.DbType = (string)regKey.GetValue("DbType", null);
                dbDetails.DbServer = (string)regKey.GetValue("DbServer", null);
                dbDetails.DbPort = (string)regKey.GetValue("DbPort", null);
                dbDetails.DbCatalog = (string)regKey.GetValue("DbCatalog", null);
                dbDetails.DbAdminUser = (string)regKey.GetValue("DbAdminUser", null);
                dbDetails.DbAdminPwd = (string)regKey.GetValue("DbAdminPwd", null);
              

                //decrypt the password
                dbDetails.DbAdminPwd = dbDetails.DbAdminPwd.Decrypt(AppKey);
                dbDetails.DbAdminUser = dbDetails.DbAdminUser.Decrypt(AppKey);
            }
            catch (Exception x)
            {
                //TO DO: Handle error
                Program.Logger.Error(x);
                IsConfigured = false;
            }

        }

        /// <summary>
        /// Creates the keys in the Registry
        /// </summary>
        public void WriteRegistry()
        {
            //The current Date
            //var now = DateTime.Now;
            //var cDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond, DateTimeKind.Local);

            RegistryKey applicationKey = Registry.CurrentUser.OpenSubKey(AppKey, true);
            // Create new application key
            if (applicationKey == null)
                applicationKey = Registry.CurrentUser.CreateSubKey(AppKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            
            applicationKey.SetValue("DbType", dbDetails.DbType, RegistryValueKind.String);
            applicationKey.SetValue("DbServer", dbDetails.DbServer, RegistryValueKind.String);
            applicationKey.SetValue("DbPort", dbDetails.DbPort, RegistryValueKind.String);
            applicationKey.SetValue("DbCatalog", dbDetails.DbCatalog, RegistryValueKind.String);
            applicationKey.SetValue("DbAdminUser", dbDetails.DbAdminUser.Encrypt(AppKey), RegistryValueKind.String);
            applicationKey.SetValue("DbAdminPwd", dbDetails.DbAdminPwd.Encrypt(AppKey), RegistryValueKind.String);
           
            applicationKey.SetValue("IsConfigured", IsConfigured);
            applicationKey.SetValue("AppVersion", AppVersion, RegistryValueKind.String);

        }

        public void WriteRegistry(string Key, object Value)
        {
            RegistryKey applicationKey = Registry.CurrentUser.OpenSubKey(AppKey, true);
            // Create new application key
            if (applicationKey == null)
                applicationKey = Registry.CurrentUser.CreateSubKey(AppKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            
            applicationKey.SetValue(Key, Value, RegistryValueKind.String);
        }

        public string ReadRegistry(string Key)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(AppKey, false);

                if (regKey == null)
                    throw new Exception("Registry Key Invalid or not defined");

                return (string)regKey.GetValue(Key, null);
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
            return null;
        }
    }
}
