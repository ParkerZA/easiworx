using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class Have : BaseEntity<int>
	{
		private double _InitialAmount;

		private double _CurrentAmount;

		private double _FutureIncome;

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

				Calculate();
				InvokePropertyChanged("InitialAmount");
			}
		}

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

				Calculate();
				InvokePropertyChanged("CurrentAmount");
			}
		}

		public virtual double GrowthPercentage
		{
			get;
			set;
		}

		public virtual double MonthlyContribution
		{
			get;
			set;
		}

		public virtual double EscalationPercentage
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual double FutureAmount
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual double FutureAmountInflationAdj
		{
			get;
			set;
		}

		public virtual int InvestmentYears
		{
			get;
			set;
		}

		public virtual int InvestmentAge
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int _RetirementYears
		{
			get;
			set;
		}

		public virtual int Periods
		{
			get;
			set;
		}

		public virtual double InflationPercentage
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int CurrentAge
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		public virtual double FutureIncome
		{
			get
			{
				return _FutureIncome;
			}
			set
			{
				_FutureIncome = value;
				InvokePropertyChanged("FutureIncome");
			}
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			InvestmentYears = InvestmentAge - CurrentAge;
			FutureAmount = FinFormula.FutureValue(CurrentAmount, (MonthlyContribution < 0.0) ? 0.0 : MonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 0);
			FutureAmountInflationAdj = FinFormula.FutureValue(CurrentAmount, (MonthlyContribution < 0.0) ? 0.0 : MonthlyContribution, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 0);
			
			InvokeModelCalculated();
		}
	}
}
