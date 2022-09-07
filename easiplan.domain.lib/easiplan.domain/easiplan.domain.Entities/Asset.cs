using System.ComponentModel;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;

namespace easiplan.domain.Entities
{
	//public class Asset : BaseEntity<int>
	//{
	//	#region Private Variables
	//	private string _type;

	//	private string _class;

	//	private string _description;

	//	private double _value;
	//	#endregion

	//	public virtual string Type
	//	{
	//		get
	//		{
	//			return _type;
	//		}
	//		set
	//		{
	//			_type = value;
	//			InvokePropertyChanged("type");
	//		}
	//	}

	//	public virtual string Class
	//	{
	//		get
	//		{
	//			return _class;
	//		}
	//		set
	//		{
	//			_class = value;
	//			InvokePropertyChanged("class");
	//		}
	//	}

	//	public virtual string Description
	//	{
	//		get
	//		{
	//			return _description;
	//		}
	//		set
	//		{
	//			_description = value;
	//			InvokePropertyChanged("description");
	//		}
	//	}

	//	public virtual double Value
	//	{
	//		get
	//		{
	//			return _value;
	//		}
	//		set
	//		{
	//			_value = value;
	//			InvokePropertyChanged("value");
	//		}
	//	}







	//}

	public class Asset : BaseEntity<int>
	{
		#region Private Variables
		private string _type;

		private string _class;

		private string _description;

		private double _value;

		private double _basecost;

		private double _cgt;//capital gains tax

		private string _bequethTo;
		#endregion

		public virtual string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				InvokePropertyChanged("type");
			}
		}

		public virtual string Class
		{
			get
			{
				return _class;
			}
			set
			{
				_class = value;
				InvokePropertyChanged("class");
			}
		}

		public virtual string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
				InvokePropertyChanged("description");
			}
		}

		public virtual double Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				InvokePropertyChanged("value");
			}
		}

		public virtual double BaseCost
		{
			get
			{
				return _basecost;
			}
			set
			{
				_basecost = value;
				InvokePropertyChanged("basecost");
			}
		}

		[IgnoreAutoMap]
		public virtual double ProfitLoss
		{
			get
			{
				return Value- BaseCost;
			}
			
		}

		public virtual string BequethTo
		{
			get
			{
				return _bequethTo;
			}
			set
			{
				_bequethTo = value;
				InvokePropertyChanged("BequethTo");
			}
		}
	}
}
