// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseManager`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseManager<TClass, TKey> : IDisposable
    where TClass : class, IClass<TKey>
    where TKey : IEquatable<TKey>
    {
        private bool _disposed;
        private IValidator<TClass> _Validator;

        protected internal IStore<TClass, TKey> Store { get; set; }

        public BaseManager(IStore<TClass, TKey> store)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));
            this.Store = store;
        }

        public IValidator<TClass> Validator
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Validator;
            }
            set
            {
                this.ThrowIfDisposed();
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Validator = value;
            }
        }

        public virtual bool SupportsQueryableItems
        {
            get
            {
                this.ThrowIfDisposed();
                return this.Store is IQueryableStore<TClass, TKey>;
            }
        }

        public virtual IQueryable<TClass> Items
        {
            get
            {
                IQueryableStore<TClass, TKey> store = this.Store as IQueryableStore<TClass, TKey>;
                if (store == null)
                    throw new NotSupportedException(nameof(Items));
                return store.Items;
            }
        }

        public virtual async Task<DataResult> CreateAsync(TClass item)
        {
            this.ThrowIfDisposed();
            DataResult result = await this.Validator.ValidateForInsertAsync(item).ConfigureAwait(false);
            if (!result.Succeeded)
                return result;
            await this.Store.CreateAsync(item).ConfigureAwait(false);
            return DataResult.Success;
        }

        public virtual async Task<DataResult> UpdateAsync(TClass item)
        {
            this.ThrowIfDisposed();
            if ((object)item == null)
                throw new ArgumentNullException("user");
            DataResult result = await this.Validator.ValidateForUpdateAsync(item).ConfigureAwait(false);
            if (!result.Succeeded)
                return result;
            await this.Store.UpdateAsync(item).ConfigureAwait(false);
            return DataResult.Success;
        }

        public virtual async Task<DataResult> DeleteAsync(TClass item)
        {
            this.ThrowIfDisposed();
            DataResult result = await this.Validator.ValidateForDeleteAsync(item).ConfigureAwait(false);
            if (!result.Succeeded)
                return result;
            await this.Store.DeleteAsync(item).ConfigureAwait(false);
            return DataResult.Success;
        }

        public virtual Task<TClass> FindByIdAsync(TKey itemId)
        {
            this.ThrowIfDisposed();
            return this.Store.FindByIdAsync(itemId);
        }

        public virtual Task<TClass> FindByNameAsync(string itemName)
        {
            this.ThrowIfDisposed();
            if (itemName == null)
                throw new ArgumentNullException("Name");
            return this.Store.FindByNameAsync(itemName);
        }

        public virtual Task<IList<TClass>> FindByKeyAsync(string key)
        {
            this.ThrowIfDisposed();
            return this.Store.FindByKeyAsync(key);
        }

        public virtual Task<IList<TClass>> FindByStatusIdAsync(int status)
        {
            this.ThrowIfDisposed();
            return this.Store.FindByStatusIdAsync(status);
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this._disposed)
                return;
            this.Store.Dispose();
            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(false);
        }
    }
}
