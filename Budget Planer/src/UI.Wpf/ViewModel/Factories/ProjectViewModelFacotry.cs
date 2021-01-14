using IData.Interfaces;
using IData.Services;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class ProjectViewModelFacotry : ViewModelFactoryGeneric<ProjectViewModel, IProject>
    {
        public ProjectViewModelFacotry(IRepositoryService repositoryService)
            : base(repositoryService)
        {
        }

        public override ProjectViewModel CreateVm(IProject element)
        {
            var vm = base.CreateVm(element);

            vm.YearsVm = new YearsViewModelFactory(RepositoryService).ConvertToVms(element.Years);
            vm.CategorieVms = new CategoryViewModelFacotry(RepositoryService).ConvertToVms(element.Categories);
            vm.SubCategorieVms = new CategoryViewModelFacotry(RepositoryService).ConvertToVms(element.SubCategories);

            vm.CurrentYear = vm.YearsVm.First();

            return vm;
        }
    }
}