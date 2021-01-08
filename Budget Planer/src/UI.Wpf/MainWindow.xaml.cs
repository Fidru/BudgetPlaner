using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using XmlSaver.Save;
using IData.Services;
using System;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        public IMonth CurrentMonth { get; set; }

        private IProject _project { get; set; }
        private IEnumerable<IService> _services { get; set; }

        public MainWindow()
        {
            var data = new TestData();
            _services = data.Services;
            _project = data.Project;
            CurrentMonth = _project.Months.First();

            InitializeComponent();

            CreateMenu();
            SetCurrentMonth(CurrentMonth);
        }

        private void SetCurrentMonth(IMonth month)
        {
            monthDisplay.Text = month.Name;
            CurrentMonth = month;

            SetListViewItemSource(bills, month.Bills);
            SetListViewItemSource(foodBills, month.FoodPayments);
            SetListViewItemSource(creditCard, month.CreditCardPayments);
            SetListViewItemSource(expectedBills, month.ExpectedUnexpectedPayments);
        }

        private void SetListViewItemSource(ListView view, IEnumerable<ITransaction> transactions)
        {
            if (view.ItemsSource != null)
            {
                view.ItemsSource = transactions;
            }
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

            SetCurrentMonth(transaction.Month.Element);
        }

        private void textAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void textAmount_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBox item = sender as TextBox;

                if (item == null) return;

                ITransaction transaction = item.DataContext as ITransaction;

                // if contains numbers only
                transaction.Amount = Convert.ToDouble(item.Text);
                transaction.Month.Element.UpdateBankBalanceEndOfMonth();
                SetCurrentMonth(transaction.Month.Element);
            }
        }
    }
}