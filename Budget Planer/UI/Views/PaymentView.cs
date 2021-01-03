using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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

            AddCategories(categoryRadio, Project.Categories.FilterByTypes(_types), _payment.Category.Element);
            AddCategories(subCategoryRadio, Project.SubCategories.FilterByTypes(_types), _payment.SubCategory.Element);
            AddCategories(intervalRadio, Project.Intervals, _payment.PayPattern.Element);

            FillMonthListBox(((IPayPattern)_payment.PayPattern.Element).AffectedMonths);
        }

        private void AddCategories(RadioGroup box, IEnumerable<IElement> items, IElement checkedItem)
        {
            box.Properties.Items.Clear();

            foreach (var item in items)
            {
                var radioItem = new RadioGroupItem(item, item.Name);

                box.Properties.Items.Add(radioItem);
            }

            box.SelectedIndex = items.ToList().IndexOf(checkedItem);
        }

        private void FillMonthListBox(IEnumerable<MonthEnum> checkedMonths)
        {
            monthBox.Items.Clear();
            var months = new AffectedMonthsCollection();

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

            _payment.Category.Element = GetCheckedCategory ?? _payment.Category.Element;
            _payment.SubCategory.Element = GetCheckedSubCategory ?? _payment.SubCategory.Element;
            _payment.PayPattern.Element.Interval = GetCheckedInterval ?? _payment.PayPattern.Element.Interval;
            _payment.PayPattern.Element.UpdateAffectedMonths();

            TransactionFactory.UpdatePayment(_payment, _month);

            Close();
        }

        private ICategory GetCheckedCategory
        {
            get { return GetItemFromRadioBox(categoryRadio) as ICategory; }
        }

        private ICategory GetCheckedSubCategory
        {
            get { return GetItemFromRadioBox(subCategoryRadio) as ICategory; }
        }

        private IPaymentInterval GetCheckedInterval
        {
            get { return GetItemFromRadioBox(intervalRadio) as IPaymentInterval; }
        }

        private IElement GetItemFromRadioBox(RadioGroup radio)
        {
            if (radio.SelectedIndex < 0)
                return null;

            var radioItem = radio.Properties.Items[radio.SelectedIndex] as RadioGroupItem;

            return radioItem.Value as IElement;
        }

        private void intervalRadio_EditValueChanged(object sender, System.EventArgs e)
        {
            var affectedMonths = new AffectedMonthsCollection(_month.MonthType, GetCheckedInterval.Type);

            FillMonthListBox(affectedMonths);
        }
    }
}