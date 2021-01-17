using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace UI.ViewModel.Factories
{
    public class YearsViewModelFactory : ViewModelFactoryGeneric<YearViewModel, IYear>
    {
        public YearsViewModelFactory(IEnumerable<IService> services)
            : base(services)
        {
        }

        public override YearViewModel CreateVm(IYear element)
        {
            var vm = base.CreateVm(element);

            vm.MonthVms = new MonthViewModelFactory(Services).ConvertToVms(element.Months.Elements);
            vm.CurrentMonthVm = vm.MonthVms.First();

            return vm;
        }
    }
}