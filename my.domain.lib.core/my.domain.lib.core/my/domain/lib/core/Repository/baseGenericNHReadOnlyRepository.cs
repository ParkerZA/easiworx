// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.baseGenericNHReadOnlyRepository
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public abstract class baseGenericNHReadOnlyRepository : IGenericReadOnlyRepository, IDisposable, IRepositoryContext
    {
        private bool disposedValue = false;
        internal ISession Session;
        internal IStatelessSession SSession;
        internal static ISessionFactory _sessionFactory;
        internal ITransaction Transaction;

        public virtual event EventHandler<EntityEventArgs> PreLoadEvent;

        public virtual event EventHandler<EntityEventArgs> PostLoadEvent;

        public virtual void InvokePreLoadEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> preLoadEvent = this.PreLoadEvent;
            if (preLoadEvent == null)
                return;
            preLoadEvent(sender, e);
        }

        public virtual void InvokePostLoadEvent(object sender, EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> postLoadEvent = this.PostLoadEvent;
            if (postLoadEvent == null)
                return;
            postLoadEvent(sender, e);
        }

        public IConnectionInfo ConnectionInfo { get; set; }

        public baseGenericNHReadOnlyRepository(IConnectionInfo connectionInfo)
        {
            this.ConnectionInfo = connectionInfo;
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return baseGenericNHReadOnlyRepository._sessionFactory;
            }
            set
            {
                baseGenericNHReadOnlyRepository._sessionFactory = value;
            }
        }

        internal ISession OpenSession()
        {
            return baseGenericNHReadOnlyRepository.SessionFactory.OpenSession((IInterceptor)new SqlStatementInterceptor());
        }

        public void CloseSession()
        {
            baseGenericNHReadOnlyRepository.SessionFactory.Close();
        }

        public bool Contains<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>
        {
            return (object)this.Get<TEntity, TId>(id) != null;
        }

        public int Count<TEntity, TId>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : EntityTypedId<TId>
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

        public IEnumerable<TEntity> List<TEntity, TId>(
          System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
          int StartPos = 0,
          int PageSize = 0,
          System.Linq.Expressions.Expression<Func<TEntity, object>> OrderBy = null,
          bool Asc = true)
          where TEntity : EntityTypedId<TId>
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
            catch (FluentConfigurationException ex)
            {
                throw new SchemaException((Exception)ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Get<TEntity, TId>(TId id) where TEntity : EntityTypedId<TId>
        {
            try
            {
                if (this.Transaction == null)
                {
                    using (ISession session = this.OpenSession())
                    {
                        using (session.BeginTransaction())
                        {
                            TEntity entity = session.Get<TEntity>((object)id);
                            if ((object)entity != null)
                                entity.InvokeEntityLoaded((EntityEventArgs)null);
                            return entity;
                        }
                    }
                }
                else
                {
                    TEntity entity = this.SSession.Get<TEntity>((object)id);
                    entity.InvokeEntityLoaded((EntityEventArgs)null);
                    return entity;
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DataException(nameof(Get), ex);
            }
        }

        public AutoPersistenceModel CreateAutomappings(
          Assembly assembly,
          string namespaces)
        {
            return AutoMap.Assembly(assembly).Where((Func<Type, bool>)(t => t.Namespace == namespaces)).OverrideAll((Action<IPropertyIgnorer>)(x => x.IgnoreProperties((Func<Member, bool>)(property =>
         {
             if (!property.Name.StartsWith("_") && property.CanWrite)
                 return (uint)property.MemberInfo.GetCustomAttributes(typeof(IgnoreAutoMapAttribute), false).Length > 0U;
             return true;
         })))).Conventions.Add<CascadeConvention>().Conventions.Add<IPropertyConvention>(ConventionBuilder.Property.Always((Action<IPropertyInstance>)(instance =>
     {
             if (instance.Property.PropertyType == typeof(byte[]))
                 instance.Length(30000000);
             if (instance.Property.PropertyType == typeof(Stream))
                 instance.CustomSqlType("varbinary(MAX)");
             object[] customAttributes = instance.Property.MemberInfo.GetCustomAttributes(typeof(NHMaxLengthAttr), false);
             if (((IEnumerable<object>)customAttributes).Count<object>() <= 0)
                 return;
             NHMaxLengthAttr nhMaxLengthAttr = (NHMaxLengthAttr)((IEnumerable<object>)customAttributes).FirstOrDefault<object>();
             instance.Length(nhMaxLengthAttr.Length);
         }))).IgnoreBase<EntityTypedId<int>>();
        }

        public virtual void Configure(bool NewConfig = false, bool UpdateConfig = false)
        {
            this.IsConfigured = true;
            this.IsInError = false;
        }

        public bool IsConfigured { get; set; }

        public string UserName { get; set; }

        public bool IsInError { get; set; }

        public bool IsOutOfDate { get; set; }

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
    }
}
