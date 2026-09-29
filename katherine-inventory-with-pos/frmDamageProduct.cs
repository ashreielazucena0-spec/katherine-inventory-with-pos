using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmDamageProduct : Form
    {
        public frmDamageProduct()
        {
            InitializeComponent();
        }

        private void frmDamageProduct_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadHistory();
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
                    "SELECT sl.created_at AS DateReported, p.product_name AS ProductName, " +
                    "sl.quantity AS Quantity, sl.remarks AS Reason, a.user_name AS ReportedBy " +
                    "FROM tbl_stock_log sl " +
                    "INNER JOIN tbl_product p ON p.product_id = sl.product_id " +
                    "LEFT JOIN tbl_account a ON a.acc_id = sl.performed_by " +
                    "WHERE sl.reason = 'DAMAGED' " +
                    "ORDER BY sl.created_at DESC LIMIT 100;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load damage history:\n" + ex.Message, "Error",
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

            string productId = cboProduct.SelectedValue.ToString();
            int quantity = (int)numQuantity.Value;
            string reason = txtReason.Text.Trim();

            DataRowView row = cboProduct.SelectedItem as DataRowView;
            int currentStock = row == null ? 0 : Convert.ToInt32(row["QuantityInStock"]);

            if (quantity > currentStock)
            {
                MessageBox.Show("Damaged quantity cannot exceed current stock (" + currentStock + ").",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_stock SET quantity = quantity - @Qty WHERE product_id = @ProductID;",
                    new MySqlParameter("@Qty", quantity),
                    new MySqlParameter("@ProductID", productId));

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO tbl_stock_log (product_id, movement_type, reason, quantity, performed_by, remarks) " +
                    "VALUES (@ProductID, 'OUT', 'DAMAGED', @Qty, @UserID, @Remarks);",
                    new MySqlParameter("@ProductID", productId),
                    new MySqlParameter("@Qty", quantity),
                    new MySqlParameter("@UserID", SessionManager.UserID),
                    new MySqlParameter("@Remarks", string.IsNullOrEmpty(reason) ? (object)"Damaged product" : reason));

                MessageBox.Show("Damage reported and stock updated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnClear_Click(sender, e);
                LoadProducts();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            numQuantity.Value = 1;
            txtReason.Clear();
        }
    }
}
