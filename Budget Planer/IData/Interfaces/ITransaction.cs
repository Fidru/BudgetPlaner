using System;

namespace IData.Interfaces
{
    public interface ITransaction : IElement
    {
        IMonth Month { get; set; }
        Guid MonthId { get; set; }
        IPayment Payment { get; set; }
        Guid PaymentId { get; set; }
        double Amount { get; set; }
        bool Payed { get; set; }
    }
}