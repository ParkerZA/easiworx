// Decompiled with JetBrains decompiler
// Type: om.corporate.shared.Serialization.NamespaceAttribute
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;

namespace my.domain.lib.core.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class NamespaceAttribute : Attribute
    {
        public NamespaceAttribute()
        {
        }

        public NamespaceAttribute(string prefix, string uri)
        {
            this.Prefix = prefix;
            this.Uri = uri;
        }

        public string Prefix { get; set; }

        public string Uri { get; set; }
    }
}
