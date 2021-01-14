using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class TransactionViewModelFacotry : ViewModelFactoryGeneric<TransactionViewModel, ITransaction>
    {
        public TransactionViewModelFacotry(IRepositoryService repositoryService)
            : base(repositoryService)
        {
        }

        public override TransactionViewModel CreateVm(ITransaction element)
        {
            var vm = base.CreateVm(element);

            vm.MonthVm = new MonthViewModelFactory(RepositoryService).ConvertToVm(element.Month.Element);
            vm.PaymentViewModel = new PaymentViewModelFactory(RepositoryService).ConvertToVm(element.Payment.Element);

            return vm;
        }
    }
}