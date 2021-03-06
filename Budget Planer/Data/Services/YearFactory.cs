﻿using Data.Classes;
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

            Project.CurrentProject.Elements.AddElement(year);

            return year;
        }

        public IYear Create(string name, bool addOneTimePayments = false)
        {
            var lastYear = Project.CurrentProject.Years.OrderBy(y => y.SortOrder).LastOrDefault();
            int lastSortOrder = lastYear != null ? lastYear.SortOrder : 0;

            var sortOrder = lastSortOrder + 1;
            var year = new Year(name, sortOrder);

            CreateMonths(year, addOneTimePayments);

            Project.CurrentProject.Elements.AddElement(year);

            return year;
        }

        private void CreateMonths(IYear year, bool addOneTimePayments)
        {
            var monthFactory = new MonthFactory() { Project = Project };

            year.Months.AddElement(monthFactory.Create(MonthEnum.Jan, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Feb, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Mar, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Apr, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Mai, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Jun, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Jul, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Aug, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Sep, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Oct, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Nov, year, addOneTimePayments));
            year.Months.AddElement(monthFactory.Create(MonthEnum.Dez, year, addOneTimePayments));

            AlligneMonths();
        }

        public void AlligneMonths()
        {
            IEnumerable<IMonth> months = Project.CurrentProject.Months;
            var allMonthsSorted = months.OrderBy(m => m.Year.Element.SortOrder).ThenBy(m => (int)m.MonthType);

            ConnectRelatedMonths(allMonthsSorted);
        }

        public void ConnectRelatedMonths(IOrderedEnumerable<IMonth> allMonthsSorted)
        {
            foreach (var month in allMonthsSorted)
            {
                month.AlignedMonths = new AlignedMonths(month)
                {
                    Previous = allMonthsSorted.GetRelatedMonth(month, -1),
                    Next = allMonthsSorted.GetRelatedMonth(month, 1)
                };

                month.UpdateBankBalanceFromPreviousMonth();
            }
        }

        public IYear Copy(IYear original)
        {
            return original;
        }
    }
}