// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseObject
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace my.domain.lib.core.Domain
{
    [DataContract(Namespace = "")]
    [Serializable]
    public abstract class BaseObject : AuditableObject
    {
        private const int HashMultiplier = 31;
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<PropertyInfo>> signaturePropertiesDictionary;

        public override bool Equals(object obj)
        {
            BaseObject compareTo = obj as BaseObject;
            if (this == compareTo)
                return true;
            return compareTo != null && this.GetType().Equals(compareTo.GetTypeUnproxied()) && this.HasSameObjectSignatureAs(compareTo);
        }

        public override int GetHashCode()
        {
            IEnumerable<PropertyInfo> signatureProperties = this.GetSignatureProperties();
            int hashCode = this.GetType().GetHashCode();
            int num = signatureProperties.Select<PropertyInfo, object>((Func<PropertyInfo, object>)(property => property.GetValue((object)this, (object[])null))).Where<object>((Func<object, bool>)(value => value != null)).Aggregate<object, int>(hashCode, (Func<int, object, int>)((current, value) => current * 31 ^ value.GetHashCode()));
            if (signatureProperties.Any<PropertyInfo>())
                return num;
            return base.GetHashCode();
        }

        public virtual IEnumerable<PropertyInfo> GetSignatureProperties()
        {
            if (BaseObject.signaturePropertiesDictionary == null)
                BaseObject.signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
            IEnumerable<PropertyInfo> propertyInfos;
            if (BaseObject.signaturePropertiesDictionary.TryGetValue(this.GetType(), out propertyInfos))
                return propertyInfos;
            return BaseObject.signaturePropertiesDictionary[this.GetType()] = this.GetTypeSpecificSignatureProperties();
        }

        public virtual bool HasSameObjectSignatureAs(BaseObject compareTo)
        {
            //IEnumerable<PropertyInfo> signatureProperties = this.GetSignatureProperties();
            //if (signatureProperties.Select(property => new
            //{
            //  property = property,
            //  valueOfThisObject = property.GetValue((object) this, (object[]) null)
            //}).Select(_param1 => new
            //{
            //  \u003C\u003Eh__TransparentIdentifier0 = _param1,
            //  valueToCompareTo = _param1.property.GetValue((object) compareTo, (object[]) null)
            //}).Where(_param1 =>
            //{
            //  if (_param1.\u003C\u003Eh__TransparentIdentifier0.valueOfThisObject == null)
            //    return _param1.valueToCompareTo != null;
            //  return true;
            //}).Where(_param1 =>
            //{
            //  if (!(_param1.\u003C\u003Eh__TransparentIdentifier0.valueOfThisObject == null ^ _param1.valueToCompareTo == null))
            //    return !_param1.\u003C\u003Eh__TransparentIdentifier0.valueOfThisObject.Equals(_param1.valueToCompareTo);
            //  return true;
            //}).Select(_param1 => _param1.\u003C\u003Eh__TransparentIdentifier0.valueOfThisObject).Any<object>())
            //  return false;
            //return signatureProperties.Any<PropertyInfo>() || base.Equals((object) compareTo);

            return true;
        }

        protected abstract IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties();

        protected virtual Type GetTypeUnproxied()
        {
            return this.GetType();
        }
    }
}
