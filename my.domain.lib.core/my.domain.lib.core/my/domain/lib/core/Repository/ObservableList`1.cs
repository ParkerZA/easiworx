// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.ObservableList`1
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate.Collection;
using NHibernate.Engine;
using NHibernate.Persister.Collection;
using NHibernate.UserTypes;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace my.domain.lib.core.Repository
{
    public class ObservableList<T> : List<T>, IObservableList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged, IUserCollectionType
    {
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

        public bool Contains(object collection, object entity)
        {
            return ((ICollection<T>)collection).Contains((T)entity);
        }

        public IEnumerable GetElements(object collection)
        {
            return (IEnumerable)collection;
        }

        public object IndexOf(object collection, object entity)
        {
            return (object)((IList<T>)collection).IndexOf((T)entity);
        }

        public object Instantiate()
        {
            return (object)new ObservableList<T>();
        }

        public IPersistentCollection Instantiate(
          ISessionImplementor session,
          ICollectionPersister persister)
        {
            return (IPersistentCollection)new PersistentGenericObservableBag<T>(session);
        }

        public object ReplaceElements(
          object original,
          object target,
          ICollectionPersister persister,
          object owner,
          IDictionary copyCache,
          ISessionImplementor session)
        {
            IObservableList<T> observableList = (IObservableList<T>)target;
            observableList.Clear();
            foreach (object obj in (IEnumerable)original)
                observableList.Add((T)obj);
            this.OnCollectionReset();
            return (object)observableList;
        }

        public IPersistentCollection Wrap(
          ISessionImplementor session,
          object collection)
        {
            return (IPersistentCollection)new PersistentGenericObservableBag<T>(session, (ObservableList<T>)collection);
        }

        public object Instantiate(int anticipatedSize)
        {
            return (object)new ObservableList<T>();
        }
    }
}
