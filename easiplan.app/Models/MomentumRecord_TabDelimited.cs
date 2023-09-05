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
using my.domain.lib.core.Validation;
using System.Linq;

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
        private string _investmentStartDate;
        private string _birthDate;
        private string _fundName;
        private string _validationErrors;
        private string _passportNo="";
        private string _accountNo;
        private string _lastname;
        private string _initials;
        private string _title;
        private string _fundCode;
        private string _fundPerc;
        private string _productType;
       

        //Policy Number
        [Index(3)]
        [Optional]
        public string AccountNo
        {
            get 
            { 
                return _accountNo; 
            }
            set 
            { 
                _accountNo = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                _accountNo = _accountNo.Replace('"', ' ').Trim();
            }
        }


        //Clients Title (Mr/Mrs)
        [Index(4)]
        [Optional]
        public string Title
        {
            get 
            { 
                return _title; 
            }
            set 
            { 
                _title = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _title = _title.Replace('"', ' ').Trim();
            }
        }

        public static string CapitalizeSentence(string sentence)
        {
            string[] words = sentence.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length > 0)
                {
                    char firstLetter = char.ToUpper(word[0]);
                    string restOfWord = word.Substring(1);
                    words[i] = firstLetter + restOfWord;
                }
            }
            return string.Join(" ", words);
        }

        //Clients first name initial
        [Index(5)]
        public string Initials
        {
            get 
            { 
                return _initials; 
            }
            set 
            { 
                _initials = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _initials = _initials.Replace('"', ' ').Trim();
            }
        }


        //Clients last name
        [Index(6)]
        public string Lastname
        {
            get 
            { 
                return _lastname; 
            }
            set 
            { 
                _lastname = (value.Replace("'", string.Empty.Replace("\"", string.Empty)));
                _lastname = _lastname.Replace('"', ' ').Trim().ToLower();
                _lastname = CapitalizeSentence(_lastname);
            }
        }

        
        //Type of policy 
        [Index(8)]
        public string ProductType
        {
            get 
            { 
                return _productType; 
            }
            set 
            {
                 if (value.ToLower().Contains("retirement income option") || value.ToLower().Contains("glacier living annuity"))
                 {
                    _productType = "Living Annuity";
                 }
                 else if (value.ToLower().Contains("retirement annuity option") || value.ToLower().Contains("retirement annuity fund"))
                 {
                   _productType = "Retirement Annuities";
                 }
                 else if (value.ToLower().Contains("investment platform unit trust") || value.ToLower().Contains("flexible investment option") || value.ToLower().Contains("investment plan"))
                 {
                   _productType = "Unit Trust";
                 }
                 else if (value.ToLower().Contains("preservation") && value.ToLower().Contains("provident"))
                 {
                   _productType = "Provident/Preservation Funds";
                 }
                 else if (value.ToLower().Contains("tax-free"))
                 {
                   _productType = "Tax Free";
                 }
                 else if (value.ToLower().Contains("pension preservation fund"))
                 {
                   _productType = "Pension/Preservation Fund";
                 }
                 else if (value.ToLower().Contains("flexible endowment option"))
                 {
                   _productType = "Endowment";
                 }
                 else
                 {
                   _productType = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                   _productType = _productType.Replace('"', ' ').Trim();
                 }
                
            }
        }


        //Clients ID number
        [Index(7)]
        public string IDNumber
        {
            get 
            { 
                return _idNo; 
            }
            set 
            {
                string temp;
                temp = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                temp = temp.Replace('"', ' ').Trim();
                if (CsvFileHelper.IsPassportNo(temp))
                        this.PassportNo = temp;
                else
                      _idNo = CsvFileHelper.FixSAIDNo(temp);
                
            }
        }


        //Clients date of birth, not currently supplied by momentum
        [Optional]
        public string DateOfBirth
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp.Replace(" ", string.Empty);
                    temp.Replace("'", string.Empty.Replace("\"", string.Empty));
                    temp.Replace('"', ' ').Trim();
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));

                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;

                    DateTime dtDob;
                    if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob)
                        )
                    {
                        _birthDate = dtDob.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    _birthDate = value;
                }
            }
        }

        //Clients passport number, used in place of ID for foreign nationals
        [Optional]
        public string PassportNo
        {
            get 
            { 
                return _passportNo; 
            }
            set 
            { 
                _passportNo = value.Replace("'", string.Empty.Replace("\"", string.Empty));
                _passportNo = _passportNo.Replace('"', ' ').Trim();
            }
        }

        //This should not be optional
        //[Index(13)]
        [Index(17)]
        public string FundCode
        {
            get 
            { 
                return _fundCode; 
            }
            set 
            { 
                _fundCode = value.Replace("'", string.Empty.Replace("\"", string.Empty)); 
                _fundCode = _fundCode.Replace('"', ' ').Trim();
            }
        }


        //Name of fund
        [Index(18)]
        public string FundName
        {
            get 
            { 
                return _fundName; 
            }
            set 
            { 
                _fundName = value.Replace(",", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty);
                _fundName = _fundName.Replace('"', ' ').Trim();
            }
        }


        //Total amount that the fund is worth
        [Index(21)]
        public string FundValue 
        {
            get 
            { 
                return _fundValue; 
            }
            set 
            { 
                _fundValue = value.Replace(",", ".").Replace("\"", string.Empty); 
               _fundValue = _fundValue.Replace('"', ' ').Trim();

                
            }
        }
       
        
        //Split percentage for fund amounts
        [Index(22)]
        public string AccountFundAllocation {
            get 
            { 
                return _fundPerc; 
            }
            set 
            { 
                _fundPerc = value.Replace(',', '.');
                _fundPerc = _fundPerc.Replace('"', ' ').Trim();
            }
        }


        //Date at which the fund value was pulled
        [Index(25)] 
        [Optional]
        public string FundValueDate
        {
            get 
            { 
                return _fundValueDate; 
            }
            set
            {
                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp = temp.Replace('"', ' ' ).Trim();
                    temp =temp.Replace("\"", string.Empty).Trim();
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));
                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;

                    DateTime dtFVD;
                    if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD))
                    {
                        _fundValueDate = dtFVD.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    _fundValueDate = value;
                }
            }
        }


        //LISP for this investment. Defaulted to Momentum
        [Optional]
        public string LISP 
        { 
            get 
            { 
                return _lisp; 
            } 
            set 
            { 
                _lisp = "Momentum"; 
            } 
        }



        [Index(42)]
        [Optional]
        public string StartDate
        {
            get
            {
                return _investmentStartDate;
            }
            set
            {
                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp = temp.Replace("\"", string.Empty).Trim();
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));
                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;

                    DateTime dtSD;
                    if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD))
                    {
                        _investmentStartDate = dtSD.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    _investmentStartDate = value;
                }
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
            Map(c => c.DateOfBirth);
            Map(c => c.AccountNo);
            Map(c => c.LISP).Default("Momentum");
            Map(c => c.ProductType);
            Map(c => c.PassportNo);
            Map(c => c.FundCode);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.AccountFundAllocation);
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
                  /*
                  if (!Regex.IsMatch(idNumber, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
                      errors.Append("Invalid RSA ID Number!");
                  */
                  SaIdValidator validator = new SaIdValidator();
                  if (!(validator.Validate(idNumber)))
                  {
                      errors.Append("Invalid SA ID No!");
                  }

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
