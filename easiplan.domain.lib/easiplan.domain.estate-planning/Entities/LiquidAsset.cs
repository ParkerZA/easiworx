using my.domain.lib.core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.estate.MetaEntities
{
    [IgnoreAutoMap]
    public class LiquidAsset:BaseEntity<int>
    {
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual string ReferenceNo { get; set; }
        public virtual double Value { get; set; }

        public LiquidAsset()
        {

        }
    }

    [IgnoreAutoMap]
    public class EstateExpense : BaseEntity<int>
    {
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual string ReferenceNo { get; set; }
        public virtual double Value { get; set; }

        public EstateExpense()
        {

        }
    }
}
