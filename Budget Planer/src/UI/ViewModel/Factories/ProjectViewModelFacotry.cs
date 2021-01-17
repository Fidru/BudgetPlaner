using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace UI.ViewModel.Factories
{
    public class ProjectViewModelFacotry : ViewModelFactoryGeneric<ProjectViewModel, IProject>
    {
        public ProjectViewModelFacotry(IEnumerable<IService> services)
            : base(services)
        {
        }

        public override ProjectViewModel CreateVm(IProject element)
        {
            var vm = base.CreateVm(element);

            vm.YearVms = new YearsViewModelFactory(Services).ConvertToVms(element.Years);
            vm.CategorieVms = new CategoryViewModelFacotry(Services).ConvertToVms(element.Categories);
            vm.SubCategorieVms = new CategoryViewModelFacotry(Services).ConvertToVms(element.SubCategories);

            vm.CurrentYear = vm.YearVms.First();

            return vm;
        }
    }
}