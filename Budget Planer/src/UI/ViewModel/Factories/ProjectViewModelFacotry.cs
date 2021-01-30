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

            //LazyLoading(element, vm);

            vm.YearVms = new YearsViewModelFactory(Services).ConvertToVms(element.Years);
            vm.CategorieVms = new CategoryViewModelFacotry(Services).ConvertToVms(element.Categories);
            vm.SubCategorieVms = new CategoryViewModelFacotry(Services).ConvertToVms(element.SubCategories);

            var currentMonth = vm.YearVms.Select(x => x.MonthVms.FirstOrDefault(m => m.Element.OpenTransactions.Any()));

            if (currentMonth.Any())
            {
                vm.CurrentYear = currentMonth.First().Year;
                vm.CurrentYear.CurrentMonthVm = currentMonth.First();
            }
            else
            {
                vm.CurrentYear = vm.YearVms.First();
            }

            return vm;
        }

        private void LazyLoading(IProject element, ProjectViewModel vm)
        {
            var lastEditedMonth = element.Years.Select(y => y.Months.Elements.LastOrDefault(m => m.Transactions.Elements.Any(t => t.Payed))).FirstOrDefault();

            if (lastEditedMonth != null)
            {
                var lastEditedYear = lastEditedMonth.Year;

                vm.YearVms = new YearsViewModelFactory(Services).ConvertToVms(new[] { lastEditedYear.Element });
            }
        }
    }
}