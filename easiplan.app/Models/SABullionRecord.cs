using Finx.App.Models;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using CsvHelper;

namespace Finx.App.Models
{
    public sealed class SABullionRecord : ICsvRecord
    {
        [Ignore]
        public int RowNo { get; set; }
        private string _idNo = "";
        private string _fundValue="";
        private string _fundValueDate;
        private string _lisp;

        [Index(3)]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
        public string Firstname { get; set; }
        [Optional]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
        public string IDNumber
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        [Optional]
        public string PassportNo
        {
            get; set;
        }
        [Optional]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
        public string FundName { get; set; }
        [Index(2)]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
        public string AccountNo { get; set; }
        [Index(8)]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
        public string FundValue {
            get { return _fundValue; } 
            set 
            {
                _fundValue = value.Replace(",", string.Empty);
            } 
        } 
        [Index(4)]
        [CsvHelper.Configuration.Attributes.Default("N/A")]
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
        public string LISP { get { return _lisp; } set { _lisp = "SA Bullion"; } }

        [Optional]
        public bool HasErrors { get; set; }

        [Optional]
        public string ValidationErrors { get; set; }

        public void ValidateHeadings(HeaderValidatedArgs args)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class SABullionRecordMap : ClassMap<SABullionRecord>
    {
        public SABullionRecordMap()
        {
            Map(c => c.AccountNo);
            //Map(c => c.LISP);
            Map(c => c.FundName);
            Map(c => c.FundValue);
            Map(c => c.FundValueDate);
            Map(c => c.Firstname);
            Map(c => c.IDNumber);
        }
    }
}
