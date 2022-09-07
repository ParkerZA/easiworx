namespace easiplan.domain.Entities
{
	public class Provider : BaseEntity<int>
	{
		public virtual LispProviders LispProviders
		{
			get;
			set;
		}

		public virtual MedicalProviders MedicalProviders
		{
			get;
			set;
		}

		public virtual LifeProviders LifeProviders
		{
			get;
			set;
		}

		public Provider()
		{
			LispProviders = new LispProviders();
			MedicalProviders = new MedicalProviders();
			LifeProviders = new LifeProviders();
		}
	}
}
