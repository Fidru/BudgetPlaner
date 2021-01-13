using IData.Interfaces;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel
{
    public class MonthViewModel : ElementViewModel<IMonth>
    {
        public MonthViewModel(IMonth element) : base(element)
        {
        }

        public List<TransactionViewModel> TransactionVms { get; set; }

        public List<TransactionViewModel> Bills { get; set; }
        public List<TransactionViewModel> FoodPayments { get; set; }
        public List<TransactionViewModel> CreditCardPayments { get; set; }
        public List<TransactionViewModel> ExpectedUnexpectedPayments { get; set; }

        public List<TransactionViewModel> Empty { get; set; }

        public AlignedMonthsViewModel AlignedMonths { get; set; }
    }
}