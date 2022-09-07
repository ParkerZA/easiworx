// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.baseNHRepository`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;

namespace my.domain.lib.core.Repository
{
    public abstract class baseNHRepository<TEntity, TId> : baseNHReadOnlyRepository<TEntity, TId>, IRepository<TEntity, TId>, IReadOnlyRepository<TEntity, TId>, IDisposable, IRepositoryContext
    where TEntity : EntityTypedId<TId>
    {
        public void Commit()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
                this.Transaction = (ITransaction)null;
            }
            if (this.Session != null)
            {
                this.Session.Close();
                this.Session = (ISession)null;
            }
            if (this.SSession == null)
                return;
            this.SSession.Close();
            this.SSession = (IStatelessSession)null;
        }

        public void Rollback()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Rollback();
                this.Transaction = (ITransaction)null;
            }
            if (this.Session != null)
            {
                this.Session.Close();
                this.Session = (ISession)null;
            }
            if (this.SSession == null)
                return;
            this.SSession.Close();
            this.SSession = (IStatelessSession)null;
        }

        public void BeginTransaction()
        {
            if (this.SSession == null)
                this.SSession = baseNHReadOnlyRepository<TEntity, TId>.SessionFactory.OpenStatelessSession();
            if (this.Transaction != null)
                return;
            this.Transaction = this.SSession.BeginTransaction();
        }

        public void Add(TEntity entity)
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save((object)entity);
                            transaction.Commit();
                        }
                    }
                }
                else
                    this.SSession.Insert((object)entity);
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

        public void Remove(TId id)
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Delete((object)this.Get(id));
                            transaction.Commit();
                        }
                    }
                }
                else
                    this.SSession.Delete((object)this.Get(id));
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

        public void Update(TEntity entity)
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Update((object)entity);
                            transaction.Commit();
                        }
                    }
                }
                else
                    this.SSession.Update((object)entity);
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

        public int Execute(string commandText, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public object QueryValue(string commandText, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public List<Dictionary<string, string>> Query(
          string commandText,
          Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public object CreateCommand(string commandText, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public string GetStrValue(string commandText, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
