using easiplan.domain.estate.Entities;
using easiplan.domain.estate.Validators;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using FluentValidation.Results;

namespace easiplan.domain.estate.Services
{
    public class EstateAnalysisService : BaseGenericService<EstateAnalysis, int>
    {
        public EstateAnalysisService(IGenericRepository repository) : base(repository)
        {
        }
        
        public override void Add(EstateAnalysis entity)
        {
            //var validator = new EstateAnalysisInsertValidator(base.Repository);
            //var results = validator.Validate(entity);
            //if (!results.IsValid)
            //{
            //    throw new MyValidationException(results.Errors[0].ErrorMessage);
            //}
            //entity.Calculate();
            base.Add(entity);
        }

        public override void Update(EstateAnalysis entity)
        {
            //var validator = new EstateAnalysisInsertValidator(base.Repository);
            //var results = validator.Validate(entity);
            //if (!results.IsValid)
            //{
            //    throw new MyValidationException(results.Errors[0].ErrorMessage);
            //}
            //entity.Calculate();
            base.Update(entity);
        }

        public override void Remove(int id)
        {
            var client = Get(id);
            if (client != null)
            {
                Update(client);
            }
            base.Remove(id);
        }
    }
}