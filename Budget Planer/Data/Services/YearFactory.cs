using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class YearFactory : ElementFactory, IYearFactory
    {
        public override IElement CreateEmpty()
        {
            var year = new Year();

            //CreateMonths(year);

            Project.CurrentProject.Elements.AddElement(year);

            return year;
        }

        public IYear Create(string name)
        {
            var year = new Year(name);

            CreateMonths(year);

            Project.CurrentProject.Elements.AddElement(year);

            return year;
        }

        private void CreateMonths(IYear year)
        {
            var monthFactory = new MonthFactory() { Project = Project };

            year.Months.AddElement(monthFactory.Create(MonthEnum.Jan, 2202.40));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Feb));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Mar));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Apr));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Mai));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Jun));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Jul));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Aug));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Sep));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Oct));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Nov));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Dez));

            AlligneMonths(year.Months.Elements);
        }

        public void AlligneMonths(IEnumerable<IMonth> months)
        {
            var allMonthsSorted = months.OrderBy(m => (int)m.MonthType);

            UpdateFutureBankBalance(allMonthsSorted);
        }

        public void UpdateFutureBankBalance(IOrderedEnumerable<IMonth> allMonthsSorted)
        {
            foreach (var month in allMonthsSorted)
            {
                month.AlignedMonths = new AlignedMonths(month)
                {
                    Previous = allMonthsSorted.GetRelatedMonth(month, -1),
                    Next = allMonthsSorted.GetRelatedMonth(month, 1)
                };

                if (month.AlignedMonths.Previous != null)
                {
                    month.UpdateBankBalance(month.AlignedMonths.Previous.GetBankBalanceEndOfMonthPayment.Amount);
                }
                month.UpdateBankBalanceEndOfMonth();
            }
        }

        public IYear Copy(IYear original)
        {
            return original;
        }
    }
}