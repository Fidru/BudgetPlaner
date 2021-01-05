using IData.Constants;

namespace IData.Interfaces
{
    public interface IPayment : IElement
    {
        ISaveableXmlElement<ICategory> Category { get; set; }
        ISaveableXmlElement<ICategory> SubCategory { get; set; }
        CategoryType CategoryType { get; }
        CategoryType SubCategoryType { get; }
        double Amount { get; set; }
        ISaveableXmlElement<IPayPattern> PayPattern { get; set; }
        bool IsOneTimePayment { get; }
    }
}