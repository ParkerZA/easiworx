// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.PostInsertEventListener
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate.Event;
using System;

namespace my.domain.lib.core.Repository
{
    public class PostInsertEventListener : IPostInsertEventListener
    {
        public virtual event EventHandler<EntityEventArgs> EntityInserted;

        void IPostInsertEventListener.OnPostInsert(PostInsertEvent @event)
        {
            EventHandler<EntityEventArgs> entityInserted = this.EntityInserted;
            if (entityInserted == null)
                return;
            entityInserted(@event.Entity, new EntityEventArgs());
        }
    }
}
