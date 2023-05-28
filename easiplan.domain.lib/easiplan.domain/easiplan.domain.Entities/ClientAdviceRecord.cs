using FluentValidation;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
    public class ClientAdviceRecord : BaseEntity<int>
    {
        private string _clientName;
        private string _adviserName;
        private string _venue;
        private string _generalNotes;

        private string _productKnowledge;
        private string _riskProfile;
        private string _investmentType;
        private string _policyNumber;
        private string _needsAndObjectives;
        private string _financialSituation;
        private string _investmentHorizen;
        private string _accessToCapital;
        private string _otherInformation;

        private string _medicalConditions;
        private string _medicalCover;
        private string _hospitalisation;
        private string _chronicConditions;
        private string _waitingPeriods;
        private string _lateJoynerPenalty;
        private string _coPayments;


        private string _additionalInfo;
        private string _recommendedProduct;
        private string _motivation;
        private string _initialRecommendation;
        private bool _IsCompleted;



        public virtual bool IsCompleted
        {
            get
            {
                return _IsCompleted;
            }
            set
            {
                _IsCompleted = value;
                InvokePropertyChanged("IsCompleted");
            }
        }

        public virtual DateTime AdviceDate
        {
            get => base.CreateDate; set => base.CreateDate = value;
        }

        //Name of the client that owns the policy
        public virtual string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                _clientName = value;
                InvokePropertyChanged("ClientName");
            }
        }


        //Name of the advisor responsible for the client
        public virtual string AdviserName
        {
            get
            {
                return _adviserName;
            }
            set
            {
                _adviserName = value;
                InvokePropertyChanged("AdviserName");
            }
        }


        //Product knowlege and Experience
        public virtual string ProductKnowledge
        {
            get
            {
                return _productKnowledge;
            }
            set
            {
                _productKnowledge = value;
                InvokePropertyChanged("ProductKnowledge");
            }
        }

        //Clients financial Situation
        public virtual string FinancialSituation
        {
            get
            {
                return _financialSituation;
            }
            set
            {
                _financialSituation = value;
                InvokePropertyChanged("FinancialSituation");
            }
        }

        public virtual string OtherInformation
        {
            get
            {
                return _otherInformation;
            }
            set
            {
                _otherInformation = value;
                InvokePropertyChanged("OtherInformation");
            }
        }

        //Products recomended to the client
        public virtual string RecommendedFunds
        {
            get
            {
                return _recommendedProduct;
            }
            set
            {
                _recommendedProduct = value;
                InvokePropertyChanged("RecommendedFunds");
            }
        }

        //Motivation for recommendation
        public virtual string Motivation
        {
            get
            {
                return _motivation;
            }
            set
            {
                _motivation = value;
                InvokePropertyChanged("Motivation");
            }
        }

        //Venue
        public virtual string Venue
        {
            get
            {
                return _venue;
            }
            set
            {
                _venue = value;
                InvokePropertyChanged("Venue");
            }
        }


        //General notes from the advisor
        public virtual string GeneralNotes
        {
            get
            {
                return _generalNotes;
            }
            set
            {
                _generalNotes = value;
                InvokePropertyChanged("GeneralNotes");
            }
        }


        //Clients risk profile
        public virtual string RiskProfile
        {
            get
            {
                return _riskProfile;
            }
            set
            {
                _riskProfile = value;
                InvokePropertyChanged("RiskProfile");
            }
        }


        //Type of investment
        public virtual string InvestmentType
        {
            get
            {
                return _investmentType;
            }
            set
            {
                _investmentType = value;
                InvokePropertyChanged("InvestmentType");
            }
        }


        //Policy number for the policy
        public virtual string PolicyNumber
        {
            get
            {
                return _policyNumber;
            }
            set
            {
                _policyNumber = value;
                InvokePropertyChanged("PolicyNumber");
            }
        }


        //Clients needs and objectives as given by the adviser
        public virtual string NeedsAndObjectives
        {
            get
            {
                return _needsAndObjectives;
            }
            set
            {
                _needsAndObjectives = value;
                InvokePropertyChanged("NeedsAndObjectives");
            }
        }


        //Clients financial situation
        public virtual string FincancialSituation
        {
            get
            {
                return _financialSituation;
            }
            set
            {
                _financialSituation = value;
                InvokePropertyChanged("FinancialSituation");
            }
        }


        //The spot where the sun comes out and the money glistens
        public virtual string InvestmentHorizen
        {
            get
            {
                return _investmentHorizen;
            }
            set
            {
                _investmentHorizen = value;
                InvokePropertyChanged("InvestmentHorizen");
            }
        }


        public virtual string AccessToCapital
        {
            get
            {
                return _accessToCapital;
            }
            set
            {
                _accessToCapital = value;
                InvokePropertyChanged("AccessToCapital");
            }
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


        //Additional information given by the adviser
        public virtual string AdditionalInfo
        {
            get
            {
                return _additionalInfo;
            }
            set
            {
                _additionalInfo = value;
                InvokePropertyChanged("AdditionalInfo");
            }
        }


     


        //Advisors recommendation to the client
        public virtual string InitialRecommendation
        {
            get
            {
                return _initialRecommendation;
            }
            set
            {
                _initialRecommendation = value;
                InvokePropertyChanged("InitalRecommendation");
            }
        }


        #region Medical Aid Needs and Goals table methods
        public virtual string hcCoverDiscussed
        {
            get;
            set;
        }

        public virtual string hcCoverTaken
        {
            get;
            set;
        }

        public virtual string hcComments
        {
            get;
            set;
        }

        public virtual string ddCoverDiscussed
        {
            get;
            set;
        }

        public virtual string ddCoverTaken
        {
            get;
            set;
        }

        public virtual string ddComments
        {
            get;
            set;
        }

        public virtual string tbCoverDiscussed
        {
            get;
            set;
        }

        public virtual string tbCoverTaken
        {
            get;
            set;
        }

        public virtual string tbComments
        {
            get;
            set;
        }

        public virtual string cbCoverDiscussed
        {
            get;
            set;
        }

        public virtual string cbCoverTaken
        {
            get;
            set;
        }

        public virtual string cbComments
        {
            get;
            set;
        }

        public virtual string saCoverDiscussed
        {
            get;
            set;
        }

        public virtual string saCoverTaken
        {
            get;
            set;
        }

        public virtual string saComments
        {
            get;
            set;
        }

        public virtual string hpCoverDiscussed
        {
            get;
            set;
        }

        public virtual string hpCoverTaken
        {
            get;
            set;
        }

        public virtual string hpComments
        {
            get;
            set;
        }

        public virtual string gcCoverDiscussed
        {
            get;
            set;
        }

        public virtual string gcCoverTaken
        {
            get;
            set;
        }

        public virtual string gcComments
        {
            get;
            set;
        }

        public virtual string oCoverDiscussed
        {
            get;
            set;
        }

        public virtual string oCoverTaken
        {
            get;
            set;
        }

        public virtual string oComments
        {
            get;
            set;
        }


        #endregion

        #region Medical Aid Medical Scheme comparison table methods
        //Methods for storage of the string descriptions for the current medical aids, and the replaced medical aids
        public virtual string policyNumberCurrent
        {
            get;
            set;
        }

        public virtual string policyNumberReplaced
        {
            get;
            set;
        }

        public virtual string insurerCurrent
        {
            get;
            set;
        }

        public virtual string insurerReplaced
        {
            get;
            set;
        }

        public virtual string productNameCurrent
        {
            get;
            set;
        }

        public virtual string productNameReplaced
        {
            get;
            set;
        }

        public virtual string premiumCurrent
        {
            get;
            set;
        }

        public virtual string premiumReplaced
        {
            get;
            set;
        }

        public virtual string benefitsCurrent
        {
            get;
            set;
        }

        public virtual string benefitsReplaced
        {
            get;
            set;
        }

        public virtual string savingsAccountCurrent
        {
            get;
            set;
        }

        public virtual string savingsAccountReplaced
        {
            get;
            set;
        }

        public virtual string chronicBenefitCurrent
        {
            get;
            set;
        }

        public virtual string chronicBenefitReplaced
        {
            get;
            set;
        }

        public virtual string hospitalCoverCurrent
        {
            get;
            set;
        }

        public virtual string hospitalCoverReplaced
        {
            get;
            set;
        }

        public virtual string limitsOnCoverCurrent
        {
            get;
            set;
        }

        public virtual string limitsOnCoverReplaced
        {
            get;
            set;
        }

        public virtual string otherCurrent
        {
            get;
            set;
        }

        public virtual string otherReplaced
        {
            get;
            set;
        }

        #endregion



        public override void Calculate()
        {
            base.Calculate();
        }
    }
}