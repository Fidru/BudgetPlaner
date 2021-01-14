using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class YearsViewModelFactory : ViewModelFactoryGeneric<YearViewModel, IYear>
    {
        public YearsViewModelFactory(IRepositoryService repositoryService)
             : base(repositoryService)
        {
        }

        public override YearViewModel CreateVm(IYear element)
        {
            var vm = base.CreateVm(element);

            vm.MonthVms = new MonthViewModelFactory(RepositoryService).ConvertToVms(element.Months.Elements);
            vm.CurrentMonthVm = vm.MonthVms.First();

            foreach (MonthViewModel month in vm.MonthVms)
            {
                month.AlignedMonths = new AlignedMonthsViewModel
                {
                    Current = month,
                    Previous = GetRelatedMonth(vm.MonthVms, month.Element.AlignedMonths.Previous?.Id),
                    Next = GetRelatedMonth(vm.MonthVms, month.Element.AlignedMonths.Next?.Id),
                };
            }

            var categories = RepositoryService.CreatedViewModels.Values.OfType<CategoryViewModel>();
            var months = RepositoryService.CreatedViewModels.Values.OfType<MonthViewModel>();
            var payments = RepositoryService.CreatedViewModels.Values.OfType<PaymentViewModel>();
            var payPatterns = RepositoryService.CreatedViewModels.Values.OfType<PayPatternViewModel>();
            var project = RepositoryService.CreatedViewModels.Values.OfType<ProjectViewModel>();
            var transaction = RepositoryService.CreatedViewModels.Values.OfType<TransactionViewModel>();
            var year = RepositoryService.CreatedViewModels.Values.OfType<YearViewModel>();

            return vm;
        }

        private MonthViewModel GetRelatedMonth(IEnumerable<MonthViewModel> vms, Guid? relatedMonthId)
        {
            if (relatedMonthId.HasValue)
            {
                return vms.SingleOrDefault(x => x.Id == relatedMonthId.Value);
            }
            return null;
        }
    }
}