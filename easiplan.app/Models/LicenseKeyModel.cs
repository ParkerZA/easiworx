using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Finx.App.Models
{
    [XmlRoot(ElementName = "License")]
    public class LicenseKeyModel
    {
        [XmlElement]
        public string Id { get; set; }
        [XmlElement]
        public string MachineKey { get; set; }
        [XmlElement]
        public string Type { get; set; }
        [XmlElement]
        public string Quantity { get; set; }
        [XmlElement]
        public ProductFeature ProductFeatures { get; set; }
        [XmlElement]
        public Database Database { get; set; }
        [XmlElement]
        public Customer Customer { get; set; }
        [XmlElement]
        public string Status { get; set; }
        [XmlElement]
        public string Signature { get; set; }
        [XmlElement]
        public string IssueDate { get; set; } 
        [XmlElement]
        public string ExpiryDate { get; set; }
    }
}