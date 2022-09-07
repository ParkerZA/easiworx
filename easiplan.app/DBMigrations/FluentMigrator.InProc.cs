
using System.IO;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator;

namespace Finx.App.DBMigrations
{
    public class MigratorContext : IMigratorContext
    {
        public MigratorContext(TextWriter announcerOutput)
        {
            Announcer = new TextWriterAnnouncer(announcerOutput)
            {
                ShowElapsedTime = true,
                ShowSql = true
            };
        }

        public Assembly MigrationsAssembly { get; set; }
        public int Timeout { get; set; }
        public string Connection { get; set; }
        public string Database { get; set; }
        public IAnnouncer Announcer { get; private set; }
        public bool PreviewOnly { get; set; }
    }

    public class Migrator
    {
        private class InProcRunnerContext : RunnerContext
        {
            public InProcRunnerContext(IMigratorContext migratorContext) : base(migratorContext.Announcer)
            {
                Timeout = migratorContext.Timeout;
                Connection = migratorContext.Connection;
                Database = migratorContext.Database;
                PreviewOnly = migratorContext.PreviewOnly;
            }
        }

        public Migrator(IMigratorContext runnerContext)
        {
            RunnerContext = new InProcRunnerContext(runnerContext);
            MigrationsAssembly = runnerContext.MigrationsAssembly;
        }

        public void MigrateUp()
        {
            Initialize();
            Runner.MigrateUp();
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        public void MigrateUp(long version)
        {
            Initialize();
            Runner.MigrateUp(version);
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        public void Rollback(int steps)
        {
            Initialize();
            if (steps <= 0)
            {
                steps = 1;
            }
            Runner.Rollback(steps);
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        public void RollbackToVersion(long version)
        {
            Initialize();
            Runner.RollbackToVersion(version);
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        public void RollbackAll()
        {
            Initialize();
            Runner.RollbackToVersion(0);
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        public void MigrateDown(long version)
        {
            Initialize();
            Runner.MigrateDown(version);
            RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
        }

        private void Initialize()
        {
            var processor = InitializeProcessor(MigrationsAssembly.Location);
            Runner = new MigrationRunner(MigrationsAssembly, RunnerContext, processor);
        }

        private IMigrationProcessor InitializeProcessor(string assemblyLocation)
        {
            var manager = new ConnectionStringManager(new NetConfigManager(),RunnerContext.Announcer, RunnerContext.Connection, RunnerContext.ConnectionStringConfigPath, assemblyLocation, RunnerContext.Database);

            manager.LoadConnectionString();

            if (RunnerContext.Timeout == 0)
            {
                RunnerContext.Timeout = 30; // Set default timeout for command
            }

            MigrationProcessorFactoryProvider factory = new MigrationProcessorFactoryProvider();

            var processorFactory = factory.GetFactory(RunnerContext.Database);
            var processor = processorFactory.Create(manager.ConnectionString, RunnerContext.Announcer, new ProcessorOptions
            {
                PreviewOnly = RunnerContext.PreviewOnly,
                //Timeout = RunnerContext.Timeout
            });

            return processor;
        }

        private IMigrationRunner Runner { get; set; }
        private IRunnerContext RunnerContext { get; set; }
        private Assembly MigrationsAssembly { get; set; }
    }
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
