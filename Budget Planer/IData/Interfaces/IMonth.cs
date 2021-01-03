using IData.Constants;

namespace IData.Interfaces
{
    public interface IMonth : IElement
    {
        MonthEnum MonthType { get; set; }

        IAlignedMonths AlignedMonths { get; set; }

        IElementCollection<ITransaction> Transactions { get; set; }

        void AddTransaction(ITransaction transaction);

        double OpenTransactions { get; }

        public double Bankbalance { get; set; }

        public void UpdateBankBalance(double bankbalance);

        public void UpdateBankBalanceEndOfMonth();

        ITransaction GetBankBalanceEndOfMonthPayment { get; }

        void UpdateIfIsBankBalanceRow(ITransaction transaction);
    }
}