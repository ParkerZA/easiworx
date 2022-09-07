// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.License.ActivationKeyDecryption
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace my.domain.lib.core.License
{
    internal class ActivationKeyDecryption
    {
        public static string Decrypt(string cipherText, string password)
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, new byte[13]
            {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
            });
            return Encoding.Unicode.GetString(ActivationKeyDecryption.Decrypt(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
    }
}
