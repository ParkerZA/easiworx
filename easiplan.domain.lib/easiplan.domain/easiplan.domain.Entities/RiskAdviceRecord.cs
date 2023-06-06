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

        #region Life private variables

        private string _lifeNeedsQuantified;
        private string _lifeNeedsPriority;
        private string _lifeNeedAddressed;
        private string _lifeShortfall;
        private string _lifeReviewDate;

        #endregion

        #region Permanent Disability (Income Protection) private variables

        private string _incomeProtectionNeedsQuantified;
        private string _incomeProtectionNeedsPriority;
        private string _incomeProtectionNeedAddressed;
        private string _incomeProtectionShortfall;
        private string _incomeProtectionReviewDate;

        #endregion

        #region Permanent Disability (Lump Sum) private variables

        private string _lumpSumNeedsQuantified;
        private string _lumpSumNeedsPriority;
        private string _lumpSumNeedAddressed;
        private string _lumpSumShortfall;
        private string _lumpSumReviewDate;

        #endregion

        #region Temporary Disability private variables

        private string _temporaryDisabilityNeedsQuantified;
        private string _temporaryDisabilityNeedsPriority;
        private string _temporaryDisabilityNeedAddressed;
        private string _temporaryDisabilityShortfall;
        private string _temporaryDisabilityReviewDate;

        #endregion

        #region Trauma/Illness private variables

        private string _traumaAndIllnessNeedsQuantified;
        private string _traumaAndIllnessNeedsPriority;
        private string _traumaAndIllnessNeedAddressed;
        private string _traumaAndIllnessShortfall;
        private string _traumaAndIllnessReviewDate;

        #endregion

        #region Funeral Cover/Immediate Expenses private variables

        private string _funeralCoverNeedsQuantified;
        private string _funeralCoverNeedsPriority;
        private string _funeralCoverNeedAddressed;
        private string _funeralCoverShortfall;
        private string _funeralCoverReviewDate;

        #endregion

        #region Other private variables

        private string _otherNeedsQuantified;
        private string _otherNeedsPriority;
        private string _otherNeedAddressed;
        private string _otherShortfall;
        private string _otherReviewDate;

        #endregion

        #region Constructors

        public RiskAdviceRecord()
        {


        }

        public RiskAdviceRecord(RiskAdviceRecord copy) : base(copy)
        {
            this._lifeNeedsQuantified =copy._lifeNeedsQuantified;
            this._lifeNeedsPriority=copy._lifeNeedsPriority;
            this._lifeNeedAddressed=copy._lifeNeedAddressed;
            this._lifeShortfall=copy._lifeShortfall;
            this._lifeReviewDate=copy._lifeReviewDate;

            this._incomeProtectionNeedsQuantified = copy._incomeProtectionNeedsQuantified;
            this._incomeProtectionNeedsPriority = copy._incomeProtectionNeedsPriority;
            this._incomeProtectionNeedAddressed = copy._incomeProtectionNeedAddressed;
            this._incomeProtectionShortfall = copy._incomeProtectionShortfall;
            this._incomeProtectionReviewDate = copy._incomeProtectionReviewDate;

            this._lumpSumNeedsQuantified = copy._lumpSumNeedsQuantified;
            this._lumpSumNeedsPriority = copy._lumpSumNeedsPriority;
            this._lumpSumNeedAddressed = copy._lumpSumNeedAddressed;
            this._lumpSumShortfall = copy._lumpSumShortfall;
            this._lumpSumReviewDate = copy._lumpSumReviewDate;

            this._temporaryDisabilityNeedsQuantified = copy._temporaryDisabilityNeedsQuantified;
            this._temporaryDisabilityNeedsPriority = copy._temporaryDisabilityNeedsPriority;
            this._temporaryDisabilityNeedAddressed = copy._temporaryDisabilityNeedAddressed;
            this._temporaryDisabilityShortfall = copy._temporaryDisabilityShortfall;
            this._temporaryDisabilityReviewDate = copy._temporaryDisabilityReviewDate;

            this._traumaAndIllnessNeedsQuantified = copy._traumaAndIllnessNeedsQuantified;
            this._traumaAndIllnessNeedsPriority = copy._traumaAndIllnessNeedsPriority;
            this._traumaAndIllnessNeedAddressed = copy._traumaAndIllnessNeedAddressed;
            this._traumaAndIllnessShortfall = copy._traumaAndIllnessShortfall;
            this._traumaAndIllnessReviewDate = copy._traumaAndIllnessReviewDate;

            this._funeralCoverNeedsQuantified = copy._funeralCoverNeedsQuantified;
            this._funeralCoverNeedsPriority = copy._funeralCoverNeedsPriority;
            this._funeralCoverNeedAddressed = copy._funeralCoverNeedAddressed;
            this._funeralCoverShortfall = copy._funeralCoverShortfall;
            this._funeralCoverReviewDate = copy._funeralCoverReviewDate;

            this._otherNeedsQuantified = copy._otherNeedsQuantified;
            this._otherNeedsPriority = copy._otherNeedsPriority;
            this._otherNeedAddressed = copy._otherNeedAddressed;
            this._otherShortfall = copy._otherShortfall;
            this._otherReviewDate = copy._otherReviewDate;
        }

        #endregion

        #region Life accessors

        public virtual string LifeNeedsQuantified
        {
            get
            {
                return _lifeNeedsQuantified;
            }
            set
            {
                _lifeNeedsQuantified = value;
                InvokePropertyChanged("LifeNeedsQuantified");
            }
        }

        public virtual string LifeNeedsPriority
        {
            get
            {
                return _lifeNeedsPriority;
            }
            set
            {
                _lifeNeedsPriority = value;
                InvokePropertyChanged("LifeNeedsPriority");
            }
        }

        public virtual string LifeNeedAddressed
        {
            get
            {
                return _lifeNeedAddressed;
            }
            set
            {
                _lifeNeedAddressed = value;
                InvokePropertyChanged("LifeNeedAddressed");
            }
        }

        public virtual string LifeShortfall
        {
            get
            {
                return _lifeShortfall;
            }
            set
            {
                _lifeShortfall = value;
                InvokePropertyChanged("LifeShortfall");
            }
        }

        public virtual string LifeReviewDate
        {
            get
            {
                return _lifeReviewDate;
            }
            set
            {
                _lifeReviewDate = value;
                InvokePropertyChanged("LifeReviewDate");
            }
        }

        #endregion

        #region Income Protection accessors

        public virtual string IncomeProtectionNeedsQuantified
        {
            get
            {
                return _incomeProtectionNeedsQuantified;
            }
            set
            {
                _incomeProtectionNeedsQuantified = value;
                InvokePropertyChanged("IncomeProtectionNeedsQuantified");
            }
        }

        public virtual string IncomeProtectionNeedsPriority
        {
            get
            {
                return _incomeProtectionNeedsPriority;
            }
            set
            {
                _incomeProtectionNeedsPriority = value;
                InvokePropertyChanged("IncomeProtectionNeedsPriority");
            }
        }

        public virtual string IncomeProtectionNeedAddressed
        {
            get
            {
                return _incomeProtectionNeedAddressed;
            }
            set
            {
                _incomeProtectionNeedAddressed = value;
                InvokePropertyChanged("IncomeProtectionNeedAddressed");
            }
        }

        public virtual string IncomeProtectionShortfall
        {
            get
            {
                return _incomeProtectionShortfall;
            }
            set
            {
                _incomeProtectionShortfall = value;
                InvokePropertyChanged("IncomeProtectionShortfall");
            }
        }

        public virtual string IncomeProtectionReviewDate
        {
            get
            {
                return _incomeProtectionReviewDate;
            }
            set
            {
                _incomeProtectionReviewDate = value;
                InvokePropertyChanged("IncomeProtectionReviewDate");
            }
        }

        #endregion

        #region Lump Sump accessors

        public virtual string LumpSumNeedsQuantified
        {
            get
            {
                return _lumpSumNeedsQuantified;
            }
            set
            {
                _lumpSumNeedsQuantified = value;
                InvokePropertyChanged("LumpSumNeedsQuantified");
            }
        }

        public virtual string LumpSumNeedsPriority
        {
            get
            {
                return _lumpSumNeedsPriority;
            }
            set
            {
                _lumpSumNeedsPriority = value;
                InvokePropertyChanged("LumpSumNeedsPriority");
            }
        }

        public virtual string LumpSumNeedAddressed
        {
            get
            {
                return _lumpSumNeedAddressed;
            }
            set
            {
                _lumpSumNeedAddressed = value;
                InvokePropertyChanged("LumpSumNeedAddressed");
            }
        }

        public virtual string LumpSumShortfall
        {
            get
            {
                return _lumpSumShortfall;
            }
            set
            {
                _lumpSumShortfall = value;
                InvokePropertyChanged("LumpSumShortfall");
            }
        }

        public virtual string LumpSumReviewDate
        {
            get
            {
                return _lumpSumReviewDate;
            }
            set
            {
                _lumpSumReviewDate = value;
                InvokePropertyChanged("LumpSumReviewDate");
            }
        }

        #endregion

        #region Temporary Disability accessors

        public virtual string TemporaryDisabilityNeedsQuantified
        {
            get
            {
                return _temporaryDisabilityNeedsQuantified;
            }
            set
            {
                _temporaryDisabilityNeedsQuantified = value;
                InvokePropertyChanged("TemporaryDisabilityNeedsQuantified");
            }
        }

        public virtual string TemporaryDisabilityNeedsPriority
        {
            get
            {
                return _temporaryDisabilityNeedsPriority;
            }
            set
            {
                _temporaryDisabilityNeedsPriority = value;
                InvokePropertyChanged("TemporaryDisabilityNeedsPriority");
            }
        }

        public virtual string TemporaryDisabilityNeedAddressed
        {
            get
            {
                return _temporaryDisabilityNeedAddressed;
            }
            set
            {
                _temporaryDisabilityNeedAddressed = value;
                InvokePropertyChanged("TemporaryDisabilityNeedAddressed");
            }
        }

        public virtual string TemporaryDisabilityShortfall
        {
            get
            {
                return _temporaryDisabilityShortfall;
            }
            set
            {
                _temporaryDisabilityShortfall = value;
                InvokePropertyChanged("TemporaryDisabilityShortfall");
            }
        }

        public virtual string TemporaryDisabilityReviewDate
        {
            get
            {
                return _temporaryDisabilityReviewDate;
            }
            set
            {
                _temporaryDisabilityReviewDate = value;
                InvokePropertyChanged("TemporaryDisabilityReviewDate");
            }
        }

        #endregion

        #region Trauma And Illness accessors

        public virtual string TraumaAndIllnessNeedsQuantified
        {
            get
            {
                return _traumaAndIllnessNeedsQuantified;
            }
            set
            {
                _traumaAndIllnessNeedsQuantified = value;
                InvokePropertyChanged("TraumaAndIllnessNeedsQuantified");
            }
        }

        public virtual string TraumaAndIllnessNeedsPriority
        {
            get
            {
                return _traumaAndIllnessNeedsPriority;
            }
            set
            {
                _traumaAndIllnessNeedsPriority = value;
                InvokePropertyChanged("TraumaAndIllnessNeedsPriority");
            }
        }

        public virtual string TraumaAndIllnessNeedAddressed
        {
            get
            {
                return _traumaAndIllnessNeedAddressed;
            }
            set
            {
                _traumaAndIllnessNeedAddressed = value;
                InvokePropertyChanged("TraumaAndIllnessNeedAddressed");
            }
        }

        public virtual string TraumaAndIllnessShortfall
        {
            get
            {
                return _traumaAndIllnessShortfall;
            }
            set
            {
                _traumaAndIllnessShortfall = value;
                InvokePropertyChanged("TraumaAndIllnessShortfall");
            }
        }

        public virtual string TraumaAndIllnessReviewDate
        {
            get
            {
                return _traumaAndIllnessReviewDate;
            }
            set
            {
                _traumaAndIllnessReviewDate = value;
                InvokePropertyChanged("TraumaAndIllnessReviewDate");
            }
        }

        #endregion

        #region Funeral Cover accessors

        public virtual string FuneralCoverNeedsQuantified
        {
            get
            {
                return _funeralCoverNeedsQuantified;
            }
            set
            {
                _funeralCoverNeedsQuantified = value;
                InvokePropertyChanged("FuneralCoverNeedsQuantified");
            }
        }

        public virtual string FuneralCoverNeedsPriority
        {
            get
            {
                return _funeralCoverNeedsPriority;
            }
            set
            {
                _funeralCoverNeedsPriority = value;
                InvokePropertyChanged("FuneralCoverNeedsPriority");
            }
        }

        public virtual string FuneralCoverNeedAddressed
        {
            get
            {
                return _funeralCoverNeedAddressed;
            }
            set
            {
                _funeralCoverNeedAddressed = value;
                InvokePropertyChanged("FuneralCoverNeedAddressed");
            }
        }

        public virtual string FuneralCoverShortfall
        {
            get
            {
                return _funeralCoverShortfall;
            }
            set
            {
                _funeralCoverShortfall = value;
                InvokePropertyChanged("FuneralCoverShortfall");
            }
        }

        public virtual string FuneralCoverReviewDate
        {
            get
            {
                return _funeralCoverReviewDate;
            }
            set
            {
                _funeralCoverReviewDate = value;
                InvokePropertyChanged("FuneralCoverReviewDate");
            }
        }

        #endregion

        #region Other accessors

        public virtual string OtherNeedsQuantified
        {
            get
            {
                return _otherNeedsQuantified;
            }
            set
            {
                _otherNeedsQuantified = value;
                InvokePropertyChanged("OtherNeedsQuantified");
            }
        }

        public virtual string OtherNeedsPriority
        {
            get
            {
                return _otherNeedsPriority;
            }
            set
            {
                _otherNeedsPriority = value;
                InvokePropertyChanged("OtherNeedsPriority");
            }
        }

        public virtual string OtherNeedAddressed
        {
            get
            {
                return _otherNeedAddressed;
            }
            set
            {
                _otherNeedAddressed = value;
                InvokePropertyChanged("OtherNeedAddressed");
            }
        }

        public virtual string OtherShortfall
        {
            get
            {
                return _otherShortfall;
            }
            set
            {
                _otherShortfall = value;
                InvokePropertyChanged("OtherShortfall");
            }
        }

        public virtual string OtherReviewDate
        {
            get
            {
                return _otherReviewDate;
            }
            set
            {
                _otherReviewDate = value;
                InvokePropertyChanged("OtherReviewDate");
            }
        }

        #endregion
    }
}
