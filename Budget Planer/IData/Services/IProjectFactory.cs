using IData.Interfaces;

namespace IData.Services
{
    public interface IProjectFactory : IElementFactory<IProject>, IElementService
    {
        IProject Create(string name);
    }
}