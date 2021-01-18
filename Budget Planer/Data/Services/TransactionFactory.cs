using Data.Classes;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class TransactionFactory : ElementFactory, ITransactionFactory
    {
        public override IElement CreateEmpty()
        {
            var transaction = new Transaction();

            Project.CurrentProject.Elements.AddElement(transaction);
            return transaction;
        }

        public ITransaction Copy(ITransaction original)
        {
            return original;
        }

        public ITransaction Create(IMonth month, IPayment payment)
        {
            var transaction = new Transaction(month, payment);
            month.Transactions.AddElement(transaction);
            Project.CurrentProject.Elements.AddElement(transaction);

            return transaction;
        }

        public void AddTransactions(IMonth month, IEnumerable<IPayment> paymentsForMonth)
        {
            foreach (IPayment payment in paymentsForMonth)
            {
                UpdateTransactions(payment, new[] { month }, month);
            }
        }

        public void UpdatePayment(IPayment payment, IMonth month)
        {
            var months = payment.IsOneTimePayment ?
                new[] { month } :
                Project.CurrentProject.Months.GeMonthsForPayment(payment);

            UpdateTransactions(payment, months, month);
        }

        private IEnumerable<IMonth> GetFutureMonths(IEnumerable<IMonth> months, IMonth currentMonth)
        {
            var futureMonths = months.Where(m => m.Year.Element.SortOrder >= currentMonth.Year.Element.SortOrder
            && (int)m.MonthType >= (int)currentMonth.MonthType).ToArray();

            return futureMonths;
        }

        private void UpdateTransactions(IPayment payment, IEnumerable<IMonth> months, IMonth currentMonth)
        {
            UpdateProjectMonths(payment, months, currentMonth);
            RemoveOldTransactions(payment);
        }

        private void UpdateProjectMonths(IPayment payment, IEnumerable<IMonth> months, IMonth currentMonth)
        {
            var updateMonths = new List<IMonth>();

            var futureMonths = GetFutureMonths(months, currentMonth);
            var oldMonths = months.Except(futureMonths);

            foreach (var month in futureMonths)
            {
                var transaction = month.Transactions.Elements.SingleOrDefault(t => t.Payment.Id == payment.Id);
                if (transaction != null)
                {
                    transaction.Payment.Element = payment;
                    transaction.Amount = payment.Amount;
                    transaction.Name = payment.Name;
                    transaction.IsDeleted = false;
                }
                else
                {
                    updateMonths.Add(month);
                }
            }

            updateMonths.ForEach(m => m.AddTransaction(Create(m, payment)));
        }

        private void RemoveOldTransactions(IPayment payment)
        {
            var newAffectedMonths = payment.PayPattern.Element.AffectedMonths;
            IEnumerable<ITransaction> projectTransactions = Project.CurrentProject.Transactions.GetTransactionsByPayment(payment);

            if (!newAffectedMonths.Any())
            {
                payment.Delete();
                projectTransactions.ToList().ForEach(t => t.Delete());
            }

            var removedTransactions = projectTransactions.Where(t => !newAffectedMonths.Contains(t.Month.Element.MonthType));

            removedTransactions.ToList().ForEach(t => t.Delete());
        }
    }
}