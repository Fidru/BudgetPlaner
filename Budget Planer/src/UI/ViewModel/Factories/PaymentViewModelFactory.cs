using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace UI.ViewModel.Factories
{
    public class PaymentViewModelFactory : ViewModelFactoryGeneric<PaymentViewModel, IPayment>
    {
        public PaymentViewModelFactory(IEnumerable<IService> services)
            : base(services)
        {
        }

        public override PaymentViewModel CreateVm(IPayment element)
        {
            var vm = base.CreateVm(element);

            vm.TransactionFactory = Services.GetService<ITransactionFactory>();

            vm.Categories = new CategoryViewModelFacotry(Services).ConvertToVms(CurrentProject.Categories.WithoutBankBalance());
            vm.SelectedCategory = new CategoryViewModelFacotry(Services).ConvertToVm(element.Category.Element);

            vm.SubCategories = new CategoryViewModelFacotry(Services).ConvertToVms(CurrentProject.SubCategories.WithoutBankBalance());
            vm.SelectedSubCategory = new CategoryViewModelFacotry(Services).ConvertToVm(element.SubCategory.Element);

            var transactions = CurrentProject.Transactions.Where(t => t.Payment.Id == element.Id);
            vm.Transactions = new TransactionViewModelFacotry(Services).ConvertToVms(transactions);

            vm.AllAffectedMonths = GetAffectedMonthViewModels(new AffectedMonthsCollection(), vm);
            vm.AffecctedMonths = GetAffectedMonthViewModels(element.PayPattern.Element.AffectedMonths, vm);

            vm.Intervals = new PaymentIntervalViewModelFactory(Services).ConvertToVms(CurrentProject.Intervals);
            vm.SelectedInterval = new PaymentIntervalViewModelFactory(Services).ConvertToVm(element.PayPattern.Element.Interval.Element);

            return vm;
        }

        private List<AffectedMonthViewModel> GetAffectedMonthViewModels(IEnumerable<MonthEnum> affectedMonths, PaymentViewModel payment)
        {
            var allMonths = new AffectedMonthsCollection();
            return allMonths.Select(m => new AffectedMonthViewModel(m, affectedMonths.Contains(m), payment)).ToList();
        }
    }
}