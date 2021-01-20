using IData.Interfaces;

namespace IData.Services
{
    public interface IYearFactory : IFactory<IYear>
    {
        IYear Create(string name, bool addOneTimePayments = false);

        void AlligneMonths();
    }
}