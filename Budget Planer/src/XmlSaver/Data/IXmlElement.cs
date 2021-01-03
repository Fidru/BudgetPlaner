using System.Xml;

namespace XmlSaver.Data
{
    public interface IXmlElement
    {
        object GetValue { get; }

        void WriteAttribute(XmlWriter writer);

        void ReadAttribute(XmlReader writer);
    }
}