using IData.Interfaces;

namespace IData.Services
{
    public interface IProjectFactory : IFactory<IProject>, IService
    {
        IProject Create(string name);
    }
}