using IData.Constants;
using System.Collections.Generic;
using System.Linq;

namespace IData.Interfaces
{
    public static class IPaymentExtension
    {
        public static IEnumerable<IPayment> GetPaymentsForMonth(this IEnumerable<IPayment> payments, MonthEnum currentMonth)
        {
            return payments.Where(t => t.PayPattern.AffectedMonths.Any(p => p == currentMonth))
            .OrderBy(p => p.Category.SortOrder).ThenBy(p => p.SubCategory?.SortOrder);
        }

        public static IEnumerable<IPayment> GetNotAddedPaymentsForMonth(this IEnumerable<IPayment> payments, IMonth month)
        {
            var paymentsForMonth = GetPaymentsForMonth(payments, month.MonthType);
            var notAdded = paymentsForMonth.Where(p => month.Transactions.Elements.Any(t => t.Id == p.Id));

            return notAdded;
        }

        public static IEnumerable<ITransaction> GetTransactionsForCategory(this IEnumerable<ITransaction> transactions, IEnumerable<CategoryType> categories)
        {
            if (transactions.Any(x => x.Name.Contains("billa")))
            {
            }

            return transactions.Where(p => categories.Any(c => c == p.Payment.Category?.CategoryType));
        }
    }
}