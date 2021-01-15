using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class PaymentViewModel : ElementViewModel<IPayment>
    {
        private CategoryViewModel _selectedCategory;

        public PaymentViewModel(IPayment element) : base(element)
        {
            Categories = new List<CategoryViewModel>();
            Transactions = new List<TransactionViewModel>();
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

        public List<TransactionViewModel> Transactions
        {
            get; set;
        }

        public List<PaymentPatternViewModel> PaymentPatterns { get; set; }

        public PaymentPatternViewModel SelectedPaymentPattern { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;

                Transactions.ForEach(t => t.Element.Category = value.Element);
                Transactions.ForEach(t => NotifyPropertyChanged(t, "Category"));
            }
        }

        public CategoryViewModel SelectedSubCategory { get; set; }
    }
}