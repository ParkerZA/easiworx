using easiplan.domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ClientUpdateValidator : baseValidator<Client>
    {
		public ClientUpdateValidator(IGenericRepository repository)
			: base(repository)
		{
            
        }

		public override ValidationResult Validate(ValidationContext<Client> context)
		{
            ClientDetailsUpdateValidator ClientDetailsUpdateValidator = new ClientDetailsUpdateValidator(base.Repository);
            ValidationResult results = ClientDetailsUpdateValidator.Validate(context.InstanceToValidate.ClientDetails);
            if (!results.IsValid)
            {
                throw new MyValidationException(results.Errors[0].ErrorMessage);
            }
            return base.Validate(context);
        }
	}

}
