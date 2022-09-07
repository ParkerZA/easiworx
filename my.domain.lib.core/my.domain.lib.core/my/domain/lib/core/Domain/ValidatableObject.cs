// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.ValidatableObject
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace my.domain.lib.core.Domain
{
    [DataContract(Namespace = "")]
    [Serializable]
    public abstract class ValidatableObject : BaseObject
    {
        public virtual bool IsValid()
        {
            return this.ValidationResults().Count == 0;
        }

        public virtual ICollection<ValidationResult> ValidationResults()
        {
            List<ValidationResult> validationResultList = new List<ValidationResult>();
            Validator.TryValidateObject((object)this, new ValidationContext((object)this, (IServiceProvider)null, (IDictionary<object, object>)null), (ICollection<ValidationResult>)validationResultList, true);
            return (ICollection<ValidationResult>)validationResultList;
        }
    }
}
