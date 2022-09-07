// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.PreInsertEventListener
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Domain;
using NHibernate.Event;
using System;

namespace my.domain.lib.core.Repository
{
    public class PreInsertEventListener : IPreInsertEventListener
    {
        private readonly DatabaseCryptography _crypto = new DatabaseCryptography();
        private readonly DataValConverter _valConverter = new DataValConverter();

        public virtual event EventHandler<EntityEventArgs> EntityInserting;

        public bool OnPreInsert(PreInsertEvent @event)
        {
            EventHandler<EntityEventArgs> entityInserting = this.EntityInserting;
            if (entityInserting != null)
                entityInserting(@event.Entity, new EntityEventArgs());
            this._valConverter.NullToMinVal(@event.Entity, @event.State, @event.Persister.PropertyNames, @event.Persister.PropertyTypes);
            this._crypto.EncryptProperties(@event.Entity, @event.Persister.PropertyNames, @event.State);
            return false;
        }
    }
}
