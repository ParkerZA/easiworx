using easiplan.domain.easiplan.domain.Entities;
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

            //this.ProductNameComparison = copy.ProductNameComparison;
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
        //Methods to get and set the infomations for each row of the Needs and Goals table in Medical Aid CAR

        public virtual MedicalAidNeedsAndGoalsTableRow HospitalCoverInfo{ get; set; }

        public virtual MedicalAidNeedsAndGoalsTableRow DayToDayBenefitInfo { get; set; }
        
        public virtual MedicalAidNeedsAndGoalsTableRow ThresholdBenefitInfo { get; set;}

        public virtual MedicalAidNeedsAndGoalsTableRow ChronicBenefitInfo { get; set; }

        public virtual MedicalAidNeedsAndGoalsTableRow SavingsAccountInfo { get; set; }

        public virtual MedicalAidNeedsAndGoalsTableRow HospitalPreferenceInfo { get; set; }

        public virtual MedicalAidNeedsAndGoalsTableRow GapCoverInfo { get; set; }

        public virtual MedicalAidNeedsAndGoalsTableRow OtherInfo { get; set; }

        #endregion

        #region Medical Aid Medical Scheme comparison table methods
        //Methods for storage of the string descriptions for the current medical aids, and the replaced medical aid

        public virtual MedicalSchemeComparison PolicyNumberComparison { get; set; }
        public virtual MedicalSchemeComparison InsurerComparison { get; set; }

        public virtual MedicalSchemeComparison ProductNameComparison { get; set; }

        public virtual MedicalSchemeComparison PremiumComparison { get; set; }

        public virtual MedicalSchemeComparison BenefitsComparison { get; set; }

        public virtual MedicalSchemeComparison SavingsAccountComparison { get; set; }

        public virtual MedicalSchemeComparison ChronicBenefitComparison { get; set; }

        public virtual MedicalSchemeComparison HospitalCoverComparison { get; set; }

        public virtual MedicalSchemeComparison LimitsOnCoverComparison { get; set; }

        public virtual MedicalSchemeComparison OtherComparison { get; set; }
        
        #endregion

    }
}
