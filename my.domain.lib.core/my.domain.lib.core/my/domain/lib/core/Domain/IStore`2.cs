// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.IStore`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my.domain.lib.core.Domain
{
    public interface IStore<TClass, in TKey> : IDisposable where TClass : class, IClass<TKey>
    {
        Task CreateAsync(TClass o);

        Task DeleteAsync(TClass o);

        Task<TClass> FindByIdAsync(TKey id);

        Task<TClass> FindByNameAsync(string name);

        Task<IList<TClass>> FindByKeyAsync(string key);

        Task<IList<TClass>> FindByStatusIdAsync(int statusId);

        Task UpdateAsync(TClass o);
    }
}
