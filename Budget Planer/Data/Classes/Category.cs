using IData.Constants;
using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Category : Element, ICategory, IComparable
    {
        public Category() : base()
        {
            Description = GetDefaultName;
            CategoryType = CategoryType.None;
        }

        public Category(string name, string description, CategoryType categoryType, int sortOrder, bool isMain) : this()
        {
            Description = description;
            CategoryType = categoryType;
            SortOrder = sortOrder;
            Name = name;
            IsMainCategory = isMain;
        }

        public string Description { get; set; }
        public CategoryType CategoryType { get; set; }
        public int SortOrder { get; set; }
        public bool IsMainCategory { get; set; }

        public int CompareTo(object obj)
        {
            var toCompare = obj as ICategory;

            if (SortOrder > toCompare.SortOrder)
            {
                return 1;
            }
            else if (SortOrder == toCompare.SortOrder)
            {
                return 0;
            }
            return -1;
        }
    }
}