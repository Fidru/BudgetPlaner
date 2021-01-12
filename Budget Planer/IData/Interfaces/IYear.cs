namespace IData.Interfaces
{
    public interface IYear : IElement
    {
        IElementCollection<IMonth> Months { get; set; }
        int SortOrder { get; set; }
    }
}