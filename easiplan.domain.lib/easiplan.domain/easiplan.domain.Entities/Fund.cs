using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class Fund : baseCalcEntity
	{
        #region Private Variables
        private double _SplitPercentage;

		private DateTime _StartDate;

        private DateTime _FundValueDate;

		private DateTime _EndDate;

        private double _WithdrawalAmount;
        #endregion

        #region NonPersisted Properties

        [IgnoreAutoMap]
        public virtual double PolicyPremium
        {
            get;
            set;
        }
      
        [IgnoreAutoMap]
        public virtual string Risk
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double IRR
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
        public virtual LispFund LispFund
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual double NewFundValue
        {
            get;
            set;
        }
        #endregion

        #region Persisted Properties

       public virtual string FundCode
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

        

        public virtual double SplitPerc
		{
			get
			{
				return _SplitPercentage;
			}
			set
			{
                if (_SplitPercentage == value) return;

                _SplitPercentage = value;

				Calculate();

				InvokePropertyChanged("SplitPercentage");
			}
		}

		public virtual DateTime StartDate
		{
			get
			{
				return _StartDate;
			}
			set
			{
                if (_StartDate == value) return;

                _StartDate = value;

				//Calculate();

				InvokePropertyChanged("StartDate");
			}
		}


        public virtual DateTime FundValueDate
        {
            get
            {
                return _FundValueDate;
            }
            set
            {
                if (_FundValueDate == value) return;

                _FundValueDate = value;

                //Calculate();

                //InvokePropertyChanged("UpdateDate");
            }
        }


        public virtual DateTime EndDate
		{
			get
			{
				return _EndDate;
			}
			set
			{
                if (_EndDate == value) return;

                _EndDate = value;

				//Calculate();

				InvokePropertyChanged("EndDate");
			}
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

        public virtual double WithdrawalAmount
        {
            get
            {
                return _WithdrawalAmount;
            }
            set
            {
                if (_WithdrawalAmount == value) return;

                _WithdrawalAmount = value;
				Calculate();
                InvokePropertyChanged("WithdrawalAmount");
            }
        }
        #endregion

        public Fund()
		{
            IsLoading = true;

			StartDate = DateTime.Now;

            IsLoading = false;
		}

		public override void Calculate()
		{
            if (!InvokeModelCalculating(new EntityEventArgs()))
                return;

            DateTime dateTime = StartDate;
			if (!dateTime.Equals(MinDateTime))
			{
				dateTime = StartDate;
				EndDate = dateTime.AddYears(InvestmentYears);
				dateTime = StartDate;
				StartDate = DateTime.Parse(dateTime.ToShortDateString());
				dateTime = EndDate;
				EndDate = DateTime.Parse(dateTime.ToShortDateString());
			}

			MonthlyContribution = SplitPerc / 100.0 * PolicyPremium;

			NewFundValue = CurrentAmount - WithdrawalAmount + InitialAmount;

            FutureAmount = FinFormula.FutureValue(NewFundValue, MonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 0);
			FutureAmountInflationAdj = FinFormula.FutureValue(NewFundValue, MonthlyContribution, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 0);

            if (LispFund != null)
			{
				Risk = LispFund.RiskCat;
				IRR = LispFund.IRR;
			}

			InvokeModelCalculated();
		}

		public override void Validate(string FieldName = null)
		{
			if (FieldName != null && FieldName.ToLower() == "startdate" && StartDate.Equals(MinDateTime))
			{
				throw new MyValidationException($"{Name} :Fund Inception Date is required.");
			}
			base.Validate(FieldName);
		}
	}
}
