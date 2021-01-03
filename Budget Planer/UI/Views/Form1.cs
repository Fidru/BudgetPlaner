using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UI.WinForms.Views.Controls;
using XmlSaver.Save;

namespace UI.WinForms
{
    public partial class Form1 : Form
    {
        private IProject _project;
        private IEnumerable<IElementService> _services;
        private MyXmlSaver saver;

        public Form1(IProject project, IEnumerable<IElementService> services)
        {
            InitializeComponent();
            saver = new MyXmlSaver();

            _project = project;
            _services = services;

            tree.AfterSelect += Tree_AfterSelect;

            var types = new CategoryTypeCollection();

            monthlyBills.InitView(types.MonthlyBills, services);
            creditCardGrid.InitView(types.CreditCardCategories, services);
            foodGrid.InitView(types.FoodCategories, services);
            expectedGrid.InitView(types.ExpectedUnexpectedCategories, services);

            InitViews();
        }

        private void InitViews()
        {
            InitTreeView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateView();

            saver.Save(_project);
        }

        private void UpdateView()
        {
            var month = GetSelectedMonth;

            if (month == null)
            {
                return;
            }
        }

        private void read_Click(object sender, EventArgs e)
        {
            _project = saver.Read(_services);

            InitViews();
        }

        private void InitTreeView()
        {
            tree.Nodes.Clear();

            TreeNode firstMonth = null;

            foreach (var year in _project.Years)
            {
                var node = new TreeNode(year.Name);
                node.Tag = year;

                foreach (var month in year.Months.Elements)
                {
                    var child = new TreeNode(month.Name)
                    {
                        Tag = month
                    };
                    if (firstMonth == null)
                    {
                        firstMonth = child;
                    }
                    node.Nodes.Add(child);
                }

                tree.Nodes.Add(node);
            }

            tree.Refresh();
            tree.ExpandAll();

            if (firstMonth != null)
            {
                tree.SelectedNode = firstMonth;
            }
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedMonth = GetSelectedMonth;

            if (selectedMonth == null)
            {
                return;
            }

            monthlyBills.FillItems(selectedMonth, Grids);
            creditCardGrid.FillItems(selectedMonth, Grids);
            foodGrid.FillItems(selectedMonth, Grids);
            expectedGrid.FillItems(selectedMonth, Grids);
        }

        private IEnumerable<IElementGridView> Grids
        {
            get { return new[] { monthlyBills, creditCardGrid, foodGrid, expectedGrid }; }
        }

        private IMonth GetSelectedMonth
        {
            get
            {
                return tree.SelectedNode?.Tag as IMonth;
            }
        }
    }
}