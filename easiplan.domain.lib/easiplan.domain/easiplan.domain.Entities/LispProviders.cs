using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class LispProviders : BaseEntity<int>
	{
		public virtual IList<Lisp> Lisps
		{
			get;
			set;
		}

		public LispProviders()
		{
			Lisps = new List<Lisp>();
		}
	}
}
