namespace IData.Interfaces
{
    public interface IAlignedMonths
    {
        IMonth Current { get; set; }
        IMonth Next { get; set; }
        IMonth Previous { get; set; }
    }
}