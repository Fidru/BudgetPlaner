using System.Collections.Generic;
using System.Xml;

namespace XmlSaver.Data
{
    public abstract class XmlElement<T> : IXmlElement
    {
        protected XmlElement(string xmlTag, T value)
        {
            XmlTag = xmlTag;
            Value = value;
            Attributes = new Dictionary<string, SaveableAttribute>();
            AddAtributes();
        }

        public string XmlTag { get; set; }
        public Dictionary<string, SaveableAttribute> Attributes { get; }
        public T Value { get; set; }

        public object GetValue
        {
            get { return Value; }
        }

        private void AddAtributes()
        {
            Attributes.Clear();

            AddBaseAttributes();
            AddCustomAttributes();
        }

        public abstract void ReadAttributes();

        public virtual void AddBaseAttributes()
        {
        }

        public virtual void AddCustomAttributes()
        {
        }

        public abstract void WriteAttribute(XmlWriter writer);

        public abstract T ReadAttribute(XmlReader writer);

        public void WriteAttributes(XmlWriter writer)
        {
            foreach (var attr in Attributes)
            {
                new SaveableXmlElement(attr.Key, attr.Value).WriteAttribute(writer);
            }
        }

        public void ReadAttributes(XmlReader reader)
        {
            foreach (var attr in Attributes)
            {
                new SaveableXmlElement(attr.Key, attr.Value).ReadAttribute(reader);
            }

            ReadAttributes();
        }

        void IXmlElement.ReadAttribute(XmlReader reader)
        {
            Value = ReadAttribute(reader);
        }
    }
}