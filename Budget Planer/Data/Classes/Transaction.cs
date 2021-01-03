using IData.Constants;
using IData.Interfaces;

namespace Data.Classes
{
    public class Transaction : Element, ITransaction
    {
        public Transaction()
            : base()
        {
            Month = new SaveableXmlElement<IMonth>();
            Payment = new SaveableXmlElement<IPayment>();
        }

        public Transaction(IMonth month, IPayment payment)
            : this()
        {
            Month.Element = month;
            Payment.Element = payment;
            Amount = payment.Amount;
            Name = Payment.Element.Name;
        }

        public ISaveableXmlElement<IMonth> Month { get; set; }
        public ISaveableXmlElement<IPayment> Payment { get; set; }
        public double Amount { get; set; }
        public bool Payed { get; set; }

        public ICategory Category { get { return Payment.Element.Category.Element; } }
        public CategoryType CategoryType { get { return Category.CategoryType; } }

        public ICategory SubCategory { get { return Payment.Element.SubCategory.Element; } }
        public CategoryType SubCategoryType { get { return SubCategory.CategoryType; } }

        public override void ConnectElements(IProject project)
        {
            Month.Element = (IMonth)project.Months.GetElementById(Month.Id);
            Payment.Element = (IPayment)project.Payments.GetElementById(Payment.Id);
        }
    }
}