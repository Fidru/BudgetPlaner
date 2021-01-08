using IData.Interfaces;
using System.Windows;
using UI.WinForms;
using System.Windows.Controls;

namespace UI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            var data = new TestData();

            var project = data.Project;

            foreach (var year in project.Years)
            {
                var mainItem = new MenuItem();
                mainItem.Header = year.Name;

                foreach (var month in year.Months.Elements)
                {
                    var subItem = new MenuItem();
                    subItem.Header = month.Name;
                    mainItem.Items.Add(subItem);
                }

                mainMenu.Items.Add(mainItem);
            }
        }
    }
}