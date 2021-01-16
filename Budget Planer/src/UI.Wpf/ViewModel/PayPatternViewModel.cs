using IData.Interfaces;

namespace UI.Wpf.ViewModel
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

    public class AffectedMonthViewModel
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}