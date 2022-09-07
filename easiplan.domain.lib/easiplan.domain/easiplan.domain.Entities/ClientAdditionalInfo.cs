using my.domain.lib.core.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientAdditionalInfo : BaseEntity<int>
	{
		public virtual IList<AdditionalInfo> AdditionalInfos
		{
			get;
			set;
		}

		public ClientAdditionalInfo()
		{
			AdditionalInfos = new List<AdditionalInfo>();
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual BindingList<AdditionalInfo> AdditionalInfoBindingList { get; set; }

		public override void Initialise(bool isLoading = false)
		{
			AdditionalInfoBindingList = new BindingList<AdditionalInfo>(AdditionalInfos);
			AdditionalInfoBindingList.RaiseListChangedEvents = true;
			AdditionalInfoBindingList.ListChanged += AdditionalInfoBindingList_ListChanged;


			base.Initialise(isLoading);
		}

		private void AdditionalInfoBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			IsLoading = false;

			if (e.PropertyDescriptor != null && e.ListChangedType == ListChangedType.ItemChanged)
				InvokePropertyChanged(e.PropertyDescriptor.Name);


		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
