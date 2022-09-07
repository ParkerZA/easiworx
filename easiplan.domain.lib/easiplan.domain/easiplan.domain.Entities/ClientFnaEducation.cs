
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
	public class ClientFnaEducation : BaseEntity<int>
	{
        #region Private Variables
        private int _Periods;

		private double _InflationPercentage;

		private double _GrowthPercentage;

		private double _EscalationPercentage;
        #endregion

        #region NonPersisted Properties
        [IgnoreAutoMap]
        public virtual int RetirementYears
		{
			get;
			set;
		}

      
        [IgnoreAutoMap]
		public virtual int InvestmentYears
		{
            get {
                if (EducationNeeds.Count > 0)
                {
                    return EducationNeeds.Max((EducationNeed x) => x.InvestmentYears);
                }
                return 0;
            }
            set { }
		}
      
		[IgnoreAutoMap]
		public virtual int _CurrentAge
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		public virtual int _RetirementAge
		{
			get;
			set;
		}

		[IgnoreAutoMap]
		public virtual int _LifeExpectancy
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
        public virtual IList<EducationNeed> EducationNeeds
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<EducationNeed> EducationNeedsBindingList { get; set; }

        public virtual IList<Note> FnaNotes
        {
            get;
            set;
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual BindingList<Note> NotesBindingList { get; set; }


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

        #endregion

        public ClientFnaEducation()
		{
            IsLoading = true;

			EducationNeeds = new List<EducationNeed>();
			Periods = 12;
			FnaNotes = new List<Note>();

            IsLoading = false;
		}

        public override void Initialise(bool isLoading = false)
        {
            //EXCLUDE Implemented Needs
            EducationNeeds = EducationNeeds.Where(x => x.Status != "Implemented").ToList();

            EducationNeedsBindingList = new BindingList<EducationNeed>(EducationNeeds);
            EducationNeedsBindingList.RaiseListChangedEvents = true;
            EducationNeedsBindingList.ListChanged += EducationNeedsBindingList_ListChanged;
                   
            NotesBindingList = new BindingList<Note>(FnaNotes);
            NotesBindingList.RaiseListChangedEvents = true;
            NotesBindingList.ListChanged += NotesBindingList_ListChanged;

            //Initialise the Education Needs
            foreach (EducationNeed educationNeed in EducationNeeds)
                educationNeed.Initialise(isLoading);

            base.Initialise(isLoading);
        }

        private void EducationNeedsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
           

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                EducationNeeds[e.NewIndex].IsLoading = true;

                EducationNeeds[e.NewIndex].NeedType = NeedTypes.EducationNeed;
                EducationNeeds[e.NewIndex].InflationPercentage = InflationPercentage;
                EducationNeeds[e.NewIndex].GrowthPercentage = GrowthPercentage;
                EducationNeeds[e.NewIndex].EscalationPercentage = EsclPercentage;

                //EducationNeeds[e.NewIndex].CurrentAge = _CurrentAge;
                //EducationNeeds[e.NewIndex].EscalationPercentage = EsclPercentage;
                //EducationNeeds[e.NewIndex].EscalationPercentage = EsclPercentage;

                EducationNeeds[e.NewIndex].Initialise();

                EducationNeeds[e.NewIndex].IsLoading = false;

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

            foreach (EducationNeed educationNeed in EducationNeeds)
            {
                educationNeed.IsLoading = true;

                //educationNeed.CurrentAge = _CurrentAge;
                educationNeed.NeedType = NeedTypes.EducationNeed;
                educationNeed.Periods = Periods;
                educationNeed.InflationPercentage = InflationPercentage;
                educationNeed.GrowthPercentage = GrowthPercentage;
                educationNeed.EscalationPercentage = EsclPercentage;

                if (ServiceProvider != null)
                {
                    educationNeed.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
                                                  where x.LispName == educationNeed.Description
                                                  select x).FirstOrDefault();
                }

                educationNeed.RecalcNewMonthlyPremium = true;

                educationNeed.IsLoading = false;
                educationNeed.Calculate();
            }

            InvokeModelCalculated(new EntityEventArgs());
        }

    }
}
