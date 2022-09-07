// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.AutomappingConfiguration
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentNHibernate;
using FluentNHibernate.Automapping;
using my.domain.lib.core.Attributes;
using System;

namespace my.domain.lib.core.Repository
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private string _Namespace = string.Empty;

        public AutomappingConfiguration(string Namespace)
        {
            this._Namespace = Namespace;
        }

        public override bool ShouldMap(Type type)
        {
            return type.Namespace == this._Namespace;
        }

        public override bool ShouldMap(Member member)
        {
            if ((uint)member.MemberInfo.GetCustomAttributes(typeof(IgnoreAutoMapAttribute), true).Length > 0U)
                return false;
            return base.ShouldMap(member);
        }

        public override bool IsComponent(Type type)
        {
            return type == typeof(object);
        }
    }
}
