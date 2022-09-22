using System.Globalization;
using System.Text;

namespace easiplan.app.Models
{
    public interface ICsvConfiguration
    {
        CultureInfo CultureInfo { get;} 
        Encoding Encoding { get; set; }
        bool DetectDelimiter { get; set; }
        string[] Delimiters { get; set; }
        bool AllowComments { get; set; }
        bool DetectColumnCountChanges { get; set; }
        bool HasHeaderRecord { get; set; }
        int[] RecordsToSkip { get; set; }
        void ValidateCsvFileHeadings();
    }
}