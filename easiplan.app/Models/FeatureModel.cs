using System.Xml.Serialization;

namespace Finx.App.Models
{
    public class FeatureModel
    {
        public string name { get; set; }
        [XmlElement(ElementName = "Active")]
        public string Status { get; set; }
    }
}