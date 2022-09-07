using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Repository;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Tool.hbm2ddl;

namespace my.domain.lib.core.test.Repository
{
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
            //using (MySqlConnection mySqlConnection = new MySqlConnection(this.ConnectionInfo.ConnectionString(true)))
            //{
            //    try
            //    {
            //        ((DbConnection)mySqlConnection).Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        this.IsInError = true;
            //        throw new ConnectionException(ex)
            //        {
            //            ExceptionMessage = string.Format("Cannot connect to the database requested. : {0}", (object)this.ConnectionInfo.ConnectionString(true))
            //        };
            //    }
            //}
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
                    .Mappings((Action<MappingConfiguration>)
                        (m => {
                            m.FluentMappings.AddFromAssemblyOf<my.domain.lib.core.test.Models.Views.ClientDetailsView>(); //ClassMap mappings   
                            //m.AutoMappings.Add(this.CreateAutomappings(referencedAssembly, this.ConnectionInfo.MappingNamespace)); //Automap entities in namespace                                                     
                        }
                        ))
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
