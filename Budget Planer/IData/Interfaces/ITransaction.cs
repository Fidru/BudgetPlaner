using IData.Constants;

namespace IData.Interfaces
{
    public interface ITransaction : IElement
    {
        ISaveableXmlElement<IMonth> Month { get; set; }
        ISaveableXmlElement<IPayment> Payment { get; set; }
        double Amount { get; set; }
        bool Payed { get; set; }
        ICategory Category { get; set; }
        CategoryType CategoryType { get; }
        ICategory SubCategory { get; set; }
        CategoryType SubCategoryType { get; }
    }
}