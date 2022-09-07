using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.estate.Entities
{

    public class OtherDeduction : BaseEntity<int>
    {
        public virtual string Type { get;set;}
        public virtual string Description { get; set; }
        public virtual double Value { get; set; }

        public OtherDeduction()
        {
            Type = "Donations To PBO";
        }
     
    }

}
