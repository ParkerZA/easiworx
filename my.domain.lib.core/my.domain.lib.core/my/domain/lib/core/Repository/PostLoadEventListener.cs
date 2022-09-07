// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.PostLoadEventListener
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate.Event;
using System;

namespace my.domain.lib.core.Repository
{
    public class PostLoadEventListener : IPostLoadEventListener
    {
        public virtual event EventHandler<EntityEventArgs> EntityLoaded;

        void IPostLoadEventListener.OnPostLoad(PostLoadEvent @event)
        {
            EventHandler<EntityEventArgs> entityLoaded = this.EntityLoaded;
            if (entityLoaded == null)
                return;
            entityLoaded(@event.Entity, new EntityEventArgs());
        }
    }
}
