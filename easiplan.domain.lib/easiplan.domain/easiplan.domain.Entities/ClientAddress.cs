using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class ClientAddress : BaseEntity<int>
	{
		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual IList<AddressDetail> AddressDetails
		{
			get;
			set;
		}

		public ClientAddress()
		{
			AddressDetails = new List<AddressDetail>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
