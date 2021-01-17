using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;
using UI.ViewModel.Factories;
using XmlSaver.Save;

namespace UI.ViewModel
{
    public class ProjectViewModel : ElementViewModel<IProject>
    {
        public IEnumerable<IService> Services { get; set; }

        public ProjectViewModel(IProject element) : base(element)
        {
        }

        public List<YearViewModel> YearVms { get; set; }

        public YearViewModel CurrentYear { get; set; }

        public string CombinedMonthName
        {
            get
            {
                return $"{CurrentYear.Name} - {CurrentYear.CurrentMonthVm.Name}";
            }
            set
            {
                CurrentYear.Name = value;
            }
        }

        public List<CategoryViewModel> CategorieVms { get; set; }
        public List<CategoryViewModel> SubCategorieVms { get; set; }
        public List<PayPatternViewModel> PayPatternVms { get; set; }

        public void SaveToXml()
        {
            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(Element);
        }

        public bool SelectNextMonth()
        {
            if (CurrentYear.SelectNextMonth())
            {
                SetNewMonthAndYear();

                return true;
            }
            return false;
        }

        private void SetNewMonthAndYear()
        {
            CurrentYear = CurrentYear.CurrentMonthVm.Year;
            CurrentYear.CurrentMonthVm.UpdateLists();

            NotifyPropertyChanged(CurrentYear, nameof(CurrentYear.CurrentMonthVm));
            NotifyPropertyChanged(CurrentYear.CurrentMonthVm, nameof(CurrentYear.CurrentMonthVm.Name));

            NotifyPropertyChanged(nameof(CombinedMonthName));
            NotifyPropertyChanged(CurrentYear, nameof(Name));
        }

        public bool SelectPreviousMonth()
        {
            if (CurrentYear.SelectPreviousMonth())
            {
                SetNewMonthAndYear();

                return true;
            }
            return false;
        }

        public void UpdateViewModels()
        {
            var filtered = Element.Transactions.Where(t => t.IsNew);

            foreach (var transaction in filtered)
            {
                if (transaction.IsNew)
                {
                    var transactionVm = new TransactionViewModelFacotry(Services).ConvertToVm(transaction);

                    transactionVm.MonthVm.AddTransaction(transactionVm);
                    transactionVm.PaymentViewModel.AddTransaction(transactionVm);
                    transaction.IsNew = false;
                }
            }
        }
    }
}