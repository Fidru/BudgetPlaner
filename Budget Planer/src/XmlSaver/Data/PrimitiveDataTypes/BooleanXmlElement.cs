using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class BooleanXmlElement : XmlElement<bool>
    {
        public BooleanXmlElement(string id, bool value) : base(id, value)
        {
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlTag, Value ? "1" : "0");
        }

        public override bool ReadAttribute(XmlReader reader)
        {
            var value = reader.GetAttribute(XmlTag);
            return value == "1";
        }

        public override void ReadAttributes()
        {
            throw new System.NotImplementedException();
        }
    }
}