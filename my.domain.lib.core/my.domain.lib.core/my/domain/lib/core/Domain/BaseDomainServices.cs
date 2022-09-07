// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseDomainServices
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseDomainServices : BaseDomainReadOnlyServices
    {
        private readonly IGenericRepository _repository;

        protected BaseDomainServices(IGenericRepository repository)
          : base((IGenericReadOnlyRepository)repository)
        {
            this._repository = repository;
        }

        public event EventHandler<EventArgs<object>> Added;

        public event EventHandler<CancelEventArgs<object>> Adding;

        public event EventHandler<EventArgs<object>> Deleted;

        public event EventHandler<CancelEventArgs<object>> Deleting;

        public event EventHandler<EventArgs<object>> Updated;

        public event EventHandler<CancelEventArgs<object>> Updating;

        public virtual void Add<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>
        {
            if (this._repository.Contains<TEntity, TId>(entity.Id))
                throw new ArgumentException(string.Format("{0} with id {1} already exists", (object)typeof(TEntity), (object)entity.Id));
            CancelEventArgs<object> e = new CancelEventArgs<object>((object)entity);
            this.OnAdding(e);
            if (e.Cancel)
                return;
            this._repository.Add<TEntity, TId>(entity);
            this.OnAdded((object)entity);
        }

        public virtual void Remove<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>
        {
            if (!this._repository.Contains<TEntity, TId>(id))
                throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", (object)typeof(TEntity), (object)id));
            CancelEventArgs<object> e = new CancelEventArgs<object>((object)id);
            this.OnDeleting(e);
            if (e.Cancel)
                return;
            this._repository.Remove<TEntity, TId>(id);
            this.OnDeleted((object)id);
        }

        public virtual void Update<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>
        {
            if (!this._repository.Contains<TEntity, TId>(entity.Id))
                throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", (object)typeof(TEntity), (object)entity.Id));
            CancelEventArgs<object> e = new CancelEventArgs<object>((object)entity);
            this.OnUpdating(e);
            if (e.Cancel)
                return;
            this._repository.Update<TEntity, TId>(entity);
            this.OnUpdated((object)entity);
        }

        protected virtual void OnAdded(object entity)
        {
            if (this.Added == null)
                return;
            this.Added((object)this, new EventArgs<object>(entity));
        }

        protected virtual void OnAdding(CancelEventArgs<object> e)
        {
            if (this.Adding == null)
                return;
            this.Adding((object)this, e);
        }

        protected virtual void OnDeleted(object entityId)
        {
            if (this.Deleted == null)
                return;
            this.Deleted((object)this, new EventArgs<object>(entityId));
        }

        protected virtual void OnDeleting(CancelEventArgs<object> e)
        {
            if (this.Deleting == null)
                return;
            this.Deleting((object)this, e);
        }

        protected virtual void OnUpdated(object entity)
        {
            if (this.Updated == null)
                return;
            this.Updated((object)this, new EventArgs<object>(entity));
        }

        protected virtual void OnUpdating(CancelEventArgs<object> e)
        {
            EventHandler<CancelEventArgs<object>> updating = this.Updating;
            if (updating == null)
                return;
            updating((object)this, e);
        }
    }
}
