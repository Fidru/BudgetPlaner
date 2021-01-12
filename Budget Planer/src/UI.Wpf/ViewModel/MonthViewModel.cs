using IData.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace UI.Wpf.ViewModel
{
    public class YearViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IYear Year { get; set; }

        public IEnumerable<IMonth> Months { get; set; }

        public string Name { get; set; }

        public List<MonthViewModel> MonthsVm { get; set; }

        public MonthViewModel CurrentMonth { get; set; }

        public void SelectNewCurrentMonth(MonthViewModel month)
        {
            CurrentMonth = month;
            NotifyPropertyChanged("CurrentMonth");
        }

        public void SelectNextMonth()
        {
            if (CurrentMonth.AlignedMonths.Next != null)
            {
                CurrentMonth = CurrentMonth.AlignedMonths.Next;
                Months.Single(m => m.Name == CurrentMonth.Name).UpdateBankBalanceFromPreviousMonth();
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

    public class MonthViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<TransactionViewModel> TransactionVms { get; set; }

        public List<TransactionViewModel> Bills { get; set; }
        public List<TransactionViewModel> Empty { get; set; }

        public AlignedMonthsViewModel AlignedMonths { get; set; }

        public string Name { get; set; }
        public IMonth Month { get; set; }
    }

    public class AlignedMonthsViewModel : INotifyPropertyChanged
    {
        public MonthViewModel Current { get; set; }
        public MonthViewModel Next { get; set; }
        public MonthViewModel Previous { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class TransactionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; set; }
        public string Name { get; set; }

        public double Amount { get; set; }

        public bool Payed { get; set; }
        public ITransaction Transaction { get; set; }
    }
}