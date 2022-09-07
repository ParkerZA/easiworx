using my.domain.lib.core.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientFees : BaseEntity<int>
	{
		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual IList<ClientFee> Fees
		{
			get;
			set;
		}

		public ClientFees()
		{
			Fees = new List<ClientFee>();
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual BindingList<ClientFee> FeesBindingList { get; set; }

		public override void Initialise(bool isLoading = false)
		{
			FeesBindingList = new BindingList<ClientFee>(Fees);
			FeesBindingList.RaiseListChangedEvents = true;
			FeesBindingList.ListChanged += FeesBindingList_ListChanged;


			base.Initialise(isLoading);
		}

		private void FeesBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			IsLoading = false;

			if (e.PropertyDescriptor != null && e.ListChangedType == ListChangedType.ItemChanged)
				InvokePropertyChanged(e.PropertyDescriptor.Name);


		}

		public override void Calculate()
		{
			foreach (ClientFee fee in Fees)
			{
				fee.Calculate();
			}
			base.Calculate();
		}
	}
}
