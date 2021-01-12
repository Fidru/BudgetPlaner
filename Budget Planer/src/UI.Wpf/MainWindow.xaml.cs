using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UI.WinForms;
using UI.Wpf.ViewModel;
using XmlSaver.Save;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly Storyboard _nextAnimationFadeIn;
        private readonly Storyboard _previousAnimation;

        private ProjectViewModel _projectVm;
        private YearViewModel _yearVm;

        private IEnumerable<IService> _services { get; set; }
        public object Threat { get; private set; }

        public MainWindow()
        {
            var data = new TestData();
            _services = data.Services;

            InitializeComponent();

            _projectVm = new ViewModelFactory().ConvertVm(data.Project);
            _yearVm = _projectVm.YearsVm.First();
            DataContext = _yearVm;

            CreateMenu();

            _nextAnimationFadeIn = CreateStoryboard(1200, 0, nameof(innerPanel.Opacity), nameof(innerPanel.RenderTransform), 800);
            _previousAnimation = CreateStoryboard(-1200, 0, nameof(innerPanel.Opacity), nameof(innerPanel.RenderTransform), 800);
        }

        private Storyboard CreateStoryboard(int fromRender, int toRender, string opacity, string render, int maxTimer)
        {
            Storyboard sb = new Storyboard();
            sb.FillBehavior = FillBehavior.HoldEnd;

            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(opacity));
            sb.Children.Add(fadeIn);

            DoubleAnimation moveIn = new DoubleAnimation(fromRender, toRender, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(moveIn, new PropertyPath($"{render}.{nameof(TranslateTransform.X)}"));
            sb.Children.Add(moveIn);

            return sb;
        }

        private void CreateMenu()
        {
            while (mainMenu.Items.Count > 2)
            {
                mainMenu.Items.RemoveAt(2);
            }

            foreach (var year in _projectVm.Project.Years)
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
            var month = menu.Tag as MonthViewModel;

            _yearVm.SelectNewCurrentMonth(month);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            // todo
            _yearVm.SelectNextMonth();
            _nextAnimationFadeIn.Begin(innerPanel);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            // todo
            _yearVm.SelectPreviousMonth();
            _previousAnimation.Begin(innerPanel);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(_projectVm.Project);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            IProject project = new MyXmlSaver().Read(_services);

            DataContext = new ViewModelFactory().ConvertVm(project);

            CreateMenu();
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