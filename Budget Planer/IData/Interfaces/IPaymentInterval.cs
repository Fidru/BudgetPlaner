using IData.Constants;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IPaymentInterval : IElement
    {
        PaymentIntervalType Type { get; set; }
    }
}