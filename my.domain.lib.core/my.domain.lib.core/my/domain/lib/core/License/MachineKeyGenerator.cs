// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.License.MachineKeyGenerator
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace my.domain.lib.core.License
{
    public static class MachineKeyGenerator
    {
        private static string fingerPrint = string.Empty;

        private static string GetHash(string s)
        {
            return MachineKeyGenerator.GetHexString(new MD5CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(s)));
        }

        private static string GetHexString(byte[] bt)
        {
            string str1 = string.Empty;
            for (int index = 0; index < bt.Length; ++index)
            {
                int num1 = (int)bt[index];
                int num2 = num1 & 15;
                int num3 = num1 >> 4 & 15;
                string str2 = num3 <= 9 ? str1 + num3.ToString() : str1 + ((char)(num3 - 10 + 65)).ToString();
                str1 = num2 <= 9 ? str2 + num2.ToString() : str2 + ((char)(num2 - 10 + 65)).ToString();
                if (index + 1 != bt.Length && (index + 1) % 2 == 0)
                    str1 += "-";
            }
            return str1;
        }

        public static string Value()
        {
            if (string.IsNullOrEmpty(MachineKeyGenerator.fingerPrint))
                MachineKeyGenerator.fingerPrint = MachineKeyGenerator.GetHash("CPU >> " + MachineKeyGenerator.cpuId() + "\nBIOS >> " + MachineKeyGenerator.biosId() + "\nBASE >> " + MachineKeyGenerator.baseId());
            return MachineKeyGenerator.fingerPrint;
        }

        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string str = "";
            foreach (ManagementObject instance in new ManagementClass(wmiClass).GetInstances())
            {
                if (instance[wmiMustBeTrue].ToString() == "True")
                {
                    if (str == "")
                    {
                        try
                        {
                            str = instance[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return str;
        }

        private static string identifier(string wmiClass, string wmiProperty)
        {
            string str = "";
            foreach (ManagementObject instance in new ManagementClass(wmiClass).GetInstances())
            {
                if (str == "")
                {
                    try
                    {
                        str = instance[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return str;
        }

        private static string cpuId()
        {
            string str1 = MachineKeyGenerator.identifier("Win32_Processor", "UniqueId");
            if (str1 == "")
            {
                str1 = MachineKeyGenerator.identifier("Win32_Processor", "ProcessorId");
                if (str1 == "")
                {
                    string str2 = MachineKeyGenerator.identifier("Win32_Processor", "Name");
                    if (str2 == "")
                        str2 = MachineKeyGenerator.identifier("Win32_Processor", "Manufacturer");
                    str1 = str2 + MachineKeyGenerator.identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return str1;
        }

        private static string biosId()
        {
            return MachineKeyGenerator.identifier("Win32_BIOS", "Manufacturer") + MachineKeyGenerator.identifier("Win32_BIOS", "SMBIOSBIOSVersion") + MachineKeyGenerator.identifier("Win32_BIOS", "IdentificationCode") + MachineKeyGenerator.identifier("Win32_BIOS", "SerialNumber") + MachineKeyGenerator.identifier("Win32_BIOS", "ReleaseDate") + MachineKeyGenerator.identifier("Win32_BIOS", "Version");
        }

        private static string diskId()
        {
            return MachineKeyGenerator.identifier("Win32_DiskDrive", "Model") + MachineKeyGenerator.identifier("Win32_DiskDrive", "Manufacturer") + MachineKeyGenerator.identifier("Win32_DiskDrive", "Signature") + MachineKeyGenerator.identifier("Win32_DiskDrive", "TotalHeads");
        }

        private static string baseId()
        {
            return MachineKeyGenerator.identifier("Win32_BaseBoard", "Model") + MachineKeyGenerator.identifier("Win32_BaseBoard", "Manufacturer") + MachineKeyGenerator.identifier("Win32_BaseBoard", "Name") + MachineKeyGenerator.identifier("Win32_BaseBoard", "SerialNumber");
        }

        private static string videoId()
        {
            return MachineKeyGenerator.identifier("Win32_VideoController", "DriverVersion") + MachineKeyGenerator.identifier("Win32_VideoController", "Name");
        }

        private static string macId()
        {
            return MachineKeyGenerator.identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
    }
}
