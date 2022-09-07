using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientAssets : BaseEntity<int>
	{
		private double _Total;

		public virtual double Total
		{
			get
			{
                _Total = Assets.Sum((Asset x) => x.Value); return _Total ;
			}
			set
			{
				_Total = value;
				
			}
		}

		public virtual IList<Asset> Assets
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[IgnoreAutoMap]
        public virtual BindingList<Asset> AssetsBindingList { get; set; }	

		public ClientAssets()
		{

            IsLoading = true;

			Assets = new List<Asset>();
			Assets.Add(new Asset
			{
				Class = "Property",
				Type = "Non-Income"
			});
			Assets.Add(new Asset
			{
				Class = "Property",
				Type = "Income"
			});
			Assets.Add(new Asset
			{
				Class = "Vehicles",
				Type = "Depreciating"
			});
			Assets.Add(new Asset
			{
				Class = "Jewellery",
				Type = "Investments"
			});
			Assets.Add(new Asset
			{
				Class = "Shares",
				Type = "Investments"
			});
			Assets.Add(new Asset
			{
				Class = "Cash",
				Type = "Fixed"
			});

            IsLoading = false;
        }

        public override void Initialise(bool isLoading = false)
        {
            AssetsBindingList = new BindingList<Asset>(Assets);
            AssetsBindingList.RaiseListChangedEvents = true;
			
			AssetsBindingList.ListChanged += BindingList_ListChanged;
  
            base.Initialise(isLoading);
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.PropertyDescriptor!=null)
                InvokePropertyChanged(e.PropertyDescriptor.Name);
            
        }

   
    }
}
