using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Classes
{
    public class ElementCollection<T> : IElementCollection<T> where T : IElement
    {
        public ElementCollection()
        {
            Ids = string.Empty;
            Elements = new List<T>();
        }

        public IEnumerable<T> Elements { get; set; }

        public string Ids { get; set; }

        public void AddElement(T element)
        {
            element.IsNew = false;
            if (Elements.Contains(element))
            {
                return;
            }

            var newElements = Elements.ToList();
            newElements.Add(element);

            Elements = newElements;
            Ids = Elements.OfType<IIdentifier>().ConvertToStringIds();
        }

        public void AddElements(IEnumerable<T> elements)
        {
            elements.ToList().ForEach(e => AddElement(e));
        }

        public void ConnectIds(IEnumerable<IElement> projectElements)
        {
            AddElements(projectElements.GetElementsByIds(Ids).OfType<T>());
        }

        public IEnumerable<E> ElementOfType<E>()
        {
            return Elements.OfType<E>();
        }

        public string GetIdsOfType<E>()
        {
            return ElementOfType<E>().OfType<IIdentifier>().ConvertToStringIds();
        }
    }
}