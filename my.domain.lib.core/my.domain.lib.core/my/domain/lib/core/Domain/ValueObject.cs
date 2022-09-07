// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.ValueObject
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace my.domain.lib.core.Domain
{
    [Serializable]
    public abstract class ValueObject : BaseObject
    {
        public static bool operator ==(ValueObject valueObject1, ValueObject valueObject2)
        {
            if ((object)valueObject1 == null)
                return (object)valueObject2 == null;
            return valueObject1.Equals((object)valueObject2);
        }

        public static bool operator !=(ValueObject valueObject1, ValueObject valueObject2)
        {
            return !(valueObject1 == valueObject2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            IEnumerable<PropertyInfo> source = ((IEnumerable<PropertyInfo>)this.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => Attribute.IsDefined((MemberInfo)p, typeof(DomainSignatureAttribute), true)));
            string message = "Properties were found within " + (object)this.GetType() + " having the\r\n                [DomainSignature] attribute. The domain signature of a value object includes all\r\n                of the properties of the object by convention; consequently, adding [DomainSignature]\r\n                to the properties of a value object's properties is misleading and should be removed. \r\n                Alternatively, you can inherit from Entity if that fits your needs better.";
            if (source.Any<PropertyInfo>())
                throw new InvalidOperationException(message);
            return (IEnumerable<PropertyInfo>)this.GetType().GetProperties();
        }
    }
}
