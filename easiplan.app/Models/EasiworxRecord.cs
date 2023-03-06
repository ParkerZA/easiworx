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
        private string _dob = "";
        private string _monthlyPremium;
        private string _productType;
        

        //Sets row number as displayed on import screen
        [Ignore]
        public int RowNo 
        { 
            get; 
            set;
        }


        //Clients first name
        [Index(0)]
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


        //Clients last name
        [Index(1)]
        public string Lastname
        {
            get;
            set;
        }


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
                /* if (DateTime.TryParse(value, out DateTime dobDt))
                     _dob = dobDt.ToString("dd MMM yyyy");
                 else
                     _dob = value;*/

                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp.Replace(" ", string.Empty);

                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;
                    
                    DateTime dtDob;
                    if (DateTime.TryParseExact(strdt, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob) ||
                        DateTime.TryParseExact(strdt, "y-M-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDob))
                        {
                            _dob = dtDob.ToString("dd MMM yyyy");
                            Console.WriteLine(_dob);
                        }
                }
                else
                {
                    _dob = value;
                }
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

        }


        //Clients ID number
        [Index(3)] 
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


        //Clients registration number as provided by LISP 
        [Index(4)] 
        public string RegistrationNo
        {
            get;
            set;
        }


        //Clients passport number if ID number is absent
        [Index(5)]
        public string PassportNo
        {
            get; 
            set;
        }


        //Client number as provided by LISP
        [Index(6)]
        public string ClientNo
        {
            get; set;
        }


        //Street number of clients postal address
        [Optional]
        [Index(7)]
        public string PostalAddressStreetNo
        {
            get; 
            set;
        }


        //Road name for clients postal address
        [Optional]
        [Index(8)]
        public string PostalAddress
        {
            get;
            set;
        }


        //Suburb of clients postal address
        [Optional]
        [Index(9)]
        public string PostalSuburb
        {
            get; 
            set;
        }


        //Postal code for clients postal address
        [Optional]
        [Index(10)]
        public string PostalCode
        {
            get; 
            set;
        }


        //Street number for clients home address
        [Index(11)]
        [Optional]
        public string PhysicalAddressStreetNo
        {
            get; 
            set;
        }


        //Road name for clients home address
        [Index(12)]
        [Optional]
        public string PhysicalAddress
        {
            get; 
            set;
        }


        //Suburb for clients home address
        [Index(13)]
        [Optional]
        public string PhysicalAddressSuburb
        {
            get; 
            set;
        }


        //Clients home address postal code
        [Index(14)]
        [Optional]
        public string PhysicalAddressPostalCode
        {
            get; 
            set;
        }


        //Clients cell phone number
        [Optional]
        [Index(15)]
        public string CellNo
        {
            get; 
            set;
        }
        

        //Clients work telephone number
        [Optional]
        [Index(16)]
        public string OfficeTel
        {
            get; 
            set;
        }


        //Clients home telephone number
        [Optional]
        [Index(17)]
        public string HomeTel
        {
            get;
            set;
        }
        

        //Clients Email address
        [Optional]
        [Index(18)]
        public string EmailAddress
        {
            get; set;
        }


        //Name of policy owned by client
        [Index(19) ]
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
                else if (value.ToLower().Contains("retirement annuity option"))
                {
                    _productType = "Retirement Annuities";
                }
                else if (value.ToLower().Contains("investment platform unit trust") || value.ToLower().Contains("flexible investment option"))
                {
                    _productType = "Unit Trust";
                }
                else if (value.ToLower().Contains("preservation")&& value.ToLower().Contains("provident"))
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
                    _productType = value;
                }
            }
        }


        //Model portfolio
        [Index(20)]
        public string ModelPortfolio
        {
            get; 
            set;
        }


        //Clients surname gets assigned as account name
        [Index(21)]
        public string AccountName 
        { 
            get; 
            set; 
        }


        //Policy Number
        [Index(22)]  
        public string AccountNo 
        { 
            get;
            set; 
        }


        //Fund code
        [Index(23)] 
        public string FundCode 
        { 
            get;
            set;
        }


        //Name of fund
        [Index(24)] 
        public string FundName 
        { 
            get;
            set;
        }


        //Market value in rands
        [Index(25)] 
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


        //Date at which the fund value is pulled
        [Index(26)] 
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
                    //temp.Replace(" ", string.Empty);
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));
                    Console.WriteLine(temp);
                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;

                    DateTime dtFVD;
                    if (DateTime.TryParseExact(strdt, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD) ||
                        DateTime.TryParseExact(strdt, "y-M-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFVD))
                    {
                        _fundValueDate = dtFVD.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    _fundValueDate = value;
                }
            }
            /*{
                //if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fundValDt))
                if (DateTime.TryParse(value, out DateTime fundValDt))
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                else
                {
                    _fundValueDate = value;
                }
            }*/
        }


        //Split percentage for fund amounts
        [Optional]
        [Index(27)] 
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


        //Fund inception date
        [Index(28)] 
        public string InceptionDate
        {
            get 
            {
                return _startDate; 
            }
            set
            /*{
                //if (DateTime.TryParseExact(value, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtStartDt))
                if (DateTime.TryParse(value, out DateTime dtStartDt))
                    _startDate = dtStartDt.ToString("dd MMM yyyy");
                else
                {
                    _startDate = value;
                }
            }*/
            {
                if (value.Length > 8)
                {
                    String temp = value.Replace("/", string.Empty);
                    temp = String.Concat(temp.Where(c => !Char.IsWhiteSpace(c)));
                    var day = int.Parse(temp.Substring(0, 2));
                    var month = int.Parse(temp.Substring(2, 2));
                    var year = int.Parse(temp.Substring(4, 4));

                    var strdt = year + "-" + month + "-" + day;

                    DateTime dtSD;
                    if (DateTime.TryParseExact(strdt, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD) ||
                        DateTime.TryParseExact(strdt, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD) ||
                        DateTime.TryParseExact(strdt, "y-M-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSD))
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


        //Monthly Debit Order Premium
        [Optional] 
        [Index(29)]
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
                _monthlyPremium = _monthlyPremium.Replace(",",".");  
            }
        }

       
        //Name of LISP responsible for this policy
        [Optional]
        [Index(30)]
        public string LISP 
        { 
            get;
            set; 
        }


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
                if(!string.IsNullOrEmpty(value))
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


    
    public sealed class EasiworxRecordMap : ClassMap<EasiworxRecord>
    {
        public EasiworxRecordMap()
        {
            //Console.WriteLine("What does this even rite: " +Map(c=>c.Dob));
            Map(c => c.Firstname);
            Map(c => c.Lastname);
            Map(c => c.DateOfBirth);
            Map(c => c.IDNumber);
            Map(c => c.RegistrationNo);
            Map(c => c.PassportNo);
            Map(c => c.ClientNo);

            Map(c => c.PostalAddressStreetNo);
            Map(c => c.PostalAddress);
            Map(c => c.PostalSuburb);
            Map(c => c.PostalCode);

            Map(c => c.PhysicalAddressStreetNo);
            Map(c => c.PhysicalAddress);
            Map(c => c.PhysicalAddressSuburb);
            Map(c => c.PhysicalAddressPostalCode);

            Map(c => c.CellNo); 
            Map(c => c.OfficeTel); 
            Map(c => c.HomeTel);
            Map(c => c.EmailAddress);
            

            Map(c => c.ProductType);
            Map(c => c.ModelPortfolio);
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundCode);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.AccountFundAllocation);
            Map(c => c.InceptionDate);
            Map(c => c.MonthlyPremium);
            Map(c => c.LISP);


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

                        return errors.ToString();

                    });
            }).Wait();
        }
    }
}
