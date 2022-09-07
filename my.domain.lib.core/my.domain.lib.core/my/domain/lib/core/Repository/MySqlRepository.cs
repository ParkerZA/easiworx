// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.MySqlRepository
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using my.domain.lib.core.Domain;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Data.Common;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public class MySqlRepository : baseGenericNHRepository
    {
        private string DbNameType = "mysql";

        public MySqlRepository(IConnectionInfo connectionInfo)
          : base(connectionInfo)
        {
            NHibernateExpressionHelper.RegisterMethods();
        }

        public override void Configure(bool NewConfig = false, bool UpdateConfig = false)
        {
            this.IsConfigured = false;
            this.IsInError = false;
            using (MySqlConnection mySqlConnection = new MySqlConnection(this.ConnectionInfo.ConnectionString(true)))
            {
                try
                {
                    ((DbConnection)mySqlConnection).Open();
                }
                catch (Exception ex)
                {
                    this.IsInError = true;
                    throw new ConnectionException(ex)
                    {
                        ExceptionMessage = string.Format("Cannot connect to the database requested. : {0}", (object)this.ConnectionInfo.ConnectionString(true))
                    };
                }
            }
            using (MySqlConnection mySqlConnection = new MySqlConnection(this.ConnectionInfo.ConnectionString(false)))
            {
                string str = string.Format("{0}", (object)this.ConnectionInfo.DbName);
                try
                {
                    if (str != "")
                    {
                        ((DbConnection)mySqlConnection).Open();
                        this.IsConfigured = true;
                    }
                }
                catch (Exception ex)
                {
                    this.IsInError = true;
                    throw new ConnectionException(ex)
                    {
                        ExceptionMessage = string.Format("Cannot connect to the database requested. Contact your database administrator to create the database : {0}", (object)this.ConnectionInfo.DbName)
                    };
                }
            }
            if (!this.IsConfigured)
                return;
            FluentConfiguration fluentConfiguration1 = Fluently.Configure();
            FluentConfiguration fluentConfiguration2;
            try
            {
                Assembly referencedAssembly = Assembly.GetAssembly(this.ConnectionInfo.AssemblyType);
                PreLoadEventListener PreLoadEventListener = new PreLoadEventListener();
                PreLoadEventListener.EntityLoading += new EventHandler<EntityEventArgs>(this.PreLoadEventListener_EntityLoaded);
                PostLoadEventListener PostLoadEventListener = new PostLoadEventListener();
                PostLoadEventListener.EntityLoaded += new EventHandler<EntityEventArgs>(this.PostLoadEventListener_EntityLoaded);
                PreInsertEventListener PreInsertEventListener = new PreInsertEventListener();
                PreInsertEventListener.EntityInserting += new EventHandler<EntityEventArgs>(this.PreInsertEventListener_EntityInserted);
                PreUpdateEventListener PreUpdateEventListener = new PreUpdateEventListener();
                PreUpdateEventListener.EntityUpdating += new EventHandler<EntityEventArgs>(this.PreUpdateEventListener_EntityLoaded);
                fluentConfiguration2 = fluentConfiguration1
                    .Database((IPersistenceConfigurer)MySQLConfiguration.Standard.ConnectionString(this.ConnectionInfo.ConnectionString(false))
                    .Dialect<MySQL5Dialect>().UseReflectionOptimizer())
                    .Mappings((Action<MappingConfiguration>)(m => m.AutoMappings.Add(this.CreateAutomappings(referencedAssembly, this.ConnectionInfo.MappingNamespace)))) //"Finx.Domain.Entities"
                    .ExposeConfiguration((Action<Configuration>)(s =>
                         {
                             s.SetListener(ListenerType.PreUpdate, (object)PreUpdateEventListener);
                             s.SetListener(ListenerType.PreInsert, (object)PreInsertEventListener);
                             s.SetListener(ListenerType.PreLoad, (object)PreLoadEventListener);
                             s.SetListener(ListenerType.PostLoad, (object)PostLoadEventListener);
                         }));
                baseGenericNHReadOnlyRepository.SessionFactory = fluentConfiguration2.BuildSessionFactory();
            }
            catch (MappingException ex)
            {
                this.IsInError = true;
                throw new SchemaException((Exception)ex)
                {
                    ExceptionMessage = string.Format("Error NHibernate Mapping : {0} {1}", (object)ex.Message, ex.InnerException == null ? (object)"" : (object)ex.InnerException.Message)
                };
            }
            catch (FluentConfigurationException ex)
            {
                this.IsInError = true;
                throw new ConnectionException((Exception)ex)
                {
                    ExceptionMessage = string.Format("Error Fluent NHibernate Configuration : {0} {1}", (object)ex.Message, ex.InnerException == null ? (object)"" : (object)ex.InnerException.Message)
                };
            }
            catch (Exception ex)
            {
                this.IsInError = true;
                throw ex;
            }
            if (UpdateConfig)
            {
                try
                {
                    fluentConfiguration2.ExposeConfiguration((Action<Configuration>)(cfg => new SchemaUpdate(cfg).Execute(true, true))).BuildConfiguration();
                }
                catch (MappingException ex)
                {
                    this.IsInError = true;
                    throw new SchemaException((Exception)ex)
                    {
                        ExceptionMessage = string.Format("Error NHibernate Mapping : {0} {1}", (object)ex.Message, ex.InnerException == null ? (object)"" : (object)ex.InnerException.Message)
                    };
                }
                catch (FluentConfigurationException ex)
                {
                    this.IsInError = true;
                    throw new ConnectionException((Exception)ex)
                    {
                        ExceptionMessage = string.Format("Error Fluent NHibernate Configuration : {0} {1}", (object)ex.Message, ex.InnerException == null ? (object)"" : (object)ex.InnerException.Message)
                    };
                }
                catch (Exception ex)
                {
                    this.IsInError = true;
                    throw ex;
                }
            }
            base.Configure(NewConfig, UpdateConfig);
        }

        public override void DBMigrateUp(Assembly assembly, string dbNameType = "")
        {
            new Migrator((IMigratorContext)new MigratorContext(Console.Out)
            {
                Database = (dbNameType == "" ? this.DbNameType : dbNameType),
                Connection = this.ConnectionInfo.ConnectionString(false),
                MigrationsAssembly = assembly
            }).MigrateUp();
            base.DBMigrateUp(assembly, dbNameType);
        }

        public override void DBMigrateDown(Assembly assembly, long version, string dbNameType = "")
        {
            new Migrator((IMigratorContext)new MigratorContext(Console.Out)
            {
                Database = (dbNameType == "" ? this.DbNameType : dbNameType),
                Connection = this.ConnectionInfo.ConnectionString(false),
                MigrationsAssembly = assembly
            }).MigrateDown(version);
            base.DBMigrateDown(assembly, version, dbNameType);
        }

        private void PreLoadEventListener_EntityLoaded(object sender, EntityEventArgs e)
        {
            this.InvokePreLoadEvent(sender, e);
        }

        private void PostLoadEventListener_EntityLoaded(object sender, EntityEventArgs e)
        {
            this.InvokePostLoadEvent(sender, e);
        }

        private void PreUpdateEventListener_EntityLoaded(object sender, EntityEventArgs e)
        {
            this.InvokePreUpdateEvent(sender, e);
        }

        private void PreInsertEventListener_EntityInserted(object sender, EntityEventArgs e)
        {
            this.InvokePreInsertEvent(sender, e);
        }
    }
}
