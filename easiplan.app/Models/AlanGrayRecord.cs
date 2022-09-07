using Finx.App.Models;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Models
{

    public class AlanGrayRecord : ICsvRecord
    {
        private string _idNo = "";
        private string _fundValue = "";
        private string _fundValueDate = "";
        private string _lisp = "Alan Gray";
        private string _fundAllocationPercentage;
        private string _startDate;
        private string _firstname;
        private string _validationErrors;

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
                        _firstname = fullnames[0];
                        this.Lastname = fullnames[1];
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
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Trim().Length >= 6 && value.Trim().Length <= 9) //this is most likely a passport no
                    this.PassportNo = value.Trim();
                else
                    _idNo = value.Replace("'", string.Empty);
            }
        }

        [Index(4)] //Product
        public string Product
        {
            get; set;
        }

        [Optional]
        public string PassportNo
        {
            get; set;
        }
        [Index(10)] //Fund name
        public string FundName { get; set; }

        [Index(11)] //Fund code
        public string FundCode { get; set; }


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

        [Index(19)] //Account fund allocation
        public string FundAllocationPercentage
        {
            get { return _fundAllocationPercentage; }
            set
            {
                _fundAllocationPercentage = value.Replace("%",string.Empty);
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
        public string LISP { get { return _lisp; } set { _lisp = "Alan Gray"; } }

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

    }

    public class AlanGrayRecordMap : ClassMap<AlanGrayRecord>
    {
        public AlanGrayRecordMap()
        {
            Map(c => c.Firstname);
            Map(c => c.IDNumber);
            Map(c => c.Product);
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundCode);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.StartDate);
            Map(c => c.FundAllocationPercentage);

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

                        if (!Regex.IsMatch(idNumber, @"(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][1-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])"))
                            errors.Append("Invalid RSA ID Number!");

                        if (string.IsNullOrEmpty(policyNo))
                            errors.Append("Account or Policy Number is null!");

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
