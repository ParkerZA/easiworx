using my.domain.lib.core.Domain;
using System;

namespace easiplan.domain.Entities
{
	public class Benefit : BaseEntity<int>
	{
		private double _CoverAmount;

		public virtual string Type
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual double CoverAmount
		{
			get
			{
				return _CoverAmount;
			}
			set
			{
				_CoverAmount = value;
				InvokePropertyChanged("CoverAmount");
			}
		}

		public virtual double MonthlyContribution
		{
			get;
			set;
		}

		public virtual double EscalationPercentage
		{
			get;
			set;
		}

		public virtual DateTime StartDate
		{
			get;
			set;
		}

		public virtual DateTime EndDate
		{
			get;
			set;
		}

		public virtual string ReferenceNo
		{
			get;
			set;
		}

		public virtual int ReferenceId
		{
			get;
			set;
		}

		public Benefit()
		{
			StartDate = DateTime.Now;
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
