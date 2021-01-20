﻿using Data.Services;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;
using UI.ViewModel;
using UI.ViewModel.Factories;
using XmlSaver.Save;

namespace UI.DefaultData
{
    public class TestData
    {
        public IEnumerable<IService> Services { get; set; }

        public TestData()
        {
            Services = CreateServices();
        }

        public ProjectViewModel GetDefaultData()
        {
            var project = CreateTestData(Services);
            Services.GetService<IYearFactory>().AlligneMonths();

            return CreateProjectViewModel(project);
        }

        private ProjectViewModel CreateProjectViewModel(IProject project)
        {
            var factory = new ProjectViewModelFacotry(Services);
            factory.Services = Services;

            var vm = factory.ConvertToVm(project);
            vm.Services = Services;

            return vm;
        }

        public ProjectViewModel LoadFromXml()
        {
            IProject project = new MyXmlSaver().Read(Services);

            Services.GetService<IYearFactory>().AlligneMonths();

            return CreateProjectViewModel(project);
        }

        private IEnumerable<IService> CreateServices()
        {
            var projectFact = new ProjectFactory();
            var project = projectFact.Create("Betriebskosten");

            var currentProject = new CurentProjectService(project);
            projectFact.Project = currentProject;

            return new IService[]
            {
                projectFact,
                currentProject,
                new CategoryFactory() { Project = currentProject },
                new MonthFactory() { Project = currentProject },
                new PaymentIntervalFactory() { Project = currentProject },
                new PayPatternFactory() { Project = currentProject },
                new PaymentFactory() { Project = currentProject },
                new RepositoryService() { Project = currentProject },
                new TransactionFactory() { Project = currentProject },
                new YearFactory() { Project = currentProject },
            };
        }

        private IProject CreateTestData(IEnumerable<IService> services)
        {
            var currentProject = services.GetService<ICurentProjectService>();

            var project = currentProject.CurrentProject;

            var categoryFactory = services.GetService<ICategoryFactory>();
            var yearFact = services.GetService<IYearFactory>();
            var intervalFactory = services.GetService<IPaymentIntervalFactory>();
            var paymentFactory = services.GetService<IPaymentFactory>();
            var payPatternFactory = services.GetService<IPayPatternFactory>();

            categoryFactory.Create("Income", "", CategoryType.Income, 0, true);
            categoryFactory.Create("Rent and Housing", "", CategoryType.RentAndHousing, 10, true);
            categoryFactory.Create("Internet and Mobile", "", CategoryType.InternetAndMobile, 20, true);
            categoryFactory.Create("Insurance", "", CategoryType.Insurance, 30, true);
            categoryFactory.Create("Food and household", "", CategoryType.FoodAndHousehold, 40, true);
            categoryFactory.Create("Credit card", "", CategoryType.CreditCard, 50, true);
            categoryFactory.Create("Extra expenses", "", CategoryType.ExtraExpenses, 60, true);
            categoryFactory.Create("Unexpected expenses", "", CategoryType.UnexpectedExpenses, 70, true);
            categoryFactory.Create("Bank balance", "", CategoryType.Bankbalance, 80, true);

            categoryFactory.Create("Sallary", "", CategoryType.Sallary, 1);

            categoryFactory.Create("Rent", "", CategoryType.Rent, 11);
            categoryFactory.Create("Operating Costs", "", CategoryType.OperatingCosts, 12);
            categoryFactory.Create("Water & Heating", "", CategoryType.WaterAndHeating, 13);
            categoryFactory.Create("Electricity", "", CategoryType.Electricity, 14);

            categoryFactory.Create("Tv", "", CategoryType.Tv, 21);
            categoryFactory.Create("Gis", "", CategoryType.Gis, 22);
            categoryFactory.Create("Phone", "", CategoryType.Phone, 23);
            categoryFactory.Create("Streaming", "", CategoryType.Streaming, 24);

            categoryFactory.Create("Insurance", "", CategoryType.CarInsurance, 31);
            categoryFactory.Create("Lease", "", CategoryType.Lease, 32);
            categoryFactory.Create("Credit Appartment", "", CategoryType.CreditAppartment, 33);

            categoryFactory.Create("Food", "", CategoryType.Food, 41);
            categoryFactory.Create("Dog Food", "", CategoryType.DogFood, 42);
            categoryFactory.Create("Fuel", "", CategoryType.Fuel, 43);

            categoryFactory.Create("Open Bills", "", CategoryType.OpenBills, 81);
            categoryFactory.Create("Bank balance End Of MOnth", "", CategoryType.BankbalanceEndOfMonth, 82);

            intervalFactory.Create(PaymentIntervalType.Monthly);
            intervalFactory.Create(PaymentIntervalType.EverySecondMonth);
            intervalFactory.Create(PaymentIntervalType.Yearly);
            intervalFactory.Create(PaymentIntervalType.EverySixthMonth);
            intervalFactory.Create(PaymentIntervalType.OneTimePayment);
            //intervalFactory.Create(PaymentIntervalType.Custom);

            var categories = project.Categories;
            var subCategories = project.SubCategories;

            paymentFactory.Create("Income", categories.GetByType(CategoryType.Income), 1964.45, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Sallary));

            paymentFactory.Create("Rent", categories.GetByType(CategoryType.RentAndHousing), -524.70, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Rent));
            paymentFactory.Create("OperatingCosts", categories.GetByType(CategoryType.RentAndHousing), -128.70, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.OperatingCosts));
            paymentFactory.Create("Water and Heating", categories.GetByType(CategoryType.RentAndHousing), -57.36, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.WaterAndHeating));
            paymentFactory.Create("Electricity", categories.GetByType(CategoryType.RentAndHousing), -65, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Electricity));

            paymentFactory.Create("A1", categories.GetByType(CategoryType.InternetAndMobile), -32.80, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Tv));
            paymentFactory.Create("Gis", categories.GetByType(CategoryType.InternetAndMobile), -41.86, PaymentIntervalType.EverySecondMonth, subCategories.GetByType(CategoryType.Gis));
            paymentFactory.Create("Phone", categories.GetByType(CategoryType.InternetAndMobile), -16.55, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Phone));

            paymentFactory.Create("Insurance", categories.GetByType(CategoryType.Insurance), -91.92, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.CarInsurance));
            paymentFactory.Create("Lease", categories.GetByType(CategoryType.Insurance), -205.55, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Lease));
            paymentFactory.Create("Credit Appartment", categories.GetByType(CategoryType.Insurance), -220, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.CreditAppartment));

            paymentFactory.Create("Food", categories.GetByType(CategoryType.FoodAndHousehold), -200, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Food));
            paymentFactory.Create("Dog Food", categories.GetByType(CategoryType.FoodAndHousehold), -180, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.DogFood));
            paymentFactory.Create("Fuel", categories.GetByType(CategoryType.FoodAndHousehold), -60, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Fuel));

            paymentFactory.Create("Deezer", categories.GetByType(CategoryType.CreditCard), -14.99, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Streaming));
            paymentFactory.Create("Netflix", categories.GetByType(CategoryType.CreditCard), -17.99, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Streaming));
            paymentFactory.Create("OneDrive", categories.GetByType(CategoryType.CreditCard), -2, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.Streaming));

            paymentFactory.Create("OÖ Versicherung", categories.GetByType(CategoryType.ExtraExpenses), -209.13, PaymentIntervalType.Yearly);
            paymentFactory.Create("Uniqa Rechtschutz", categories.GetByType(CategoryType.ExtraExpenses), -110, PaymentIntervalType.Yearly);
            paymentFactory.Create("OÖ Hausrat", categories.GetByType(CategoryType.ExtraExpenses), -110, PaymentIntervalType.Yearly);
            paymentFactory.Create("Öamtc", categories.GetByType(CategoryType.ExtraExpenses), -133.70, PaymentIntervalType.Yearly);
            paymentFactory.Create("Bank balance", categories.GetByType(CategoryType.Bankbalance), 0.0, PaymentIntervalType.Monthly);
            paymentFactory.Create("Open Bills", categories.GetByType(CategoryType.Bankbalance), 0.0, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.OpenBills));
            paymentFactory.Create("Bank balance end of Month", categories.GetByType(CategoryType.Bankbalance), 0.0, PaymentIntervalType.Monthly, subCategories.GetByType(CategoryType.BankbalanceEndOfMonth));

            yearFact.Create("2021");
            //yearFact.Create("2022");

            return project;
        }

        public void CreateYear()
        {
            var yearFact = Services.GetService<IYearFactory>();
            yearFact.Create("New Year");
        }
    }
}