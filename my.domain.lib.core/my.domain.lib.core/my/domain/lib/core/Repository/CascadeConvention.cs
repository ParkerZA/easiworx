// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.CascadeConvention
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace my.domain.lib.core.Repository
{
    public class CascadeConvention : IReferenceConvention, IConvention<IManyToOneInspector, IManyToOneInstance>, IConvention, IHasManyConvention, IConvention<IOneToManyCollectionInspector, IOneToManyCollectionInstance>, IHasManyToManyConvention, IConvention<IManyToManyCollectionInspector, IManyToManyCollectionInstance>
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
