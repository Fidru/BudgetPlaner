using IData.Interfaces;

namespace IData.Services
{
    public interface IYearFactoy : IFactory<IYear>
    {
        IYear Create(string name);
    }
}