// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.PostUpdateEventListener
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate.Event;
using System;

namespace my.domain.lib.core.Repository
{
    public class PostUpdateEventListener : IPostUpdateEventListener
    {
        public virtual event EventHandler<EntityEventArgs> EntityUpdated;

        void IPostUpdateEventListener.OnPostUpdate(PostUpdateEvent @event)
        {
            EventHandler<EntityEventArgs> entityUpdated = this.EntityUpdated;
            if (entityUpdated == null)
                return;
            entityUpdated(@event.Entity, new EntityEventArgs());
        }
    }
}
