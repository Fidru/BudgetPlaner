using IData.Constants;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IMonth : IElement
    {
        MonthEnum MonthType { get; set; }
        ISaveableXmlElement<IYear> Year { get; set; }

        IAlignedMonths AlignedMonths { get; set; }

        IElementCollection<ITransaction> Transactions { get; set; }
        IEnumerable<ITransaction> Bills { get; }
        IEnumerable<ITransaction> CreditCardPayments { get; }
        IEnumerable<ITransaction> FoodPayments { get; }
        IEnumerable<ITransaction> ExpectedUnexpectedPayments { get; }
        IEnumerable<ITransaction> OpenTransactions { get; }
        bool HasOpenTransactions { get; }

        void AddTransaction(ITransaction transaction);

        double OpenTransactionsSum { get; }

        void UpdateBankBalanceFromPreviousMonth();

        public void UpdateBankBalance(double bankbalance);

        public void AddToBankBalance(double amount);

        public IEnumerable<IIdentifier> UpdateBankBalanceEndOfMonth();

        ITransaction GetBankBalanceEndOfMonthPayment { get; }

        new string Name { get; set; }
    }
}