using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;
using System.Collections.Generic;
using XmlSaver.Save;
using IData.Services;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Media;
using UI.Wpf.ViewModel;
using System.Threading.Tasks;
using System.Threading;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly Storyboard _nextAnimationFadeOut;
        private readonly Storyboard _nextAnimationFadeIn;
        private readonly Storyboard _previousAnimation;

        private YearViewModel _vm;

        private IProject _project { get; set; }
        private IEnumerable<IService> _services { get; set; }
        public object Threat { get; private set; }

        public MainWindow()
        {
            var data = new TestData();
            _services = data.Services;
            _project = data.Project;

            InitializeComponent();

            _vm = new YearViewModel(_project);
            DataContext = _vm;

            CreateMenu();

            //_nextAnimationFadeOut = CreateStoryboard(1, 0, 0, -800, nameof(innerPanel.Opacity), nameof(innerPanel.RenderTransform));
            _nextAnimationFadeIn = CreateStoryboard(1200, 0, nameof(innerPanel.Opacity), nameof(innerPanel.RenderTransform), 800);
            _previousAnimation = CreateStoryboard(-1200, 0, nameof(innerPanel.Opacity), nameof(innerPanel.RenderTransform), 800);
        }

        private Storyboard CreateStoryboard(int fromRender, int toRender, string opacity, string render, int maxTimer)
        {
            Storyboard sb = new Storyboard();
            sb.FillBehavior = FillBehavior.HoldEnd;

            //var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            //Storyboard.SetTargetProperty(fadeOut, new PropertyPath(opacity));
            //sb.Children.Add(fadeOut);

            //DoubleAnimation moveOut = new DoubleAnimation(toRender * -1, fromRender, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            //Storyboard.SetTargetProperty(moveOut, new PropertyPath($"{render}.{nameof(TranslateTransform.X)}"));
            //sb.Children.Add(moveOut);

            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(opacity));
            sb.Children.Add(fadeIn);

            DoubleAnimation moveIn = new DoubleAnimation(fromRender, toRender, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(moveIn, new PropertyPath($"{render}.{nameof(TranslateTransform.X)}"));
            sb.Children.Add(moveIn);

            return sb;
        }

        private void SetCurrentMonth(IMonth month)
        {
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
            UpdateNext();
        }

        private void UpdateNext()
        {
            _vm.SelectNextMonth();

            _nextAnimationFadeIn.Begin(innerPanel);
            //_nextAnimationFadeIn.Begin(monthDisplay);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            UpdatePrevious();
        }

        private void UpdatePrevious()
        {
            _vm.SelectPreviousMonth();

            _previousAnimation.Begin(innerPanel);
            //_previousAnimation.Begin(monthDisplay);
        }

        private async void UpdatePreviousAsync()
        {
            Task<bool> updateTask = UpdatePreviousDataContextAsync();

            _previousAnimation.Begin(innerPanel);

            await updateTask;
        }

        private async Task<bool> UpdatePreviousDataContextAsync()
        {
            await Task.Run(() => _vm.SelectPreviousMonth());
            return true;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(_project);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            IProject project = new MyXmlSaver().Read(_services);

            DataContext = new YearViewModel(project);

            CreateMenu();
        }

        private void textAmount_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBox item = sender as TextBox;

                if (item == null) return;

                ITransaction transaction = item.DataContext as ITransaction;

                // if contains numbers only
<<<<<<< Updated upstream
                transaction.Amount = Convert.ToDouble(item.Text);
                transaction.Month.Element.UpdateBankBalanceEndOfMonth();
=======
                //transaction.Amount = Convert.ToDouble(item.Text);
                //transaction.UpdateBankBalance();
>>>>>>> Stashed changes
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //new MyXmlSaver().Save(_project);

            //Thread.Sleep(2000);
            this.Close();
        }
    }
}