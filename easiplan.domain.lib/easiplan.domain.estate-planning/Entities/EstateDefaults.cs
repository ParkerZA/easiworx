using my.domain.lib.core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.estate.MetaEntities
{
    [IgnoreAutoMap]
    public class EstateDefaults: BaseEntity<int>
    {
        public virtual double ExecutorPercentage { get; set; } = 3.99D;

        public virtual double CgtPrimaryPropertyExclusion { get; set; } = 2000000D;
        public virtual double CgtDeathExclusion { get; set; } = 300000D;
        public virtual double CgtPercentage { get; set; } = 40D;

        public virtual double EstateDutyCeiling { get; set; } = 3500000.00D;
        public virtual double EstateDutyPercentage { get; set; } = 20D;
        public virtual double EstateDutyEx { get; set; } = 30000000.00D;
        public virtual double EstateDutyPercentageEx { get; set; } = 25D;

        public virtual double VAT { get; set; } = 15D;

    }
}
