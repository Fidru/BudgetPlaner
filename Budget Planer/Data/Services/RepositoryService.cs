using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class RepositoryService : IRepositoryService
    {
        public RepositoryService()
        {
            CreatedViewModels = new Dictionary<Guid, IElementViewModel>();
        }

        public Dictionary<Guid, IElementViewModel> CreatedViewModels { get; set; }
        public ICurentProjectService Project { get; set; }

        public void AddViewModel(IElementViewModel vm)
        {
            if (CreatedViewModels.ContainsKey(vm.Id))
            {
                return;
            }

            CreatedViewModels.Add(vm.Id, vm);
        }

        public IElementViewModel GetViewModel(Guid id)
        {
            IElementViewModel vm;
            CreatedViewModels.TryGetValue(id, out vm);
            return vm;
        }
    }
}