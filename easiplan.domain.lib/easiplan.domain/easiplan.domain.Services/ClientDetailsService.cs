using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using easiplan.domain.Entities;
using easiplan.domain.Views;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ClientDetailsService : BaseGenericService<ClientDetails, int>
	{
        public IEnumerable<ClientDetails> ClientDetailsCache = null;
		User _user;

        public ClientDetailsService(IGenericRepository repository,User user)
			: base(repository)
		{
			_user = user;
		}

        public override IEnumerable<ClientDetails> List(Expression<Func<ClientDetails, bool>> predicate)
        {
            //if (ClientDetailsCache == null || predicate!=null)
            //    ClientDetailsCache = base.List(predicate);

            //return ClientDetailsCache;

			return base.List(predicate);
        }

        public IEnumerable<ClientDetailsView> ListView(Expression<Func<ClientDetailsView, bool>> predicate)
        {
			//TO DO : Add Cacheing

			//if (ClientDetailsCache == null || predicate != null)
			//	ClientDetailsCache = base.List(predicate);

			var _list = (List<ClientDetailsView>)Repository.List<ClientDetailsView, int>(predicate);

			if (_user != null)
				if (_user.IsAdvisorOnly) // restrict list to clients of this advisor
					_list = _list.Where(x => x.AgentId == _user.Id).ToList();

			return _list;

        }

		//public IEnumerable<ClientRetirementPortfolio_View> ListView(Expression<Func<ClientRetirementPortfolio_View, bool>> predicate)
		//{
		//	//TO DO : Add Cacheing

		//	//if (ClientDetailsCache == null || predicate != null)
		//	//	ClientDetailsCache = base.List(predicate);

		//	return Repository.List<ClientRetirementPortfolio_View, int>(predicate);

		//}

		public override void Add(ClientDetails entity)
		{
			ClientDetailsInsertValidator validator = new ClientDetailsInsertValidator(base.Repository);
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}
			base.Add(entity);
		}

		public override void Update(ClientDetails entity)
		{
			ClientDetailsUpdateValidator validator = new ClientDetailsUpdateValidator(base.Repository);
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}
			base.Update(entity);
		}
	}
}
