// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.IStore`1
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;

namespace my.domain.lib.core.Domain
{
    public interface IStore<TClass> : IStore<TClass, string>, IDisposable where TClass : class, IClass<string>
    {
    }
}
