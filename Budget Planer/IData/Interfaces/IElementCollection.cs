﻿using System;
using System.Collections.Generic;

namespace IData.Interfaces
{
    public interface IElementCollection<T> where T : IElement
    {
        IEnumerable<T> Elements { get; set; }

        void AddElement(T element);

        void AddElements(IEnumerable<T> elements);

        string Ids { get; set; }

        void ConnectIds(IEnumerable<IElement> projectElements);

        IEnumerable<E> ElementOfType<E>();

        string GetIdsOfType<E>();
    }
}