using IData.Constants;
using IData.Interfaces;
using System;
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
        }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);
            Transactions.ConnectIds(project.Transactions);
        }

        public Month(MonthEnum monthType) : this()
        {
            MonthType = monthType;
            Name = MonthType.ConvertToText();
        }

        public IAlignedMonths AlignedMonths { get; set; }
        public IElementCollection<ITransaction> Transactions { get; set; }

        public MonthEnum MonthType { get; set; }
        public double Bankbalance { get; set; }

        public double OpenTransactions
        {
            get
            {
                return Transactions.Elements.Where(p => !p.Payed && p.Payment.Category.CategoryType != CategoryType.Bankbalance).Sum(e => e.Amount);
            }
        }

        public void AddTransaction(ITransaction transaction)
        {
            transaction.Month = this;
            Transactions.AddElement(transaction);
        }

        public void UpdateIfIsBankBalanceRow(ITransaction transaction)
        {
            if (GetBankBalancePayment.Id == transaction.Id)
            {
                Bankbalance = transaction.Amount;
            }
        }

        public void UpdateBankBalance(double amount)
        {
            var bankBalancePayment = GetBankBalancePayment;
            bankBalancePayment.Amount += amount;
            Bankbalance = bankBalancePayment.Amount;
        }

        public void UpdateBankBalanceEndOfMonth()
        {
            var bankBalanceEndOfMonthPayment = GetBankBalanceEndOfMonthPayment;
            bankBalanceEndOfMonthPayment.Amount = Bankbalance + OpenTransactions;
        }

        private ITransaction GetBankBalancePayment
        {
            get { return Transactions.Elements.Single(p => p.Payment.Category.CategoryType == CategoryType.Bankbalance && p.Payment.SubCategory == null); }
        }

        public ITransaction GetBankBalanceEndOfMonthPayment
        {
            get
            {
                return Transactions.Elements.Single(p => p.Payment.Category.CategoryType == CategoryType.Bankbalance
                            && p.Payment.SubCategory != null && p.Payment.SubCategory.CategoryType == CategoryType.BankbalanceEndOfMonth);
            }
        }
    }
}