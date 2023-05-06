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
	/// <summary>
	/// A Needs Analysis Entity
	/// </summary>
	public class Need : baseCalcEntity
	{
        #region Properties
        public virtual NeedTypes NeedType
		{
			get;
			set;
		}

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

		public virtual int ReferenceId
		{
			get;
			set;
		}

		public virtual int InstructionId
		{
			get;
			set;
		}

		public virtual bool IsImplemented
		{
			get;
			set;
		}

		public virtual bool IsCancelled
		{
			get;
			set;
		}

		public virtual bool IsUpdate
		{
			get;
			set;
		}

		public virtual double FundsSplitPerc
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
        public virtual BindingList<Fund> FundsBindingList { get; set; }

        public virtual IList<Benefit> Benefits
		{
			get;
			set;
		}
        [IgnoreAutoMap]
        public virtual BindingList<Benefit> BenefitsBindingList { get; set; }

        public virtual IList<Note> Notes
		{
			get;
			set;
		}

        public virtual IList<ClientDependent> Beneficiaries
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        public virtual BindingList<ClientDependent> BeneficiariesBindingList { get; set; }


        [IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual Lisp LispProvider
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		public virtual int CurrentAge
		{
			get;
			set;
		}

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual double NewPolicyValue
        {
            get;
            set;
        }

		private double _NewMonthlyContribution;
		public virtual double NewMonthlyContribution
        {
            get
            {
                return _NewMonthlyContribution;
            }
            set
            {
                if (_NewMonthlyContribution == value) return;

                _NewMonthlyContribution = value;

				if (IsLoading) return;

				RecalcNewMonthlyPremium = false;

				Calculate();

                InvokePropertyChanged("NewMonthlyContribution");
            }
        }

        public virtual string Insured
        {
            get;
            set;
        }
        #endregion

        public Need()
		{
            IsLoading = true;

			Status = "Advised";
			Funds = new List<Fund>();
			NeedType = NeedTypes.RetirementNeed;
			Notes = new List<Note>();
			Benefits = new List<Benefit>();
            Beneficiaries = new List<ClientDependent>();

            IsLoading = false;

		}
        
        public override void Initialise(bool isLoading = false)
        {
            FundsBindingList = new BindingList<Fund>(Funds);
            FundsBindingList.RaiseListChangedEvents = true;
            FundsBindingList.ListChanged += Funds_BindingList_ListChanged;

            BenefitsBindingList = new BindingList<Benefit>(Benefits);
            BenefitsBindingList.RaiseListChangedEvents = true;
            BenefitsBindingList.ListChanged += Benefits_BindingList_ListChanged;

            BeneficiariesBindingList = new BindingList<ClientDependent>(Beneficiaries);
            BeneficiariesBindingList.RaiseListChangedEvents = true;
            BeneficiariesBindingList.ListChanged += Beneficiaries_BindingList_ListChanged;

			foreach (Fund fund in Funds)
				fund.Initialise(isLoading);

			foreach (Benefit benefit in Benefits)
				benefit.Initialise(isLoading);

			base.Initialise(isLoading);
        }

        private void Funds_BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				Funds[e.NewIndex].InvestmentAge = InvestmentAge;
				Funds[e.NewIndex].InflationPercentage = InflationPercentage;
				Funds[e.NewIndex].GrowthPercentage = GrowthPercentage;
				Funds[e.NewIndex].EscalationPercentage = EscalationPercentage;
				Funds[e.NewIndex].CurrentAge = CurrentAge;
				Funds[e.NewIndex].PolicyPremium = NewMonthlyContribution;
				Funds[e.NewIndex].InvestmentYears = InvestmentYears;
				Funds[e.NewIndex].Periods = Periods;

				Funds[e.NewIndex].Initialise();
			}
			CalculateFunds();
		}
		private void Benefits_BindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
            //if (e.PropertyDescriptor != null)
            //{
            //    InvokePropertyChanged(e.PropertyDescriptor.Name);
            //}
        }
		private void Beneficiaries_BindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
            //if (e.PropertyDescriptor != null)
            //{
            //    InvokePropertyChanged(e.PropertyDescriptor.Name);
            //}
        }

        public override void Calculate()
		{
            if (!InvokeModelCalculating())
                return;

			InvestmentYears = InvestmentAge - CurrentAge;

			FutureAmount = FinFormula.FutureValue(InitialAmount, NewMonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 0);
			FutureAmountInflationAdj = FinFormula.FutureValue(InitialAmount, NewMonthlyContribution, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 0);
						
			CalculateFunds();

			SetCompletedStatus();

			InvokeModelCalculated();
			
		}

		protected virtual void CalculateFunds()
        {
			
			if (Funds.Count > 0)
			{
				foreach (Fund fund in Funds)
				{
					fund.IsLoading = true;

					fund.PolicyPremium = NewMonthlyContribution;
					fund.InvestmentYears = InvestmentYears;
					fund.Periods = Periods;
					fund.InflationPercentage = InflationPercentage;
					fund.EscalationPercentage = EscalationPercentage;

					if (LispProvider != null)
					{
						fund.LispFund = (from x in LispProvider.LispFunds
										 where x.FundName == fund.Description
										 select x).FirstOrDefault();
					}

					fund.IsLoading = false;

					fund.Calculate();

					
				}

				//CurrentAmount = Funds.Sum((Fund x) => x.CurrentAmount);

				//if(InitialAmount==0)
				//	InitialAmount = Funds.Sum((Fund x) => x.InitialAmount)- Funds.Sum((Fund x) => x.WithdrawalAmount);

				//NewPolicyValue = CurrentAmount + InitialAmount;

				//YJ 04/05/2023
				double sumInitialAmt = Funds.Sum((Fund x) => x.InitialAmount);
                double sumWithdrawals = Funds.Sum((Fund x) => x.WithdrawalAmount);
				InitialAmount= sumInitialAmt- sumWithdrawals;
            }
		}
		protected virtual void SetCompletedStatus()
		{

			if (Status == "Completed")
			{
				IsImplemented = true;
			}
		}
		public override void Validate(string FieldName = null)
		{
			if (FieldName != null && FieldName.ToLower() == "referenceno" && string.IsNullOrEmpty(ReferenceNo))
			{
				throw new MyValidationException($"{Name} :Policy No is required.");
			}

            switch (NeedType)
            {
				case NeedTypes.LifeDisabilityNeed:
					break;
				case NeedTypes.MedicalNeed:
					break;
				case NeedTypes.RiskNeed:
					break;
				default:
					if (Funds.Count == 0)
						throw new MyValidationException($"{Name} :Funds allocation is required.");

					if (FieldName != null && FieldName.ToLower() == "startdate" && Funds.Count > 0)
					{
						foreach (Fund fund in Funds)
						{
							fund.Validate("startdate");
						}
					}
					if (Funds.Count > 0 && 100.0 != Funds.Sum((Fund x) => x.SplitPerc))
					{
						throw new MyValidationException($"{Name} :Incorrect or missing % Premium split.");
					}

					double sumInitialAmt = Funds.Sum((Fund x) => x.InitialAmount);
					double sumWithdrawals = Funds.Sum((Fund x) => x.WithdrawalAmount);
                    double totalInitialAmt = sumInitialAmt - sumWithdrawals;
                    if (totalInitialAmt != InitialAmount)
					{
						throw new MyValidationException($"{Name} :Incorrect or missing Deposit/Withdrawal allocation.");
					}
					break;
            }
			

			base.Validate(FieldName);
		}
	}
}
