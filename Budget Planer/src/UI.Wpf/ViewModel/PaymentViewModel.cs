using IData.Interfaces;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel
{
    public class PaymentViewModel : ElementViewModel<IPayment>
    {
        private CategoryViewModel _selectedCategory;
        private CategoryViewModel _selectedSubCategory;
        private PaymentIntervalViewModel _selectedInterval;

        public PaymentViewModel(IPayment element) : base(element)
        {
            Categories = new List<CategoryViewModel>();
            SubCategories = new List<CategoryViewModel>();
            Transactions = new List<TransactionViewModel>();
            Intervals = new List<PaymentIntervalViewModel>();
        }

        public double Amount
        {
            get { return Element.Amount; }
            set
            {
                Element.Amount = value;

                Transactions.ForEach(t => t.Amount = value);
                Transactions.ForEach(t => NotifyPropertyChanged(t, "Amount"));
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

        public List<TransactionViewModel> Transactions { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;

                Transactions.ForEach(t => t.Element.Category = value.Element);
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

                Transactions.ForEach(t => t.Element.SubCategory = value.Element);
                UpdateTransactions("SubCategory");
            }
        }

        private void UpdateTransactions(string property)
        {
            NotifyPropertyChanged(property);
            Transactions.ForEach(t => NotifyPropertyChanged(t, property));
        }

        public List<PaymentIntervalViewModel> Intervals { get; set; }

        public PaymentIntervalViewModel SelectedInterval
        {
            get { return _selectedInterval; }
            set
            {
                _selectedInterval = value;

                Element.PayPattern.Element.Interval.Element = value.Element;
                Element.PayPattern.Element.UpdateAffectedMonths();

                //Transactions.ForEach(t => t.Element.Payment.Element.payment = value.Element);
                //UpdateTransactions("SubCategory");
            }
        }
    }
}