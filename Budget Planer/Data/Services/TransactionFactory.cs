﻿using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class TransactionFactory : ElementFactory, ITransactionFactory
    {
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

        public ITransaction CreateEmpty()
        {
            var transaction = new Transaction();

            Project.CurrentProject.Elements.AddElement(transaction);
            return transaction;
        }

        public void AddTransactions(IMonth month, IEnumerable<IPayment> paymentsForMonth)
        {
            foreach (var payment in paymentsForMonth)
            {
                month.AddTransaction(Create(month, payment));
            }
        }

        public void AddCustomPayment(IPayment payment, IMonth month)
        {
            month.AddTransaction(Create(month, payment));
        }

        public void UpdatePayment(IPayment payment, IMonth month)
        {
            if (payment.PayPattern.Interval.Type == PaymentIntervalType.OneTimePayment)
            {
                AddCustomPayment(payment, month);
                return;
            }

            UpdateAllProjectMonths(payment);
        }

        private void UpdateAllProjectMonths(IPayment payment)
        {
            var months = Project.CurrentProject.Months.Where(m => payment.PayPattern.AffectedMonths.Any(affected => affected == m.MonthType));

            foreach (var projectMonth in months)
            {
                var transaction = projectMonth.Transactions.Elements.SingleOrDefault(t => t.Payment.Id == payment.Id);

                if (transaction != null)
                {
                    transaction.Payment = payment;
                }
                else
                {
                    projectMonth.AddTransaction(Create(projectMonth, payment));
                }
            }
        }
    }
}