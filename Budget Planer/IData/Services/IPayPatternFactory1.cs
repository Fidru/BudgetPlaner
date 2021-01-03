using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface IPaymentIntervalFactory : IElementFactory<IPaymentInterval>
    {
        IPaymentInterval Create(PaymentIntervalType paymentInterval);
    }

    public interface IPayPatternFactory : IElementFactory<IPayPattern>
    {
        IPayPattern Create(IPaymentInterval interval, MonthEnum startsInMonth);
    }
}