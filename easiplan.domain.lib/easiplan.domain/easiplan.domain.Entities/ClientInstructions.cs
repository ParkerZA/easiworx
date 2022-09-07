using System.Collections.Generic;

namespace easiplan.domain.Entities
{
	public class ClientInstructions : BaseEntity<int>
	{
		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual IList<Instruction> Instructions
		{
			get;
			set;
		}

		public ClientInstructions()
		{
			Instructions = new List<Instruction>();
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
