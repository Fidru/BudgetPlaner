using Data.Classes;
using IData.Interfaces;
using IData.Services;
using System.Linq;

namespace Data.Services
{
    public class PaymentFactory : ElementFactory, IPaymentFactory
    {
        public new IPayment CreateEmpty()
        {
            var interval = Project.CurrentProject.Intervals.First();
            var payment = new Payment(interval);
            Project.CurrentProject.Elements.AddElement(payment);
            return payment;
        }

        public IPayment Create(string name, ICategory category, double amount, IPayPattern payPattern, ICategory subCategory = null)
        {
            var payment = new Payment(name, category, amount, payPattern, subCategory);
            Project.CurrentProject.Elements.AddElement(payment);
            return payment;
        }

        public IPayment Copy(IPayment original)
        {
            var newPayment = CreateEmpty();

            newPayment.Name = original.Name;
            newPayment.PayPattern = original.PayPattern;
            newPayment.Category = original.Category;
            newPayment.SubCategory = original.SubCategory;

            newPayment.Amount = original.Amount;

            return newPayment;
        }

        public void Delete(IPayment element)
        {
        }
    }
}