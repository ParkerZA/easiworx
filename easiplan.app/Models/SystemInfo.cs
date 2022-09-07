using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Win32;

using System.Windows.Forms;
using Finx.App;
using easiplan.domain.Entities;
using System.Net.NetworkInformation;
using Finx.App.Models;
using System.Diagnostics;
using System.IO;

namespace Finx.App.Models
{
    public static class SystemInfo
    {
        public static string SystemInformation(string outFile)
        {
            XDocument xdoc = new XDocument();

            XElement docElement = new XElement("SystemInfo");
            try
            {
                XElement AppInfo = new XElement("ApplicationInfo",
                   new XElement("ProductName", Application.ProductName),
                   new XElement("ProductVersion", Application.ProductVersion),
                   new XElement("DbVersion", Program.Repository.List<DbVersion, int>(null).FirstOrDefault().Version),
                   new XElement("ConnectionString", Program.Repository.ConnectionInfo.ConnectionString())
                   );
                docElement.Add(AppInfo);
            }
            catch (Exception x) 
            {
                Program.Logger.Error(x);
            }

            var macAddr =
               (
                   from nic in NetworkInterface.GetAllNetworkInterfaces()
                   where nic.OperationalStatus == OperationalStatus.Up
                   select nic.GetPhysicalAddress().ToString()
               ).FirstOrDefault();

            XElement osInfo = new XElement("OSInfo",
                new XElement("OS", Environment.OSVersion),
                new XElement("CLRVersion", Environment.Version),
                new XElement("Is64bit", Environment.Is64BitOperatingSystem),
                new XElement("SystemDirectory", Environment.SystemDirectory),
                new XElement("CurrentDirectory", Environment.CurrentDirectory),
                new XElement("ProcessorCount", Environment.ProcessorCount),
                new XElement("UserDomainName", Environment.UserDomainName),
                new XElement("UserName", Environment.UserName),
                new XElement("MachineName", Environment.MachineName),
                new XElement("MacAddress", macAddr)
                );
            docElement.Add(osInfo);

            XElement officeInfo = new XElement("OfficeInfo",
                new XElement("Excel", GetMajorVersion(GetComponentPath(OfficeComponent.Excel))),
                new XElement("Word", GetMajorVersion(GetComponentPath(OfficeComponent.Word))),
                new XElement("Outlook", GetMajorVersion(GetComponentPath(OfficeComponent.Outlook)))
                );
            docElement.Add(officeInfo);

            Type _type = typeof(LicenseModel);
            var regPath = "Software\\Mojoe\\HostKeys\\" + _type.GUID.ToString();

            XElement licInfo = new XElement("LicenseInfo",
                 new XElement("MachineKey", GetRegKey(regPath, "machine")),//FingerPrint.Value()
                 new XElement("Version", Program.Licensing.licenseKeyModel.Type),
                 new XElement("InstallDate", Program.Licensing.licenseKeyModel.IssueDate),
                 new XElement("LicenceKey", GetRegKey(regPath, "license")),
                 new XElement("IsValid", GetRegKey(regPath, "valid")),
                 new XElement("ExpireDate", Program.Licensing.licenseKeyModel.ExpiryDate)

                 );
            docElement.Add(licInfo);

            xdoc.Add(docElement);

            if (!string.IsNullOrEmpty(outFile))
                xdoc.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), outFile));

            return xdoc.ToString();

        }

        /// <span class="code-SummaryComment"><summary></span>
        /// gets the component's path from the registry. if it can't find it - retuns 
        ///an empty string
        /// <span class="code-SummaryComment"></summary></span>
        internal static string GetComponentPath(OfficeComponent _component)
        {
            const string RegKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
            string toReturn = string.Empty;
            string _key = string.Empty;

            switch (_component)
            {
                case OfficeComponent.Word:
                    _key = "winword.exe";
                    break;
                case OfficeComponent.Excel:
                    _key = "excel.exe";
                    break;
                case OfficeComponent.PowerPoint:
                    _key = "powerpnt.exe";
                    break;
                case OfficeComponent.Outlook:
                    _key = "outlook.exe";
                    break;
            }

            //looks inside CURRENT_USER:
            RegistryKey _mainKey = Registry.CurrentUser;
            try
            {
                _mainKey = _mainKey.OpenSubKey(RegKey + "\\" + _key, false);
                if (_mainKey != null)
                {
                    toReturn = _mainKey.GetValue(string.Empty).ToString();
                }
            }
            catch
            { }

            //if not found, looks inside LOCAL_MACHINE:
            _mainKey = Registry.LocalMachine;
            if (string.IsNullOrEmpty(toReturn))
            {
                try
                {
                    _mainKey = _mainKey.OpenSubKey(RegKey + "\\" + _key, false);
                    if (_mainKey != null)
                    {
                        toReturn = _mainKey.GetValue(string.Empty).ToString();
                    }
                }
                catch
                { }
            }

            //closing the handle:
            if (_mainKey != null)
                _mainKey.Close();

            return toReturn;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Gets the major version of the path. if file not found (or any other        
        /// exception occures - returns 0
        /// <span class="code-SummaryComment"></summary></span>
        internal static int GetMajorVersion(string _path)
        {
            int toReturn = 0;
            if (File.Exists(_path))
            {
                try
                {
                    FileVersionInfo _fileVersion = FileVersionInfo.GetVersionInfo(_path);
                    toReturn = _fileVersion.FileMajorPart;
                }
                catch
                { }
            }
            return toReturn;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// gets the component's path from the registry. if it can't find it - retuns 
        ///an empty string
        /// <span class="code-SummaryComment"></summary></span>
        internal static string GetRegKey(string RegPath, string RegKey)
        {


            //looks inside CURRENT_USER:
            using (RegistryKey mainKey = Registry.CurrentUser)
            {
                try
                {
                    var _mainKey = mainKey.OpenSubKey(RegPath, false);
                    if (_mainKey != null)
                    {
                        return _mainKey.GetValue(RegKey, string.Empty).ToString();
                    }
                }
                catch
                { }
            }
            //if not found, looks inside LOCAL_MACHINE:
            using (RegistryKey mainKey = Registry.LocalMachine)
            {
                try
                {
                    var _mainKey = mainKey.OpenSubKey(RegPath, false);
                    if (_mainKey != null)
                    {
                        return _mainKey.GetValue(RegKey, string.Empty).ToString();
                    }
                }
                catch
                { }
            }

            return null;
        }

        internal enum OfficeComponent
        {
            Word,
            Excel,
            PowerPoint,
            Outlook
        }
    }
}
