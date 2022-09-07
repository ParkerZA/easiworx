using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using easiplan.domain.Entities;
using easiplan.domain.Views;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ClientRetirementPortfolioService : BaseGenericService<ClientPortfolio, int>
	{
		public IEnumerable<ClientRetirementPortfolio_View> ClientRetirementPortfolioCache = null;

		public ClientRetirementPortfolioService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override IEnumerable<ClientPortfolio> List(Expression<Func<ClientPortfolio, bool>> predicate)
		{
			return base.List(predicate);
		}

		public IEnumerable<ClientRetirementPortfolio_View> ListView(Expression<Func<ClientRetirementPortfolio_View, bool>> predicate)
		{
			try
            {
					return Repository.List<ClientRetirementPortfolio_View, int>(predicate);
			}
            catch (Exception)
            {
			    throw;
            }
		}

		
		public override void Add(ClientPortfolio entity)
		{
			base.Add(entity);
		}

		public override void Update(ClientPortfolio entity)
		{
			base.Update(entity);
		}
	}
}
