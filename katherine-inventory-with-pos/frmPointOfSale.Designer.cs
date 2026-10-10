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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.colPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCartButtons = new System.Windows.Forms.Panel();
            this.btnQtyMinus = new System.Windows.Forms.Button();
            this.btnQtyPlus = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.pnlScan = new System.Windows.Forms.Panel();
            this.btnRegisterProduct = new System.Windows.Forms.Button();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblScan = new System.Windows.Forms.Label();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.tlpSummary = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubtotalCaption = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.cboDiscountType = new System.Windows.Forms.ComboBox();
            this.numDiscountValue = new System.Windows.Forms.NumericUpDown();
            this.lblVatCaption = new System.Windows.Forms.Label();
            this.lblVatValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.btnExact = new System.Windows.Forms.Button();
            this.numAmountPaid = new System.Windows.Forms.NumericUpDown();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblChangeValue = new System.Windows.Forms.Label();
            this.chkPrintDirect = new System.Windows.Forms.CheckBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.tlpRoot.SuspendLayout();
            this.pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartButtons.SuspendLayout();
            this.pnlScan.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.tlpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountPaid)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 3;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tlpRoot.Controls.Add(this.pnlProducts, 0, 0);
            this.tlpRoot.Controls.Add(this.pnlCart, 1, 0);
            this.tlpRoot.Controls.Add(this.pnlSummary, 2, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.Padding = new System.Windows.Forms.Padding(6);
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(1200, 520);
            this.tlpRoot.TabIndex = 0;
            // 
            // pnlProducts
            // 
            this.pnlProducts.BackColor = System.Drawing.Color.White;
            this.pnlProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProducts.Controls.Add(this.dgvProducts);
            this.pnlProducts.Controls.Add(this.btnAddSelected);
            this.pnlProducts.Controls.Add(this.cboCategory);
            this.pnlProducts.Controls.Add(this.txtSearch);
            this.pnlProducts.Controls.Add(this.lblProductsTitle);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(9, 9);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Size = new System.Drawing.Size(316, 502);
            this.pnlProducts.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPName,
            this.colPPrice,
            this.colPStock});
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(0, 96);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.RowTemplate.Height = 28;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(314, 364);
            this.dgvProducts.TabIndex = 2;
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);
            this.dgvProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProducts_CellFormatting);
            this.dgvProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProducts_KeyDown);
            // 
            // colPName
            // 
            this.colPName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPName.HeaderText = "Product";
            this.colPName.MinimumWidth = 6;
            this.colPName.Name = "colPName";
            this.colPName.ReadOnly = true;
            // 
            // colPPrice
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.colPPrice.HeaderText = "Price";
            this.colPPrice.MinimumWidth = 6;
            this.colPPrice.Name = "colPPrice";
            this.colPPrice.ReadOnly = true;
            this.colPPrice.Width = 80;
            // 
            // colPStock
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPStock.DefaultCellStyle = dataGridViewCellStyle7;
            this.colPStock.HeaderText = "Stock";
            this.colPStock.MinimumWidth = 6;
            this.colPStock.Name = "colPStock";
            this.colPStock.ReadOnly = true;
            this.colPStock.Width = 60;
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAddSelected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddSelected.FlatAppearance.BorderSize = 0;
            this.btnAddSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSelected.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSelected.ForeColor = System.Drawing.Color.White;
            this.btnAddSelected.Location = new System.Drawing.Point(0, 460);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(314, 40);
            this.btnAddSelected.TabIndex = 3;
            this.btnAddSelected.Text = "ADD TO CART  (Enter)";
            this.btnAddSelected.UseVisualStyleBackColor = false;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // cboCategory
            // 
            this.cboCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(0, 65);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(314, 31);
            this.cboCategory.TabIndex = 1;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSearch.Location = new System.Drawing.Point(0, 34);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(314, 31);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblProductsTitle
            // 
            this.lblProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblProductsTitle.Name = "lblProductsTitle";
            this.lblProductsTitle.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblProductsTitle.Size = new System.Drawing.Size(314, 34);
            this.lblProductsTitle.TabIndex = 4;
            this.lblProductsTitle.Text = "Product List";
            this.lblProductsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCart
            // 
            this.pnlCart.BackColor = System.Drawing.Color.White;
            this.pnlCart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCart.Controls.Add(this.dgvCart);
            this.pnlCart.Controls.Add(this.pnlCartButtons);
            this.pnlCart.Controls.Add(this.pnlScan);
            this.pnlCart.Controls.Add(this.lblCartTitle);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCart.Location = new System.Drawing.Point(331, 9);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Size = new System.Drawing.Size(519, 502);
            this.pnlCart.TabIndex = 1;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colProductName,
            this.colUnitPrice,
            this.colQuantity,
            this.colSubtotal});
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(0, 100);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 28;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(517, 354);
            this.dgvCart.TabIndex = 2;
            this.dgvCart.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellEndEdit);
            this.dgvCart.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCart_DataError);
            // 
            // colProductCode
            // 
            this.colProductCode.HeaderText = "Code";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.HeaderText = "Product";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colUnitPrice
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUnitPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.colUnitPrice.HeaderText = "Unit Price";
            this.colUnitPrice.MinimumWidth = 6;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.Width = 90;
            // 
            // colQuantity
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colQuantity.DefaultCellStyle = dataGridViewCellStyle9;
            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 55;
            // 
            // colSubtotal
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle10;
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.MinimumWidth = 6;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            // 
            // pnlCartButtons
            // 
            this.pnlCartButtons.Controls.Add(this.btnQtyMinus);
            this.pnlCartButtons.Controls.Add(this.btnQtyPlus);
            this.pnlCartButtons.Controls.Add(this.btnRemoveItem);
            this.pnlCartButtons.Controls.Add(this.btnClearCart);
            this.pnlCartButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartButtons.Location = new System.Drawing.Point(0, 454);
            this.pnlCartButtons.Name = "pnlCartButtons";
            this.pnlCartButtons.Size = new System.Drawing.Size(517, 46);
            this.pnlCartButtons.TabIndex = 3;
            // 
            // btnQtyMinus
            // 
            this.btnQtyMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQtyMinus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQtyMinus.Location = new System.Drawing.Point(8, 7);
            this.btnQtyMinus.Name = "btnQtyMinus";
            this.btnQtyMinus.Size = new System.Drawing.Size(40, 32);
            this.btnQtyMinus.TabIndex = 0;
            this.btnQtyMinus.Text = "-";
            this.btnQtyMinus.UseVisualStyleBackColor = true;
            this.btnQtyMinus.Click += new System.EventHandler(this.btnQtyMinus_Click);
            // 
            // btnQtyPlus
            // 
            this.btnQtyPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQtyPlus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQtyPlus.Location = new System.Drawing.Point(54, 7);
            this.btnQtyPlus.Name = "btnQtyPlus";
            this.btnQtyPlus.Size = new System.Drawing.Size(40, 32);
            this.btnQtyPlus.TabIndex = 1;
            this.btnQtyPlus.Text = "+";
            this.btnQtyPlus.UseVisualStyleBackColor = true;
            this.btnQtyPlus.Click += new System.EventHandler(this.btnQtyPlus_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Location = new System.Drawing.Point(110, 7);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(130, 32);
            this.btnRemoveItem.TabIndex = 2;
            this.btnRemoveItem.Text = "Remove (Del)";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Location = new System.Drawing.Point(248, 7);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(110, 32);
            this.btnClearCart.TabIndex = 3;
            this.btnClearCart.Text = "Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = true;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // pnlScan
            // 
            this.pnlScan.Controls.Add(this.btnRegisterProduct);
            this.pnlScan.Controls.Add(this.txtBarcode);
            this.pnlScan.Controls.Add(this.lblScan);
            this.pnlScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScan.Location = new System.Drawing.Point(0, 34);
            this.pnlScan.Name = "pnlScan";
            this.pnlScan.Size = new System.Drawing.Size(517, 66);
            this.pnlScan.TabIndex = 4;
            // 
            // btnRegisterProduct
            // 
            this.btnRegisterProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegisterProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterProduct.Location = new System.Drawing.Point(733, 26);
            this.btnRegisterProduct.Name = "btnRegisterProduct";
            this.btnRegisterProduct.Size = new System.Drawing.Size(150, 30);
            this.btnRegisterProduct.TabIndex = 1;
            this.btnRegisterProduct.Text = "Register New Product";
            this.btnRegisterProduct.UseVisualStyleBackColor = true;
            this.btnRegisterProduct.Click += new System.EventHandler(this.btnRegisterProduct_Click);
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtBarcode.Location = new System.Drawing.Point(8, 26);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(717, 35);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // lblScan
            // 
            this.lblScan.AutoSize = true;
            this.lblScan.Location = new System.Drawing.Point(8, 4);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(224, 16);
            this.lblScan.TabIndex = 2;
            this.lblScan.Text = "Scan barcode or type product code  ";
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.Location = new System.Drawing.Point(0, 0);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblCartTitle.Size = new System.Drawing.Size(517, 34);
            this.lblCartTitle.TabIndex = 5;
            this.lblCartTitle.Text = "Order Cart";
            this.lblCartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSummary
            // 
            this.pnlSummary.AutoScroll = true;
            this.pnlSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.tlpSummary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummary.Location = new System.Drawing.Point(856, 9);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSummary.Size = new System.Drawing.Size(335, 502);
            this.pnlSummary.TabIndex = 2;
            // 
            // tlpSummary
            // 
            this.tlpSummary.AutoSize = true;
            this.tlpSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpSummary.ColumnCount = 2;
            this.tlpSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSummary.Controls.Add(this.lblSubtotalCaption, 0, 0);
            this.tlpSummary.Controls.Add(this.lblSubtotalValue, 1, 0);
            this.tlpSummary.Controls.Add(this.lblDiscountCaption, 0, 1);
            this.tlpSummary.Controls.Add(this.lblDiscountValue, 1, 1);
            this.tlpSummary.Controls.Add(this.cboDiscountType, 0, 2);
            this.tlpSummary.Controls.Add(this.numDiscountValue, 0, 3);
            this.tlpSummary.Controls.Add(this.lblVatCaption, 0, 4);
            this.tlpSummary.Controls.Add(this.lblVatValue, 1, 4);
            this.tlpSummary.Controls.Add(this.lblTotalCaption, 0, 5);
            this.tlpSummary.Controls.Add(this.lblTotalValue, 0, 6);
            this.tlpSummary.Controls.Add(this.lblPaymentMethod, 0, 7);
            this.tlpSummary.Controls.Add(this.cboPaymentMethod, 0, 8);
            this.tlpSummary.Controls.Add(this.lblAmountPaid, 0, 9);
            this.tlpSummary.Controls.Add(this.btnExact, 1, 9);
            this.tlpSummary.Controls.Add(this.numAmountPaid, 0, 10);
            this.tlpSummary.Controls.Add(this.lblChange, 0, 11);
            this.tlpSummary.Controls.Add(this.lblChangeValue, 1, 11);
            this.tlpSummary.Controls.Add(this.chkPrintDirect, 0, 12);
            this.tlpSummary.Controls.Add(this.btnCheckout, 0, 13);
            this.tlpSummary.Controls.Add(this.btnReprint, 0, 14);
            this.tlpSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpSummary.Location = new System.Drawing.Point(10, 10);
            this.tlpSummary.Name = "tlpSummary";
            this.tlpSummary.RowCount = 15;
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSummary.Size = new System.Drawing.Size(292, 520);
            this.tlpSummary.TabIndex = 0;
            // 
            // lblSubtotalCaption
            // 
            this.lblSubtotalCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotalCaption.Location = new System.Drawing.Point(3, 0);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new System.Drawing.Size(74, 23);
            this.lblSubtotalCaption.TabIndex = 0;
            this.lblSubtotalCaption.Text = "Subtotal";
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubtotalValue.AutoSize = true;
            this.lblSubtotalValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotalValue.Location = new System.Drawing.Point(248, 0);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(41, 23);
            this.lblSubtotalValue.TabIndex = 1;
            this.lblSubtotalValue.Text = "0.00";
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountCaption.Location = new System.Drawing.Point(3, 23);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(77, 23);
            this.lblDiscountCaption.TabIndex = 2;
            this.lblDiscountCaption.Text = "Discount";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountValue.Location = new System.Drawing.Point(248, 23);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(41, 23);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "0.00";
            // 
            // cboDiscountType
            // 
            this.tlpSummary.SetColumnSpan(this.cboDiscountType, 2);
            this.cboDiscountType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDiscountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiscountType.FormattingEnabled = true;
            this.cboDiscountType.Items.AddRange(new object[] {
            "No discount",
            "Senior Citizen (20%)",
            "PWD (20%)",
            "Custom percent (%)",
            "Fixed amount"});
            this.cboDiscountType.Location = new System.Drawing.Point(3, 49);
            this.cboDiscountType.Name = "cboDiscountType";
            this.cboDiscountType.Size = new System.Drawing.Size(286, 24);
            this.cboDiscountType.TabIndex = 0;
            this.cboDiscountType.SelectedIndexChanged += new System.EventHandler(this.cboDiscountType_SelectedIndexChanged);
            // 
            // numDiscountValue
            // 
            this.tlpSummary.SetColumnSpan(this.numDiscountValue, 2);
            this.numDiscountValue.DecimalPlaces = 2;
            this.numDiscountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDiscountValue.Enabled = false;
            this.numDiscountValue.Location = new System.Drawing.Point(3, 79);
            this.numDiscountValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numDiscountValue.Name = "numDiscountValue";
            this.numDiscountValue.Size = new System.Drawing.Size(286, 22);
            this.numDiscountValue.TabIndex = 1;
            this.numDiscountValue.ValueChanged += new System.EventHandler(this.numDiscountValue_ValueChanged);
            // 
            // lblVatCaption
            // 
            this.lblVatCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVatCaption.AutoSize = true;
            this.lblVatCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVatCaption.Location = new System.Drawing.Point(3, 104);
            this.lblVatCaption.Name = "lblVatCaption";
            this.lblVatCaption.Size = new System.Drawing.Size(86, 23);
            this.lblVatCaption.TabIndex = 4;
            this.lblVatCaption.Text = "VAT (12%)";
            // 
            // lblVatValue
            // 
            this.lblVatValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVatValue.AutoSize = true;
            this.lblVatValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVatValue.Location = new System.Drawing.Point(248, 104);
            this.lblVatValue.Name = "lblVatValue";
            this.lblVatValue.Size = new System.Drawing.Size(41, 23);
            this.lblVatValue.TabIndex = 5;
            this.lblVatValue.Text = "0.00";
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalCaption.AutoSize = true;
            this.tlpSummary.SetColumnSpan(this.lblTotalCaption, 2);
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(3, 139);
            this.lblTotalCaption.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(68, 25);
            this.lblTotalCaption.TabIndex = 6;
            this.lblTotalCaption.Text = "TOTAL";
            // 
            // lblTotalValue
            // 
            this.tlpSummary.SetColumnSpan(this.lblTotalValue, 2);
            this.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTotalValue.Location = new System.Drawing.Point(3, 164);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(286, 56);
            this.lblTotalValue.TabIndex = 7;
            this.lblTotalValue.Text = "0.00";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPaymentMethod.AutoSize = true;
            this.tlpSummary.SetColumnSpan(this.lblPaymentMethod, 2);
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPaymentMethod.Location = new System.Drawing.Point(3, 220);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(141, 23);
            this.lblPaymentMethod.TabIndex = 8;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // cboPaymentMethod
            // 
            this.tlpSummary.SetColumnSpan(this.cboPaymentMethod, 2);
            this.cboPaymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "GCash",
            "Card"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(3, 246);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(286, 24);
            this.cboPaymentMethod.TabIndex = 2;
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAmountPaid.AutoSize = true;
            this.lblAmountPaid.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAmountPaid.Location = new System.Drawing.Point(3, 277);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(109, 23);
            this.lblAmountPaid.TabIndex = 9;
            this.lblAmountPaid.Text = "Amount Paid";
            // 
            // btnExact
            // 
            this.btnExact.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExact.Location = new System.Drawing.Point(209, 276);
            this.btnExact.Name = "btnExact";
            this.btnExact.Size = new System.Drawing.Size(80, 26);
            this.btnExact.TabIndex = 3;
            this.btnExact.Text = "Exact";
            this.btnExact.UseVisualStyleBackColor = true;
            this.btnExact.Click += new System.EventHandler(this.btnExact_Click);
            // 
            // numAmountPaid
            // 
            this.tlpSummary.SetColumnSpan(this.numAmountPaid, 2);
            this.numAmountPaid.DecimalPlaces = 2;
            this.numAmountPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numAmountPaid.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.numAmountPaid.Location = new System.Drawing.Point(3, 308);
            this.numAmountPaid.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numAmountPaid.Name = "numAmountPaid";
            this.numAmountPaid.Size = new System.Drawing.Size(286, 39);
            this.numAmountPaid.TabIndex = 4;
            this.numAmountPaid.ThousandsSeparator = true;
            this.numAmountPaid.ValueChanged += new System.EventHandler(this.numAmountPaid_ValueChanged);
            // 
            // lblChange
            // 
            this.lblChange.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChange.Location = new System.Drawing.Point(3, 357);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(69, 23);
            this.lblChange.TabIndex = 10;
            this.lblChange.Text = "Change";
            // 
            // lblChangeValue
            // 
            this.lblChangeValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblChangeValue.AutoSize = true;
            this.lblChangeValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblChangeValue.Location = new System.Drawing.Point(217, 350);
            this.lblChangeValue.Name = "lblChangeValue";
            this.lblChangeValue.Size = new System.Drawing.Size(72, 37);
            this.lblChangeValue.TabIndex = 11;
            this.lblChangeValue.Text = "0.00";
            // 
            // chkPrintDirect
            // 
            this.chkPrintDirect.AutoSize = true;
            this.tlpSummary.SetColumnSpan(this.chkPrintDirect, 2);
            this.chkPrintDirect.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPrintDirect.Location = new System.Drawing.Point(3, 390);
            this.chkPrintDirect.Name = "chkPrintDirect";
            this.chkPrintDirect.Size = new System.Drawing.Size(210, 24);
            this.chkPrintDirect.TabIndex = 5;
            this.chkPrintDirect.Text = "Print directly (skip preview)";
            this.chkPrintDirect.UseVisualStyleBackColor = true;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.tlpSummary.SetColumnSpan(this.btnCheckout, 2);
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(3, 425);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(286, 54);
            this.btnCheckout.TabIndex = 6;
            this.btnCheckout.Text = "CHECKOUT && PRINT  (F9)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnReprint
            // 
            this.tlpSummary.SetColumnSpan(this.btnReprint, 2);
            this.btnReprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReprint.Enabled = false;
            this.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprint.Location = new System.Drawing.Point(3, 485);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(286, 32);
            this.btnReprint.TabIndex = 7;
            this.btnReprint.Text = "Reprint Last Receipt";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // frmPointOfSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 520);
            this.ControlBox = false;
            this.Controls.Add(this.tlpRoot);
            this.Name = "frmPointOfSale";
            this.Load += new System.EventHandler(this.frmPointOfSale_Load);
            this.tlpRoot.ResumeLayout(false);
            this.pnlProducts.ResumeLayout(false);
            this.pnlProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.pnlCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartButtons.ResumeLayout(false);
            this.pnlScan.ResumeLayout(false);
            this.pnlScan.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.tlpSummary.ResumeLayout(false);
            this.tlpSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountPaid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPStock;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Panel pnlCart;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.Panel pnlScan;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnRegisterProduct;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Panel pnlCartButtons;
        private System.Windows.Forms.Button btnQtyMinus;
        private System.Windows.Forms.Button btnQtyPlus;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TableLayoutPanel tlpSummary;
        private System.Windows.Forms.Label lblSubtotalCaption;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblDiscountCaption;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.ComboBox cboDiscountType;
        private System.Windows.Forms.NumericUpDown numDiscountValue;
        private System.Windows.Forms.Label lblVatCaption;
        private System.Windows.Forms.Label lblVatValue;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblAmountPaid;
        private System.Windows.Forms.Button btnExact;
        private System.Windows.Forms.NumericUpDown numAmountPaid;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblChangeValue;
        private System.Windows.Forms.CheckBox chkPrintDirect;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnReprint;
    }
}