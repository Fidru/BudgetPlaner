﻿using IData.Interfaces;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class PaymentViewModelFactory : ViewModelFactoryGeneric<PaymentViewModel, IPayment>
    {
        public override PaymentViewModel CreateVm(IPayment element)
        {
            var vm = new PaymentViewModel(element);

            vm.SelectedCategory = new CategoryViewModelFacotry().ConvertToVm(element.Category.Element);

            return vm;
        }
    }

    public class YearsViewModelFactory : ViewModelFactoryGeneric<YearViewModel, IYear>
    {
        public override YearViewModel CreateVm(IYear element)
        {
            var months = new MonthViewModelFactory().ConvertToVms(element.Months.Elements);

            foreach (MonthViewModel month in months)
            {
                month.AlignedMonths = new AlignedMonthsViewModel
                {
                    Current = month,
                    Previous = months.SingleOrDefault(x => x.Element == element.Months.Elements.GetRelatedMonth(month.Element, -1)),
                    Next = months.SingleOrDefault(x => x.Element == element.Months.Elements.GetRelatedMonth(month.Element, 1)),
                };
            }

            var vm = new YearViewModel(element)
            {
                MonthsVm = months,
                CurrentMonthVm = months.First(),
            };

            return vm;
        }
    }
}