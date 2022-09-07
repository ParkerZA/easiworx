// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.License.ActivationKeyFileCheck
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.IO;
using System.Xml;

namespace my.domain.lib.core.License
{
    internal class ActivationKeyFileCheck
    {
        public void CheckActivationKeyFile(
          string activationKeyFileFullPath,
          KeyByteSet keyByteSet1,
          KeyByteSet keyByteSet2,
          string licenceKeyFileEncryptionString,
          string friendlyApplicationName)
        {
            if (!File.Exists(activationKeyFileFullPath))
                throw new ActivationKeyFileNotPresentException("The product activation key file '" + activationKeyFileFullPath + "' was expected at '" + activationKeyFileFullPath + "', but was not found. Please visit the vendor website to obtain a product activation key file.");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(activationKeyFileFullPath);
            string innerText = xmlDocument.GetElementsByTagName("Key")[0].InnerText;
            if (string.IsNullOrEmpty(innerText))
                return;
            string empty = string.Empty;
            string key;
            try
            {
                key = ActivationKeyDecryption.Decrypt(innerText, licenceKeyFileEncryptionString);
            }
            catch
            {
                throw new ActivationKeyInvalidException(string.Format("The licence key that allows {0} to run is invalid. Please check that the key file exists in the executing directory, and has not been modified.", (object)friendlyApplicationName));
            }
            if (key.ToLower() == "trial")
                throw new TrialPeriodExpiredException(string.Format("The trial period for {0} has expired.", (object)friendlyApplicationName));
            if ((uint)new PkvKeyCheck().CheckKey(key, new KeyByteSet[2]
            {
        keyByteSet1,
        keyByteSet2
            }, 8, (string[])null) > 0U)
                throw new ActivationKeyInvalidException(string.Format("The licence key that allows {0} to run is invalid. Please check that the key file exists in the executing directory, and has not been modified.", (object)friendlyApplicationName));
        }
    }
}
