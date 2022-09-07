// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseGenericReadOnlyService`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseGenericReadOnlyService<TEntity, TId> where TEntity : EntityTypedId<TId>
    {
        private readonly IGenericReadOnlyRepository _repository;

        protected BaseGenericReadOnlyService(IGenericReadOnlyRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            if (!repository.IsConfigured)
                throw new ConnectionException((Exception)null);
            this._repository = repository;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this._repository.Count<TEntity, TId>(predicate);
        }

        public virtual bool Exists(TId id)
        {
            return this._repository.Contains<TEntity, TId>(id);
        }

        public virtual TEntity Get(TId id)
        {
            if (this._repository.Contains<TEntity, TId>(id))
                return this._repository.Get<TEntity, TId>(id);
            throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", (object)typeof(TEntity), (object)id));
        }

        public virtual IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> predicate)
        {
            return this._repository.List<TEntity, TId>(predicate, 0, 0, (Expression<Func<TEntity, object>>)null, true);
        }
    }
}
