using easiplan.domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace easiplan.domain.Services
{
	public class ClientDetailsUpdateValidator : baseValidator<ClientDetails>
    {
		public ClientDetailsUpdateValidator(IGenericRepository repository)
			: base(repository)
		{
            RuleFor((ClientDetails x) => x.FirstName).NotEmpty().WithMessage("Firstname is Required");
            RuleFor((ClientDetails x) => x.LastName).NotEmpty().WithMessage("Surname is Required");
            RuleFor((ClientDetails x) => x.DateOfBirth).NotEmpty().WithMessage("Date Of Birth is required");
        }

		public override ValidationResult Validate(ValidationContext<ClientDetails> context)
		{
            if (string.IsNullOrEmpty(context.InstanceToValidate.IdentificationNo) && string.IsNullOrEmpty(context.InstanceToValidate.PassportNo))
            {
                throw new MyValidationException(string.Format("Identification or Passport Number is required.", context.InstanceToValidate.IdentificationNo), "IdentificationNo");
            }
            
            return base.Validate(context);
        }
	}

   

}
