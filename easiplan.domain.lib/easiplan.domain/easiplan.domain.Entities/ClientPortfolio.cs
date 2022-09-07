using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace easiplan.domain.Entities
{
	public class ClientPortfolio : BaseEntity<int>
	{
        #region Private Variables
        private int _RetirementYears;

		private int _InvestmentYears;

		private int _Periods;

		private double _InflationPercentage;

		private double _GrowthPercentage;

		private double _LifeGauge;

		private double _MedicalGauge;

		private double _RetirementGauge;

		private double _EstateGauge;

		private double _InvestmentGauge;

		private double _EducationGauge;
        #endregion

        #region NonPersisted Properties
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual Provider ServiceProvider
        {
            get;
            set;
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
                //InvokePropertyChanged("RetirementYears");
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
                //InvokePropertyChanged("InvestmentYears");
            }
        }


        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double LifeGauge
        {
            get
            {
                return _LifeGauge;
            }
            set
            {
                _LifeGauge = value;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double MedicalGauge
        {
            get
            {
                return _MedicalGauge;
            }
            set
            {
                _MedicalGauge = value;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementGauge
        {
            get
            {
                return _RetirementGauge;
            }
            set
            {
                _RetirementGauge = value;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double EstateGauge
        {
            get
            {
                return _EstateGauge;
            }
            set
            {
                _EstateGauge = value;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double InvestmentGauge
        {
            get
            {
                return _InvestmentGauge;
            }
            set
            {
                _InvestmentGauge = value;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double EducationGauge
        {
            get
            {
                return _EducationGauge;
            }
            set
            {
                _EducationGauge = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _LifeGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _MedicalGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _RetirementGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _EstateGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _InvestmentGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _EducationGaugeMax
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double _TotalRetirement
        {
            get
            {
                return RetirementsBindingList.Sum((Retirement x) => x.CurrentAmount); 
            }
            set
            {
               // __TotalRetirement = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalRetirementFuture
        {
            get
            {
                return RetirementsBindingList.Sum((Retirement x) => x.FutureAmount); 
            }
            set
            {
               // __TotalRetirementFuture = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalRetirementPremium
        {
            get
            {
                return RetirementsBindingList.Sum((Retirement x) => x.MonthlyContribution); ;
            }
            set
            {
              //  __TotalRetirementPremium = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalMedical
        {
            get
            {
                return MedicalsBindingList.Sum((Medical x) => x.CurrentAmount); ;
            }
            set
            {
              //  __TotalMedical = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalMedicalPremium
        {
            get
            {
                return MedicalsBindingList.Sum((Medical x) => x.MonthlyContribution); 
            }
            set
            {
               // __TotalMedicalPremium = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalLife
        {
            get
            {
                return LifesBindingList.Sum((Life x) => x.CurrentAmount); 
            }
            set
            {
               // __TotalLife = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalLifeFuture
        {
            get
            {
                return LifesBindingList.Sum((Life x) => x.FutureAmount); ;
            }
            set
            {
               // __TotalLifeFuture = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalLifePremium
        {
            get
            {
                return LifesBindingList.Sum((Life x) => x.MonthlyContribution); ;
            }
            set
            {
               // __TotalLifePremium = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalInv
        {
            get
            {
                return InvestmentsBindingList.Sum((Investment x) => x.CurrentAmount); 
            }
            set
            {
              //  __TotalInv = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalInvFuture
        {
            get
            {
                return InvestmentsBindingList.Sum((Investment x) => x.FutureAmount); 
            }
            set
            {
               // __TotalInvFuture = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalInvPremium
        {
            get
            {
                return InvestmentsBindingList.Sum((Investment x) => x.MonthlyContribution); 
            }
            set
            {
               // __TotalInvPremium = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalEdu
        {
            get
            {
                return EducationsBindingList.Sum((Education x) => x.CurrentAmount); 
            }
            set
            {
              //  __TotalEdu = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalEduFuture
        {
            get
            {
                return EducationsBindingList.Sum((Education x) => x.FutureAmount); 
            }
            set
            {
              //  __TotalEduFuture = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalEduPremium
        {
            get
            {
                return EducationsBindingList.Sum((Education x) => x.MonthlyContribution); 
            }
            set
            {
              //  __TotalEduPremium = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalIncomeAsset
        {
            get
            {
                return IncomeAssetsBindingList.Sum((IncomeAsset x) => x.InitialAmount); 
            }
            set
            {
              //  __TotalIncomeAsset = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalIncomeAssetFuture
        {
            get
            {
                return IncomeAssetsBindingList.Sum((IncomeAsset x) => x.FutureAmount); 
            }
            set
            {
                //__TotalIncomeAssetFuture = value;
            }
        }

        [IgnoreAutoMap]
        public virtual double _TotalIncomeAssetPremium
        {
            get
            {
                return IncomeAssetsBindingList.Sum((IncomeAsset x) => x.MonthlyPayment); 
            }
            set
            {
               // __TotalIncomePremium = value;
            }
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
        public virtual double TotalPremium
        {
            get { return _TotalRetirementPremium + _TotalMedicalPremium + _TotalInvPremium + _TotalEduPremium + _TotalLifePremium + _TotalIncomeAssetPremium; }
            set { }
        }

        [IgnoreAutoMap]
        public virtual double TotalAssets
        {
            get { return _TotalRetirement + _TotalInv + _TotalEdu + _TotalIncomeAsset; }
            set { }
        }
        #endregion

        #region Persisted Properties

        IList<Retirement> _Retirements = new List<Retirement>();
        public virtual IList<Retirement> Retirements
		{
            get { return _Retirements.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Retirements = value; }
		}
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Retirement> RetirementsBindingList { get; set; }

        IList<Investment> _Investments = new List<Investment>();
        public virtual IList<Investment> Investments
		{
            get { return _Investments.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Investments = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Investment> InvestmentsBindingList { get; set; }

        IList<Medical> _Medicals = new List<Medical>();
        public virtual IList<Medical> Medicals
		{
            get { return _Medicals.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Medicals = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Medical> MedicalsBindingList { get; set; }

        IList<Life> _Lifes = new List<Life>();
        public virtual IList<Life> Lifes
		{
            get { return _Lifes.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Lifes = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Life> LifesBindingList { get; set; }

        IList<Education> _Educations = new List<Education>();
        public virtual IList<Education> Educations
		{
            get { return _Educations.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Educations = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Education> EducationsBindingList { get; set; }

        IList<Estate> _Estates = new List<Estate>();
        public virtual IList<Estate> Estates
		{
            get { return _Estates.Where(x => x.Status != "Cancelled").ToList(); }
            set { _Estates = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<Estate> EstatesBindingList { get; set; }

        IList<IncomeAsset> _IncomeAssets = new List<IncomeAsset>();
        public virtual IList<IncomeAsset> IncomeAssets
		{
            get { return _IncomeAssets.Where(x => x.Status != "Cancelled").ToList(); }
            set { _IncomeAssets = value; }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<IncomeAsset> IncomeAssetsBindingList { get; set; }


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
        #endregion

        #region Series
        public virtual Series GetRetirementSeries()
		{
			Series series = new Series("Retirement");
			try
			{
				series.Points.Add(Math.Round(RetirementGauge, 2));
			}
			catch (Exception)
			{
				series.Points.Add(default(double));
			}
			return series;
		}

		public virtual Series GetLifeSeries()
		{
			Series series = new Series("Life");
			try
			{
				series.Points.Add(Math.Round(LifeGauge, 2));
			}
			catch (Exception)
			{
				series.Points.Add(default(double));
			}
			return series;
		}

		public virtual Series GetMedicalSeries()
		{
			Series series = new Series("Medical");
			try
			{
				series.Points.Add(Math.Round(MedicalGauge, 2));
			}
			catch (Exception)
			{
				series.Points.Add(default(double));
			}
			return series;
		}

		public virtual Series GetInvestmentSeries()
		{
			Series series = new Series("Investment");
			try
			{
				series.Points.Add(Math.Round(InvestmentGauge, 2));
			}
			catch (Exception)
			{
				series.Points.Add(default(double));
			}
			return series;
		}

		public virtual Series GetEstateSeries()
		{
			Series series = new Series("Estate");
			try
			{
				series.Points.Add(Math.Round(EstateGauge, 2));
			}
			catch (Exception)
			{
				series.Points.Add(default(double));
			}
			return series;
		}
        #endregion

        public ClientPortfolio()
		{
            IsLoading = true;

			Lifes = new List<Life>();
			Medicals = new List<Medical>();
			Retirements = new ObservableList<Retirement>();
			Estates = new List<Estate>();
			Investments = new List<Investment>();
			Educations = new List<Education>();
			IncomeAssets = new List<IncomeAsset>();
			Periods = 12;

            IsLoading = false;

            Initialise();

        }

        public override void Initialise(bool isLoading = false)
        {
            RetirementsBindingList = new BindingList<Retirement>(Retirements);
            RetirementsBindingList.RaiseListChangedEvents = true;
            RetirementsBindingList.ListChanged += RetirementsBindingList_ListChanged;

            InvestmentsBindingList = new BindingList<Investment>(Investments);
            InvestmentsBindingList.RaiseListChangedEvents = true;
            InvestmentsBindingList.ListChanged += InvestmentsBindingList_ListChanged;

            MedicalsBindingList = new BindingList<Medical>(Medicals);
            MedicalsBindingList.RaiseListChangedEvents = true;
            MedicalsBindingList.ListChanged += MedicalsBindingList_ListChanged;

            LifesBindingList = new BindingList<Life>(Lifes);
            LifesBindingList.RaiseListChangedEvents = true;
            LifesBindingList.ListChanged += LifesBindingList_ListChanged;

            EducationsBindingList = new BindingList<Education>(Educations);
            EducationsBindingList.RaiseListChangedEvents = true;
            EducationsBindingList.ListChanged += EducationsBindingList_ListChanged;

            EstatesBindingList = new BindingList<Estate>(Estates);
            EstatesBindingList.RaiseListChangedEvents = true;
            EstatesBindingList.ListChanged += EstatesBindingList_ListChanged;

            IncomeAssetsBindingList = new BindingList<IncomeAsset>(IncomeAssets);
            IncomeAssetsBindingList.RaiseListChangedEvents = true;
            IncomeAssetsBindingList.ListChanged += IncomeAssetsBindingList_ListChanged;

            base.Initialise(isLoading);

        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);
        }
        private void RetirementsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                RetirementsBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                RetirementsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                RetirementsBindingList[e.NewIndex].Periods = Periods;
                RetirementsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                RetirementsBindingList[e.NewIndex].Initialise();

                _Retirements.Add(RetirementsBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void InvestmentsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                InvestmentsBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                InvestmentsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                InvestmentsBindingList[e.NewIndex].Periods = Periods;
                InvestmentsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                InvestmentsBindingList[e.NewIndex].Initialise();

                _Investments.Add(InvestmentsBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void MedicalsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                MedicalsBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                MedicalsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                MedicalsBindingList[e.NewIndex].Periods = Periods;
                MedicalsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                MedicalsBindingList[e.NewIndex].Initialise();

                _Medicals.Add(MedicalsBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void LifesBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                LifesBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                LifesBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                LifesBindingList[e.NewIndex].Periods = Periods;
                LifesBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                LifesBindingList[e.NewIndex].Initialise();

                _Lifes.Add(LifesBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void EducationsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                EducationsBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                EducationsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                EducationsBindingList[e.NewIndex].Periods = Periods;
                EducationsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                EducationsBindingList[e.NewIndex].Initialise();

                _Educations.Add(EducationsBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void EstatesBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                EstatesBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                EstatesBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                EstatesBindingList[e.NewIndex].Periods = Periods;
                EstatesBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                EstatesBindingList[e.NewIndex].Initialise();

                _Estates.Add(EstatesBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }
        private void IncomeAssetsBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                IsLoading = true;

                IncomeAssetsBindingList[e.NewIndex].InvestmentAge = _RetirementAge;
                IncomeAssetsBindingList[e.NewIndex].CurrentAge = _CurrentAge;
                IncomeAssetsBindingList[e.NewIndex].Periods = Periods;
                IncomeAssetsBindingList[e.NewIndex].InflationPercentage = InflationPercentage;

                IncomeAssetsBindingList[e.NewIndex].Initialise();

                _IncomeAssets.Add(IncomeAssetsBindingList[e.NewIndex]);

                IsLoading = false;
            }

            BindingList_ListChanged(sender, e);
        }

        public override void Calculate()
		{
            if (!InvokeModelCalculating(new EntityEventArgs()))
                return;

            foreach (Life life in Lifes)
			{
                life.Initialise();

				life.Periods = Periods;
				life.InflationPercentage = InflationPercentage;
				if (ServiceProvider != null)
				{
					life.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
					where x.LispName == life.Description
					select x).FirstOrDefault();
				}
				life.Calculate();
			}
			foreach (Medical medical in Medicals)
			{
                medical.Initialise();

                medical.Periods = Periods;
				medical.InflationPercentage = InflationPercentage;
				medical.Calculate();
			}
			foreach (Retirement retirement in Retirements)
			{
                retirement.Initialise();

                retirement.InvestmentAge = _RetirementAge;
				retirement.CurrentAge = _CurrentAge;
				retirement.Periods = Periods;
				retirement.InflationPercentage = InflationPercentage;
				if (ServiceProvider != null)
				{
					retirement.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
					where x.LispName == retirement.Description
					select x).FirstOrDefault();
				}
				retirement.Calculate();
			}
			foreach (Investment investment in Investments)
			{
                investment.Initialise();

                if (investment.InvestmentAge == 0)
				{
					investment.InvestmentAge = _RetirementAge;
				}
				investment.CurrentAge = _CurrentAge;
				investment.Periods = Periods;
				investment.InflationPercentage = InflationPercentage;
				if (ServiceProvider != null)
				{
					investment.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
					where x.LispName == investment.Description
					select x).FirstOrDefault();
				}
				investment.Calculate();
			}
			foreach (Education education in Educations)
			{
                education.Initialise();

				education.Periods = Periods;
				education.InflationPercentage = InflationPercentage;
				if (ServiceProvider != null)
				{
					education.LispProvider = (from x in ServiceProvider.LispProviders.Lisps
					where x.LispName == education.Description
					select x).FirstOrDefault();
				}
				education.Calculate();
			}
			foreach (IncomeAsset incomeAsset in IncomeAssets)
			{
				if (incomeAsset.InvestmentAge == 0)
				{
					incomeAsset.InvestmentAge = _RetirementAge;
				}
				incomeAsset.CurrentAge = _CurrentAge;
				incomeAsset.Periods = Periods;
				incomeAsset.InflationPercentage = InflationPercentage;
				incomeAsset.Calculate();
			}


            InvokeModelCalculated();
		}
	}
}
