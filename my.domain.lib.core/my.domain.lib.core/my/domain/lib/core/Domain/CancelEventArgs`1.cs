// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.CancelEventArgs`1
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.ComponentModel;

namespace my.domain.lib.core.Domain
{
    public class CancelEventArgs<T> : CancelEventArgs
    {
        private readonly T _item;

        public CancelEventArgs(T item)
        {
            this._item = item;
        }

        public T Item
        {
            get
            {
                return this._item;
            }
        }
    }
}
