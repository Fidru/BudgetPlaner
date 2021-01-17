﻿using System.Windows;
using System.Windows.Controls;
using UI.DefaultData;
using UI.ViewModel;
using UI.Wpf.Animations;

namespace UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly CustomAnimations _animations;

        public MainWindow()
        {
            InitializeComponent();
            _animations = new CustomAnimations();

            SetNewDefaultData();
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
            DataContext = new TestData().GetDefaultData();
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

            ShowPaymentView(AnimationTag.Bills);
        }

        private void ClosePayment_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var payment = (PaymentViewModel)button.DataContext;

            payment.Update();

            GetProjectViewModel.UpdateViewModels();

            _animations.ResetAnimations();
        }

        private void Add_Year_Click(object sender, RoutedEventArgs e)
        {
            GetProjectViewModel.AddYear();

            CreateMenu();
        }
    }
}