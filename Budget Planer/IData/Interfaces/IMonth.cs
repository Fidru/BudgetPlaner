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

        public double Bankbalance { get; set; }

        public void UpdateBankBalance(double bankbalance);

        public void UpdateBankBalanceEndOfMonth();

        ITransaction GetBankBalanceEndOfMonthPayment { get; }

        void UpdateBankBalanceRow(ITransaction transaction);

        new string Name { get; set; }
    }
}