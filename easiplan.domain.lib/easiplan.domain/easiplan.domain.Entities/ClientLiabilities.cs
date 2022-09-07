using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientLiabilities : BaseEntity<int>
	{
		private double _Total;

		public virtual double Total
		{
			get
			{
                Total = Liabilities.Sum((Liability x) => x.Value); return _Total;
			}
			set
			{
				_Total = value;
			}
		}

		public virtual IList<Liability> Liabilities
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Liability> LiabilitiesBindingList { get; set; }

        public ClientLiabilities()
		{
			Liabilities = new List<Liability>();
			Liabilities.Add(new Liability
			{
				Class = "Bond",
				Type = "Variable"
			});
			Liabilities.Add(new Liability
			{
				Class = "Loan",
				Type = "Variable"
			});
			Liabilities.Add(new Liability
			{
				Class = "Credit Card",
				Type = "Fixed"
			});
			Liabilities.Add(new Liability
			{
				Class = "Overdraft",
				Type = "Fixed"
			});
			Liabilities.Add(new Liability
			{
				Class = "Hire Purchase",
				Type = "Fixed"
			});
			Liabilities.Add(new Liability
			{
				Class = "Accounts",
				Type = "Fixed"
			});
			Liabilities.Add(new Liability
			{
				Class = "Creditors",
				Type = "Fixed"
			});
		}

        public override void Initialise(bool isLoading = false)
        {
            LiabilitiesBindingList = new BindingList<Liability>(Liabilities);
            LiabilitiesBindingList.RaiseListChangedEvents = true;
            LiabilitiesBindingList.ListChanged += BindingList_ListChanged;

            base.Initialise(isLoading);
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);
        }

    }
}
