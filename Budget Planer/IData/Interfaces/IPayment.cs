using System;

namespace IData.Interfaces
{
    public interface IPayment : IElement
    {
        IElementGuidPair<ICategory> Category { get; set; }
        IElementGuidPair<ICategory> SubCategory { get; set; }
        double Amount { get; set; }
        IElementGuidPair<IPayPattern> PayPattern { get; set; }
    }
}