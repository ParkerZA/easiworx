using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientExpenses : BaseEntity<int>
	{
		private double _Total;

		public virtual double Total
		{
			get
			{
                _Total = (from x in Expenses
                         where x.Class == "Monthly"
                         select x).Sum((Expense x) => x.Value);
                _Total += (from x in Expenses
                          where x.Class == "Annually"
                          select x).Sum((Expense x) => x.Value) / 12.0;
                _Total += (from x in Expenses
                          where x.Class == "BiAnnually"
                          select x).Sum((Expense x) => x.Value) / 6.0;
                return _Total;
			}
			set
			{
				_Total = value;
			}
		}

		public virtual IList<Expense> Expenses
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Expense> ExpensesBindingList { get; set; }

        public ClientExpenses()
		{
			Expenses = new List<Expense>();
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Rent/Bond Repayments"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Food/Groceries"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Credit Card"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Children Expenses"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Entertainment"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Household Insurance"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Housing Expenses"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Legal Expenses"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Personal Expenses"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Rates & Taxes"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "School Fees"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Transport Expenses"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Water & Electricity"
			});
			Expenses.Add(new Expense
			{
				Class = "Annually",
				Type = "Zakaat"
			});
			Expenses.Add(new Expense
			{
				Class = "Monthly",
				Type = "Other"
			});
		}

        public override void Initialise(bool isLoading = false)
        {
            ExpensesBindingList = new BindingList<Expense>(Expenses);
            ExpensesBindingList.RaiseListChangedEvents = true;
            ExpensesBindingList.ListChanged += BindingList_ListChanged;

            base.Initialise(isLoading);
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);

        }
    }
}
