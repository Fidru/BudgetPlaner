using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface IPaymentFactory : IElementFactory
    {
        IPayment Create(string name, ICategory category, double amount, PaymentIntervalType intervalType, MonthEnum startingMonth, ICategory subCategory = null);

        IPayment Create(IMonth month, CategoryType categoryType);
    }
}