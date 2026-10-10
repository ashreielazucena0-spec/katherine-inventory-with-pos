using katherine_inventory_with_pos.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Windows.Forms;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmPointOfSale : Form
    {
        // ------------------------------------------------------------------
        // Tax settings
        // ------------------------------------------------------------------
        private const decimal VatRate = 0.12m;                          // 12% VAT
        // false = shelf prices do NOT include VAT, so 12% is ADDED at checkout.
        // true  = shelf prices already include VAT, so VAT is only shown, not added.
        private static readonly bool PricesIncludeVat = false;

        private const decimal SeniorPwdRate = 0.20m;                    // 20% SC / PWD discount

        // Indexes of cboDiscountType items (same order as the designer)
        private const int DiscNone = 0, DiscSenior = 1, DiscPwd = 2, DiscPercent = 3, DiscFixed = 4;

        private readonly BindingList<CartItem> cart = new BindingList<CartItem>();
        private DataTable productTable;
        private DataView productView;
        private bool loadingProducts;
        private List<string> lastReceipt;

        private class OrderTotals
        {
            public decimal Subtotal;
            public decimal Discount;
            public decimal Vat;
            public decimal Total;
        }

        public frmPointOfSale()
        {
            InitializeComponent();
        }

        private void frmPointOfSale_Load(object sender, EventArgs e)
        {
            // cart grid
            dgvCart.AutoGenerateColumns = false;
            colProductCode.DataPropertyName = "ProductCode";
            colProductName.DataPropertyName = "ProductName";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colQuantity.DataPropertyName = "Quantity";
            colSubtotal.DataPropertyName = "Subtotal";
            colUnitPrice.DefaultCellStyle.Format = "N2";
            colSubtotal.DefaultCellStyle.Format = "N2";
            dgvCart.DataSource = cart;

            // product grid
            dgvProducts.AutoGenerateColumns = false;
            colPName.DataPropertyName = "ProductName";
            colPPrice.DataPropertyName = "Price";
            colPStock.DataPropertyName = "Stock";
            colPPrice.DefaultCellStyle.Format = "N2";

            cboPaymentMethod.SelectedIndex = 0;
            cboDiscountType.SelectedIndex = DiscNone;
            lblVatCaption.Text = PricesIncludeVat
                ? "VAT (" + (VatRate * 100m).ToString("0") + "% incl.)"
                : "VAT (" + (VatRate * 100m).ToString("0") + "%)";

            LoadProducts();
            RecalculateTotals();
            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Keyboard shortcuts: F2 barcode, F3 search, F9 checkout, Del remove line
        // ------------------------------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    txtBarcode.Focus();
                    txtBarcode.SelectAll();
                    return true;
                case Keys.F3:
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    return true;
                case Keys.F9:
                    btnCheckout.PerformClick();
                    return true;
                case Keys.Delete:
                    if (dgvCart.Focused && !dgvCart.IsCurrentCellInEditMode)
                    {
                        btnRemoveItem.PerformClick();
                        return true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ------------------------------------------------------------------
        // Product list (left panel)
        // ------------------------------------------------------------------
        private void LoadProducts()
        {
            try
            {
                loadingProducts = true;

                productTable = DBConnection.ExecuteQuery(
                    "SELECT p.product_id AS ProductID, IFNULL(p.barcode, '') AS Barcode, " +
                    "p.product_name AS ProductName, IFNULL(c.category_name, 'Uncategorized') AS Category, " +
                    "p.selling_price AS Price, CAST(IFNULL(s.quantity, 0) AS SIGNED) AS Stock " +
                    "FROM tbl_product p " +
                    "LEFT JOIN tbl_category c ON c.category_id = p.category_id " +
                    "LEFT JOIN tbl_stock s ON s.product_id = p.product_id " +
                    "WHERE p.product_status = 'Active' " +
                    "ORDER BY p.product_name;");

                // keep the selected category if it still exists
                string previousCategory = cboCategory.SelectedIndex > 0 ? cboCategory.Text : null;

                cboCategory.Items.Clear();
                cboCategory.Items.Add("All Categories");
                foreach (string cat in productTable.AsEnumerable()
                    .Select(r => r.Field<string>("Category")).Distinct().OrderBy(c => c))
                {
                    cboCategory.Items.Add(cat);
                }

                int idx = previousCategory == null ? 0 : cboCategory.Items.IndexOf(previousCategory);
                cboCategory.SelectedIndex = idx < 0 ? 0 : idx;

                productView = new DataView(productTable);
                dgvProducts.DataSource = productView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the product list:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingProducts = false;
            }

            ApplyProductFilter();
        }

        private void ApplyProductFilter()
        {
            if (productView == null || loadingProducts) return;

            List<string> filters = new List<string>();

            string text = txtSearch.Text.Trim();
            if (text.Length > 0)
            {
                string escaped = EscapeForRowFilter(text);
                filters.Add("(ProductName LIKE '%" + escaped + "%' OR Barcode LIKE '%" + escaped + "%')");
            }

            if (cboCategory.SelectedIndex > 0)
            {
                filters.Add("Category = '" + cboCategory.Text.Replace("'", "''") + "'");
            }

            productView.RowFilter = string.Join(" AND ", filters);
        }

        /// <summary>Escapes a user-typed string for use inside a DataView RowFilter LIKE '%...%'.</summary>
        private static string EscapeForRowFilter(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                switch (c)
                {
                    case '\'': sb.Append("''"); break;
                    case '[': sb.Append("[[]"); break;
                    case ']': sb.Append("[]]"); break;
                    case '*': sb.Append("[*]"); break;
                    case '%': sb.Append("[%]"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyProductFilter();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // Enter adds the first match, so you can type a name and press Enter.
                if (dgvProducts.Rows.Count > 0)
                {
                    dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells[0];
                    AddSelectedProductToCart();
                }
            }
            else if (e.KeyCode == Keys.Down && dgvProducts.Rows.Count > 0)
            {
                e.SuppressKeyPress = true;
                dgvProducts.Focus();
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyProductFilter();
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AddSelectedProductToCart();
        }

        private void dgvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddSelectedProductToCart();
            }
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            AddSelectedProductToCart();
        }

        // Grey out and mark products that are out of stock; flag low stock in orange.
        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataRowView drv = dgvProducts.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            int stock = Convert.ToInt32(drv["Stock"]);
            if (stock <= 0)
                e.CellStyle.ForeColor = Color.Firebrick;
            else if (stock <= 5 && e.ColumnIndex == colPStock.Index)
                e.CellStyle.ForeColor = Color.DarkOrange;
        }

        private void AddSelectedProductToCart()
        {
            if (dgvProducts.CurrentRow == null) return;
            DataRowView drv = dgvProducts.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            AddToCart(
                drv["ProductID"].ToString(),
                drv["Barcode"].ToString(),
                drv["ProductName"].ToString(),
                Convert.ToDecimal(drv["Price"]),
                Convert.ToInt32(drv["Stock"]));
        }

        // ------------------------------------------------------------------
        // Barcode scanning
        // ------------------------------------------------------------------
        // USB / Bluetooth scanners act like a keyboard: they "type" the barcode and
        // finish with Enter, so handling Enter on a plain TextBox is all that is needed.
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;
            string code = txtBarcode.Text.Trim();
            txtBarcode.Clear();

            if (string.IsNullOrEmpty(code)) return;

            ScanBarcode(code);
        }

        private void ScanBarcode(string code)
        {
            try
            {
                DataTable table = DBConnection.ExecuteQuery(
                    "SELECT p.product_id AS ProductID, p.barcode AS ProductCode, p.product_name AS ProductName, " +
                    "p.selling_price AS Price, IFNULL(s.quantity, 0) AS QuantityInStock " +
                    "FROM tbl_product p " +
                    "LEFT JOIN tbl_stock s ON s.product_id = p.product_id " +
                    "WHERE (p.barcode = @Code OR p.product_id = @Code) AND p.product_status = 'Active';",
                    new MySqlParameter("@Code", code));

                if (table.Rows.Count == 0)
                {
                    DialogResult result;
                    using (frmConfirmDialog confirm = new frmConfirmDialog("Product Not Found",
                        "No product found for code \"" + code + "\".\nDo you want to register it as a new product now?"))
                    {
                        result = confirm.ShowDialog(this);
                    }

                    if (result == DialogResult.Yes)
                    {
                        RegisterAndAddProduct(code);
                    }
                    return;
                }

                DataRow row = table.Rows[0];
                AddToCart(
                    row["ProductID"].ToString(),
                    row["ProductCode"] == DBNull.Value ? string.Empty : row["ProductCode"].ToString(),
                    row["ProductName"].ToString(),
                    Convert.ToDecimal(row["Price"]),
                    Convert.ToInt32(row["QuantityInStock"]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not look up that barcode:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtBarcode.Focus();
            }
        }

        // ------------------------------------------------------------------
        // Register a brand-new product straight from the POS screen
        // ------------------------------------------------------------------
        private void btnRegisterProduct_Click(object sender, EventArgs e)
        {
            RegisterAndAddProduct(txtBarcode.Text.Trim());
        }

        private void RegisterAndAddProduct(string prefillCode)
        {
            using (frmQuickAddProduct form = new frmQuickAddProduct(prefillCode))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtBarcode.Clear();
                    AddToCart(form.NewProductID, form.NewProductCode, form.NewProductName,
                        form.NewProductPrice, form.NewProductQuantity);
                    LoadProducts(); // so the new product shows up in the list
                }
            }
            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Cart maintenance
        // ------------------------------------------------------------------
        private void AddToCart(string productId, string code, string name, decimal price, int availableStock)
        {
            CartItem existing = cart.FirstOrDefault(c => c.ProductID == productId);

            if (existing != null)
            {
                if (existing.Quantity + 1 > existing.AvailableStock)
                {
                    MessageBox.Show("Only " + existing.AvailableStock + " unit(s) of \"" + name + "\" left in stock.",
                        "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existing.Quantity += 1;
                dgvCart.Refresh();
            }
            else
            {
                if (availableStock <= 0)
                {
                    MessageBox.Show("\"" + name + "\" is out of stock.", "Insufficient Stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cart.Add(new CartItem
                {
                    ProductID = productId,
                    ProductCode = code,
                    ProductName = name,
                    UnitPrice = price,
                    Quantity = 1,
                    AvailableStock = availableStock
                });

                // keep the newest line visible and selected
                if (dgvCart.Rows.Count > 0)
                {
                    int last = dgvCart.Rows.Count - 1;
                    dgvCart.CurrentCell = dgvCart.Rows[last].Cells[colProductName.Index];
                }
            }

            RecalculateTotals();
        }

        private CartItem SelectedCartItem()
        {
            if (dgvCart.CurrentRow == null) return null;
            int index = dgvCart.CurrentRow.Index;
            return (index >= 0 && index < cart.Count) ? cart[index] : null;
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colQuantity.Index) return;
            if (e.RowIndex < 0 || e.RowIndex >= cart.Count) return;

            CartItem item = cart[e.RowIndex];

            if (item.Quantity < 1) item.Quantity = 1;
            if (item.Quantity > item.AvailableStock)
            {
                MessageBox.Show("Only " + item.AvailableStock + " unit(s) of \"" + item.ProductName + "\" available.",
                    "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                item.Quantity = item.AvailableStock;
            }

            dgvCart.Refresh();
            RecalculateTotals();
        }

        // Typing letters in the Qty cell would otherwise throw a grid exception dialog.
        private void dgvCart_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Please enter a whole number for the quantity.", "Invalid Quantity",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = false;
        }

        private void btnQtyPlus_Click(object sender, EventArgs e)
        {
            CartItem item = SelectedCartItem();
            if (item == null) return;

            if (item.Quantity + 1 > item.AvailableStock)
            {
                MessageBox.Show("Only " + item.AvailableStock + " unit(s) of \"" + item.ProductName + "\" available.",
                    "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            item.Quantity += 1;
            dgvCart.Refresh();
            RecalculateTotals();
        }

        private void btnQtyMinus_Click(object sender, EventArgs e)
        {
            CartItem item = SelectedCartItem();
            if (item == null) return;

            if (item.Quantity > 1)
            {
                item.Quantity -= 1;
                dgvCart.Refresh();
                RecalculateTotals();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            CartItem item = SelectedCartItem();
            if (item == null) return;

            cart.Remove(item);
            RecalculateTotals();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            using (frmConfirmDialog confirm = new frmConfirmDialog("Clear Cart", "Remove all items from the current order?"))
            {
                if (confirm.ShowDialog(this) != DialogResult.Yes) return;
            }

            ResetOrder();
        }

        /// <summary>Empties the cart and puts discount / payment back to their starting values.</summary>
        private void ResetOrder()
        {
            cart.Clear();
            cboDiscountType.SelectedIndex = DiscNone;
            numDiscountValue.Value = 0;
            numAmountPaid.Value = 0;
            RecalculateTotals();
            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Discount, VAT and totals
        // ------------------------------------------------------------------
        private static decimal Round2(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private OrderTotals ComputeTotals()
        {
            OrderTotals t = new OrderTotals();
            t.Subtotal = Round2(cart.Sum(c => c.Subtotal));

            switch (cboDiscountType.SelectedIndex)
            {
                case DiscSenior:
                case DiscPwd:
                    t.Discount = Round2(t.Subtotal * SeniorPwdRate);
                    break;
                case DiscPercent:
                    t.Discount = Round2(t.Subtotal * (numDiscountValue.Value / 100m));
                    break;
                case DiscFixed:
                    t.Discount = Round2(numDiscountValue.Value);
                    break;
                default:
                    t.Discount = 0m;
                    break;
            }

            if (t.Discount > t.Subtotal) t.Discount = t.Subtotal;

            decimal afterDiscount = t.Subtotal - t.Discount;

            if (PricesIncludeVat)
            {
                // VAT is already inside the price: just show how much of it is VAT.
                t.Vat = Round2(afterDiscount * VatRate / (1m + VatRate));
                t.Total = afterDiscount;
            }
            else
            {
                t.Vat = Round2(afterDiscount * VatRate);
                t.Total = afterDiscount + t.Vat;
            }

            return t;
        }

        private string DiscountLabel()
        {
            switch (cboDiscountType.SelectedIndex)
            {
                case DiscSenior: return "Senior Citizen";
                case DiscPwd: return "PWD";
                case DiscPercent: return "Custom " + numDiscountValue.Value.ToString("0.##") + "%";
                case DiscFixed: return "Fixed amount";
                default: return null;
            }
        }

        private void cboDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = cboDiscountType.SelectedIndex;

            // Only "Custom %" and "Fixed amount" need a typed value.
            numDiscountValue.Enabled = (type == DiscPercent || type == DiscFixed);

            if (type == DiscPercent)
            {
                numDiscountValue.Maximum = 100m;
            }
            else
            {
                numDiscountValue.Maximum = 10000000m;
            }

            if (!numDiscountValue.Enabled) numDiscountValue.Value = 0;

            RecalculateTotals();
        }

        private void numDiscountValue_ValueChanged(object sender, EventArgs e)
        {
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            OrderTotals t = ComputeTotals();

            lblSubtotalValue.Text = t.Subtotal.ToString("N2");
            lblDiscountValue.Text = t.Discount > 0 ? "-" + t.Discount.ToString("N2") : "0.00";
            lblVatValue.Text = t.Vat.ToString("N2");
            lblTotalValue.Text = t.Total.ToString("N2");

            RecalculateChange(t);
        }

        private void numAmountPaid_ValueChanged(object sender, EventArgs e)
        {
            RecalculateChange(ComputeTotals());
        }

        private void btnExact_Click(object sender, EventArgs e)
        {
            decimal total = ComputeTotals().Total;
            numAmountPaid.Value = Math.Min(total, numAmountPaid.Maximum);
        }

        private void RecalculateChange(OrderTotals t)
        {
            decimal change = numAmountPaid.Value - t.Total;
            lblChangeValue.Text = change.ToString("N2");
            lblChangeValue.ForeColor = change < 0
                ? Color.Firebrick
                : Color.FromArgb(39, 174, 96);
        }

        // ------------------------------------------------------------------
        // Checkout
        // ------------------------------------------------------------------
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("The cart is empty.", "Nothing to Checkout",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrderTotals totals = ComputeTotals();

            if (numAmountPaid.Value < totals.Total)
            {
                MessageBox.Show("Amount paid is less than the total due (" + totals.Total.ToString("N2") + ").",
                    "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAmountPaid.Focus();
                return;
            }

            decimal amountPaid = numAmountPaid.Value;
            decimal change = amountPaid - totals.Total;
            string paymentMethod = cboPaymentMethod.SelectedItem == null ? "Cash" : cboPaymentMethod.SelectedItem.ToString();
            string discountLabel = DiscountLabel();
            string saleId = IdGenerator.Next("tbl_sale", "sale_id", "SALE", 6);

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO tbl_sale (sale_id, cashier_id, subtotal, discount_amount, discount_type, tax_amount, " +
                        "total_amount, amount_paid, change_due, payment_method, sale_status) " +
                        "VALUES (@SaleID, @UserID, @Subtotal, @Discount, @DiscountType, @Tax, " +
                        "@Total, @Paid, @Change, @Method, 'Completed');",
                        conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleId);
                        cmd.Parameters.AddWithValue("@UserID", SessionManager.UserID);
                        cmd.Parameters.AddWithValue("@Subtotal", totals.Subtotal);
                        cmd.Parameters.AddWithValue("@Discount", totals.Discount);
                        cmd.Parameters.AddWithValue("@DiscountType", (object)discountLabel ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tax", totals.Vat);
                        cmd.Parameters.AddWithValue("@Total", totals.Total);
                        cmd.Parameters.AddWithValue("@Paid", amountPaid);
                        cmd.Parameters.AddWithValue("@Change", change);
                        cmd.Parameters.AddWithValue("@Method", paymentMethod);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (CartItem item in cart)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO tbl_sale_item (sale_id, product_id, quantity, unit_price, line_total) " +
                            "VALUES (@SaleID, @ProductID, @Qty, @Price, @Subtotal);", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@SaleID", saleId);
                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            cmd.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmd.Parameters.AddWithValue("@Price", item.UnitPrice);
                            cmd.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                            cmd.ExecuteNonQuery();
                        }

                        // Only deduct if enough stock is still there (another terminal may have sold it).
                        using (MySqlCommand cmd = new MySqlCommand(
                            "UPDATE tbl_stock SET quantity = quantity - @Qty " +
                            "WHERE product_id = @ProductID AND quantity >= @Qty;",
                            conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                throw new InvalidOperationException(
                                    "Not enough stock left for \"" + item.ProductName + "\".");
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO tbl_stock_log (product_id, movement_type, reason, quantity, reference_id, performed_by, remarks) " +
                            "VALUES (@ProductID, 'OUT', 'SALE', @Qty, @SaleID, @UserID, @Remarks);", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            cmd.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmd.Parameters.AddWithValue("@SaleID", saleId);
                            cmd.Parameters.AddWithValue("@UserID", SessionManager.UserID);
                            cmd.Parameters.AddWithValue("@Remarks", "Sale #" + saleId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Checkout failed and was cancelled:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadProducts(); // stock numbers may have changed
                    return;
                }
            }

            // The sale is saved. Build the receipt BEFORE clearing the cart.
            lastReceipt = BuildReceipt(saleId, totals, discountLabel, amountPaid, change, paymentMethod);
            btnReprint.Enabled = true;

            ResetOrder();
            LoadProducts();
            ShowReceipt(lastReceipt);
            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Receipt
        // ------------------------------------------------------------------
        private List<string> BuildReceipt(string saleId, OrderTotals t, string discountLabel,
            decimal amountPaid, decimal change, string paymentMethod)
        {
            List<string> lines = new List<string>();
            const int width = 40;
            string vatPct = (VatRate * 100m).ToString("0") + "%";

            lines.Add(Center("KATHERINE STORE", width));
            lines.Add(Center("Inventory & POS System", width));
            lines.Add(new string('-', width));
            lines.Add("Receipt #: " + saleId);
            lines.Add("Date:      " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            lines.Add("Cashier:   " + SessionManager.FullName);
            lines.Add(new string('-', width));

            foreach (CartItem item in cart)
            {
                lines.Add(Truncate(item.ProductName, width));
                lines.Add(PadLeftRight(
                    "  " + item.Quantity + " x " + item.UnitPrice.ToString("N2"),
                    item.Subtotal.ToString("N2"), width));
            }

            lines.Add(new string('-', width));
            lines.Add(PadLeftRight("Subtotal", t.Subtotal.ToString("N2"), width));

            if (t.Discount > 0)
            {
                lines.Add(PadLeftRight("Discount (" + discountLabel + ")", "-" + t.Discount.ToString("N2"), width));
            }

            lines.Add(PadLeftRight(
                PricesIncludeVat ? "VAT " + vatPct + " (included)" : "VAT " + vatPct,
                t.Vat.ToString("N2"), width));
            lines.Add(new string('=', width));
            lines.Add(PadLeftRight("TOTAL", t.Total.ToString("N2"), width));
            lines.Add(new string('=', width));
            lines.Add(PadLeftRight("Payment Method", paymentMethod, width));
            lines.Add(PadLeftRight("Amount Paid", amountPaid.ToString("N2"), width));
            lines.Add(PadLeftRight("Change", change.ToString("N2"), width));
            lines.Add(new string('-', width));
            lines.Add(Center("Thank you for shopping!", width));

            return lines;
        }

        private void ShowReceipt(List<string> lines)
        {
            try
            {
                ReceiptPrinter printer = new ReceiptPrinter(lines);
                if (chkPrintDirect.Checked)
                    printer.Print();
                else
                    printer.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The sale was saved, but the receipt could not be printed:\n" + ex.Message +
                    "\n\nYou can try again with \"Reprint Last Receipt\".", "Printing Problem",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            if (lastReceipt == null) return;
            ShowReceipt(lastReceipt);
        }

        private static string Center(string text, int width)
        {
            if (text.Length >= width) return text;
            int padding = (width - text.Length) / 2;
            return new string(' ', padding) + text;
        }

        private static string Truncate(string text, int width)
        {
            return text.Length <= width ? text : text.Substring(0, width);
        }

        private static string PadLeftRight(string left, string right, int width)
        {
            if (left.Length + right.Length + 1 > width)
                left = left.Substring(0, Math.Max(0, width - right.Length - 1));
            int spaces = width - left.Length - right.Length;
            if (spaces < 1) spaces = 1;
            return left + new string(' ', spaces) + right;
        }
    }
}