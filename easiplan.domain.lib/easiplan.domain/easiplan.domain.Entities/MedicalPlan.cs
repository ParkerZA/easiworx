namespace easiplan.domain.Entities
{
	public class MedicalPlan : BaseEntity<int>
	{
		public virtual string PlanCode
		{
			get;
			set;
		}

		public virtual string PlanName
		{
			get;
			set;
		}

		public virtual string PlanType
		{
			get;
			set;
		}

		public virtual double CoverLimit
		{
			get;
			set;
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
