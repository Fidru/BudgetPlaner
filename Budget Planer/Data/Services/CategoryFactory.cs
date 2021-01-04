using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class CategoryFactory : ElementFactory, ICategoryFactory
    {
        public ICategory CreateEmpty()
        {
            var category = new Category();

            Project.CurrentProject.Elements.AddElement(category);
            return category;
        }

        public ICategory Create(string name, string description, CategoryType categoryType, int sortOrder, bool isMain = false)
        {
            var category = new Category(name, description, categoryType, sortOrder, isMain);

            Project.CurrentProject.Elements.AddElement(category);
            return category;
        }

        public ICategory Copy(ICategory original)
        {
            return original;
        }

        public void Delete(ICategory element)
        {
            throw new System.NotImplementedException();
        }
    }
}