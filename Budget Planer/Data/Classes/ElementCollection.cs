using IData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Classes
{
    public class ElementCollection<T> : IElementCollection<T> where T : IElement
    {
        private List<T> _elements;
        private List<Guid> _ids;

        public ElementCollection()
        {
            _elements = new List<T>();
            _ids = new List<Guid>();
        }

        public IEnumerable<T> Elements
        {
            get
            {
                return _elements.Where(e => !e.IsDeleted);
            }
        }

        public string Ids
        {
            get
            {
                return _ids.ConvertToStringIds();
            }
            set
            {
                _ids = value.ConvertStringIdsToGuids().ToList();
            }
        }

        public void AddElement(T element)
        {
            element.IsNew = false;
            if (Elements.Contains(element))
            {
                return;
            }

            _elements.Add(element);
            _ids.Add(element.Id);
        }

        public void RemoveElement(T element)
        {
            if (!Elements.Contains(element))
            {
                return;
            }

            _elements.Remove(element);
            _ids.Remove(element.Id);
        }

        public void AddElements(IEnumerable<T> elements)
        {
            elements.ToList().ForEach(e => AddElement(e));
        }

        public void ConnectIds(IEnumerable<IElement> projectElements)
        {
            AddElements(projectElements.GetElementsByIds(Ids).OfType<T>());
        }
    }
}