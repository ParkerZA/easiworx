// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.NHDataConfig
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using SharpArch.Domain.DomainModel;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public abstract class NHDataConfig
    {
        private bool _UpdateSchema = false;

        public string ConnectionString { get; set; }

        public string Namespaces { get; set; }

        public HbmMapping HbmMappingHelper { get; set; }

        public Assembly MappingsAssembly { get; set; }

        public string SessionFactoryKey { get; set; }

        private void SharpArchConfigure()
        {
            PreUpdateEventListener PreUpdateEventListener = new PreUpdateEventListener();
            PreUpdateEventListener.EntityUpdating += new EventHandler<EntityEventArgs>(this.PreUpdateEventListener_EntityUpdating);
            PreInsertEventListener PreInsertEventListener = new PreInsertEventListener();
            PreInsertEventListener.EntityInserting += new EventHandler<EntityEventArgs>(this.PreInsertEventListener_EntityInserting);
            PreLoadEventListener PreLoadEventListener = new PreLoadEventListener();
            PreLoadEventListener.EntityLoading += new EventHandler<EntityEventArgs>(this.PreLoadEventListener_EntityLoading);
            PostLoadEventListener PostLoadEventListener = new PostLoadEventListener();
            PostLoadEventListener.EntityLoaded += new EventHandler<EntityEventArgs>(this.PostLoadEventListener_EntityLoaded);
            Configuration configuration = Fluently.Configure().Database((IPersistenceConfigurer)MySQLConfiguration.Standard.ConnectionString(this.ConnectionString)).Mappings((Action<MappingConfiguration>)(x => x.AutoMappings.Add(this.CreateAutomappings(this.MappingsAssembly, this.Namespaces)))).ExposeConfiguration((Action<Configuration>)(cfg =>
         {
             cfg.SetProperty("current_session_context_class", "thread");
             if (this.HbmMappingHelper != null)
                 cfg.AddDeserializedMapping(this.HbmMappingHelper, (string)null);
             if (this._UpdateSchema)
                 new SchemaUpdate(cfg).Execute(false, true);
             cfg.SetListener(ListenerType.PreUpdate, (object)PreUpdateEventListener);
             cfg.SetListener(ListenerType.PreInsert, (object)PreInsertEventListener);
             cfg.SetListener(ListenerType.PreLoad, (object)PreLoadEventListener);
             cfg.SetListener(ListenerType.PostLoad, (object)PostLoadEventListener);
         })).BuildConfiguration();

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            NHibernateSession.AddConfiguration(this.SessionFactoryKey == null ? (string)NHibernateSession.DefaultFactoryKey : this.SessionFactoryKey, sessionFactory, configuration, (string)null);
        }

        public virtual void PostLoadEventListener_EntityLoaded(object sender, EntityEventArgs e)
        {
        }

        public virtual void PreUpdateEventListener_EntityUpdating(object sender, EntityEventArgs e)
        {
        }

        public virtual void PreLoadEventListener_EntityLoading(object sender, EntityEventArgs e)
        {
        }

        public virtual void PreInsertEventListener_EntityInserting(object sender, EntityEventArgs e)
        {
        }

        internal AutoPersistenceModel CreateAutomappings(
          Assembly assembly,
          string namespaces)
        {
            return AutoMap.Assembly(assembly).Where((Func<Type, bool>)(t => t.Namespace == namespaces)).OverrideAll((Action<IPropertyIgnorer>)(x => x.IgnoreProperties((Func<Member, bool>)(property =>
         {
             if (!property.Name.StartsWith("_") && property.CanWrite)
                 return (uint)property.MemberInfo.GetCustomAttributes(typeof(IgnoreAutoMapAttribute), false).Length > 0U;
             return true;
         })))).UseOverridesFromAssembly(assembly).Conventions.Add<NHDataConfig.CascadeConvention>().Conventions.Add<IPropertyConvention>(ConventionBuilder.Property.Always((Action<IPropertyInstance>)(instance =>
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
         }))).IgnoreBase<EntityTypedId<string>>().IgnoreBase<EntityTypedId<int>>().IgnoreBase<EntityWithTypedId<string>>().IgnoreBase<EntityWithTypedId<int>>();
        }

        private void DefineBaseClass(ConventionModelMapper mapper, Type[] baseEntityToIgnore)
        {
            if (baseEntityToIgnore == null)
                return;
            mapper.IsEntity((Func<Type, bool, bool>)((type, declared) =>
           {
               if (((IEnumerable<Type>)baseEntityToIgnore).Any<Type>((Func<Type, bool>)(x => x.IsAssignableFrom(type))) && !((IEnumerable<Type>)baseEntityToIgnore).Any<Type>((Func<Type, bool>)(x => x == type)))
                   return !type.IsInterface;
               return false;
           }));
            mapper.IsRootEntity((Func<Type, bool, bool>)((type, declared) => ((IEnumerable<Type>)baseEntityToIgnore).Any<Type>((Func<Type, bool>)(x => x == type.BaseType))));
        }

        private void BuildSchema(Configuration config, bool NewSchema = false)
        {
            string filename = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "membership_schema.sql");
            if (NewSchema)
                new SchemaExport(config).SetOutputFile(filename).Create(true, true);
            else
                new SchemaUpdate(config).Execute(true, true);
        }

        public void ConfigureData(bool UpdateSchema = false)
        {
            this._UpdateSchema = UpdateSchema;
            this.SharpArchConfigure();
        }

        private class CascadeConvention : IReferenceConvention, IConvention<IManyToOneInspector, IManyToOneInstance>, IConvention, IHasManyConvention, IConvention<IOneToManyCollectionInspector, IOneToManyCollectionInstance>, IHasManyToManyConvention, IConvention<IManyToManyCollectionInspector, IManyToManyCollectionInstance>
        {
            public void Apply(IManyToOneInstance instance)
            {
                instance.Cascade.All();
                instance.Not.LazyLoad(Laziness.False);
            }

            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.Cascade.All();
                instance.Cascade.AllDeleteOrphan();
                instance.Not.LazyLoad();
            }

            public void Apply(IManyToManyCollectionInstance instance)
            {
                instance.Cascade.All();
                instance.Cascade.AllDeleteOrphan();
                instance.Not.LazyLoad();
            }
        }
    }
}
