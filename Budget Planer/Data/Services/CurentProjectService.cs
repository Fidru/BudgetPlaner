using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class CurentProjectService : ICurentProjectService
    {
        public CurentProjectService(IProject currentProject)
        {
            CurrentProject = currentProject;
        }

        public IProject CurrentProject { get; set; }
    }
}