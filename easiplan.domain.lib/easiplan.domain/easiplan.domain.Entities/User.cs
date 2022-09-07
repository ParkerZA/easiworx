using my.domain.lib.core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class User : BaseEntity<int>
	{
		private string _Username;

		private string _Password;

		private string _Title;

		private string _Firstname;

		private string _Surname;

		private string _Designation;

		private bool _IsAdministrator;

		private bool _IsActive;

		private string _CompanyName;

		[IgnoreAutoMap]
		public virtual string CompanyName
		{
			get
			{
				return _CompanyName;
			}
			set
			{
				_CompanyName = value;				
			}
		}

		[IgnoreDataMember]
		//[Required(ErrorMessage = "Username is Required")]
		public virtual string Username
		{
			get
			{
				return _Username;
			}
			set
			{
				_Username = value;
				InvokePropertyChanged("Username");
			}
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual string Fullname
		{
			get
			{
				return string.Format("{0} {1}",Firstname,Surname);
			}
			set
			{
				
			}
		}

		[Encrypt]
		[IgnoreDataMember]
		//[Required(ErrorMessage = "Password is Required")]
		public virtual string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				_Password = value;
				InvokePropertyChanged("Password");
			}
		}

		public virtual string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title = value;
				InvokePropertyChanged("Title");
			}
		}

		[Required(ErrorMessage = "Firstname is Required")]
		public virtual string Firstname
		{
			get
			{
				return _Firstname;
			}
			set
			{
				_Firstname = value;
				InvokePropertyChanged("Firstname");
			}
		}

		[Required(ErrorMessage = "Surname is Required")]
		public virtual string Surname
		{
			get
			{
				return _Surname;
			}
			set
			{
				_Surname = value;
				InvokePropertyChanged("Surname");
			}
		}

		[Required(ErrorMessage = "Designation is Required")]
		[IgnoreDataMember]
		public virtual string Designation
		{
			get
			{
				return _Designation;
			}
			set
			{
				_Designation = value;
				InvokePropertyChanged("Designation");
			}
		}

		[IgnoreDataMember]
		public virtual bool IsAdministrator
		{
			get
			{
				return _IsAdministrator;
			}
			set
			{
				_IsAdministrator = value;
				InvokePropertyChanged("IsAdministrator");
			}
		}

		[IgnoreDataMember]
		public virtual bool IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive = value;
				InvokePropertyChanged("IsActive");
			}
		}

		[Hash]
		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual byte[] Hash
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsClerk
		{
			get { return Designation.Contains("Clerk"); }
			set { }
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsAdvisor
		{
			get { return Designation.Contains("Advisor"); }
			set { }
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsGuest
		{
			get { return Designation.Contains("Guest"); }
			set { }
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsAdvisorOnly
		{
			get { return IsAdvisor && !IsAdministrator && !IsClerk && !IsAuthorisor; }
			set { }
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsAuthorisor
		{
			get { return Designation.Contains("Authoriser"); }
			set { }
		}

		public User()
		{
			IsLoading = true;

			IsActive = true;
			Designation = "";

			IsLoading = false;
		}
		public override void Calculate()
		{
			if (!InvokeModelCalculating())
				return;


			InvokeModelCalculated();
		}

	
	}
}
