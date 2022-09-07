using my.domain.lib.core.Attributes;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.estate.Entities
{
    public class RiskNeed : baseCalcEntity
    {
		#region Properties

		string _Type;
		public virtual string Type
		{
			get { return _Type; }
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
			get;
			set;
		}
		
		public virtual string Beneficiary
		{
			get;
			set;
		}

		double _Value;

		public virtual double Value
		{
			get { return _Value; }
			set
			{
				if (_Value == value) return;

				_Value = value;

				Calculate();

				InvokePropertyChanged("Value");
			}
	}

		public virtual double RequiredValue
		{
			get;
			set;
		}
		#endregion

		public override void Calculate()
		{
			if (!InvokeModelCalculating())
				return;

			if (this.Type== "Income Protection")
            {
				RequiredValue = Value * 12 * 20;
				FutureAmount = FinFormula.FValue(Value, InflationPercentage, 1, 0.0, 0.0);
				RequiredValue = FinFormula.RequiredValue(Value, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 12);

			};

			if(this.Type == "Education"){
				//InflationPercentage = 3.5;
				//InvestmentYears = 10;
				//RequiredValue = FinFormula.FutureValue(Value, 0.0, InflationPercentage, 0.0, 0.0, InvestmentYears, 0);

				RequiredValue = Value;
			};

			if (this.Type == "Other")
			{
				RequiredValue = Value;
			};

				InvokeModelCalculated();
		}
		}
}
