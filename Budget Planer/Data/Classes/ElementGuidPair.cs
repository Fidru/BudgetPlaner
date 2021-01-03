using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class ElementGuidPair<T> : IElementGuidPair<T> where T : IElement
    {
        private T _element;

        public ElementGuidPair()
        {
        }

        public ElementGuidPair(T element)
        {
            Element = element;
        }

        public T Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
                Id = value == null ? Guid.Empty : value.Id;
            }
        }

        public Guid Id { get; set; }
    }
}