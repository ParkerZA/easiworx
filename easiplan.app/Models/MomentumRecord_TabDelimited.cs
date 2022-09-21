using Finx.App.Models;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;

namespace Finx.App.Models
{
    public sealed class MomentumRecord_TabDelimited : ICsvRecord
    {
        [Ignore]
        public int RowNo { get; set; }
        private string _idNo = "";
        private string _fundValue="";
        private string _fundValueDate;
        private string _lisp="Momentum";
        //private string _investmentStartDate;
        private string _fundName;
        private string _validationErrors;

        [Optional]
        public string Title { get; set; }

        [Index(3)]
        public string Initials { get; set; }
        
        [Index(4)]
        public string Firstname { get; set; }

        [Index(5)]
        public string AccountNo { get; set; }

        [Index(7)]
        public string Product
        {
            get; set;
        }

        [Index(9)]
        public string IDNumber
        {
            get { return _idNo; }
            set
            {
                _idNo = value.Replace("'", string.Empty);
            }
        }
        
        [Optional]
        public string PassportNo
        {
            get; set;
        }
      

        [Index(13)]
        public string FundCode { get; set; }
        
        [Index(14)]
        public string FundName { get { return _fundName; } set { _fundName = value.Replace(",", string.Empty).Replace("'", string.Empty); } }
       
        [Index(17)]
        
        public string FundValue {
            get { return _fundValue; } 
            set 
            {
                _fundValue = value.Replace(",", string.Empty);
            } 
        } 
        [Index(11)]
        
        public string FundValueDate
        {
            get { return _fundValueDate; }
            set
            {
                if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                else
                    _fundValueDate = value;
            }
        }
        [Optional]
        public string LISP { get { return _lisp; } set { _lisp = "Momentum"; } }


        [Index(29)]
        public string ProductType
        {
            get;set;
        }

        
        [Optional]
        public string StartDate
        {
            get;set;
            //get { return _investmentStartDate; }
            //set
            //{
            //    if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
            //        _investmentStartDate = fundValDt.ToString("dd MMM yyyy");
            //    else
            //        _investmentStartDate = value;
            //}
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
            throw new NotImplementedException();
        }
    }

    public sealed class MomentumRecord_TabDelimited_Map : ClassMap<MomentumRecord_TabDelimited>
    {
        public MomentumRecord_TabDelimited_Map()
        {
            Map(c => c.Title);
            Map(c => c.Firstname);
            Map(c => c.IDNumber);
            Map(c => c.AccountNo);
            Map(c => c.LISP).Default("Momentum");
            Map(c => c.Product);
            Map(c => c.ProductType);
            Map(c => c.FundCode);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            //Map(c => c.StartDate);

            Map(c => c.ValidationErrors)
              .Convert(r => {
                  var errors = new StringBuilder();

                  var idNumber = r.Row.GetField<string>("IDNumber");
                  var policyNo = r.Row.GetField<string>("AccountNo");
                  var fundName = r.Row.GetField<string>("FundName");
                  var fundValueDate = r.Row.GetField<string>("FundValueDate");

                  if (string.IsNullOrEmpty(idNumber))
                      errors.Append("ID Number is null!");

                  if (!Regex.IsMatch(idNumber, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
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
