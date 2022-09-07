using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Finx.App.Models
{
    public class ProductFeature
    {
        [XmlElement(ElementName = "Feature")]
        public List<FeatureModel> FeatureModels { get; set; }
    }
}