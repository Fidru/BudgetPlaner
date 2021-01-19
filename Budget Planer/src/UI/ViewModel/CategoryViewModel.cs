using IData.Constants;
using IData.Interfaces;

namespace UI.ViewModel
{
    public class CategoryViewModel : ElementViewModel<ICategory>
    {
        public CategoryViewModel(ICategory element) : base(element)
        {
        }

        public CategoryType CategoryType
        {
            get
            {
                return Element.CategoryType;
            }
            set
            {
                Element.CategoryType = value;
            }
        }

        public int SortOrder
        {
            get
            {
                return Element.SortOrder;
            }
            set
            {
                Element.SortOrder = value;
            }
        }
    }
}