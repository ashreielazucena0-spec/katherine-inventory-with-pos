using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmManageStock : Form
    {
        public frmManageStock()
        {
            InitializeComponent();
        }

        private void frmManageStock_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadHistory();
            cboType.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            try
            {
                DataTable products = DBConnection.ExecuteQuery(
                    "SELECT p.product_id AS ProductID, " +
                    "CONCAT(IFNULL(p.barcode, p.product_id), ' - ', p.product_name) AS Display, " +
                    "IFNULL(s.quantity, 0) AS QuantityInStock " +
                    "FROM tbl_product p " +
                    "LEFT JOIN tbl_stock s ON s.product_id = p.product_id " +
                    "WHERE p.product_status = 'Active' ORDER BY p.product_name;");

                cboProduct.DisplayMember = "Display";
                cboProduct.ValueMember = "ProductID";
                cboProduct.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load products:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistory()
        {
            try
            {
                dgvHistory.DataSource = DBConnection.ExecuteQuery(
                    "SELECT sl.created_at AS TransactionDate, p.product_name AS ProductName, " +
                    "CASE WHEN sl.movement_type = 'IN' THEN 'Stock In' ELSE 'Adjustment' END AS TransactionType, " +
                    "sl.quantity AS Quantity, a.user_name AS RecordedBy, sl.remarks AS Remarks " +
                    "FROM tbl_stock_log sl " +
                    "INNER JOIN tbl_product p ON p.product_id = sl.product_id " +
                    "LEFT JOIN tbl_account a ON a.acc_id = sl.performed_by " +
                    "WHERE sl.reason IN ('PURCHASE', 'ADJUSTMENT') " +
                    "ORDER BY sl.created_at DESC LIMIT 100;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load stock history:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = cboProduct.SelectedItem as DataRowView;
            lblCurrentStock.Text = row == null
                ? "Current stock: -"
                : "Current stock: " + row["QuantityInStock"];
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Select a transaction type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productId = cboProduct.SelectedValue.ToString();
            string type = cboType.SelectedItem.ToString();
            int quantity = (int)numQuantity.Value;
            string remarks = txtRemarks.Text.Trim();
            string reason = type == "Stock In" ? "PURCHASE" : "ADJUSTMENT";
            string movementType = "IN";

            try
            {
                ApplyStockChange(productId, movementType, reason, quantity, remarks);

                MessageBox.Show("Stock transaction saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(sender, e);
                LoadProducts();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStockChange(string productId, string movementType, string reason, int quantity, string remarks)
        {
            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_stock SET quantity = quantity + @Qty WHERE product_id = @ProductID;",
                new MySqlParameter("@Qty", quantity),
                new MySqlParameter("@ProductID", productId));

            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_stock_log (product_id, movement_type, reason, quantity, performed_by, remarks) " +
                "VALUES (@ProductID, @MovementType, @Reason, @Qty, @UserID, @Remarks);",
                new MySqlParameter("@ProductID", productId),
                new MySqlParameter("@MovementType", movementType),
                new MySqlParameter("@Reason", reason),
                new MySqlParameter("@Qty", quantity),
                new MySqlParameter("@UserID", SessionManager.UserID),
                new MySqlParameter("@Remarks", string.IsNullOrEmpty(remarks) ? (object)DBNull.Value : remarks));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            numQuantity.Value = 1;
            txtRemarks.Clear();
            cboType.SelectedIndex = 0;
        }

        // ------------------------------------------------------------------
        // Product picker: search/select from the full catalog instead of
        // hunting through the dropdown, with an "Add New Product" fallback.
        // Picking one product just selects it in the dropdown as before;
        // picking several applies the same Type/Quantity/Remarks to each of
        // them as a bulk stock transaction.
        // ------------------------------------------------------------------
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (frmSelectProduct picker = new frmSelectProduct())
            {
                if (picker.ShowDialog(this) != DialogResult.OK) return;
                if (picker.SelectedProducts.Count == 0) return;

                if (picker.SelectedProducts.Count == 1)
                {
                    LoadProducts();
                    cboProduct.SelectedValue = picker.SelectedProducts[0].ProductID;
                    numQuantity.Focus();
                    return;
                }

                ApplyBulkStockTransaction(picker.SelectedProducts);
            }
        }

        private void ApplyBulkStockTransaction(System.Collections.Generic.List<SelectedProduct> products)
        {
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Select a transaction type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string type = cboType.SelectedItem.ToString();
            string reason = type == "Stock In" ? "PURCHASE" : "ADJUSTMENT";
            int quantity = (int)numQuantity.Value;
            string remarks = txtRemarks.Text.Trim();
            int succeeded = 0;

            try
            {
                foreach (SelectedProduct product in products)
                {
                    ApplyStockChange(product.ProductID, "IN", reason, quantity, remarks);
                    succeeded++;
                }

                MessageBox.Show(succeeded + " stock transaction(s) saved.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(this, EventArgs.Empty);
                LoadProducts();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(succeeded + " of " + products.Count + " transaction(s) saved before this error:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadProducts();
                LoadHistory();
            }
        }
    }
}
