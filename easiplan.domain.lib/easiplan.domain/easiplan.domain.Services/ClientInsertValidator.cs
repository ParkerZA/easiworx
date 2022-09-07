using easiplan.domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public class ClientInsertValidator : baseValidator<Client>
	{
		public ClientInsertValidator(IGenericRepository repository)
			: base(repository)
		{
		}

		public override ValidationResult Validate(ValidationContext<Client> context)
		{
			ClientDetailsInsertValidator ClientDetailsInsertValidator = new ClientDetailsInsertValidator(base.Repository);
			ValidationResult results = ClientDetailsInsertValidator.Validate(context.InstanceToValidate.ClientDetails);
			if (!results.IsValid)
			{
				//log the error
				//throw new MyValidationException(results.Errors[0].ErrorMessage);
			}
			return base.Validate(context);
		}
	}

}
