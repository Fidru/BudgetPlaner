using IData.Interfaces;

namespace IData.Services
{
    public interface IElementFactory : IFactory<IElement>
    {
        IElement CreateEmpty();

        void Delete(IElement toDelete);
    }
}