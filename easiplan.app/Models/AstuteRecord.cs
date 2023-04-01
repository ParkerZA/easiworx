using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using easiplan.app.Extensions;
using easiplan.domain.Entities;
using Finx.App.Forms;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using my.domain.lib.core.Validation;
using System.Linq;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using DocumentFormat.OpenXml.EMMA;

namespace Finx.App.Models
{
    public sealed class AstuteRecord : ICsvRecord
    {
        private string _idNo = "";
        private string _fundValue = "";
        private string _fundValueDate = "";
        private string _fundAllocationPercentage;
        private string _startDate;
        private string _firstname;
        private string _lastname;
        private string _accountName;
        private string _validationErrors;
        private string _dob = "";
        private DateTime _birthday;
        private string _monthlyPremium;
        private string _productType;


        //Sets row number as displayed on import screen
        [Ignore]
        public int RowNo
        {
            get;
            set;
        }

        //ConcentProvider in csv
        [Index(0)]
        public string LISP
        {
            get;
            set;
        }


        //InvestorTitle in csv
        [Index(11)]
        public string Title
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value.ToLower().Trim();
                _lastname = _lastname.FormatEasiworxString();
            }
        }


        //InvestorFirstName in csv
        [Index(12)]
        public string Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                if (value.Contains(","))
                {

                    var fullnames = value.Split(',');
                    if (fullnames.Length > 1)
                    {
                        _firstname = fullnames[1].Replace("\"", string.Empty);
                        this.Lastname = fullnames[0].Replace("\"", string.Empty);
                        this.Lastname = this.Lastname.FormatEasiworxString();
                    }
                    else
                        _firstname = value.FormatEasiworxString();
                }
                else
                    _firstname = value.FormatEasiworxString();

                _firstname = _firstname.FormatEasiworxString();

            }
        }


        //InvestorSurname in csv
        [Index(13)]
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value.ToLower().Trim();
                _lastname = _lastname.FormatEasiworxString();
            }
        }


        //InvestorIDNumber in csv
        [Index(15)]
        public string IDNumber
        {
            get
            {
                return _idNo;
            }
            set
            {
                if (CsvFileHelper.IsPassportNo(value))
                    this.PassportNo = value;
                else
                    _idNo = CsvFileHelper.FixSAIDNo(value);
            }
        }


        public string PassportNo
        {
            get;

            set;
            
        }

        //ContractNumber in csv
        [Index(16)]
        public string AccountNo
        {
            get;
            set;
        }



        //ProductName in csv
        [Index(18)]
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
                    _productType = value.FormatEasiworxString();
                }
            }
        }


        //FundID in csv
        [Index(19)]
        public string FundCode
        {
            get;
            set;
        }


        //FundName in csv
        [Index(20)]
        public string FundName
        {
            get;
            set;
        }

        //ValueDate in csv
        [Index(21)]
        public string FundValueDate
        {
            get
            {
                return _fundValueDate;
            }
            set
            {
                try
                {
                    String temp = value.Replace("/", string.Empty);
                    //temp.Replace(" ", string.Empty);
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));

                    var strdt = "";


                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    strdt = year + "-" + month + "-" + day;


                    DateTime dtFVD;
                    if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD))
                    {
                        _fundValueDate = dtFVD.ToString("dd MMM yyyy");
                    }
                }
                catch (FormatException)
                {
                    _fundValueDate = "-";
                }


            }

        }

        //MarketValue in csv
        [Index(23)]
        public string FundValue
        {
            get
            {
                return _fundValue;
            }
            set
            {
                _fundValue = value.Replace(" ", string.Empty);
                _fundValue = _fundValue.Replace(",", ".");

            }
        }


        //InceptionEntryDate
        [Index(25)]
        public string InceptionDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));

                    var strdt = "";
                    try
                    {
                        var day = int.Parse(temp.Substring(0, 2));
                        var month = int.Parse(temp.Substring(2, 2));
                        var year = int.Parse(temp.Substring(4, 4));

                        strdt = year + "-" + month + "-" + day;
                    }
                    catch (FormatException)
                    {

                    }

                    DateTime dtSD;
                    if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD))
                    {
                        _startDate = dtSD.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    _startDate = value;
                }
            }
        }
        /*
                //Clients date of birth
                [Index(2)]
                public string DateOfBirth
                {

                    get
                    {
                        return _dob;
                    }
                    set
                    {
                        try
                        {
                            String temp = value.Replace("/", string.Empty);
                            temp.Replace(" ", string.Empty);
                            temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));
                            var strdt = "";

                            var day = int.Parse(temp.Substring(0, 2));
                            var month = int.Parse(temp.Substring(2, 2));
                            var year = int.Parse(temp.Substring(4, 4));
                            strdt = year + "-" + month + "-" + day;

                            DateTime dtDob;
                            if (DateTime.TryParseExact(strdt, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob) ||
                                DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob)
                                )
                            {
                                _birthday = dtDob;
                                _dob = dtDob.ToString("dd MMM yyyy");
                            }
                        }
                        catch (FormatException)
                        {
                            _dob = "-";
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            _dob = "-";
                        }
                    }

                }

                public DateTime getBirthday
                {
                    get { return _birthday; }
                }

                /*get { return _dob; }
                set
                {
                    //yyyy/mm/dd
                    if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime fundValDt))
                        _dob = fundValDt.ToString("dd MMM yyyy"); //27 Oct 2022
                    else
                        _dob = value;
                }*/



        /*
        //Split percentage for fund amounts
        [Optional]
        [Index(32)]
        public string AccountFundAllocation
        {
            get
            {
                return _fundAllocationPercentage;
            }
            set
            {
                _fundAllocationPercentage = value.Replace("%", string.Empty);
                _fundAllocationPercentage = _fundAllocationPercentage.Replace(",", ".");
            }
        }


        //Monthly Debit Order Premium
        [Optional]
        [Index(33)]
        public string MonthlyPremium
        {
            get
            {
                return _monthlyPremium;
            }
            set
            {
                //Console.WriteLine("ThE ONE is: " + _monthlyPremium);
                _monthlyPremium = value.Replace("R", string.Empty);
                _monthlyPremium = _monthlyPremium.Replace(" ", string.Empty);
                _monthlyPremium = _monthlyPremium.Replace(",", ".");
            }
        }
        */




        [Optional]
        public string ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
                if (!string.IsNullOrEmpty(value))
                    this.HasErrors = true;
            }
        }

        [Optional]
        public bool HasErrors
        {
            get;
            set;
        }

        public void ValidateHeadings(HeaderValidatedArgs args)
        {
            //throw new NotImplementedException();
        }
    }



    public sealed class AstuteRecordMap : ClassMap<EasiworxRecord>
    {
        public AstuteRecordMap()
        {

            Map(c => c.LISP);
            Map(c => c.Firstname);
            Map(c => c.Lastname);
            Map(c => c.IDNumber);
            Map(c => c.PassportNo);
            Map(c => c.ProductType);
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundCode);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            //Map(c => c.AccountFundAllocation);
            Map(c => c.InceptionDate);
            //Map(c => c.MonthlyPremium);

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
                        //var birthDate = r.Row.GetField<string>("BirthDate");

                        if (string.IsNullOrEmpty(idNumber))
                            errors.Append("ID Number is null!");

                        SaIdValidator validator = new SaIdValidator();
                        if (!(validator.Validate(idNumber)))
                        {
                            errors.Append("Invalid SA ID No!");
                        }

                        /*if (!Regex.IsMatch(idNumber, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
                            errors.Append("Invalid SA ID No!");*/

                        if (string.IsNullOrEmpty(policyNo))
                            errors.Append("Policy Number is null!");

                        if (string.IsNullOrEmpty(fundName))
                            errors.Append("Fund Name is null!");

                        if (string.IsNullOrEmpty(fundValueDate))
                            errors.Append("Fund Value Date is null!");

                        //if (string.IsNullOrEmpty(birthDate))
                          //  errors.Append("Birthdate is null!");

                        return errors.ToString();

                    });
            }).Wait();
        }
    }
}
