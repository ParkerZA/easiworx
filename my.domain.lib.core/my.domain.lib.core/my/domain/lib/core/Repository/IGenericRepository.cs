// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IGenericRepository
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public interface IGenericRepository : IGenericReadOnlyRepository, IDisposable, IRepositoryContext
    {
        event EventHandler<EntityEventArgs> PreInsertEvent;

        event EventHandler<EntityEventArgs> PostInsertEvent;

        event EventHandler<EntityEventArgs> PreUpdateEvent;

        event EventHandler<EntityEventArgs> PostUpdateEvent;

        void Add<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>;

        void Remove<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>;

        void Update<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>;

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

        void DBMigrateUp(Assembly assembly, string dbNameType = "");

        void DBMigrateDown(Assembly assembly, long version, string dbNameType = "");
    }
}
