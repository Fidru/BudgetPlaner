using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class CategoryViewModelFacotry : ViewModelFactoryGeneric<CategoryViewModel, ICategory>
    {
        public CategoryViewModelFacotry(IRepositoryService repositoryService) : base(repositoryService)
        {
        }
    }
}