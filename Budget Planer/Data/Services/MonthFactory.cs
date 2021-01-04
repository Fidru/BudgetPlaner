using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class MonthFactory : ElementFactory, IMonthFactory
    {
        public IMonth CreateEmpty()
        {
            var month = new Month();
            Project.CurrentProject.Elements.AddElement(month);
            return month;
        }

        public IMonth Create(MonthEnum monthType)
        {
            var transactionFactory = new TransactionFactory() { Project = Project };
            var newMonth = new Month(monthType);
            var paymentsForMonth = Project.CurrentProject.Payments.GetPaymentsForMonth(monthType);

            transactionFactory.AddTransactions(newMonth, paymentsForMonth);

            Project.CurrentProject.Elements.AddElement(newMonth);
            return newMonth;
        }

        public IMonth Create(MonthEnum month, double bankBalance)
        {
            var newMonth = Create(month);
            newMonth.Bankbalance = bankBalance;

            return newMonth;
        }

        public IMonth Copy(IMonth original)
        {
            return original;
        }

        public void Delete(IMonth element)
        {
            throw new System.NotImplementedException();
        }
    }
}