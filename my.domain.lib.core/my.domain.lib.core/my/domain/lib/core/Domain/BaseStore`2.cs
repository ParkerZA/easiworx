// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseStore`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseStore<TClass, TKey> : IQueryableStore<TClass, TKey>, IStore<TClass, TKey>, IDisposable
    where TClass : EntityTypedId<TKey>
    {
        private bool _disposed;

        public bool ShouldDisposeSession { get; set; }

        public ISession Context { get; private set; }

        public BaseStore(ISession context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (!context.IsConnected)
                context = context.SessionFactory.OpenSession();
            this.ShouldDisposeSession = true;
            this.Context = context;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.Context != null && this.ShouldDisposeSession)
                this.Context.Dispose();
            this._disposed = true;
            this.Context.Flush();
            this.Context = (ISession)null;
        }

        public IQueryable<TClass> Items
        {
            get
            {
                this.ThrowIfDisposed();
                return this.Context.Query<TClass>();
            }
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        public virtual Task CreateAsync(TClass o)
        {
            this.ThrowIfDisposed();
            if ((object)o == null)
                throw new ArgumentNullException("user");
            o.CreateDate = DateTime.Now;
            o.UpdateBy = "";
            this.Context.Save((object)o);
            this.Context.Flush();
            return (Task)Task.FromResult<int>(0);
        }

        public virtual Task DeleteAsync(TClass o)
        {
            if ((object)o == null)
                throw new ArgumentNullException("user");
            this.Context.Delete((object)o);
            this.Context.Flush();
            return (Task)Task.FromResult<int>(0);
        }

        public virtual Task<TClass> FindByIdAsync(TKey id)
        {
            this.ThrowIfDisposed();
            return Task.FromResult<TClass>(this.Context.Get<TClass>((object)id));
        }

        public virtual Task<TClass> FindByNameAsync(string name)
        {
            this.ThrowIfDisposed();
            return Task.FromResult<TClass>(this.Context.Query<TClass>().Where<TClass>((Expression<Func<TClass, bool>>)(u => u.Name.ToUpper() == name.ToUpper())).FirstOrDefault<TClass>());
        }

        public abstract Task<IList<TClass>> FindByKeyAsync(string key);

        public virtual Task<IList<TClass>> FindByStatusIdAsync(int statusId)
        {
            this.ThrowIfDisposed();
            return Task.FromResult<IList<TClass>>((IList<TClass>)this.Context.Query<TClass>().Where<TClass>((Expression<Func<TClass, bool>>)(u => u.Status == statusId.ToString())).ToList<TClass>());
        }

        public virtual Task UpdateAsync(TClass o)
        {
            this.ThrowIfDisposed();
            if ((object)o == null)
                throw new ArgumentNullException("user");
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = "";
            this.Context.Merge<TClass>(o);
            this.Context.Flush();
            return (Task)Task.FromResult<int>(0);
        }

        public virtual Task<TClass> GetUserAggregateAsync(Expression<Func<TClass, bool>> filter)
        {
            return Task.Run<TClass>((Func<TClass>)(() => this.Context.Query<TClass>().Where<TClass>(filter).ToFuture<TClass>().FirstOrDefault<TClass>()));
        }
    }
}
