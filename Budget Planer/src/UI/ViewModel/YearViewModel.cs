using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.ViewModel
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
    }
}