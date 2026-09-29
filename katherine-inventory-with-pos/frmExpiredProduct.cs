using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmExpiredProduct : Form
    {
        public frmExpiredProduct()
        {
            InitializeComponent();
        }

        private void frmExpiredProduct_Load(object sender, EventArgs e)
        {
            LoadExpired();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExpired();
        }

        // Expiry is now tracked per incoming batch in tbl_stock_log rather than
        // on the product record itself, so this reads from the vw_expired_products
        // view (based on each product's most recent "IN" batch) instead of a
        // per-product ExpiryDate column.
        private void LoadExpired()
        {
            try
            {
                dgvExpired.DataSource = DBConnection.ExecuteQuery(
                    "SELECT product_id AS ProductID, product_name AS ProductName, category_name AS CategoryName, " +
                    "quantity_in_stock AS QuantityInStock, expiry_date AS ExpiryDate, days_expired AS DaysExpired " +
                    "FROM vw_expired_products ORDER BY days_expired DESC;");

                if (dgvExpired.Columns.Contains("ProductID"))
                    dgvExpired.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load expired products:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveFromStock_Click(object sender, EventArgs e)
        {
            if (dgvExpired.CurrentRow == null)
            {
                MessageBox.Show("Select a product from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productId = dgvExpired.CurrentRow.Cells["ProductID"].Value.ToString();
            string productName = dgvExpired.CurrentRow.Cells["ProductName"].Value.ToString();
            int currentQty = Convert.ToInt32(dgvExpired.CurrentRow.Cells["QuantityInStock"].Value);

            DialogResult confirm = MessageBox.Show(
                "Remove all " + currentQty + " unit(s) of \"" + productName + "\" from stock?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_stock SET quantity = 0 WHERE product_id = @ProductID;",
                    new MySqlParameter("@ProductID", productId));

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO tbl_stock_log (product_id, movement_type, reason, quantity, performed_by, remarks) " +
                    "VALUES (@ProductID, 'OUT', 'EXPIRED', @Qty, @UserID, 'Removed - expired');",
                    new MySqlParameter("@ProductID", productId),
                    new MySqlParameter("@Qty", currentQty),
                    new MySqlParameter("@UserID", SessionManager.UserID));

                MessageBox.Show("Stock removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadExpired();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
