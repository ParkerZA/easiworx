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
			
			
			Client client = Get(id);
			if (client != null)
			{
                base.Repository.Execute("UPDATE client SET AgentDetails_id = null where Id=" + id, null);

				base.Repository.Remove<Client, int>(id);

				if(client.ClientDetails!= null)
                    base.Repository.Remove<ClientDetails, int>(client.ClientDetails.Id);

                if (client.SpouseDetails != null)
                    base.Repository.Remove<ClientDetails, int>(client.SpouseDetails.Id);

				if (client.ClientDependents != null){
					foreach(var x in client.ClientDependents.Dependents)
                        base.Repository.Remove<ClientDependent, int>(x.Id);
                    base.Repository.Remove<ClientDependents, int>(client.ClientDependents.Id);
                }

                if (client.PhysicalAddress != null)
                    base.Repository.Remove<AddressDetail, int>(client.PhysicalAddress.Id);

                if (client.PostalAddress != null)
                    base.Repository.Remove<AddressDetail, int>(client.PostalAddress.Id);

                if (client.ClientAdditionalInfo != null)
                {
                    foreach (var x in client.ClientAdditionalInfo.AdditionalInfos)
                        base.Repository.Remove<AdditionalInfo, int>(x.Id);
                    base.Repository.Remove<ClientAdditionalInfo, int>(client.ClientAdditionalInfo.Id);
                }

                if (client.ClientContacts != null)
                {
                    foreach (var x in client.ClientContacts.ContactDetails)
                        base.Repository.Remove<ContactDetail, int>(x.Id);
                    base.Repository.Remove<ClientContacts, int>(client.ClientContacts.Id);
                }

                if (client.SpouseContacts != null)
                {
                    foreach (var x in client.SpouseContacts.ContactDetails)
                        base.Repository.Remove<ContactDetail, int>(x.Id);
                    base.Repository.Remove<ClientContacts, int>(client.SpouseContacts.Id);
                }

                if (client.BankDetails != null)
                    base.Repository.Remove<BankDetail, int>(client.BankDetails.Id);

                if (client.ClientAssets != null)
                {
                    foreach (var x in client.ClientAssets.Assets)
                        base.Repository.Remove<Asset, int>(x.Id);
                    base.Repository.Remove<ClientAssets, int>(client.ClientAssets.Id);
                }

                if (client.ClientLiabilities != null)
                {
                    foreach (var x in client.ClientLiabilities.Liabilities)
                        base.Repository.Remove<Liability, int>(x.Id);
                    base.Repository.Remove<ClientLiabilities, int>(client.ClientLiabilities.Id);
                }

                if (client.ClientIncomes != null)
                {
                    foreach (var x in client.ClientIncomes.Incomes)
                        base.Repository.Remove<Income, int>(x.Id);
                    base.Repository.Remove<ClientIncomes, int>(client.ClientIncomes.Id);
                }

                if (client.ClientExpenses != null)
                {
                    foreach (var x in client.ClientExpenses.Expenses)
                        base.Repository.Remove<Expense, int>(x.Id);
                    base.Repository.Remove<ClientExpenses, int>(client.ClientExpenses.Id);
                }
                
                if (client.ClientFna != null)
                {
                    foreach (var x in client.ClientFna.Wants)
                        base.Repository.Remove<Want, int>(x.Id);                    
                    foreach (var x in client.ClientFna.Needs)
                        base.Repository.Remove<Need, int>(x.Id);
                    foreach (var x in client.ClientFna.FnaNotes)
                        base.Repository.Remove<Note, int>(x.Id);

                    base.Repository.Remove<ClientFna, int>(client.ClientFna.Id);
                }

                if (client.ClientPortfolio != null)
                {
                    foreach (var x in client.ClientPortfolio.Retirements)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Funds)
                            base.Repository.Remove<Fund, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);
                       
                        base.Repository.Remove<Retirement, int>(x.Id);
                    }
                    foreach (var x in client.ClientPortfolio.Investments)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Funds)
                            base.Repository.Remove<Fund, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);

                        base.Repository.Remove<Investment, int>(x.Id);
                    }
                    foreach (var x in client.ClientPortfolio.Educations)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Funds)
                            base.Repository.Remove<Fund, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);

                        base.Repository.Remove<Education, int>(x.Id);
                    }
                    foreach (var x in client.ClientPortfolio.Lifes)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Benefits)
                            base.Repository.Remove<Benefit, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);

                        base.Repository.Remove<Life, int>(x.Id);
                    }
                    foreach (var x in client.ClientPortfolio.Medicals)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Benefits)
                            base.Repository.Remove<Benefit, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);

                        base.Repository.Remove<Medical, int>(x.Id);
                    }

                    foreach (var x in client.ClientPortfolio.IncomeAssets)
                    {
                        foreach (var y in x.Amendments)
                            base.Repository.Remove<Need, int>(y.Id);
                        foreach (var y in x.Funds)
                            base.Repository.Remove<Benefit, int>(y.Id);
                        foreach (var y in x.Beneficiaries)
                            base.Repository.Remove<ClientDependent, int>(y.Id);
                        foreach (var y in x.Notes)
                            base.Repository.Remove<Note, int>(y.Id);

                        base.Repository.Remove<IncomeAsset, int>(x.Id);
                    }

                    base.Repository.Remove<ClientPortfolio, int>(client.ClientPortfolio.Id);
                }




                // base.Remove(id);
            }

			

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