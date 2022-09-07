using my.domain.lib.core.Domain;
using System;

namespace easiplan.domain.Entities
{
	public class ClientFee : BaseEntity<int>
	{
		DateTime _effectiveDate;
		public virtual DateTime EffectiveDate
		{
			get { return _effectiveDate; }
			set { 
				if (_effectiveDate == value)return;

				_effectiveDate = value;			

				Calculate();
				InvokePropertyChanged("EffectiveDate");
			}
		}

		public virtual DateTime ExpiryDate
		{
			get;
			set;
		}

		public virtual double InitialFee
		{
			get;
			set;
		}

		public virtual double ManagementFee
		{
			get;
			set;
		}

		public virtual double OngoingFee
		{
			get;
			set;
		}

		public virtual double OtherFee
		{
			get;
			set;
		}

		public ClientFee()
		{
			EffectiveDate = DateTime.Now;
			ExpiryDate = DateTime.Now.AddYears(1);
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating(new EntityEventArgs()))
				return;

			DateTime dateTime = DateTime.MinValue;
			if (dateTime.CompareTo(EffectiveDate) >= 0)
			{
				EffectiveDate = DateTime.Now;
			}
			dateTime = DateTime.MaxValue;
			if (dateTime.CompareTo(ExpiryDate) <= 0)
			{
				dateTime = DateTime.Now;
				ExpiryDate = dateTime.AddYears(1);
			}
			dateTime = EffectiveDate;
			EffectiveDate = DateTime.Parse(dateTime.ToShortDateString());
			dateTime = ExpiryDate;
			ExpiryDate = DateTime.Parse(dateTime.ToShortDateString());


			InvokeModelCalculated(new EntityEventArgs());
		}
	}
}
