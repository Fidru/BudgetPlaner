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
            Year = new SaveableXmlElement<IYear>();
            MonthType = (MonthEnum)DateTime.Now.Month;
            AlignedMonths = new AlignedMonths(this);
            Transactions = new ElementCollection<ITransaction>();
            Name = MonthType.ConvertToText();
            Types = new CategoryTypeCollection();
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);
            Transactions.ConnectIds(project.Transactions);
            Year.Element = (IYear)project.Years.GetElementById(Year.Id);

            AlignedMonths = new AlignedMonths(this);
        }

        public Month(MonthEnum monthType, IYear year) : this()
        {
            MonthType = monthType;
            Name = MonthType.ConvertToText();
            Year.Element = year;
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

        public double OpenTransactionsSum
        {
            get
            {
                return OpenTransactions.Sum(e => e.Amount);
            }
        }

        public bool HasOpenTransactions
        {
            get { return OpenTransactions.Any(); }
        }

        public IEnumerable<ITransaction> OpenTransactions
        {
            get
            {
                return Transactions.Elements.Where(t => !t.Payed
                          && t.CategoryType != CategoryType.Bankbalance).ToArray();
            }
        }

        public void AddTransaction(ITransaction transaction)
        {
            transaction.Month.Element = this;
            Transactions.AddElement(transaction);
        }

        public void UpdateBankBalanceFromPreviousMonth()
        {
            if (AlignedMonths.Previous != null)
            {
                UpdateBankBalance(AlignedMonths.Previous.GetBankBalanceEndOfMonthPayment.Amount);
            }
            UpdateBankBalanceEndOfMonth();
        }

        public void UpdateBankBalance(double amount)
        {
            var bankBalancePayment = BankBalancePayment;
            bankBalancePayment.Amount = amount;
        }

        public void AddToBankBalance(double amount)
        {
            var bankBalancePayment = BankBalancePayment;
            bankBalancePayment.Amount += amount;
        }

        public IEnumerable<IIdentifier> UpdateBankBalanceEndOfMonth()
        {
            var cotsEndOfMonth = GetCostsEndOfMonth;
            cotsEndOfMonth.Amount = OpenTransactionsSum;

            var bankBalanceEndOfMonthPayment = GetBankBalanceEndOfMonthPayment;
            bankBalanceEndOfMonthPayment.Amount = BankBalancePayment.Amount + OpenTransactionsSum;

            return new[] { bankBalanceEndOfMonthPayment, cotsEndOfMonth };
        }

        private ITransaction BankBalancePayment
        {
            get { return Transactions.Elements.Single(p => p.CategoryType == CategoryType.Bankbalance && p.SubCategory == null); }
        }

        public ITransaction GetBankBalanceEndOfMonthPayment
        {
            get
            {
                return Transactions.Elements.Single(p => p.CategoryType == CategoryType.Bankbalance
                            && p.SubCategory != null && p.SubCategoryType == CategoryType.BankbalanceEndOfMonth);
            }
        }

        public ITransaction GetCostsEndOfMonth
        {
            get
            {
                return Transactions.Elements.Single(p => p.CategoryType == CategoryType.Bankbalance
                            && p.SubCategory != null && p.SubCategoryType == CategoryType.OpenBills);
            }
        }

        public CategoryTypeCollection Types { get; }
        public ISaveableXmlElement<IYear> Year { get; set; }

        public override void Delete()
        {
            base.Delete();

            Transactions.Elements.ToList().ForEach(t => t.Delete());
        }
    }
}