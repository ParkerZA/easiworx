namespace easiplan.domain.Entities
{
	public class LispFund : BaseEntity<int>
	{
		public virtual string FundCode
		{
			get;
			set;
		}

		public virtual string FundName
		{
			get;
			set;
		}

		public virtual string FundClass
		{
			get;
			set;
		}

		public virtual string UnitClass
		{
			get;
			set;
		}

		public virtual string RiskCat
		{
			get;
			set;
		}

		public virtual double TER
		{
			get;
			set;
		}

		public virtual double IRR
		{
			get;
			set;
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
