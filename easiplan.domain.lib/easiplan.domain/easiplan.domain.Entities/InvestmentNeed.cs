using easiplan.domain.Model;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace easiplan.domain.Entities
{
	public class InvestmentNeed : Need
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
        #endregion

        public InvestmentNeed()
		{
			IsLoading = true;

			Status = "Advised";
			Funds = new List<Fund>();
			Notes = new List<Note>();
			NeedType = NeedTypes.InvestmentNeed;

			IsLoading = false;
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;
	
			FutureAmount = FinFormula.FutureValue(RequiredAmount==0?InitialAmount:RequiredAmount, 0.0, InflationPercentage, 0.0, 0.0, InvestmentYears, 0);

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
                Series series = new Series(string.Format("{0}", Type));
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
