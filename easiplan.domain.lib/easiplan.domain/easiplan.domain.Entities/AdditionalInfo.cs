namespace easiplan.domain.Entities
{
	public class AdditionalInfo : BaseEntity<int>
	{
		public virtual string Type
		{
			get;
			set;
		}

		public virtual string InfoName
		{
			get;
			set;
		}

		public virtual string InfoValue
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
