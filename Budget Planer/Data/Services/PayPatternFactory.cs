using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Linq;

namespace Data.Services
{
    public class PayPatternFactory : ElementFactory, IPayPatternFactory
    {
        public new IPayPattern GetCreateEmpty()
        {
            var interval = Project.CurrentProject.Intervals.First();
            var payPattern = new PayPattern(interval, MonthEnum.Jan);
            Project.CurrentProject.Elements.AddElement(payPattern);

            return payPattern;
        }

        public IPayPattern Create(IPaymentInterval interval, MonthEnum startsInMonth)
        {
            var payPattern = new PayPattern(interval, startsInMonth);
            Project.CurrentProject.Elements.AddElement(payPattern);

            return payPattern;
        }

        public IPayPattern Copy(IPayPattern original)
        {
            return original;
        }

        public void Delete(IPayPattern toDelete)
        {
            base.Delete(toDelete);
        }
    }
}