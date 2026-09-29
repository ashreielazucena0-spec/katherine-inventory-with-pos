using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmPointOfSale : Form
    {
        private readonly BindingList<CartItem> cart = new BindingList<CartItem>();

        public frmPointOfSale()
        {
            InitializeComponent();
        }

        private void frmPointOfSale_Load(object sender, EventArgs e)
        {
            dgvCart.AutoGenerateColumns = false;
            colProductCode.DataPropertyName = "ProductCode";
            colProductName.DataPropertyName = "ProductName";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colQuantity.DataPropertyName = "Quantity";
            colSubtotal.DataPropertyName = "Subtotal";
            colUnitPrice.DefaultCellStyle.Format = "N2";
            colSubtotal.DefaultCellStyle.Format = "N2";

            dgvCart.DataSource = cart;
            cboPaymentMethod.SelectedIndex = 0;

            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Barcode scanning
        // ------------------------------------------------------------------
        // Most USB / Bluetooth barcode scanners act as a keyboard: they "type" the
        // barcode digits very quickly and finish with an Enter key press. Handling
        // KeyDown for Enter on a plain TextBox is therefore all that is needed to
        // support a real scanner - no special SDK is required.
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

                    txtBarcode.Focus();
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
                RefreshCartGrid();
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
            }

            RecalculateTotal();
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
                }
            }
            txtBarcode.Focus();
        }

        // ------------------------------------------------------------------
        // Cart maintenance
        // ------------------------------------------------------------------
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

            RefreshCartGrid();
            RecalculateTotal();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null) return;
            int index = dgvCart.CurrentRow.Index;
            if (index >= 0 && index < cart.Count)
            {
                cart.RemoveAt(index);
                RecalculateTotal();
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            using (frmConfirmDialog confirm = new frmConfirmDialog("Clear Cart", "Remove all items from the current order?"))
            {
                if (confirm.ShowDialog(this) != DialogResult.Yes) return;
            }

            cart.Clear();
            RecalculateTotal();
        }

        private void RefreshCartGrid()
        {
            dgvCart.Refresh();
        }

        private decimal CartTotal
        {
            get { return cart.Sum(c => c.Subtotal); }
        }

        private void RecalculateTotal()
        {
            lblTotalValue.Text = CartTotal.ToString("N2");
            RecalculateChange();
        }

        private void numAmountPaid_ValueChanged(object sender, EventArgs e)
        {
            RecalculateChange();
        }

        private void RecalculateChange()
        {
            decimal change = numAmountPaid.Value - CartTotal;
            lblChangeValue.Text = change.ToString("N2");
            lblChangeValue.ForeColor = change < 0
                ? System.Drawing.Color.Firebrick
                : System.Drawing.Color.FromArgb(39, 174, 96);
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

            decimal total = CartTotal;

            if (numAmountPaid.Value < total)
            {
                MessageBox.Show("Amount paid is less than the total due.", "Insufficient Payment",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal amountPaid = numAmountPaid.Value;
            decimal change = amountPaid - total;
            string paymentMethod = cboPaymentMethod.SelectedItem == null ? "Cash" : cboPaymentMethod.SelectedItem.ToString();
            string saleId = IdGenerator.Next("tbl_sale", "sale_id", "SALE", 6);

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO tbl_sale (sale_id, cashier_id, subtotal, total_amount, amount_paid, change_due, payment_method, sale_status) " +
                        "VALUES (@SaleID, @UserID, @Total, @Total, @Paid, @Change, @Method, 'Completed');",
                        conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleId);
                        cmd.Parameters.AddWithValue("@UserID", SessionManager.UserID);
                        cmd.Parameters.AddWithValue("@Total", total);
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

                        using (MySqlCommand cmd = new MySqlCommand(
                            "UPDATE tbl_stock SET quantity = quantity - @Qty WHERE product_id = @ProductID;",
                            conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            cmd.ExecuteNonQuery();
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

                    PrintReceipt(saleId, total, amountPaid, change, paymentMethod);

                    cart.Clear();
                    numAmountPaid.Value = 0;
                    RecalculateTotal();
                    txtBarcode.Focus();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Checkout failed and was cancelled:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintReceipt(string saleId, decimal total, decimal amountPaid, decimal change, string paymentMethod)
        {
            List<string> lines = new List<string>();
            const int width = 40;

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
                string qtyLine = string.Format("  {0} x {1,10:N2} = {2,10:N2}",
                    item.Quantity.ToString().PadRight(3), item.UnitPrice, item.Subtotal);
                lines.Add(qtyLine);
            }

            lines.Add(new string('-', width));
            lines.Add(PadLeftRight("TOTAL", total.ToString("N2"), width));
            lines.Add(PadLeftRight("Payment Method", paymentMethod, width));
            lines.Add(PadLeftRight("Amount Paid", amountPaid.ToString("N2"), width));
            lines.Add(PadLeftRight("Change", change.ToString("N2"), width));
            lines.Add(new string('-', width));
            lines.Add(Center("Thank you for shopping!", width));

            ReceiptPrinter printer = new ReceiptPrinter(lines);
            printer.ShowPreview();
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
            int spaces = width - left.Length - right.Length;
            if (spaces < 1) spaces = 1;
            return left + new string(' ', spaces) + right;
        }
    }
}
