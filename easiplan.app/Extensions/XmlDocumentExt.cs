using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Finx.App.Extensions
{
    public static class XmlDocumentExt
    {
        public static void  Save(this XmlDocument model,string fileName,string EncKey)
        {
            model.Save(fileName);
        }

        public static void Load(this XmlDocument model, string fileName, string EncKey)
        {
            model.Load(fileName);
        }
    }
}
