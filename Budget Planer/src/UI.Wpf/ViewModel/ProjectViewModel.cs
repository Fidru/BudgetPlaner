using IData.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace UI.Wpf.ViewModel
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public IProject Project { get; set; }

        public List<YearViewModel> YearsVm { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}