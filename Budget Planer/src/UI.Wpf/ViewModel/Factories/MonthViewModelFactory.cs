using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class MonthViewModelFactory : ViewModelFactoryGeneric<MonthViewModel, IMonth>
    {
        public MonthViewModelFactory(IRepositoryService repositoryService) : base(repositoryService)
        {
        }

        public override MonthViewModel CreateVm(IMonth element)
        {
            var vm = base.CreateVm(element);

            vm.TransactionVms = new TransactionViewModelFacotry(RepositoryService).ConvertToVms(element.Transactions.Elements);
            //vm.TransactionVms.ForEach(t => t.MonthVm = vm);

            vm.Bills = vm.TransactionVms.GetFilteredViewModels(element.Bills);
            vm.FoodPayments = vm.TransactionVms.GetFilteredViewModels(element.FoodPayments);
            vm.CreditCardPayments = vm.TransactionVms.GetFilteredViewModels(element.CreditCardPayments);
            vm.ExpectedUnexpectedPayments = vm.TransactionVms.GetFilteredViewModels(element.ExpectedUnexpectedPayments);

            return vm;
        }
    }
}