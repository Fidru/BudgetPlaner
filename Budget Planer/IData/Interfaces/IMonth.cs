using IData.Constants;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IMonth : IElement
    {
        MonthEnum MonthType { get; set; }

        IAlignedMonths AlignedMonths { get; set; }

        IElementCollection<ITransaction> Transactions { get; set; }
        IEnumerable<ITransaction> Bills { get; }
        IEnumerable<ITransaction> CreditCardPayments { get; }
        IEnumerable<ITransaction> FoodPayments { get; }
        IEnumerable<ITransaction> ExpectedUnexpectedPayments { get; }

        void AddTransaction(ITransaction transaction);

        double OpenTransactions { get; }

        void UpdateBankBalanceFromPreviousMonth();

        public void UpdateBankBalance(double bankbalance);

        public void AddToBankBalance(double amount);

        public IIdentifier UpdateBankBalanceEndOfMonth();

        ITransaction GetBankBalanceEndOfMonthPayment { get; }

        new string Name { get; set; }
    }
}