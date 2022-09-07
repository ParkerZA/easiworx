using FluentValidation;
using my.domain.lib.core.Repository;

namespace easiplan.domain.Services
{
	public abstract class baseValidator<T> : AbstractValidator<T>
	{
		public IGenericRepository Repository { get; }

		public baseValidator(IGenericRepository repository)
		{
			Repository = repository;
		}
	}
}
