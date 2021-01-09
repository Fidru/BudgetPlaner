using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using XmlSaver.Save;
using IData.Services;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Threading;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly Storyboard _nextAnimation;
        private readonly Storyboard _previousAnimation;
        public IMonth CurrentMonth { get; set; }

        private IProject _project { get; set; }
        private IEnumerable<IService> _services { get; set; }
        public object Threat { get; private set; }

        public MainWindow()
        {
            var data = new TestData();
            _services = data.Services;
            _project = data.Project;
            CurrentMonth = _project.Months.First();

            InitializeComponent();

            CreateMenu();
            SetCurrentMonth(CurrentMonth);
            _nextAnimation = CreateStoryboard(0, 1, 800, 0);
            _previousAnimation = CreateStoryboard(0, 1, -800, 0);
        }

        private Storyboard CreateStoryboard(int from, int to, int fromRender, int toRender)
        {
            Storyboard sb = new Storyboard();
            sb.FillBehavior = FillBehavior.HoldEnd;

            var opacityAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(800)));
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(innerGrid.Opacity)));

            sb.Children.Add(opacityAnimation);

            DoubleAnimation translateAnimation = new DoubleAnimation(fromRender, toRender, new Duration(TimeSpan.FromMilliseconds(800)));
            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath($"{nameof(innerGrid.RenderTransform)}.{nameof(TranslateTransform.X)}"));

            sb.Children.Add(translateAnimation);

            return sb;
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
            _nextAnimation.Begin(innerGrid);
            _nextAnimation.Begin(monthDisplay);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SetNewMonth(CurrentMonth.AlignedMonths.Previous);
            _previousAnimation.Begin(innerGrid);
            _previousAnimation.Begin(monthDisplay);
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

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton item = sender as ToggleButton;

            if (item == null) return;

            ITransaction transaction = item.DataContext as ITransaction;

            transaction.Month.Element.UpdateBankBalanceEndOfMonth();
            SetCurrentMonth(transaction.Month.Element);
        }
    }
}