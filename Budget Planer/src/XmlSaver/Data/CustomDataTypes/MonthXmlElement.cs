using IData.Constants;
using IData.Interfaces;
using System;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class MonthXmlElement : ElementXmlElement<IMonth>
    {
        public MonthXmlElement(string xmlTag, IMonth value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.MonthType, new SaveableAttribute(XmlIds.MonthType, (int)Value.MonthType));
            Attributes.Add(XmlIds.Transactions, new SaveableAttribute(XmlIds.Transactions, Value.Transactions.Ids));
            Attributes.Add(XmlIds.Year, new SaveableAttribute(XmlIds.Year, Value.Year.Id));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.MonthType = (MonthEnum)GetAttribute(XmlIds.MonthType);
            Value.Transactions.Ids = GetAttribute(XmlIds.Transactions).ToString();
            Value.Year.Id = (Guid)GetAttribute(XmlIds.Year);
        }
    }
}