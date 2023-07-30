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

        #region Constructors
        
        //Standard constructor
        public MedicalAidAdviceRecord()
        {
            #region Needs And Goals Table

            this.HospitalCoverInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.DayToDayBenefitInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.ThresholdBenefitInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.ChronicBenefitInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.SavingsAccountInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.HospitalPreferenceInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.GapCoverInfo = new MedicalAidNeedsAndGoalsTableRow();
            this.OtherInfo = new MedicalAidNeedsAndGoalsTableRow();

            #endregion

            #region Comparison Table

            this.PolicyNumberComparison = new MedicalSchemeComparison();
            this.InsurerComparison = new MedicalSchemeComparison();
            this.ProductNameComparison = new MedicalSchemeComparison();
            this.PremiumComparison = new MedicalSchemeComparison();
            this.BenefitsComparison = new MedicalSchemeComparison();
            this.SavingsAccountComparison = new MedicalSchemeComparison();
            this.ChronicBenefitComparison = new MedicalSchemeComparison();
            this.HospitalCoverComparison = new MedicalSchemeComparison();
            this.LimitsOnCoverComparison = new MedicalSchemeComparison();
            this.OtherComparison = new MedicalSchemeComparison();

            #endregion
        }

        //Copy Constructor
        public MedicalAidAdviceRecord (MedicalAidAdviceRecord copy): base(copy)
        {
            #region Instance Variables

            this._medicalConditions = copy._medicalConditions;
            this._medicalCover = copy._medicalCover;
            this._hospitalisation= copy._hospitalisation;
            this._chronicConditions=copy._chronicConditions;
            this._waitingPeriods= copy._waitingPeriods;
            this._lateJoynerPenalty=copy._lateJoynerPenalty;
            this._coPayments=copy._coPayments;
            this._otherImportantInformation=copy._otherImportantInformation;
            this._notes=copy._notes;

            #endregion

            #region Needs And Goals Table

            this.HospitalCoverInfo = new MedicalAidNeedsAndGoalsTableRow(copy.HospitalCoverInfo);
            this.DayToDayBenefitInfo = new MedicalAidNeedsAndGoalsTableRow(copy.DayToDayBenefitInfo);
            this.ThresholdBenefitInfo = new MedicalAidNeedsAndGoalsTableRow(copy.ThresholdBenefitInfo);
            this.ChronicBenefitInfo = new MedicalAidNeedsAndGoalsTableRow(copy.ChronicBenefitInfo);
            this.SavingsAccountInfo = new MedicalAidNeedsAndGoalsTableRow(copy.SavingsAccountInfo);
            this.HospitalPreferenceInfo = new MedicalAidNeedsAndGoalsTableRow(copy.HospitalPreferenceInfo);
            this.GapCoverInfo = new MedicalAidNeedsAndGoalsTableRow(copy.GapCoverInfo);
            this.OtherInfo = new MedicalAidNeedsAndGoalsTableRow(copy.OtherInfo);

            #endregion

            #region Comparison Table

            this.PolicyNumberComparison = new MedicalSchemeComparison(copy.PolicyNumberComparison);
            this.InsurerComparison = new MedicalSchemeComparison(copy.InsurerComparison);
            this.ProductNameComparison = new MedicalSchemeComparison(copy.ProductNameComparison);
            this.PremiumComparison = new MedicalSchemeComparison(copy.PremiumComparison);
            this.BenefitsComparison = new MedicalSchemeComparison(copy.BenefitsComparison);
            this.SavingsAccountComparison = new MedicalSchemeComparison(copy.SavingsAccountComparison);
            this.ChronicBenefitComparison = new MedicalSchemeComparison(copy.ChronicBenefitComparison);
            this.HospitalCoverComparison = new MedicalSchemeComparison(copy.HospitalCoverComparison);
            this.LimitsOnCoverComparison = new MedicalSchemeComparison(copy.LimitsOnCoverComparison);
            this.OtherComparison = new MedicalSchemeComparison(copy.OtherComparison);

            #endregion
        }

        #endregion

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

        public virtual void Initialise_Names(){

            //Cover
            HospitalCoverInfo.Name = "Hospitilization Cover";
            DayToDayBenefitInfo.Name = "Day-to-Day Benefit";
            ThresholdBenefitInfo.Name = "Threshold Benefit";
            ChronicBenefitInfo.Name = "Chronic Benefit";
            SavingsAccountInfo.Name = "Savings Account";
            HospitalPreferenceInfo.Name = "Hospital Preference";
            GapCoverInfo.Name = "Gap Cover";
            OtherInfo.Name = "Other";

            //Comparison
            PolicyNumberComparison.Name = "Policy/Applicaion Number";
            InsurerComparison.Name = "Insurer";
            ProductNameComparison.Name = "Product Name";
            PremiumComparison.Name = "Premium";
            BenefitsComparison.Name = "Benefits";
            SavingsAccountComparison.Name = "Savings Account";
            ChronicBenefitComparison.Name = "Chronic Benefits";
            HospitalCoverComparison.Name = "Hospital Cover";
            LimitsOnCoverComparison.Name = "Limits on Cover";
            OtherComparison.Name = "Other";

        }
    }
}
