using easiplan.domain.Entities;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class DocumentTemplateService : BaseGenericService<DocumentTemplate, int>
	{
		public DocumentTemplateService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override void Add(DocumentTemplate entity)
		{
			int cnt = base.Count((DocumentTemplate x) => x.TemplateName == entity.TemplateName);
			if (cnt > 0)
			{
				throw new MyValidationException($"There is already a Template with the name '{entity.TemplateName}'.", "Username");
			}
			base.Add(entity);
		}

		public override void Update(DocumentTemplate entity)
		{
			base.Update(entity);
		}

		public override void Remove(int id)
		{
			base.Remove(id);
		}
	}
}
