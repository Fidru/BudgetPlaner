using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class TransactionFactory : ElementFactory, ITransactionFactory
    {
        public new ITransaction GetCreateEmpty()
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
            Project.CurrentProject.Elements.AddElement(transaction);

            return transaction;
        }

        public void AddTransactions(IMonth month, IEnumerable<IPayment> paymentsForMonth)
        {
            foreach (IPayment payment in paymentsForMonth)
            {
                UpdateTransactions(payment, new[] { month });
            }
        }

        public void UpdatePayment(IPayment payment, IMonth month)
        {
            var months = payment.IsOneTimePayment ?
                new[] { month } :
                Project.CurrentProject.Months.GeMonthsForPayment(payment);

            UpdateTransactions(payment, months);
        }

        private void UpdateTransactions(IPayment payment, IEnumerable<IMonth> months)
        {
            UpdateProjectMonths(payment, months);
            RemoveOldTransactions(payment);
        }

        private void UpdateProjectMonths(IPayment payment, IEnumerable<IMonth> months)
        {
            var updateMonths = new List<IMonth>();

            foreach (var month in months)
            {
                var transaction = month.Transactions.Elements.SingleOrDefault(t => t.Payment.Id == payment.Id);

                if (transaction != null)
                {
                    transaction.Payment.Element = payment;
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

            var removedTransactions = projectTransactions.Where(t => !newAffectedMonths.Contains(t.Month.Element.MonthType));

            removedTransactions.ToList().ForEach(t => t.Delete());
        }
    }
}