using easiplan.domain.Model;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace easiplan.domain.Entities
{
	public class EducationNeed : Need
	{
		#region Properties
		private double _RequiredAmount;
		public virtual double RequiredAmount
		{
			get
			{
				return _RequiredAmount;
			}
			set
			{
				if (_RequiredAmount == value) return;

				_RequiredAmount = value;

				if (IsLoading) return;

				if (!IsLoading)
					RecalcNewMonthlyPremium = true;

				Calculate();

				InvokePropertyChanged("RequiredAmount");
			}
		}

		private string _DependentName;
		public virtual string DependentName
		{
			get
			{
				return _DependentName;
			}
			set
			{
				if (_DependentName == value) return;

				_DependentName = value;

				InvokePropertyChanged("DependentName");
			}
		}

		private DateTime _DependentDOB;
		public virtual DateTime DependentDOB
		{
			get
			{
				return _DependentDOB;
			}
			set
			{
				if (_DependentDOB == value) return;

				_DependentDOB = value;
				CurrentAge = (int)((double)DateTime.Now.Subtract(_DependentDOB).Days / 365.242199);
				InvestmentYears = InvestmentAge - CurrentAge;

				if (IsLoading) return;

				if (!IsLoading)
					RecalcNewMonthlyPremium = true;

				Calculate();

				InvokePropertyChanged("DependentDOB");
			}
		}

        #endregion

        public EducationNeed()
		{
			IsLoading = true;

			Status = "Advised";
			InvestmentAge = 18;
			Funds = new List<Fund>();
			Notes = new List<Note>();
			//DependentDOB = MinDateTime;
			NeedType = NeedTypes.EducationNeed;

			IsLoading = false;
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating())
				return;


			DateTime dateTime;
			if (DependentDOB > DateTime.MinValue)
			{
				dateTime = DateTime.Now;
				CurrentAge = (int)((double)dateTime.Subtract(DependentDOB).Days / 365.242199);
			}
			else
			{
				DependentDOB = DateTime.Now;
			}
			
			dateTime = DependentDOB;
			DependentDOB = DateTime.Parse(dateTime.ToShortDateString());

			InvestmentYears = InvestmentAge - CurrentAge;

			FutureAmount = FinFormula.FutureValue(RequiredAmount, 0.0, InflationPercentage, 0.0, 0.0, InvestmentYears, 0);

			if (RecalcNewMonthlyPremium)
				NewMonthlyContribution = FinFormula.MonthlyPayment(InitialAmount, Math.Abs(FutureAmount), GrowthPercentage, 0.0, EscalationPercentage, InvestmentYears, 12);

            RecalcNewMonthlyPremium = false;

			base.CalculateFunds();

			base.SetCompletedStatus();

			InvokeModelCalculated(new EntityEventArgs());
		}

        #region Series
        public virtual Series GetNeedsSeries()
        {
            try
            {
                Series series = new Series(string.Format("{0}", DependentName));
                try
                {
                    for (int i = 0; i <= InvestmentYears; i++)
                    {
                        double totNeeds = 0.0;

                        totNeeds += FinFormula.FutureValue(InitialAmount, NewMonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, i, 0);

                        series.Points.Add(new DataPoint((double)i, totNeeds));
                    }
                }
                catch (Exception)
                {
                    series.Points.Add(new DataPoint(0.0, 0.0));
                }
                return series;
            }
            catch (Exception x)
            {

            }
            return null;
        }

        public virtual List<FinDataPoint> GetNeedsDataPoints()
        {
            List<FinDataPoint> series = new List<FinDataPoint>();
            try
            {
                for (int i = 0; i <= InvestmentYears; i++)
                {
                    double totNeeds = 0.0;

                    totNeeds += FinFormula.FutureValue(InitialAmount, NewMonthlyContribution, GrowthPercentage, 0.0, EscalationPercentage, i, 0);

                    series.Add(new FinDataPoint((double)i, totNeeds, "Needs"));
                }
            }
            catch (Exception)
            {
                series.Add(new FinDataPoint(0.0, 0.0, "Needs"));
            }
            return series;
        }
        #endregion
    }
}
