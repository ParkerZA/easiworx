using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class LifeProviders : BaseEntity<int>
	{
		public virtual IList<LifeInsurer> LifeInsurers
		{
			get;
			set;
		}

		public LifeProviders()
		{
			LifeInsurers = new List<LifeInsurer>();
		}
	}
}
