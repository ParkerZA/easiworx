using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class LifeInsurer : BaseEntity<int>
	{
		public virtual string InsurerName
		{
			get;
			set;
		}

		public virtual string FSB
		{
			get;
			set;
		}

		public virtual string Contact
		{
			get;
			set;
		}

		public virtual string Tel
		{
			get;
			set;
		}

		public virtual double InitialFees
		{
			get;
			set;
		}

		public virtual double AdminFees
		{
			get;
			set;
		}

		public virtual IList<LifeProduct> LifeProducts
		{
			get;
			set;
		}

		public LifeInsurer()
		{
			LifeProducts = new List<LifeProduct>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
