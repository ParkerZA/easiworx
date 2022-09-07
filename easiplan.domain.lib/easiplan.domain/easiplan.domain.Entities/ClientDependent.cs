using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;

namespace easiplan.domain.Entities
{
	public class ClientDependent : BaseEntity<int>
	{
		private string _DependentName;

		private string _DependentType;

		private DateTime _BirthDate;

		private string _IdNumber;

		private double _Percentage;

		private bool _MedAid;

		public virtual string DependentName
		{
			get
			{
				return _DependentName;
			}
			set
			{
				if (_DependentName == value)
					return;

				_DependentName = value;
				InvokePropertyChanged("DependentName");
			}
		}

		public virtual string DependentType
		{
			get
			{
				return _DependentType;
			}
			set
			{
				if (_DependentType == value)
					return;

				_DependentType = value;
				InvokePropertyChanged("DependentType");
			}
		}

		public virtual DateTime BirthDate
		{
			get
			{
				return DateTime.Parse(_BirthDate.ToShortDateString()) ;
				
			}
			set
			{
				if (_BirthDate == value)
					return;

				_BirthDate = value;

				InvokePropertyChanged("BirthDate");
			}
		}

		[IgnoreAutoMap]
		public virtual int _Age
		{
			get {
				DateTime dateTime = DateTime.MinValue;
				if (dateTime.CompareTo(BirthDate) >= 0)
				{
					BirthDate = DateTime.Now;
				}
				if (BirthDate > DateTime.MinValue)
				{
					dateTime = DateTime.Now;
					return dateTime.Subtract(BirthDate).Days / 365;
				};
				return 0;
			}
			set { }
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		[Encrypt]
		public virtual string IdNumber
		{
			get
			{
				return _IdNumber;
			}
			set
			{
				if (_IdNumber == value)
					return;

				_IdNumber = value;
				InvokePropertyChanged("IdNumber");
			}
		}

		public virtual double Percentage
		{
			get
			{
				return _Percentage;
			}
			set
			{
				if (_Percentage == value)
					return;

				_Percentage = value;
				InvokePropertyChanged("Percentage");
			}
		}

		public virtual bool MedAid
		{
			get
			{
				return _MedAid;
			}
			set
			{
				if (_MedAid == value)
					return;

				_MedAid = value;
				InvokePropertyChanged("MedAid");
			}
		}

		public ClientDependent()
		{
			IsLoading = true;
			BirthDate = MinDateTime;
			IsLoading = false;
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			InvokeModelCalculated();
		}
				
	}
}
