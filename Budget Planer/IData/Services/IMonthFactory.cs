using IData.Constants;
using IData.Interfaces;

namespace IData.Services
{
    public interface IMonthFactory : IElementFactory<IMonth>
    {
        IMonth Create(MonthEnum month);

        IMonth Create(MonthEnum month, double bankBalance);
    }
}