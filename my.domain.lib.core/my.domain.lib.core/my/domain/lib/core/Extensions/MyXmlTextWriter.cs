// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Extensions.MyXmlTextWriter
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.IO;
using System.Text;
using System.Xml;

namespace my.domain.lib.core.Extensions
{
    public class MyXmlTextWriter : XmlTextWriter
    {
        public MyXmlTextWriter(Stream stream)
          : base(stream, (Encoding)null)
        {
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            base.WriteStartElement((string)null, localName, "");
        }
    }
}
