using IData.Constants;
using IData.Interfaces;
using System;
using System.Collections.Generic;

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
            Interval = interval;
            IntervalId = interval.Id;
            StartsInMonth = startsInMonth;

            UpdateAffectedMonths();
        }

        public IPaymentInterval Interval { get; set; }
        public MonthEnum StartsInMonth { get; set; }
        public Guid IntervalId { get; set; }
        public IEnumerable<MonthEnum> AffectedMonths { get; set; }

        public void UpdateAffectedMonths()
        {
            AffectedMonths = new AffectedMonthsCollection(StartsInMonth, Interval.Type);
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            UpdateAffectedMonths();
        }
    }
}