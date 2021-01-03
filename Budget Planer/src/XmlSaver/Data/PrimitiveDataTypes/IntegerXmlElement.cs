using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class IntegerXmlElement : XmlElement<int>
    {
        public IntegerXmlElement(string id, int value) : base(id, value)
        {
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlTag, Value.ToString());
        }

        public override int ReadAttribute(XmlReader reader)
        {
            var result = reader.GetAttribute(XmlTag);
            return XmlConvert.ToInt32(result);
        }

        public override void ReadAttributes()
        {
            throw new System.NotImplementedException();
        }
    }
}