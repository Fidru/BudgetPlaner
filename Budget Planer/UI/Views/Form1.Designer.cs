
namespace UI.WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.save = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.Menu_Panel = new System.Windows.Forms.SplitContainer();
            this.Design = new System.Windows.Forms.SplitContainer();
            this.tree = new System.Windows.Forms.TreeView();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.expectedGrid = new UI.WinForms.Views.Controls.ElementGridView();
            this.monthlyBills = new UI.WinForms.Views.Controls.ElementGridView();
            this.foodGrid = new UI.WinForms.Views.Controls.ElementGridView();
            this.creditCardGrid = new UI.WinForms.Views.Controls.ElementGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Category = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Payed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Panel)).BeginInit();
            this.Menu_Panel.Panel1.SuspendLayout();
            this.Menu_Panel.Panel2.SuspendLayout();
            this.Menu_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Design)).BeginInit();
            this.Design.Panel1.SuspendLayout();
            this.Design.Panel2.SuspendLayout();
            this.Design.SuspendLayout();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(93, 12);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 0;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.button1_Click);
            // 
            // read
            // 
            this.read.Location = new System.Drawing.Point(12, 12);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(75, 23);
            this.read.TabIndex = 1;
            this.read.Text = "Load";
            this.read.UseVisualStyleBackColor = true;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // Menu_Panel
            // 
            this.Menu_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Menu_Panel.IsSplitterFixed = true;
            this.Menu_Panel.Location = new System.Drawing.Point(0, 0);
            this.Menu_Panel.Name = "Menu_Panel";
            this.Menu_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Menu_Panel.Panel1
            // 
            this.Menu_Panel.Panel1.Controls.Add(this.read);
            this.Menu_Panel.Panel1.Controls.Add(this.save);
            // 
            // Menu_Panel.Panel2
            // 
            this.Menu_Panel.Panel2.Controls.Add(this.Design);
            this.Menu_Panel.Size = new System.Drawing.Size(1045, 603);
            this.Menu_Panel.SplitterDistance = 41;
            this.Menu_Panel.TabIndex = 2;
            // 
            // Design
            // 
            this.Design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Design.Location = new System.Drawing.Point(0, 0);
            this.Design.Name = "Design";
            // 
            // Design.Panel1
            // 
            this.Design.Panel1.Controls.Add(this.tree);
            // 
            // Design.Panel2
            // 
            this.Design.Panel2.Controls.Add(this.layout);
            this.Design.Size = new System.Drawing.Size(1045, 558);
            this.Design.SplitterDistance = 113;
            this.Design.TabIndex = 0;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(113, 558);
            this.tree.TabIndex = 0;
            // 
            // layout
            // 
            this.layout.ColumnCount = 3;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.layout.Controls.Add(this.expectedGrid, 0, 3);
            this.layout.Controls.Add(this.monthlyBills, 0, 1);
            this.layout.Controls.Add(this.foodGrid, 1, 1);
            this.layout.Controls.Add(this.creditCardGrid, 2, 1);
            this.layout.Controls.Add(this.label2, 0, 0);
            this.layout.Controls.Add(this.label4, 2, 2);
            this.layout.Controls.Add(this.label3, 1, 0);
            this.layout.Controls.Add(this.label1, 2, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 4;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Size = new System.Drawing.Size(928, 558);
            this.layout.TabIndex = 3;
            // 
            // expectedGrid
            // 
            this.expectedGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expectedGrid.Location = new System.Drawing.Point(624, 302);
            this.expectedGrid.Name = "expectedGrid";
            this.expectedGrid.Size = new System.Drawing.Size(301, 253);
            this.expectedGrid.TabIndex = 8;
            // 
            // monthlyBills
            // 
            this.monthlyBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthlyBills.Location = new System.Drawing.Point(3, 23);
            this.monthlyBills.Name = "monthlyBills";
            this.layout.SetRowSpan(this.monthlyBills, 3);
            this.monthlyBills.Size = new System.Drawing.Size(309, 532);
            this.monthlyBills.TabIndex = 0;
            // 
            // foodGrid
            // 
            this.foodGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foodGrid.Location = new System.Drawing.Point(318, 23);
            this.foodGrid.Name = "foodGrid";
            this.layout.SetRowSpan(this.foodGrid, 3);
            this.foodGrid.Size = new System.Drawing.Size(300, 532);
            this.foodGrid.TabIndex = 1;
            // 
            // creditCardGrid
            // 
            this.creditCardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creditCardGrid.Location = new System.Drawing.Point(624, 23);
            this.creditCardGrid.Name = "creditCardGrid";
            this.creditCardGrid.Size = new System.Drawing.Size(301, 253);
            this.creditCardGrid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Monthly Bills";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Expected / Unexpected Bills";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Food Bills";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(624, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Credit Card Bills";
            // 
            // Category
            // 
            this.Category.Caption = "Category";
            this.Category.Name = "Category";
            this.Category.Visible = true;
            this.Category.VisibleIndex = 0;
            // 
            // Payed
            // 
            this.Payed.Caption = "Payed";
            this.Payed.Name = "Payed";
            this.Payed.Visible = true;
            this.Payed.VisibleIndex = 1;
            // 
            // Amount
            // 
            this.Amount.Caption = "€";
            this.Amount.Name = "Amount";
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 603);
            this.Controls.Add(this.Menu_Panel);
            this.Name = "Form1";
            this.Text = "x";
            this.Menu_Panel.Panel1.ResumeLayout(false);
            this.Menu_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Panel)).EndInit();
            this.Menu_Panel.ResumeLayout(false);
            this.Design.Panel1.ResumeLayout(false);
            this.Design.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Design)).EndInit();
            this.Design.ResumeLayout(false);
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.SplitContainer Menu_Panel;
        private System.Windows.Forms.SplitContainer Design;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.TableLayoutPanel layout;
        private DevExpress.XtraGrid.Columns.GridColumn Category;
        private DevExpress.XtraGrid.Columns.GridColumn Payed;
        private DevExpress.XtraGrid.Columns.GridColumn Amount;
        private Views.Controls.ElementGridView monthlyBills;
        private Views.Controls.ElementGridView foodGrid;
        private Views.Controls.ElementGridView creditCardGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Views.Controls.ElementGridView expectedGrid;
        private System.Windows.Forms.Label label4;
    }
}

