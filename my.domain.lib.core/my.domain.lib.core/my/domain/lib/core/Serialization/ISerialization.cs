// Decompiled with JetBrains decompiler
// Type: om.corporate.shared.Serialization.ISerialization
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.IO;

namespace my.domain.lib.core.Serialization
{
    public interface ISerialization
    {
        string Serialize<T>(T o);

        T DeSerialize<T>(Stream stream);
    }
}
