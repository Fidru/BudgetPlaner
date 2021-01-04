using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

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