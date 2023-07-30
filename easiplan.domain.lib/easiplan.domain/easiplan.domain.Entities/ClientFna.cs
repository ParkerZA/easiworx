
using easiplan.domain.Model;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Formula;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace easiplan.domain.Entities
{
	public class ClientFna : BaseEntity<int>
	{
        #region Private Variables
        private int _RetirementYears;

		private int _InvestmentYears;

		private int _Periods;

		private double _InflationPercentage;

		private double _GrowthPercentage;

		private double _EscalationPercentage;

		private double _AvailableCash;

		private int _currentAge;

		private int _retireAge;

		private int _lifeAge;

        private bool _ShowNeeds = true;
        #endregion

        #region NonPersisted Properties
        [IgnoreAutoMap]
        public virtual double HavesTotal
        {
            get
            {
                return Haves.Sum((Have x) => x.FutureAmount);
            }
            set
            {
              
            }
        }
        [IgnoreAutoMap]
       // [IgnoreDataMember]
        public virtual double _HavesTotalPremium
        {
            get
            {
                return Haves.Sum((Have x) => x.MonthlyContribution);
            }
            set
            {
           
            }
        }

		[IgnoreAutoMap]
        public virtual double NeedsTotal
        {
            get
            {
                if (!_ShowNeeds)
                    return 0.0;
				try
				{
					return Needs.Sum((Need x) => x.FutureAmount);
				}
				catch (Exception x) { }

				return 0.0;
            }
            set
            {

				//InvokePropertyChanged("NeedsTotal");
			}
        }

        [IgnoreAutoMap]
        //[IgnoreDataMember]
        public virtual double _NeedsTotalPremium
        {
            get
            {
                if (!_ShowNeeds)
                    return 0.0;

				return Needs.Sum((Need x) => x.NewMonthlyContribution);
				//return (from a in Needs
    //                    where !a.IsImplemented && !a.IsCancelled
    //                    select a).Sum((Need x) => x.NewMonthlyContribution); ;
            }
            set
            {
              
            }
        }

		[IgnoreAutoMap]
        public virtual double WantsTotal
        {
            get
            {
				return Wants.Sum((Want x) => x._RequiredAmount); ;
            }
            set
            {
				InvokePropertyChanged("WantsTotal");
			}
        }

        [IgnoreAutoMap]
        //[IgnoreDataMember]
        public virtual double _WantsTotalPremium
        {
            get
            {
                return Wants.Sum((Want x) => x.MonthlyContribution); ;
            }
            set
            {
           
            }
        }

        [IgnoreAutoMap]
        public virtual double Shortfall
        {
            get
            {
                return (HavesTotal+NeedsTotal) - WantsTotal ;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual double AddContribs
        {
            get
            {
                if (!_ShowNeeds)
                    return 0.0;

                return (from a in Needs
                        where !a.IsImplemented && !a.IsCancelled
                        select a).Sum((Need x) => x.NewMonthlyContribution); ;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual double RequiredPremium
        {
            get
            {
                if (Shortfall <= 0.0)
                {
                    return FinFormula.MonthlyPayment(0.0, Math.Abs(Shortfall), GrowthPercentage, 0.0, EsclPercentage, InvestmentYears, 12);
                };
                return 0.0;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual double TotalContribs
        {
            get
            {
                return _HavesTotalPremium + AddContribs; ;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual double AvailableCash
        {
            get
            {
                return _AvailableCash;
            }
            set
            {
				if (_AvailableCash == value)
					return;

				_AvailableCash = value;

				InvokePropertyChanged("AvailableCash");
			}
        }

        [IgnoreAutoMap]
        public virtual double NettCashFlow
        {
            get
            {
                return AvailableCash - AddContribs;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual int _CurrentAge
        {
            get
            {
                return _currentAge;
            }
            set
            {
                _currentAge = value;
               // InvokePropertyChanged("_CurrentAge");
            }
        }

        [IgnoreAutoMap]
        public virtual int RetirementYears
        {
            get
            {
                return _RetirementYears;
            }
            set
            {
                _RetirementYears = value;
               // InvokePropertyChanged("RetirementYears");
            }
        }

        [IgnoreAutoMap]
        public virtual int InvestmentYears
        {
            get
            {
                return _InvestmentYears;
            }
            set
            {
                _InvestmentYears = value;
               // InvokePropertyChanged("InvestmentYears");
            }
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual Provider ServiceProvider
        {
            get;
            set;
        }

		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual bool ShowNeeds
        {
            get
            {
                return _ShowNeeds;
            }
            set
            {
				if (_ShowNeeds == value) return;

				_ShowNeeds = value;

				Calculate();

                InvokePropertyChanged("ShowNeeds");
            }
        }

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual Client Client
		{
			get;
			set;
		}
		#endregion

		#region Persisted Properties
		public virtual int RetirementAge
        {
            get
            {
                return _retireAge;
            }
            set
            {
				if (_retireAge == value) return;

                _retireAge = value;

				Calculate();

				InvokePropertyChanged("RetirementAge");
            }
        }

        public virtual int LifeExpectancy
        {
            get
            {
                return _lifeAge;
            }
            set
            {
				if (_lifeAge == value) return;
				_lifeAge = value;

				Calculate();

				InvokePropertyChanged("LifeExpectancy");
            }
        }        

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

				Calculate();

				InvokePropertyChanged("Periods");
			}
		}

		public virtual double InflationPercentage
		{
			get
			{
				return _InflationPercentage;
			}
			set
			{
				if (_InflationPercentage == value) return;

				_InflationPercentage = value;

				Calculate();

				InvokePropertyChanged("InflationPercentage");
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

		public virtual double EsclPercentage
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
        public virtual IList<Have> Haves
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Have> HavesBindingList { get; set; }

		IList<Want> _Wants = new List<Want>();
		public virtual IList<Want> Wants
        {
			get { return _Wants.ToList(); }
			set { _Wants = value; }
		}
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Want> WantsBindingList { get; set; }

		IList<Need> _Needs = new List<Need>();
		public virtual IList<Need> Needs
        {
			get { return _Needs.Where(x => x.Status != "Cancelled").Where(x=>x.Status != "Implemented").ToList(); }
            set { _Needs = value; }
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Need> NeedsBindingList { get; set; }

        public virtual IList<Note> FnaNotes
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Note> NotesBindingList { get; set; }


        #endregion

        public ClientFna()
		{
            IsLoading = true;

			Haves = new List<Have>();
			Wants = new List<Want>();
			Needs = new List<Need>();
			Periods = 12;
			FnaNotes = new List<Note>();

            RetirementAge = 65;
            LifeExpectancy = 85;
            
            IsLoading = false;
		}

        public override void Initialise(bool isLoading = false)
        {
            WantsBindingList = new BindingList<Want>(Wants);
            WantsBindingList.RaiseListChangedEvents = true;
            WantsBindingList.ListChanged += WantsBindingList_ListChanged;

            HavesBindingList = new BindingList<Have>(Haves);
            HavesBindingList.RaiseListChangedEvents = true;
            HavesBindingList.ListChanged += HavesBindingList_ListChanged;
						
			NeedsBindingList = new BindingList<Need>(Needs);
			NeedsBindingList.RaiseListChangedEvents = true;
            NeedsBindingList.ListChanged += NeedsBindingList_ListChanged;

            NotesBindingList = new BindingList<Note>(FnaNotes);
            NotesBindingList.RaiseListChangedEvents = true;
            NotesBindingList.ListChanged += NotesBindingList_ListChanged;

			//Initialise the FNA Needs
			foreach (var need in Needs)
				need.Initialise(isLoading);

			base.Initialise(isLoading);
        }
                
        private void NeedsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
	
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				IsLoading = true;

				NeedsBindingList[e.NewIndex].NeedType = NeedTypes.RetirementNeed;
				NeedsBindingList[e.NewIndex].InvestmentAge = RetirementAge;
				NeedsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
				NeedsBindingList[e.NewIndex].Periods = Periods;
				NeedsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;
				NeedsBindingList[e.NewIndex].GrowthPercentage = GrowthPercentage;
				NeedsBindingList[e.NewIndex].EscalationPercentage = EsclPercentage;

				NeedsBindingList[e.NewIndex].Initialise();

				_Needs.Add(NeedsBindingList[e.NewIndex]);

				IsLoading = false;
			}

			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				_Needs.RemoveAt(e.NewIndex);

				
				InvokePropertyChanged("NeedsTotal");
			}

				//Refresh the summary by invoking the propertychanged event
			if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);
			
		}
		private void WantsBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				IsLoading = true;
				WantsBindingList[e.NewIndex].Client = this.Client;

				WantsBindingList[e.NewIndex].Type = "Monthly";
				WantsBindingList[e.NewIndex].InvestmentAge = RetirementAge;
				WantsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
				WantsBindingList[e.NewIndex]._RetirementYears = RetirementYears;
				WantsBindingList[e.NewIndex].Periods = Periods;
				WantsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;
				WantsBindingList[e.NewIndex].GrowthPercentage = GrowthPercentage;
				WantsBindingList[e.NewIndex].EscalationPercentage = EsclPercentage;
						

				WantsBindingList[e.NewIndex].Initialise();

				_Wants.Add(WantsBindingList[e.NewIndex]);

				IsLoading = false;
			}

			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				_Wants.RemoveAt(e.NewIndex);

				InvokePropertyChanged("WantsTotal");
			}

			//Refresh the summary by invoking the propertychanged event
			if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);
        }
        private void HavesBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				HavesBindingList[e.NewIndex].CurrentAge = _CurrentAge;

				HavesBindingList[e.NewIndex].Calculate();
			}
		}
		private void NotesBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			//No refresh required
		}


		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			InvestmentYears = RetirementAge - _CurrentAge;
			RetirementYears = LifeExpectancy - RetirementAge;

			//Recalc the Wants
			foreach (Want want in Wants)
			{
				want.InvestmentAge = RetirementAge;
				want.CurrentAge = _CurrentAge;
				want._RetirementYears = RetirementYears;
				want.Periods = Periods;
				want.InflationPercentage = InflationPercentage;
				//want.GrowthPercentage = GrowthPercentage; YJ This is not required fo rthe want calculation
				//want.EscalationPercentage = _EscalationPercentage; - YJ This is not required fo rthe want calculation		

				if (want.Client == null)
					want.Client = this.Client;

				want.Calculate();
			}

			//Recalc the haves
			//foreach (Have have in Haves)
			//{
			//    have.InvestmentAge = RetirementAge;
			//    have.CurrentAge = _CurrentAge;
			//    have._RetirementYears = RetirementYears;
			//    have.Periods = Periods;
			//    have.InflationPercentage = InflationPercentage;
			//    //want.GrowthPercentage = GrowthPercentage; YJ This is not required fo rthe have calculation
			//    //want.EscalationPercentage = _EscalationPercentage; - YJ This is not required fo rthe have calculation
			//    have.Calculate();
			//}

			//Recalc the Needs
			foreach (Need need in Needs.Where(x => x.Status != "Implemented").ToList())
            {
                need.InvestmentAge = RetirementAge;
                need.CurrentAge = _CurrentAge;
                need.Periods = Periods;
                need.InflationPercentage = InflationPercentage;
                if (need.Id == 0)
                {
                    if (need.GrowthPercentage == 0.0)
                    {
                        need.GrowthPercentage = GrowthPercentage;
                    }
                    if (need.EscalationPercentage == 0.0)
                    {
                        need.EscalationPercentage = EsclPercentage;
                    }
                }
                if (ServiceProvider != null)
                {
                    need.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
                                         where x.LispName == need.Description
                                         select x).FirstOrDefault();
                }
                need.Calculate();
            }

			InvokeModelCalculated();
		}

        #region Series
        public virtual Series GetHavesSeries()
		{
			Series series = new Series("Haves");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totHaves = 0.0;
					foreach (Have have in Haves)
					{
						totHaves += FinFormula.FutureValue(have.CurrentAmount, have.MonthlyContribution, have.GrowthPercentage, 0.0, have.EscalationPercentage, i, 0);
					}
					series.Points.Add(new DataPoint((double)i, totHaves));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual Series GetHavesSeriesInflationAdj()
		{
			Series series = new Series("Haves Inflation Adj");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totHaves = 0.0;
					foreach (Have have in Haves)
					{
						totHaves += FinFormula.FutureValue(have.CurrentAmount, have.MonthlyContribution, have.GrowthPercentage, InflationPercentage, have.EscalationPercentage, i, 0);
					}
					series.Points.Add(new DataPoint((double)i, totHaves));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual Series GetWantsSeries()
		{
			Series series = new Series("Wants");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totWants = 0.0;
					foreach (Want want in Wants)
					{
						double FutureAmount = FinFormula.FValue(want.InitialAmount, InflationPercentage, i, 0.0, 0.0);
						string type = want.Type;
						if (!(type == "LumpSum"))
						{
							totWants = ((!(type == "Annually")) ? (totWants + FinFormula.RequiredValue(FutureAmount, want.GrowthPercentage, InflationPercentage, want.EscalationPercentage, want._RetirementYears, 12)) : (totWants + FinFormula.RequiredValue(FutureAmount, want.GrowthPercentage, InflationPercentage, want.EscalationPercentage, want._RetirementYears, 1)));
						}
						else if (i <= want.InvestmentYears)
						{
							totWants += FutureAmount;
						}
					}
					series.Points.Add(new DataPoint((double)i, totWants));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual Series GetWantsSeriesInflationAdj()
		{
			Series series = new Series("Wants  Inflation Adj");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					series.Points.Add(new DataPoint((double)i, WantsTotal));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual Series GetNeedsSeries()
		{
			Series series = new Series("Needs");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totNeeds = 0.0;
					foreach (Need item in from a in Needs
					where !a.IsImplemented && !a.IsCancelled && a.Status != "Implemented"
                                          select a)
					{
                        item.NewMonthlyContribution = item.NewMonthlyContribution == 0 ? item.MonthlyContribution : item.NewMonthlyContribution;
                        totNeeds += FinFormula.FutureValue(item.InitialAmount, item.NewMonthlyContribution, item.GrowthPercentage, 0.0, item.EscalationPercentage, i, 0);
					}
					series.Points.Add(new DataPoint((double)i, totNeeds));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual Series GetNeedsSeriesInflationAdj()
		{
			Series series = new Series("Needs Inflation Adj");
			try
			{
				foreach (KeyValuePair<int, double> item in FinFormula.FutureValueList(Needs.Sum((Need x) => x.InitialAmount), Needs.Sum((Need x) => x.MonthlyContribution), Needs.Average((Need x) => x.GrowthPercentage), InflationPercentage, Needs.Average((Need x) => x.EscalationPercentage), InvestmentYears, 0))
				{
					series.Points.Add(new DataPoint((double)item.Key, item.Value));
				}
			}
			catch (Exception)
			{
				series.Points.Add(new DataPoint(0.0, 0.0));
			}
			return series;
		}

		public virtual List<FinDataPoint> GetHavesDataPoints()
		{
			List<FinDataPoint> series = new List<FinDataPoint>();
			try
			{
				for (int j = 0; j <= InvestmentYears; j++)
				{
					double totHaves = 0.0;
					foreach (Have hafe in Haves)
					{
						totHaves += FinFormula.FutureValue(hafe.InitialAmount, hafe.MonthlyContribution, hafe.GrowthPercentage, 0.0, hafe.EscalationPercentage, j, 0);
					}
					series.Add(new FinDataPoint((double)j, totHaves, "Haves"));
				}
			}
			catch (Exception)
			{
				series.Add(new FinDataPoint(0.0, 0.0, "Haves"));
			}
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totNeeds = 0.0;
					foreach (Need need in Needs)
					{
						totNeeds += FinFormula.FutureValue(need.InitialAmount, need.MonthlyContribution, need.GrowthPercentage, 0.0, need.EscalationPercentage, i, 0);
					}
					series.Add(new FinDataPoint((double)i, totNeeds, "Needs"));
				}
			}
			catch (Exception)
			{
				series.Add(new FinDataPoint(0.0, 0.0, "Needs"));
			}
			return series;
		}

		public virtual List<FinDataPoint> GetWantsDataPoints()
		{
			List<FinDataPoint> series = new List<FinDataPoint>();
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totWants = 0.0;
					foreach (Want want in Wants)
					{
						double FutureAmount = FinFormula.FValue(want.InitialAmount, InflationPercentage, i, 0.0, 0.0);
						string type = want.Type;
						if (!(type == "LumpSum"))
						{
							totWants = ((!(type == "Annually")) ? (totWants + FinFormula.RequiredValue(FutureAmount, want.GrowthPercentage, InflationPercentage, want.EscalationPercentage, want._RetirementYears, 12)) : (totWants + FinFormula.RequiredValue(FutureAmount, want.GrowthPercentage, InflationPercentage, want.EscalationPercentage, want._RetirementYears, 1)));
						}
						else if (i <= want.InvestmentYears)
						{
							totWants += FutureAmount;
						}
					}
					series.Add(new FinDataPoint((double)i, totWants, "Wants"));
				}
			}
			catch (Exception)
			{
				series.Add(new FinDataPoint(0.0, 0.0, "Wants"));
			}
			return series;
		}

		public virtual List<FinDataPoint> GetNeedsDataPoints()
		{
			List<FinDataPoint> series = new List<FinDataPoint>();
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totNeeds = 0.0;
					foreach (Need need in Needs)
					{
						totNeeds += FinFormula.FutureValue(need.InitialAmount, need.MonthlyContribution, need.GrowthPercentage, 0.0, need.EscalationPercentage, i, 0);
					}
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
