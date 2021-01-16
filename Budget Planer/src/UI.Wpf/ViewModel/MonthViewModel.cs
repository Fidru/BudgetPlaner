using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class MonthViewModel : ElementViewModel<IMonth>
    {
        private TransactionViewModel _selectedTransaction;

        public MonthViewModel(IMonth element)
            : base(element)
        {
            TransactionVms = new List<TransactionViewModel>();
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

        public void UpdateLists()
        {
            var newTrans = TransactionVms.Where(x => !x.Element.IsDeleted).ToList();

            if (TransactionVms.Any())
            {
                TransactionVms = newTrans;

                NotifyPropertyChanged("TransactionVms");
                NotifyPropertyChanged("Bills");
                NotifyPropertyChanged("FoodPayments");
                NotifyPropertyChanged("CreditCardPayments");
                NotifyPropertyChanged("ExpectedUnexpectedPayments");
            }
        }

        public List<TransactionViewModel> TransactionVms { get; set; }

        public List<TransactionViewModel> Bills
        {
            get
            {
                return TransactionVms.GetFilteredViewModels(Element.Bills);
            }
            set
            {
            }
        }

        public List<TransactionViewModel> FoodPayments
        {
            get
            {
                return TransactionVms.GetFilteredViewModels(Element.FoodPayments);
            }
            set
            {
            }
        }

        public List<TransactionViewModel> CreditCardPayments
        {
            get
            {
                return TransactionVms.GetFilteredViewModels(Element.CreditCardPayments);
            }
            set
            {
            }
        }

        public List<TransactionViewModel> ExpectedUnexpectedPayments
        {
            get
            {
                return TransactionVms.GetFilteredViewModels(Element.ExpectedUnexpectedPayments);
            }
            set
            {
            }
        }

        public List<TransactionViewModel> Empty { get; set; }

        public AlignedMonthsViewModel AlignedMonths { get; set; }
    }
}