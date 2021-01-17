using IData.Constants;
using IData.Interfaces;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class PaymentIntervalXmlElement : ElementXmlElement<IPaymentInterval>
    {
        public PaymentIntervalXmlElement(string xmlTag, IPaymentInterval value)
            : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Type, new SaveableAttribute(XmlIds.Type, (int)Value.Type));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Type = (PaymentIntervalType)GetAttribute(XmlIds.Type);
        }
    }
}