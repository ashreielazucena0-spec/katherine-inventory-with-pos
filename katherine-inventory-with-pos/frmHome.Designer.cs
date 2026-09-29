namespace katherine_inventory_with_pos.Forms
{
    partial class frmHome
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.FlowLayoutPanel();
            this.cardProducts = new System.Windows.Forms.Panel();
            this.lblProductsValue = new System.Windows.Forms.Label();
            this.lblProductsCaption = new System.Windows.Forms.Label();
            this.cardStock = new System.Windows.Forms.Panel();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.lblStockCaption = new System.Windows.Forms.Label();
            this.cardSales = new System.Windows.Forms.Panel();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.lblSalesCaption = new System.Windows.Forms.Label();
            this.cardLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockValue = new System.Windows.Forms.Label();
            this.lblLowStockCaption = new System.Windows.Forms.Label();
            this.lblLowStockList = new System.Windows.Forms.Label();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.pnlCards.SuspendLayout();
            this.cardProducts.SuspendLayout();
            this.cardStock.SuspendLayout();
            this.cardSales.SuspendLayout();
            this.cardLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.SuspendLayout();
            //
            // lblWelcome
            //
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(20, 16);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome!";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(22, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(300, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Here is a quick snapshot of your store today.";
            //
            // pnlCards
            //
            this.pnlCards.Controls.Add(this.cardProducts);
            this.pnlCards.Controls.Add(this.cardStock);
            this.pnlCards.Controls.Add(this.cardSales);
            this.pnlCards.Controls.Add(this.cardLowStock);
            this.pnlCards.Location = new System.Drawing.Point(20, 80);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(960, 130);
            this.pnlCards.TabIndex = 2;
            //
            // cardProducts
            //
            this.cardProducts.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.cardProducts.Controls.Add(this.lblProductsCaption);
            this.cardProducts.Controls.Add(this.lblProductsValue);
            this.cardProducts.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardProducts.Name = "cardProducts";
            this.cardProducts.Size = new System.Drawing.Size(220, 120);
            this.cardProducts.TabIndex = 0;
            //
            // lblProductsValue
            //
            this.lblProductsValue.AutoSize = true;
            this.lblProductsValue.Font = new System.Drawing.Font("Calibri", 26F, System.Drawing.FontStyle.Bold);
            this.lblProductsValue.ForeColor = System.Drawing.Color.White;
            this.lblProductsValue.Location = new System.Drawing.Point(16, 16);
            this.lblProductsValue.Name = "lblProductsValue";
            this.lblProductsValue.Size = new System.Drawing.Size(40, 47);
            this.lblProductsValue.TabIndex = 0;
            this.lblProductsValue.Text = "0";
            //
            // lblProductsCaption
            //
            this.lblProductsCaption.AutoSize = true;
            this.lblProductsCaption.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.lblProductsCaption.ForeColor = System.Drawing.Color.White;
            this.lblProductsCaption.Location = new System.Drawing.Point(16, 72);
            this.lblProductsCaption.Name = "lblProductsCaption";
            this.lblProductsCaption.Size = new System.Drawing.Size(130, 19);
            this.lblProductsCaption.TabIndex = 1;
            this.lblProductsCaption.Text = "Active Products";
            //
            // cardStock
            //
            this.cardStock.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.cardStock.Controls.Add(this.lblStockCaption);
            this.cardStock.Controls.Add(this.lblStockValue);
            this.cardStock.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardStock.Name = "cardStock";
            this.cardStock.Size = new System.Drawing.Size(220, 120);
            this.cardStock.TabIndex = 1;
            //
            // lblStockValue
            //
            this.lblStockValue.AutoSize = true;
            this.lblStockValue.Font = new System.Drawing.Font("Calibri", 26F, System.Drawing.FontStyle.Bold);
            this.lblStockValue.ForeColor = System.Drawing.Color.White;
            this.lblStockValue.Location = new System.Drawing.Point(16, 16);
            this.lblStockValue.Name = "lblStockValue";
            this.lblStockValue.Size = new System.Drawing.Size(40, 47);
            this.lblStockValue.TabIndex = 0;
            this.lblStockValue.Text = "0";
            //
            // lblStockCaption
            //
            this.lblStockCaption.AutoSize = true;
            this.lblStockCaption.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.lblStockCaption.ForeColor = System.Drawing.Color.White;
            this.lblStockCaption.Location = new System.Drawing.Point(16, 72);
            this.lblStockCaption.Name = "lblStockCaption";
            this.lblStockCaption.Size = new System.Drawing.Size(140, 19);
            this.lblStockCaption.TabIndex = 1;
            this.lblStockCaption.Text = "Units in Stock";
            //
            // cardSales
            //
            this.cardSales.BackColor = System.Drawing.Color.FromArgb(211, 84, 0);
            this.cardSales.Controls.Add(this.lblSalesCaption);
            this.cardSales.Controls.Add(this.lblSalesValue);
            this.cardSales.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardSales.Name = "cardSales";
            this.cardSales.Size = new System.Drawing.Size(220, 120);
            this.cardSales.TabIndex = 2;
            //
            // lblSalesValue
            //
            this.lblSalesValue.AutoSize = true;
            this.lblSalesValue.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Bold);
            this.lblSalesValue.ForeColor = System.Drawing.Color.White;
            this.lblSalesValue.Location = new System.Drawing.Point(16, 20);
            this.lblSalesValue.Name = "lblSalesValue";
            this.lblSalesValue.Size = new System.Drawing.Size(70, 42);
            this.lblSalesValue.TabIndex = 0;
            this.lblSalesValue.Text = "0.00";
            //
            // lblSalesCaption
            //
            this.lblSalesCaption.AutoSize = true;
            this.lblSalesCaption.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.lblSalesCaption.ForeColor = System.Drawing.Color.White;
            this.lblSalesCaption.Location = new System.Drawing.Point(16, 72);
            this.lblSalesCaption.Name = "lblSalesCaption";
            this.lblSalesCaption.Size = new System.Drawing.Size(120, 19);
            this.lblSalesCaption.TabIndex = 1;
            this.lblSalesCaption.Text = "Today\'s Sales";
            //
            // cardLowStock
            //
            this.cardLowStock.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.cardLowStock.Controls.Add(this.lblLowStockCaption);
            this.cardLowStock.Controls.Add(this.lblLowStockValue);
            this.cardLowStock.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardLowStock.Name = "cardLowStock";
            this.cardLowStock.Size = new System.Drawing.Size(220, 120);
            this.cardLowStock.TabIndex = 3;
            //
            // lblLowStockValue
            //
            this.lblLowStockValue.AutoSize = true;
            this.lblLowStockValue.Font = new System.Drawing.Font("Calibri", 26F, System.Drawing.FontStyle.Bold);
            this.lblLowStockValue.ForeColor = System.Drawing.Color.White;
            this.lblLowStockValue.Location = new System.Drawing.Point(16, 16);
            this.lblLowStockValue.Name = "lblLowStockValue";
            this.lblLowStockValue.Size = new System.Drawing.Size(40, 47);
            this.lblLowStockValue.TabIndex = 0;
            this.lblLowStockValue.Text = "0";
            //
            // lblLowStockCaption
            //
            this.lblLowStockCaption.AutoSize = true;
            this.lblLowStockCaption.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.lblLowStockCaption.ForeColor = System.Drawing.Color.White;
            this.lblLowStockCaption.Location = new System.Drawing.Point(16, 72);
            this.lblLowStockCaption.Name = "lblLowStockCaption";
            this.lblLowStockCaption.Size = new System.Drawing.Size(120, 19);
            this.lblLowStockCaption.TabIndex = 1;
            this.lblLowStockCaption.Text = "Low Stock Alerts";
            //
            // lblLowStockList
            //
            this.lblLowStockList.AutoSize = true;
            this.lblLowStockList.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.lblLowStockList.Location = new System.Drawing.Point(22, 226);
            this.lblLowStockList.Name = "lblLowStockList";
            this.lblLowStockList.Size = new System.Drawing.Size(230, 19);
            this.lblLowStockList.TabIndex = 3;
            this.lblLowStockList.Text = "Products needing restock";
            //
            // dgvLowStock
            //
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AllowUserToDeleteRows = false;
            this.dgvLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLowStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvLowStock.Location = new System.Drawing.Point(20, 252);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.RowHeadersWidth = 24;
            this.dgvLowStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLowStock.Size = new System.Drawing.Size(960, 200);
            this.dgvLowStock.TabIndex = 4;
            //
            // frmHome
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 470);
            this.Controls.Add(this.dgvLowStock);
            this.Controls.Add(this.lblLowStockList);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblWelcome);
            this.Name = "frmHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.pnlCards.ResumeLayout(false);
            this.cardProducts.ResumeLayout(false);
            this.cardProducts.PerformLayout();
            this.cardStock.ResumeLayout(false);
            this.cardStock.PerformLayout();
            this.cardSales.ResumeLayout(false);
            this.cardSales.PerformLayout();
            this.cardLowStock.ResumeLayout(false);
            this.cardLowStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlCards;
        private System.Windows.Forms.Panel cardProducts;
        private System.Windows.Forms.Label lblProductsValue;
        private System.Windows.Forms.Label lblProductsCaption;
        private System.Windows.Forms.Panel cardStock;
        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.Label lblStockCaption;
        private System.Windows.Forms.Panel cardSales;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.Label lblSalesCaption;
        private System.Windows.Forms.Panel cardLowStock;
        private System.Windows.Forms.Label lblLowStockValue;
        private System.Windows.Forms.Label lblLowStockCaption;
        private System.Windows.Forms.Label lblLowStockList;
        private System.Windows.Forms.DataGridView dgvLowStock;
    }
}
