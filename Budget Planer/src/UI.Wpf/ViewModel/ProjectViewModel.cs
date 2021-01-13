using IData.Interfaces;
using System.Collections.Generic;

namespace UI.Wpf.ViewModel
{
    public class ProjectViewModel : ElementViewModel<IProject>
    {
        public ProjectViewModel(IProject element) : base(element)
        {
        }

        public List<YearViewModel> YearsVm { get; set; }

        public YearViewModel CurrentYear { get; set; }

        public List<CategoryViewModel> CategorieVms { get; set; }
        public List<CategoryViewModel> SubCategorieVms { get; set; }
        public List<PayPatternViewModel> PayPatternVms { get; set; }
    }
}