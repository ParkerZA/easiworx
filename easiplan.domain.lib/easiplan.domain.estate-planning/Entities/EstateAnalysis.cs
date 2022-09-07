using easiplan.domain.Entities;
using easiplan.domain.estate.MetaEntities;
using my.domain.lib.core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.estate.Entities
{

    public class EstateAnalysis : BaseEntity<int>
    {
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual EstateDefaults EstateDefaults { get; set; } = new EstateDefaults();

        public EstateAnalysis() {
            Client = new Client() { };
            Client.Initialise();
        }

        #region Client
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual Client Client { get; set; }

        [DataMember]
        [IgnoreAutoMap]
        public virtual string ClientName { get { return string.Format("{0}. {1} {2} ({3})", Client.ClientDetails.ClientTitle, Client.ClientDetails.Initials, Client.ClientDetails.LastName, Client.ClientDetails.IdentificationNo); } set { } }

        [IgnoreDataMember]
        public virtual int ClientId { get { return Client.Id; } set { } }
        #endregion

        #region Marital Regime

        private int _MaritalRegime=1;
        public virtual int MaritalRegime
        {
            get
            {
                return _MaritalRegime;
            }
            set
            {
                _MaritalRegime = value;
                InvokePropertyChanged("Calculate");
            }
        }

        private string _MaritalRegimeDesc;
        [IgnoreAutoMap]
        public virtual string MaritalRegimeDesc { get { return _MaritalRegimeDesc; } set { _MaritalRegimeDesc = value; } }

        protected virtual internal double CalculateMaritalRegime(double value)
        {
            if (MaritalRegime != 1 && MaritalRegime != 4)//In COP and Islamic
                value = value / 2;

            return value;
        }
        #endregion

        #region Property Assets
        //Fixed Property Assets
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> PropertyAssets
        {
            get { return Client.ClientAssets.Assets.Where(x => x.Class == "Property" && x.Value>0).ToList(); }
            
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsTotal { get { 
                
                return CalculateMaritalRegime(PropertyAssets.Sum(x => x.Value));
             } 
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsTotalBasic
        {
            get
            {

                return PropertyAssets.Sum(x => x.Value);
            }
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsCostTotal { get {

                return CalculateMaritalRegime(PropertyAssets.Sum(x => x.BaseCost));
            } 
        }

        //Fixed Property Assets to Estate
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> PropertyAssetsToEstate
        {
            get { return PropertyAssets.Where(x => x.BequethTo == "ESTATE").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToEstateTotal { get {

                return CalculateMaritalRegime(PropertyAssetsToEstate.Sum(x => x.Value));
            } 
        
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToEstateCostTotal
        {
            get
            {
                return CalculateMaritalRegime(PropertyAssetsToEstate.Sum(x => x.BaseCost));

            }
        }

        //Fixed Property Assets Bequethed
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> PropertyAssetsBequethed
        {
            get { return PropertyAssets.Where(x => x.BequethTo != "ESTATE").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetBequethedsTotal { get { return CalculateMaritalRegime(PropertyAssetsBequethed.Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsBequethedCostTotal { get { return CalculateMaritalRegime(PropertyAssetsBequethed.Sum(x => x.BaseCost)); } }

        //Fixed Property Assets To Spouse
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> PropertyAssetsToSpouse
        {
            get { return PropertyAssets.Where(x => x.BequethTo == "SPOUSE").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToSpouseTotal { get { return CalculateMaritalRegime(PropertyAssetsToSpouse.Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToSpouseCostTotal { get { return CalculateMaritalRegime(PropertyAssetsToSpouse.Sum(x => x.BaseCost)); } }

        //Fixed Property Assets to Trust
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> PropertyAssetsToTrust
        {
            get { return PropertyAssets.Where(x => x.BequethTo == "TRUST").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToTrustTotal { get { return CalculateMaritalRegime(PropertyAssetsToTrust.Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double PropertyAssetsToTrustCostTotal { get { return CalculateMaritalRegime(PropertyAssetsToTrust.Sum(x => x.BaseCost)); } }

        //UsuFruct Assets
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> Usufructs
        {
            get { return PropertyAssets.Where(x => x.BequethTo == "USUFRUCT").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double UsufructTotal { get { 
                
                return Usufructs.Sum(x => x.Value - x.BaseCost); 
            } 
        }
        [IgnoreAutoMap]
        public virtual double UsufructCostTotal { get { return Usufructs.Sum(x => x.BaseCost); } }

        //Other non-investment assets - Cash, Vehicles, Household Goods
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Asset> OtherAssets
        {
            get { return Client.ClientAssets.Assets.Where(x => x.Class != "Property" && x.Value > 0).ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double OtherAssetsTotal { get { return CalculateMaritalRegime(OtherAssets.Where(x => x.BequethTo == "ESTATE").Sum(x => x.Value)); } }

        #endregion

        #region Deemed Property
        //Retirement/Endowment Policies 
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Retirement> RetirementAssets
        {
            get { return Client.ClientPortfolio.Retirements.Where(x => x.Type != "Property" && x.CurrentAmount>0).ToList(); }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementAssetsTotal
        {
            get
            {
                return CalculateMaritalRegime(RetirementAssets.Sum(x => x.CurrentAmount));
            }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Retirement> RetirementAssetsToEstate
        {
            get { return RetirementAssets.Where(x => x.BequethTo == "ESTATE").ToList(); }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementAssetsToEstateTotal
        {
            get
            {
                return CalculateMaritalRegime(RetirementAssetsToEstate.Sum(x => x.CurrentAmount));
            }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Retirement> RetirementAssetsToSpouse
        {
            get { return RetirementAssets.Where(x => x.BequethTo == "SPOUSE").ToList(); }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementAssetsToSpouseTotal
        {
            get
            {
                return CalculateMaritalRegime(RetirementAssetsToSpouse.Sum(x => x.CurrentAmount));
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Retirement> RetirementAssetsExcluded
        {
            get { return Client.ClientPortfolio.Retirements.Where(x => x.Type == "Property" 
            || x.Type == "Pension/Provident Funds"
            || x.Type == "Pension/Preservation Funds"
            || x.Type == "Provident / Preservation Funds"
            || x.Type == "Living Annuity"            
            || x.Type == "Retirement Annuities").ToList(); }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementAssetsExcludedTotal
        {
            get
            {
                return CalculateMaritalRegime(RetirementAssetsExcluded.Sum(x => x.CurrentAmount));
            }
        }
         [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Retirement> RetirementAssetsIncluded
        {
            get { return Client.ClientPortfolio.Retirements.Where(x => x.Type == "Property"
            && x.Type != "Pension/Provident Funds"
            && x.Type != "Pension/Preservation Funds"
            && x.Type != "Provident / Preservation Funds"
            && x.Type != "Living Annuity"
            && x.Type != "Retirement Annuities").ToList();
            }
        }
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double RetirementAssetsIncludedTotal
        {
            get
            {
                return CalculateMaritalRegime(RetirementAssetsIncluded.Sum(x => x.CurrentAmount));
            }
        }

        //[IgnoreDataMember]
        //[IgnoreAutoMap]
        //public virtual IList<Retirement> DeemedPropertyExcl
        //{
        //    get { return Client.ClientPortfolio.Retirements.Where(x => x.Type == "Property" 
        //    && x.Type == "Pension/Provident Funds" 
        //    && x.Type == "Pension/Preservation" 
        //    && x.Type == "Retirement Annuities").ToList(); }
        //}
        //[IgnoreAutoMap]
        //public virtual double DeemedPropertyExclTotal
        //{
        //    get
        //    {
        //        var total = DeemedPropertyExcl.Sum(x => x.CurrentAmount);

        //        return CalculateMaritalRegime(total);
 
        //    }
        //}
        //[IgnoreAutoMap]
        //public virtual double DeemedPropertyExclToSpouseTotal
        //{
        //    get
        //    {
        //        var total = DeemedPropertyExcl.Where(x => x.BequethTo == "SPOUSE").Sum(x => x.CurrentAmount);

        //        return CalculateMaritalRegime(total);
        //    }
        //}
        //[IgnoreAutoMap]
        //public virtual double DeemedPropertyExclToEstateTotal
        //{
        //    get
        //    {
        //        var total = DeemedPropertyExcl.Where(x => x.BequethTo == "ESTATE").Sum(x => x.CurrentAmount);                

        //        return CalculateMaritalRegime(total);
        //    }
        //}
        //[IgnoreAutoMap]
        //public virtual double DeemedPropertyExclTotal
        //{
        //    get
        //    {
        //        return CalculateMaritalRegime(DeemedPropertyExcl.Sum(x => x.CurrentAmount));
        //    }
        //}
        #endregion

        #region Risk Policies
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Life> LifePolicies
        {
            get { return Client.ClientPortfolio.Lifes; }//.Where(x=>x.Type == "Life Cover" || x.Type == "Keyman" || x.Type=="Buy And Sell").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double LifePoliciesTotal { get { return LifePolicies.Sum(x => x.InitialAmount); } }

        [IgnoreAutoMap]
        public virtual double LifePoliciesToEstateTotal { get { return LifePolicies.Where(x => x.BequethTo == "ESTATE").Sum(x => x.InitialAmount); } }

        [IgnoreAutoMap]
        public virtual double LifePoliciesToSpouseTotal { get { return LifePolicies.Where(x=>x.BequethTo=="SPOUSE").Sum(x => x.InitialAmount); } }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Life> LifePoliciesKeyman
        {
            get { return LifePolicies.Where(x => x.BequethTo == "ESTATE" && x.Type=="Keyman").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double LifePoliciesKeymanTotal { get { return LifePoliciesKeyman.Sum(x => x.InitialAmount); } }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Life> LifePoliciesBuyAndSell
        {
            get { return LifePolicies.Where(x => x.BequethTo == "ESTATE" && x.Type == "Buy And Sell").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double LifePoliciesBuyAndSellTotal { get { return LifePoliciesBuyAndSell.Sum(x => x.InitialAmount); } }

        #endregion

        #region Investment Assets
        //Investments/Shares/Bonds etc
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Investment> InvestmentAssets
        {
            get { return Client.ClientPortfolio.Investments.Where(x => x.Type != "Property" ).ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double InvestmentAssetsTotal { get { return CalculateMaritalRegime(InvestmentAssets.Where(x=>x.BequethTo=="ESTATE").Sum(x => x.CurrentAmount)); } }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Investment> InvestmentAssetsToSpouse
        {
            get { return Client.ClientPortfolio.Investments.Where(x => x.Type != "Property" && x.BequethTo == "SPOUSE").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double InvestmentAssetsToSpouseTotal { get { return CalculateMaritalRegime(InvestmentAssetsToSpouse.Sum(x => x.CurrentAmount)); } }

        //Rental Properties
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<IncomeAsset> IncomeAssets
        {
            get { return Client.ClientPortfolio.IncomeAssets.Where(x => x.Type != "Property" ).ToList(); }//&& x.BequethTo == "ESTATE"
        }
        [IgnoreAutoMap]
        public virtual double IncomeAssetsTotal { get { return CalculateMaritalRegime(IncomeAssets.Sum(x => x.CurrentAmount)); } }

        #endregion

        #region Liabilities

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Liability> Liabilities
        {
            get { return Client.ClientLiabilities.Liabilities.Where(x => x.Value > 0).ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double LiabilitiesTotal { get { return CalculateMaritalRegime(Liabilities.Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double LiabilitiesEstate { get { return CalculateMaritalRegime(Liabilities.Where(x => x.Owner == "ESTATE").Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double LiabilitiesTrust { get { return CalculateMaritalRegime(Liabilities.Where(x => x.Owner == "TRUST").Sum(x => x.Value)); } }
        //Fixed Property
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Liability> PropertyLiabilities
        {
            get { return Client.ClientLiabilities.Liabilities.Where(x => x.Class == "Bond").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double PropertyLiabilitiesEstate { get { return CalculateMaritalRegime(PropertyLiabilities.Where(x => x.Owner == "ESTATE").Sum(x => x.Value)); } }
        [IgnoreAutoMap]
        public virtual double PropertyLiabilitiesTrust { get { return CalculateMaritalRegime(PropertyLiabilities.Where(x => x.Owner == "TRUST").Sum(x => x.Value)); } }
        //[IgnoreAutoMap]
        //public virtual double PropertyLiabilitiesTotal { get { return PropertyLiabilities.Where(x=>x.Owner=="ESTATE").Sum(x => x.Value); } }

        //Loans/Credit Cards/Hire Purchase
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Liability> CreditLiabilities
        {
            get { return Client.ClientLiabilities.Liabilities.Where(x => x.Class != "Bond").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double CreditLiabilitiesTotal { get { return CalculateMaritalRegime(CreditLiabilities.Sum(x => x.Value)); } }

        //Third Party Liabilities TO DO
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Liability> ThirdPartyLiabilities
        {
            get { return Client.ClientLiabilities.Liabilities.Where(x => x.Class != "Bond" && x.Class=="").ToList(); }
        }
        [IgnoreAutoMap]
        public virtual double ThirdPartyLiabilitiesTotal { get { return CalculateMaritalRegime(ThirdPartyLiabilities.Sum(x => x.Value)); } }


        #endregion

        # region Liquidity Analysis

        //Liquid Assets
        IList<LiquidAsset> _liquidAssets = new List<LiquidAsset>();
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<LiquidAsset> LiquidAssets
        {
            get { return _liquidAssets; }

        }

        [IgnoreAutoMap]
        public virtual double LiquidAssetsTotal
        {
            get
            {

                return CalculateMaritalRegime(LiquidAssets.Sum(x => x.Value));
            }
        }

        //Estate Expenses
        IList<EstateExpense> _estateExpense = new List<EstateExpense>();
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<EstateExpense> EstateExpenses
        {
            get { return _estateExpense; }

        }

        [IgnoreAutoMap]
        public virtual double EstateExpensesTotal
        {
            get
            {

                var total = EstateExpenses.Sum(x => x.Value);

                return total;
            }
        }
        #endregion

        #region RiskCover Analysis

        IList<EstateExpense> _riskExpense = new List<EstateExpense>();
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<EstateExpense> RiskExpenses
        {
            get { return _riskExpense; }

        }

        [IgnoreAutoMap]
        public virtual double RiskExpensesTotal
        {
            get
            {

                var total = RiskExpenses.Sum(x => x.Value);

                return total;
            }
        }

        IList<EstateExpense> _riskCover = new List<EstateExpense>();
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<EstateExpense> RiskCover
        {
            get { return _riskCover; }

        }

        [IgnoreAutoMap]
        public virtual double RiskCoverTotal
        {
            get
            {

                var total = RiskCover.Sum(x => x.Value);

                return total;
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<Life> RiskLifeBenefits
        {
            get { return Client.ClientPortfolio.Lifes.Where(x => x.Insured == Client.ClientDetails.FirstName).ToList(); }
        }


        //[IgnoreDataMember]
        //[IgnoreAutoMap]
        public virtual IList<RiskNeed> RiskNeeds
        {
            get; set;
        } = new List<RiskNeed>();

        #endregion

        #region Bequests
        //Bequests Assets
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual IList<Asset> Bequests
        {
            get { return new List<Asset>(); }
        }
        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual double BequestsTotal { get { return Bequests.Sum(x => x.Value); } }

        #endregion

        #region Other Deductions/Donations etc
        //Bequests Assets
        [IgnoreDataMember]
        public virtual IList<OtherDeduction> OtherDeductions
        {
            get;set;
        } = new List<OtherDeduction>();

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual BindingList<OtherDeduction> OtherDeductionsList { get; set; }

        [IgnoreAutoMap]
        public virtual double OtherDeductionsTotal { get { return OtherDeductions.Sum(x => x.Value); } }

        [IgnoreAutoMap]
        public virtual double OtherDeductionsThirdPartyTotal { get { return OtherDeductions.Where(x => x.Type!= "Bequests To Spouse").Sum(x => x.Value); } }
        [IgnoreAutoMap]
        public virtual double OtherDeductionsSpouseTotal { get { return OtherDeductions.Where(x => x.Type == "Bequests To Spouse").Sum(x => x.Value); } }

        #endregion

        #region Fees

        double _executorsPerc;
        public virtual double ExecutorsPerc
        {
            get
            {
                if (_executorsPerc > 0)
                    return _executorsPerc;

                return EstateDefaults.ExecutorPercentage;
            }

            set { _executorsPerc = value; InvokePropertyChanged("Calculate"); }
        }

        double _executorsFees;
        public virtual double ExecutorsFees
        {
            get
            {
                return _executorsFees; ;
                //if (_executorsFees > 0)
                //    return _executorsFees;

                ////return (PropertyAssetsTotal + InvestmentAssetsTotal + RetirementAssetsTotal + OtherAssetsTotal - PropertyAssetsToTrustTotal) * (ExecutorsPerc / 100) * ((100 + VAT) / 100);
                //return (PropertyAssetsTotal + RetirementAssetsTotal -PropertyAssetsToTrustTotal) * (ExecutorsPerc/100) *((100+ VAT) /100);
            }

            set { _executorsFees = value; 
                //InvokePropertyChanged("ExecutorFees"); 
            }
        }

        double _mastersFees;
        public virtual double MastersFees
        {
            get
            {
                return _mastersFees;
            }

            set { _mastersFees = value; InvokePropertyChanged("Calculate"); }
        }

        double _funeralFees;
        public virtual double FuneralFees
        {
            get
            {
                return _funeralFees;
            }

            set { _funeralFees = value; InvokePropertyChanged("Calculate"); }
        }

        #endregion

        #region Taxes
        double _cgtInclRatePerc;
        [IgnoreAutoMap]
        public virtual double CGTInclRatePerc
        {
            get
            {
                if (_cgtInclRatePerc > 0)
                    return _cgtInclRatePerc;

                return EstateDefaults.CgtPercentage;
            }

            set { _cgtInclRatePerc = value; InvokePropertyChanged("Calculate"); }
        }

        double _cgt;
        [IgnoreAutoMap]
        public virtual double CapitalGainsTax
        {
            get
            {
                return _cgt;
            }

            set { _cgt = value;  }
        }

        double _incomeTax;
        [IgnoreAutoMap]
        public virtual double IncomeTax
        {
            get
            {
                return _incomeTax;
            }

            set { _incomeTax = value; }
        }

        double _estateDutyTax;
        [IgnoreAutoMap]
        public virtual double EstateDuty
        {
            get
            {
                return -_estateDutyTax;
            }

            set { _estateDutyTax = value; InvokePropertyChanged("EstateDuty"); }
        }

        string _estateDutyTaxDesc;
        [IgnoreAutoMap]
        public virtual string EstateDutyDesc
        {
            get
            {
                return string.Format("{0}% on first {1}, then {2}%", EstateDefaults.EstateDutyPercentage, EstateDefaults.EstateDutyEx.ToString("R ### ### ##0.00"), EstateDefaults.EstateDutyPercentageEx);
            }

            set {  }
        }

        double _clientsTaxRate=40;
        public virtual double TaxRate
        {
            get
            {
                return _clientsTaxRate;
            }

            set { _clientsTaxRate = value; InvokePropertyChanged("Calculate"); }
        }

        double _VAT;
        [IgnoreAutoMap]
        public virtual double VAT
        {
            get
            {
                if (_VAT > 0)
                    return _VAT;

                return EstateDefaults.VAT;
            }

            set { _VAT = value; InvokePropertyChanged("Calculate"); }
        }
        #endregion

        #region Totals
        /// <summary>
        /// Gross Estate Total - All Assets
        /// </summary>
        [IgnoreAutoMap]
        public virtual double GrossTotal { get; set; }
        /// <summary>
        /// Nett Estate Total - All Assets/Liabilities
        /// </summary>
        [IgnoreAutoMap]
        public virtual double NettTotal { get; set; }

        /// <summary>
        /// Final Estate Total -less Fees
        /// </summary>
        [IgnoreAutoMap]
        public virtual double Total { get; set; }

        //double _LiquidityTotal = 0;
        //[IgnoreAutoMap]
        //public virtual double LiquidityTotal { 
        //    get { return _LiquidityTotal; } 
        //    set {

        //        if (_LiquidityTotal == value)
        //            return;
        //        _LiquidityTotal = value;
        //        InvokePropertyChanged("LiquidityTotal"); 
        //    } 
        //}

        [IgnoreAutoMap]
        public virtual double LiquidityTotal { get; set; }
        #endregion

        #region Summaries

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual string PrintSummary { get; set; } = "Print Summary";

        [IgnoreAutoMap]
        public virtual IList<EstateSummaryItem> CalculationCGTSummary
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual IList<EstateSummaryItem> CalculationExecutorSummary
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual IList<EstateSummaryItem> EstateDutySummary
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual IList<EstateSummaryItem> LiquiditySummary
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual IList<EstateSummaryItem> RiskAnalysisSummary
        {
            get;
            set;
        }
        #endregion

        public override void Initialise(bool isLoading = false)
		{
            OtherDeductionsList = new BindingList<OtherDeduction>(OtherDeductions);
            OtherDeductionsList.RaiseListChangedEvents = true;
            //OtherDeductionsList.ListChanged += BindingList_ListChanged;

            //PropertyAssets ESTATE
            foreach (Asset asset in PropertyAssets)
                if (string.IsNullOrEmpty(asset.BequethTo))
                    asset.BequethTo = "ESTATE";

            foreach (Asset asset in OtherAssets)
                if (string.IsNullOrEmpty(asset.BequethTo))
                    asset.BequethTo = "ESTATE";

            foreach (Investment asset in InvestmentAssets)
                if (asset.Insured == Client.ClientDetails.FirstName)
                    asset.BequethTo = "ESTATE";

            foreach (Retirement asset in RetirementAssets)
                if (string.IsNullOrEmpty(asset.BequethTo))
                    asset.BequethTo = "ESTATE";

            foreach (Liability asset in PropertyLiabilities)
                if (string.IsNullOrEmpty(asset.Owner))
                    asset.Owner = "ESTATE";

            foreach (Life life in LifePolicies)
            {
                if (life.Beneficiaries.Count() > 0)
                {
                    if (life.Beneficiaries.Where(x => x.DependentType.ToUpper() == "SPOUSE").Count() > 0)
                        life.BequethTo = "SPOUSE";
                    else
                        life.BequethTo = "BENEFICIARY";
                }
                else
                {
                    if (life.Insured == Client.ClientDetails.FirstName)
                        life.BequethTo = "ESTATE";
                    else
                        life.BequethTo = "BENEFICIARY";

                }
      
            }

            Calculate();

            base.Initialise(isLoading);
		}

        public override void InvokePropertyChanged(string propertyName)
        {
            if (propertyName == "Calculate")
                Calculate();

            base.InvokePropertyChanged(propertyName);
        }

        public override void Calculate()
        {
            //if (!InvokeModelCalculating())
            //    return;

            double assetsTotal = 0;
            double deductionsTotal = 0;
            double liabilitiesTotal = 0;
            double estateFeesTotal = 0;
            double section4qDeductionsTotal = 0;

            CalculationCGTSummary = new List<EstateSummaryItem>();
            CalculationExecutorSummary = new List<EstateSummaryItem>();

            #region Assets Total
            assetsTotal += PropertyAssetsTotal;
            assetsTotal += OtherAssetsTotal;            
            assetsTotal += InvestmentAssetsTotal;
            assetsTotal += IncomeAssetsTotal;
            assetsTotal += UsufructTotal;
            #endregion

            #region Calculate CGT     
            //CalculationCGTSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            CalculationCGTSummary.Add(new EstateSummaryItem("CAPITAL GAINS TAX", null,null,null).SetFormat(FormatType.Bold));

            //Primary Property Exclusion
            var taxableGains = Math.Max(this.PropertyAssets.Where(x => x.Type == "Non-Income" && x.BequethTo=="ESTATE").Sum(x => x.ProfitLoss) - EstateDefaults.CgtPrimaryPropertyExclusion, 0);

            CalculationCGTSummary.Add(new EstateSummaryItem("Primary Residences ", null, null, taxableGains));
            
            foreach (var item in this.PropertyAssets.Where(x => x.Type == "Non-Income" && x.BequethTo == "ESTATE"))
            {
                CalculationCGTSummary.Add(new EstateSummaryItem("-     " + item.Description, item.Value, item.ProfitLoss, null));
            }
            CalculationCGTSummary.Add(new EstateSummaryItem("-     Primary Exclusion", null, -EstateDefaults.CgtPrimaryPropertyExclusion, null));

            //Other Properties
            var taxableGainsNP = Math.Max(this.PropertyAssets.Where(x => x.Type != "Non-Income" && x.BequethTo == "ESTATE").Sum(x => x.ProfitLoss),0);

            CalculationCGTSummary.Add(new EstateSummaryItem("Non-Primary Residences ", null, null, taxableGainsNP));
            foreach (var item in this.PropertyAssets.Where(x => x.Type != "Non-Income" && x.BequethTo == "ESTATE"))
            {
                CalculationCGTSummary.Add(new EstateSummaryItem("-     " + item.Description, item.Value, item.ProfitLoss, null));
            }

            taxableGains += taxableGainsNP;

            //Other Assets            
            //var otherAssetsCGT = Math.Max(this.OtherAssets.Where(x => x.BequethTo == "ESTATE").Sum(x => x.ProfitLoss),0);
            //CalculationSummary.Add(new EstateSummaryItem("  Other Assets ", null, null, otherAssetsCGT));
            //foreach (var item in this.OtherAssets.Where(x => x.BequethTo == "ESTATE"))
            //{
            //    CalculationSummary.Add(new EstateSummaryItem("      " + item.Description, item.Value, item.ProfitLoss, null));
            //}

            //taxableGains += otherAssetsCGT;

            //Death Exclusion
            taxableGains = Math.Max(taxableGains- EstateDefaults.CgtDeathExclusion, 0);

            CalculationCGTSummary.Add(new EstateSummaryItem("Death Exclusion ", null, null, -EstateDefaults.CgtDeathExclusion));

            CalculationCGTSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));

            CalculationCGTSummary.Add(new EstateSummaryItem("TOTAL Taxable Gains", null, null, taxableGains).SetFormat(FormatType.Bold));
            //Marital Regime
            if (MaritalRegime != 1 && MaritalRegime != 4)//In COP and Islamic
            {
                taxableGains = taxableGains / 2;
                CalculationCGTSummary.Add(new EstateSummaryItem("TOTAL @50% (In COP)", null, null, taxableGains).SetFormat(FormatType.Bold));
            }

            //CGT Inclusion Rate
            CapitalGainsTax = taxableGains * (CGTInclRatePerc / 100);

            CalculationCGTSummary.Add(new EstateSummaryItem("CGT Inclusion Rate @" + CGTInclRatePerc + "%", null, null, CapitalGainsTax).SetFormat(FormatType.Bold));

            //Client's Marginal Tax Rate
            CapitalGainsTax = CapitalGainsTax * (TaxRate/100);

            CalculationCGTSummary.Add(new EstateSummaryItem("CGT @Client's Tax Rate of " + TaxRate + "%", null, null, CapitalGainsTax).SetFormat(FormatType.Bold));

            CalculationCGTSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            #endregion

            #region Executor Fees

            //CalculationExecutorSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            CalculationExecutorSummary.Add(new EstateSummaryItem("EXECUTOR FEES", null, null, null).SetFormat(FormatType.Bold));
            CalculationExecutorSummary.Add(new EstateSummaryItem("Total Fixed Property ", null, null, PropertyAssetsTotalBasic)); 
            foreach(var item in PropertyAssets)
                CalculationExecutorSummary.Add(new EstateSummaryItem("-     " + item.Description, item.Value, null,  null));
            CalculationExecutorSummary.Add(new EstateSummaryItem("less Property to Trust ", null, PropertyAssetsToTrustTotal, -PropertyAssetsToTrustTotal));
            foreach (var item in PropertyAssetsToTrust)
                CalculationExecutorSummary.Add(new EstateSummaryItem("      " + item.Description, item.Value, null, null));
            CalculationExecutorSummary.Add(new EstateSummaryItem("Deemed Property to Estate ", null, null, RetirementAssetsToEstateTotal + LifePoliciesToEstateTotal));
            foreach (var item in RetirementAssetsToEstate)
                CalculationExecutorSummary.Add(new EstateSummaryItem("-     " + item.Description + " - " + item.Type, item.CurrentAmount, null,  null));
            //TO DO Add Risk Policies to Deemed Property
            foreach (var item in LifePolicies.Where(x => x.BequethTo == "ESTATE"))
                CalculationExecutorSummary.Add(new EstateSummaryItem("-     " + item.Description + " - " + item.Type, item.InitialAmount, null, null));
            CalculationExecutorSummary.Add(new EstateSummaryItem("Other Assets ", null, null, OtherAssetsTotal));
            foreach (var item in OtherAssets)
                CalculationExecutorSummary.Add(new EstateSummaryItem("-     " + item.Description + " - " + item.Class, item.Value, null, null));

            CalculationExecutorSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));

            var totalAssets = (PropertyAssetsTotalBasic - PropertyAssetsToTrustTotal) + RetirementAssetsToEstateTotal + LifePoliciesToEstateTotal + OtherAssetsTotal;

            CalculationExecutorSummary.Add(new EstateSummaryItem("TOTAL ASSETS", null, null, totalAssets).SetFormat(FormatType.Bold));
            //if (MaritalRegime != 1 && MaritalRegime != 4)//In COP and Islamic
            //{
            //    totalAssets = totalAssets / 2;
            //    CalculationSummary.Add(new EstateSummaryItem("TOTAL @50% (In COP)", null, null, totalAssets).SetFormat(FormatType.Bold));
            //}
            ExecutorsFees = totalAssets * (ExecutorsPerc / 100);
            CalculationExecutorSummary.Add(new EstateSummaryItem("EXECUTOR FEES @" + ExecutorsPerc + "%", null, null, ExecutorsFees).SetFormat(FormatType.Bold));
            var vat = ExecutorsFees * VAT / 100;
            CalculationExecutorSummary.Add(new EstateSummaryItem("+ VAT @"+ VAT + "%", null, null, vat));
            ExecutorsFees += vat;
            CalculationExecutorSummary.Add(new EstateSummaryItem("TOTAL FEES", null, null, ExecutorsFees).SetFormat(FormatType.Bold));
            #endregion

            #region Calculate Liabilities 

            liabilitiesTotal += PropertyLiabilitiesEstate;
            liabilitiesTotal += PropertyLiabilitiesTrust;      
            liabilitiesTotal += CreditLiabilitiesTotal;
            //ThirdPartyLiabilitiesTotal - TO DO
            liabilitiesTotal += ThirdPartyLiabilitiesTotal;

            #endregion

            #region Section 4q Deductions
            section4qDeductionsTotal += PropertyAssetsToSpouseTotal;
            section4qDeductionsTotal += PropertyAssetsToTrustTotal;
            section4qDeductionsTotal += InvestmentAssetsToSpouseTotal;
            section4qDeductionsTotal += RetirementAssetsToSpouseTotal;
            section4qDeductionsTotal += LifePoliciesToSpouseTotal;
            //section4qDeductionsTotal += LifePoliciesBuyAndSellTotal;
            //section4qDeductionsTotal += LifePoliciesKeymanTotal;
            #endregion

            #region Other Deductions Total

            deductionsTotal += LifePoliciesBuyAndSellTotal;
            deductionsTotal += LifePoliciesKeymanTotal;
            deductionsTotal += UsufructTotal;
            deductionsTotal += OtherDeductionsTotal;
            deductionsTotal += RetirementAssetsExcludedTotal;

            #endregion

            #region Totals
            //Gross Total
            GrossTotal = assetsTotal;//Fixed Asset
            GrossTotal += RetirementAssetsTotal;//Retirement Policies Deemed Property
            GrossTotal += LifePoliciesTotal;//Risk policies

            //Nett Total :assets less liabilities
            NettTotal = GrossTotal - liabilitiesTotal;

            //Capital Gains Tax deductions
            NettTotal -= CapitalGainsTax;

            //Section 4(q) deductions 
            int shariaFraction = Client.ClientDetails.Gender == "Male" ? 8 : 4;//to spouse (female) is 1/8 of Assets else 1/4 to male spouse
            if (MaritalRegime==4)//Islamic Sharia 
                section4qDeductionsTotal += Math.Max(assetsTotal/ shariaFraction, 0);
            NettTotal -= section4qDeductionsTotal;

            //less other deductions
            NettTotal -= deductionsTotal;

            //less fees
            estateFeesTotal += ExecutorsFees;
            estateFeesTotal += MastersFees;
            estateFeesTotal += FuneralFees;

            NettTotal -= estateFeesTotal;

            //less Taxes
            EstateDuty = 0;
            var estateTotal = Math.Max((NettTotal) - EstateDefaults.EstateDutyCeiling,0);
            if (estateTotal > 0)
            {
                var estateTotalEx = Math.Max(estateTotal - EstateDefaults.EstateDutyEx, 0);
                if (estateTotalEx > 0)
                {
                    double firstAmt = estateTotal - estateTotalEx;
                    double secondAmt = estateTotalEx;
                    double estTax = 0;
                    //20% on the first R30M
                    estTax = firstAmt * (EstateDefaults.EstateDutyPercentage / 100); //Apply % discounts

                    //25% on the amount above R30M
                    estTax += secondAmt * (EstateDefaults.EstateDutyPercentageEx / 100); //Apply % discounts

                    EstateDuty = estTax;
                }
                else
                {
                    EstateDuty = Math.Abs(estateTotal) * (EstateDefaults.EstateDutyPercentage / 100); //Apply % discounts
                }
            }

            //Total
            Total = NettTotal;            
            Total += EstateDuty;

            #endregion

            #region Estate Duty Summary
            EstateDutySummary = new List<EstateSummaryItem>();
           
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("Gross Estate", GrossTotal).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("-  Fixed Property ", PropertyAssetsTotal , null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Deemed Property", RetirementAssetsToEstateTotal, null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Risk Policies", LifePoliciesTotal, null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Other Assets", OtherAssetsTotal + IncomeAssetsTotal, null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Investments", InvestmentAssetsTotal, null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Usufructs", UsufructTotal, null, null));

            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("Liabilities", -(LiabilitiesEstate + LiabilitiesTrust)).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("-  Liabilities (Estate) ", null, LiabilitiesEstate, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Liabilities (Trust) ", null, LiabilitiesTrust, null));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("Estate Costs ", null,  null, -estateFeesTotal).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("-  Executor Fees @" + (ExecutorsPerc) + "% + " + VAT +"% VAT", null, ExecutorsFees, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Masters Fees ", null, MastersFees, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Funeral Expenses ", null, FuneralFees, null));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("CGT @Client's Tax Rate of " + TaxRate + "%", null,  null, -CapitalGainsTax).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("Section 4(q) Deductions ", null, null, -(section4qDeductionsTotal)).SetFormat(FormatType.Bold));
            if (MaritalRegime == 4)
                EstateDutySummary.Add(new EstateSummaryItem("-  Assets to Spouse (1/" + shariaFraction + " th to spouse)", null, Math.Max(assetsTotal / 8, 0), null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Policies with Spouse as Beneficiary", null, InvestmentAssetsToSpouseTotal+ RetirementAssetsToSpouseTotal + LifePoliciesToSpouseTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Properties to Spouse", null, PropertyAssetsToSpouseTotal, null));
            // EstateDutySummary.Add(new EstateSummaryItem("-  Properties to Trust", null, PropertyAssetsToTrustTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("Other Deductions ", null, null, -(deductionsTotal)).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("-  Group Benefits", null, null, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Usufructs", null, UsufructTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Buy & Sell Policies", null, LifePoliciesBuyAndSellTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Keyman Policies", null, LifePoliciesKeymanTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Bequests", null, OtherDeductionsTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("-  Pension/Provident Exclusions", null, RetirementAssetsExcludedTotal, null));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));

            EstateDutySummary.Add(new EstateSummaryItem("NETT ESTATE ", null, null, NettTotal).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("-  Less Abatement (Article 4A)", null, null,-EstateDefaults.EstateDutyCeiling));
            EstateDutySummary.Add(new EstateSummaryItem("-  Taxable Estate", null, null, Math.Max(NettTotal - EstateDefaults.EstateDutyCeiling,0)));
            EstateDutySummary.Add(new EstateSummaryItem("ESTATE DUTY @"+ (EstateDefaults.EstateDutyPercentage) + "%" + " and then @" + (EstateDefaults.EstateDutyPercentageEx) + "%", null, null, EstateDuty).SetFormat(FormatType.Bold));
            EstateDutySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            EstateDutySummary.Add(new EstateSummaryItem("NETT ESTATE (After Taxes) ", null, null, Total).SetFormat(FormatType.Bold));
          
            #endregion

            # region Liquidity Analysis

            #region Available Liquidity 
            LiquidAssets.Clear();

            //Life Policies bequeth to Estate
            foreach (var asset in LifePolicies.Where(x => x.BequethTo == "ESTATE" ))//|| x.BequethTo =="SPOUSE"
            {
                LiquidAssets.Add(new LiquidAsset()
                {
                    Type = asset.Type,
                    Description = asset.Description,
                    Value = CalculateMaritalRegime(asset.InitialAmount),
                    ReferenceNo = asset.ReferenceNo
                });
            }

            foreach (var asset in RetirementAssetsIncluded.Where(x => x.BequethTo == "ESTATE"))
            {
                LiquidAssets.Add(new LiquidAsset()
                {
                    Type = asset.Type,
                    Description = asset.Description,
                    Value = CalculateMaritalRegime(asset.CurrentAmount),
                    ReferenceNo = asset.ReferenceNo
                });
            }

            foreach (var asset in InvestmentAssets.Where(x=>x.BequethTo=="ESTATE"))
            {
               LiquidAssets.Add(new LiquidAsset()
                {
                    Type = asset.Type,
                    Description = asset.Description,
                    Value = CalculateMaritalRegime(asset.CurrentAmount),
                    ReferenceNo = asset.ReferenceNo
                });
            }

            foreach (var asset in OtherAssets.Where(x => x.BequethTo == "ESTATE" && x.Class.ToUpper()=="CASH"))
            {
                LiquidAssets.Add(new LiquidAsset()
                {
                    Type = asset.Class,
                    Description = asset.Description,
                    Value = CalculateMaritalRegime(asset.Value),
                    ReferenceNo = ""
                });
            }

            #endregion

            #region Needs & Expenses
            EstateExpenses.Clear();

            foreach (var liability in Liabilities.Where(x => x.Owner == "ESTATE")) //Loans/Credit Cards/Hire Purchase
            {
                EstateExpenses.Add(new EstateExpense()
                {
                    Type = liability.Class,
                    Description = liability.Description,
                    Value = CalculateMaritalRegime(liability.Value)
                    
                });
            }

            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Estate Duty",
                Value = Math.Abs(EstateDuty)

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Executor Fees",
                Value = ExecutorsFees

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Master Fees",
                Value = MastersFees

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Funeral Expenses",
                Value = FuneralFees

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Bequests 3rd Party",
                Value = OtherDeductionsThirdPartyTotal

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Bequest Spouse",
                Value = OtherDeductionsSpouseTotal

            });
            EstateExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "CGT",
                Value = CapitalGainsTax

            });

            LiquidityTotal = LiquidAssetsTotal - EstateExpensesTotal;
            #endregion

            #region Liquidity Analysis Summary
            LiquiditySummary = new List<EstateSummaryItem>();
            //LiquiditySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            LiquiditySummary.Add(new EstateSummaryItem("LIQUIDITY ANALYSIS", null, null, null).SetFormat(FormatType.Bold));
            LiquiditySummary.Add(new EstateSummaryItem("Available Liquidity", LiquidAssetsTotal).SetFormat(FormatType.Bold));

            foreach (var item in LiquidAssets)
            {
                LiquiditySummary.Add(new EstateSummaryItem("-   " + item.Description + " - " + item.Type, item.Value, null, null));
            }

            LiquiditySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));

            LiquiditySummary.Add(new EstateSummaryItem("Liabilities & Expenses", -EstateExpensesTotal).SetFormat(FormatType.Bold));
            foreach (var item in EstateExpenses)
            {
                LiquiditySummary.Add(new EstateSummaryItem("-   " + item.Description + " - " + item.Type, null, item.Value, null));
            }

            LiquiditySummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            LiquiditySummary.Add(new EstateSummaryItem("Liquidity Shortfall / Surplus", null, null, LiquidityTotal).SetFormat(FormatType.Bold));
            #endregion

            #endregion

            #region Risk Cover Analysis

            #region Risk Expenses
            RiskExpenses.Clear();

            foreach (var liability in Liabilities.Where(x => x.Owner == "ESTATE")) //Loans/Credit Cards/Hire Purchase
            {
                RiskExpenses.Add(new EstateExpense()
                {
                    Type = liability.Class,
                    Description = liability.Description,
                    Value = CalculateMaritalRegime(liability.Value)

                });
            }

            RiskExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Estate Duty",
                Value = Math.Abs(EstateDuty)

            });
            RiskExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Executor Fees",
                Value = ExecutorsFees

            });
            RiskExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Master Fees",
                Value = MastersFees

            });
            RiskExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "Funeral Expenses",
                Value = FuneralFees

            });
            //RiskExpenses.Add(new EstateExpense()
            //{
            //    Type = "",
            //    Description = "Bequests 3rd Party",
            //    Value = OtherDeductionsThirdPartyTotal

            //});
            //RiskExpenses.Add(new EstateExpense()
            //{
            //    Type = "",
            //    Description = "Bequest Spouse",
            //    Value = OtherDeductionsSpouseTotal

            //});
            RiskExpenses.Add(new EstateExpense()
            {
                Type = "",
                Description = "CGT",
                Value = CapitalGainsTax

            });

            var RiskExpensesTotal = RiskExpenses.Sum(x => x.Value);
            #endregion

            #region Risk Cover
            RiskCover.Clear();

            foreach (Life life in RiskLifeBenefits)
            {
                foreach (var benefit in life.Benefits)
                {
                    RiskCover.Add(new EstateExpense()
                    {
                        Type = benefit.Type,
                        Description = life.Description,
                        Value = benefit.CoverAmount

                    });
                }
            }

            var currentIncomeProtection = RiskCover.Where(x => x.Type == "Income Protection").Sum(x => x.Value);
            var currentEducationProtection = RiskCover.Where(x => x.Type == "Education").Sum(x => x.Value);
            #endregion

            #region Risk Analysis Summary
            RiskAnalysisSummary = new List<EstateSummaryItem>();

            //RiskAnalysisSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            RiskAnalysisSummary.Add(new EstateSummaryItem("RISK COVER ANALYSIS", null, null, null).SetFormat(FormatType.Bold));
            RiskAnalysisSummary.Add(new EstateSummaryItem("Liabilities & Expenses", -RiskExpensesTotal).SetFormat(FormatType.Bold));
            foreach (var item in RiskExpenses)
            {
                RiskAnalysisSummary.Add(new EstateSummaryItem("-    " + item.Description + " - " + item.Type, null, item.Value, null));
            }

            RiskAnalysisSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));

            var riskCoverNettTotal = RiskCoverTotal;
            RiskAnalysisSummary.Add(new EstateSummaryItem("Existing Risk Cover", null, null, null).SetFormat(FormatType.Bold));
           
            //foreach (var item in RiskCover)
            //{
            //    RiskAnalysisSummary.Add(new EstateSummaryItem("-    " + item.Description + " - " + item.Type, item.Value, null, item.Value-RiskExpensesTotal));
            //}

            //foreach (RiskNeed need in RiskNeeds)
            //{
            //    RiskCover.Add(new EstateExpense()
            //    {
            //        Type = need.Type,
            //        Description = need.Description,
            //        Value = need.RequiredValue

            //    });
            //}

            var totalLife = RiskCover.Where(x => x.Type == "Life" || x.Type =="GLA").Sum(x => x.Value);
            var totalDisability = RiskCover.Where(x => x.Type == "Disability").Sum(x => x.Value);
            var totalDreadedDisease = RiskCover.Where(x => x.Type == "Dreaded Disease").Sum(x => x.Value);
            var totalIncomeProtection = RiskCover.Where(x => x.Type == "Income Protection").Sum(x => x.Value);
            var totalEducationProtection = RiskCover.Where(x => x.Type == "Education").Sum(x => x.Value);
            var totalOtherProtection = RiskCover.Where(x => x.Type == "Other").Sum(x => x.Value);



            //var totalCoverRequired = Math.Abs(Math.Min(totalLife - RiskExpensesTotal, 0))
            //   + Math.Abs(Math.Min(totalDisability - RiskExpensesTotal, 0))
            //   + Math.Abs(Math.Min(totalDreadedDisease - RiskExpensesTotal, 0))
            //   + Math.Abs(Math.Max(totalIncomeProtection - currentIncomeProtection, 0))
            //   + Math.Abs(Math.Max(totalEducationProtection - currentEducationProtection, 0))
            //   + totalOtherProtection;

            //RiskAnalysisSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            //RiskAnalysisSummary.Add(new EstateSummaryItem("Risk Cover Required", null, null, -totalCoverRequired).SetFormat(FormatType.Bold));

            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Life & GLA", totalLife, null, totalLife - RiskExpensesTotal));// Math.Abs(Math.Min(totalLife - RiskExpensesTotal,0))));
            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Disability", totalDisability, null, totalDisability - RiskExpensesTotal));// Math.Abs(Math.Min(totalDisability - RiskExpensesTotal, 0))));
            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Dreaded Disease", totalDreadedDisease, null, totalDreadedDisease - RiskExpensesTotal)); //Math.Abs(Math.Min(totalDreadedDisease - RiskExpensesTotal, 0))));

            //Income Protection
            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Income Protection", totalIncomeProtection, null, totalIncomeProtection - RiskExpensesTotal));// Math.Abs(Math.Min(totalIncomeProtection- RiskExpensesTotal, 0))));
            //Education Protection
            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Children's Education", totalEducationProtection, null, totalEducationProtection - currentEducationProtection));//  Math.Abs(Math.Min(totalEducationProtection-currentEducationProtection, 0))));
            //Other Protection
            RiskAnalysisSummary.Add(new EstateSummaryItem("-   Other", totalOtherProtection,null,null));

            //RiskAnalysisSummary.Add(new EstateSummaryItem("", null).SetFormat(FormatType.Break));
            //RiskAnalysisSummary.Add(new EstateSummaryItem("Risk Cover Shortfall/Surplus", null, null, riskCoverNettTotal - (totalCoverRequired + RiskExpensesTotal)).SetFormat(FormatType.Bold));
            

            #endregion

            #endregion

            base.Calculate();
        }
    }

}