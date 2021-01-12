using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IElementCollection<T> where T : IElement
    {
        IEnumerable<T> Elements { get; }

        void AddElement(T element);

        void AddElements(IEnumerable<T> elements);

        void RemoveElement(T element);

        string Ids { get; set; }

        void ConnectIds(IEnumerable<IElement> projectElements);
    }
}