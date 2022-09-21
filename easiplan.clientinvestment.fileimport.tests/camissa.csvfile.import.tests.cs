using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace easiplan.clientinvestment.fileimport.tests
{
    [TestClass]
    public class camissa_csvfile_import_tests
    {
        private readonly string _lispFileDir = @"C:\Users\Taurique\Source\Repos\Easiworx_FeatureBranches\easiplan\easiplan.clientinvestment.fileimport.tests\LISPFiles\";
        private string[] _columnHeadings = {"investor no", "investor name", "account no", "fund name", "market value", "value date", "date started", "last activity date", "title", "first name", "initials", "surname", "investor type", "id no", "passport no", "registration no", "tax no", "vat no.", "birth date", "non resident", "blocked rand account", "restriction", "aci description", "exchange control type", "sarb type", "fica compliant", "entity status", "quarterly correspondent distribution", "income distribution method", "email addr", "office tel", "home tel", "cell", "fax", "postal addr1", "postal addr2", "postal addr3", "postal addr4", "postal addr5", "postal addr6", "postal code", "physical addr1", "physical addr2", "physical addr3", "physical addr4", "physical addr5", "physical addr6", "physical code", "broker code", "broker name", "broker type", "broker house code", "broker house name", "broker house type" };
       
        [TestMethod]
        public void ValidateFile_WithSummaryHeading()
        {
            try
            {
                var filePath = _lispFileDir + "Camissa Investor Demographic by Fund 31Aug22.csv";
                var result = FileHasSummaryHeadingText(filePath);
                if(result)
                    Assert.Fail("Csv file is invalid & cannot be imported! Please make sure the file has no summary headings.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ValidateFile_WithNoSummaryHeading()
        {
            try
            {
                var filePath = _lispFileDir + "MOJAFF CAMISSA client data June 22.csv";
                var result = FileHasSummaryHeadingText(filePath);
                Assert.IsTrue(!result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private bool FileHasSummaryHeadingText(string filePath)
        {
            var result = false;
            try
            {
                var fileLines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
                
                if (fileLines.Length == 0)
                    throw new ApplicationException("File is empty!");

                result = fileLines[0].ToLower().StartsWith("investor no")?false:true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [TestMethod]
        public void ValidateFile_ValidFile_ColumnHeadings()
        {
            try
            {
                var filePath = _lispFileDir + "MOJAFF CAMISSA client data June 22.csv";
                var result = FileHasSummaryHeadingText(filePath);
                
                if (result)
                    throw new ApplicationException("Csv file is invalid & cannot be imported! Please make sure the file has no summary headings.");

                var fileLines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);

                if (fileLines.Length == 0)
                    throw new ApplicationException("File is empty!");
                char[] delimeters = { ',',';' };
                var fileHeaders = fileLines[0].ToLower().Split(delimeters, StringSplitOptions.None);
                for (var i = 0; i < fileHeaders.Length; i++)
                {
                    if (fileHeaders[i].Trim() != _columnHeadings[i].Trim())
                    { 
                        Assert.Fail("Camissa csv file headings mismatch!");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
