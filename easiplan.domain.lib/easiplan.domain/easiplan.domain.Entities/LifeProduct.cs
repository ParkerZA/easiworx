namespace easiplan.domain.Entities
{
	public class LifeProduct : BaseEntity<int>
	{
		public virtual string ProductCode
		{
			get;
			set;
		}

		public virtual string ProductName
		{
			get;
			set;
		}

		public virtual string ProductType
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
