
namespace UI.WinForms.Views
{
    partial class PaymentView
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
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.monthBox = new System.Windows.Forms.CheckedListBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.amountBox = new System.Windows.Forms.NumericUpDown();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.save = new System.Windows.Forms.Button();
            this.intervalRadio = new DevExpress.XtraEditors.RadioGroup();
            this.categoryRadio = new DevExpress.XtraEditors.RadioGroup();
            this.subCategoryRadio = new DevExpress.XtraEditors.RadioGroup();
            this.layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervalRadio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryRadio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoryRadio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.monthBox, 1, 5);
            this.layout.Controls.Add(this.labelControl3, 0, 2);
            this.layout.Controls.Add(this.labelControl1, 0, 0);
            this.layout.Controls.Add(this.nameBox, 1, 0);
            this.layout.Controls.Add(this.labelControl2, 0, 1);
            this.layout.Controls.Add(this.amountBox, 1, 1);
            this.layout.Controls.Add(this.labelControl4, 0, 3);
            this.layout.Controls.Add(this.labelControl5, 0, 4);
            this.layout.Controls.Add(this.save, 1, 6);
            this.layout.Controls.Add(this.intervalRadio, 1, 4);
            this.layout.Controls.Add(this.categoryRadio, 1, 2);
            this.layout.Controls.Add(this.subCategoryRadio, 1, 3);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 7;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.028573F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.028573F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.14286F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.14286F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.14286F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06117F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.453093F));
            this.layout.Size = new System.Drawing.Size(927, 553);
            this.layout.TabIndex = 0;
            // 
            // monthBox
            // 
            this.monthBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.monthBox.Location = new System.Drawing.Point(103, 474);
            this.monthBox.MultiColumn = true;
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(821, 49);
            this.monthBox.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(3, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(45, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Category";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameBox.Location = new System.Drawing.Point(103, 3);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(821, 20);
            this.nameBox.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Amount";
            // 
            // amountBox
            // 
            this.amountBox.DecimalPlaces = 2;
            this.amountBox.Location = new System.Drawing.Point(103, 30);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(242, 20);
            this.amountBox.TabIndex = 2;
            this.amountBox.ThousandsSeparator = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(3, 196);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Sub Category";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 335);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(81, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Payment pattern";
            // 
            // save
            // 
            this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.save.Location = new System.Drawing.Point(849, 529);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 21);
            this.save.TabIndex = 11;
            this.save.Text = "OK";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.Save);
            // 
            // intervalRadio
            // 
            this.intervalRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intervalRadio.Location = new System.Drawing.Point(103, 335);
            this.intervalRadio.Name = "intervalRadio";
            this.intervalRadio.Size = new System.Drawing.Size(821, 133);
            this.intervalRadio.TabIndex = 12;
            this.intervalRadio.EditValueChanged += new System.EventHandler(this.intervalRadio_EditValueChanged);
            // 
            // categoryRadio
            // 
            this.categoryRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryRadio.Location = new System.Drawing.Point(103, 57);
            this.categoryRadio.Name = "categoryRadio";
            this.categoryRadio.Size = new System.Drawing.Size(821, 133);
            this.categoryRadio.TabIndex = 13;
            // 
            // subCategoryRadio
            // 
            this.subCategoryRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subCategoryRadio.Location = new System.Drawing.Point(103, 196);
            this.subCategoryRadio.Name = "subCategoryRadio";
            this.subCategoryRadio.Size = new System.Drawing.Size(821, 133);
            this.subCategoryRadio.TabIndex = 13;
            // 
            // PaymentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 553);
            this.Controls.Add(this.layout);
            this.Name = "PaymentView";
            this.Text = "TransactionView";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervalRadio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryRadio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoryRadio.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox nameBox;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.NumericUpDown amountBox;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.CheckedListBox monthBox;
        private System.Windows.Forms.Button save;
        private DevExpress.XtraEditors.RadioGroup intervalRadio;
        private DevExpress.XtraEditors.RadioGroup categoryRadio;
        private DevExpress.XtraEditors.RadioGroup subCategoryRadio;
    }
}