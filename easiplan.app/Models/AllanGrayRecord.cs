using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using FluentValidation.Resources;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    public sealed class AllanGrayRecord : ICsvRecord
    {
        private string _idNo = "";
        private string _product = "";
        private string _fundValue = "";
        private string _fundValueDate = "";
        private string _lisp = "Allan Gray";
        private string _fundAllocationPercentage;
        private string _startDate;
        private string _firstname;
        private string _validationErrors;
        private string _clientNo;
        private string _premium;

        [Ignore]
        public int RowNo { get; set; }



        [Index(1)]//fullname - Client name e.g. Taurique, Toffie
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

        [Optional]
        public string Lastname
        {
            get;set;
        }

        [Index(2)] //ID number/Registration number. This column holds both rsa id no or passport no
        public string IDNumber
        {
            get { return _idNo; }
            set {
                if (CsvFileHelper.IsPassportNo(value))
                    this.PassportNo = value;
                else
                    _idNo = CsvFileHelper.FixSAIDNo(value);
            }
        }

        [Index(3)] //Client number
        public string ClientNo
        {
            get { return _clientNo; }
            set { _clientNo = value; }

        }

        [Index(4)] //Product
        public string ProductType
        {
            get { return _product; } 
            set { _product = value; }
           
        }

        [Optional]
        public string PassportNo
        {
            get; set;
        }
        

        [Index(7)] //Account number
        public string AccountNo { get; set; }

        [Index(8)] //Inception date
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtStartDt))
                    _startDate = dtStartDt.ToString("dd MMM yyyy");
                else
                    _startDate = value;
            }
        }

        [Index(10)] //Fund name
        public string FundName { get; set; }

        [Index(11)] //Fund code
        public string FundCode { get; set; }

        [Index(15)] //Monthly debit Order
        public string MonthlyPremium
        {
            get { return _premium; }
            set 
            { 
                _premium = value.Replace("R", String.Empty).Trim().Replace(".", ","); 
            } 
        }

        [Index(19)] //Account fund allocation
        public string FundAllocationPercentage
        {
            get { return _fundAllocationPercentage; }
            set
            {
                _fundAllocationPercentage = value.Replace("%", string.Empty);//.Replace(".",",");
                
            }
        }

        [Index(22)] //Price date
        public string FundValueDate
        {
            get { return _fundValueDate; }
            set
            {
                if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                else
                    _fundValueDate = value;
            }
        }

        [Index(26)] //Market value in rands
        public string FundValue
        {
            get { return _fundValue; }
            set
            {
                _fundValue = value.Replace(",", string.Empty);
            }
        }


        [Optional]
        public string LISP { get { return _lisp; } set { _lisp = "Allan Gray"; } }

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

    public sealed class AllanGrayRecordMap : ClassMap<AllanGrayRecord>
    {
        public AllanGrayRecordMap()
        {
            Map(c => c.Firstname);
            Map(c => c.IDNumber);
            Map(c => c.ClientNo);
            Map(c => c.ProductType);
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundCode);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.StartDate);
            Map(c => c.FundAllocationPercentage);
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
