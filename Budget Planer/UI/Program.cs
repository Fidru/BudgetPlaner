using Data.Services;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            IProject project = LoadData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MainWindow(project));
        }

        private static IProject LoadData()
        {
            //return new XmlSaver.Save.MyXmlSaver().Read(services);
            IEnumerable<IService> services = CreateServices();
            return new TestData().CreateTestData(services);
        }

        private static IEnumerable<IService> CreateServices()
        {
            var projectFact = new ProjectFactory();
            var project = projectFact.Create("Betriebskosten 2021");

            var currentProject = new CurentProjectService(project);
            projectFact.Project = currentProject;

            return new IService[]
            {
                projectFact,
                currentProject,
                new CategoryFactory() { Project = currentProject },
                new PaymentIntervalFactory() { Project = currentProject },
                new MonthFactory() { Project = currentProject },
                new PaymentFactory() { Project = currentProject },
                new YearFactoy() { Project = currentProject },
                new TransactionFactory() { Project = currentProject },
                new PaymentIntervalFactory() { Project = currentProject },
                new PayPatternFactory() { Project = currentProject },
            };
        }
    }
}