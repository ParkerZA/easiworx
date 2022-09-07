// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.baseNHReadOnlyRepository`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Data;

namespace my.domain.lib.core.Repository
{
    public abstract class baseNHReadOnlyRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId>, IDisposable, IRepositoryContext
    where TEntity : EntityTypedId<TId>
    {
        protected Dictionary<TId, TEntity> _entities = new Dictionary<TId, TEntity>();
        private bool disposedValue = false;
        internal ISession Session;
        internal IStatelessSession SSession;
        internal static ISessionFactory _sessionFactory;
        internal ITransaction Transaction;

        public static ISessionFactory SessionFactory
        {
            get
            {
                return baseNHReadOnlyRepository<TEntity, TId>._sessionFactory;
            }
            set
            {
                baseNHReadOnlyRepository<TEntity, TId>._sessionFactory = value;
            }
        }

        internal ISession OpenSession()
        {
            return baseNHReadOnlyRepository<TEntity, TId>.SessionFactory.OpenSession((IInterceptor)new SqlStatementInterceptor());
        }

        public void CloseSession()
        {
            baseNHReadOnlyRepository<TEntity, TId>.SessionFactory.Close();
        }

        public bool Contains(TId id)
        {
            return this._entities.ContainsKey(id);
        }

        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (ISession session = this.OpenSession())
                {
                    using (session.BeginTransaction())
                    {
                        IQueryOver<TEntity, TEntity> queryOver = session.QueryOver<TEntity>();
                        if (predicate != null)
                            queryOver.Where(predicate);
                        return queryOver.RowCount();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<TEntity> List(
          System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
          int StartPos = 0,
          int PageSize = 0,
          System.Linq.Expressions.Expression<Func<TEntity, object>> OrderBy = null,
          bool Asc = true)
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (session.BeginTransaction())
                        {
                            IQueryOver<TEntity, TEntity> queryOver = session.QueryOver<TEntity>();
                            if (predicate != null)
                                queryOver.Where(predicate);
                            if (StartPos > 0)
                                queryOver.Skip(StartPos);
                            if (PageSize > 0)
                                queryOver.Take(PageSize);
                            if (OrderBy != null)
                            {
                                if (Asc)
                                    queryOver.OrderBy(OrderBy).Asc<TEntity, TEntity>();
                                else
                                    queryOver.OrderBy(OrderBy).Desc<TEntity, TEntity>();
                            }
                            return (IEnumerable<TEntity>)queryOver.List<TEntity>();
                        }
                    }
                }
                else
                {
                    IQueryOver<TEntity, TEntity> queryOver = this.SSession.QueryOver<TEntity>();
                    if (predicate != null)
                        queryOver.Where(predicate);
                    if (StartPos > 0)
                        queryOver.Skip(StartPos);
                    if (PageSize > 0)
                        queryOver.Take(PageSize);
                    if (OrderBy != null)
                    {
                        if (Asc)
                            queryOver.OrderBy(OrderBy).Asc<TEntity, TEntity>();
                        else
                            queryOver.OrderBy(OrderBy).Desc<TEntity, TEntity>();
                    }
                    return (IEnumerable<TEntity>)queryOver.List<TEntity>();
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message, ex.InnerException);
            }
        }

        public TEntity Get(TId id)
        {
            try
            {
                if (this.Transaction != null)
                    return this.SSession.Get<TEntity>((object)id);
                using (ISession session = this.OpenSession())
                {
                    using (session.BeginTransaction())
                        return session.Get<TEntity>((object)id);
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message, ex.InnerException);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue)
                return;
            if (!disposing)
                ;
            this.disposedValue = true;
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        public string UserName { get; set; }
    }
}
