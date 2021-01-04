using Data.Classes;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;

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
            element.Delete();
        }

        public void Delete(IEnumerable<IElement> toDelete)
        {
            foreach (var element in toDelete)
            {
                Delete(element);
            }
        }
    }
}