using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class MedicalProviders : BaseEntity<int>
	{
		public virtual IList<MedicalAid> MedicalAids
		{
			get;
			set;
		}

		public MedicalProviders()
		{
			MedicalAids = new List<MedicalAid>();
		}
	}
}
