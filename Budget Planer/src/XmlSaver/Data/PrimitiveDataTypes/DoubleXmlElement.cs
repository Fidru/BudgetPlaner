using System;
using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class DoubleXmlElement : XmlElement<double>
    {
        public DoubleXmlElement(string xmlTag, double value) : base(xmlTag, value)
        {
        }

        public override double ReadAttribute(XmlReader reader)
        {
            var result = reader.GetAttribute(XmlTag);
            return Convert.ToDouble(result);
        }

        public override void ReadAttributes()
        {
            throw new NotImplementedException();
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            var value = Value.ToString("n2");

            writer.WriteAttributeString(XmlTag, value);
        }
    }
}