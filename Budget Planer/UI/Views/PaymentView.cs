using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UI.WinForms.Views
{
    public partial class PaymentView : Form
    {
        private IPayment _payment;
        private IEnumerable<IElementService> _services;
        private IEnumerable<CategoryType> _types;
        private IMonth _month;

        public PaymentView(IEnumerable<IElementService> services, IPayment payment, IMonth month, IEnumerable<CategoryType> categoryTypes)
        {
            InitializeComponent();

            _payment = payment;
            _services = services;
            _types = categoryTypes;
            _month = month;

            InitControls();
        }

        private IProject Project
        {
            get { return _services.GetService<ICurentProjectService>().CurrentProject; }
        }

        private ITransactionFactory TransactionFactory
        {
            get { return _services.GetService<ITransactionFactory>(); }
        }

        private void InitControls()
        {
            nameBox.Text = _payment.Name;

            amountBox.Minimum = decimal.MinValue;
            amountBox.Maximum = decimal.MaxValue;
            amountBox.Value = (decimal)_payment.Amount;

            AddCategories(categoryBox, Project.Categories.FilterByTypes(_types), new[] { _payment.Category });
            AddCategories(subCategoryBox, Project.SubCategories.FilterByTypes(_types), new[] { _payment.SubCategory });
            AddCategories(intervalBox, Project.Intervals, new[] { _payment.PayPattern });

            AddMonths();
        }

        private void AddCategories(CheckedListBox box, IEnumerable<object> items, IEnumerable<object> checkedItems)
        {
            box.Items.Clear();

            foreach (var item in items)
            {
                box.Items.Add(item, checkedItems.Contains(item));
            }
            box.MultiColumn = true;
        }

        private void AddMonths()
        {
            monthBox.Items.Clear();
            var months = new AffectedMonthsCollection();
            var checkedMonths = _payment.PayPattern.AffectedMonths;

            foreach (var month in months)
            {
                monthBox.Items.Add(month.ConvertToText(), checkedMonths.Contains(month));
            }
            monthBox.MultiColumn = true;
        }

        private void Save(object sender, System.EventArgs e)
        {
            _payment.Name = nameBox.Text;
            _payment.Amount = (double)amountBox.Value;

            foreach (var item in categoryBox.CheckedItems)
            {
                _payment.Category = (item as ICategory) ?? _payment.Category;
            }
            foreach (var item in subCategoryBox.CheckedItems)
            {
                _payment.SubCategory = (item as ICategory) ?? _payment.SubCategory;
            }
            foreach (var item in intervalBox.CheckedItems)
            {
                _payment.PayPattern.Interval = (item as IPaymentInterval) ?? _payment.PayPattern.Interval;
                _payment.PayPattern.UpdateAffectedMonths();
            }

            TransactionFactory.UpdatePayment(_payment, _month);

            Close();
        }
    }
}