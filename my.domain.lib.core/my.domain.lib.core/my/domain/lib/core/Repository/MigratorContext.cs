// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.MigratorContext
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using System.IO;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    public class MigratorContext : IMigratorContext
    {
        public MigratorContext(TextWriter announcerOutput)
        {
            TextWriterAnnouncer textWriterAnnouncer = new TextWriterAnnouncer(announcerOutput);
            textWriterAnnouncer.ShowElapsedTime = true;
            textWriterAnnouncer.ShowSql = true;
            this.Announcer = (IAnnouncer)textWriterAnnouncer;
        }

        public Assembly MigrationsAssembly { get; set; }

        public int Timeout { get; set; }

        public string Connection { get; set; }

        public string Database { get; set; }

        public IAnnouncer Announcer { get; private set; }

        public bool PreviewOnly { get; set; }
    }
}
