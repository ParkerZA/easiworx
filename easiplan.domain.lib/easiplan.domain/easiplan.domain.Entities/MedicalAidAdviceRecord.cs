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





    }
}
