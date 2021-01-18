using IData.Constants;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IPayPattern : IElement
    {
        ISaveableXmlElement<IPaymentInterval> Interval { get; set; }
        MonthEnum StartsInMonth { get; set; }
        IEnumerable<MonthEnum> AffectedMonths { get; set; }

        void AddAffectedMonth(MonthEnum month);

        void RemoveAffectedMonth(MonthEnum month);

        void UpdateAffectedMonths();
    }
}