using IData.Interfaces;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel
{
    public class PaymentPatternViewModel : ElementViewModel<IPayPattern>
    {
        public PaymentPatternViewModel(IPayPattern element) : base(element)
        {
        }

        public List<MonthViewModel> Months { get; set; }
    }
}