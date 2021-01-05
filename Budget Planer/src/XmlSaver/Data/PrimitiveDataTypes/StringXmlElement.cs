using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class StringXmlElement : XmlElement<string>
    {
        public StringXmlElement(string id, string value) : base(id, value)
        {
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlTag, Value);
        }

        public override string ReadAttribute(XmlReader reader)
        {
            return reader.GetAttribute(XmlTag);
        }

        public override void ReadAttributes()
        {
            throw new System.NotImplementedException();
        }
    }
}