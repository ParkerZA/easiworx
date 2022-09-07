using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace easiplan.domain
{
	public class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler ItemPropertyChanged;

		
        public TrulyObservableCollection()
         : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
        }
        private void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (object newItem in e.NewItems)
				{
					(newItem as INotifyPropertyChanged).PropertyChanged += Item_PropertyChanged;
				}
			}
			if (e.OldItems != null)
			{
				foreach (object oldItem in e.OldItems)
				{
					(oldItem as INotifyPropertyChanged).PropertyChanged -= Item_PropertyChanged;
				}
			}
		}

		private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
			OnCollectionChanged(a);

            this.ItemPropertyChanged?.Invoke(sender, e);
        }
	}
}
