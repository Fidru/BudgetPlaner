using IData.Interfaces;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class TransactionViewModel : ElementViewModel
    {
        public MonthViewModel MonthVm
        {
            get; set;
        }

        public double Amount
        {
            get
            {
                return Transaction.Amount;
            }
            set
            {
                Transaction.Amount = value;
                UpdateBankBalance();
            }
        }

        public bool Payed
        {
            get { return Transaction.Payed; }
            set
            {
                Transaction.Payed = value;
                UpdateBankBalance();
            }
        }

        private void UpdateBankBalance()
        {
            var updatedTransaction = Transaction.Month.Element.UpdateBankBalanceEndOfMonth();
            var transactionVm = MonthVm.TransactionVms.Single(t => t.Id == updatedTransaction.Id);

            NotifyPropertyChanged(transactionVm, nameof(Amount));
        }

        public ITransaction Transaction { get; set; }
    }
}