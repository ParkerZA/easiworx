// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.baseGenericNHRepository
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public abstract class baseGenericNHRepository : baseGenericNHReadOnlyRepository, IGenericRepository, IGenericReadOnlyRepository, IDisposable, IRepositoryContext
    {
        public virtual event EventHandler<EntityEventArgs> PreInsertEvent;

        public virtual event EventHandler<EntityEventArgs> PostInsertEvent;

        public virtual event EventHandler<EntityEventArgs> PreUpdateEvent;

        public virtual event EventHandler<EntityEventArgs> PostUpdateEvent;

        public virtual void InvokePreInsertEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> preInsertEvent = this.PreInsertEvent;
            if (preInsertEvent == null)
                return;
            preInsertEvent(sender, e);
        }

        public virtual void InvokePostInsertEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> postInsertEvent = this.PostInsertEvent;
            if (postInsertEvent == null)
                return;
            postInsertEvent(sender, e);
        }

        public virtual void InvokePreUpdateEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> preUpdateEvent = this.PreUpdateEvent;
            if (preUpdateEvent == null)
                return;
            preUpdateEvent(sender, e);
        }

        public virtual void InvokePostUpdateEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> postUpdateEvent = this.PostUpdateEvent;
            if (postUpdateEvent == null)
                return;
            postUpdateEvent(sender, e);
        }

        public baseGenericNHRepository(IConnectionInfo connectionInfo)
          : base(connectionInfo)
        {
        }

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
                this.SSession = baseGenericNHReadOnlyRepository.SessionFactory.OpenStatelessSession();
            if (this.Transaction != null)
                return;
            this.Transaction = this.SSession.BeginTransaction();
        }

        public void Add<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            entity.UpdateBy = this.UserName;
                            entity.UpdateDate = DateTime.Now;
                            entity.CreateDate = DateTime.Now;
                            session.Save((object)entity);
                            transaction.Commit();
                            if ((object)entity == null)
                                return;
                            entity.InvokeEntitySaved((EntityEventArgs)null);
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

        public void Remove<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Delete((object)this.Get<TEntity, TId>(id));
                            transaction.Commit();
                        }
                    }
                }
                else
                    this.SSession.Delete((object)this.Get<TEntity, TId>(id));
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

        public void Update<TEntity, TId>(TEntity entity) where TEntity : EntityTypedId<TId>
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            if (session.Contains((object)entity))
                                session.Evict((object)entity);
                            entity.UpdateBy = this.UserName;
                            entity.UpdateDate = DateTime.Now;
                            session.Update((object)entity);
                            transaction.Commit();
                            if ((object)entity == null)
                                return;
                            entity.InvokeEntitySaved((EntityEventArgs)null);
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
           
            try
            {
                //if (this.Transaction == null)
                //{
                //    using (ISession session = this.OpenSession())
                //    {
                //        session.CreateSQLQuery(commandText);

                //        using (ITransaction transaction = session.BeginTransaction())
                //        {
                //            transaction.Commit();
                //        }
                //    }
                //}
                //else
                //{
                   var query = this.SSession.CreateSQLQuery(commandText);

                   query.ExecuteUpdate();

                //}

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message, ex.InnerException);
            }



            return 0;
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

        public virtual void DBMigrateUp(Assembly assembly, string dbNameType)
        {
        }

        public virtual void DBMigrateDown(Assembly assembly, long version, string dbNameType)
        {
        }
    }
}
