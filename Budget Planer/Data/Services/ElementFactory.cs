using Data.Classes;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class ElementFactory : IElementFactory, IService

    {
        public ElementFactory()
        {
        }

        public ElementFactory(ICurentProjectService currentProject)
        {
            Project = currentProject;
        }

        public ICurentProjectService Project { get; set; }

        public IElement Copy(IElement original)
        {
            return original;
        }

        public IElement GetCreateEmpty()
        {
            return new Element();
        }

        public void Delete(IElement element)
        {
            Project.CurrentProject.Elements.RemoveElement(element);
        }
    }
}