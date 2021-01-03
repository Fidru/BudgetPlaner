using IData.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IData.Interfaces
{
    public static class IElementExtension
    {
        public static string ConvertToStringIds(this IEnumerable<MonthEnum> items)
        {
            return string.Join(";", items.Select(i => (int)i));
        }

        public static string ConvertToStringIds(this IEnumerable<IIdentifier> items)
        {
            return string.Join(";", items.Select(i => i.Id));
        }

        public static IEnumerable<Guid> ConvertStringIdsToGuids(this string stringId)
        {
            if (string.IsNullOrEmpty(stringId))
                return new List<Guid>();

            var guids = stringId.Split(';').ToList().Select(id => Guid.Parse(id));

            return guids;
        }

        public static T GetByName<T>(this IEnumerable<IElement> elements, string elementName)
        {
            return (T)elements.Single(c => c.Name == elementName);
        }

        public static ICategory GetByType(this IEnumerable<ICategory> categories, CategoryType type)
        {
            return categories.Single(c => c.CategoryType == type);
        }

        public static IEnumerable<IIdentifier> GetElementsByIds(this IEnumerable<IIdentifier> elements, string ids)
        {
            var guids = ConvertStringIdsToGuids(ids);

            return elements.Where(e => guids.Contains(e.Id));
        }

        public static IIdentifier GetElementById(this IEnumerable<IIdentifier> elements, Guid id)
        {
            return elements.SingleOrDefault(e => e.Id == id);
        }
    }
}