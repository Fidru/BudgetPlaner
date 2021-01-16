using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UI.WinForms;
using UI.Wpf.Animations;
using UI.Wpf.ViewModel;
using UI.Wpf.ViewModel.Factories;
using XmlSaver.Save;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly CustomAnimations _animations;
        private ProjectViewModel _projectVm;

        private IEnumerable<IService> _services { get; set; }
        public object Threat { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            SetNewDefaultData();

            _animations = new CustomAnimations();
        }

        private void CreateMenu()
        {
            while (mainMenu.Items.Count > 3)
            {
                mainMenu.Items.RemoveAt(3);
            }

            foreach (var year in GetProjectViewModel.YearsVm)
            {
                var mainItem = new MenuItem();
                mainItem.Header = year.Name;
                mainItem.Tag = year;

                foreach (var month in year.MonthVms)
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

        private ProjectViewModel GetProjectViewModel
        {
            get { return ((ProjectViewModel)DataContext); }
        }

        private void MonthClicked(object sender, RoutedEventArgs e)
        {
            var menu = sender as MenuItem;
            var month = menu.Tag as MonthViewModel;

            _projectVm.CurrentYear.SelectNewCurrentMonth(month);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            // todo
            _projectVm.CurrentYear.SelectNextMonth();
            _animations.StartAnimation(AnimationTag.Next, innerPanel);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            // todo
            _projectVm.CurrentYear.SelectPreviousMonth();
            _animations.StartAnimation(AnimationTag.Previous, innerPanel);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MyXmlSaver saver = new MyXmlSaver();
            saver.Save(_projectVm.Element);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            IProject project = new MyXmlSaver().Read(_services);

            Load(project);
        }

        private void Load(IProject project)
        {
            var repService =

            _projectVm = new ProjectViewModelFacotry(_services).ConvertToVm(project);
            DataContext = _projectVm;

            CreateMenu();
        }

        private void SetNewDefaultData()
        {
            var data = new TestData();
            _services = data.Services;

            Load(data.Project);
        }

        private void Edit_Bills_Click(object sender, RoutedEventArgs e)
        {
            SelectTransaction(sender);

            ShowPaymentView(AnimationTag.Bills);
        }

        private void ShowPaymentView(AnimationTag tag)
        {
            if (_animations.PaymentIsActive)
            {
                return;
            }

            if (tag == AnimationTag.Bills)
            {
                var hideElements = new FrameworkElement[] { monthDisplay, heading_FoodBills, heading_creditCards, heading_expectedBills, foodBills, creditCard, expectedBills, };
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel, hideElements);
            }

            if (tag == AnimationTag.FoodBills)
            {
                var hiddenElements = new FrameworkElement[] { monthDisplay, heading_Bills, heading_creditCards, heading_expectedBills, bills, creditCard, expectedBills, };

                _animations.StartAnimation(AnimationTag.MiddleToLeft, foodBills, hiddenElements);
                _animations.StartAnimation(AnimationTag.MiddleToLeft, heading_FoodBills);
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel);
            }

            if (tag == AnimationTag.CreditCardBills)
            {
                var hideElements = new FrameworkElement[] { monthDisplay, heading_Bills, heading_FoodBills, heading_expectedBills, bills, foodBills, expectedBills, };
                _animations.StartAnimation(AnimationTag.RightToLeft, creditCard, hideElements);
                _animations.StartAnimation(AnimationTag.RightToLeft, heading_creditCards);
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel);
            }

            if (tag == AnimationTag.ExpectedUnexpectedBills)
            {
                var hideElements = new FrameworkElement[] { monthDisplay, heading_Bills, heading_FoodBills, heading_creditCards, bills, foodBills, creditCard, };
                _animations.StartAnimation(AnimationTag.RightToTopLeft, expectedBills, hideElements);
                _animations.StartAnimation(AnimationTag.RightToTopLeft, heading_expectedBills);
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel);
            }
        }

        private void Edit_FoodBills_Click(object sender, RoutedEventArgs e)
        {
            SelectTransaction(sender);
            ShowPaymentView(AnimationTag.FoodBills);
        }

        private void Edit_CreditCardBills_Click(object sender, RoutedEventArgs e)
        {
            SelectTransaction(sender);
            ShowPaymentView(AnimationTag.CreditCardBills);
        }

        private void Edit_ExpectedBills_Click(object sender, RoutedEventArgs e)
        {
            SelectTransaction(sender);
            ShowPaymentView(AnimationTag.ExpectedUnexpectedBills);
        }

        private void SelectTransaction(object sender)
        {
            Button button = (Button)sender;
            var transaction = (TransactionViewModel)button.DataContext;

            transaction.CurrentMonthVm = GetProjectViewModel.CurrentYear.CurrentMonthVm;
            transaction.SelectTransaction();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //new MyXmlSaver().Save(_project);

            //Thread.Sleep(2000);
            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            SetNewDefaultData();
        }

        private void Add_Payment_Click(object sender, RoutedEventArgs e)
        {
            var paymentService = _services.GetService<IPaymentFactory>();
            var newPayment = paymentService.CreateEmpty() as IPayment;

            var monthVm = GetProjectViewModel.CurrentYear.CurrentMonthVm;

            var transactionService = _services.GetService<ITransactionFactory>();

            var newTransaction = transactionService.Create(monthVm.Element, newPayment);
            var transactionVm = new TransactionViewModelFacotry(_services).ConvertToVm(newTransaction);
            transactionVm.CurrentMonthVm = monthVm;
            transactionVm.SelectTransaction();

            monthVm.AddTransaction(transactionVm);

            ShowPaymentView(AnimationTag.Bills);
        }

        private void ClosePayment_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var payment = (PaymentViewModel)button.DataContext;

            payment.Update();
            _animations.ResetAnimations();
        }
    }
}