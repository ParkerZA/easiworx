// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IGenericReadOnlyRepository
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace my.domain.lib.core.Repository
{
    public interface IGenericReadOnlyRepository : IDisposable, IRepositoryContext
    {
        event EventHandler<EntityEventArgs> PreLoadEvent;

        event EventHandler<EntityEventArgs> PostLoadEvent;

        IConnectionInfo ConnectionInfo { get; set; }

        void Configure(bool NewConfig = false, bool UpdateConfig = false);

        bool IsConfigured { get; set; }

        bool IsInError { get; set; }

        bool IsOutOfDate { get; set; }

        int Count<TEntity, TId>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityTypedId<TId>;

        bool Contains<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>;

        TEntity Get<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>;

        IEnumerable<TEntity> List<TEntity, TId>(
          Expression<Func<TEntity, bool>> predicate,
          int StartPos = 0,
          int PageSize = 0,
          Expression<Func<TEntity, object>> OrderBy = null,
          bool Asc = true)
          where TEntity : EntityTypedId<TId>;
    }
}
