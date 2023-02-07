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
        private string _fundValue = "";
        private string _fundValueDate;
        private string _lisp = "Momentum";
        //private string _investmentStartDate;
        private string _fundName;
        private string _validationErrors;
        private string _passportNo;
        private string _product;
        private string _accountNo;
        private string _lastname;
        private string _initials;
        private string _title;
        private string _fundCode;
        private string _productType;


        //Policy Number
        [Index(3)]
        [Optional]
        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                _accountNo = _accountNo.Replace('"', ' ').Trim();
            }
        }


        [Index(4)]
        [Optional]
        public string Title
        {
            get { return _title; }
            set { _title = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _title = _title.Replace('"', ' ').Trim();
            }
        }

        [Index(5)]
        public string Initials
        {
            get { return _initials; }
            set { _initials = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _initials = _initials.Replace('"', ' ').Trim();
            }
        }

        [Index(6)]
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = (value.Replace("'", string.Empty.Replace("\"", string.Empty)));
                _lastname = _lastname.Replace('"', ' ').Trim();
            }
        }

        
        [Index(8)]
        public string ProductName
        {
            get { return _product; }
            set { _product = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                _product = _product.Replace('"', ' ').Trim();
            }
        }

        [Index(7)]
        public string IDNumber
        {
            get { return _idNo; }
            set { _idNo = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _idNo = _idNo.Replace('"', ' ').Trim();
            }
        }

        [Optional]
        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                _passportNo = _passportNo.Replace('"', ' ').Trim();
            }
        }

        //This should not be optional
        //[Index(13)]
        [Optional]
        public string FundCode
        {
            get { return _fundCode; }
            set { _fundCode = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _fundCode = _fundCode.Replace('"', ' ').Trim();
            }
        }

        [Index(18)]
        public string FundName
        {
            get { return _fundName; }
            set { _fundName = value.Replace(",", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty);
                _fundName = _fundName.Replace('"', ' ').Trim();
            }
        }

        [Index(21)]

        public string FundValue {
            get { return _fundValue; }
            set { _fundValue = value.Replace(",", string.Empty).Replace("\"", string.Empty); 
               _fundValue = _fundValue.Replace('"', ' ').Trim();
            }
        }
        
        [Index(25)] 
        [Optional]
        public string FundValueDate
        {
            get { return _fundValueDate; }
            set
            {
                var strfundValueDate = value.Replace("\"", string.Empty).Trim();
                if (DateTime.TryParseExact(strfundValueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
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
            get { return _productType; }
            set { _productType = value.Replace(",", string.Empty).Replace("\"", string.Empty);
                _productType = _productType.Replace('"', ' ').Trim();
            }
        }

        [Index(42)]
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
            Map(c => c.Lastname);
            Map(c => c.IDNumber);
            Map(c => c.AccountNo);
            Map(c => c.LISP).Default("Momentum");
            Map(c => c.ProductName);
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
