using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface IPaymentIntervalFactory : IFactory<IPaymentInterval>
    {
        IPaymentInterval Create(PaymentIntervalType paymentInterval);
    }

    public interface IPayPatternFactory : IFactory<IPayPattern>
    {
        IPayPattern Create(IPaymentInterval interval, MonthEnum startsInMonth);
    }
}