using IData.Constants;
using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Transaction : Element, ITransaction
    {
        private double _amount;

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

        public double Amount
        {
            get
            {
                return Math.Round(_amount, 2);
            }
            set
            {
                _amount = value;
            }
        }

        public bool Payed { get; set; }

        public ICategory Category
        {
            get { return Payment.Element.Category.Element; }
            set { Payment.Element.Category.Element = value; }
        }

        public CategoryType CategoryType
        {
            get { return Category != null ? Category.CategoryType : CategoryType.None; }
        }

        public ICategory SubCategory
        {
            get { return Payment.Element.SubCategory.Element; }
            set { Payment.Element.SubCategory.Element = value; }
        }

        public CategoryType SubCategoryType
        {
            get { return SubCategory != null ? SubCategory.CategoryType : CategoryType.None; }
        }

        public override void ConnectElements(IProject project)
        {
            Month.Element = (IMonth)project.Months.GetElementById(Month.Id);
            Payment.Element = (IPayment)project.Payments.GetElementById(Payment.Id);
        }

        public override bool CanEdit
        {
            get
            {
                return Category.CategoryType != CategoryType.Bankbalance;
            }
        }
    }
}