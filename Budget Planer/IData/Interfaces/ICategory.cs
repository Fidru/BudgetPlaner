using IData.Constants;

namespace IData.Interfaces
{
    public interface ICategory : IElement
    {
        string Description { get; set; }
        CategoryType CategoryType { get; set; }
        int SortOrder { get; set; }
        bool IsMainCategory { get; set; }
    }
}