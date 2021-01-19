using IData.Constants;
using IData.Interfaces;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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
            var transactionVm = MonthVm.TransactionVms.Where(t => updatedTransaction.Any(updated => updated.Id == t.Id));

            transactionVm.ToList().ForEach(t => NotifyPropertyChanged(t, nameof(Amount)));
        }

        public Visibility CanEdit
        {
            get
            {
                return Element.CanEdit ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public int CategorySortOrder
        {
            get
            {
                var sortOrder = Element.SubCategory == null ? Element.Category.SortOrder : Element.SubCategory.SortOrder;

                return sortOrder;
            }
        }

        public FontWeight FontWeight
        {
            get
            {
                return IsEndOfMonth ? FontWeights.Bold : FontWeights.Normal;
            }
        }

        private bool IsEndOfMonth
        {
            get
            {
                return Element.SubCategoryType == CategoryType.BankbalanceEndOfMonth
                  || (Element.CategoryType == CategoryType.Bankbalance && Element.SubCategoryType == CategoryType.None);
            }
        }

        public bool IsNegativeBankBalance
        {
            get
            {
                return IsEndOfMonth && Amount < 0;
            }
        }

        public bool IsPositiveBankBalance
        {
            get { return IsEndOfMonth && Amount > 0; }
        }
    }
}