// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.PersistentGenericObservableBag`1
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate.Collection.Generic;
using NHibernate.Engine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace my.domain.lib.core.Repository
{
    public class PersistentGenericObservableBag<T> : PersistentGenericBag<T>, IObservableList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged
    {
        public PersistentGenericObservableBag(ISessionImplementor session, ObservableList<T> list)
          : base(session, (IEnumerable<T>)list)
        {
        }

        public PersistentGenericObservableBag(ISessionImplementor session)
          : base(session)
        {
        }

        public new void Add(T item)
        {
            base.Add(item);
            this.OnItemAdded(item);
        }

        public new void Clear()
        {
            base.Clear();
            this.OnCollectionReset();
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            this.OnItemInserted(index, item);
        }

        public new bool Remove(T item)
        {
            int index = this.IndexOf(item);
            bool flag = base.Remove(item);
            this.OnItemRemoved(item, index);
            return flag;
        }

        public new void RemoveAt(int index)
        {
            T obj = this[index];
            base.RemoveAt(index);
            this.OnItemRemoved(obj, index);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnItemAdded(T item)
        {
            if (this.CollectionChanged == null)
                return;
            this.CollectionChanged((object)this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object)item, this.Count - 1));
        }

        protected void OnCollectionReset()
        {
            if (this.CollectionChanged == null)
                return;
            this.CollectionChanged((object)this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected void OnItemInserted(int index, T item)
        {
            if (this.CollectionChanged == null)
                return;
            this.CollectionChanged((object)this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object)item, index));
        }

        protected void OnItemRemoved(T item, int index)
        {
            if (this.CollectionChanged == null)
                return;
            this.CollectionChanged((object)this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object)item, index));
        }
    }
}
