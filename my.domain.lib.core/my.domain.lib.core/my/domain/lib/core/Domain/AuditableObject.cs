// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.AuditableObject
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Runtime.Serialization;

namespace my.domain.lib.core.Domain
{
    [DataContract(Namespace = "")]
    public abstract class AuditableObject
    {
        public virtual string Status { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime UpdateDate { get; set; }

        public virtual string UpdateBy { get; set; }
    }
}
