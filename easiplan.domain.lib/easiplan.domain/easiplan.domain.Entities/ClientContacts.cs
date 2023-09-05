using my.domain.lib.core.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientContacts : BaseEntity<int>
	{
		private string _BussTel;

		private string _HomeTel;

		private string _CellNo;

		private string _EMailAddr;

		private string _FaxNo;

        [IgnoreDataMember]
        public virtual int ClientId
		{
			get;
			set;
		}
        [IgnoreDataMember]
        public virtual int ClientDetailsId
		{
			get;
			set;
		}

		[Encrypt]
		public virtual string BussTel
		{
			get
			{
				return _BussTel;
			}
			set
			{
				if (_BussTel == value) return;

				_BussTel = value;
				InvokePropertyChanged("BussTel");
			}
		}

		[Encrypt]
		public virtual string HomeTel
		{
			get
			{
				return _HomeTel;
			}
			set
			{
				if (_HomeTel == value) return;

				_HomeTel = value;
				InvokePropertyChanged("HomeTel");
			}
		}

		[Encrypt]
		public virtual string CellNo
		{
			get
			{
				return _CellNo;
			}
			set
			{
				if (_CellNo == value) return;

				_CellNo = value;
				InvokePropertyChanged("CellNo");
			}
		}

		[Encrypt]
		public virtual string EMailAddr
		{
			get
			{
				return _EMailAddr;
			}
			set
			{
				if (_EMailAddr == value) return;

				_EMailAddr = value;
				InvokePropertyChanged("EMailAddr");
			}
		}

		[Encrypt]
		public virtual string FaxNo
		{
			get
			{
				return _FaxNo;
			}
			set
			{
				if (_FaxNo == value) return;

				_FaxNo = value;
				InvokePropertyChanged("FaxNo");
			}
		}

		public virtual IList<ContactDetail> ContactDetails
		{
			get;
			set;
		}
		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual BindingList<ContactDetail> ContactDetailsBindingList { get; set; }

		public override void Initialise(bool isLoading = false)
		{
			ContactDetailsBindingList = new BindingList<ContactDetail>(ContactDetails);
			ContactDetailsBindingList.RaiseListChangedEvents = true;
			ContactDetailsBindingList.ListChanged += ContactDetailsBindingList_ListChanged;


			base.Initialise(isLoading);
		}

		private void ContactDetailsBindingList_ListChanged(object sender, ListChangedEventArgs e)
		{
			IsLoading = false;

			if (e.PropertyDescriptor != null && e.ListChangedType == ListChangedType.ItemChanged)
				InvokePropertyChanged(e.PropertyDescriptor.Name);


		}
		public ClientContacts()
		{
			ContactDetails = new List<ContactDetail>();
			ContactDetails.Add(new ContactDetail
			{
				Type = "Home Telephone",
				Description = "Emergency"
			});
			ContactDetails.Add(new ContactDetail
			{
				Type = "Cellphone",
				Description = "Emergency"
			});
			ContactDetails.Add(new ContactDetail
			{
				Type = "Email",
				Description = "Other Email"
			});
			ContactDetails.Add(new ContactDetail
			{
				Type = "Fax",
				Description = "Fax Number"
			});
		}

		public override void Calculate()
		{
			base.Calculate();
		}
	}
}
