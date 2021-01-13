using IData.Interfaces;

namespace UI.Wpf.ViewModel.Factories
{
    public class MonthViewModelFactory : ViewModelFactoryGeneric<MonthViewModel, IMonth>
    {
        public override MonthViewModel CreateVm(IMonth element)
        {
            var vm = new MonthViewModel(element);

            vm.TransactionVms = new TransactionViewModelFacotry().ConvertToVms(element.Transactions.Elements);
            vm.TransactionVms.ForEach(t => t.MonthVm = vm);

            vm.Bills = vm.TransactionVms.GetFilteredViewModels(element.Bills);
            vm.FoodPayments = vm.TransactionVms.GetFilteredViewModels(element.FoodPayments);
            vm.CreditCardPayments = vm.TransactionVms.GetFilteredViewModels(element.CreditCardPayments);
            vm.ExpectedUnexpectedPayments = vm.TransactionVms.GetFilteredViewModels(element.ExpectedUnexpectedPayments);

            return vm;
        }
    }
}