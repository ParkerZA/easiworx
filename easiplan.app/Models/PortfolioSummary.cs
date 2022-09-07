using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    public class PortfolioSummary
    {
        public string Description { get; set; }
        public double TotalRetirement { get; set; }
        public double TotalInv { get; set; }
        public double TotalLife { get; set; }
        public double TotalMedical { get; set; }
        public double TotalIncomeAsset { get; set; }

        public double TotalEducation { get; set; }


    }

    public class Summary
    {
        public string Description { get; set; }
        public double Amount { get; set; }
    }
}
