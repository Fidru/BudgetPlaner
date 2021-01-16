using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel.Factories
{
    public class PaymentIntervalViewModelFactory : ViewModelFactoryGeneric<PaymentIntervalViewModel, IPaymentInterval>
    {
        public PaymentIntervalViewModelFactory(IEnumerable<IService> services)
            : base(services)
        {
        }
    }
}