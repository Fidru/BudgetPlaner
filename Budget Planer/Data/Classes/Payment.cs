using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Payment : Element, IPayment
    {
        public Payment(IPaymentInterval interval) : base()
        {
            Category = new Category();
            SubCategory = new Category();
            PayPattern = new PayPattern(interval);

            Amount = 0.0;

            CategoryId = Category.Id;
            SubCategoryId = SubCategory.Id;
            PayPatternId = PayPattern.Id;
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            if (Name == "Bank balance end of Month")
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

        public ICategory Category { get; set; }
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