using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel.Factories
{
    public class CategoryViewModelFacotry : ViewModelFactoryGeneric<CategoryViewModel, ICategory>
    {
        public CategoryViewModelFacotry(IEnumerable<IService> services)
            : base(services)
        {
        }
    }
}