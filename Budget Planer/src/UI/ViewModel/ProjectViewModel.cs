using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;
using UI.ViewModel.Factories;
using XmlSaver.Save;

namespace UI.ViewModel
{
    public class ProjectViewModel : ElementViewModel<IProject>
    {
        public IEnumerable<IService> Services { get; set; }

        public ProjectViewModel(IProject element) : base(element)
        {
        }

        public List<YearViewModel> YearVms { get; set; }

        public YearViewModel CurrentYear { get; set; }

        public string CombinedMonthName
        {
            get
            {
                return $"{CurrentYear.Name} - {CurrentYear.CurrentMonthVm.Name}";
            }
            set
            {
                CurrentYear.Name = value;
            }
        }

        public List<CategoryViewModel> CategorieVms { get; set; }
        public List<CategoryViewModel> SubCategorieVms { get; set; }
        public List<PayPatternViewModel> PayPatternVms { get; set; }

        public void SaveToXml()
        {
            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(Element);
        }

        public bool SelectNextMonth()
        {
            var nextMonth = CurrentYear.CurrentMonthVm.AlignedMonths.Next;

            if (nextMonth != null)
            {
                nextMonth.Element.UpdateBankBalanceFromPreviousMonth();
                SetNewMonthAndYear(nextMonth);

                return true;
            }
            return false;
        }

        public bool SelectPreviousMonth()
        {
            var previousMonth = CurrentYear.CurrentMonthVm.AlignedMonths.Previous;

            if (previousMonth != null)
            {
                SetNewMonthAndYear(previousMonth);

                return true;
            }
            return false;
        }

        private void SetNewMonthAndYear(MonthViewModel newMonth)
        {
            CurrentYear = newMonth.Year;
            CurrentYear.CurrentMonthVm = newMonth;

            CurrentYear.CurrentMonthVm.UpdateLists();

            NotifyPropertyChanged(nameof(CurrentYear));
            NotifyPropertyChanged(CurrentYear, nameof(CurrentYear.CurrentMonthVm));

            NotifyPropertyChanged(nameof(CombinedMonthName));
        }

        public void UpdateViewModels()
        {
            // New Monthly Transactions only get added to the Current ViewModel, when the Month is clicked
            // If a future Month gets selected through the main menu, the new Transaction viewModels
            // have not been created yet

            var filtered = Element.Transactions.Where(t => t.IsNew);

            foreach (var transaction in filtered)
            {
                if (transaction.IsNew)
                {
                    var transactionVm = new TransactionViewModelFacotry(Services).ConvertToVm(transaction);

                    transactionVm.MonthVm.AddTransaction(transactionVm);
                    transactionVm.PaymentViewModel.AddTransaction(transactionVm);
                    transaction.IsNew = false;
                }
            }
        }

        public void AddYear()
        {
            var yearFactory = Services.GetService<IYearFactory>();
            var year = yearFactory.Create("New Year");
            var newYear = new YearsViewModelFactory(Services).ConvertToVm(year);

            var lastMonth = YearVms.OrderBy(y => y.Element.SortOrder).Last().MonthVms.OrderBy(m => (int)m.Element.MonthType).Last();
            lastMonth.AlignedMonths.Next = newYear.MonthVms.OrderBy(m => (int)m.Element.MonthType).First();

            YearVms.Add(newYear);
        }
    }
}