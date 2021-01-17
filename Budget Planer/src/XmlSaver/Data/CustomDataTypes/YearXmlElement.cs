using IData.Interfaces;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class YearXmlElement : ElementXmlElement<IYear>
    {
        public YearXmlElement(string xmlTag, IYear value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Months, new SaveableAttribute(XmlIds.Months, Value.Months.Ids));
            Attributes.Add(XmlIds.SortOrder, new SaveableAttribute(XmlIds.SortOrder, Value.SortOrder));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Months.Ids = GetAttribute(XmlIds.Months).ToString();
            Value.SortOrder = (int)GetAttribute(XmlIds.SortOrder);
        }
    }
}