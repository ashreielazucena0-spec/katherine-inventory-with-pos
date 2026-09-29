namespace katherine_inventory_with_pos.Forms
{
    partial class frmPointOfSale
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblScan = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnRegisterProduct = new System.Windows.Forms.Button();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.numAmountPaid = new System.Windows.Forms.NumericUpDown();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblChangeValue = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountPaid)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Point of Sale";
            //
            // lblScan
            //
            this.lblScan.AutoSize = true;
            this.lblScan.Location = new System.Drawing.Point(20, 54);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(230, 17);
            this.lblScan.TabIndex = 1;
            this.lblScan.Text = "Scan barcode or type product code";
            //
            // txtBarcode
            //
            this.txtBarcode.Font = new System.Drawing.Font("Consolas", 13F);
            this.txtBarcode.Location = new System.Drawing.Point(20, 74);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(430, 30);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            //
            // btnRegisterProduct
            //
            this.btnRegisterProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterProduct.Location = new System.Drawing.Point(460, 74);
            this.btnRegisterProduct.Name = "btnRegisterProduct";
            this.btnRegisterProduct.Size = new System.Drawing.Size(180, 30);
            this.btnRegisterProduct.TabIndex = 3;
            this.btnRegisterProduct.Text = "Register New Product";
            this.btnRegisterProduct.UseVisualStyleBackColor = true;
            this.btnRegisterProduct.Click += new System.EventHandler(this.btnRegisterProduct_Click);
            //
            // dgvCart
            //
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colProductName,
            this.colUnitPrice,
            this.colQuantity,
            this.colSubtotal});
            this.dgvCart.Location = new System.Drawing.Point(20, 116);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersWidth = 24;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(620, 300);
            this.dgvCart.TabIndex = 4;
            this.dgvCart.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellEndEdit);
            //
            // colProductCode
            //
            this.colProductCode.HeaderText = "Code";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.Width = 90;
            //
            // colProductName
            //
            this.colProductName.HeaderText = "Product";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.Width = 190;
            //
            // colUnitPrice
            //
            this.colUnitPrice.HeaderText = "Unit Price";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.Width = 90;
            //
            // colQuantity
            //
            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 60;
            //
            // colSubtotal
            //
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            this.colSubtotal.Width = 100;
            //
            // btnRemoveItem
            //
            this.btnRemoveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Location = new System.Drawing.Point(20, 422);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(150, 32);
            this.btnRemoveItem.TabIndex = 5;
            this.btnRemoveItem.Text = "Remove Selected";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            //
            // btnClearCart
            //
            this.btnClearCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Location = new System.Drawing.Point(180, 422);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(150, 32);
            this.btnClearCart.TabIndex = 6;
            this.btnClearCart.Text = "Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = true;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            //
            // panelSummary
            //
            this.panelSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummary.Controls.Add(this.lblTotalCaption);
            this.panelSummary.Controls.Add(this.lblTotalValue);
            this.panelSummary.Controls.Add(this.lblPaymentMethod);
            this.panelSummary.Controls.Add(this.cboPaymentMethod);
            this.panelSummary.Controls.Add(this.lblAmountPaid);
            this.panelSummary.Controls.Add(this.numAmountPaid);
            this.panelSummary.Controls.Add(this.lblChange);
            this.panelSummary.Controls.Add(this.lblChangeValue);
            this.panelSummary.Controls.Add(this.btnCheckout);
            this.panelSummary.Location = new System.Drawing.Point(660, 74);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(320, 380);
            this.panelSummary.TabIndex = 7;
            //
            // lblTotalCaption
            //
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblTotalCaption.Location = new System.Drawing.Point(20, 20);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(90, 22);
            this.lblTotalCaption.TabIndex = 0;
            this.lblTotalCaption.Text = "TOTAL";
            //
            // lblTotalValue
            //
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Calibri", 26F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblTotalValue.Location = new System.Drawing.Point(20, 44);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(90, 47);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "0.00";
            //
            // lblPaymentMethod
            //
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(20, 116);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(122, 17);
            this.lblPaymentMethod.TabIndex = 2;
            this.lblPaymentMethod.Text = "Payment Method";
            //
            // cboPaymentMethod
            //
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "GCash",
            "Card"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(20, 136);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(270, 25);
            this.cboPaymentMethod.TabIndex = 3;
            //
            // lblAmountPaid
            //
            this.lblAmountPaid.AutoSize = true;
            this.lblAmountPaid.Location = new System.Drawing.Point(20, 176);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(96, 17);
            this.lblAmountPaid.TabIndex = 4;
            this.lblAmountPaid.Text = "Amount Paid";
            //
            // numAmountPaid
            //
            this.numAmountPaid.DecimalPlaces = 2;
            this.numAmountPaid.Font = new System.Drawing.Font("Calibri", 14F);
            this.numAmountPaid.Location = new System.Drawing.Point(20, 196);
            this.numAmountPaid.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numAmountPaid.Name = "numAmountPaid";
            this.numAmountPaid.Size = new System.Drawing.Size(270, 30);
            this.numAmountPaid.TabIndex = 5;
            this.numAmountPaid.ValueChanged += new System.EventHandler(this.numAmountPaid_ValueChanged);
            //
            // lblChange
            //
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(20, 236);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(59, 17);
            this.lblChange.TabIndex = 6;
            this.lblChange.Text = "Change";
            //
            // lblChangeValue
            //
            this.lblChangeValue.AutoSize = true;
            this.lblChangeValue.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lblChangeValue.Location = new System.Drawing.Point(20, 256);
            this.lblChangeValue.Name = "lblChangeValue";
            this.lblChangeValue.Size = new System.Drawing.Size(60, 29);
            this.lblChangeValue.TabIndex = 7;
            this.lblChangeValue.Text = "0.00";
            //
            // btnCheckout
            //
            this.btnCheckout.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(20, 306);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(270, 48);
            this.btnCheckout.TabIndex = 8;
            this.btnCheckout.Text = "CHECKOUT && PRINT RECEIPT";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            //
            // frmPointOfSale
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 470);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.btnClearCart);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnRegisterProduct);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmPointOfSale";
            this.Text = "Point of Sale";
            this.Load += new System.EventHandler(this.frmPointOfSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountPaid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnRegisterProduct;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblAmountPaid;
        private System.Windows.Forms.NumericUpDown numAmountPaid;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblChangeValue;
        private System.Windows.Forms.Button btnCheckout;
    }
}
