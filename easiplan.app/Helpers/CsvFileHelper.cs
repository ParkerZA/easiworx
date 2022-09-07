using CsvFileImporter.CsvFile.Entities;
using CsvHelper;
using CsvHelper.Configuration;
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
        public static FileProperties GetFileProperties(string fileName, string fileSource = "SelfReported", string fileFormat = ".csv")
        {
            FileProperties fileProperties = null;
            try
            {

                if (string.IsNullOrEmpty(fileName))
                    throw new ApplicationException("Invalid filename!");

                if (!File.Exists(fileName))
                    throw new ApplicationException("File does not exist!");

                var fileInfo = new FileInfo(fileName);

                if (fileInfo.Extension.ToLower() != fileFormat.ToLower())
                    throw new ApplicationException("Invalid file format!");

                fileProperties = new FileProperties() { FileDate = fileInfo.CreationTime, Filename = fileInfo.Name, FileSize = fileInfo.Length, Filesource = fileSource, Format = fileInfo.Extension };

            }
            catch (Exception)
            {

                throw;
            }
            return fileProperties;
        }

        //public async static Task<List<CamissaRecord>> GetRecords(string filePath, CancellationToken cancellationToken, string delimiter = "|")
        //{
        //    List<CamissaRecord> fileRecordList = null;
        //    try
        //    {
        //        //Thread.Sleep(10000);
        //        var csvHelperConfiguration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8, Delimiter = delimiter };
        //        using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
        //            using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
        //            {
        //                var fileRecords = csvReader.GetRecordsAsync<CamissaRecord>(cancellationToken);
        //                var RowNo = 0;
        //                if (fileRecords != null)
        //                    fileRecordList = new List<CamissaRecord>();

        //                await foreach (CamissaRecord fileRecord in fileRecords)
        //                {
        //                    RowNo++;
        //                    fileRecord.RowNo = RowNo;
        //                    //var fundValueProperty = fileRecord.GetType().GetProperty("FundValue", System.Reflection.BindingFlags.Public);
        //                    //var displayAttributes = (DisplayAttribute[])fundValueProperty.GetCustomAttributes(typeof(DisplayAttribute), true);
                            
        //                    fileRecordList.Add(fileRecord);
        //                }
        //            }
        //        }

        //    }
        //    catch (OperationCanceledException)
        //    {
        //        //log that operation was cancelled
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return fileRecordList;
        //}

        //public async static Task<List<ICsvRecord>> GetRecords(Type RecordType, string filePath, CancellationToken cancellationToken, string delimiter = "|")
        //{
        //    IAsyncEnumerable<ICsvRecord> fileRecords = null;
        //    List<ICsvRecord> fileRecordList = null;
        //    try
        //    {
        //        //Thread.Sleep(10000);
        //        var csvHelperConfiguration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        //        { Encoding = Encoding.UTF8, Delimiter = delimiter, IgnoreBlankLines = true, AllowComments = false, IgnoreReferences = true);
        //        using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
        //            using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
        //            {
                        
        //                if(RecordType.GetType() == typeof(CamissaRecord))
        //                    fileRecords = csvReader.GetRecordsAsync<CamissaRecord>(cancellationToken);

        //                if (RecordType.GetType() == typeof(AlanGrayRecord))
        //                    fileRecords = csvReader.GetRecordsAsync<AlanGrayRecord>(cancellationToken);

        //                if (RecordType.GetType() == typeof(NedgroupRecord))
        //                    throw new NotImplementedException(); //fileRecords = csvReader.GetRecordsAsync<NedgroupRecord>(cancellationToken);

        //                if (RecordType.GetType() == typeof(MomentumRecord))
        //                    fileRecords = csvReader.GetRecordsAsync<MomentumRecord>(cancellationToken);

        //                var RowNo = 0;
        //                if (fileRecords != null)
        //                    fileRecordList = new List<ICsvRecord>();

        //                await foreach (ICsvRecord fileRecord in fileRecords)
        //                {
        //                    RowNo++;
        //                    fileRecord.RowNo = RowNo;
        //                    //var fundValueProperty = fileRecord.GetType().GetProperty("FundValue", System.Reflection.BindingFlags.Public);
        //                    //var displayAttributes = (DisplayAttribute[])fundValueProperty.GetCustomAttributes(typeof(DisplayAttribute), true);

        //                    fileRecordList.Add(fileRecord);
        //                }
        //            }
        //        }

        //    }
        //    catch (OperationCanceledException)
        //    {
        //        //log that operation was cancelled
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return fileRecordList;
        //}

        //public static Task<List<ICsvRecord>> GetRecords<T>(string filePath, CsvConfiguration csvHelperConfiguration, CancellationToken cancellationToken) where T:ICsvRecord
        //{
        //    IAsyncEnumerable<T> fileRecords = null;
        //    List<ICsvRecord> fileRecordList = null;
        //    try
        //    {
        //        //Thread.Sleep(10000);
        //        using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
        //            using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
        //            {
        //                ///TODO: Cleanup files unwanted header text for Nedgroup file 

        //                fileRecords = csvReader.GetRecordsAsync<T>(cancellationToken);

        //                var RowNo = 0;
        //                if (fileRecords != null)
        //                    fileRecordList = new List<ICsvRecord>();

        //                await foreach (ICsvRecord fileRecord in fileRecords)
        //                {
        //                    RowNo++;
        //                    fileRecord.RowNo = RowNo;
        //                    //var fundValueProperty = fileRecord.GetType().GetProperty("FundValue", System.Reflection.BindingFlags.Public);
        //                    //var displayAttributes = (DisplayAttribute[])fundValueProperty.GetCustomAttributes(typeof(DisplayAttribute), true);

        //                    fileRecordList.Add(fileRecord);
        //                }
        //            }
        //        }

        //    }
        //    catch (OperationCanceledException)
        //    {
        //        //log that operation was cancelled
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return fileRecordList;
        //}

        public static List<ICsvRecord> GetRecords<T>(string filePath, CsvConfiguration csvHelperConfiguration) where T : ICsvRecord
        {
            IEnumerable<T> fileRecords = null;
            List<ICsvRecord> fileRecordList = null;
            try
            {
                //Thread.Sleep(10000);
                using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                    using (var csvReader = new CsvReader(streamReader, csvHelperConfiguration))
                    {
                        ///TODO: Cleanup files unwanted header text for Nedgroup file 

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
            catch (OperationCanceledException)
            {
                //log that operation was cancelled
                throw;
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
    }
}
