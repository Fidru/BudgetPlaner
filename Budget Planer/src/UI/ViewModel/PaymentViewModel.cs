using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace UI.ViewModel
{
    public class PaymentViewModel : ElementViewModel<IPayment>
    {
        private CategoryViewModel _selectedCategory;
        private CategoryViewModel _selectedSubCategory;
        private PaymentIntervalViewModel _selectedInterval;
        private List<TransactionViewModel> _transactions;

        public PaymentViewModel(IPayment element) : base(element)
        {
            Categories = new List<CategoryViewModel>();
            SubCategories = new List<CategoryViewModel>();
            Transactions = new List<TransactionViewModel>();
            Intervals = new List<PaymentIntervalViewModel>();
            AllAffectedMonths = new List<AffectedMonthViewModel>();
            AffecctedMonths = new List<AffectedMonthViewModel>();
        }

        public double Amount
        {
            get { return Element.Amount; }
            set
            {
                Element.Amount = value;

                CurrentTransactions.ForEach(t => t.Amount = value);
                CurrentTransactions.ForEach(t => NotifyPropertyChanged(t, "Amount"));
            }
        }

        public new string Name
        {
            get
            {
                return Element.Name;
            }
            set
            {
                Element.Name = value;

                Transactions.ForEach(t => t.Name = value);
                UpdateTransactions("Name");
            }
        }

        public List<TransactionViewModel> Transactions
        {
            get
            {
                return _transactions.Where(t => !t.Element.IsDeleted).ToList();
            }
            set
            {
                _transactions = value;
            }
        }

        public List<TransactionViewModel> CurrentTransactions
        {
            get { return Transactions.Where(x => x.CurrentMonthVm != null).ToList(); }
        }

        public List<CategoryViewModel> Categories { get; set; }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                if (value != null)
                {
                    Element.Category.Element = value.Element;
                }

                CurrentTransactions.ForEach(t => t.Element.Category = value.Element);
                UpdateTransactions("Category");
            }
        }

        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel SelectedSubCategory
        {
            get { return _selectedSubCategory; }
            set
            {
                _selectedSubCategory = value;
                if (value != null)
                {
                    Element.SubCategory.Element = value.Element;
                }

                Transactions.ForEach(t => t.Element.SubCategory = value.Element);
                UpdateTransactions("SubCategory");
            }
        }

        private void UpdateTransactions(string property)
        {
            CurrentTransactions.ForEach(t => NotifyPropertyChanged(t, property));
        }

        public List<PaymentIntervalViewModel> Intervals { get; set; }

        public PaymentIntervalViewModel SelectedInterval
        {
            get { return _selectedInterval; }
            set
            {
                _selectedInterval = value;
                Element.PayPattern.Element.Interval.Element = value.Element;

                if (Element.PayPattern.Element.Interval.Element.Type == PaymentIntervalType.Custom)
                {
                    Element.PayPattern.Element.AffectedMonths = AffecctedMonths.Where(x => x.IsSelected).Select(x => x.MonthType);
                }
                else
                {
                    if (MonthVm != null)
                    {
                        Element.PayPattern.Element.AffectedMonths = new AffectedMonthsCollection(MonthVm.Element.MonthType, value.Element.Type);
                    }
                }

                AffecctedMonths.ForEach(am => am.IsSelected = Element.PayPattern.Element.AffectedMonths.Contains(am.MonthType));
                AffecctedMonths.ForEach(am => NotifyPropertyChanged(am, "IsSelected"));
            }
        }

        public MonthViewModel MonthVm
        {
            get { return Transactions.FirstOrDefault(t => t.CurrentMonthVm != null)?.CurrentMonthVm; }
        }

        public void Update()
        {
            var monthVm = MonthVm;

            if (MonthVm != null)
            {
                var payment = Element;
                TransactionFactory.UpdatePayment(payment, monthVm.Element);
                UpdateTransactions(nameof(MonthVm));

                monthVm.UpdateLists();
                Transactions.ForEach(t => t.CurrentMonthVm = null);
            }
        }

        public ITransactionFactory TransactionFactory { get; set; }

        public List<AffectedMonthViewModel> AffecctedMonths { get; set; }
        public List<AffectedMonthViewModel> AllAffectedMonths { get; set; }

        internal void AddTransaction(TransactionViewModel transactionVm)
        {
            if (Transactions.All(t => t.Id != transactionVm.Id))
            {
                _transactions.Add(transactionVm);
            }
        }
    }
}