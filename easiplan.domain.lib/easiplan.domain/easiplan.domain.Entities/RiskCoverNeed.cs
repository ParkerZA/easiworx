using easiplan.domain.Model;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace easiplan.domain.Entities
{
	public class RiskCoverNeed : Need
	{
        [IgnoreAutoMap]
        public virtual new RiskAdviceRecord CurrentAdviceRecord
        {
            get
            {
                if (this.RiskAdviceRecords == null || this.RiskAdviceRecords.Count == 0)
                    this.RiskAdviceRecords.Add(new RiskAdviceRecord());

                return this.RiskAdviceRecords.LastOrDefault();
            }
            set
            {

            }
        }
        #region Properties
        /// <summary>
        /// The Risk Cover type
        /// e.g LifeCover, Disability, DreadedDisease etc
        /// </summary>
        private string _CoverType;
		public virtual string CoverType { 
            get { return _CoverType; } 
            set { 
                _CoverType = value;
                _RequiredAmount = 0;

                Calculate();

                InvokePropertyChanged("CoverType");
            } 
        }

		private double _RequiredAmount;
        /// <summary>
        /// The monthly amount required when the policy pays out
        /// </summary>
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

                Calculate();

                InvokePropertyChanged("RequiredAmount");
			}
		}

        public virtual IList<RiskAdviceRecord> RiskAdviceRecords 
        {
            get;
            set;
        }

        #endregion

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual Client Client
        {
            get;
            set;
        }

        bool _CalculateCurrentAmount;
       
        [IgnoreDataMember]
        public virtual bool CalculateCurrentAmount
        {
            get { return _CalculateCurrentAmount; }
            set
            {
                _CalculateCurrentAmount = value;
               
                Calculate();

                InvokePropertyChanged("CalculateCurrentAmount");
            }
        }

        

        public RiskCoverNeed()
		{
			IsLoading = true;

			Status = "Advised";
			Funds = new List<Fund>();
			Notes = new List<Note>();
			NeedType = NeedTypes.RiskNeed;
            RiskAdviceRecords = new List<RiskAdviceRecord>();   

            IsLoading = false;
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

            if (InitialAmount==0)
            {
                if (CoverType == "Life Cover" && CalculateCurrentAmount)
                    CurrentAmount = Client.ClientExpenses.Expenses.Where(x => x.LifePerc > 0).Sum(y => y.Value * y.LifePerc / 100);

                if (CoverType == "Disability Cover" && CalculateCurrentAmount)
                    CurrentAmount = Client.ClientExpenses.Expenses.Where(x => x.DisabilityPerc > 0).Sum(y => y.Value * y.DisabilityPerc / 100);

                if (InvestmentAge > CurrentAge)               
                    _RequiredAmount = FinFormula.FValue(CurrentAmount, InflationPercentage, InvestmentAge - CurrentAge, 0.0, 0.0);                    
                else
                    _RequiredAmount = 0;

                FutureAmount = FinFormula.RequiredValue(_RequiredAmount, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears, 12);

            }
            else
            {
                FutureAmount = InitialAmount;

                if (InvestmentAge > CurrentAge)
                    _RequiredAmount = FinFormula.FValue(CurrentAmount, InflationPercentage, InvestmentAge - CurrentAge, 0.0, 0.0);
                else
                    _RequiredAmount = 0;

                if (_RequiredAmount > 0)
                    InvestmentYears = FinFormula.RequiredTerm(0, FutureAmount, _RequiredAmount, GrowthPercentage, InflationPercentage, EscalationPercentage);
                else
                {
                    InvestmentYears = 0;
                    FutureAmount = 0;
                }

                
            }


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
                Series series = new Series(string.Format("{0}", CoverType));
                try
                {
                    

                    if (InitialAmount == 0)
                    {
                        for (int i = 0; i <= InvestmentYears; i++)
                        {
                            double totNeeds = 0.0;

                            totNeeds += FinFormula.RequiredValue(RequiredAmount, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears - i);


                            series.Points.Add(new DataPoint((double)i, totNeeds));
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= InvestmentYears; i++)
                        {
                            double totNeeds = 0.0;

                            totNeeds += FinFormula.FutureValue(FutureAmount, RequiredAmount, GrowthPercentage, InflationPercentage, EscalationPercentage, InvestmentYears-i);


                            series.Points.Add(new DataPoint((double)i, totNeeds));
                        }
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
