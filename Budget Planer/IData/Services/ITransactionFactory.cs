using IData.Interfaces;
using System.Collections.Generic;

namespace IData.Services
{
    public interface ITransactionFactory : IFactory<ITransaction>
    {
        ITransaction Create(IMonth month, IPayment payment);

        void AddTransactions(IMonth month, IEnumerable<IPayment> paymentsForMonth);

        void UpdatePayment(IPayment payment, IMonth month);
    }
}