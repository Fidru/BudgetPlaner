using IData.Constants;
using System.Collections.Generic;
using System.Linq;

namespace IData.Interfaces
{
    public static class IMonthExtension
    {
        public static IMonth GetRelatedMonth(this IEnumerable<IMonth> months, IMonth current, int increment)
        {
            int relatedMonth = (int)current.MonthType + increment;

            MonthEnum relatedMont;

            if (relatedMonth <= 0)
            {
                return null;
                relatedMont = MonthEnum.Dez;
            }
            else if (relatedMonth > 12)
            {
                return null;
                relatedMont = MonthEnum.Jan;
            }
            else
            {
                relatedMont = (MonthEnum)relatedMonth;
            }

            return months.Single(m => m.MonthType == relatedMont);
        }

        public static IEnumerable<IMonth> GeMonthsForPayment(this IEnumerable<IMonth> months, IPayment payment)
        {
            return months.Where(m => payment.PayPattern.Element.AffectedMonths.Any(affected => affected == m.MonthType));
        }
    }
}