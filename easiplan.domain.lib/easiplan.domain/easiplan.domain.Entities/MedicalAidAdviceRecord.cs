using easiplan.domain.Entities;
using FluentValidation;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
    public class MedicalAidAdviceRecord : ClientAdviceRecord
    {
        private string _medicalConditions;
        private string _medicalCover;
        private string _hospitalisation;
        private string _chronicConditions;
        private string _waitingPeriods;
        private string _lateJoynerPenalty;
        private string _coPayments;
        private string _otherImportantInformation;
        private string _notes;

        #region Needs and Goals table private variables

        private string _hospitalCoverDiscussed;
        private string _hospitalCoverTaken;
        private string _hospitalCoverComment;

        private string _dayToDayBenefitDiscussed;
        private string _dayToDayBenefitTaken;
        private string _dayToDayBenefitComment;


        private string _thresholdBenefitDiscussed;
        private string _thresholdBenefitTaken;
        private string _thresholdBenefitComment;


        private string _chronicBenefitDiscussed;
        private string _chronicBenefitTaken;
        private string _chronicBenefitComment;

        private string _savingsAccountDiscussed;
        private string _savingsAccountTaken;
        private string _savingsAccountComment;
        
        private string _hospitalisationPreferenceDiscussed;
        private string _hospitalisationPreferenceTaken;
        private string _hospitalisationPreferenceComment;


        private string _gapCoverDiscussed;
        private string _gapCoverTaken;
        private string _gapCoverComment;

        private string _otherDiscussed;
        private string _otherTaken;
        private string _otherComment;

        #endregion

        #region Medical Scheme Comparison table private variables

        private string _policyNumberCurrent;
        private string _policyNumberReplaced;
        private string _insurerCurrent;
        private string _insurerReplaced;
        private string _productNameCurrent;
        private string _productNameReplaced;
        private string _premiumCurrent;
        private string _premiumReplaced;
        private string _benefitsCurrent;
        private string _benefitsReplaced;
        private string _savingsAccountCurrent;
        private string _savingsAccountReplaced;
        private string _chronicBenefitCurrent;
        private string _chronicBenefitReplaced;
        private string _hospitalCoverCurrent;
        private string _hospitalCoverReplaced;
        private string _limitsOnCoverCurrent;
        private string _limitsOnCoverReplaced;
        private string _otherCurrent;
        private string _otherReplaced;

        #endregion


        public MedicalAidAdviceRecord()
        {
           

        }

        public MedicalAidAdviceRecord (MedicalAidAdviceRecord copy): base(copy)
        {
            this._medicalConditions = copy._medicalConditions;
            this._medicalCover = copy._medicalCover;
            this._hospitalisation= copy._hospitalisation;
            this._chronicConditions=copy._chronicConditions;
            this._waitingPeriods= copy._waitingPeriods;
            this._lateJoynerPenalty=copy._lateJoynerPenalty;
            this._coPayments=copy._coPayments;
            this._otherImportantInformation=copy._otherImportantInformation;
            this._notes=copy._notes;

            this._hospitalCoverDiscussed = copy._hospitalCoverDiscussed;
            this._hospitalCoverTaken = copy._hospitalCoverTaken;
            this._hospitalCoverComment = copy._hospitalCoverComment;

            this._dayToDayBenefitDiscussed = copy._dayToDayBenefitDiscussed;
            this._dayToDayBenefitTaken = copy._dayToDayBenefitTaken;
            this._dayToDayBenefitComment = copy._dayToDayBenefitComment;

            this._thresholdBenefitDiscussed=copy._thresholdBenefitDiscussed;
            this._thresholdBenefitTaken = copy._thresholdBenefitTaken;
            this._thresholdBenefitComment = copy._thresholdBenefitComment;

            this._chronicBenefitDiscussed = copy._chronicBenefitDiscussed;
            this._chronicBenefitTaken = copy._chronicBenefitTaken;
            this._chronicBenefitComment = copy._chronicBenefitComment;

            this._savingsAccountDiscussed = copy._savingsAccountDiscussed;
            this._savingsAccountTaken = copy._savingsAccountTaken;
            this._savingsAccountComment = copy._savingsAccountComment;

            this._hospitalisationPreferenceDiscussed = copy._hospitalisationPreferenceDiscussed;
            this._hospitalisationPreferenceTaken = copy._hospitalisationPreferenceTaken;
            this._hospitalisationPreferenceComment = copy._hospitalisationPreferenceComment;

            this._gapCoverDiscussed= copy._gapCoverDiscussed;
            this._gapCoverTaken = copy._gapCoverTaken;
            this._gapCoverComment = copy._gapCoverComment;

            this._otherDiscussed= copy._otherDiscussed;
            this._otherTaken= copy._otherTaken;
            this._otherComment= copy._otherComment;


            this._policyNumberCurrent = copy._policyNumberCurrent;
            this._policyNumberReplaced = copy._policyNumberReplaced;
            this._insurerCurrent = copy._insurerCurrent;
            this._insurerReplaced = copy._insurerReplaced;
            this._productNameCurrent = copy._productNameCurrent;
            this._productNameReplaced = copy._productNameReplaced;
            this._premiumCurrent = copy._premiumCurrent;
            this._premiumReplaced = copy._premiumReplaced;
            this._benefitsCurrent = copy._benefitsCurrent;
            this._benefitsReplaced = copy._benefitsReplaced;
            this._savingsAccountCurrent = copy._savingsAccountCurrent;
            this._savingsAccountReplaced = copy._savingsAccountReplaced;
            this._chronicBenefitCurrent = copy._chronicBenefitCurrent;
            this._chronicBenefitReplaced = copy._chronicBenefitReplaced;
            this._hospitalCoverCurrent = copy._hospitalCoverCurrent;
            this._hospitalCoverReplaced = copy._hospitalCoverReplaced;
            this._limitsOnCoverCurrent = copy._limitsOnCoverCurrent;
            this._limitsOnCoverReplaced = copy._limitsOnCoverReplaced;
            this._otherCurrent = copy._otherCurrent;
            this._otherReplaced = copy._otherReplaced;
    }


        public virtual string MedicalConditions
        {
            get
            {
                return _medicalConditions;
            }
            set
            {
                _medicalConditions = value;
                InvokePropertyChanged("MedicalConditions");
            }
        }

        public virtual string MedicalCover
        {
            get
            {
                return _medicalCover;
            }
            set
            {
                _medicalCover = value;
                InvokePropertyChanged("MedicalCover");
            }
        }

        public virtual string Hospitalisation
        {
            get
            {
                return _hospitalisation;
            }
            set
            {
                _hospitalisation = value;
                InvokePropertyChanged("Hospitalisation");
            }
        }

        public virtual string ChronicConditions
        {
            get
            {
                return _chronicConditions;
            }
            set
            {
                _chronicConditions = value;
                InvokePropertyChanged("ChronicConditions");
            }
        }

        public virtual string WaitingPeriods
        {
            get
            {
                return _waitingPeriods;
            }
            set
            {
                _waitingPeriods = value;
                InvokePropertyChanged("WaitingPeriods");
            }
        }

        public virtual string LateJoynerPenalty
        {
            get
            {
                return _lateJoynerPenalty;
            }
            set
            {
                _lateJoynerPenalty = value;
                InvokePropertyChanged("LateJoynerPenalty");
            }
        }

        public virtual string CoPayments
        {
            get
            {
                return _coPayments;
            }
            set
            {
                _coPayments = value;
                InvokePropertyChanged("CoPayments");
            }
        }

        public virtual string OtherImportantInformation
        {
            get
            {
                return _otherImportantInformation;
            }
            set
            {
                _otherImportantInformation = value;
                InvokePropertyChanged("OtherImportantInformation");
            }
        }

        public virtual string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
                InvokePropertyChanged("Notes");
            }
        }


        #region Medical Aid Needs and Goals table methods

        public virtual CARInfo hcInfo{ get; set; }


        //public virtual string hcCoverDiscussed
        //{
        //    get
        //    {
        //        return _hospitalCoverDiscussed;
        //    }
        //    set
        //    {
        //        _hospitalCoverDiscussed = value;
        //        InvokePropertyChanged("hcCoverDiscussed");
        //    }
        //}

        //public virtual string hcCoverTaken
        //{
        //    get
        //    {
        //        return _hospitalCoverTaken;
        //    }
        //    set
        //    {
        //        _hospitalCoverTaken = value;
        //        InvokePropertyChanged("hcCoverTaken");
        //    }
        //}

        //public virtual string hcComments
        //{
        //    get
        //    {
        //        return _hospitalCoverComment;
        //    }
        //    set
        //    {
        //        _hospitalCoverComment = value;
        //        InvokePropertyChanged("hcComments");
        //    }
        //}

        public virtual string ddCoverDiscussed
        {
            get
            {
                return _dayToDayBenefitDiscussed;
            }
            set
            {
                _dayToDayBenefitDiscussed = value;
                InvokePropertyChanged("ddCoverDiscussed");
            }
        }

        public virtual string ddCoverTaken
        {
            get
            {
                return _dayToDayBenefitTaken;
            }
            set
            {
                _dayToDayBenefitTaken = value;
                InvokePropertyChanged("ddCoverTaken");
            }
        }

        public virtual string ddComments
        {
            get
            {
                return _dayToDayBenefitComment;
            }
            set
            {
                _dayToDayBenefitComment = value;
                InvokePropertyChanged("ddComments");
            }
        }

        public virtual string tbCoverDiscussed
        {
            get
            {
                return _thresholdBenefitDiscussed;
            }
            set
            {
                _thresholdBenefitDiscussed = value;
                InvokePropertyChanged("tbCoverDiscussed");
            }
        }

        public virtual string tbCoverTaken
        {
            get
            {
                return _thresholdBenefitTaken;
            }
            set
            {
                _thresholdBenefitTaken = value;
                InvokePropertyChanged("tbCoverTaken");
            }
        }

        public virtual string tbComments
        {
            get
            {
                return _thresholdBenefitComment;
            }
            set
            {
                _thresholdBenefitComment = value;
                InvokePropertyChanged("tbComments");
            }
        }

        public virtual string cbCoverDiscussed
        {
            get
            {
                return _chronicBenefitDiscussed;
            }
            set
            {
                _chronicBenefitDiscussed = value;
                InvokePropertyChanged("cbCoverDiscussed");
            }
        }

        public virtual string cbCoverTaken
        {
            get
            {
                return _chronicBenefitTaken;
            }
            set
            {
                _chronicBenefitTaken = value;
                InvokePropertyChanged("cbCoverTaken");
            }
        }

        public virtual string cbComments
        {
            get
            {
                return _chronicBenefitComment;
            }
            set
            {
                _chronicBenefitComment = value;
                InvokePropertyChanged("cbComments");
            }
        }

        public virtual string saCoverDiscussed
        {
            get
            {
                return _savingsAccountDiscussed;
            }
            set
            {
                _savingsAccountDiscussed = value;
                InvokePropertyChanged("saCoverDiscussed");
            }
        }

        public virtual string saCoverTaken
        {
            get
            {
                return _savingsAccountTaken;
            }
            set
            {
                _savingsAccountTaken = value;
                InvokePropertyChanged("saCoverTaken");
            }
        }

        public virtual string saComments
        {
            get
            {
                return _savingsAccountComment;
            }
            set
            {
                _savingsAccountComment = value;
                InvokePropertyChanged("saComments");
            }
        }

        public virtual string hpCoverDiscussed
        {
            get
            {
                return _hospitalisationPreferenceDiscussed;
            }
            set
            {
                _hospitalisationPreferenceDiscussed = value;
                InvokePropertyChanged("hpCoverDiscussed");
            }
        }

        public virtual string hpCoverTaken
        {
            get
            {
                return _hospitalisationPreferenceTaken;
            }
            set
            {
                _hospitalisationPreferenceTaken = value;
                InvokePropertyChanged("hpCoverTaken");
            }
        }

        public virtual string hpComments
        {
            get
            {
                return _hospitalisationPreferenceComment;
            }
            set
            {
                _hospitalisationPreferenceComment = value;
                InvokePropertyChanged("hpComments");
            }
        }

        public virtual string gcCoverDiscussed
        {
            get
            {
                return _gapCoverDiscussed;
            }
            set
            {
                _gapCoverDiscussed = value;
                InvokePropertyChanged("gcCoverDiscussed");
            }
        }

        public virtual string gcCoverTaken
        {
            get
            {
                return _gapCoverTaken;
            }
            set
            {
                _gapCoverTaken = value;
                InvokePropertyChanged("gcCoverTaken");
            }
        }

        public virtual string gcComments
        {
            get
            {
                return _gapCoverComment;
            }
            set
            {
                _gapCoverComment = value;
                InvokePropertyChanged("gcComments");
            }
        }

        public virtual string oCoverDiscussed
        {
            get
            {
                return _otherDiscussed;
            }
            set
            {
                _otherDiscussed = value;
                InvokePropertyChanged("oCoverDiscussed");
            }
        }

        public virtual string oCoverTaken
        {
            get
            {
                return _otherTaken;
            }
            set
            {
                _otherTaken = value;
                InvokePropertyChanged("oCoverTaken");
            }
        }

        public virtual string oComments
        {
            get
            {
                return _otherComment;
            }
            set
            {
                _otherComment = value;
                InvokePropertyChanged("oComments");
            }
        }


        #endregion

        #region Medical Aid Medical Scheme comparison table methods
        //Methods for storage of the string descriptions for the current medical aids, and the replaced medical aids
        public virtual string PolicyNumberCurrent
        {
            get
            {
                return _policyNumberCurrent;
            }
            set
            {
                _policyNumberCurrent = value;
                InvokePropertyChanged("PolicyNumberCurrent");
            }
        }

        public virtual string PolicyNumberReplaced
        {
            get
            {
                return _policyNumberReplaced;
            }
            set
            {
                _policyNumberReplaced = value;
                InvokePropertyChanged("PolicyNumberReplaced");
            }
        }

        public virtual string InsurerCurrent
        {
            get
            {
                return _insurerCurrent;
            }
            set
            {
                _insurerCurrent = value;
                InvokePropertyChanged("InsurerCurrent");
            }
        }
        
        public virtual string InsurerReplaced
        {
            get
            {
                return _insurerReplaced;
            }
            set
            {
                _insurerReplaced = value;
                InvokePropertyChanged("InsurerReplaced");
            }
        }

        public virtual string ProductNameCurrent
        {
            get
            {
                return _productNameCurrent;
            }
            set
            {
                _productNameCurrent = value;
                InvokePropertyChanged("ProductNameCurrent");
            }
        }

        public virtual string ProductNameReplaced
        {
            get
            {
                return _productNameReplaced;
            }
            set
            {
                _productNameReplaced = value;
                InvokePropertyChanged("ProductNameReplaced");
            }
        }

        public virtual string PremiumCurrent
        {
            get
            {
                return _premiumCurrent;
            }
            set
            {
                _premiumCurrent = value;
                InvokePropertyChanged("PremiumCurrent");
            }
        }

        public virtual string PremiumReplaced
        {
            get
            {
                return _premiumReplaced;
            }
            set
            {
                _premiumReplaced = value;
                InvokePropertyChanged("PremiumReplaced");
            }
        }

        public virtual string BenefitsCurrent
        {
            get
            {
                return _benefitsCurrent;
            }
            set
            {
                _benefitsCurrent = value;
                InvokePropertyChanged("BenefitsCurrent");
            }
        }

        public virtual string BenefitsReplaced
        {
            get
            {
                return _benefitsReplaced;
            }
            set
            {
                _benefitsReplaced = value;
                InvokePropertyChanged("BenefitsReplaced");
            }
        }
        
        public virtual string SavingsAccountCurrent
        {
            get
            {
                return _savingsAccountCurrent;
            }
            set
            {
                _savingsAccountCurrent = value;
                InvokePropertyChanged("SavingsAccountCurrent");
            }
        }

        public virtual string SavingsAccountReplaced
        {
            get
            {
                return _savingsAccountReplaced;
            }
            set
            {
                _savingsAccountReplaced = value;
                InvokePropertyChanged("SavingsAccountReplaced");
            }
        }

        public virtual string ChronicBenefitCurrent
        {
            get
            {
                return _chronicBenefitCurrent;
            }
            set
            {
                _chronicBenefitCurrent = value;
                InvokePropertyChanged("ChronicBenefitCurrent");
            }
        }

        public virtual string ChronicBenefitReplaced
        {
            get
            {
                return _chronicBenefitReplaced;
            }
            set
            {
                _chronicBenefitReplaced = value;
                InvokePropertyChanged("ChronicBenefitReplaced");
            }
        }

        public virtual string HospitalCoverCurrent
        {
            get
            {
                return _hospitalCoverCurrent;
            }
            set
            {
                _hospitalCoverCurrent = value;
                InvokePropertyChanged("HospitalCoverCurrent");
            }
        }

        public virtual string HospitalCoverReplaced
        {
            get
            {
                return _hospitalCoverReplaced;
            }
            set
            {
                _hospitalCoverReplaced = value;
                InvokePropertyChanged("HospitalCoverReplaced");
            }
        }

        public virtual string LimitsOnCoverCurrent
        {
            get
            {
                return _limitsOnCoverCurrent;
            }
            set
            {
                _limitsOnCoverCurrent = value;
                InvokePropertyChanged("LimitsOnCoverCurrent");
            }
        }

        public virtual string LimitsOnCoverReplaced
        {
            get
            {
                return _limitsOnCoverReplaced;
            }
            set
            {
                _limitsOnCoverReplaced = value;
                InvokePropertyChanged("LimitsOnCoverReplaced");
            }
        }

        public virtual string OtherCurrent
        {
            get
            {
                return _otherCurrent;
            }
            set
            {
                _otherCurrent = value;
                InvokePropertyChanged("OtherCurrent");
            }
        }

        public virtual string OtherReplaced
        {
            get
            {
                return _otherReplaced;
            }
            set
            {
                _otherReplaced = value;
                InvokePropertyChanged("OtherReplaced");
            }
        }
        
        #endregion





    }
}
