using IData.Constants;

namespace IData.Interfaces
{
    public interface IPaymentInterval : IElement
    {
        PaymentIntervalType Type { get; set; }
    }
}