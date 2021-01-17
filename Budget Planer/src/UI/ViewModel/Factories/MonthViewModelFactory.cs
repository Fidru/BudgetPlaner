using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

namespace UI.ViewModel.Factories
{
    public class MonthViewModelFactory : ViewModelFactoryGeneric<MonthViewModel, IMonth>
    {
        public MonthViewModelFactory(IEnumerable<IService> services)
            : base(services)
        {
        }

        public override MonthViewModel CreateVm(IMonth element)
        {
            var vm = base.CreateVm(element);

            vm.TransactionVms = new TransactionViewModelFacotry(Services).ConvertToVms(element.Transactions.Elements);

            vm.TransactionFactory = Services.GetService<ITransactionFactory>();
            vm.PaymentFactory = Services.GetService<IPaymentFactory>();
            vm.Services = Services;

            return vm;
        }
    }
}