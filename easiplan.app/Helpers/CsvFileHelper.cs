using CsvFileImporter.CsvFile.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Finx.App.Enums;
using Finx.App.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finx.App.Helpers
{
    public class CsvFileHelper
    {
        public static FileProperties GetFileProperties(string fileName, FileFormat fileFormat = FileFormat.Csv)
        {
            FileProperties fileProperties;
            try
            {

                if (string.IsNullOrEmpty(fileName))
                    throw new ApplicationException("Invalid filename!");

                if (!File.Exists(fileName))
                    throw new ApplicationException("File does not exist!");

                var fileInfo = new FileInfo(fileName);

                if (fileInfo.Extension.ToLower().Replace(".",string.Empty) != fileFormat.ToString().ToLower())
                    throw new ApplicationException("Invalid file format!");

                fileProperties = new FileProperties() { 
                    FileDate = fileInfo.CreationTime, 
                    FileName = fileInfo.Name, 
                    FileSize = fileInfo.Length, 
                    FileFormat = FileProperties.GetFileFormat(fileInfo.Extension) };

            }
            catch (Exception)
            {

                throw;
            }
            return fileProperties;
        }
        public static List<ICsvRecord> GetRecords<T>(string filePath, CsvConfiguration csvHelperConfiguration) where T : ICsvRecord
        {
            IEnumerable<T> fileRecords = null;
            List<ICsvRecord> fileRecordList = null;
            try
            {
                using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                    {
                        using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
                        {
                            fileRecords = csvReader.GetRecords<T>();

                            var RowNo = 0;
                            if (fileRecords != null)
                                fileRecordList = new List<ICsvRecord>();

                            foreach (ICsvRecord fileRecord in fileRecords)
                            {
                                RowNo++;
                                fileRecord.RowNo = RowNo;
                                fileRecordList.Add(fileRecord);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                if (ex.InnerException != null)
                    msg = ex.InnerException.ToString();
                throw;
            }

            return fileRecordList;
        }
        public static List<ICsvRecord> GetRecords<T>(string filePath, CsvConfiguration csvHelperConfiguration,string Lisp) where T : ICsvRecord
        {
            IEnumerable<T> fileRecords = null;
            List<ICsvRecord> fileRecordList = null;
            try
            {
                if (Lisp.ToLower() == "easiworxtemplate")
                    Lisp = "Easiworx";

                using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                    {
                        using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
                        {
                            fileRecords = csvReader.GetRecords<T>();

                            var RowNo = 0;
                            if (fileRecords != null)
                                fileRecordList = new List<ICsvRecord>();

                            foreach (ICsvRecord fileRecord in fileRecords)
                            {
                                RowNo++;
                                fileRecord.RowNo = RowNo;
                                //fileRecord.LISP = Lisp;
                                fileRecordList.Add(fileRecord);
                                
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                if (ex.InnerException != null)
                    msg = ex.InnerException.ToString();
                throw;
            }

            return fileRecordList;
        }
        public static string DetectDelimiter(StreamReader reader,string[] possibleDelimiters)
        {
            var headerLine = reader.ReadLine();

            // reset the reader to initial position for outside reuse
            // Eg. Csv helper won't find header line, because it has been read in the Reader
            reader.BaseStream.Position = 0;
            reader.DiscardBufferedData();

            foreach (var possibleDelimiter in possibleDelimiters)
            {
                if (headerLine.Contains(possibleDelimiter))
                    return possibleDelimiter;
            }
            return possibleDelimiters[0];
        }
        public static string CamelCase(string s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return "null";
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
        }
        public async static Task<byte[]> MD5Hash(string filePath)
        {
            byte[] md5Hash;
            using (var streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                var fileString = await streamReader.ReadToEndAsync();
                var fileBytes = UTF8Encoding.UTF8.GetBytes(fileString);
                var md5 = MD5.Create();
                md5Hash = md5.ComputeHash(fileBytes);
            }
            return md5Hash;
        }

        public static bool IsPassportNo(string value)
        {
            var result = false;
            var cleanValue = value.Replace("'", string.Empty).Trim();
            if (!string.IsNullOrEmpty(cleanValue) && cleanValue.Length >= 6 && cleanValue.Length <= 9) //this is most likely a passport no
                result = true;
            return result;
        }
        public static string FixSAIDNo(string value)
        {
            var result = "";
            var cleanIdNo = value.Replace("'", string.Empty).Trim();

            if (cleanIdNo.Length == 12)
                result = "0" + cleanIdNo; //prepend a 0 to id nos that are only 12 chars long
            else
                result = cleanIdNo;

            return result;
        }
    }
}
