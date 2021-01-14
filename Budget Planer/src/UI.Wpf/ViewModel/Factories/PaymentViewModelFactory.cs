using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class PaymentViewModelFactory : ViewModelFactoryGeneric<PaymentViewModel, IPayment>
    {
        public PaymentViewModelFactory(IRepositoryService repositoryService)
            : base(repositoryService)
        {
        }

        public override PaymentViewModel CreateVm(IPayment element)
        {
            var vm = base.CreateVm(element);

            vm.SelectedCategory = new CategoryViewModelFacotry(RepositoryService).ConvertToVm(element.Category.Element);

            return vm;
        }
    }
}