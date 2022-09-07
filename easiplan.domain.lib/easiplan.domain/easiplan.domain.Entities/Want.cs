using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class Want : BaseEntity<int>
	{
		private string _Type;

		private string _Description;

		private double _InitialAmount;

		private double _CurrentAmount;

		private double _GrowthPercentage;

		private double _MonthlyContribution;

		private double _EscalationPercentage;

		private bool _CalculateInitialAmount;


		public virtual string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (_Type == value) return;
				_Type = value;

				Calculate();
				InvokePropertyChanged("Type");
			}
		}

		public virtual string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (_Description == value) return;
				_Description = value;

				//Calculate();
				//InvokePropertyChanged("Description");
			}
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
			get
			{
				return _GrowthPercentage;
			}
			set
			{
				if (_GrowthPercentage == value) return;
				_GrowthPercentage = value;

				Calculate();
				InvokePropertyChanged("GrowthPercentage");
			}
		}

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

				Calculate();
				InvokePropertyChanged("MonthlyContribution");
			}
		}

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

				Calculate();
				InvokePropertyChanged("EscalationPercentage");
			}
		}

		[IgnoreAutoMap]
		public virtual double FutureAmount
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		public virtual double FutureAmountInflationAdj
		{
			get;
			set;
		}

		[IgnoreAutoMap]
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

		[IgnoreAutoMap]
		public virtual double _RequiredAmount
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
		[IgnoreDataMember]
		public virtual Client Client
		{
			get;
			set;
		}
				
		public virtual bool CalculateInitialAmount {
			get
			{
				return _CalculateInitialAmount;
			}
			set
			{
				if (_CalculateInitialAmount == value) return;
				_CalculateInitialAmount = value;

				Calculate();

				InvokePropertyChanged("CalculateInitialAmount");
			}
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			InvestmentYears = InvestmentAge - CurrentAge;
			string type = Type;
			if (!(type == "LumpSum"))
			{
				

				if (type == "Annually")
				{
					MonthlyContribution = 0.0;
					FutureAmount = FinFormula.FValue(InitialAmount, InflationPercentage, InvestmentYears, 0.0, 0.0);
					_RequiredAmount = FinFormula.RequiredValue(FutureAmount, GrowthPercentage, InflationPercentage, EscalationPercentage, _RetirementYears, 1);
				}
				else
				{
					//Calculate Expected Expenses
					if (CalculateInitialAmount)
						InitialAmount = Client.ClientExpenses.Expenses.Where(x => x.RetirePerc > 0).Sum(y => y.Value * y.RetirePerc / 100);

					FutureAmount = FinFormula.FValue(InitialAmount, InflationPercentage, InvestmentYears, 0.0, 0.0);
					_RequiredAmount = FinFormula.RequiredValue(FutureAmount, GrowthPercentage, InflationPercentage, EscalationPercentage, _RetirementYears, 12);
				}
			}
			else
			{
				FutureAmount = FinFormula.FValue(InitialAmount, InflationPercentage, InvestmentYears, 0.0, 0.0);
				_RequiredAmount = FutureAmount;
			}

			InvokeModelCalculated();
		}
	}
}
