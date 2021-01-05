using System;
using System.Xml;

namespace XmlSaver.Data.PrimitiveDataTypes
{
    public class DateTimeXmlElement : XmlElement<DateTime>
    {
        private string format = "yyyy-MM-ddTHH:mm:ss";

        public DateTimeXmlElement(string xmlTag, DateTime value) : base(xmlTag, value)
        {
        }

        public override DateTime ReadAttribute(XmlReader reader)
        {
            var result = reader.GetAttribute(XmlTag);

            DateTime date = DateTime.Parse(result);

            return date;
        }

        public override void ReadAttributes()
        {
            throw new NotImplementedException();
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            var formatedValue = XmlConvert.ToString(Value, format);

            writer.WriteAttributeString(XmlTag, formatedValue);
        }
    }
}