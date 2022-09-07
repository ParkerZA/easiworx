using easiplan.domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace easiplan.domain.Services
{
	public class UserService : BaseGenericService<User, int>
	{

		IList<User> _userList;

		public class UserLogonValidator : AbstractValidator<User>
		{
			public UserLogonValidator()
			{
				RuleFor((User user) => user.Username).NotEmpty().WithMessage("Username is Required");
				RuleFor((User user) => user.Password).NotEmpty().WithMessage("Password is Required");
			}

			public override ValidationResult Validate(ValidationContext<User> context)
			{
				return base.Validate(context);
			}
		}

		public class UserInsertValidator : AbstractValidator<User>
		{
			public UserInsertValidator()
			{
				RuleFor((User user) => user.Firstname).NotEmpty().WithMessage("Firstname is Required");
				RuleFor((User user) => user.Surname).NotEmpty().WithMessage("Surname is Required");
				RuleFor((User user) => user.Username).NotEmpty().WithMessage("Username is Required");
				RuleFor((User user) => user.Password).NotEmpty().WithMessage("Password is Required");
				RuleFor((User user) => user.Designation).NotEmpty().WithMessage("Designation is Required");
				RuleFor((User user) => user.Password).NotEmpty().Length(6, 20).WithMessage("Password is required. Min 6-20 characters");
			}

			public override ValidationResult Validate(ValidationContext<User> context)
			{
				return base.Validate(context);
			}
		}

		public class UserUpdateValidator : UserInsertValidator
		{
			public UserUpdateValidator()
			{
				RuleFor((User user) => user.Designation).NotEmpty().WithMessage("Designation is Required");
			}

			public override ValidationResult Validate(ValidationContext<User> context)
			{
				return base.Validate(context);
			}
		}

		public UserService(IGenericRepository repository)
			: base(repository)
		{
			Refresh();
		}

		public void Refresh()
        {
		
			_userList = base.List(x => x.IsActive == true && x.Firstname != "") as List<User>;
			_userList.Add(new User() { Firstname = "", Designation = "",Id=0 });
		}
		public override void Add(User entity)
		{
			UserInsertValidator validator = new UserInsertValidator();
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}
			int cnt = base.Count((User x) => x.Username == entity.Username);
			if (cnt > 0)
			{
				throw new MyValidationException($"There is already a User with the username '{entity.Username}'.", "Username");
			}
			base.Add(entity);
		}

		public override void Update(User entity)
		{
			UserUpdateValidator validator = new UserUpdateValidator();
			ValidationResult results = validator.Validate(entity);
			if (!results.IsValid)
			{
				throw new MyValidationException(results.Errors[0].ErrorMessage);
			}
			base.Update(entity);
		}

		public override void Remove(int id)
		{
			base.Remove(id);
		}

		public void ValidateUserPassword(ref User entity)
		{
			try
			{
				UserLogonValidator validator = new UserLogonValidator();
				ValidationResult results = validator.Validate(entity);
				if (!results.IsValid)
				{
					throw new MyValidationException(results.Errors[0].ErrorMessage);
				}
				string _userName = entity.Username;
				IEnumerable<User> _users = base.List((User x) => x.Username == _userName);
				if (_users == null || _users.Count() <= 0)
				{
					throw new MyValidationException("An Invalid Username or Password was entered");
				}
				string _password = entity.Password;
				User _user = (from x in _users
				where x.Password == _password
				select x).FirstOrDefault();
				if (_user == null)
				{
					throw new MyValidationException("An Invalid Username or Password was entered");
				}
				if (!_user.IsActive)
				{
					throw new MyValidationException("Your account has been locked. Please contact your administrator.");
				}
				entity = _user;

                entity.Calculate();
			}
			catch (MyValidationException vx)
			{
				throw vx;
			}
			catch (Exception x2)
			{
				throw x2;
			}
		}

		/// <summary>
		/// Verify user has been added to the database , else add user
		/// </summary>
		/// <param name="entity"></param>
		public void VerifyUser(ref User entity)
        {
            try {
				string _userName = entity.Username;

				User _user = base.List((User x) => x.Username == _userName).FirstOrDefault();
				if (_user == null)
					base.Add(entity);
				else //return user 
                {				
					if (!_user.IsActive)
					{
						throw new MyValidationException("Your account has been locked. Please contact your administrator.");
					}
					_user.CompanyName = entity.CompanyName;

					entity = _user;
				}
				entity.Calculate();
			}
			catch(Exception x) { }
        }

		public IList<User> ListActiveUsers()
        {
			return _userList;
		}

		public IList<User> ListAdvisors()
		{
			return ListActiveUsers().Where(x => x.IsAdvisor == true).ToList();
		}

		public User GetAdvisor(int agentId)
		{
			if (agentId < 1)
				return null;

			return ListAdvisors().Where(x => x.Id == agentId).FirstOrDefault();
		}

		public IList<User> ListClerks()
		{
			return ListActiveUsers().Where(x => x.IsClerk == true).ToList();
		}

		public IList<User> ListAuthorisers()
		{
			return ListActiveUsers().Where(x => x.IsAdvisor == true).ToList();
		}
	}
}
