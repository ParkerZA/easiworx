// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IRepository`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;

namespace my.domain.lib.core.Repository
{
    public interface IRepository<TEntity, in TId> : IReadOnlyRepository<TEntity, TId>, IDisposable, IRepositoryContext
    where TEntity : IEntityTypedId<TId>
    {
        void Add(TEntity entity);

        void Remove(TId id);

        void Update(TEntity entity);

        void Commit();

        void Rollback();

        void BeginTransaction();

        int Execute(string commandText, Dictionary<string, object> parameters);

        object QueryValue(string commandText, Dictionary<string, object> parameters);

        List<Dictionary<string, string>> Query(
          string commandText,
          Dictionary<string, object> parameters);

        object CreateCommand(string commandText, Dictionary<string, object> parameters);

        string GetStrValue(string commandText, Dictionary<string, object> parameters);
    }
}
