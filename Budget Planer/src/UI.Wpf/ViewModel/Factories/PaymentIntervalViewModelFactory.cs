using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class PaymentIntervalViewModelFactory : ViewModelFactoryGeneric<PaymentIntervalViewModel, IPaymentInterval>
    {
        public PaymentIntervalViewModelFactory(IRepositoryService repositoryService)
            : base(repositoryService)
        {
        }
    }
}