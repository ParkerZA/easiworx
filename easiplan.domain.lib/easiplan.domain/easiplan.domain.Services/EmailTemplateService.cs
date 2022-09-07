using easiplan.domain.Entities;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class EmailTemplateService : BaseGenericService<EmailTemplate, int>
	{
		public EmailTemplateService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override void Add(EmailTemplate entity)
		{
			int cnt = base.Count((EmailTemplate x) => x.TemplateName == entity.TemplateName);
			if (cnt > 0)
			{
				throw new MyValidationException($"There is already a Template with the name '{entity.TemplateName}'.", "Username");
			}
			base.Add(entity);
		}

		public override void Update(EmailTemplate entity)
		{
			base.Update(entity);
		}

		public override void Remove(int id)
		{
			base.Remove(id);
		}
	}
}
