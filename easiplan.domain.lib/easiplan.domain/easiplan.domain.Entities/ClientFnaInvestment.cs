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
	public class ClientFnaInvestment : BaseEntity<int>
	{
        #region Private Variables
        private int _Periods;

		private double _InflationPercentage;

		private double _GrowthPercentage;

		private double _EscalationPercentage;
        #endregion

        #region NonPersisted Properties
        [IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int InvestmentYears
		{
			get {
				if (Needs.Count > 0)
				{
					return Needs.Max((InvestmentNeed x) => x.InvestmentYears);
				}
				return 0;
			}
			set { }
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int _CurrentAge
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual Provider ServiceProvider
		{
			get;
			set;
		}
        #endregion

        #region Persisted Properties
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

		public virtual IList<InvestmentNeed> Needs
		{
			get;
			set;
		}
		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<InvestmentNeed> InvestmentNeedsBindingList { get; set; }

        public virtual IList<Note> FnaNotes
		{
			get;
			set;
		}
		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Note> NotesBindingList { get; set; }
        #endregion

        public ClientFnaInvestment()
		{
			IsLoading = true;

			Needs = new List<InvestmentNeed>();
			Periods = 12;
			FnaNotes = new List<Note>();

			IsLoading = false;
		}

        public override void Initialise(bool isLoading = false)
        {
            //EXCLUDE Implemented Needs
            Needs = Needs.Where(x => x.Status != "Implemented").ToList();

            InvestmentNeedsBindingList = new BindingList<InvestmentNeed>(Needs);
            InvestmentNeedsBindingList.RaiseListChangedEvents = true;
            InvestmentNeedsBindingList.ListChanged += InvestmentNeedsBindingList_ListChanged;


            NotesBindingList = new BindingList<Note>(FnaNotes);
            NotesBindingList.RaiseListChangedEvents = true;
            NotesBindingList.ListChanged += NotesBindingList_ListChanged;

			foreach (InvestmentNeed need in Needs)
				need.Initialise(isLoading);

			base.Initialise(isLoading);
        }

        private void InvestmentNeedsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
			
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				IsLoading = true;

				Needs[e.NewIndex].IsLoading = true;
				Needs[e.NewIndex].NeedType = NeedTypes.InvestmentNeed;
				Needs[e.NewIndex].InflationPercentage = InflationPercentage;
				Needs[e.NewIndex].GrowthPercentage = GrowthPercentage;
				Needs[e.NewIndex].EscalationPercentage = EsclPercentage;

				Needs[e.NewIndex].Initialise();
				Needs[e.NewIndex].IsLoading = false;

				IsLoading = false;
			}

            //if (e.PropertyDescriptor != null)
                //InvokePropertyChanged(e.PropertyDescriptor.Name);
        }
		private void NotesBindingList_ListChanged(object sender, ListChangedEventArgs e) { }

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			foreach (InvestmentNeed need in Needs)
			{
				need.IsLoading = true;

				need.CurrentAge = _CurrentAge;
				need.NeedType = NeedTypes.InvestmentNeed;
				need.Periods = Periods;
				need.InflationPercentage = InflationPercentage;
				need.GrowthPercentage = GrowthPercentage;
				need.EscalationPercentage = EsclPercentage;
				if (ServiceProvider != null)
				{
					need.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
					where x.LispName == need.Description
					select x).FirstOrDefault();
				}

				need.IsLoading = false;

				need.RecalcNewMonthlyPremium = true;

				need.Calculate();
			}

			InvokeModelCalculated(new EntityEventArgs());
		}

		#region Series
		public virtual Series GetNeedsSeries()
		{
			Series series = new Series("Investments");
			try
			{
				for (int i = 0; i <= InvestmentYears; i++)
				{
					double totNeeds = 0.0;
					foreach (InvestmentNeed item in from a in Needs
													where !a.IsImplemented && !a.IsCancelled
													select a)
					{
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
		#endregion
	}
}
