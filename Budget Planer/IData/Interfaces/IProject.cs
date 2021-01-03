using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IProject : IElement
    {
        IEnumerable<IYear> Years { get; }
        IEnumerable<IPayment> Payments { get; }
        IEnumerable<IPaymentInterval> Intervals { get; }
        IEnumerable<ITransaction> Transactions { get; }
        IEnumerable<IPayPattern> PayPatterns { get; }
        IEnumerable<ICategory> Categories { get; }
        IEnumerable<ICategory> SubCategories { get; }
        IEnumerable<IMonth> Months { get; }
        IElementCollection<IElement> Elements { get; set; }
    }
}