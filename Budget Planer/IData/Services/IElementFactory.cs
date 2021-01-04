using IData.Interfaces;
using System.Collections.Generic;

namespace IData.Services
{
    public interface IElementFactory : IFactory<IElement>
    {
        void Delete(IElement toDelete);

        void Delete(IEnumerable<IElement> toDelete);
    }
}