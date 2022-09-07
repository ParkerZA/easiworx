// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.ConnectionException
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;

namespace my.domain.lib.core.Repository
{
    public class ConnectionException : Exception
    {
        public ConnectionException(Exception x)
        {
            this.BaseException = x;
        }

        public Exception BaseException { get; internal set; }

        public IConnectionInfo ConnectionInfo { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
