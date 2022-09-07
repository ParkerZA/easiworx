using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class Company : BaseEntity<int>
	{
		public virtual IList<User> Users
		{
			get;
			set;
		}

		public virtual string TradeName
		{
			get;
			set;
		}

		public virtual string RegistrationNumber
		{
			get;
			set;
		}

		public virtual string FSBNumber
		{
			get;
			set;
		}

		public virtual string Caption
		{
			get;
			set;
		}

		public virtual byte[] BgImage
		{
			get;
			set;
		}

		public Company()
		{
			Users = new List<User>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
