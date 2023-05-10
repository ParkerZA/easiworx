using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Runtime.Serialization;


namespace easiplan.domain
{
	/// <summary>
	/// Abstract Calculation Entity
	/// </summary>
	public abstract class baseCalcEntity : BaseEntity<int>
	{
		private bool _IsCalculating = false;

		[IgnoreAutoMap]
		public virtual bool RecalcNewMonthlyPremium { get; set; }

        #region Private Variables
        private double _InitialAmount;

		private double _CurrentAmount;

		private double _GrowthPercentage;

		private double _MonthlyContribution;

		private double _EscalationPercentage;

		private int _InvestmentAge;

		private int _InvestmentYears;

		private int _Periods;

        private double _FutureAmount;

        private double _FutureAmountAdj;
        #endregion

        #region NonPersisted Properties

        [IgnoreAutoMap]
        public virtual double FutureAmount
        {
            get
            {
				return _FutureAmount;				
			}
			set
            {
                if(_FutureAmount == value) return;

                _FutureAmount = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double FutureAmountInflationAdj
        {
            get
            {
                return _FutureAmountAdj;
				
			}
			set
            {
                if (_FutureAmountAdj == value) return;

                _FutureAmountAdj = value;
                
            }
        }      
        

        [IgnoreAutoMap]
        public virtual double InflationPercentage
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public override string Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                if (base.Status == value) return;

                base.Status = value;
               
            }
        }

        #endregion

        #region Persisted Properties
		/// <summary>
		/// Lumpsum deposit or starting value
		/// </summary>
        public virtual double InitialAmount
		{
			get
			{
				return _InitialAmount;
			}
			set
			{
                if (_InitialAmount == value) return;

                _InitialAmount = value;

				if (IsLoading) return;

				Calculate();

				InvokePropertyChanged("InitialAmount");
			}
		}

		/// <summary>
		/// Current Value
		/// </summary>
		public virtual double CurrentAmount
		{
			get
			{
				return _CurrentAmount;
			}
			set
			{
                if (_CurrentAmount == value) return;

                _CurrentAmount = value;

				if (IsLoading) return;

				Calculate();

				InvokePropertyChanged("CurrentAmount");
			}
		}

		/// <summary>
		/// Expected average growth percentage of underlying investments
		/// </summary>
		public virtual double GrowthPercentage
		{
			get
			{
				return _GrowthPercentage;
			}
			set
			{
                if (_GrowthPercentage == value) return;

                _GrowthPercentage = value;

				if (IsLoading) return;

				RecalcNewMonthlyPremium = true;

                Calculate();

				InvokePropertyChanged("GrowthPercentage");
			}
		}

		/// <summary>
		/// Monthly installments or premiums
		/// </summary>
		/// 
		//[Index(29)]
		public virtual double MonthlyContribution
		{
			get
			{
                return _MonthlyContribution;
			}
			set
			{
                if (_MonthlyContribution == value) return;

                _MonthlyContribution = value;

				if (IsLoading) return;

				Calculate();

				InvokePropertyChanged("MonthlyContribution");
			}
		}

		/// <summary>
		/// Annual Premium escalation % to counter inflation
		/// </summary>
		public virtual double EscalationPercentage
		{
			get
			{
				return _EscalationPercentage;
			}
			set
			{
                if (_EscalationPercentage == value) return;

                _EscalationPercentage = value;

				if (IsLoading) return;

                RecalcNewMonthlyPremium = true;

                Calculate();

				InvokePropertyChanged("EscalationPercentage");
			}
		}

		/// <summary>
		/// Total number of years invested
		/// </summary>
		public virtual int InvestmentYears
		{
			get
			{
				return _InvestmentYears;
			}
			set
			{
				if (_InvestmentYears == value) return;

				_InvestmentYears = value;

				if (IsLoading || IsCalculating) return;

				Calculate();

				InvokePropertyChanged("InvestmentYears");
			}
		}

		/// <summary>
		/// The members future age
		/// </summary>
		public virtual int InvestmentAge
		{
			get
			{
				return _InvestmentAge;
			}
			set
			{
                if (_InvestmentAge == value) return;

                _InvestmentAge = value;

				if (IsLoading) return;

				Calculate();

				InvokePropertyChanged("InvestmentAge");
			}
		}

		/// <summary>
		/// The number of payment periods in 1 year ...usually 12 (monthly)
		/// </summary>
		public virtual int Periods
		{
			get
			{
				return _Periods;
			}
			set
			{
                if (_Periods == value) return;

                _Periods = value;

				if (IsLoading) return;

				Calculate();

				InvokePropertyChanged("Periods");
			}
		}

		#endregion


	}
}
