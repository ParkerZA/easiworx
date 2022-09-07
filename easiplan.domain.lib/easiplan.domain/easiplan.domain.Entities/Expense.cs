using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;

namespace easiplan.domain.Entities
{
	public class Expense : BaseEntity<int>
	{
		private string _type;

		private string _class;

		private string _description;

		private double _value;

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

		public virtual double LifePerc
		{
			get;set;
		}
	
		public virtual double DisabilityPerc
		{
			get; set;
		}

		public virtual double RetirePerc
		{
			get; set;
		}
		

		public Expense()
		{
			Class = "Monthly";
		}

		public override void Calculate()
		{
		
			base.Calculate();
		}
	}
}
