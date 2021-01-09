using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class MonthFactory : ElementFactory, IMonthFactory
    {
        public override IElement CreateEmpty()
        {
            var month = new Month();
            Project.CurrentProject.Elements.AddElement(month);
            return month;
        }

        public IMonth Create(MonthEnum monthType)
        {
            var transactionFactory = new TransactionFactory() { Project = Project };
            var newMonth = new Month(monthType);
            Project.CurrentProject.Elements.AddElement(newMonth);

            var paymentsForMonth = Project.CurrentProject.Payments.GetPaymentsForMonth(monthType);
            transactionFactory.AddTransactions(newMonth, paymentsForMonth);

            return newMonth;
        }

        public IMonth Create(MonthEnum month, double bankBalance)
        {
            var newMonth = Create(month);
            newMonth.UpdateBankBalance(bankBalance);

            return newMonth;
        }

        public IMonth Copy(IMonth original)
        {
            return original;
        }
    }
}