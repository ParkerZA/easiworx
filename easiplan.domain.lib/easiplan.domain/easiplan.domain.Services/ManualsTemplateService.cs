using easiplan.domain.Entities;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ManualsTemplateService : BaseGenericService<ManualsTemplate, int>
	{
		public ManualsTemplateService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override void Add(ManualsTemplate entity)
		{
			int cnt = base.Count((ManualsTemplate x) => x.TemplateName == entity.TemplateName);
			if (cnt > 0)
			{
				throw new MyValidationException($"There is already a Template with the name '{entity.TemplateName}'.", "Username");
			}
			base.Add(entity);
		}
	}
}
