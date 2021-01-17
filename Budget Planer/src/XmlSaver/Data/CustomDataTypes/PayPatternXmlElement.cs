using IData.Constants;
using IData.Interfaces;
using System;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class PayPatternXmlElement : ElementXmlElement<IPayPattern>
    {
        public PayPatternXmlElement(string xmlTag, IPayPattern value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Interval, new SaveableAttribute(XmlIds.Interval, Value.Interval.Id));
            Attributes.Add(XmlIds.StartsInMonth, new SaveableAttribute(XmlIds.StartsInMonth, (int)Value.StartsInMonth));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Interval.Id = (Guid)GetAttribute(XmlIds.Interval);
            Value.StartsInMonth = (MonthEnum)GetAttribute(XmlIds.StartsInMonth);
        }
    }
}