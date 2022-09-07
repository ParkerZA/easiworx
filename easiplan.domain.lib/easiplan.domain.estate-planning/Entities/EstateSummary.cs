using my.domain.lib.core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace easiplan.domain.estate.MetaEntities
{
    [IgnoreAutoMap]
    [DataContract]
    public class EstateSummaryItem//: BaseEntity<int>
    {
        [DataMember(Name = "ItemDescription")]
        public virtual ItemDescription ItemDescription { get; set; }
        //[DataMember(Name = "Description")]
        public virtual string Description { get { return ItemDescription.Description; } set { } }
        [DataMember(Name = "Column1")]
        public virtual double? Asset { get; set; }
        [DataMember(Name = "Column2")]
        public virtual double? Expense { get; set; }
        [DataMember(Name = "Column3")]
        public virtual double? Total { get; set; }
        //[DataMember(Name = "Format")]
        //public virtual FormatType FormatType { get; set; }

        public EstateSummaryItem(string description,double? asset,double? expense,double? total)
        {
            ItemDescription = new ItemDescription(description);
            Asset = asset;
            Expense = expense;
            Total = total;
        }
        public EstateSummaryItem(string description, double? total)
        {
            ItemDescription = new ItemDescription(description);
            Total = total;
        }

        public EstateSummaryItem SetFormat(FormatType formatType)
        {
            this.ItemDescription.Format = formatType;

            return this;
        }
    }


    public class ItemDescription: IXmlSerializable
    {
        public string Description { get; set; }
        public FormatType Format { get; set; }

        public ItemDescription(string description)
        {
            Description = description;
            Format = FormatType.Normal;
        }
        public ItemDescription(string description, FormatType format)
        {
            Description = description;
            Format = format;
        }

        public XmlSchema GetSchema()
        {
            return null;

        }

        public void ReadXml(XmlReader reader)
        {
            //var data = reader.ReadString();
            //reader.ReadEndElement();
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Description");
            writer.WriteAttributeString("Format", Format.ToString());
            if (Format == FormatType.Break)
                writer.WriteString("-");
            else
                writer.WriteString(Description);
            writer.WriteEndElement();           
        }
    }
    public enum FormatType
    {
        Normal,
        NormalMedium,
        NormalLarge,
        Bold,
        BoldMedium,
        BoldLarge,
        NormalItalic,
        NormalItalicMedium,
        NormalItalicLarge,
        BoldItalic,
        BoldItalicMedium,
        BoldItalicLarge,
        Break
    }
}
