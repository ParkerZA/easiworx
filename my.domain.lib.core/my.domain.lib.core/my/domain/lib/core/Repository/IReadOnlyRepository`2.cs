// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IReadOnlyRepository`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace my.domain.lib.core.Repository
{
    public interface IReadOnlyRepository<TEntity, in TId> : IDisposable, IRepositoryContext
    where TEntity : IEntityTypedId<TId>
    {
        int Count(Expression<Func<TEntity, bool>> predicate);

        bool Contains(TId id);

        TEntity Get(TId id);

        IEnumerable<TEntity> List(
          Expression<Func<TEntity, bool>> predicate,
          int StartPos = 0,
          int PageSize = 0,
          Expression<Func<TEntity, object>> OrderBy = null,
          bool Asc = true);
    }
}
