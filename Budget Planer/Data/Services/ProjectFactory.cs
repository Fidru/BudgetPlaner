using Data.Classes;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class ProjectFactory : ElementFactory, IProjectFactory
    {
        public IProject CreateEmpty()
        {
            var project = new Project();

            return project;
        }

        public IProject Create(string name)
        {
            var project = new Project(name);

            return project;
        }

        public IProject Copy(IProject original)
        {
            return original;
        }

        public void Delete(IProject element)
        {
            throw new System.NotImplementedException();
        }
    }
}