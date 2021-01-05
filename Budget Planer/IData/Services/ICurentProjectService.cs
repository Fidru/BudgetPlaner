using IData.Interfaces;

namespace IData.Services
{
    public interface ICurentProjectService : IService
    {
        IProject CurrentProject { get; set; }
    }
}