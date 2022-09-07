using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using my.domain.lib.core.License;
using Standard.Licensing;

namespace my.domain.lib.core.test
{
    [TestClass]
    public class LicenseTest
    {
        [TestMethod]
        public void Can_Generate_License_And_Verify_Signature()
        {
            StandardLicense lic = new StandardLicense();
            string id = Guid.NewGuid().ToString();
            string publicKey;

            var license = lic.GenerateLicenseKeyFile(id,out publicKey);

            Assert.AreEqual(license.VerifySignature(publicKey), true);
        }
        [TestMethod]
        public void Can_Detect_Hacked_License()
        {
            StandardLicense lic = new StandardLicense();
            string id = Guid.NewGuid().ToString();
            string publicKey;

            var license = lic.GenerateLicenseKeyFile(id, out publicKey);

            // validate xml
            var xmlElement = XElement.Parse(license.ToString(), LoadOptions.None);
            Assert.AreEqual(xmlElement.HasElements,true);
            Assert.AreEqual(xmlElement.Element("Quantity").Value, "5");

            xmlElement.Element("Quantity").Value = "11"; // now we want to have 11 licenses

            var hackedLicense = Standard.Licensing.License.Load(xmlElement.ToString());

            Assert.AreEqual(hackedLicense.VerifySignature(publicKey), false);
        }

        [TestMethod]
        public void Can_Register_License_With_MachineKey()
        {
            StandardLicense lic = new StandardLicense();
            string id = MachineKeyGenerator.Value();// Guid.NewGuid().ToString();
            try
            {
                lic.RegisterLicense(id);

                Assert.IsNotNull(lic);
            }
            catch(Exception x)
            {
                Assert.Fail(x.Message);
            }

           
        }

        [TestMethod]
        public void Can_CheckLicense_With_Valid_LicenseType()
        {

            try
            {
                StandardLicense.CheckLicense();

                Assert.IsTrue(true);
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }


        }

        [TestMethod]
        public void Can_CheckLicense_For_Invalid_LicenseType()
        {

            try
            {
                StandardLicense.CheckLicense(LicenseType.Standard);

                Assert.Fail("Invalid License Type");
            }
            catch (Exception x)
            {
                Assert.IsTrue(true);
            }


        }

        [TestMethod]
        public void Test_CheckLicense_For_Valid_ProductFeature()
        {

            try
            {
                StandardLicense.CheckLicense(LicenseType.Trial, "Sales Module");

                Assert.IsTrue(true);
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }


        }

        [TestMethod]
        public void Test_CheckLicense_For_InValid_ProductFeature()
        {

            try
            {
                StandardLicense.CheckLicense(LicenseType.Trial, "New Feature");

                Assert.Fail("Invalid Feature");
               
            }
            catch (Exception x)
            {
                Assert.IsTrue(true);
            }


        }
    }
}
