using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Helpers;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Finx.App.Models
{

    public sealed class CamissaRecord : ICsvRecord
    {
        private string _idNo;
        private string _fundValue;
        private string _premium;
        private string _fundValueDate;
        private string _lisp="Camissa";
        private string _dob;
        private string _investmentStartDate;
        private string _workTel;
        private string _homeTel;
        private string _cellphone;
        private string _faxNo;
        private string _title;
        private string _firstname;
        private string _lastname;
        private string _fundName;
        private string _validationErrors;


        //Row number which appears next to record on import screen
        [Ignore]
        public int RowNo 
        { 
            get;
            set;
        }


        //LISP responsible for this record
        [Optional]
        public string LISP
        {
            get 
            { 
                return _lisp; 
            }
            set 
            { 
                _lisp = "Camissa"; 
            }
        }


        //Policy number associated with this fund
        [Index(2)]
        public string AccountNo 
        { 
            get;
            set;
        }
        
        
        //Name of fund
        [Index(3)]
        public string FundName 
        {
            get 
            {
                return _fundName; 
            }
            set
            {
                if (value.ToLower() == "kagiso islamic high yield fund")
                {
                    _fundName = "Camissa Islamic High Yield Fund";
                }
                else
                {
                    _fundName = value.Replace("kagiso", "Camissa").Replace("class a", string.Empty).Trim();
                }
            }
        }


        //Monthly debit order
        [Index(5)]
        public string MonthlyPremium
        {
            get 
            { 
                return _premium; 
            }
            set
            {
                if (value.ToLower().Contains("none"))
                {
                    _premium = "0";
                }
                else if (value.StartsWith("R"))
                {
                    _premium = value.Substring(1);
                }
                else
                {
                    _premium = value;
                }
            }
        }


        //Total amount which the fund is worth
        [Index(6)]
        public string FundValue
        {
            get 
            { 
                return _fundValue; 
            }
            set
            {
                _fundValue = value.Replace(",", string.Empty);
            }
        }


        //Date at which the fund value was pulled
        [Index(7)]
        public string FundValueDate
        {
            get 
            { 
                return _fundValueDate; 
            }
            set
            {
                //if (DateTime.TryParse(value, out DateTime fundValDt))
                if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime fundValDt))
                {
                    _fundValueDate = fundValDt.ToString("dd MMM yyyy");
                }
                else
                {
                    _fundValueDate = value;
                }
            }
        }


        //Starting date of the investment
        [Index(8)]
        public string InvestmentStartDate
        {
            get 
            { 
                return _investmentStartDate; 
            }
            set
            {
                if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime investmentStartDt))
                {    //if (DateTime.TryParse(value, out DateTime investmentStartDt))
                    _investmentStartDate = investmentStartDt.ToString("dd MMM yyyy");
                }
                else
                {
                    _investmentStartDate = value;
                }
            }
        }


        //Clients title (eg: Mr, Mrs)
        [Index(10)]
        public string Title 
        { 
            get 
            { 
                return _title; 
            } 
            set 
            { 
                _title = value; 
            } 
        }
       
        
        //First name of client
        [Index(11)]
        public string Firstname 
        { 
            get 
            { 
                return _firstname;
            } 
            set 
            {
                _firstname = value.Replace("(TFI)","").Trim(); 
            } 
        }


        //Second name of client
        [Index(13)]
        public string Lastname 
        { 
            get 
            { 
                return _lastname; 
            } 
            set 
            { 
                _lastname = value.Replace("(TFI)", "").Trim(); 
            } 
        }


        //Investor type
        [Index(14)]
        public string InvestorType
        {
            get;
            set;
        }
        

        //Client ID number
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
                {
                    this.PassportNo = value;
                }
                else
                {
                    _idNo = CsvFileHelper.FixSAIDNo(value);
                }
            }
        }


        //Clients passport number
        [Index(16)]
        public string PassportNo
        {
            get;
            set;
        }


        //Companies registration number
        [Index(17)]
        public string CompanyRegistrationNo
        {
            get; 
            set;
        }


        //Clients tax number
        [Index(18)]
        public string TaxNo
        {
            get; 
            set;
        }


        //Clients date of birth
        [Index(20)]
        public string DateOfBirth
        {
            get { return _dob; }
            set
            {
                //yyyy/mm/dd
                if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime fundValDt))
                {
                    _dob = fundValDt.ToString("dd MMM yyyy"); //27 Oct 2022
                }
                else
                {
                    _dob = value;
                }
            }
        }


        //Restriction
        [Index(23)]
        public string Restriction
        {
            get;
            set;
        }


        //Clients email address
        [Index(31)]
        public string EmailAddress
        {
            get;
            set;
        }


        //Clients office telephone number
        [Index(32)]
        public string WorkTelephone
        {
            get 
            { 
                return _workTel; 
            }
            set
            {
                _workTel = value.Replace("'", string.Empty);
            }
        }


        //Clients home telephone number
        [Index(33)]
        public string HomeTelephone
        {
            get 
            { 
                return _homeTel;
            }
            set
            {
                _homeTel = value.Replace("'", string.Empty);
            }
        }


        //Clients cellphone number
        [Index(34)]
        public string Cellphone
        {
            get 
            { 
                return _cellphone ; 
            }
            set
            {
                _cellphone = value.Replace("'", string.Empty);
            }
        }

        //Clients fax number
        [Index(35)]
        public string FaxNumber
        {
            get 
            { 
                return _faxNo; 
            }
            set
            {
                _faxNo = value.Replace("'", string.Empty);
            }
        }


        // Clients postal address street number
        [Index(36)]
        public string PostalAddress1
        {
            get; 
            set;
        }


        //Clients postal address road name
        [Index(37)]
        public string PostalAddress2
        {
            get;
            set;
        }


        //Clients postal address suburb name
        [Index(38)]
        public string PostalAddress3
        {
            get;
            set;
        }


        //Additional postal address information
        [Index(39)]
        public string PostalAddress4
        {
            get;
            set;
        }


        //Additional postal address information
        [Index(40)]
        public string PostalAddress5
        {
            get; 
            set;
        }


        //Additional postal address information
        [Index(41)]
        public string PostalAddress6
        {
            get; 
            set;
        }


        //Clients postal address postal code
        [Index(42)]
        public string PostalCode
        {
            get;
            set;
        }


        //Clients home address street number
        [Index(43)]
        public string PhysicalAddress1
        {
            get;
            set;
        }


        //Clients home address road name
        [Index(44)]
        public string PhysicalAddress2
        {
            get;
            set;
        }


        //Clients home address suburb name
        [Index(45)]
        public string PhysicalAddress3
        {
            get;
            set;
        }


        //Additional home address information
        [Index(46)]
        public string PhysicalAddress4
        {
            get; 
            set;
        }


        //Additional home address information
        [Index(47)]
        public string PhysicalAddress5
        {
            get;
            set;
        }


        //Additional home address information
        [Index(48)]
        public string PhysicalAddress6
        {
            get;
            set;
        }


        //Clients home address postal code
        [Index(49)]
        public string PhysicalAddressPostalCode
        {
            get; 
            set;
        }


        [Optional]
        public bool HasErrors 
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
                if (!string.IsNullOrEmpty(value))
                {
                    this.HasErrors = true;
                }
            }
        }

        public void ValidateHeadings(HeaderValidatedArgs args)
        {
            //throw new NotImplementedException();
        }
    }

    public sealed class CamissaRecordMap : ClassMap<CamissaRecord>
    {
        public CamissaRecordMap()
        {
            Map(c => c.LISP).Default("Camissa");
            Map(c => c.AccountNo);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.MonthlyPremium);
            Map(c => c.InvestmentStartDate);

            Map(c => c.Title);
            Map(c => c.Firstname);
            Map(c => c.Lastname);
            Map(c => c.InvestorType);
            Map(c => c.IDNumber);
            Map(c => c.PassportNo);
            
            Map(c => c.CompanyRegistrationNo);
            Map(c => c.TaxNo);

            Map(c => c.DateOfBirth);
            Map(c => c.Restriction);
            
            Map(c => c.PostalAddress1);
            Map(c => c.PostalAddress2);
            Map(c => c.PostalAddress3);
            Map(c => c.PostalAddress4);
            Map(c => c.PostalAddress5);
            Map(c => c.PostalAddress6);
            Map(c => c.PostalCode);

            Map(c => c.PhysicalAddress1);
            Map(c => c.PhysicalAddress2);
            Map(c => c.PhysicalAddress3);
            Map(c => c.PhysicalAddress4);
            Map(c => c.PhysicalAddress5);
            Map(c => c.PhysicalAddress6);
            Map(c => c.PhysicalAddressPostalCode);

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
