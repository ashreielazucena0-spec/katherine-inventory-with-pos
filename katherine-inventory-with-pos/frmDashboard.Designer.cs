namespace katherine_inventory_with_pos.Forms
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tlpCards = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSales = new System.Windows.Forms.Panel();
            this.lblSalesTitle = new System.Windows.Forms.Label();
            this.dgvSalesByDay = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTopTitle = new System.Windows.Forms.Label();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.pnlActivity = new System.Windows.Forms.Panel();
            this.lblActivityTitle = new System.Windows.Forms.Label();
            this.dgvActivity = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.pnlSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesByDay)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.pnlActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).BeginInit();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.lblUpdated);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 72);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitle.Location = new System.Drawing.Point(22, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard";
            //
            // lblUpdated
            //
            this.lblUpdated.AutoSize = true;
            this.lblUpdated.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdated.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblUpdated.Location = new System.Drawing.Point(26, 42);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(120, 15);
            this.lblUpdated.TabIndex = 1;
            this.lblUpdated.Text = "Sales and stock overview";
            //
            // btnRefresh
            //
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.btnRefresh.Location = new System.Drawing.Point(958, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(118, 38);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // tlpCards
            //
            this.tlpCards.ColumnCount = 4;
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCards.Location = new System.Drawing.Point(0, 72);
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.Padding = new System.Windows.Forms.Padding(24, 16, 12, 8);
            this.tlpCards.RowCount = 1;
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCards.Size = new System.Drawing.Size(1100, 112);
            this.tlpCards.TabIndex = 1;
            //
            // tlpBody
            //
            this.tlpBody.ColumnCount = 2;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.Controls.Add(this.pnlSales, 0, 0);
            this.tlpBody.Controls.Add(this.pnlTop, 1, 0);
            this.tlpBody.Controls.Add(this.pnlActivity, 0, 1);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Location = new System.Drawing.Point(0, 184);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.Padding = new System.Windows.Forms.Padding(24, 8, 12, 12);
            this.tlpBody.RowCount = 2;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.Size = new System.Drawing.Size(1100, 516);
            this.tlpBody.TabIndex = 2;
            //
            // pnlSales
            //
            this.pnlSales.BackColor = System.Drawing.Color.White;
            this.pnlSales.Controls.Add(this.dgvSalesByDay);
            this.pnlSales.Controls.Add(this.lblSalesTitle);
            this.pnlSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSales.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.pnlSales.Name = "pnlSales";
            this.pnlSales.Padding = new System.Windows.Forms.Padding(1);
            this.pnlSales.TabIndex = 0;
            //
            // lblSalesTitle
            //
            this.lblSalesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSalesTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblSalesTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblSalesTitle.Name = "lblSalesTitle";
            this.lblSalesTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblSalesTitle.Size = new System.Drawing.Size(100, 40);
            this.lblSalesTitle.TabIndex = 0;
            this.lblSalesTitle.Text = "Sales - Last 7 Days";
            this.lblSalesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvSalesByDay
            //
            this.dgvSalesByDay.AllowUserToAddRows = false;
            this.dgvSalesByDay.AllowUserToDeleteRows = false;
            this.dgvSalesByDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalesByDay.Name = "dgvSalesByDay";
            this.dgvSalesByDay.ReadOnly = true;
            this.dgvSalesByDay.TabIndex = 1;
            //
            // pnlTop
            //
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.dgvTopProducts);
            this.pnlTop.Controls.Add(this.lblTopTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(1);
            this.pnlTop.TabIndex = 1;
            //
            // lblTopTitle
            //
            this.lblTopTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTopTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTopTitle.Name = "lblTopTitle";
            this.lblTopTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblTopTitle.Size = new System.Drawing.Size(100, 40);
            this.lblTopTitle.TabIndex = 0;
            this.lblTopTitle.Text = "Top Selling Products";
            this.lblTopTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvTopProducts
            //
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.TabIndex = 1;
            //
            // pnlActivity
            //
            this.pnlActivity.BackColor = System.Drawing.Color.White;
            this.pnlActivity.Controls.Add(this.dgvActivity);
            this.pnlActivity.Controls.Add(this.lblActivityTitle);
            this.pnlActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActivity.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlActivity.Name = "pnlActivity";
            this.pnlActivity.Padding = new System.Windows.Forms.Padding(1);
            this.pnlActivity.TabIndex = 2;
            this.tlpBody.SetColumnSpan(this.pnlActivity, 2);
            //
            // lblActivityTitle
            //
            this.lblActivityTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActivityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblActivityTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblActivityTitle.Name = "lblActivityTitle";
            this.lblActivityTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblActivityTitle.Size = new System.Drawing.Size(100, 40);
            this.lblActivityTitle.TabIndex = 0;
            this.lblActivityTitle.Text = "Recent Stock Activity";
            this.lblActivityTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvActivity
            //
            this.dgvActivity.AllowUserToAddRows = false;
            this.dgvActivity.AllowUserToDeleteRows = false;
            this.dgvActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivity.Name = "dgvActivity";
            this.dgvActivity.ReadOnly = true;
            this.dgvActivity.TabIndex = 1;
            //
            // frmDashboard
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.tlpBody);
            this.Controls.Add(this.tlpCards);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tlpCards.ResumeLayout(false);
            this.tlpBody.ResumeLayout(false);
            this.pnlSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesByDay)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.pnlActivity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private System.Windows.Forms.Panel pnlSales;
        private System.Windows.Forms.Label lblSalesTitle;
        private System.Windows.Forms.DataGridView dgvSalesByDay;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTopTitle;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Panel pnlActivity;
        private System.Windows.Forms.Label lblActivityTitle;
        private System.Windows.Forms.DataGridView dgvActivity;
    }
}