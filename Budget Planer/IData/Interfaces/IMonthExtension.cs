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
            int yearSortOrder = current.Year.Element.SortOrder;
            MonthEnum relatedMont;

            if (relatedMonth <= 0)
            {
                //todo previousYear
                yearSortOrder += -1;
                relatedMont = MonthEnum.Dez;
            }
            else if (relatedMonth > 12)
            {
                // todo nextYear
                yearSortOrder += 1;
                relatedMont = MonthEnum.Jan;
            }
            else
            {
                relatedMont = (MonthEnum)relatedMonth;
            }

            return months.SingleOrDefault(m => m.MonthType == relatedMont && m.Year.Element.SortOrder == yearSortOrder);
        }

        public static IEnumerable<IMonth> GeMonthsForPayment(this IEnumerable<IMonth> months, IPayment payment)
        {
            return months.Where(m => payment.PayPattern.Element.AffectedMonths.Any(affected => affected == m.MonthType));
        }
    }
}