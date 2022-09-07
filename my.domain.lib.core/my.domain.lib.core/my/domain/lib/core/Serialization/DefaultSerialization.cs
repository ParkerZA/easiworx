// Decompiled with JetBrains decompiler
// Type: om.corporate.shared.Serialization.DefaultSerialization
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace my.domain.lib.core.Serialization
{
    public class DefaultSerialization : ISerialization
    {
        public string Serialize<T>(T o)
        {
            string str;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject((Stream)memoryStream, (object)o);
                str = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return str;
        }

        public T DeSerialize<T>(Stream stream)
        {
            return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
        }
    }
}
