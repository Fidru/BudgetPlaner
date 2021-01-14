using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class PaymentViewModel : ElementViewModel<IPayment>
    {
        public PaymentViewModel(ProjectViewModel projectvm, IPayment element) : this(element)
        {
            PossibleCategories = projectvm.CategorieVms;
        }

        public PaymentViewModel(IPayment element) : base(element)
        {
            PossibleCategories = new List<CategoryViewModel>();
        }

        public double Amount
        {
            get { return Element.Amount; }
            set { Element.Amount = value; }
        }

        public List<CategoryViewModel> PossibleCategories { get; set; }

        public CategoryViewModel SelectedCategory { get; set; }

        public int SelectedCategoryIndex
        {
            get
            {
                return PossibleCategories.IndexOf(SelectedCategory);
            }
            set
            {
                SelectedCategory = PossibleCategories[value];
            }
        }
    }
}