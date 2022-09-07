// Decompiled with JetBrains decompiler
// Type: om.corporate.shared.Serialization.JsonNetSerialization
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace my.domain.lib.core.Serialization
{
    public class JsonNetSerialization : ISerialization
    {
        public string Serialize<T>(T o)
        {
            JsonSerializerSettings serializerSettings1 = new JsonSerializerSettings();
            //serializerSettings1.set_DateFormatHandling((DateFormatHandling) 1);
            //serializerSettings1.set_ReferenceLoopHandling((ReferenceLoopHandling) 1);
            JsonSerializerSettings serializerSettings2 = serializerSettings1;
            return JsonConvert.SerializeObject((object)o, serializerSettings2);
        }

        public T DeSerialize<T>(Stream stream)
        {
            if (typeof(T).IsGenericType)
                return ((JToken)JsonConvert.DeserializeObject(new StreamReader(stream).ReadToEnd())).ToObject<T>();
            return JsonConvert.DeserializeObject<T>(new StreamReader(stream).ReadToEnd());
        }
    }
}
