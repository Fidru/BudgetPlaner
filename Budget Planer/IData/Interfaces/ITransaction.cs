using IData.Constants;

namespace IData.Interfaces
{
    public interface ITransaction : IElement
    {
        ISaveableXmlElement<IMonth> Month { get; set; }
        ISaveableXmlElement<IPayment> Payment { get; set; }
        double Amount { get; set; }
        string AmountTxt { get; }
        bool Payed { get; set; }
        ICategory Category { get; }
        string CategoryName { get; }
        CategoryType CategoryType { get; }
        ICategory SubCategory { get; }
        CategoryType SubCategoryType { get; }
    }
}