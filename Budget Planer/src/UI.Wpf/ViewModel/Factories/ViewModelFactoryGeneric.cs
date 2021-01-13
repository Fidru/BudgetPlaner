using IData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class ViewModelFactoryGeneric<T, K>
        where K : IElement
        where T : ElementViewModel<K>

    {
        public T ConvertToVm(K element)
        {
            return CreateVm(element);
        }

        public List<T> ConvertToVms(IEnumerable<K> element)
        {
            return element.Select(e => CreateVm(e)).OfType<T>().ToList();
        }

        public virtual T CreateVm(K element)
        {
            return Activator.CreateInstance(typeof(T), element) as T;
        }
    }
}