using IData.Interfaces;
using System;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class TransactionViewModel : ElementViewModel<ITransaction>
    {
        public TransactionViewModel(ITransaction element) : base(element)
        {
        }

        public PaymentViewModel PaymentViewModel { get; set; }

        public MonthViewModel MonthVm { get; set; }

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

        internal void SelectTransaction()
        {
            MonthVm.SelectedTransaction = this;
        }
    }
}