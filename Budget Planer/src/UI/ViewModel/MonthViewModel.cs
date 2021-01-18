using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;
using UI.ViewModel.Factories;

namespace UI.ViewModel
{
    public class MonthViewModel : ElementViewModel<IMonth>
    {
        private TransactionViewModel _selectedTransaction;
        private List<TransactionViewModel> _transactions;

        public MonthViewModel CurrentMonth
        {
            get { return this; }
        }

        public MonthViewModel(IMonth element)
            : base(element)
        {
            TransactionVms = new List<TransactionViewModel>();
        }

        public YearViewModel Year { get; set; }

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
            if (TransactionVms.Any())
            {
                NotifyPropertyChanged("TransactionVms");
                NotifyPropertyChanged("Bills");
                NotifyPropertyChanged("FoodPayments");
                NotifyPropertyChanged("CreditCardPayments");
                NotifyPropertyChanged("ExpectedUnexpectedPayments");
            }
        }

        public List<TransactionViewModel> TransactionVms
        {
            get
            {
                return _transactions.Where(x => !x.Element.IsDeleted).ToList();
            }
            set
            {
                _transactions = value;
            }
        }

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

        public AlignedMonthsViewModel AlignedMonths { get; set; }

        internal void AddTransaction(TransactionViewModel transactionVm)
        {
            if (TransactionVms.All(t => t.Id != transactionVm.Id))
            {
                _transactions.Add(transactionVm);
                UpdateLists();
            }
        }

        public void AddNewTransaction()
        {
            var newPayment = PaymentFactory.Create(Element);

            var newTransaction = TransactionFactory.Create(Element, newPayment);
            var transactionVm = new TransactionViewModelFacotry(Services).ConvertToVm(newTransaction);

            AddTransaction(transactionVm);
            transactionVm.PaymentViewModel.AddTransaction(transactionVm);
            transactionVm.CurrentMonthVm = this;

            UpdateLists();
        }

        public ITransactionFactory TransactionFactory { get; set; }
        public IPaymentFactory PaymentFactory { get; set; }

        public IEnumerable<IService> Services { get; set; }
    }
}