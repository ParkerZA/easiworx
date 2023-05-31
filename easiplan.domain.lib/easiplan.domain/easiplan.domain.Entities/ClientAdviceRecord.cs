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
        private string _additionalInfo;
        private string _recommendedProduct;
        private string _motivation;
        private string _initialRecommendation;
        private string _implementedProduct;
        private string _implementedMotivation;

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

        
        public virtual string ImplementedProduct
        {
            get
            { 
                return this._implementedProduct;
            }
            set 
            { 
                this._implementedProduct = value;
                InvokePropertyChanged("ImplementedProduct");
            }
        }

        public virtual string ImplementedMotivation
        {
            get
            {
                return this._implementedMotivation;
            }
            set
            {
                this._implementedMotivation = value;
                InvokePropertyChanged("ImplementedMotivation");
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


        public override void Calculate()
        {
            base.Calculate();
        }
    }
}