using System.Collections.Generic;
using System.Linq;

namespace IData.Constants
{
    public enum MonthEnum
    {
        Jan = 1,
        Feb = 2,
        Mar = 3,
        Apr = 4,
        Mai = 5,
        Jun = 6,
        Jul = 7,
        Aug = 8,
        Sep = 9,
        Oct = 10,
        Nov = 11,
        Dez = 12
    }

    public static class MonthEnumExtension
    {
        public static string ConvertToText(this MonthEnum month)
        {
            switch (month)
            {
                case (MonthEnum.Jan):
                    return "Jan";

                case (MonthEnum.Feb):
                    return "Feb";

                case (MonthEnum.Mar):
                    return "Mar";

                case (MonthEnum.Apr):
                    return "Apr";

                case (MonthEnum.Mai):
                    return "Mai";

                case (MonthEnum.Jun):
                    return "Jun";

                case (MonthEnum.Jul):
                    return "Jul";

                case (MonthEnum.Aug):
                    return "Aug";

                case (MonthEnum.Sep):
                    return "Sep";

                case (MonthEnum.Oct):
                    return "Oct";

                case (MonthEnum.Nov):
                    return "Nov";

                default:
                    return "Dez";
            }
        }
    }

    public class AffectedMonthsCollection : List<MonthEnum>
    {
        public AffectedMonthsCollection()
        {
            CreateDefaultPayments();
        }

        public AffectedMonthsCollection(MonthEnum startingMonth, PaymentIntervalType paymentInterval)
        {
            if (paymentInterval == PaymentIntervalType.Monthly)
            {
                CreateDefaultPayments();
            }
            else if (paymentInterval == PaymentIntervalType.Yearly
                || paymentInterval == PaymentIntervalType.OneTimePayment)
            {
                Clear();
                Add(startingMonth);
            }
            else
            {
                GetPatternPayment(startingMonth, paymentInterval);
            }
        }

        public void CreateDefaultPayments()
        {
            Clear();
            Add(MonthEnum.Jan);
            Add(MonthEnum.Feb);
            Add(MonthEnum.Mar);
            Add(MonthEnum.Apr);
            Add(MonthEnum.Mai);
            Add(MonthEnum.Jun);
            Add(MonthEnum.Jul);
            Add(MonthEnum.Aug);
            Add(MonthEnum.Sep);
            Add(MonthEnum.Oct);
            Add(MonthEnum.Nov);
            Add(MonthEnum.Dez);
        }

        public void GetPatternPayment(MonthEnum startingMonth, PaymentIntervalType paymentType)
        {
            CreateDefaultPayments();
            int startingIndex = this.IndexOf(startingMonth);

            List<MonthEnum> collectionSorted = new List<MonthEnum>();
            collectionSorted.Add(startingMonth);
            collectionSorted.AddRange(this.Where(month => (int)month > (int)startingMonth));
            collectionSorted.AddRange(this.Where(month => (int)month < (int)startingMonth));

            int index = 0;
            Dictionary<int, MonthEnum> monthsDictionary = new Dictionary<int, MonthEnum>();

            foreach (var month in collectionSorted)
            {
                monthsDictionary.Add(index, month);
                index++;
            }

            Clear();

            if (paymentType == PaymentIntervalType.EverySecondMonth)
            {
                AddRange(monthsDictionary.Where(m => m.Key % 2 == 0).Select(m => m.Value));
            }
            else if (paymentType == PaymentIntervalType.EverySixthMonth)
            {
                AddRange(monthsDictionary.Where(m => m.Key % 6 == 0).Select(m => m.Value));
            }
        }
    }
}