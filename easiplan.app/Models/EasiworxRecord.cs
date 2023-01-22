using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    public sealed class EasiworxRecord : ICsvRecord
    {
        private string _idNo = "";
        private string _fundValue = "";
        private string _fundValueDate = "";
        private string _fundAllocationPercentage;
        private string _startDate;
        private string _firstname;
        private string _validationErrors;
        private string _premium;
        private string _dob = "";
        private string _monthlyPremium;

        [Ignore]
        public int RowNo { get; set; }

        [Index(0)]
        public string Firstname 
        { 
            get { return _firstname; } 
            set 
            {
                if (value.Contains(","))
                {
                    
                    var fullnames = value.Split(',');
                    if (fullnames.Length > 1)
                    {
                        _firstname = fullnames[1].Replace("\"",string.Empty).Trim();
                        this.Lastname = fullnames[0].Replace("\"", string.Empty).Trim();
                    }
                    else
                        _firstname = value;
                }
                else
                    _firstname = value; 
            } 
        }

        [Index(1)]
        public string Lastname
        {
            get;set;
        }

        [Index(2)]
        public string Dob
        {
            get { return _dob; }
            set 
            {
                if (DateTime.TryParse(value, out DateTime dobDt))
                    _dob = dobDt.ToString("dd MMM yyyy");
                else
                    _dob = value;
            }
        }

        [Index(3)] 
        public string IDNumber
        {
            get { return _idNo; }
            set
            {
                if (CsvFileHelper.IsPassportNo(value))
                    this.PassportNo = value;
                else
                    _idNo = CsvFileHelper.FixSAIDNo(value);
            }
        }

        [Index(4)] 
        public string RegistrationNo
        {
            get;set;
        }

        [Index(5)]
        public string PassportNo
        {
            get; set;
        }

        [Index(6)]
        public string ClientNo
        {
            get; set;
        }

        [Optional]
        public string PostalAddressStreetNo
        {
            get; set;
        }

        [Optional]
        public string PostalAddress
        {
            get; set;
        }
        [Optional]
        public string Suburb
        {
            get; set;
        }
        [Optional]
        public string PostalCode
        {
            get; set;
        }

        [Optional]
        public string PhysicalAddressStreetNo
        {
            get; set;
        }

        [Optional]
        public string PhysicalAddress
        {
            get; set;
        }
        [Optional]
        public string PhysicalAddressSuburb
        {
            get; set;
        }
        [Optional]
        public string PhysicalAddressPostalCode
        {
            get; set;
        }

        [Optional] 
        public string CellNo
        {
            get; set;
        }
        [Optional]
        public string OfficeTel
        {
            get; set;
        }
        [Optional]
        public string HomeTel
        {
            get; set;
        }
        
        [Optional]
        public string EmailAddress
        {
            get; set;
        }


        [Index(19) ]
        public string ProductName
        {
            get; set;
        }

        [Index(20)]
        public string ModelPortfolio
        {
            get; set;
        }

        [Index(21)]
        public string AccountName { get; set; }

        [Index(22)] //Policy No
        public string AccountNo { get; set; }

        [Optional] 
        public string FundCode { get; set; }


        [Index(24)] 
        public string FundName { get; set; }

        [Index(25)] //Market value in rands
        public string FundValue
        {
            get { return _fundValue; }
            set
            {
                _fundValue = value;
            }
        }

        [Index(26)] 
        public string FundValueDate
        {
            get { return _fundValueDate; }
            set
            {
                //if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                if (DateTime.TryParse(value, out DateTime fundValDt))
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                else
                    _fundValueDate = value;
            }
        }

        [Optional]
        public string AccountFundAllocation
        {
            get { return _fundAllocationPercentage; }
            set
            {
                _fundAllocationPercentage = value.Replace("%", string.Empty);
            }
        }

        [Index(28)] 
        public string InceptionDate
        {
            get { return _startDate; }
            set
            {
                //if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtStartDt))
                if (DateTime.TryParse(value, out DateTime dtStartDt))
                    _startDate = dtStartDt.ToString("dd MMM yyyy");
                else
                    _startDate = value;
            }
        }

        [Optional] //Monthly Debit Order Premium
        [Index(29)]
        public string MonthlyPremium
        {
            get { return _monthlyPremium; }
            set { _monthlyPremium = value; }
        }

        [Optional]
        public string LISP { get; set; }

        [Optional]
        public string ValidationErrors 
        { 
            get { return _validationErrors; } 
            set 
            { 
                _validationErrors = value;
                if(!string.IsNullOrEmpty(value))
                    this.HasErrors = true;
            } 
        }

        [Optional]
        public bool HasErrors
        {
            get;set;
        }

        public void ValidateHeadings(HeaderValidatedArgs args)
        {
            //throw new NotImplementedException();
        }
    }

    public sealed class EasiworxRecordMap : ClassMap<EasiworxRecord>
    {
        public EasiworxRecordMap()
        {
            Map(c => c.Firstname);
            Map(c => c.Lastname);
            Map(c => c.Dob);
            Map(c => c.IDNumber);
            Map(c => c.RegistrationNo);
            Map(c => c.PassportNo);
            Map(c => c.ClientNo);

            Map(c => c.PostalAddressStreetNo);
            Map(c => c.PostalAddress);
            Map(c => c.Suburb);
            Map(c => c.PostalCode);

            Map(c => c.PhysicalAddressStreetNo);
            Map(c => c.PhysicalAddress);
            Map(c => c.PhysicalAddressSuburb);
            Map(c => c.PhysicalAddressPostalCode);

            Map(c => c.CellNo); 
            Map(c => c.OfficeTel); 
            Map(c => c.HomeTel);
            Map(c => c.EmailAddress);
            

            Map(c => c.ProductName);
            Map(c => c.ModelPortfolio);
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundCode);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.AccountFundAllocation);
            Map(c => c.InceptionDate);
            Map(c => c.MonthlyPremium);


            Task.Run(() =>
            {
                Map(c => c.ValidationErrors)
                    .Convert(r =>
                    {
                        var errors = new StringBuilder();

                        var idNumber = r.Row.GetField<string>("IDNumber");
                        var policyNo = r.Row.GetField<string>("AccountNo");
                        var fundName = r.Row.GetField<string>("FundName");
                        var fundValueDate = r.Row.GetField<string>("FundValueDate");

                        if (string.IsNullOrEmpty(idNumber))
                            errors.Append("ID Number is null!");
                    
                        if (!Regex.IsMatch(idNumber, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
                            errors.Append("Invalid SA ID No!");

                        if (string.IsNullOrEmpty(policyNo))
                            errors.Append("Policy Number is null!");

                        if (string.IsNullOrEmpty(fundName))
                            errors.Append("Fund Name is null!");

                        if (string.IsNullOrEmpty(fundValueDate))
                            errors.Append("Fund Value Date is null!");

                        return errors.ToString();

                    });
            }).Wait();
        }
    }
}
