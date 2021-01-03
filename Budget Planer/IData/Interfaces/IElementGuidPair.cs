using System;

namespace IData.Interfaces
{
    public interface IElementGuidPair<T>
    {
        T Element { get; set; }

        Guid Id { get; set; }
    }
}