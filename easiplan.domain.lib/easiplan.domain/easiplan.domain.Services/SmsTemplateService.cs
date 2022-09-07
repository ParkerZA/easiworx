using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using easiplan.domain.Entities;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class SmsTemplateService : BaseGenericService<SmsTemplate, int>
	{
		public SmsTemplateService(IGenericRepository repository)
			: base(repository)
		{
		}

        public override void Add(SmsTemplate entity)
		{
			int cnt = base.Count((SmsTemplate x) => x.TemplateName == entity.TemplateName);
			if (cnt > 0)
			{
				throw new MyValidationException($"There is already a Template with the name '{entity.TemplateName}'.", "Username");
			}
			base.Add(entity);
		}

		public override void Update(SmsTemplate entity)
		{
			base.Update(entity);
		}

		public override void Remove(int id)
		{
			base.Remove(id);
		}
	}
}
