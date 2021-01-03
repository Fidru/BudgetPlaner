using IData.Constants;
using System.Collections.Generic;
using System.Linq;

namespace IData.Interfaces
{
    public static class ICategoryExtension
    {
        public static IEnumerable<ICategory> FilterByTypes(this IEnumerable<ICategory> categories, IEnumerable<CategoryType> types)
        {
            return categories.Where(c => types.Contains(c.CategoryType));
        }
    }
}