using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Payment : Element, IPayment
    {
        private ICategory _category;

        public Payment(IPaymentInterval interval) : base()
        {
            PayPattern = new PayPattern(interval);
            PayPatternId = PayPattern.Id;

            Amount = 0.0;
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            if (Name.Contains("billa"))
            {
            }

            var found = (ICategory)project.Categories.GetElementById(CategoryId);

            Category = found;
            PayPattern = (IPayPattern)project.PayPatterns.GetElementById(PayPatternId);
            if (SubCategoryId != Guid.Empty)
            {
                SubCategory = (ICategory)project.SubCategories.GetElementById(SubCategoryId);
            }
            else
            {
                SubCategory = null;
            }
        }

        public Payment(string name, ICategory category, double amount, IPayPattern payPattern, ICategory subCategory)
        {
            Name = name;
            Category = category;
            SubCategory = subCategory;
            PayPattern = payPattern;

            Amount = amount;

            CategoryId = Category.Id;
            SubCategoryId = SubCategory?.Id ?? Guid.Empty;
            PayPatternId = PayPattern.Id;
        }

        public ICategory Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                CategoryId = value.Id;
            }
        }

        public double Amount { get; set; }
        public IPayPattern PayPattern { get; set; }
        public Guid PayPatternId { get; set; }
        public Guid CategoryId { get; set; }
        public bool Payed { get; set; }
        public ICategory SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
        public IMonth Month { get; set; }
    }
}