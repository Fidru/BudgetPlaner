using IData.Interfaces;
using System;
using System.Xml;
using XmlSaver.Data.CustomDataTypes;
using XmlSaver.Data.PrimitiveDataTypes;

namespace XmlSaver.Data
{
    public class SaveableXmlElement : XmlElement<SaveableAttribute>
    {
        public SaveableXmlElement(string xmlTag, SaveableAttribute value)
            : base(xmlTag, value)
        {
        }

        public override void WriteAttribute(XmlWriter writer)
        {
            IXmlElement element = GetXmlElement();
            element.WriteAttribute(writer);
        }

        public IXmlElement GetXmlElement()
        {
            var item = Value.Data;
            var tag = Value.AttributeTag;

            if (item is bool)
            {
                return new BooleanXmlElement(tag, (bool)item);
            }
            if (item is string)
            {
                return new StringXmlElement(tag, item.ToString());
            }
            if (item != null && Type.GetTypeCode(item.GetType()) == TypeCode.Double)
            {
                return new DoubleXmlElement(tag, Convert.ToDouble(item));
            }
            if (item != null && Type.GetTypeCode(item.GetType()) == TypeCode.Int32)
            {
                return new IntegerXmlElement(tag, Convert.ToInt32(item));
            }
            if (item is DateTime)
            {
                return new DateTimeXmlElement(tag, (DateTime)item);
            }
            if (item is Guid)
            {
                return new GuidXmlElement(tag, (Guid)item);
            }
            if (item is ICategory)
            {
                return new CategoryXmlElement(tag, (ICategory)item);
            }
            if (item is IMonth)
            {
                return new MonthXmlElement(tag, (IMonth)item);
            }
            if (item is IPayPattern)
            {
                return new PayPatternXmlElement(tag, (IPayPattern)item);
            }
            if (item is IProject)
            {
                return new ProjectXmlElement(tag, (IProject)item);
            }
            if (item is IPayment)
            {
                return new PaymentXmlElement(tag, (IPayment)item);
            }
            if (item is IYear)
            {
                return new YearXmlElement(tag, (IYear)item);
            }
            if (item is ITransaction)
            {
                return new TransactionXmlElement(tag, (ITransaction)item);
            }
            if (item is IPaymentInterval)
            {
                return new PaymentIntervalXmlElement(tag, (IPaymentInterval)item);
            }

            return null;
        }

        public override SaveableAttribute ReadAttribute(XmlReader reader)
        {
            if (Value.Data == null)
            {
                return null;
            }

            var element = GetXmlElement();

            element.ReadAttribute(reader);

            Value.Data = element.GetValue;
            return Value;
        }

        public override void ReadAttributes()
        {
            throw new NotImplementedException();
        }
    }
}