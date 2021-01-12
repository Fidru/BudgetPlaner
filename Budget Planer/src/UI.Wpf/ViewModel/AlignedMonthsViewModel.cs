namespace UI.Wpf.ViewModel
{
    public class AlignedMonthsViewModel : ElementViewModel
    {
        public MonthViewModel Current { get; set; }
        public MonthViewModel Next { get; set; }
        public MonthViewModel Previous { get; set; }
    }
}