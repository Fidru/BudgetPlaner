using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.Wpf.ViewModel.Factories
{
    public class ViewModelFactoryGeneric<T, K>
        where K : IElement
        where T : ElementViewModel<K>

    {
        public IRepositoryService RepositoryService { get; set; }

        public IProject CurrentProject
        {
            get { return RepositoryService.Project.CurrentProject; }
        }

        public ViewModelFactoryGeneric(IRepositoryService repositoryService)
        {
            RepositoryService = repositoryService;
        }

        public T ConvertToVm(K element)
        {
            if (element == null)
            {
                return null;
            }

            var vm = RepositoryService.GetViewModel(element.Id) as T;

            if (vm == null)
            {
                return CreateVm(element);
            }
            return vm;
        }

        public List<T> ConvertToVms(IEnumerable<K> element)
        {
            return element.Select(e => ConvertToVm(e)).ToList();
        }

        public virtual T CreateVm(K element)
        {
            var vm = Activator.CreateInstance(typeof(T), element) as T;

            RepositoryService.AddViewModel(vm);

            return vm;
        }
    }
}