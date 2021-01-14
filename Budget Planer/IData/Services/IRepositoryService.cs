using IData.Interfaces;
using System;
using System.Collections.Generic;

namespace IData.Services
{
    public interface IRepositoryService : IService
    {
        Dictionary<Guid, IElementViewModel> CreatedViewModels { get; set; }

        ICurentProjectService Project { get; set; }

        void AddViewModel(IElementViewModel vm);

        IElementViewModel GetViewModel(Guid id);
    }
}