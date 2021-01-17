using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface IMonthFactory : IFactory<IMonth>
    {
        IMonth Create(MonthEnum month, IYear year);
    }
}