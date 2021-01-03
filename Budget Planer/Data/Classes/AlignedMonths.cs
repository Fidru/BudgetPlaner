using IData.Interfaces;

namespace Data.Classes
{
    public class AlignedMonths : IAlignedMonths
    {
        public AlignedMonths(IMonth current)
        {
            Current = current;
        }

        public IMonth Current { get; set; }
        public IMonth Next { get; set; }
        public IMonth Previous { get; set; }
    }
}