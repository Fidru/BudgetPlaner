using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using XmlSaver.Save;
using IData.Services;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        public IList<ITransaction> Transactions { get; set; }
        public IMonth CurrentMonth { get; set; }
        private IProject _project { get; set; }
        private IEnumerable<IService> _services { get; set; }

        public MainWindow()
        {
            var data = new TestData();
            _services = data.Services;
            _project = data.Project;

            CurrentMonth = _project.Months.First();
            Transactions = CurrentMonth.Transactions.Elements.ToList();

            InitializeComponent();

            CreateMenu();
            SetCurrentMonth(CurrentMonth);
        }

        private void SetCurrentMonth(IMonth month)
        {
            monthDisplay.Text = month.Name;
            CurrentMonth = month;
            SetListViews(CurrentMonth);
        }

        private void CreateMenu()
        {
            while (mainMenu.Items.Count > 2)
            {
                mainMenu.Items.RemoveAt(2);
            }

            foreach (var year in _project.Years)
            {
                var mainItem = new MenuItem();
                mainItem.Header = year.Name;
                mainItem.Tag = year;

                foreach (var month in year.Months.Elements)
                {
                    var subItem = new MenuItem();
                    subItem.Header = month.Name;
                    subItem.Tag = month;
                    subItem.Click += MonthClicked;
                    mainItem.Items.Add(subItem);
                }

                mainMenu.Items.Add(mainItem);
            }
        }

        private void MonthClicked(object sender, RoutedEventArgs e)
        {
            var menu = sender as MenuItem;
            var month = menu.Tag as IMonth;

            SetCurrentMonth(month);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            SetNewMonth(CurrentMonth.AlignedMonths.Next);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SetNewMonth(CurrentMonth.AlignedMonths.Previous);
        }

        private void SetNewMonth(IMonth newMonth)
        {
            if (newMonth == null)
            {
                return;
            }

            SetCurrentMonth(newMonth);
        }

        private void SetListViews(IMonth month)
        {
            Transactions = month.Transactions.Elements.ToList();
            bills.ItemsSource = Transactions;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //Update Views

            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(_project);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            MyXmlSaver saver = new MyXmlSaver();
            _project = saver.Read(_services);
            CreateMenu();
            SetCurrentMonth(_project.Years.First().Months.Elements.First());
        }

        private void radioPaid_Click(object sender, RoutedEventArgs e)
        {
            RadioButton item = sender as RadioButton;

            if (item == null) return;

            ITransaction transaction = item.DataContext as ITransaction;
            transaction.Payed = item.IsChecked.HasValue ? item.IsChecked.Value : false;

            transaction.Month.Element.UpdateBankBalanceEndOfMonth();
        }
    }
}