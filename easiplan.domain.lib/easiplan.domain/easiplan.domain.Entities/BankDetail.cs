using my.domain.lib.core.Attributes;
using my.domain.lib.core.Extensions;

namespace easiplan.domain.Entities
{
	public class BankDetail : BaseEntity<int>
	{
		private string _BnkName;

		private string _BrnchName;

		private string _BrnchCode;

		private string _AcctName;

		private string _AcctType;

		private string _AcctNumber;

		private bool _AcctVerified;

		private string _AcctReason;

		public virtual string BnkName
		{
			get
			{
				return _BnkName;
			}
			set
			{
				_BnkName = value;
				InvokePropertyChanged("BnkName");
			}
		}

		public virtual string BrnchName
		{
			get
			{
				return _BrnchName.InitCaps();
			}
			set
			{
				_BrnchName = value.InitCaps();
				InvokePropertyChanged("BrnchName");
			}
		}

		public virtual string BrnchCode
		{
			get
			{
				return _BrnchCode;
			}
			set
			{
				_BrnchCode = value;
				InvokePropertyChanged("BrnchCode");
			}
		}

		public virtual string AcctName
		{
			get
			{
				return _AcctName;
			}
			set
			{
				_AcctName = value;
				InvokePropertyChanged("AcctName");
			}
		}

		public virtual string AcctType
		{
			get
			{
				return _AcctType;
			}
			set
			{
				_AcctType = value;
				InvokePropertyChanged("AcctType");
			}
		}

		[Encrypt]
		public virtual string AcctNumber
		{
			get
			{
				return _AcctNumber;
			}
			set
			{
				_AcctNumber = value;
				InvokePropertyChanged("AcctNumber");
			}
		}

		public virtual bool AcctVerified
		{
			get
			{
				return _AcctVerified;
			}
			set
			{
				_AcctVerified = value;
				InvokePropertyChanged("AcctVerified");
			}
		}

		public virtual string AcctReason
		{
			get
			{
				return _AcctReason;
			}
			set
			{
				_AcctReason = value;
				InvokePropertyChanged("AcctReason");
			}
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
