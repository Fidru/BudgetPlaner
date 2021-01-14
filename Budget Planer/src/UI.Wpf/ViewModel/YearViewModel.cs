using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class YearViewModel : ElementViewModel<IYear>
    {
        public YearViewModel(IYear element) : base(element)
        {
        }

        public List<MonthViewModel> MonthVms { get; set; }

        public MonthViewModel CurrentMonthVm { get; set; }

        public void SelectNewCurrentMonth(MonthViewModel month)
        {
            CurrentMonthVm = month;
            NotifyPropertyChanged(nameof(CurrentMonthVm));
        }

        public void SelectNextMonth()
        {
            if (CurrentMonthVm.AlignedMonths.Next != null)
            {
                CurrentMonthVm = CurrentMonthVm.AlignedMonths.Next;
                Element.Months.Elements.Single(m => m.Name == CurrentMonthVm.Name).UpdateBankBalanceFromPreviousMonth();
                NotifyPropertyChanged(nameof(CurrentMonthVm));
            }
        }

        public void SelectPreviousMonth()
        {
            if (CurrentMonthVm.AlignedMonths.Previous != null)
            {
                CurrentMonthVm = CurrentMonthVm.AlignedMonths.Previous;
                NotifyPropertyChanged(nameof(CurrentMonthVm));
            }
        }
    }
}