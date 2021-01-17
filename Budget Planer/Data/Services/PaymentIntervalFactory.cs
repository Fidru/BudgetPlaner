using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class PaymentIntervalFactory : ElementFactory, IPaymentIntervalFactory
    {
        public override IElement CreateEmpty()
        {
            var interval = new PaymentInterval();
            Project.CurrentProject.Elements.AddElement(interval);

            return interval;
        }

        public IPaymentInterval Create(PaymentIntervalType intervalType)
        {
            var interval = new PaymentInterval(intervalType);
            Project.CurrentProject.Elements.AddElement(interval);

            return interval;
        }

        public IPaymentInterval Copy(IPaymentInterval original)
        {
            return original;
        }
    }
}