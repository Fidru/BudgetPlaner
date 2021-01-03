using IData.Interfaces;

namespace IData.Services
{
    public interface IPaymentFactory : IElementFactory<IPayment>
    {
        IPayment Create(string name, ICategory category, double amount, IPayPattern payPattern, ICategory subCategory = null);
    }
}