using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        public IList<ITransaction> Transactions { get; set; }
        public IMonth currentMonth { get; set; }

        public MainWindow()
        {
            var data = new TestData();

            currentMonth = data.Project.Months.First();
            Transactions = currentMonth.Transactions.Elements.ToList();

            InitializeComponent();
            LoadData(data);
            SetCurrentMonth(data.Project.Months.First());
        }

        private void SetCurrentMonth(IMonth month)
        {
            monthDisplay.Text = month.Name;
            currentMonth = month;
        }

        private void LoadData(TestData data)
        {
            var project = data.Project;

            foreach (var year in project.Years)
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
            SetViewElements(month);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            SetNewMonth(currentMonth.AlignedMonths.Next);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SetNewMonth(currentMonth.AlignedMonths.Previous);
        }

        private void SetNewMonth(IMonth newMonth)
        {
            if (newMonth == null)
            {
                return;
            }

            SetCurrentMonth(newMonth);
            SetViewElements(currentMonth);
        }

        private void SetViewElements(IMonth month)
        {
            Transactions = month.Transactions.Elements.ToList();
            bills.ItemsSource = Transactions;
        }
    }
}