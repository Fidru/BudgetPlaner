using System.Collections.Generic;
using System.Linq;

namespace IData.Constants
{
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
                || paymentInterval == PaymentIntervalType.OneTimePayment
                || paymentInterval == PaymentIntervalType.Custom)
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
            Dictionary<int, MonthEnum> monthsSorted = GetMonthsSorted(startingMonth);

            if (paymentType == PaymentIntervalType.EverySecondMonth)
            {
                AddRange(monthsSorted.Where(m => m.Key % 2 == 0).Select(m => m.Value));
            }
            else if (paymentType == PaymentIntervalType.EverySixthMonth)
            {
                AddRange(monthsSorted.Where(m => m.Key % 6 == 0).Select(m => m.Value));
            }
        }

        private Dictionary<int, MonthEnum> GetMonthsSorted(MonthEnum startingMonth)
        {
            CreateDefaultPayments();
            int startingIndex = this.IndexOf(startingMonth);

            List<MonthEnum> monthsSorted = new List<MonthEnum>();
            monthsSorted.Add(startingMonth);
            monthsSorted.AddRange(this.Where(month => (int)month > (int)startingMonth));
            monthsSorted.AddRange(this.Where(month => (int)month < (int)startingMonth));

            Clear();

            int index = 0;
            Dictionary<int, MonthEnum> monthsDictionary = new Dictionary<int, MonthEnum>();

            foreach (var month in monthsSorted)
            {
                monthsDictionary.Add(index, month);
                index++;
            }

            return monthsDictionary;
        }
    }
}