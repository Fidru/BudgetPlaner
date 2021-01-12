using IData.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace UI.Wpf.ViewModel
{
    public class YearViewModel : ElementViewModel
    {
        public IYear Year { get; set; }

        public List<MonthViewModel> MonthsVm { get; set; }

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
                Year.Months.Elements.Single(m => m.Name == CurrentMonthVm.Name).UpdateBankBalanceFromPreviousMonth();
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