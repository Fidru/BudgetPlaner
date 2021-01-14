using IData.Interfaces;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel
{
    public class MonthViewModel : ElementViewModel<IMonth>
    {
        private TransactionViewModel _selectedTransaction;

        public MonthViewModel(IMonth element) : base(element)
        {
        }

        public TransactionViewModel SelectedTransaction
        {
            get
            {
                return _selectedTransaction;
            }
            set
            {
                _selectedTransaction = value;
                NotifyPropertyChanged(nameof(SelectedTransaction));
            }
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