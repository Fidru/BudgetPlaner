using IData.Interfaces;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class ProjectViewModelFacotry : ViewModelFactoryGeneric<ProjectViewModel, IProject>
    {
        public override ProjectViewModel CreateVm(IProject element)
        {
            var vm = new ProjectViewModel(element)
            {
                YearsVm = new YearsViewModelFactory().ConvertToVms(element.Years),
                CategorieVms = new CategoryViewModelFacotry().ConvertToVms(element.Categories),
                SubCategorieVms = new CategoryViewModelFacotry().ConvertToVms(element.SubCategories),
            };

            vm.CurrentYear = vm.YearsVm.First();

            return vm;
        }
    }
}