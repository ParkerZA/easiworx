// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.IConnectionInfo
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;

namespace my.domain.lib.core.Repository
{
    public interface IConnectionInfo
    {
        string ConnectionType { get; set; }

        string ConnectionString(bool CreateDb = false);

        void EnsureConnectionOpen();

        void EnsureConnectionClosed();

        string DbName { get; set; }

        string DbSchema { get; set; }

        string ServerName { get; set; }

        string PortNumber { get; set; }

        string UserName { get; set; }

        string UserPwd { get; set; }

        bool CreateNewSchema { get; set; }

        bool UpdateSchema { get; set; }

        Type AssemblyType { get; set; }

        string MappingNamespace { get; set; }
    }
}
