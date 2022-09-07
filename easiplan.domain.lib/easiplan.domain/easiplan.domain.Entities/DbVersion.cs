namespace easiplan.domain.Entities
{
	public class DbVersion : BaseEntity<int>
	{
		private string _Version;

		private string _Username;

		public virtual string Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				InvokePropertyChanged("Version");
			}
		}

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
	}
}
