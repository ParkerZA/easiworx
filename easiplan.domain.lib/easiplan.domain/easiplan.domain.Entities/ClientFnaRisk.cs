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

	public class ClientFnaRisk : BaseEntity<int>
	{
        #region Private Variables
        private int _Periods;

		private double _InflationPercentage;

		private double _GrowthPercentage;

		private double _EscalationPercentage;

		private int _RiskAge;
		#endregion

		#region NonPersisted Properties
		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int InvestmentYears
		{
			get;set;
		}

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual int CurrentAge
		{
			get { return Client.ClientDetails.Age; }
			
		}		

		[IgnoreAutoMap]
		[IgnoreDataMember]
		public virtual Provider ServiceProvider
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
		#endregion

		#region Persisted Properties

		[IgnoreDataMember]
		public virtual int ClientId { get; set; }

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
		[IgnoreDataMember]
		public virtual int RiskAge
		{
			get
			{
				return _RiskAge;
			}
			set
			{
				if (_RiskAge == value) return;
				_RiskAge = value;

				Calculate();

				InvokePropertyChanged("RiskAge");

			}
		}

		public virtual IList<RiskCoverNeed> RiskCoverNeeds
		{
			get;
			set;
		}
		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual BindingList<RiskCoverNeed> RiskCoverNeedsBindingList { get; set; }

		public virtual IList<Note> FnaNotes
		{
			get;
			set;
		}
		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Note> NotesBindingList { get; set; }
        #endregion

        public ClientFnaRisk()
		{
			IsLoading = true;

			RiskCoverNeeds = new List<RiskCoverNeed>();

			Periods = 12;
			FnaNotes = new List<Note>();

			IsLoading = false;
		}

        public override void Initialise(bool isLoading = false)
        {
			//EXCLUDE Implemented Needs

			RiskCoverNeeds = RiskCoverNeeds.Where(x => x.Status != "Implemented").ToList();

			RiskCoverNeedsBindingList = new BindingList<RiskCoverNeed>(RiskCoverNeeds);
			RiskCoverNeedsBindingList.RaiseListChangedEvents = true;
			RiskCoverNeedsBindingList.ListChanged += RiskCoverNeedsBindingList_ListChanged;

			NotesBindingList = new BindingList<Note>(FnaNotes);
            NotesBindingList.RaiseListChangedEvents = true;
            NotesBindingList.ListChanged += NotesBindingList_ListChanged;

			foreach (RiskCoverNeed need in RiskCoverNeeds)
				need.Initialise(isLoading);

			base.Initialise(isLoading);
        }
		
		private void RiskCoverNeedsBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{

			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				IsLoading = true;

				RiskCoverNeeds[e.NewIndex].Client = this.Client;

				RiskCoverNeeds[e.NewIndex].NeedType = NeedTypes.RiskNeed;
				RiskCoverNeeds[e.NewIndex].InflationPercentage = InflationPercentage;
				RiskCoverNeeds[e.NewIndex].GrowthPercentage = GrowthPercentage;
				RiskCoverNeeds[e.NewIndex].EscalationPercentage = EsclPercentage;

				
				RiskCoverNeeds[e.NewIndex].CurrentAge = this.CurrentAge;
				RiskCoverNeeds[e.NewIndex].InvestmentAge = this.CurrentAge; //this.RiskAge;

				RiskCoverNeeds[e.NewIndex].Initialise();

				IsLoading = false;
			}

			//DO NOT INVOKE PROPERTY CHANGED EVENT HERE
			//if (e.PropertyDescriptor != null)
				//InvokePropertyChanged(e.PropertyDescriptor.Name);
		}

		private void NotesBindingList_ListChanged(object sender, ListChangedEventArgs e) { }

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			foreach (RiskCoverNeed need in RiskCoverNeeds)
            {
                need.IsLoading = true;

                need.CurrentAge = CurrentAge;
				//need.InvestmentAge = RiskAge;

                need.NeedType = NeedTypes.RiskNeed;
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

				if (need.Client==null)
					need.Client = this.Client;

				need.IsLoading = false;

                need.RecalcNewMonthlyPremium = true;

                need.Calculate();
            }
			
			InvokeModelCalculated(new EntityEventArgs());
		}

	}

}
