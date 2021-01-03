using IData.Interfaces;

namespace IData.Services
{
    public interface IYearFactoy : IElementFactory<IYear>
    {
        IYear Create(string name);
    }
}