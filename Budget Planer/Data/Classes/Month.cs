using IData.Constants;
using IData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Classes
{
    public class Month : Element, IMonth
    {
        public Month() : base()
        {
            MonthType = (MonthEnum)DateTime.Now.Month;
            AlignedMonths = new AlignedMonths(this);
            Transactions = new ElementCollection<ITransaction>();
            Name = MonthType.ConvertToText();
            Bankbalance = 0.0;
            Types = new CategoryTypeCollection();
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);
            Transactions.ConnectIds(project.Transactions);

            AlignedMonths = new AlignedMonths(this);
        }

        public Month(MonthEnum monthType) : this()
        {
            MonthType = monthType;
            Name = MonthType.ConvertToText();
        }

        public IAlignedMonths AlignedMonths { get; set; }
        public IElementCollection<ITransaction> Transactions { get; set; }

        public IEnumerable<ITransaction> Bills
        {
            get
            {
                return Transactions.Elements.GetTransactionsForCategory(Types.MonthlyBills);
            }
        }

        public IEnumerable<ITransaction> CreditCardPayments
        {
            get
            {
                return Transactions.Elements.GetTransactionsForCategory(Types.CreditCardCategories);
            }
        }

        public IEnumerable<ITransaction> FoodPayments
        {
            get
            {
                return Transactions.Elements.GetTransactionsForCategory(Types.FoodCategories);
            }
        }

        public IEnumerable<ITransaction> ExpectedUnexpectedPayments
        {
            get
            {
                return Transactions.Elements.GetTransactionsForCategory(Types.ExpectedUnexpectedCategories);
            }
        }

        public MonthEnum MonthType { get; set; }
        public double Bankbalance { get; set; }

        public double OpenTransactions
        {
            get
            {
                var openPayments = Transactions.Elements.Where(t => !t.Payed
                && t.CategoryType != CategoryType.Bankbalance).ToArray();

                return openPayments.Sum(e => e.Amount);
            }
        }

        public void AddTransaction(ITransaction transaction)
        {
            transaction.Month.Element = this;
            Transactions.AddElement(transaction);
        }

        public void UpdateBankBalanceRow(ITransaction transaction)
        {
            if (IsBankBalancePayment(transaction))
            {
                Bankbalance = transaction.Amount;
            }
        }

        private bool IsBankBalancePayment(ITransaction transaction)
        {
            return BankBalancePayment.Id == transaction.Id;
        }

        public void UpdateBankBalance(double amount)
        {
            var bankBalancePayment = BankBalancePayment;
            bankBalancePayment.Amount += amount;
            Bankbalance = bankBalancePayment.Amount;
        }

        public void UpdateBankBalanceEndOfMonth()
        {
            var bankBalanceEndOfMonthPayment = GetBankBalanceEndOfMonthPayment;
            bankBalanceEndOfMonthPayment.Amount = BankBalancePayment.Amount + OpenTransactions;
        }

        private ITransaction BankBalancePayment
        {
            get { return  Transactions.Elements.Single(p => p.CategoryType == CategoryType.Bankbalance && p.SubCategory == null); }
        }

        public ITransaction GetBankBalanceEndOfMonthPayment
        {
            get
            {
                return Transactions.Elements.Single(p => p.CategoryType == CategoryType.Bankbalance
                            && p.SubCategory != null && p.SubCategoryType == CategoryType.BankbalanceEndOfMonth);
            }
        }

        public CategoryTypeCollection Types { get; }

        public override void Delete()
        {
            base.Delete();

            Transactions.Elements.ToList().ForEach(t => t.Delete());
        }
    }
}