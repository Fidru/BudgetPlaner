using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using UI;
using UI.WinForms;

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

            //LoadData();
        }

        //private static IProject LoadData()
        //{
        //    IEnumerable<IService> services = CreateServices();
        //    return new TestData().CreateTestData(services);
        //}

        //private static IEnumerable<IService> CreateServices()
        //{
        //    var projectFact = new ProjectFactory();
        //    var project = projectFact.Create("Betriebskosten 2021");

        //    var currentProject = new CurentProjectService(project);
        //    projectFact.Project = currentProject;

        //    return new IService[]
        //    {
        //        projectFact,
        //        currentProject,
        //        new CategoryFactory() { Project = currentProject },
        //        new PaymentIntervalFactory() { Project = currentProject },
        //        new MonthFactory() { Project = currentProject },
        //        new PaymentFactory() { Project = currentProject },
        //        new YearFactoy() { Project = currentProject },
        //        new TransactionFactory() { Project = currentProject },
        //        new PaymentIntervalFactory() { Project = currentProject },
        //        new PayPatternFactory() { Project = currentProject },
        //    };
        //}
    }
}