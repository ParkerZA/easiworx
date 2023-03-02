using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using MySqlConnector;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using easiplan.domain.Views;
using easiplan.domain.estate.Services;
using easiplan.domain;

namespace easiplan.app.Repository
{
    /// <summary>
    /// A repository for the Easiworx domain objects
    /// Inherits from base NHibernate repository
    /// </summary>
    public class EasiworxRepository : baseGenericNHRepository
    {
        private string DbNameType = "mysql";

        public EasiworxRepository(IConnectionInfo connectionInfo) : base(connectionInfo)
        {
            NHibernateExpressionHelper.RegisterMethods();
        }

        public override void Configure(bool NewConfig = false, bool UpdateConfig = false)
        {
            this.IsConfigured = false;
            this.IsInError = false;
            var connString = this.ConnectionInfo.ConnectionString(false) + "Connection Timeout=180;";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connString))
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

            FluentConfiguration fluentConfiguration;
            try
            {
                PreLoadEventListener PreLoadEventListener = new PreLoadEventListener();
                PreLoadEventListener.EntityLoading += new EventHandler<EntityEventArgs>(this.PreLoadEventListener_EntityLoaded);
                PostLoadEventListener PostLoadEventListener = new PostLoadEventListener();
                PostLoadEventListener.EntityLoaded += new EventHandler<EntityEventArgs>(this.PostLoadEventListener_EntityLoaded);
                PreInsertEventListener PreInsertEventListener = new PreInsertEventListener();
                PreInsertEventListener.EntityInserting += new EventHandler<EntityEventArgs>(this.PreInsertEventListener_EntityInserted);
                PreUpdateEventListener PreUpdateEventListener = new PreUpdateEventListener();
                PreUpdateEventListener.EntityUpdating += new EventHandler<EntityEventArgs>(this.PreUpdateEventListener_EntityLoaded);

                fluentConfiguration = Fluently.Configure() //fluentConfiguration1
                    .Database((IPersistenceConfigurer)MySQLConfiguration.Standard.ConnectionString(this.ConnectionInfo.ConnectionString(false))
                    .Dialect<MySQL5Dialect>().UseReflectionOptimizer())
                    .Mappings((Action<MappingConfiguration>)
                        (m =>
                        {
                            m.FluentMappings.AddFromAssemblyOf<ClientDetailsView>(); //ClassMap mappings   
                          //  m.FluentMappings.AddFromAssemblyOf<ClientRetirementPortfolio_View>(); //ClassMap mappings   
                            m.AutoMappings.Add(this.CreateAutomappings(Assembly.GetAssembly(typeof(DomainServices)), "easiplan.domain.Entities").Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Always())); //Automap entities in namespace
                            m.AutoMappings.Add(this.CreateAutomappings(Assembly.GetAssembly(typeof(EstateAnalysisService)), "easiplan.domain.estate.Entities")); //Automap entities in namespace 
                            
                        }
                        ))
                    
                    .ExposeConfiguration((Action<Configuration>)(s =>
                    {
                        s.SetProperty("command_timeout", "120");
                        s.SetProperty("query_timeout", "120");
                        s.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections,"after_transaction");
                        s.SetListener(ListenerType.PreUpdate, (object)PreUpdateEventListener);
                        s.SetListener(ListenerType.PreInsert, (object)PreInsertEventListener);
                        s.SetListener(ListenerType.PreLoad, (object)PreLoadEventListener);
                        s.SetListener(ListenerType.PostLoad, (object)PostLoadEventListener);
                    }));
                
                SessionFactory = fluentConfiguration.BuildSessionFactory();
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
                    fluentConfiguration.ExposeConfiguration((Action<Configuration>)(cfg => new SchemaUpdate(cfg).Execute(true, true))).BuildConfiguration();
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

        
    }
}
