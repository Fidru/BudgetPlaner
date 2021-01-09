using IData.Constants;
using IData.Interfaces;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class MonthXmlElement : NodeXmlElement<IMonth>
    {
        public MonthXmlElement(string xmlTag, IMonth value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.MonthType, new SaveableAttribute(XmlIds.MonthType, (int)Value.MonthType));
            Attributes.Add(XmlIds.Transactions, new SaveableAttribute(XmlIds.Transactions, Value.Transactions.Ids));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.MonthType = (MonthEnum)GetAttribute(XmlIds.MonthType);
            Value.Transactions.Ids = GetAttribute(XmlIds.Transactions).ToString();
        }
    }
}