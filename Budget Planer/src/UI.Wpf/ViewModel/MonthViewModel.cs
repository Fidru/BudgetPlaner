using IData.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace UI.Wpf.ViewModel
{
    public class YearViewModel : ElementViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IYear Year { get; set; }

        public IEnumerable<IMonth> Months { get; set; }

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

        public void SelectPreviousMonth()
        {
            if (CurrentMonth.AlignedMonths.Previous != null)
            {
                CurrentMonth = CurrentMonth.AlignedMonths.Previous;
                NotifyPropertyChanged("CurrentMonth");
            }
        }
    }

    public class MonthViewModel : ElementViewModel
    {
        public List<TransactionViewModel> TransactionVms { get; set; }

        public List<TransactionViewModel> Bills
        {
            get;
            set;
        }

        public List<TransactionViewModel> Empty { get; set; }

        public AlignedMonthsViewModel AlignedMonths { get; set; }

        public IMonth Month { get; set; }
    }

    public class AlignedMonthsViewModel : ElementViewModel
    {
        public MonthViewModel Current { get; set; }
        public MonthViewModel Next { get; set; }
        public MonthViewModel Previous { get; set; }
    }

    public class ElementViewModel : INotifyPropertyChanged
    {
        public IElement Element { get; set; }

        public Guid Id
        {
            get { return Element.Id; }
        }

        public string Name
        {
            get { return Element.Name; }
            set
            {
                Element.Name = value;
                NotifyPropertyChanged("CurrentMonth");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public class TransactionViewModel : ElementViewModel
    {
        public MonthViewModel MonthVm
        {
            get; set;
        }

        public double Amount
        {
            get
            {
                return Transaction.Amount;
            }
            set
            {
                Transaction.Amount = value;
                Transaction.Month.Element.UpdateBankBalanceEndOfMonth();
                NotifyPropertyChanged("Amount");
            }
        }

        public bool Payed
        {
            get { return Transaction.Payed; }
            set
            {
                Transaction.Payed = value;
                Transaction.Month.Element.UpdateBankBalanceEndOfMonth();
                NotifyPropertyChanged("Payed");
            }
        }

        public ITransaction Transaction { get; set; }
    }
}