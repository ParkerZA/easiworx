using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
    public class Life : baseCalcEntity
    {
       
        #region Persisted Properties

        public virtual string Type
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual string ReferenceNo
        {
            get;
            set;
        }
        public virtual string Insured
        {
            get;
            set;
        }
        [IgnoreDataMember]
        public virtual int ReferenceId
        {
            get;
            set;
        }

        public virtual IList<Benefit> Benefits
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Benefit> BenefitsBindingList { get; set; }


        public virtual IList<Note> Notes
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public virtual IList<RiskAdviceRecord> AdviceRecords
        {
            get;
            set;
        }


        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual IList<Need> Amendments
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Need> AmendmentsBindingList { get; set; }

        public virtual IList<ClientDependent> Beneficiaries
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<ClientDependent> BeneficiariesBindingList { get; set; }

        private string _bequethTo;        
        public virtual string BequethTo
        {
            get
            {
                return _bequethTo;
            }
            set
            {
                _bequethTo = value;
                InvokePropertyChanged("BequethTo");
            }
        }

        #endregion

        #region NonPersisted Properties
        [IgnoreAutoMap]
        public virtual RiskAdviceRecord CurrentAdviceRecord
        {
            get
            {
                if (this.AdviceRecords == null || this.AdviceRecords.Count == 0)
                    this.AdviceRecords.Add(new RiskAdviceRecord());

                return this.AdviceRecords.LastOrDefault();
            }
            set { }
        }
        [IgnoreAutoMap]
        public virtual Note CurrentNote
        {
            get
            {
                if (this.Notes == null || this.Notes.Count == 0)
                    this.Notes.Add(new Note() { });

                return this.Notes.LastOrDefault();
            }
            set
            {

            }
        }
        [IgnoreAutoMap]
        public virtual DateTime AdviceDate
        {
            get { return CurrentAdviceRecord == null ? DateTime.Now : CurrentAdviceRecord.AdviceDate; }

        }
        [IgnoreAutoMap]
        public virtual bool IsCompleted
        {
            get { return CurrentAdviceRecord == null ? false : CurrentAdviceRecord.IsCompleted; }
            set { }

        }

        [IgnoreAutoMap]
        public virtual int CurrentAge
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual Lisp LispProvider
        {
            get;
            set;
        }
        #endregion

        public Life()
        {
            Benefits = new List<Benefit>();
            Notes = new List<Note>();
            Amendments = new List<Need>();
            Beneficiaries = new List<ClientDependent>();
            AdviceRecords=new List<RiskAdviceRecord>();

            Status = "Pending";
        }

        public override void Initialise(bool isLoading = false)
        {
            BenefitsBindingList = new BindingList<Benefit>(Benefits);
            BenefitsBindingList.RaiseListChangedEvents = true;
            BenefitsBindingList.ListChanged += BindingList_ListChanged;

            AmendmentsBindingList = new BindingList<Need>(Amendments);
            AmendmentsBindingList.RaiseListChangedEvents = true;
            AmendmentsBindingList.ListChanged += BindingList_ListChanged;

            BeneficiariesBindingList = new BindingList<ClientDependent>(Beneficiaries);
            BeneficiariesBindingList.RaiseListChangedEvents = true;
            BeneficiariesBindingList.ListChanged += BindingList_ListChanged;

            base.Initialise(isLoading);
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);

        }

        public override void Calculate()
        {
            if (!InvokeModelCalculating(new EntityEventArgs()))
                return;

            InvestmentYears = InvestmentAge - CurrentAge;
            if (Benefits.Count > 0)
            {
                foreach (Benefit benefit in Benefits)
                {
                    benefit.Calculate();
                }

                double sumInitialAmt = Benefits.Sum((Benefit x) => x.CoverAmount);
                //if (sumInitialAmt > 0.0)
                //{
                    InitialAmount = sumInitialAmt;
                //}
                double sumCurrentAmt = 0.0;
                //if (sumCurrentAmt > 0.0)
                //{
                    CurrentAmount = sumCurrentAmt;
                //}
            }
            if (Amendments.Count > 0)
            {
                foreach (Need amendment in Amendments)
                {
                    amendment.CurrentAge = CurrentAge;
                    amendment.Periods = Periods;
                    amendment.InflationPercentage = InflationPercentage;
                    amendment.Calculate();
                }
                Status = Amendments.LastOrDefault().Status;
            }
            
            FutureAmount = CurrentAmount;// FinFormula.FutureValue(CurrentAmount, MonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 0);
            FutureAmountInflationAdj = FinFormula.FutureValue(CurrentAmount, 0, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 0);

            InvokeModelCalculated();
        }

        public override void Validate(string FieldName = null)
        {
            if (FieldName != null && FieldName.ToLower() == "referenceno" && string.IsNullOrEmpty(ReferenceNo))
            {
                throw new MyValidationException($"{Name} :Reference No is required.");
            }
            
            base.Validate(FieldName);
        }
    }
}
