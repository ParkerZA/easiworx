using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Interfaces
{
    public interface ICsvRecord
    {
        int RowNo { get; set; }
        string IDNumber { get; set; }
        string PassportNo { get; set; }
        string LISP { get; set; }
        string AccountNo { get; set; } //this is the policyno

        string ProductType { get; set; }
        string FundCode { get; set; }
        string FundName { get; set; }
        string FundValue { get; set; }
        string FundValueDate { get; set; }
        bool HasErrors { get; set; }
        string ValidationErrors { get; set; }

        void ValidateHeadings(CsvHelper.HeaderValidatedArgs args);
    }
}
