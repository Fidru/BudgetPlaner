using IData.Interfaces;
using System.Linq;
using System.Windows;

namespace UI.ViewModel
{
    public class TransactionViewModel : ElementViewModel<ITransaction>
    {
        private MonthViewModel _currentMonthVm;

        public TransactionViewModel(ITransaction element) : base(element)
        {
        }

        public PaymentViewModel PaymentViewModel { get; set; }

        public MonthViewModel MonthVm { get; set; }

        public MonthViewModel CurrentMonthVm
        {
            get
            {
                return _currentMonthVm;
            }
            set
            {
                _currentMonthVm = value;
                MonthVm.SelectedTransaction = this;
            }
        }

        public double Amount
        {
            get
            {
                return Element.Amount;
            }
            set
            {
                Element.Amount = value;
                UpdateBankBalance();
            }
        }

        public bool Payed
        {
            get { return Element.Payed; }
            set
            {
                Element.Payed = value;
                UpdateBankBalance();
            }
        }

        private void UpdateBankBalance()
        {
            var updatedTransaction = Element.Month.Element.UpdateBankBalanceEndOfMonth();
            var transactionVm = MonthVm.TransactionVms.Single(t => t.Id == updatedTransaction.Id);

            NotifyPropertyChanged(transactionVm, nameof(Amount));
        }

        public Visibility CanEdit
        {
            get
            {
                return Element.CanEdit ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }
}