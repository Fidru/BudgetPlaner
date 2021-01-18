using IData.Constants;
using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Classes
{
    public class PayPattern : Element, IPayPattern
    {
        public PayPattern(IPaymentInterval interval)
            : this(interval, MonthEnum.Jan)
        {
        }

        public PayPattern(IPaymentInterval interval, MonthEnum startsInMonth)
            : base()
        {
            Interval = new SaveableXmlElement<IPaymentInterval>(interval);
            StartsInMonth = startsInMonth;

            UpdateAffectedMonths();
        }

        public ISaveableXmlElement<IPaymentInterval> Interval { get; set; }
        public MonthEnum StartsInMonth { get; set; }
        public IEnumerable<MonthEnum> AffectedMonths { get; set; }

        public void AddAffectedMonth(MonthEnum month)
        {
            if (AffectedMonths.Contains(month))
            {
                return;
            }

            var newAffectedMonths = new List<MonthEnum>();
            newAffectedMonths.AddRange(AffectedMonths);
            newAffectedMonths.Add(month);

            AffectedMonths = newAffectedMonths;
        }

        public void RemoveAffectedMonth(MonthEnum month)
        {
            if (!AffectedMonths.Contains(month))
            {
                return;
            }

            var newAffectedMonths = new List<MonthEnum>(AffectedMonths);
            newAffectedMonths.Remove(month);

            AffectedMonths = newAffectedMonths;
        }

        public void UpdateAffectedMonths()
        {
            AffectedMonths = new AffectedMonthsCollection(StartsInMonth, Interval.Element.Type);
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            Interval.Element = (IPaymentInterval)project.Intervals.GetElementById(Interval.Element.Id);

            UpdateAffectedMonths();
        }
    }
}