using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class MedicalAid : BaseEntity<int>
	{
		public virtual string ProviderName
		{
			get;
			set;
		}

		public virtual string RegNo
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

		public virtual IList<MedicalPlan> MedicalPlans
		{
			get;
			set;
		}

		public MedicalAid()
		{
			MedicalPlans = new List<MedicalPlan>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
