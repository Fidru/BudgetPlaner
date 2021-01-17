using IData.Interfaces;
using System;
using System.Xml;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class ElementXmlElement<T> : XmlElement<T> where T : IElement
    {
        public ElementXmlElement(string xmlTag, T value) : base(xmlTag, value)
        {
        }

        public override T ReadAttribute(XmlReader reader)
        {
            ReadAttributes(reader);
            return Value;
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            writer.WriteStartElement(XmlTag);

            WriteAttributes(writer);

            writer.WriteEndElement();
        }

        public object GetAttribute(string key)
        {
            if (Attributes.ContainsKey(key))
            {
                return Attributes[key].Data;
            }
            return null;
        }

        public override void AddBaseAttributes()
        {
            Attributes.Add(XmlIds.Name, new SaveableAttribute(XmlIds.Name, Value.Name));
            Attributes.Add(XmlIds.CreatedAt, new SaveableAttribute(XmlIds.CreatedAt, Value.CreatedAt));
            Attributes.Add(XmlIds.ChangedAt, new SaveableAttribute(XmlIds.ChangedAt, Value.ChangedAt));
            Attributes.Add(XmlIds.Id, new SaveableAttribute(XmlIds.Id, Value.Id));
        }

        public override void ReadAttributes()
        {
            Value.Name = Attributes[XmlIds.Name].Data.ToString();
            Value.CreatedAt = (DateTime)Attributes[XmlIds.CreatedAt].Data;
            Value.ChangedAt = (DateTime)Attributes[XmlIds.ChangedAt].Data;
            Value.Id = (Guid)Attributes[XmlIds.Id].Data;
            Value.IsNew = false;
        }
    }
}