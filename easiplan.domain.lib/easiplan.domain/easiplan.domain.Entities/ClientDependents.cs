using my.domain.lib.core.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientDependents : BaseEntity<int>
	{
		public virtual IList<ClientDependent> Dependents
		{
			get;
			set;
		}

		public ClientDependents()
		{
			Dependents = new List<ClientDependent>();
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual BindingList<ClientDependent> DependentsBindingList { get; set; }

		public override void Initialise(bool isLoading = false)
        {
			DependentsBindingList = new BindingList<ClientDependent>(Dependents);
			DependentsBindingList.RaiseListChangedEvents = true;
			DependentsBindingList.ListChanged += DependentsBindingList_ListChanged;			

			base.Initialise(isLoading);
        }

		private void DependentsBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			IsLoading = false;

			if (e.PropertyDescriptor != null && e.ListChangedType == ListChangedType.ItemChanged)
                InvokePropertyChanged(e.PropertyDescriptor.Name);

			
        }

		public override void Calculate()
		{
			if (!InvokeModelCalculating())
				return;

			foreach (ClientDependent dependent in Dependents)
			{
				dependent.Calculate();
			}

			InvokeModelCalculated();

		}
	}
}
