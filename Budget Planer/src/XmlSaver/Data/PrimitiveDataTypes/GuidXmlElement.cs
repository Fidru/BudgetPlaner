using System;
using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class GuidXmlElement : XmlElement<Guid>
    {
        public GuidXmlElement(string xmlTag, Guid value) : base(xmlTag, value)
        {
        }

        public override Guid ReadAttribute(XmlReader reader)
        {
            var result = reader.GetAttribute(XmlTag);

            if (result != null)
            {
                Guid id = Guid.Parse(result);

                return id;
            }
            return Guid.Empty;
        }

        public override void ReadAttributes()
        {
            throw new NotImplementedException();
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlTag, Value.ToString());
        }
    }
}