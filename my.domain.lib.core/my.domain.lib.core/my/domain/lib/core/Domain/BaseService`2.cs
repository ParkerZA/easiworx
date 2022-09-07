// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseService`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseService<TEntity, TId> : BaseReadOnlyService<TEntity, TId>
    where TEntity : EntityTypedId<TId>
    {
        private readonly IRepository<TEntity, TId> _repository;

        protected BaseService(IRepository<TEntity, TId> repository)
          : base((IReadOnlyRepository<TEntity, TId>)repository)
        {
            this._repository = repository;
        }

        public event EventHandler<EventArgs<TEntity>> Added;

        public event EventHandler<CancelEventArgs<TEntity>> Adding;

        public event EventHandler<EventArgs<object>> Deleted;

        public event EventHandler<CancelEventArgs<object>> Deleting;

        public event EventHandler<EventArgs<TEntity>> Updated;

        public event EventHandler<CancelEventArgs<TEntity>> Updating;

        public virtual void Add(TEntity entity)
        {
            if (this._repository.Contains(entity.Id))
                throw new ArgumentException(string.Format("{0} with id {1} already exists", (object)typeof(TEntity), (object)entity.Id));
            CancelEventArgs<TEntity> e = new CancelEventArgs<TEntity>(entity);
            this.OnAdding(e);
            if (e.Cancel)
                return;
            this._repository.Add(entity);
            this.OnAdded(entity);
        }

        public virtual void Remove(TId id)
        {
            if (!this._repository.Contains(id))
                throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", (object)typeof(TEntity), (object)id));
            CancelEventArgs<object> e = new CancelEventArgs<object>((object)id);
            this.OnDeleting(e);
            if (e.Cancel)
                return;
            this._repository.Remove(id);
            this.OnDeleted((object)id);
        }

        public virtual void Update(TEntity entity)
        {
            if (!this._repository.Contains(entity.Id))
                throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", (object)typeof(TEntity), (object)entity.Id));
            CancelEventArgs<TEntity> e = new CancelEventArgs<TEntity>(entity);
            this.OnUpdating(e);
            if (e.Cancel)
                return;
            this._repository.Update(entity);
            this.OnUpdated(entity);
        }

        protected virtual void OnAdded(TEntity entity)
        {
            if (this.Added == null)
                return;
            this.Added((object)this, new EventArgs<TEntity>(entity));
        }

        protected virtual void OnAdding(CancelEventArgs<TEntity> e)
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

        protected virtual void OnUpdated(TEntity entity)
        {
            if (this.Updated == null)
                return;
            this.Updated((object)this, new EventArgs<TEntity>(entity));
        }

        protected virtual void OnUpdating(CancelEventArgs<TEntity> e)
        {
            EventHandler<CancelEventArgs<TEntity>> updating = this.Updating;
            if (updating == null)
                return;
            updating((object)this, e);
        }
    }
}
