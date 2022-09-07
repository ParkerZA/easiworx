using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

//https://github.com/junian/Standard.Licensing/tree/master/src/Standard.Licensing.Tests

namespace my.domain.lib.core.License
{
    public class StandardLicense
    {

#region License Generation
        public Standard.Licensing.License GenerateLicenseKeyFile(string passPhrase,out string publicKey)
        {

            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            publicKey = keyPair.ToPublicKeyString();

            return Standard.Licensing.License.New()
            .WithUniqueIdentifier(Guid.NewGuid())
            .As(LicenseType.Trial)
            .ExpiresAt(DateTime.Now.AddDays(30))
            .WithMaximumUtilization(5)
            .WithProductFeatures(new Dictionary<string, string>
                {
                    {"Sales Module", "yes"},
                    {"Purchase Module", "yes"},
                    {"Maximum Transactions", "10000"}
                })
            .LicensedTo($"omcore\\om79807", "john.doe@example.com")
            .CreateAndSignWithPrivateKey(privateKey, passPhrase);          

        }

        private static DateTime ConvertToRfc1123(DateTime dateTime)
        {
            return DateTime.ParseExact(
                dateTime.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture)
                , "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }
#endregion

        public void RegisterLicense(string machineKey)
        {
            string PublicKey = "";
            Standard.Licensing.License license;
            try
            {
                //Call webservice to check MachineKey is registered and Generate or Return Valid Licensekey
                license = GenerateLicenseKeyFile(machineKey, out PublicKey);
            }
            catch (Exception x)
            {
                throw new MachineKeyInvalidException();
            }

            if (license == null)
                    throw new LicenseKeyInvalidException();

            try
            {
                

                XDocument doc = XDocument.Parse(license.ToString(),LoadOptions.None);

                doc.Save(LicenseKeyFileName());

                //TO DO :Save the PublicKey in the Registry
                Registry.RegistryWrapper.WriteRegistry("FinX", "PublicKey", PublicKey);


            }
            catch (Exception x2)
            {
                throw;
            }
    }

        public static void CheckLicense(LicenseType LicenseType = LicenseType.Trial,string ProductFeature = null)
        {
            string PublicKey = Registry.RegistryWrapper.ReadRegistry("FinX", "PublicKey");

            if (string.IsNullOrEmpty(PublicKey))
                throw new NotYetRegisteredException();

            //Check for a .lic file in current directory          
            XDocument doc = XDocument.Load(LicenseKeyFileName());

            var license = Standard.Licensing.License.Load(doc.Root.ToString());

            try
            {
                if (!license.VerifySignature(PublicKey))
                    throw new LicenseKeyInvalidException();
            }catch(Exception x)
            {
                throw new LicenseKeyInvalidException();
            }

            if (license.Customer.Name.ToLower() != System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower())
                throw new UsernameInvalidException();

            if (license.Expiration.Subtract(ConvertToRfc1123(DateTime.Now)).Days<0)
                throw new ExpirationInvalidException();

            //TO DO ... more validations
            if (LicenseType != LicenseType.Trial)
                if (license.Type != LicenseType)
                    throw new LicenseTypeException();

            if (!string.IsNullOrEmpty(ProductFeature))
            {
                if (!license.ProductFeatures.Contains(ProductFeature))
                    throw new FeatureNotEnabledException();

            }
  
        }

        private static string LicenseKeyFileName()
        {
            //Save the license key in the current folder ... or registry path
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace($"file:\\", "");
            string xmlFileName = Path.Combine(assemblyFolder, "License.lic");

            return xmlFileName;
        }

        public class NotYetRegisteredException : Exception
        {
            public NotYetRegisteredException()
              : base("This application has not yet been registered.Please contact your support for assistance")
            {
            }
        }
        public class LicenseKeyInvalidException : Exception
        {
            public LicenseKeyInvalidException()
              : base("The License key or Public Key could not be found or is invalid.Please contact your support for assistance")
            {
            }
        }

        public class MachineKeyInvalidException : Exception
        {
            public MachineKeyInvalidException()
              : base("The machine key could not be validated. Please register this installation on our website at www.finx.net")
            {
            }
        }

        public class UsernameInvalidException : Exception
        {
            public UsernameInvalidException()
              : base("The current user is not registered for this license key.")
            {
            }
        }

        public class ExpirationInvalidException : Exception
        {
            public ExpirationInvalidException()
              : base("The current license key has expired. Please request an updated license key")
            {
            }
        }

        public class LicenseTypeException : Exception
        {
            public LicenseTypeException()
              : base("The License Type is not valid for this operation.Please contact your support for assistance")
            {
            }
        }

        public class FeatureNotEnabledException : Exception
        {
            public FeatureNotEnabledException()
              : base("The feature is not enabled for this license key.Please contact your support for assistance")
            {
            }
        }
    }
}
