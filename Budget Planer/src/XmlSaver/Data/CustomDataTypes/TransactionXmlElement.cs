using IData.Interfaces;
using System;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class TransactionXmlElement : NodeXmlElement<ITransaction>
    {
        public TransactionXmlElement(string xmlTag, ITransaction value) : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Payment, new SaveableAttribute(XmlIds.Payment, Value.Payment.Id));
            Attributes.Add(XmlIds.Month, new SaveableAttribute(XmlIds.Month, Value.Month.Id));
            Attributes.Add(XmlIds.Amount, new SaveableAttribute(XmlIds.Amount, Value.Amount));
            Attributes.Add(XmlIds.Payed, new SaveableAttribute(XmlIds.Payed, Value.Payed));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Payment.Id = (Guid)GetAttribute(XmlIds.Payment);
            Value.Month.Id = (Guid)GetAttribute(XmlIds.Month);
            Value.Amount = Convert.ToDouble(GetAttribute(XmlIds.Amount));
            Value.Payed = (bool)GetAttribute(XmlIds.Payed);
        }
    }
}