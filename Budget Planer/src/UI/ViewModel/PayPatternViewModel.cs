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

        public AffectedMonthViewModel(MonthEnum monthType, bool isSelected)
        {
            MonthType = monthType;
            IsSelected = isSelected;
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
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}