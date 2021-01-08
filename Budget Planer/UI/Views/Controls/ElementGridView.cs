using DevExpress.XtraGrid.Columns;
using IData.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using IData.Constants;
using DevExpress.XtraGrid;
using IData.Services;

namespace UI.WinForms.Views.Controls
{
    public partial class ElementGridView : UserControl, IElementGridView
    {
        private IEnumerable<CategoryType> _categories;
        private IEnumerable<IService> _services;
        private IEnumerable<IElementGridView> _grids;
        private IMonth _month;
        private IEnumerable<string> _visibleColumns;

        public ElementGridView()
        {
            InitializeComponent();

            CreateGroupSummary();
            elementView.CellValueChanged += ElementView_CellValueChanged;

            InitList();
        }

        public ElementGridView(IEnumerable<CategoryType> categories, IEnumerable<string> visibleColumns, IEnumerable<IService> services, IEnumerable<IElementGridView> grids)
        {
            _categories = categories;
            _visibleColumns = visibleColumns;
            _services = services;
            _grids = grids;

            InitList();
        }

        private void ElementView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            UpdateViews();

            var newValue = e.Value;
            var row = elementView.GetRow(e.RowHandle);
            var transaction = row as ITransaction;

            if (e.Column.FieldName == "Payed")
            {
                transaction.Month.Element.UpdateBankBalanceEndOfMonth();
            }
            else if (e.Column.FieldName == "Amount")
            {
                //Change in Amount.Setter
                transaction.Month.Element.UpdateBankBalanceRow(transaction);

                transaction.Month.Element.UpdateBankBalanceEndOfMonth();
            }

            UpdateViews();
        }

        public void InitView(IEnumerable<CategoryType> categories, IEnumerable<IService> services)
        {
            _categories = categories;
            _services = services;
        }

        private void CreateGroupSummary()
        {
            var summaryItem = new GridGroupSummaryItem();
            summaryItem.FieldName = "Amount";
            summaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            summaryItem.DisplayFormat = " {0}€";

            elementView.GroupSummary.Add(summaryItem);
            AddContextMenu();
        }

        private void InitList()
        {
            var visibleColumns = new List<string> { "Category", "Name", "Payed", "Amount" };
            var groupColumns = new List<string> { "Category" };

            CreateColumns(visibleColumns, groupColumns);
        }

        private void InitList(List<string> visibleColumns, List<string> groupColumns)
        {
            CreateColumns(visibleColumns, groupColumns);
        }

        private void AddContextMenu()
        {
            var addNew = new ToolStripButton("New");
            addNew.Click += AddNewPayment;

            var edit = new ToolStripButton("Edit");
            edit.Click += EditPayment;

            contextMenu.Items.Add(edit);
            contextMenu.Items.Add(addNew);
        }

        private void AddNewPayment(object sender, System.EventArgs e)
        {
            var payment = _services.GetService<IPaymentFactory>().CreateEmpty() as IPayment;
            payment.IsNew = true;

            ShowPaymentView(payment);
        }

        private void EditPayment(object sender, System.EventArgs e)
        {
            ShowPaymentView(GetSelectedItem.Payment.Element);
        }

        private void ShowPaymentView(IPayment newPayment)
        {
            var view = new PaymentView(_services, newPayment, _month, _categories);
            view.ShowDialog();

            if (newPayment.IsNew)
            {
                DeleteElement(newPayment);
            }

            UpdateViews();
        }

        private void DeleteElement(IElement element)
        {
            _services.GetService<IElementFactory>().Delete(element);
        }

        private ITransaction GetSelectedItem
        {
            get
            {
                var rowHandle = elementView.GetSelectedRows().FirstOrDefault();
                return elementView.GetRow(rowHandle) as ITransaction;
            }
        }

        private void CreateColumns(List<string> visibleColumns, List<string> groupbyColumns)
        {
            foreach (GridColumn column in elementView.Columns)
            {
                column.Visible = visibleColumns.Contains(column.FieldName);

                if (column.Visible)
                {
                    column.VisibleIndex = visibleColumns.IndexOf(column.FieldName) - 1;
                }
            }

            foreach (var column in groupbyColumns)
            {
                var col = elementView.Columns.SingleOrDefault(c => c.FieldName == column);

                if (col != null)
                {
                    col.Group();
                }
            }

            elementView.ExpandAllGroups();
        }

        public void FillItems(IMonth month, IEnumerable<IElementGridView> grids)
        {
            _month = month;
            _grids = grids;
            FillView(month);
        }

        private void FillView(IMonth month)
        {
            elementGridControl.BeginUpdate();

            elementGridControl.DataSource = month.Transactions.Elements.GetTransactionsForCategory(_categories);

            InitList(); // Needed: Setting the DataSource resets the Columns

            elementGridControl.EndUpdate();
        }

        public void Refresh()
        {
            FillView(_month);
        }

        public void UpdateViews()
        {
            foreach (var grid in _grids)
            {
                grid.Refresh();
            }
        }
    }
}