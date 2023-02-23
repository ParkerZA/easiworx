using Finx.App.Models;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using Finx.App.Helpers;

namespace Finx.App.Models
{
    public sealed class MomentumRecord : ICsvRecord
    {
        [Ignore]
        public int RowNo { get; set; }
        private string _idNo = "";
        private string _fundValue="";
        private string _fundValueDate;
        private string _lisp="Momentum";
        private string _investmentStartDate;
        private string _fundName;
        private string _validationErrors;
        private string _fundPerc;
        private string _productType;


        [Index(3)]
        public string AccountNo { get; set; }

        [Index(4)]
        public string Title { get; set; }
        [Index(5)]
        public string Initials { get; set; }
        [Optional]
        public string Firstname { get; set; }

        [Index(6)]
        public string Lastname { get; set; }

        [Index(7)]
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
        
        [Optional]
        public string PassportNo
        {
            get; set;
        }
        [Index(8)]//Investment Basket
        public string ProductType
        {
            get
            {
                return _productType;
            }
            set
            {
                if (value.Contains("Retirement Income Option"))
                {
                    _productType = "Living Anuity";
                }
                else 
                {
                    _productType = value;
                }
            }
        }

        [Index(17)]
        public string FundCode { get; set; }
        
        [Index(18)]
        public string FundName { get { return _fundName; } set { _fundName = value.Replace(",", string.Empty).Replace("'", string.Empty); } }
        
        [Index(21)]
        
        public string FundValue {
            get 
            { 
                return _fundValue; 
            } 
            set 
            {
                _fundValue = value.Replace(",", string.Empty);
                
                //Parsing to double in order to set the number into 2 decimal format
                double rounded = 0;
                Double.TryParse(_fundValue, out rounded);
                _fundValue = String.Format("{0:0.00}", rounded);
            } 
        }

        [Index(22)]

        public string FundPerc
        {
            get 
            { 
                return _fundPerc; 
            } 
            set 
            { 
                _fundPerc = value.Replace(',', '.'); 
            }
        }

        [Index(25)]
        public string FundValueDate
        {
            get { return _fundValueDate; }
            set
            {
                //if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                if (DateTime.TryParse(value, out DateTime fundValDt))
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                else
                    _fundValueDate = value;
            }
        }
        [Optional]
        public string LISP { get { return _lisp; } set { _lisp = "Momentum"; } }

        
        //[Index(41)]
        //public string ProductType
        //{
        //    get;set;
        //}

        
        [Index(42)]
        public string StartDate
        {
            get { return _investmentStartDate; }
            set
            {
                //if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                if (DateTime.TryParse(value, out DateTime fundValDt))
                    _investmentStartDate = fundValDt.ToString("dd MMM yyyy");
                else
                    _investmentStartDate = value;
            }
        }
        [Optional]
        public bool HasErrors { get; set; }

        [Optional]
        public string ValidationErrors
        {
            get { return _validationErrors; }
            set
            {
                _validationErrors = value;
                if (!string.IsNullOrEmpty(value))
                    this.HasErrors = true;
            }
        }

        public void ValidateHeadings(HeaderValidatedArgs args)
        {
            //throw new NotImplementedException();
        }
    }

    public sealed class MomentumRecordMap : ClassMap<MomentumRecord>
    {
        public MomentumRecordMap()
        {

            /*Map(c => c.AccountNo);
            //Map(c => c.Product);
            Map(c => c.ProductType);
            Map(c => c.LISP).Default("Momentum");
            Map(c => c.FundCode);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundPerc);
            Map(c => c.FundValueDate);
            Map(c => c.Title);
            //Map(c => c.Firstname);
            Map(c => c.Lastname);
            Map(c => c.IDNumber);
            Map(c => c.StartDate);*/

            Map(c => c.Title);
            Map(c => c.Lastname);
            Map(c => c.IDNumber);
            //Map(c => c.DateOfBirth);
            Map(c => c.AccountNo);
            Map(c => c.LISP).Default("Momentum");
            Map(c => c.ProductType);
            Map(c => c.FundCode);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.FundPerc);
            Map(c => c.StartDate);

            Map(c => c.ValidationErrors)
              .Convert(r => {
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

        }
    }
}
