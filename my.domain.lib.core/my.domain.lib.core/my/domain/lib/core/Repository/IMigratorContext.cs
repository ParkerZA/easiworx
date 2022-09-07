// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IMigratorContext
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentMigrator.Runner;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public interface IMigratorContext
    {
        Assembly MigrationsAssembly { get; set; }

        int Timeout { get; set; }

        string Connection { get; set; }

        string Database { get; set; }

        IAnnouncer Announcer { get; }

        bool PreviewOnly { get; set; }
    }
}
