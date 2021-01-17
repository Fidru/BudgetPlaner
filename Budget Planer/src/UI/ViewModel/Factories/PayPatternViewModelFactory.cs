using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

namespace UI.ViewModel.Factories
{
    public class PayPatternViewModelFactory : ViewModelFactoryGeneric<PayPatternViewModel, IPayPattern>
    {
        public PayPatternViewModelFactory(IEnumerable<IService> services)
            : base(services)
        {
        }
    }
}