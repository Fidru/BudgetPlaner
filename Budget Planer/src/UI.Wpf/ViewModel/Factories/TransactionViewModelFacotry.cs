using IData.Interfaces;

namespace UI.Wpf.ViewModel.Factories
{
    public class TransactionViewModelFacotry : ViewModelFactoryGeneric<TransactionViewModel, ITransaction>
    {
        public override TransactionViewModel CreateVm(ITransaction element)
        {
            var vm = base.CreateVm(element);

            vm.PaymentViewModel = new PaymentViewModelFactory().ConvertToVm(element.Payment.Element);

            return vm;
        }
    }
}