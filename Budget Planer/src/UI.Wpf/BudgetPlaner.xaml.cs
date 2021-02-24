using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UI.DefaultData;
using UI.ViewModel;
using UI.Wpf.Animations;
using UI.Wpf.TreeHelpers;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private CustomAnimations _animations;

        public MainWindow()
        {
            InitializeComponent();

            SetNewDefaultData();
        }

        private void baseView_Loaded(object sender, RoutedEventArgs e)
        {
            var billsCordination = GetRectangle(heading_Bills, baseView);
            var foodCordination = GetRectangle(heading_FoodBills, baseView);
            var creditCordination = GetRectangle(heading_creditCards, baseView);
            var expectedCordination = GetRectangle(heading_expectedBills, baseView);

            _animations = new CustomAnimations(billsCordination, expectedCordination, creditCordination, foodCordination);
        }

        private Rect GetRectangle(FrameworkElement child, FrameworkElement parent)
        {
            GeneralTransform generalTransform = child.TransformToVisual(parent); //parentElement should be replaced with the Parent Element Name
            Rect rectangle = generalTransform.TransformBounds(new Rect(new Point(child.Margin.Left, child.Margin.Top), child.RenderSize));

            return rectangle;
        }

        private void CreateMenu()
        {
            while (mainMenu.Items.Count > 4)
            {
                mainMenu.Items.RemoveAt(4);
            }

            if (GetProjectViewModel == null)
            {
                return;
            }

            foreach (var year in GetProjectViewModel.YearVms)
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

            var parent = menu.Parent as MenuItem;
            var project = parent.Tag as YearViewModel;

            if (GetProjectViewModel != null)
            {
                GetProjectViewModel.CurrentYear = project;
                GetProjectViewModel.CurrentYear.SelectNewCurrentMonth(month);

                GetProjectViewModel.UpdateViewModels();
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            // todo
            if (GetProjectViewModel != null && GetProjectViewModel.SelectNextMonth())
            {
                _animations.StartAnimation(AnimationTag.Next, innerPanel);
                GetProjectViewModel.CurrentYear.CurrentMonthVm.UpdateLists();
            }
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            // todo
            if (GetProjectViewModel != null && GetProjectViewModel.SelectPreviousMonth())
            {
                _animations.StartAnimation(AnimationTag.Previous, innerPanel);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (GetProjectViewModel != null)
            {
                GetProjectViewModel.SaveToXml();
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TestData().LoadFromXml();
            CreateMenu();
        }

        private void SetNewDefaultData()
        {
#if DEBUG
            DataContext = new TestData().LoadFromXml();
#endif

#if RELEASE
            DataContext = new TestData().GetDefaultData();
#endif

            CreateMenu();
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
                var hiddenElements = new FrameworkElement[] { monthDisplay, heading_Bills, monthly_add, heading_creditCards, heading_expectedBills, bills_list, creditCard, expectedBills, };

                _animations.StartAnimation(AnimationTag.RightToTopLeft, foodBills, hiddenElements);
                _animations.StartAnimation(AnimationTag.RightToTopLeft, heading_FoodBills);
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel);
            }

            if (tag == AnimationTag.CreditCardBills)
            {
                var hideElements = new FrameworkElement[] { monthDisplay, heading_Bills, monthly_add, heading_FoodBills, heading_expectedBills, bills_list, foodBills, expectedBills, };
                _animations.StartAnimation(AnimationTag.RightToLeft, creditCard, hideElements);
                _animations.StartAnimation(AnimationTag.RightToLeft, heading_creditCards);
                _animations.StartAnimation(AnimationTag.Payment, paymentPanel);
            }

            if (tag == AnimationTag.ExpectedUnexpectedBills)
            {
                var hideElements = new FrameworkElement[] { monthDisplay, heading_Bills, monthly_add, heading_FoodBills, heading_creditCards, bills_list, foodBills, creditCard, };
                _animations.StartAnimation(AnimationTag.MiddleToLeft, expectedBills, hideElements);
                _animations.StartAnimation(AnimationTag.MiddleToLeft, heading_expectedBills);
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

            var listview = button.FindParentOfType<ListView>();

            if (listview != null)
            {
                listview.SelectedItem = transaction;
            }

            transaction.CurrentMonthVm = GetProjectViewModel.CurrentYear.CurrentMonthVm;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //save_Click(sender, e);

            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            SetNewDefaultData();
        }

        private void Add_Payment_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var project = (ProjectViewModel)button.DataContext;

            var monthVm = project.CurrentYear.CurrentMonthVm;
            monthVm.AddNewTransaction();
        }

        private void ClosePayment_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var payment = (PaymentViewModel)button.DataContext;

            if (payment != null)
            {
                payment.Update();

                GetProjectViewModel.UpdateViewModels();
            }

            _animations.ResetAnimations();
        }

        private void Add_Year_Click(object sender, RoutedEventArgs e)
        {
            GetProjectViewModel.AddYear();

            CreateMenu();
        }
    }
}