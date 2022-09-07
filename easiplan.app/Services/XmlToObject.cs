using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace easiplan.app.Services
{
    public class XmlToObject
    {
        
        public T DeserializeXDocumentToObject<T>(XDocument xDocument) where T : class  
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(xDocument.ToString()))
            {
                return (T) xmlSerializer.Deserialize(textReader);
            }
        }  

    }
}