//using easiplan.app.extentions;
using my.domain.lib.core.Extensions;

namespace easiplan.domain.Entities
{
	public class AddressDetail : BaseEntity<int>
	{
		private string _Type;

		private string _Description;

		private string _Line1;

		private string _Line2;

		private string _Line3;

		private string _Line4;

		private int _Code;

		public virtual string Type
		{
			get
			{
				return _Type;
			}
			set
			{
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
				_Description = value;
				InvokePropertyChanged("Description");
			}
		}

		public virtual string Line1
		{
			get
			{
				return _Line1.InitCaps();
			}
			set
			{
				_Line1 = value.InitCaps();
				InvokePropertyChanged("Line1");
			}
		}

		public virtual string Line2
		{
			get
			{
				return _Line2.InitCaps();
			}
			set
			{
				_Line2 = value.InitCaps();
				InvokePropertyChanged("Line2");
			}
		}

		public virtual string Line3
		{
			get
			{
				return _Line3.InitCaps();
			}
			set
			{
				_Line3 = value.InitCaps();
				InvokePropertyChanged("Line3");
			}
		}

		public virtual string Line4
		{
			get
			{
				return _Line4.InitCaps();
			}
			set
			{
				_Line4 = value.InitCaps();
				InvokePropertyChanged("Line4");
			}
		}

		public virtual int Code
		{
			get
			{
				return _Code;
			}
			set
			{
				_Code = value;
				InvokePropertyChanged("Code");
			}
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
