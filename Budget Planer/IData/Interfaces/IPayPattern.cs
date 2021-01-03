using IData.Constants;
using System;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IPayPattern : IElement
    {
        IPaymentInterval Interval { get; set; }
        Guid IntervalId { get; set; }

        MonthEnum StartsInMonth { get; set; }
        IEnumerable<MonthEnum> AffectedMonths { get; set; }

        void UpdateAffectedMonths();
    }
}