using System.Xml.Serialization;

namespace Finx.App.Models
{
    public class Customer
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Email { get; set; }
    }
}