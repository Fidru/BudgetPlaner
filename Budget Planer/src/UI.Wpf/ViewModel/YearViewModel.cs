using IData.Interfaces;
using System.ComponentModel;
using System.Linq;

namespace UI.Wpf.ViewModel
{
    public class YearViewModel : INotifyPropertyChanged
    {
        public YearViewModel(IProject project)
        {
            Project = project;

            CurrentYear = Project.Years.First();
            CurrentMonth = CurrentYear.Months.Elements.First();
        }

        public IProject Project { get; set; }

        public IYear CurrentYear { get; set; }

        public IMonth CurrentMonth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SelectNextMonth()
        {
            if (CurrentMonth.AlignedMonths.Next != null)
            {
                CurrentMonth = CurrentMonth.AlignedMonths.Next;
                CurrentMonth.UpdateBankBalanceFromPreviousMonth();
                NotifyPropertyChanged("CurrentMonth");
            }
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void SelectPreviousMonth()
        {
            if (CurrentMonth.AlignedMonths.Previous != null)
            {
                CurrentMonth = CurrentMonth.AlignedMonths.Previous;
                NotifyPropertyChanged("CurrentMonth");
            }
        }
    }
}