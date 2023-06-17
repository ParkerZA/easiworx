using FluentValidation;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
    public class RiskAdviceRecord : ClientAdviceRecord
    {

        #region Constructors

        //Standard constructor
        public RiskAdviceRecord()
        {
            #region Needs and Goals Table

            this.LifeInfo = new RiskNeedsAndGoalsTableRow();
            this.IncomeProtectionInfo = new RiskNeedsAndGoalsTableRow();
            this.LumpSumInfo = new RiskNeedsAndGoalsTableRow();
            this.TemporaryDisabilityInfo = new RiskNeedsAndGoalsTableRow();
            this.TraumaAndIllnessInfo = new RiskNeedsAndGoalsTableRow();
            this.FuneralCoverInfo = new RiskNeedsAndGoalsTableRow();
            this.OtherInfo = new RiskNeedsAndGoalsTableRow();

            #endregion
        }

        //Copy Constructor
        public RiskAdviceRecord(RiskAdviceRecord copy) : base(copy)
        {
            #region Needs and Goals Table
            
            this.LifeInfo = new RiskNeedsAndGoalsTableRow(copy.LifeInfo);
            this.IncomeProtectionInfo = new RiskNeedsAndGoalsTableRow(copy.IncomeProtectionInfo);
            this.LumpSumInfo = new RiskNeedsAndGoalsTableRow(copy.LumpSumInfo);
            this.TemporaryDisabilityInfo = new RiskNeedsAndGoalsTableRow(copy.TemporaryDisabilityInfo);
            this.TraumaAndIllnessInfo = new RiskNeedsAndGoalsTableRow(copy.TraumaAndIllnessInfo);
            this.FuneralCoverInfo = new RiskNeedsAndGoalsTableRow(copy.FuneralCoverInfo);
            this.OtherInfo = new RiskNeedsAndGoalsTableRow(copy.OtherInfo);
            
            #endregion
        }

        #endregion

        #region Needs and Goals Table methods

        public virtual RiskNeedsAndGoalsTableRow LifeInfo
        {
            get;
            set;
        }


        public virtual RiskNeedsAndGoalsTableRow IncomeProtectionInfo
        {
            get;
            set;
        }

        public virtual RiskNeedsAndGoalsTableRow LumpSumInfo
        {
            get;
            set;
        }

        public virtual RiskNeedsAndGoalsTableRow TemporaryDisabilityInfo
        {
            get;
            set;
        }

        public virtual RiskNeedsAndGoalsTableRow TraumaAndIllnessInfo
        {
            get;
            set;
        }

        public virtual RiskNeedsAndGoalsTableRow FuneralCoverInfo
        {
            get;
            set;
        }

        public virtual RiskNeedsAndGoalsTableRow OtherInfo
        {
            get;
            set;
        }

        #endregion
    }
}
