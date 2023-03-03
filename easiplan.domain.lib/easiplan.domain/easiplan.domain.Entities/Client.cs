using FluentValidation;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class Client : BaseEntity<int>
	{
        #region Private Variables
        private string _StatusComments;

		private string _DocumentsFolder;

        #endregion

        #region NonPersisted Properties
        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double AvailableCash
        {
            get
            {
                return ClientIncomes.Total - (ClientExpenses.Total + ClientPortfolio.TotalPremium);
            }
            set
            {
               
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double ClientNettWorth
        {
            get
            {
                return ClientAssets.Total + ClientPortfolio.TotalAssets - ClientLiabilities.Total;
            }
            set
            {
               
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual double CashFlow
        {
            get;
            set;
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<ClientDependent> PolicyOwners
        {
            get
            {
                //Client
                List<ClientDependent> dependents = new List<ClientDependent>();
                dependents.Add(new ClientDependent() { DependentName = ClientDetails.FirstName });

                //Spouse
                if (SpouseDetails!=null)
                    dependents.Add(new ClientDependent() { DependentName = SpouseDetails.FirstName });

                //Dependents
                foreach (var c in ClientDependents.Dependents)
                    dependents.Add(c);

                return dependents;
            }
            set
            {
                
            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<ClientDependent> EstateOwners
        {
            get
            {
                List<ClientDependent> dependents = new List<ClientDependent>();

                dependents.Add(new ClientDependent() { DependentName = "ESTATE" });
                dependents.Add(new ClientDependent() { DependentName = "SPOUSE" });
                dependents.Add(new ClientDependent() { DependentName = "BENEFICIARY" });
                dependents.Add(new ClientDependent() { DependentName = "TRUST" });
                dependents.Add(new ClientDependent() { DependentName = "USUFRUCT" });

                return dependents;
            }
            set
            {

            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual IList<ClientDependent> Beneficiaries
        {
            get
            {
                List<ClientDependent> dependents = new List<ClientDependent>();
               
                //Spouse
                if (SpouseDetails != null)
                    dependents.Add(new ClientDependent() { DependentName = SpouseDetails.FirstName, BirthDate = SpouseDetails.DateOfBirth,DependentType="Spouse" });

                //Dependents
                foreach (var c in ClientDependents.Dependents)
                    dependents.Add(c);

                return dependents;
            }
            set
            {

            }
        }

        [IgnoreDataMember]
        [IgnoreAutoMap]//YJ : Changed to not save as part of the Client Aggregate
        public virtual ClientInstructions ClientInstructions
        {
            get;
            set;
        }

       

        #endregion

        #region Persisted Properties

        [NHMaxLengthAttr(4000)]
		public virtual string StatusComments
		{
			get
			{
				return _StatusComments;
			}
			set
			{
                if (_StatusComments == value) return;

                _StatusComments = value;

				InvokePropertyChanged("StatusComments");
			}
		}

		[NHMaxLengthAttr(2000)]
		[IgnoreDataMember]
		public virtual string DocumentsFolder
		{
			get
			{
				return _DocumentsFolder;
			}
			set
			{
                if (_DocumentsFolder == value) return;

                _DocumentsFolder = value;
				InvokePropertyChanged("DocumentsFolder");
			}
		}

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual int AgentId { get; set; }

        public virtual User AgentDetails
		{
			get;
			set;
		}

      
		public virtual ClientDetails ClientDetails
		{
			get;
			set;
		}

		public virtual ClientContacts ClientContacts
		{
			get;
			set;
		}

		public virtual ClientAdditionalInfo ClientAdditionalInfo
		{
			get;
			set;
		}

		public virtual AddressDetail PhysicalAddress
		{
			get;
			set;
		}

		public virtual AddressDetail PostalAddress
		{
			get;
			set;
		}

		public virtual ClientDetails SpouseDetails
		{
			get;
			set;
		}

		public virtual ClientContacts SpouseContacts
		{
			get;
			set;
		}

		public virtual ClientDependents ClientDependents
		{
			get;
			set;
		}

		public virtual BankDetail BankDetails
		{
			get;
			set;
		}

		public virtual ClientAssets ClientAssets
		{
			get;
			set;
		}

		public virtual ClientLiabilities ClientLiabilities
		{
			get;
			set;
		}

		public virtual ClientIncomes ClientIncomes
		{
			get;
			set;
		}

		public virtual ClientExpenses ClientExpenses
		{
			get;
			set;
		}

		public virtual ClientFna ClientFna
		{
			get;
			set;
		}

		public virtual ClientPortfolio ClientPortfolio
		{
			get;
			set;
		}

		public virtual ClientFnaEducation ClientFnaEducation
		{
			get;
			set;
		}

		public virtual ClientFnaInvestment ClientFnaInvestment
		{
			get;
			set;
		}

        public virtual ClientChecklist ClientChecklist
		{
			get;
			set;
		}

		public virtual ClientFees ClientFees
		{
			get;
			set;
		}

        #endregion

		public Client()
		{
            IsLoading = true;

			Status = "New";

            IsLoading = false;
		}

		public override void Initialise(bool isLoading = false)
		{
            this.PropertyChanged -= _PropertyChanged;
            this.PropertyChanged += _PropertyChanged;

            if (AgentDetails != null)
                AgentId = AgentDetails.Id;

            if (ClientDetails == null)
				ClientDetails = new ClientDetails();
            ClientDetails.PropertyChanged -= ClientDetails_PropertyChanged;
            ClientDetails.PropertyChanged += ClientDetails_PropertyChanged;
            //ClientDetails.Initialise(true);            

            if (SpouseDetails == null)
				SpouseDetails = new ClientDetails();
            SpouseDetails.PropertyChanged -= SpouseDetails_PropertyChanged;
            SpouseDetails.PropertyChanged += SpouseDetails_PropertyChanged;
            //SpouseDetails.Initialise(true);

            if (ClientDependents == null)
				ClientDependents = new ClientDependents();
            ClientDependents.PropertyChanged -= ClientDependents_PropertyChanged;
            ClientDependents.PropertyChanged += ClientDependents_PropertyChanged;
            //ClientDependents.Initialise();

            if (PhysicalAddress == null)
				PhysicalAddress = new AddressDetail();
            PhysicalAddress.PropertyChanged -= _PropertyChanged;
            PhysicalAddress.PropertyChanged += _PropertyChanged;
            //PhysicalAddress.Initialise(true);

            if (PostalAddress == null)
				PostalAddress = new AddressDetail();
            PostalAddress.PropertyChanged -= _PropertyChanged;
            PostalAddress.PropertyChanged += _PropertyChanged;
            //PostalAddress.Initialise(true);

            if (ClientContacts == null)
				ClientContacts = new ClientContacts();
            ClientContacts.PropertyChanged -= _PropertyChanged;
            ClientContacts.PropertyChanged += _PropertyChanged;
            //ClientContacts.Initialise(true);

            if (SpouseContacts == null)
				SpouseContacts = new ClientContacts();
            SpouseContacts.PropertyChanged -= _PropertyChanged;
            SpouseContacts.PropertyChanged += _PropertyChanged;
            //SpouseContacts.Initialise(true);

            if (BankDetails == null)
				BankDetails = new BankDetail();
            BankDetails.PropertyChanged -= _PropertyChanged;
            BankDetails.PropertyChanged += _PropertyChanged;
            //BankDetails.Initialise(true);

            if (ClientAssets == null)
				ClientAssets = new ClientAssets();
            ClientAssets.PropertyChanged -= ClientAssets_PropertyChanged;
            ClientAssets.PropertyChanged += ClientAssets_PropertyChanged;
            //ClientAssets.Initialise();
            
            if (ClientLiabilities == null)
				ClientLiabilities = new ClientLiabilities();
            ClientLiabilities.PropertyChanged -= ClientLiabilities_PropertyChanged;
            ClientLiabilities.PropertyChanged += ClientLiabilities_PropertyChanged;
            //ClientLiabilities.Initialise();

            if (ClientIncomes == null)
				ClientIncomes = new ClientIncomes();
            ClientIncomes.PropertyChanged -= ClientIncomes_PropertyChanged;
            ClientIncomes.PropertyChanged += ClientIncomes_PropertyChanged;
            //ClientIncomes.Initialise();

            if (ClientExpenses == null)
				ClientExpenses = new ClientExpenses();
            ClientExpenses.PropertyChanged -= ClientExpenses_PropertyChanged;
            ClientExpenses.PropertyChanged += ClientExpenses_PropertyChanged;
            //ClientExpenses.Initialise();

            if (ClientFees == null)
				ClientFees = new ClientFees();
            ClientFees.PropertyChanged -= ClientFees_PropertyChanged;
            ClientFees.PropertyChanged += ClientFees_PropertyChanged;
            //ClientFees.Initialise();

            if (ClientFna == null)
				ClientFna = new ClientFna();
            ClientFna.PropertyChanged -= ClientFna_PropertyChanged;
            ClientFna.PropertyChanged += ClientFna_PropertyChanged;
            if (ClientFna.Client == null)
                ClientFna.Client = this;

            //ClientFna.Initialise();

            if (ClientInstructions == null)
                ClientInstructions = new ClientInstructions();
            ClientInstructions.PropertyChanged += ClientInstructions_PropertyChanged;
            //ClientInstructions.Initialise();

            if (ClientFnaEducation == null)
				ClientFnaEducation = new ClientFnaEducation();
            ClientFnaEducation.PropertyChanged -= ClientFnaEducation_PropertyChanged;
            ClientFnaEducation.PropertyChanged += ClientFnaEducation_PropertyChanged;
            //ClientFnaEducation.Initialise();

            if (ClientFnaInvestment == null)
				ClientFnaInvestment = new ClientFnaInvestment();
            ClientFnaInvestment.PropertyChanged -= ClientFnaInvestment_PropertyChanged;
            ClientFnaInvestment.PropertyChanged += ClientFnaInvestment_PropertyChanged;
            //ClientFnaInvestment.Initialise();

            if (ClientChecklist == null)
				ClientChecklist = new ClientChecklist();
            ClientChecklist.PropertyChanged -= _PropertyChanged;
            ClientChecklist.PropertyChanged += _PropertyChanged;
            //ClientChecklist.Initialise(true);

            if (ClientPortfolio == null)
				ClientPortfolio = new ClientPortfolio();
            ClientPortfolio.PropertyChanged -= ClientPortfolio_PropertyChanged;
            ClientPortfolio.PropertyChanged += ClientPortfolio_PropertyChanged;
            //ClientPortfolio.Initialise();

            if (ClientAdditionalInfo == null)
				ClientAdditionalInfo = new ClientAdditionalInfo();
            ClientAdditionalInfo.PropertyChanged -= _PropertyChanged;
            ClientAdditionalInfo.PropertyChanged += _PropertyChanged;
            //ClientAdditionalInfo.Initialise();

            IsLoading = true;

            ClientDetails.ClientId = Id;
            SpouseDetails.ClientId = 0;
            ClientContacts.ClientId = Id;
            ClientContacts.ClientDetailsId = ClientDetails.Id;
            SpouseContacts.ClientId = Id;
            SpouseContacts.ClientDetailsId = SpouseDetails.Id;
            ClientFees.ClientId = Id;
            ClientInstructions.ClientId = Id;
            ClientChecklist.ClientId = Id;

            ClientPortfolio._CurrentAge = ClientDetails.Age;
            ClientPortfolio._RetirementAge = ClientFna.RetirementAge;
            ClientPortfolio._LifeExpectancy = ClientFna.LifeExpectancy;
            ClientPortfolio.InflationPercentage = ClientFna.InflationPercentage;

            ClientFna._CurrentAge = ClientDetails.Age;
            ClientFna.AvailableCash = AvailableCash;

            ClientFnaInvestment._CurrentAge = ClientDetails.Age;
            ClientFnaEducation._CurrentAge = ClientDetails.Age;

            base.Initialise(isLoading);
        }

        #region Property Changed EventHandlers
        private void _PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            try
            {
                if (!string.IsNullOrEmpty(e.PropertyName) && !IsLoading && !IsCalculating)
                {
                    this.InvokePropertyChanged(e.PropertyName);

                    SetModified(true);
                }

            }
            catch (Exception x) { }
        }

        private void ClientAdditionalInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientPortfolio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientFna_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ClientPortfolio.IsLoading = true;
            ClientPortfolio._RetirementAge = ClientFna.RetirementAge;
            ClientPortfolio._LifeExpectancy = ClientFna.LifeExpectancy;
            ClientPortfolio.IsLoading = false;

            _PropertyChanged(sender, e);
        }

        private void ClientFnaInvestment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientFnaEducation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientInstructions_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientExpenses_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ClientFna.AvailableCash = AvailableCash;

            _PropertyChanged(sender, e);
        }

        private void ClientIncomes_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ClientFna.AvailableCash = AvailableCash;

            _PropertyChanged(sender, e);

        }

        private void ClientLiabilities_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientAssets_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                //AutoFill BankAccount Name
                if (string.IsNullOrEmpty(BankDetails.AcctName))
                {
                    if (!string.IsNullOrEmpty(ClientDetails.ClientTitle) && !string.IsNullOrEmpty(ClientDetails.Initials) && !string.IsNullOrEmpty(ClientDetails.LastName))
                        BankDetails.AcctName = $"{ClientDetails.ClientTitle} {ClientDetails.Initials} {ClientDetails.LastName}";
                }

                ClientPortfolio._CurrentAge = ClientDetails.Age;
                ClientPortfolio._RetirementAge = ClientFna.RetirementAge;
                ClientPortfolio._LifeExpectancy = ClientFna.LifeExpectancy;

                ClientFna._CurrentAge = ClientDetails.Age;

                ClientFnaInvestment._CurrentAge = ClientDetails.Age;
                
                _PropertyChanged(sender, e);

            }
            catch(Exception x) { };

        }

        private void SpouseDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);

        }

        private void ClientDependents_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        private void ClientFees_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _PropertyChanged(sender, e);
        }

        #endregion

        public override void Validate(string FieldName = null)
		{
			if (FieldName == null)
			{
				if (string.IsNullOrEmpty(ClientDetails.FirstName))
				{
					throw new ValidationException($"{ClientDetails.Name} :First Name is required.");
				}
				ClientDetails.Validate("FirstName");
				if (string.IsNullOrEmpty(ClientDetails.LastName))
				{
					throw new ValidationException($"{ClientDetails.LastName} :Surname is required.");
				}
				ClientDetails.Validate("LastName");
				if (ClientDetails.DateOfBirth <= MinDateTime)
				{
					throw new ValidationException($"{ClientDetails.DateOfBirth} :Date Of Birth is required.");
				}
				ClientDetails.Validate("DateOfBirth");
				if (string.IsNullOrEmpty(ClientDetails.IdentificationNo))
				{
					if (string.IsNullOrEmpty(ClientDetails.PassportNo))
					{
						throw new ValidationException($"{ClientDetails.Name} :Id or Passport Number is required.");
					}
					if (string.IsNullOrEmpty(ClientDetails.Nationality))
					{
						throw new ValidationException($"{ClientDetails.Name} :Nationality is required.");
					}
				}
				else
				{
					ClientDetails.Validate("IdentificationNo");
				}
				ClientDetails.Validate("TaxNumber");
			}
			base.Validate(FieldName);
		}

		public override void Calculate()
		{
            if (!InvokeModelCalculating(new EntityEventArgs()))
                return;

            ClientDetails.ClientId = Id;
            SpouseDetails.ClientId = 0;
            ClientContacts.ClientId = Id;
            ClientContacts.ClientDetailsId = ClientDetails.Id;
            SpouseContacts.ClientId = Id;
            SpouseContacts.ClientDetailsId = SpouseDetails.Id;
            ClientFees.ClientId = Id;
            ClientInstructions.ClientId = Id;
            ClientChecklist.ClientId = Id;


            try
            {
                ///if (string.IsNullOrEmpty(BankDetails.AcctName))
                ///{
                    if (!string.IsNullOrEmpty(ClientDetails.ClientTitle) && !string.IsNullOrEmpty(ClientDetails.Initials) && !string.IsNullOrEmpty(ClientDetails.LastName))
                        BankDetails.AcctName = $"{ClientDetails.ClientTitle} {ClientDetails.Initials} {ClientDetails.LastName}";
                //}

                ClientPortfolio._CurrentAge = ClientDetails.Age;
                ClientPortfolio._RetirementAge = ClientFna.RetirementAge;
                ClientPortfolio._LifeExpectancy = ClientFna.LifeExpectancy;

                ClientFna._CurrentAge = ClientDetails.Age;

                ClientFnaInvestment._CurrentAge = ClientDetails.Age;
                ClientFnaEducation._CurrentAge = ClientDetails.Age;

            }
            catch (Exception x) { };
   

            InvokeModelCalculated(new EntityEventArgs());

        }

        public virtual void UpdateRetirementFNA()
        {
            //Update Client FNA
            ClientFna._CurrentAge = ClientDetails.Age;
            ClientFna.AvailableCash = ClientIncomes.Total - (ClientExpenses.Total + ClientPortfolio.TotalPremium);
            ClientFna.Calculate();

            //Update Client Portfolio
            ClientPortfolio.InflationPercentage = ClientFna.InflationPercentage;
            ClientPortfolio.Calculate();

            //Populate Haves in FNA
            ClientFna.Haves.Clear();
            foreach (Retirement retirement in ClientPortfolio.RetirementsBindingList)
            {
                ClientFna.HavesBindingList.Add(new Have
                {
                    Description = retirement.Description,
                    CurrentAmount = retirement.CurrentAmount,
                    FutureAmount = retirement.FutureAmount,
                    GrowthPercentage = retirement.GrowthPercentage,
                    EscalationPercentage = retirement.EscalationPercentage,
                    InitialAmount = retirement.InitialAmount,
                    InvestmentAge = retirement.InvestmentAge,
                    InvestmentYears = retirement.InvestmentYears,
                    MonthlyContribution = retirement.MonthlyContribution,
                    Status = retirement.Status,
                    Type = retirement.Type
                });
            }
            foreach (IncomeAsset incomeAsset in ClientPortfolio.IncomeAssetsBindingList)
            {
                ClientFna.HavesBindingList.Add(new Have
                {
                    Description = incomeAsset.Description,
                    CurrentAmount = incomeAsset.InitialAmount,
                    FutureAmount = incomeAsset.FutureAmount,
                    GrowthPercentage = incomeAsset.GrowthPercentage,
                    EscalationPercentage = incomeAsset.EscalationPercentage,
                    InvestmentAge = incomeAsset.InvestmentAge,
                    InvestmentYears = incomeAsset.InvestmentYears,
                    Status = incomeAsset.Status,
                    Type = incomeAsset.Type
                });
            }
        }

    }
}
