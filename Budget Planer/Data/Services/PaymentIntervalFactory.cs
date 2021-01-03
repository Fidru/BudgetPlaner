using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class PaymentIntervalFactory : ElementFactory, IPaymentIntervalFactory
    {
        public IPaymentInterval CreateEmpty()
        {
            var payPattern = new PaymentInterval();
            Project.CurrentProject.Elements.AddElement(payPattern);

            return payPattern;
        }

        public IPaymentInterval Create(PaymentIntervalType paymentInterval)
        {
            var payPattern = new PaymentInterval(paymentInterval);
            Project.CurrentProject.Elements.AddElement(payPattern);

            return payPattern;
        }

        public IPaymentInterval Copy(IPaymentInterval original)
        {
            return original;
        }
    }
}