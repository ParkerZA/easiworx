using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientIncomes : BaseEntity<int>
	{
		private double _Total;

		public virtual double Total
		{
			get
			{
                _Total = (from x in Incomes
                         where x.Class == "Monthly"
                         select x).Sum((Income x) => x.Value);
                _Total += (from x in Incomes
                          where x.Class == "Annually"
                          select x).Sum((Income x) => x.Value) / 12.0;
                _Total += (from x in Incomes
                          where x.Class == "BiAnnually"
                          select x).Sum((Income x) => x.Value) / 6.0;
                return _Total;
			}
			set
			{
				_Total = value;
			}
		}

		public virtual IList<Income> Incomes
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Income> IncomesBindingList { get; set; }

        public ClientIncomes()
		{
			Incomes = new List<Income>();
			Incomes.Add(new Income
			{
				Class = "Monthly",
				Type = "Salary"
			});
			Incomes.Add(new Income
			{
				Class = "Monthly",
				Type = "Rental"
			});
			Incomes.Add(new Income
			{
				Class = "Monthly",
				Type = "Business"
			});
			Incomes.Add(new Income
			{
				Class = "Monthly",
				Type = "Other"
			});
		}

        public override void Initialise(bool isLoading = false)
        {
            IncomesBindingList = new BindingList<Income>(Incomes);
            IncomesBindingList.RaiseListChangedEvents = true;
            IncomesBindingList.ListChanged += BindingList_ListChanged;

            base.Initialise(isLoading);
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);

        }
    }
}
