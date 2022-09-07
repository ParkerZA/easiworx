// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Extensions.StringExt
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace my.domain.lib.core.Extensions
{
    public static class StringExt
    {
        internal static DateTimeFormatInfo dtfi = new DateTimeFormatInfo()
        {
            ShortDatePattern = "dd/MM/yyyy"
        };
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;

        public static string Encrypt(this string str, string key)
        {
            try
            {
                return StringExt._Encrypt(str, key);
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        public static string Decrypt(this string str, string key)
        {
            try
            {
                return StringExt._Decrypt(str, key);
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        private static string _Encrypt(string plainText, string passPhrase)
        {
            byte[] salt = StringExt.Generate256BitsOfRandomEntropy();
            byte[] rgbIV = StringExt.Generate256BitsOfRandomEntropy();
            byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt, 1000))
            {
                byte[] bytes2 = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = 256;
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, rgbIV))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes1, 0, bytes1.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] array = ((IEnumerable<byte>)((IEnumerable<byte>)salt).Concat<byte>((IEnumerable<byte>)rgbIV).ToArray<byte>()).Concat<byte>((IEnumerable<byte>)memoryStream.ToArray()).ToArray<byte>();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(array);
                            }
                        }
                    }
                }
            }
        }

        private static string _Decrypt(string cipherText, string passPhrase)
        {
            byte[] numArray1 = Convert.FromBase64String(cipherText);
            byte[] array1 = ((IEnumerable<byte>)numArray1).Take<byte>(32).ToArray<byte>();
            byte[] array2 = ((IEnumerable<byte>)numArray1).Skip<byte>(32).Take<byte>(32).ToArray<byte>();
            byte[] array3 = ((IEnumerable<byte>)numArray1).Skip<byte>(64).Take<byte>(numArray1.Length - 64).ToArray<byte>();
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, array1, 1000))
            {
                byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = 256;
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, array2))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(array3))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] numArray2 = new byte[array3.Length];
                                int count = cryptoStream.Read(numArray2, 0, numArray2.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(numArray2, 0, count);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] data = new byte[32];
            using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
                cryptoServiceProvider.GetBytes(data);
            return data;
        }

        public static byte[] GetBytes(this string str)
        {
            byte[] numArray = new byte[str.Length * 2];
            Buffer.BlockCopy((Array)str.ToCharArray(), 0, (Array)numArray, 0, numArray.Length);
            return numArray;
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chArray = new char[bytes.Length / 2];
            Buffer.BlockCopy((Array)bytes, 0, (Array)chArray, 0, bytes.Length);
            return new string(chArray);
        }

        public static Stream ToStream(this string str)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter((Stream)memoryStream);
            streamWriter.Write(str);
            streamWriter.Flush();
            memoryStream.Position = 0L;
            return (Stream)memoryStream;
        }

        public static IList<KeyValuePair<string, object>> ToKeyValuePair(
          this string source,
          string sep = "&",
          string eq = "=")
        {
            IList<KeyValuePair<string, object>> keyValuePairList = (IList<KeyValuePair<string, object>>)new List<KeyValuePair<string, object>>();
            string str1 = source;
            char[] chArray1 = new char[1] { sep.ToCharArray()[0] };
            foreach (string str2 in str1.Split(chArray1))
            {
                char[] chArray2 = new char[1] { eq.ToCharArray()[0] };
                string[] strArray = str2.Split(chArray2);
                if (strArray.Length == 1)
                    keyValuePairList.Add(new KeyValuePair<string, object>(strArray[0], (object)""));
                else
                    keyValuePairList.Add(new KeyValuePair<string, object>(strArray[0], (object)strArray[1]));
            }
            return keyValuePairList;
        }

        public static string ReplacePlaceholders<T>(this string source, T model, string placeholder = "#")
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                string oldValue = string.Format("{0}{1}{0}", (object)placeholder, (object)property.Name);
                try
                {
                    if (source.Contains(oldValue))
                        source = source.Replace(oldValue, ((object)model).GetPropertyValue(property.Name).ToString());
                }
                catch (Exception ex)
                {
                }
            }
            return source;
        }

        public static string Replace(this string obj, IDictionary<string, string> values)
        {
            if (obj == null)
                return (string)null;
            string str = obj;
            foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>)values)
                str = str.Replace(string.Format("#{0}#", (object)keyValuePair.Key), keyValuePair.Value);
            return str;
        }

        public static string Replace(this string obj, IDictionary<string, object> values)
        {
            if (obj == null)
                return (string)null;
            string str = obj;
            foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>)values)
                str = str.Replace(string.Format("#{0}#", (object)keyValuePair.Key), keyValuePair.Value.ToString());
            return str;
        }

        public static IDictionary<string, string> ToDictionary(
          this string obj,
          string sepChar1,
          string sepChar2)
        {
            IDictionary<string, string> dictionary = (IDictionary<string, string>)new Dictionary<string, string>();
            if (obj == null)
                return dictionary;
            foreach (string str1 in ((IEnumerable<string>)obj.Split(sepChar1.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList<string>())
            {
                int length = str1.IndexOf(sepChar2);
                string key = str1.Substring(0, length);
                string str2 = str1.Substring(length + 1);
                if (!string.IsNullOrEmpty(key))
                    dictionary.Add(key, str2);
            }
            return dictionary;
        }

        public static byte[] ToBase64(this string base64Encoded)
        {
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (FormatException ex)
            {
                throw new Exception("Base64: (FormatException) " + ex.Message + "\r\nOn string: " + base64Encoded);
            }
        }

        public static string ToBase64(this string base64Encoded, Encoding encoding)
        {
            if (base64Encoded == null)
                throw new ArgumentNullException(nameof(base64Encoded));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            return encoding.GetString(base64Encoded.ToBase64());
        }

        public static string InitCaps(this string obj)
        {
            if (obj == null)
                return (string)null;
            return Regex.Replace(obj.ToLower(), "(?<=\\b(?:mc|mac)?)[a-zA-Z](?<!'s\\b)", (MatchEvaluator)(m => m.Value.ToUpper()));
        }

        public static string ToFormat(this string obj, FieldTypes type)
        {
            try
            {
                switch (type)
                {
                    case FieldTypes.Decimal:
                        return string.Format("{0:#0.00}", (object)(Decimal.Parse(obj) / new Decimal(100)));
                    case FieldTypes.Percentage:
                        return string.Format("{0:#0.00}", (object)(Decimal.Parse(obj) / new Decimal(100)));
                    case FieldTypes.Date:
                        return DateTime.Parse(string.Format("{0:####/##/##}", (object)int.Parse(obj))).ToString("dd/MM/yyyy");
                    default:
                        return obj.ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        public static string UTF8toASCII(this string text)
        {
            return Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.ASCII, Encoding.UTF8.GetBytes(text)));
        }

        public static string UTF8toANSII(this string text)
        {
            return Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(text)));
        }

        public static bool IsCharactersOnly(this string text, string Exceptions = " ")
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return Regex.IsMatch(text, "^[\\p{L}" + Exceptions + "]+$");
        }

        public static bool IsCharactersAndNumbersOnly(this string text, string Exceptions = "")
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return Regex.IsMatch(text, "^[\\p{L}0-9" + Exceptions + "]+$");
        }

        public static bool IsNumbersOnly(this string text, string Exceptions = "")
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return Regex.IsMatch(text, "^[0-9" + Exceptions + "]+$");
        }

        public static bool IsDate(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            if (text == "0")
                return false;
            try
            {
                DateTime dateTime;
                if (text.Length == 8)
                {
                    try
                    {
                        string s = string.Format("{0}/{1}/{2}", (object)text.Substring(0, 2), (object)text.Substring(2, 2), (object)text.Substring(4, 4));
                        dateTime = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                        text = s;
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            string s = string.Format("{0}/{1}/{2}", (object)text.Substring(6, 2), (object)text.Substring(4, 2), (object)text.Substring(0, 4));
                            dateTime = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                            text = s;
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                    }
                }
                if (text.Length == 6)
                {
                    try
                    {
                        string s = string.Format("{0}/{1}/{2}", (object)"01", (object)text.Substring(2, 2), (object)text.Substring(2, 4));
                        dateTime = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                        text = s;
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            string s = string.Format("{0}/{1}/{2}", (object)"01", (object)text.Substring(4, 2), (object)text.Substring(0, 4));
                            dateTime = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                            text = s;
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                    }
                }
                if (DateTime.Parse(text, (IFormatProvider)StringExt.dtfi).Equals(DateTime.MinValue))
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static string ToShortDate(this string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || text == "0")
                    return "";
                DateTime dateTime1;
                if (text.Length == 8)
                {
                    try
                    {
                        string s = string.Format("{0}/{1}/{2}", (object)text.Substring(0, 2), (object)text.Substring(2, 2), (object)text.Substring(4, 4));
                        dateTime1 = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                        text = s;
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            string s = string.Format("{0}/{1}/{2}", (object)text.Substring(6, 2), (object)text.Substring(4, 2), (object)text.Substring(0, 4));
                            dateTime1 = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                            text = s;
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                    }
                }
                if (text.Length == 6)
                {
                    try
                    {
                        string s = string.Format("{0}/{1}/{2}", (object)"01", (object)text.Substring(2, 2), (object)text.Substring(2, 4));
                        dateTime1 = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                        text = s;
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            string s = string.Format("{0}/{1}/{2}", (object)"01", (object)text.Substring(4, 2), (object)text.Substring(0, 4));
                            dateTime1 = DateTime.Parse(s, (IFormatProvider)StringExt.dtfi);
                            text = s;
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                    }
                }
                DateTime dateTime2 = DateTime.Parse(text, (IFormatProvider)StringExt.dtfi);
                if (dateTime2.Equals(DateTime.MinValue))
                    return "";
                int num = dateTime2.Day;
                string str1 = num.ToString("0#");
                num = dateTime2.Month;
                string str2 = num.ToString("0#");
                num = dateTime2.Year;
                string str3 = num.ToString("####");
                return string.Format("{0}/{1}/{2}", (object)str1, (object)str2, (object)str3);
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public static int MyHashCode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            return text.GetHashCode();
        }

        public static string Replace(
          this string source,
          string oldValue,
          string newValue,
          StringComparison comparisonType)
        {
            if (source.Length == 0 || oldValue.Length == 0)
                return source;
            StringBuilder stringBuilder = new StringBuilder();
            int startIndex;
            int num;
            for (startIndex = 0; (num = source.IndexOf(oldValue, startIndex, comparisonType)) > -1; startIndex = num + oldValue.Length)
            {
                stringBuilder.Append(source, startIndex, num - startIndex);
                stringBuilder.Append(newValue);
            }
            stringBuilder.Append(source, startIndex, source.Length - startIndex);
            return stringBuilder.ToString();
        }
    }
}
