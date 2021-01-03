using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface ICategoryFactory : IElementFactory<ICategory>
    {
        ICategory Create(string name, string description, CategoryType categoryType, int sortOrder, bool isMain = false);
    }
}