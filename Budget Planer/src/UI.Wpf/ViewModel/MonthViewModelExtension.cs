using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public static class MonthViewModelExtension
    {
        public static List<TransactionViewModel> GetFilteredViewModels(this IEnumerable<TransactionViewModel> transactionVms, IEnumerable<ITransaction> transactions)
        {
            return transactionVms.Where(t => transactions.Any(b => b.Id == t.Id)).ToList();
        }
    }
}