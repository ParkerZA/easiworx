// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.EntityTypedId`1
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace my.domain.lib.core.Domain
{
    [DataContract(Namespace = "")]
    [Serializable]
    public abstract class EntityTypedId<TId> : ValidatableObject, IEntityTypedId<TId>, IClass<TId>, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private const int HashMultiplier = 31;
        private int? cachedHashcode;
        private TId _id;
        private bool _isModified;
        private string _name;

        protected EntityTypedId()
        {
        }

        protected EntityTypedId(TId id, string name)
        {
            this._id = id;
            this._name = name;
        }

        [Key]
        [XmlIgnore]
        [DataMember]
        public virtual TId Id
        {
            get
            {
                return this._id;
            }
            set
            {
                if (object.Equals((object)this._id, (object)value))
                    return;
                this._id = value;
            }
        }

        [DataMember]
        [XmlIgnore]
        [IgnoreAutoMap]
        public virtual string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (object.Equals((object)this._name, (object)value))
                    return;
                this.InvokePropertyChanging(nameof(Name));
                this._name = value;
                this.InvokePropertyChanged(nameof(Name));
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        [IgnoreAutoMap]
        public virtual bool IsModified
        {
            get
            {
                return this._isModified;
            }
        }

        public virtual void SetModified(bool modified)
        {
            if (this._isModified == modified)
                return;
            this._isModified = modified;
        }

        public override bool Equals(object obj)
        {
            EntityTypedId<TId> compareTo = obj as EntityTypedId<TId>;
            if (this == compareTo)
                return true;
            if (compareTo == null || !this.GetType().Equals(compareTo.GetTypeUnproxied()))
                return false;
            if (this.HasSameNonDefaultIdAs(compareTo))
                return true;
            return this._IsTransient() && compareTo._IsTransient() && this.HasSameObjectSignatureAs((BaseObject)compareTo);
        }

        public override int GetHashCode()
        {
            if (this.cachedHashcode.HasValue)
                return this.cachedHashcode.Value;
            this.cachedHashcode = !this._IsTransient() ? new int?(this.GetType().GetHashCode() * 31 ^ this.Id.GetHashCode()) : new int?(base.GetHashCode());
            return this.cachedHashcode.Value;
        }

        public virtual bool _IsTransient()
        {
            return (object)this.Id == null || this.Id.Equals((object)default(TId));
        }

        protected override IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            return ((IEnumerable<PropertyInfo>)this.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => Attribute.IsDefined((MemberInfo)p, typeof(DomainSignatureAttribute), true)));
        }

        private bool HasSameNonDefaultIdAs(EntityTypedId<TId> compareTo)
        {
            return !this._IsTransient() && !compareTo._IsTransient() && this.Id.Equals((object)compareTo.Id);
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual void InvokePropertyChanged(string propertyName)
        {
            this.SetModified(true);
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void InvokePropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
            if (propertyChanging == null)
                return;
            propertyChanging((object)this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual event EventHandler<EntityEventArgs> EntityLoaded;

        public virtual event EventHandler<EntityEventArgs> EntityValidated;

        public virtual event EventHandler<EntityEventArgs> EntitySaved;

        public virtual void InvokeEntityLoaded(EntityEventArgs e = null)
        {
            EventHandler<EntityEventArgs> entityLoaded = this.EntityLoaded;
            if (entityLoaded == null)
                return;
            entityLoaded((object)this, e);
        }

        public virtual void InvokeEntityValidated(EntityEventArgs e = null)
        {
            EventHandler<EntityEventArgs> entityValidated = this.EntityValidated;
            if (entityValidated == null)
                return;
            entityValidated((object)this, e);
        }

        public virtual void InvokeEntitySaved(EntityEventArgs e = null)
        {
            EventHandler<EntityEventArgs> entitySaved = this.EntitySaved;
            if (entitySaved == null)
                return;
            entitySaved((object)this, e);
        }
    }
}
