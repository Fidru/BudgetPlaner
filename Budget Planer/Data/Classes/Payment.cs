using IData.Constants;
using IData.Interfaces;
using System.Linq;

namespace Data.Classes
{
    public class Payment : Element, IPayment
    {
        public Payment(ICategory category, IPayPattern pattern)
            : base()
        {
            Category = new SaveableXmlElement<ICategory>() { Element = category };
            SubCategory = new SaveableXmlElement<ICategory>();
            PayPattern = new SaveableXmlElement<IPayPattern>(pattern);

            Amount = 0.0;
        }

        public Payment(string name, ICategory category, double amount, IPayPattern payPattern, ICategory subCategory)
            : base()
        {
            Name = name;

            Category = new SaveableXmlElement<ICategory>(category);
            SubCategory = new SaveableXmlElement<ICategory>(subCategory);
            PayPattern = new SaveableXmlElement<IPayPattern>(payPattern);

            Amount = amount;
        }

        public ISaveableXmlElement<ICategory> Category { get; set; }

        public double Amount { get; set; }
        public bool Payed { get; set; }
        public ISaveableXmlElement<ICategory> SubCategory { get; set; }
        public ISaveableXmlElement<IPayPattern> PayPattern { get; set; }
        public IMonth Month { get; set; }

        public CategoryType CategoryType => Category.Element.CategoryType;

        public CategoryType SubCategoryType => SubCategory.Element.CategoryType;

        public bool IsOneTimePayment => PayPattern.Element.Interval.Element.Type == PaymentIntervalType.OneTimePayment;

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            Category.Element = (ICategory)project.Categories.GetElementById(Category.Id);
            SubCategory.Element = (ICategory)project.SubCategories.GetElementById(SubCategory.Id);
            PayPattern.Element = (IPayPattern)project.PayPatterns.GetElementById(PayPattern.Id);

            if (PayPattern.Element == null)
            {
                PayPattern.Element = project.PayPatterns.FirstOrDefault(p => p.Interval.Element.Type == PaymentIntervalType.OneTimePayment);
            }
        }

        public override void Delete()
        {
            base.Delete();

            if (PayPattern.Element.Interval.Element.Type == PaymentIntervalType.OneTimePayment)
            {
                var transactions = Month.Transactions.Elements.Where(t => t.Payment.Id == Id);
                transactions.ToList().ForEach(t => t.Delete());
            }
            else if (PayPattern.Element.Interval.Element.Type == PaymentIntervalType.Custom)
            {
            }
        }

        public void DeleteAllTransactions()
        {
            var transactions = Month.Transactions.Elements.Where(t => t.Payment.Id == Id);
            transactions.ToList().ForEach(t => t.Delete());
        }
    }
}