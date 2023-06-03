using FluentValidation;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
    public class RiskAdviceRecord : ClientAdviceRecord
    {











        public RiskAdviceRecord()
        {


        }

        public RiskAdviceRecord(RiskAdviceRecord copy) : base(copy)
        {

        }
    }
}
