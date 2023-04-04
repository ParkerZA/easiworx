using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientDetails : BaseEntity<int>
	{
        #region Private Variables
        private string _Lastname="";

		private string _ClientTitle="";

		private string _MidName="";

		private string _FirstName="";

		private string _Initials="";

		private string _IdentificationNo="";

		private string _PassportNo="";

		private string _Nationality="";

		private string _BirthPlace="";

		private string _Gender="";

		private string _MaritalStatus="";

		private string _TaxNumber="";

		private DateTime _DateOfBirth;

		private string _Occupation;

		private string _Language;

		private double _FValue;

		private double _PValue;

		private double _Delegator;

		private double _LikeMinded;

		private double _Influence;
        #endregion

        #region NonPersisted Properties

        [IgnoreAutoMap]
        public virtual string Fullname
        {
            get { return $"{LastName}, {ClientTitle} {FirstName} {MidName}"; }
        }

        [IgnoreAutoMap]
        public virtual int Age
        {
            get
            {
                if (_DateOfBirth > MinDateTime)
                {
                    return (int)((double)DateTime.Now.Subtract(_DateOfBirth).Days / 365.242199);
                }
                return 0;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual string _AgeDescription
        {
            get { return $"{Age}{getAgeSuffix(Age)}"; }
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual string RecipientAddress
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual string RecipientCell
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual int CommunicationStatus
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual string CommunicationMessage
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        [IgnoreDataMember]
        public virtual bool IsSelected
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual double TotalWeighting
        {
            get;
            set;
        }

        [IgnoreAutoMap]
        public virtual string Rating
        {
            get {
				this.Status = GetRating();
				return this.Status; }
            set { }
        }

        #endregion

        #region Persisted Properties
        public virtual int ClientId
		{
			get;
			set;
		}

		public virtual string LastName
		{
			get
			{
				return _Lastname.InitCaps();
			}
			set
			{
                if (_Lastname == value) return;

				_Lastname = value.InitCaps();

				InvokePropertyChanged("LastName");
			}
		}

		public virtual string ClientTitle
		{
			get
			{
				return _ClientTitle;
			}
			set
			{
                if (_ClientTitle == value) return;

                _ClientTitle = value;
				InvokePropertyChanged("ClientTitle");
			}
		}

		public virtual string MidName
		{
			get
			{
				return _MidName;
			}
			set
			{
                if (_MidName == value) return;

                _MidName = value;
				InvokePropertyChanged("MidName");
			}
		}

		public virtual string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
                if (_FirstName == value) return;

                _FirstName = value;
				InvokePropertyChanged("FirstName");
			}
		}

		public virtual string Initials
		{
			get
            {
				var _initials = "";
                try
                {
                    _initials = FirstName.Substring(0, 1).ToUpper();
                    _initials += MidName.Substring(0, 1).ToUpper();
                }
                catch (Exception x) { };
                return _initials;
            }
			set
			{

    //            if (_Initials == value) return;

    //            _Initials = value;
				//InvokePropertyChanged("Initials");
			}
		}

        //[Encrypt]
		public virtual string IdentificationNo
		{
			get
			{
				return _IdentificationNo;
			}
			set
			{
                if (_IdentificationNo == value) return;

                InvokePropertyChanging("IdentificationNo");

				_IdentificationNo = value;

				Calculate();

				InvokePropertyChanged("IdentificationNo");
			}
		}

		//[Encrypt]
		public virtual string PassportNo
		{
			get
			{
				return _PassportNo;
			}
			set
			{
                if (_PassportNo == value) return;

                _PassportNo = value;
				InvokePropertyChanged("PassportNo");
			}
		}

		public virtual string Nationality
		{
			get
			{
				return _Nationality;
			}
			set
			{
                if (_Nationality == value) return;

                _Nationality = value;
				InvokePropertyChanged("Nationality");
			}
		}

		public virtual string BirthPlace
		{
			get
			{
				return _BirthPlace;
			}
			set
			{
                if (_BirthPlace == value) return;

                _BirthPlace = value;
				InvokePropertyChanged("BirthPlace");
			}
		}

		public virtual string Gender
		{
			get
			{
				return _Gender;
			}
			set
			{
                if (_Gender == value) return;

                _Gender = value;
				InvokePropertyChanged("Gender");
			}
		}

		public virtual string MaritalStatus
		{
			get
			{
				return _MaritalStatus;
			}
			set
			{

                if (_MaritalStatus == value) return;

                _MaritalStatus = value;
				InvokePropertyChanged("MaritalStatus");
			}
		}

		[Encrypt]
		public virtual string TaxNumber
		{
			get
			{
				return _TaxNumber;
			}
			set
			{

                if (_TaxNumber == value) return;

                _TaxNumber = value;
				InvokePropertyChanged("TaxNumber");
			}
		}

		[IgnoreAutoMap]
		public virtual byte[] ClientImage
		{
			get;
			set;
		}

		public virtual DateTime DateOfBirth
		{
			get
			{
				return _DateOfBirth;
			}
			set
			{
                if (_DateOfBirth == value) return;

                _DateOfBirth = value;

				Calculate();

				InvokePropertyChanged("DateOfBirth");
			}
		}

		public virtual string Occupation
		{
			get
			{
				return _Occupation;
			}
			set
			{

                if (_Occupation == value) return;

                _Occupation = value;
				InvokePropertyChanged("Occupation");
			}
		}

		public virtual string Language
		{
			get
			{
				return _Language;
			}
			set
			{
                if (_Language == value) return;

                _Language = value;
				InvokePropertyChanged("Language");
			}
		}

		public virtual double FValue
		{
			get
			{
				return _FValue;
			}
			set
			{
                if (_FValue == value) return;

                _FValue = value;
				InvokePropertyChanged("FValue");
			}
		}

		public virtual double PValue
		{
			get
			{
				return _PValue;
			}
			set
			{
                if (_PValue == value) return;

                _PValue = value;
				InvokePropertyChanged("PValue");
			}
		}

		public virtual double Delegator
		{
			get
			{
				return _Delegator;
			}
			set
			{
                if (_Delegator == value) return;

                _Delegator = value;
				InvokePropertyChanged("Delegator");
			}
		}

		public virtual double LikeMinded
		{
			get
			{
				return _LikeMinded;
			}
			set
			{
                if (_LikeMinded == value) return;

                _LikeMinded = value;
				InvokePropertyChanged("LikeMinded");
			}
		}

		public virtual double Influence
		{
			get
			{
				return _Influence;
			}
			set
			{

                if (_Influence == value) return;

                _Influence = value;
				InvokePropertyChanged("Influence");
			}
		}


        #endregion

        public ClientDetails()
		{
			
		}

        public override void Initialise(bool isLoading = false)
        {

            base.Initialise(isLoading);
        }

        public override void Validate(string PropertyName = null)
		{

            if (PropertyName.ToLower() == "identificationno" && !string.IsNullOrEmpty(IdentificationNo))
			{
				SaIdValidator validator = new SaIdValidator();
				if (validator.Validate(IdentificationNo))
				{
                    //DateOfBirth = validator.DateOfBirth;
                    //Gender = validator.Gender;
                    //Nationality = validator.Nationality;

                    
                }
                else { throw new MyValidationException(string.Format("Id Number is not valid.")); }
			}
			if (PropertyName.ToLower() == "taxnumber" && !string.IsNullOrEmpty(TaxNumber) && !F.IsSATaxNumber(TaxNumber))
			{
				throw new MyValidationException($"{Name} :Tax Number is not valid.");
			}
			if (PropertyName.ToLower() == "firstname" && !string.IsNullOrEmpty(FirstName) && !FirstName.IsCharactersOnly("-'"))
			{
				throw new MyValidationException($"{Name} :First name is not valid.");
			}
			if (PropertyName.ToLower() == "lastname" && !string.IsNullOrEmpty(LastName) && !LastName.IsCharactersOnly("-'"))
			{
				throw new MyValidationException($"{Name} :Surname is not valid.");
			}
			if (PropertyName.ToLower() == "dateofbirth" && _DateOfBirth < MinDateTime)
			{
				throw new MyValidationException($"{Name} :Date Of Birth is not valid.");
			}

            base.Validate(PropertyName);

        }

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			//Calculate DateOfBirth
			if (!string.IsNullOrEmpty(IdentificationNo))
			{
				SaIdValidator validator = new SaIdValidator();
				if (validator.Validate(IdentificationNo))
				{
					DateOfBirth = validator.DateOfBirth;
					Gender = validator.Gender;
					Nationality = validator.Nationality;
				}
			}

			

			InvokeModelCalculated();
		}

		public virtual string getAgeSuffix(int Age)
		{
			switch (Age)
			{
			case 1:
			case 21:
			case 31:
			case 41:
			case 51:
			case 61:
			case 71:
			case 81:
			case 91:
			case 101:
				return "st";
			case 2:
			case 22:
			case 32:
			case 42:
			case 52:
			case 62:
			case 72:
			case 82:
			case 92:
			case 102:
				return "nd";
			case 3:
			case 23:
			case 33:
			case 43:
			case 53:
			case 63:
			case 73:
			case 83:
			case 93:
			case 103:
				return "rd";
			default:
				return "th";
			}
		}

        public virtual string GetRating()
        {
            string _rating = "";

            // Rating Calculation
            double FValueWeighted = FValue * 0.2;
            double PValueWeighted = PValue * 0.3;
            double DelegatorWeighted = Delegator * 0.1;
            double LikeMindedWeighted = LikeMinded * 0.1;
            double InfluenceWeighted = Influence * 0.3;
            TotalWeighting = FValueWeighted + PValueWeighted + DelegatorWeighted + LikeMindedWeighted + InfluenceWeighted;
            TotalWeighting = Math.Round(TotalWeighting, 3);
            if (TotalWeighting >= 2.5)
            {
                _rating = "A";
            }
            if (TotalWeighting >= 1.5 && TotalWeighting < 2.5)
            {
                _rating = "B";
            }
            if (TotalWeighting >= 0.0 && TotalWeighting < 1.5)
            {
                _rating = "C";
            }
            if (TotalWeighting <= 0.0)
            {
                _rating = "";
            }

            return _rating;
        }
	}
}
