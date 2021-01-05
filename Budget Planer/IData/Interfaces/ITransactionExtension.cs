using System.Collections.Generic;
using System.Linq;

namespace IData.Interfaces
{
    public static class ITransactionExtension
    {
        public static IEnumerable<ITransaction> GetTransactionsByPayment(this IEnumerable<ITransaction> transactions, IPayment payment)
        {
            return transactions.Where(t => t.Payment.Id == payment.Id);
        }
    }
}