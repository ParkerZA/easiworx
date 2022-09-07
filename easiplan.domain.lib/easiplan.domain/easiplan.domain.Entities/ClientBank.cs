using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class ClientBank : BaseEntity<int>
	{
		public virtual IList<BankDetail> BankDetails
		{
			get;
			set;
		}

		public ClientBank()
		{
			BankDetails = new List<BankDetail>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
