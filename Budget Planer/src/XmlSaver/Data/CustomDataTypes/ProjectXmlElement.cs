using IData.Interfaces;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class ProjectXmlElement : NodeXmlElement<IProject>
    {
        public ProjectXmlElement(string xmlTag, IProject value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Elements, new SaveableAttribute(XmlIds.Elements, Value.Elements.Ids));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Elements.Ids = GetAttribute(XmlIds.Elements).ToString();
        }
    }
}