using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Payment : Element, IPayment
    {
        public Payment(IPaymentInterval interval) : base()
        {
            PayPattern = new ElementGuidPair<IPayPattern>(new PayPattern(interval));
            Amount = 0.0;
        }

        public Payment(string name, ICategory category, double amount, IPayPattern payPattern, ICategory subCategory)
        {
            Name = name;
            Category = new ElementGuidPair<ICategory>(category);
            SubCategory = new ElementGuidPair<ICategory>(subCategory);
            PayPattern = new ElementGuidPair<IPayPattern>(payPattern);

            Amount = amount;
        }

        public IElementGuidPair<ICategory> Category { get; set; }

        public double Amount { get; set; }
        public bool Payed { get; set; }
        public IElementGuidPair<ICategory> SubCategory { get; set; }
        public IElementGuidPair<IPayPattern> PayPattern { get; set; }
        public IMonth Month { get; set; }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            Category.Element = (ICategory)project.Categories.GetElementById(Category.Id);
            PayPattern.Element = (IPayPattern)project.PayPatterns.GetElementById(PayPattern.Id);

            SubCategory.Element = (ICategory)project.SubCategories.GetElementById(SubCategory.Id);
        }
    }
}