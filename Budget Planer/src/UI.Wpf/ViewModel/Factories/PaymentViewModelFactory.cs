using IData.Interfaces;
using IData.Services;
using System.Linq;

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

            vm.Categories = new CategoryViewModelFacotry(RepositoryService).ConvertToVms(CurrentProject.Categories);
            vm.SelectedCategory = new CategoryViewModelFacotry(RepositoryService).ConvertToVm(element.Category.Element);

            vm.SubCategories = new CategoryViewModelFacotry(RepositoryService).ConvertToVms(CurrentProject.SubCategories);
            vm.SelectedSubCategory = new CategoryViewModelFacotry(RepositoryService).ConvertToVm(element.SubCategory.Element);

            vm.Intervals = new PaymentIntervalViewModelFactory(RepositoryService).ConvertToVms(CurrentProject.Intervals);
            vm.SelectedInterval = new PaymentIntervalViewModelFactory(RepositoryService).ConvertToVm(element.PayPattern.Element.Interval.Element);

            var transactions = CurrentProject.Transactions.Where(t => t.Payment.Id == element.Id);
            vm.Transactions = new TransactionViewModelFacotry(RepositoryService).ConvertToVms(transactions);

            return vm;
        }
    }
}