using easiplan.domain.estate.Entities;
using easiplan.domain.Services;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;

namespace easiplan.domain.estate.Validators
{
    public class EstateAnalysisInsertValidator : baseValidator<EstateAnalysis>
    {
        public EstateAnalysisInsertValidator(IGenericRepository repository)
            : base(repository)
        {
        }
        
        public override ValidationResult Validate(ValidationContext<EstateAnalysis> context)
        {
            var estateAnalysisInsertValidator = new EstateAnalysisInsertValidator(base.Repository);
            var results = estateAnalysisInsertValidator.Validate(context.InstanceToValidate);
            if (!results.IsValid)
            {
                throw new MyValidationException(results.Errors[0].ErrorMessage);
            }
            return base.Validate(context);
        }
    }
}