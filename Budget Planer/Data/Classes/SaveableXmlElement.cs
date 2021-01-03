using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class SaveableXmlElement<T> : ISaveableXmlElement<T> where T : IElement
    {
        private T _element;

        public SaveableXmlElement()
        {
        }

        public SaveableXmlElement(T element)
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