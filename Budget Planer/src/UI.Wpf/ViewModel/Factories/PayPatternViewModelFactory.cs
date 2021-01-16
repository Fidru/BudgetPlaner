using IData.Interfaces;
using IData.Services;

namespace UI.Wpf.ViewModel.Factories
{
    public class PayPatternViewModelFactory : ViewModelFactoryGeneric<PayPatternViewModel, IPayPattern>
    {
        public PayPatternViewModelFactory(IRepositoryService repositoryService)
           : base(repositoryService)
        {
        }
    }
}