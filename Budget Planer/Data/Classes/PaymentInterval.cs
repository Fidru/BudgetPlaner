using IData.Constants;
using IData.Interfaces;
using System.Collections.Generic;

namespace Data.Classes
{
    public class PaymentInterval : Element, IPaymentInterval
    {
        public PaymentInterval()
            : base()
        {
            Type = PaymentIntervalType.Monthly;
            Name = Type.ConvertToText();
        }

        public PaymentInterval(PaymentIntervalType paymentIntervalType)
            : base()
        {
            Type = paymentIntervalType;
            Name = Type.ConvertToText();
        }

        public PaymentIntervalType Type { get; set; }
    }
}