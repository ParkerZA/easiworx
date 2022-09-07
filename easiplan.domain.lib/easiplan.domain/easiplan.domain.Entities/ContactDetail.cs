using my.domain.lib.core.Attributes;

namespace easiplan.domain.Entities
{
	public class ContactDetail : BaseEntity<int>
	{
		private string _Type;

		private string _Description;

		private string _Text;

		public virtual string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (_Type == value)
					return;

				_Type = value;
				InvokePropertyChanged("Type");
			}
		}

		public virtual string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (_Description == value)
					return;

				_Description = value;
				InvokePropertyChanged("Description");
			}
		}

		[Encrypt]
		public virtual string Text
		{
			get
			{
				return _Text;
			}
			set
			{
				if (_Text == value)
					return;
				_Text = value;
				InvokePropertyChanged("Text");
			}
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
