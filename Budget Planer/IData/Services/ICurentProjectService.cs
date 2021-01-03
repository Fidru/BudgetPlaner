using IData.Interfaces;

namespace IData.Services
{
    public interface ICurentProjectService : IElementService
    {
        IProject CurrentProject { get; set; }
    }
}