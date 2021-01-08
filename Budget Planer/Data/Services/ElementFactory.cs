using Data.Classes;
using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public abstract class ElementFactory : IElementFactory, IService

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

        public virtual IElement CreateEmpty()
        {
            return new Element();
        }

        public void Delete(IElement element)
        {
            element.Delete();
        }
    }
}