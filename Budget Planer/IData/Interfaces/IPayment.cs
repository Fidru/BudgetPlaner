using System;

namespace IData.Interfaces
{
    public interface IPayment : IElement
    {
        ICategory Category { get; set; }
        ICategory SubCategory { get; set; }
        Guid CategoryId { get; set; }
        Guid SubCategoryId { get; set; }
        double Amount { get; set; }
        IPayPattern PayPattern { get; set; }

        Guid PayPatternId { get; set; }
    }
}