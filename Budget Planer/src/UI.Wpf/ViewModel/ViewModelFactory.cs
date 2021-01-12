using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class ViewModelFactory
    {
        public ProjectViewModel ConvertVm(IProject project)
        {
            var vm = new ProjectViewModel()
            {
                Project = project,
                YearsVm = ConvertVms(project.Years)
            };

            return vm;
        }

        public List<YearViewModel> ConvertVms(IEnumerable<IYear> years)
        {
            var vms = new List<YearViewModel>();

            foreach (var year in years)
            {
                var months = ConvertVms(year.Months.Elements);

                var vm = new YearViewModel
                {
                    Name = year.Name,
                    MonthsVm = months,
                    Months = year.Months.Elements,
                    CurrentMonth = months.First(),
                    Year = year,
                    Element = year,
                };
                vms.Add(vm);
            }

            return vms;
        }

        public List<MonthViewModel> ConvertVms(IEnumerable<IMonth> months)
        {
            var vms = new List<MonthViewModel>();

            foreach (var month in months)
            {
                vms.Add(ConvertVm(month));
            }

            foreach (var month in vms)
            {
                month.AlignedMonths = new AlignedMonthsViewModel
                {
                    Current = month,
                    Previous = vms.SingleOrDefault(x => x.Month == months.GetRelatedMonth(month.Month, -1)),
                    Next = vms.SingleOrDefault(x => x.Month == months.GetRelatedMonth(month.Month, 1)),
                };
            }

            return vms;
        }

        public MonthViewModel ConvertVm(IMonth month)
        {
            var vm = new MonthViewModel
            {
                Name = month.Name,
                Month = month,
                Element = month
            };

            vm.TransactionVms = ConvertVms(month.Transactions.Elements, vm);

            vm.Bills = vm.TransactionVms.Where(t => month.Bills.Any(b => b.Id == t.Id)).ToList();

            return vm;
        }

        public List<TransactionViewModel> ConvertVms(IEnumerable<ITransaction> transactions, MonthViewModel vm)
        {
            var vms = new List<TransactionViewModel>();

            var data = transactions.Select(t => ConvertVm(t, vm)).OfType<TransactionViewModel>().ToList();

            foreach (var transaction in transactions)
            {
                vms.Add(ConvertVm(transaction, vm));
            }

            return vms;
        }

        public TransactionViewModel ConvertVm(ITransaction transaction, MonthViewModel vm)
        {
            return new TransactionViewModel
            {
                Transaction = transaction,
                Element = transaction,
                MonthVm = vm,
            };
        }
    }
}