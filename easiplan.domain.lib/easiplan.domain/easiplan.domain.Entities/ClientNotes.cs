using System;
using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class ClientNotes : BaseEntity<int>
	{
		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual IList<Note> Notes
		{
			get;
			set;
		}

		public ClientNotes()
		{
			Notes = new List<Note>();
		}

		public override void Calculate()
		{
			try
			{
				base.Calculate();
			}
			catch (Exception)
			{
			}
		}
	}
}
