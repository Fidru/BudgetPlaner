using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Transaction : Element, ITransaction
    {
        public Transaction()
            : base()
        {
        }

        public Transaction(IMonth month, IPayment payment)
            : this()
        {
            Month = month;
            MonthId = month.Id;
            Payment = payment;
            PaymentId = payment.Id;
            Amount = payment.Amount;
            Name = Payment.Name;
        }

        public IMonth Month { get; set; }
        public Guid MonthId { get; set; }
        public IPayment Payment { get; set; }
        public Guid PaymentId { get; set; }
        public double Amount { get; set; }
        public bool Payed { get; set; }

        public IElementGuidPair<ICategory> Category { get { return Payment.Category; } }

        public override void ConnectElements(IProject project)
        {
            Month = (IMonth)project.Months.GetElementById(MonthId);
            Payment = (IPayment)project.Payments.GetElementById(PaymentId);
        }
    }
}