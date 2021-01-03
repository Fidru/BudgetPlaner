
namespace UI.WinForms.Views.Controls
{
    partial class ElementGridView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.elementGridControl = new DevExpress.XtraGrid.GridControl();
            this.elementView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.elementGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementView)).BeginInit();
            this.SuspendLayout();
            // 
            // elementGridControl
            // 
            this.elementGridControl.ContextMenuStrip = this.contextMenu;
            this.elementGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementGridControl.Location = new System.Drawing.Point(0, 0);
            this.elementGridControl.MainView = this.elementView;
            this.elementGridControl.Name = "elementGridControl";
            this.elementGridControl.Size = new System.Drawing.Size(523, 330);
            this.elementGridControl.TabIndex = 0;
            this.elementGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.elementView});
            // 
            // elementView
            // 
            this.elementView.GridControl = this.elementGridControl;
            this.elementView.Name = "elementView";
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(181, 26);
            // 
            // ElementGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementGridControl);
            this.Name = "ElementGridView";
            this.Size = new System.Drawing.Size(523, 330);
            ((System.ComponentModel.ISupportInitialize)(this.elementGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl elementGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView elementView;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}
