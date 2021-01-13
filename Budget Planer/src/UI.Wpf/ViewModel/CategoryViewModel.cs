using IData.Constants;
using IData.Interfaces;

namespace UI.Wpf.ViewModel
{
    public class CategoryViewModel : ElementViewModel<ICategory>
    {
        public CategoryViewModel(ICategory element) : base(element)
        {
        }

        public bool IsMainCategory
        {
            get
            {
                return Element.IsMainCategory;
            }
            set
            {
                Element.IsMainCategory = value;
            }
        }

        public CategoryType CategoryType
        {
            get
            {
                return Element.CategoryType;
            }
            set
            {
                Element.CategoryType = (CategoryType)value;
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