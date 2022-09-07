using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain
{
	public class DomainServices : BaseDomainServices
	{
		public DomainServices(IGenericRepository repository)
			: base(repository)
		{
		}
	}
}
