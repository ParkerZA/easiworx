// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.Migrator
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    //public class Migrator
    //{
    //    public Migrator(IMigratorContext runnerContext)
    //    {
    //        this.RunnerContext = new Migrator.InProcRunnerContext(runnerContext);
    //        this.MigrationsAssembly = runnerContext.MigrationsAssembly;
    //    }

    //    public void MigrateUp()
    //    {
    //        this.Initialize();
    //        this.Runner.MigrateUp();
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    public void MigrateUp(long version)
    //    {
    //        this.Initialize();
    //        this.Runner.MigrateUp(version);
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    public void Rollback(int steps)
    //    {
    //        this.Initialize();
    //        if (steps <= 0)
    //            steps = 1;
    //        this.Runner.Rollback(steps);
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    public void RollbackToVersion(long version)
    //    {
    //        this.Initialize();
    //        this.Runner.RollbackToVersion(version);
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    public void RollbackAll()
    //    {
    //        this.Initialize();
    //        this.Runner.RollbackToVersion(0L);
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    public void MigrateDown(long version)
    //    {
    //        this.Initialize();
    //        this.Runner.MigrateDown(version);
    //        this.RunnerContext.Announcer.Say("FluentMigrator Task Completed.");
    //    }

    //    private void Initialize()
    //    {
    //        this.Runner = new MigrationRunner(this.MigrationsAssembly, this.RunnerContext, this.InitializeProcessor(this.MigrationsAssembly.Location));
        
    //    }

    //    private IMigrationProcessor InitializeProcessor(string assemblyLocation)
    //    {
    //        ConnectionStringManager connectionStringManager = new ConnectionStringManager((INetConfigManager)new NetConfigManager(), this.RunnerContext.Announcer, this.RunnerContext.Connection, this.RunnerContext.ConnectionStringConfigPath, assemblyLocation, this.RunnerContext.Database);
    //        connectionStringManager.LoadConnectionString();
    //        if (this.RunnerContext.Timeout == 0)
    //            this.RunnerContext.Timeout = 30;
    //        return new MigrationProcessorFactoryProvider().GetFactory(this.RunnerContext.Database).Create(connectionStringManager.ConnectionString, this.RunnerContext.Announcer, (IMigrationProcessorOptions)new ProcessorOptions()
    //        {
    //            PreviewOnly = this.RunnerContext.PreviewOnly,
    //            Timeout = this.RunnerContext.Timeout
    //        });
    //    }

    //    private IMigrationRunner Runner { get; set; }

    //    private IRunnerContext RunnerContext { get; set; }

    //    private Assembly MigrationsAssembly { get; set; }

    //    private class InProcRunnerContext : FluentMigrator.Runner.Initialization.RunnerContext
    //    {
    //        public InProcRunnerContext(IMigratorContext migratorContext)
    //          : base(migratorContext.Announcer)
    //        {
    //            this.Timeout = migratorContext.Timeout;
    //            this.Connection = migratorContext.Connection;
    //            this.Database = migratorContext.Database;
    //            this.PreviewOnly = migratorContext.PreviewOnly;
    //        }
    //    }
    //}


    public class Migrator
    {
        private IMigratorContext MigratorContext { get; set; }

        private IServiceProvider serviceProvider;

        public Migrator(IMigratorContext context)
        {
            MigratorContext = context;

            Initialize();
        }

        private void Initialize()
        {
            serviceProvider = CreateServices();
        }

        public void MigrateUp()
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            }
        }

        public void MigrateUp(long version)
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp(version);
            }
        }

        public void Rollback(int steps)
        {
            if (steps <= 0)
                steps = 1;
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.Rollback(steps);
            }
        }

        public void RollbackToVersion(long version)
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.RollbackToVersion(version);
            }
        }

        public void RollbackAll()
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.RollbackToVersion(0L);
            }
        }

        public void MigrateDown(long version)
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateDown(version);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                //.Configure<AssemblySourceOptions>(x => x.AssemblyNames = new[] { MigratorContext.MigrationsAssembly.GetName().Name })
                .ConfigureRunner(rb => rb
                    // Add MySql Processsor support to FluentMigrator
                    .AddMySql5()
                    // Set the connection string
                    .WithGlobalConnectionString(MigratorContext.Connection)
                    // Define the assembly containing the migrations and any Embedded Resource files
                    .ScanIn(MigratorContext.MigrationsAssembly).For.Migrations().For.EmbeddedResources())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())                
                // Build the service provider
                .BuildServiceProvider(false);
        }
    }
}
