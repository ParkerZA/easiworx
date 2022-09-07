using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace easiplan.domain
{
	public static class ObservableCollectionExt
	{
		public static void NotifyPropertyChanged<T>(this ObservableCollection<T> observableCollection, Action<T, PropertyChangedEventArgs> callBackAction) where T : INotifyPropertyChanged
		{
			observableCollection.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				if (args.NewItems != null)
				{
					foreach (T newItem in args.NewItems)
					{
						newItem.PropertyChanged += delegate(object obj, PropertyChangedEventArgs eventArgs)
						{
							callBackAction((T)obj, eventArgs);
						};
					}
				}
			};
		}
	}
}
