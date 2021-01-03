using IData.Interfaces;
using IData.Services;

namespace Data.Services
{
    public class ElementFactory : IElementFactory<IElement>, IElementService

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
            throw new System.NotImplementedException();
        }

        public IElement CreateEmpty()
        {
            throw new System.NotImplementedException();
        }
    }
}