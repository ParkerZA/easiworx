using System.Xml.Serialization;

namespace Finx.App.Models
{
    public class Database
    {
        [XmlElement]
        public string DatabaseName { get; set; }
        [XmlElement]
        public string Port { get; set; }
        [XmlElement]
        public string Username { get; set; }
        [XmlElement]
        public string Password { get; set; }
        [XmlElement]
        public bool Hosted { get; set; }
        [XmlElement]
        public string ConnectionString { get; set; }
        [XmlElement]
        public string Status { get; set; }
    }
}