using easiplan.domain.Entities;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ClientService : BaseGenericService<Client, int>
	{
		public ClientService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override void Add(Client entity)
		{
			ClientInsertValidator validator = new ClientInsertValidator(base.Repository);
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}

            base.Add(entity);

            entity.SetModified(false);
		}

		public override void Update(Client entity)
		{
			ClientUpdateValidator validator = new ClientUpdateValidator(base.Repository);
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}

            base.Update(entity);

            entity.SetModified(false);

		}

		public override void Remove(int id)
		{
            base.Repository.BeginTransaction();
			
			base.Repository.Execute("UPDATE client SET AgentDetails_id = null where Id=" + id, null);

			base.Remove(id);

            base.Repository.Commit();
        }

	}

	public class ClientFnaRiskService : BaseGenericService<ClientFnaRisk, int>
	{
		public ClientFnaRiskService(IGenericRepository repository)
			: base(repository)
		{
		}

		public override void Add(ClientFnaRisk entity)
		{
            base.Add(entity);
           
            entity.SetModified(false);
		}

		public override void Update(ClientFnaRisk entity)
		{
           
            base.Update(entity);


            entity.SetModified(false);

		}

		public override void Remove(int id)
		{
			base.Repository.BeginTransaction();

			//ClientFnaRisk client = Get(id);
			//if (client != null)
			//{	
			//	Update(client);
			//}
			base.Remove(id);

			base.Repository.Commit();
		}
	}
}