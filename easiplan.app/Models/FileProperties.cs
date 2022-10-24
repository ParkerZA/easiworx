using Finx.App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvFileImporter.CsvFile.Entities
{
    public struct FileProperties
    {
        public int? ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime FileDate { get; set; }
        public long FileSize { get; set; }
        public FileFormat FileFormat { get; set; }
        public string LastImportUser { get; set; }
        public DateTime LastImportDate { get; set; }
        public int NewClientTotal { get; set; }
        public int ExistingClientTotal { get; set; }
        public int ValidationErrorTotal { get; set; }

        public static FileFormat GetFileFormat(string fileExtension)
        {
            Enum.TryParse(FileFormat.GetNames(typeof(FileFormat)).ToList().Where(l => l == fileExtension).FirstOrDefault(),out FileFormat fileFormat);
            return fileFormat;
        }
    }
}
