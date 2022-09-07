using System;

namespace easiplan.domain.Entities
{
	public class ClientLockStatus : BaseEntity<int>
	{
		private int _ClientId;

		private bool _IsLocked;

		private DateTime _LockDate;

		private int _UserId;

		private string _FirstName;

		private string _Surname;

		private string _MachineName;

		public virtual int ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				InvokePropertyChanged("ClientId");
			}
		}

		public virtual bool IsLocked
		{
			get
			{
				return _IsLocked;
			}
			set
			{
				_IsLocked = value;
				InvokePropertyChanged("IsLocked");
			}
		}

		public virtual DateTime LockDate
		{
			get
			{
				return _LockDate;
			}
			set
			{
				_LockDate = value;
				InvokePropertyChanged("LockDate");
			}
		}

		public virtual int UserId
		{
			get
			{
				return _UserId;
			}
			set
			{
				_UserId = value;
				InvokePropertyChanged("UserId");
			}
		}

		public virtual string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				_FirstName = value;
				InvokePropertyChanged("FirstName");
			}
		}

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

		public virtual string MachineName
		{
			get
			{
				return _MachineName;
			}
			set
			{
				_MachineName = value;
				InvokePropertyChanged("MachineName");
			}
		}
	}
}
