using CsvHelper;
using CsvHelper.Configuration;
using easiplan.app.Interfaces;
using Finx.App.Interfaces;
using System;
using System.Globalization;
using System.Text;

namespace easiplan.app.Models
{
    public sealed class GenericCsvConfiguration<T> : ICsvConfiguration where T: ICsvRecord
    {
        private CsvConfiguration _csvConfiguration;
        private int[] _recordsToSkip;

        public CsvConfiguration CsvConfiguration { get { return _csvConfiguration; } }
        public GenericCsvConfiguration(CsvConfiguration csvConfiguration)
        {
            _csvConfiguration = csvConfiguration;
        }
        public CultureInfo CultureInfo 
        { 
            get { return _csvConfiguration.CultureInfo; }
        }
        public Encoding Encoding
        {
            get { return _csvConfiguration.Encoding; }
            set { _csvConfiguration.Encoding = value; } 
        }
        public bool DetectDelimiter 
        {
            get { return _csvConfiguration.DetectDelimiter; }
            set { _csvConfiguration.DetectDelimiter = value; }
        }
        public string[] Delimiters
        {
            get { return _csvConfiguration.DetectDelimiterValues; }
            set { _csvConfiguration.DetectDelimiterValues = value; }
        }
        public bool AllowComments
        {
            get { return _csvConfiguration.AllowComments; }
            set { _csvConfiguration.AllowComments = value; }
        }
        public bool DetectColumnCountChanges
        {
            get { return _csvConfiguration.DetectColumnCountChanges; }
            set { _csvConfiguration.DetectColumnCountChanges = value; }
        }
        public bool HasHeaderRecord
        {
            get { return _csvConfiguration.HasHeaderRecord; }
            set { _csvConfiguration.HasHeaderRecord = value; }
        }
        public int[] RecordsToSkip
        {
            get { return _recordsToSkip; }
            set {
                _csvConfiguration.ShouldSkipRecord = null;
            }
        }

        private bool ShouldSkipRecord(ShouldSkipRecordArgs args)
        {
            var record = args.Record;
            
            return false;
        }
        public void ValidateCsvFileHeadings()
        {
            throw new NotImplementedException();
        }
    }
}
