using IData.Interfaces;

namespace UI.Wpf.ViewModel
{
    public class PayPatternViewModel : ElementViewModel<IPayPattern>
    {
        public PayPatternViewModel(IPayPattern element) : base(element)
        {
        }
    }
}