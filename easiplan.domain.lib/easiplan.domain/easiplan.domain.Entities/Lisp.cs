using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class Lisp : BaseEntity<int>
	{
		public virtual string LispName
		{
			get;
			set;
		}

		public virtual string LispFSB
		{
			get;
			set;
		}

		public virtual string LispContact
		{
			get;
			set;
		}

		public virtual string LispTel
		{
			get;
			set;
		}

		public virtual double InitialFees
		{
			get;
			set;
		}

		public virtual double AdminFees
		{
			get;
			set;
		}

		public virtual IList<LispFund> LispFunds
		{
			get;
			set;
		}

		public Lisp()
		{
			LispFunds = new List<LispFund>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
