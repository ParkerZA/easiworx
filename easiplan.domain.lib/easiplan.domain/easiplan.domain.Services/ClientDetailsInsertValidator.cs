using easiplan.domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace easiplan.domain.Services
{
	public class ClientDetailsInsertValidator : baseValidator<ClientDetails>
	{
		public ClientDetailsInsertValidator(IGenericRepository repository)
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
			if (!string.IsNullOrEmpty(context.InstanceToValidate.IdentificationNo))
			{
				IEnumerable<ClientDetails> clientDetails2 = base.Repository.List<ClientDetails, int>((ClientDetails x) => x.DateOfBirth == context.InstanceToValidate.DateOfBirth && x.ClientId > 0, 0, 0, null, true);
				if (clientDetails2.Count() > 0 && (from x in clientDetails2
				where x.IdentificationNo == context.InstanceToValidate.IdentificationNo && x.Id != context.InstanceToValidate.Id
				select x).FirstOrDefault() != null)
				{
					throw new MyValidationException($"There is already a Client with this identification number '{context.InstanceToValidate.IdentificationNo}'.", "IdentificationNo");
				}
			}
			if (!string.IsNullOrEmpty(context.InstanceToValidate.PassportNo))
			{
				IEnumerable<ClientDetails> clientDetails = base.Repository.List<ClientDetails, int>((ClientDetails x) => x.DateOfBirth == context.InstanceToValidate.DateOfBirth && x.ClientId > 0, 0, 0, null, true);
				if (clientDetails.Count() > 0 && (from x in clientDetails
				where x.PassportNo == context.InstanceToValidate.PassportNo && x.Id != context.InstanceToValidate.Id
				select x).FirstOrDefault() != null)
				{
					throw new MyValidationException($"There is already a Client with this passport number '{context.InstanceToValidate.PassportNo}'.", "PassportNo");
				}
			}
			return base.Validate(context);
		}
	}
}
