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
    public class Investment : baseCalcEntity
    {
        private double _fundSplitPercentage;

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

        public virtual IList<Fund> Funds
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Fund> FundsBindingList { get; set; }

        public virtual IList<ClientDependent> Beneficiaries
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<ClientDependent> BeneficiariesBindingList { get; set; }

        [IgnoreDataMember]
        public virtual IList<Note> Notes
        {
            get;
            set;
        }


        [IgnoreDataMember]
        public virtual IList<ClientAdviceRecord> AdviceRecords
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


        public virtual double FundsSplitPerc
        {
            get
            {
                return _fundSplitPercentage;
            }
            set
            {
                if (_fundSplitPercentage == value) return;
                _fundSplitPercentage = value;

                Calculate();
                InvokePropertyChanged("FundSplitPercentage");
            }
        }
        #endregion

        #region NonPersisted Properties

        [IgnoreAutoMap]
        public virtual ClientAdviceRecord CurrentAdviceRecord
        {
            get
            {
                if (this.AdviceRecords == null || this.AdviceRecords.Count == 0)
                    this.AdviceRecords.Add(new ClientAdviceRecord());

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

        public Investment()
        {
            Funds = new List<Fund>();
            Notes = new List<Note>();
            Amendments = new List<Need>();
            Beneficiaries = new List<ClientDependent>();

            AdviceRecords = new List<ClientAdviceRecord>();

            Status = "Pending";
        }

        public override void Initialise(bool isLoading = false)
        {
            FundsBindingList = new BindingList<Fund>(Funds);
            FundsBindingList.RaiseListChangedEvents = true;
            FundsBindingList.ListChanged += BindingList_ListChanged;

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
            if (Funds.Count > 0)
            {
                foreach (Fund fund in Funds)
                {
                    fund.PolicyPremium = MonthlyContribution;
                    fund.InvestmentYears = InvestmentYears;
                    fund.Periods = Periods;
                    fund.InflationPercentage = InflationPercentage;
                    if (LispProvider != null)
                    {
                        fund.LispFund = (from x in LispProvider.LispFunds
                                         where x.FundName == fund.Description
                                         select x).FirstOrDefault();
                    }
                    fund.Calculate();
                }
                FundsSplitPerc = Funds.Sum((Fund x) => x.SplitPerc);
                double sumInitialAmt = Funds.Sum((Fund x) => x.InitialAmount);
                //if (sumInitialAmt > 0.0)
                //{
                    InitialAmount = sumInitialAmt;
                //}
                double sumCurrentAmt = Funds.Sum((Fund x) => x.CurrentAmount);
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
            
            FutureAmount = FinFormula.FutureValue(CurrentAmount, MonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 0);
            FutureAmountInflationAdj = FinFormula.FutureValue(CurrentAmount, MonthlyContribution, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 0);

            InvokeModelCalculated();
        }

        public override void Validate(string FieldName = null)
        {
            if (FieldName != null && FieldName.ToLower() == "referenceno" && string.IsNullOrEmpty(ReferenceNo))
            {
                throw new MyValidationException($"{Name} :Reference No is required.");
            }
            if (Funds.Count > 0 && 100.0 != Funds.Sum((Fund x) => x.SplitPerc))
            {
                throw new MyValidationException($"{Name} :Incorrect or missing % Premium split.");
            }
            if (Funds.Count > 0 && InitialAmount != Funds.Sum((Fund x) => x.InitialAmount))
            {
                throw new MyValidationException($"{Name} :Incorrect or missing Lump Sum allocation.");
            }
            base.Validate(FieldName);
        }
    }
}
