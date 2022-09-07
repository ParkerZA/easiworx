using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace my.domain.lib.core.test.Models
{
    public class TestModel: EntityTypedId<int>
    {
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual string NonPersistedParam { get; set; }

        public virtual string PersistedParam { get; set; }
    }
}
