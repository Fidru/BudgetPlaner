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

        public MonthViewModel CurrentMonth
        {
            get { return this; }
        }

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

        public AlignedMonthsViewModel AlignedMonths { get; set; }

        internal void AddTransaction(TransactionViewModel transactionVm)
        {
            TransactionVms.Add(transactionVm);
            UpdateLists();
        }

        public void AddNewTransaction()
        {
            var newPayment = PaymentFactory.CreateEmpty() as IPayment;

            var newTransaction = TransactionFactory.Create(Element, newPayment);
            var transactionVm = new TransactionViewModelFacotry(Services).ConvertToVm(newTransaction);

            transactionVm.CurrentMonthVm = this;
        }

        public ITransactionFactory TransactionFactory { get; set; }
        public IPaymentFactory PaymentFactory { get; set; }

        public IEnumerable<IService> Services { get; set; }
    }
}