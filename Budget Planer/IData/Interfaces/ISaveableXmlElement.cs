using System;

namespace IData.Interfaces
{
    public interface ISaveableXmlElement<T>
    {
        T Element { get; set; }

        Guid Id { get; set; }
    }
}