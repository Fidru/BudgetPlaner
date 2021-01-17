using IData.Interfaces;
using System;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class PaymentXmlElement : ElementXmlElement<IPayment>
    {
        public PaymentXmlElement(string xmlTag, IPayment value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Category, new SaveableAttribute(XmlIds.Category, Value.Category.Id));
            Attributes.Add(XmlIds.PayPattern, new SaveableAttribute(XmlIds.PayPattern, Value.PayPattern.Id));
            Attributes.Add(XmlIds.Amount, new SaveableAttribute(XmlIds.Amount, Value.Amount));

            Attributes.Add(XmlIds.SubCategory, new SaveableAttribute(XmlIds.SubCategory, Value.SubCategory.Id));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Category.Id = (Guid)GetAttribute(XmlIds.Category);
            Value.PayPattern.Id = (Guid)GetAttribute(XmlIds.PayPattern);
            Value.Amount = (double)GetAttribute(XmlIds.Amount);

            Value.SubCategory.Id = (Guid)GetAttribute(XmlIds.SubCategory);
        }
    }
}