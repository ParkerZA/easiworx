namespace easiplan.domain.Entities
{
	public class SysConfig : BaseEntity<int>
	{
		private int _DefRetAge;

		private int _DefLifeSpan;

		private decimal _DefInflRate;

		public virtual int DefRetAge
		{
			get
			{
				return _DefRetAge;
			}
			set
			{
				_DefRetAge = value;
				InvokePropertyChanged("DefRetAge");
			}
		}

		public virtual int DefLifeSpan
		{
			get
			{
				return _DefLifeSpan;
			}
			set
			{
				_DefLifeSpan = value;
				InvokePropertyChanged("DefLifeSpan");
			}
		}

		public virtual decimal DefInflRate
		{
			get
			{
				return _DefInflRate;
			}
			set
			{
				_DefInflRate = value;
				InvokePropertyChanged("DefInflRate");
			}
		}
	}
}
