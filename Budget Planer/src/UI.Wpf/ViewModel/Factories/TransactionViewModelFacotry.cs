using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel.Factories
{
    public class TransactionViewModelFacotry : ViewModelFactoryGeneric<TransactionViewModel, ITransaction>
    {
        public TransactionViewModelFacotry(IEnumerable<IService> services)
            : base(services)
        {
        }

        public override TransactionViewModel CreateVm(ITransaction element)
        {
            var vm = base.CreateVm(element);

            vm.MonthVm = new MonthViewModelFactory(Services).ConvertToVm(element.Month.Element);
            vm.PaymentViewModel = new PaymentViewModelFactory(Services).ConvertToVm(element.Payment.Element);

            return vm;
        }
    }
}