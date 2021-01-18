using IData.Constants;
using IData.Interfaces;
using System.ComponentModel;

namespace UI.ViewModel
{
    public class PayPatternViewModel : ElementViewModel<IPayPattern>
    {
        public PayPatternViewModel(IPayPattern element) : base(element)
        {
        }
    }

    public class PaymentIntervalViewModel : ElementViewModel<IPaymentInterval>
    {
        public PaymentIntervalViewModel(IPaymentInterval element) : base(element)
        {
        }
    }

    public class AffectedMonthViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;

        public AffectedMonthViewModel(MonthEnum monthType, bool isSelected, PaymentViewModel payment)
        {
            MonthType = monthType;
            IsSelected = isSelected;
            Payment = payment;
        }

        public MonthEnum MonthType { get; set; }

        public string Name
        {
            get { return MonthType.ConvertToText(); }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;

                if (Payment != null)
                {
                    if (value)
                    {
                        Payment.Element.PayPattern.Element.AddAffectedMonth(MonthType);
                    }
                    else
                    {
                        Payment.Element.PayPattern.Element.RemoveAffectedMonth(MonthType);
                    }

                    Payment.SetToCustomInterval();
                }
            }
        }

        public PaymentViewModel Payment { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            NotifyPropertyChanged("IsSelected");
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}