// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.License.PkvKeyCheck
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Globalization;

namespace my.domain.lib.core.License
{
    public class PkvKeyCheck
    {
        public PkvLicenceKeyResult CheckKey(
          string key,
          KeyByteSet[] keyByteSetsToCheck,
          int totalKeyByteSets,
          string[] blackListedSeeds)
        {
            key = PkvKeyCheck.FormatKeyForCompare(key);
            PkvLicenceKeyResult licenceKeyResult = PkvLicenceKeyResult.KeyInvalid;
            if (this.CheckKeyChecksum(key, totalKeyByteSets))
            {
                if (blackListedSeeds != null && (uint)blackListedSeeds.Length > 0U)
                {
                    for (int index = 0; index < blackListedSeeds.Length; ++index)
                    {
                        if (key.StartsWith(blackListedSeeds[index]))
                            licenceKeyResult = PkvLicenceKeyResult.KeyBlackListed;
                    }
                }
                if (licenceKeyResult != PkvLicenceKeyResult.KeyBlackListed)
                {
                    licenceKeyResult = PkvLicenceKeyResult.KeyPhoney;
                    int result;
                    if (int.TryParse(key.Substring(0, 8), NumberStyles.HexNumber, (IFormatProvider)null, out result))
                    {
                        foreach (KeyByteSet keyByteSet in keyByteSetsToCheck)
                        {
                            int keySubstringStart = this.GetKeySubstringStart(keyByteSet.KeyByteNo);
                            if (keySubstringStart - 1 > key.Length)
                                throw new InvalidOperationException("The KeyByte check position is out of range. You may have specified a check KeyByteNo that did not exist in the original key generation.");
                            if (key.Substring(keySubstringStart, 2) != this.GetKeyByte((long)result, keyByteSet.KeyBytes).ToString("X2"))
                                return licenceKeyResult;
                        }
                        licenceKeyResult = PkvLicenceKeyResult.KeyGood;
                    }
                }
            }
            return licenceKeyResult;
        }

        private int GetKeySubstringStart(int keyByteNo)
        {
            return keyByteNo * 2 + 6;
        }

        public bool CheckKeyChecksum(string key, int totalKeyByteSets)
        {
            bool flag = false;
            string str1 = PkvKeyCheck.FormatKeyForCompare(key);
            if (str1.Length == 12 + 2 * totalKeyByteSets)
            {
                int num = str1.Length - 4;
                string str2 = str1.Substring(num, 4);
                flag = this.GetChecksum(str1.Substring(0, num)) == str2;
            }
            return flag;
        }

        private static string FormatKeyForCompare(string key)
        {
            if (key == null)
                key = string.Empty;
            return key.Trim().ToUpper().Replace("-", string.Empty).Replace(" ", string.Empty);
        }

        private byte GetKeyByte(long seed, byte[] byteArr)
        {
            long num1 = 0;
            for (int index = 0; index < byteArr.Length; ++index)
            {
                byte num2 = byteArr[index];
                int num3 = (int)num2 % 25;
                if ((int)num2 % 2 == 0)
                    num1 += seed >> num3 & (long)byte.MaxValue ^ (seed >> num3 | (long)num2);
                else
                    num1 += seed >> num3 & (long)byte.MaxValue ^ seed >> num3 & (long)num2;
            }
            return (byte)num1;
        }

        private string GetChecksum(string str)
        {
            ushort num1 = 86;
            ushort num2 = 175;
            if (str.Length > 0)
            {
                for (int index = 0; index < str.Length; ++index)
                {
                    num2 += (ushort)Convert.ToByte(str[index]);
                    if (num2 > (ushort)byte.MaxValue)
                        num2 -= (ushort)byte.MaxValue;
                    num1 += num2;
                    if (num1 > (ushort)byte.MaxValue)
                        num1 -= (ushort)byte.MaxValue;
                }
            }
            return ((ushort)(((uint)num1 << 8) + (uint)num2)).ToString("X4");
        }
    }
}
