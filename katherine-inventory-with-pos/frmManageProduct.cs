using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmManageProduct : Form
    {
        private string selectedProductId = null;

        public frmManageProduct()
        {
            InitializeComponent();
        }

        private void frmManageProduct_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                DataTable categories = DBConnection.ExecuteQuery(
                    "SELECT category_id AS CategoryID, category_name AS CategoryName " +
                    "FROM tbl_category ORDER BY category_name;");

                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";
                cboCategory.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load categories:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Note: per-product expiry dates aren't tracked in inventorydb anymore -
        // expiry is recorded per incoming batch in tbl_stock_log instead (see
        // Manage Stock / Expired Products). chkHasExpiry/dtpExpiry are left in
        // the form for now but no longer read from or written to the database.
        private void LoadProducts(string search = null)
        {
            try
            {
                string query = "SELECT p.product_id AS ProductID, p.barcode AS ProductCode, p.product_name AS ProductName, " +
                                "c.category_name AS CategoryName, p.selling_price AS Price, p.cost_price AS Cost, " +
                                "IFNULL(s.quantity, 0) AS QuantityInStock, IFNULL(s.low_stock_threshold, 10) AS ReorderLevel, " +
                                "CASE WHEN p.product_status = 'Active' THEN 1 ELSE 0 END AS IsActive " +
                                "FROM tbl_product p " +
                                "LEFT JOIN tbl_category c ON c.category_id = p.category_id " +
                                "LEFT JOIN tbl_stock s ON s.product_id = p.product_id ";

                DataTable table;
                if (string.IsNullOrWhiteSpace(search))
                {
                    query += "ORDER BY p.product_name;";
                    table = DBConnection.ExecuteQuery(query);
                }
                else
                {
                    query += "WHERE p.barcode LIKE @Search OR p.product_name LIKE @Search " +
                              "ORDER BY p.product_name;";
                    table = DBConnection.ExecuteQuery(query, new MySqlParameter("@Search", "%" + search + "%"));
                }

                dgvProducts.DataSource = table;
                if (dgvProducts.Columns.Contains("ProductID"))
                    dgvProducts.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load products:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void chkHasExpiry_CheckedChanged(object sender, EventArgs e)
        {
            dtpExpiry.Enabled = chkHasExpiry.Checked;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Selling price must be greater than zero.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                string productId = IdGenerator.Next("tbl_product", "product_id", "PRD", 4);

                const string insert = "INSERT INTO tbl_product " +
                    "(product_id, product_name, barcode, category_id, cost_price, selling_price, product_status) " +
                    "VALUES (@ProductID, @Name, @Code, @CategoryID, @Cost, @Price, @Status);";

                DBConnection.ExecuteNonQuery(insert, BuildParameters(productId));

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO tbl_stock (product_id, quantity, low_stock_threshold) VALUES (@ProductID, @Qty, @Reorder);",
                    new MySqlParameter("@ProductID", productId),
                    new MySqlParameter("@Qty", numQuantity.Value),
                    new MySqlParameter("@Reorder", numReorder.Value));

                MessageBox.Show("Product added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("A product with that code already exists.", "Duplicate Code",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == null)
            {
                MessageBox.Show("Select a product from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateForm()) return;

            try
            {
                const string update = "UPDATE tbl_product SET " +
                    "product_name = @Name, barcode = @Code, category_id = @CategoryID, " +
                    "cost_price = @Cost, selling_price = @Price, product_status = @Status " +
                    "WHERE product_id = @ProductID;";

                MySqlParameter[] parameters = BuildParameters(selectedProductId);
                DBConnection.ExecuteNonQuery(update, parameters);

                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_stock SET quantity = @Qty, low_stock_threshold = @Reorder WHERE product_id = @ProductID;",
                    new MySqlParameter("@Qty", numQuantity.Value),
                    new MySqlParameter("@Reorder", numReorder.Value),
                    new MySqlParameter("@ProductID", selectedProductId));

                MessageBox.Show("Product updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("A product with that code already exists.", "Duplicate Code",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == null)
            {
                MessageBox.Show("Select a product from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmConfirmDialog confirm = new frmConfirmDialog("Confirm Delete",
                "This will deactivate the product (it stays in history for past sales/stock records). Continue?"))
            {
                if (confirm.ShowDialog(this) != DialogResult.Yes) return;
            }

            try
            {
                // Soft-delete so existing sales/stock-log foreign keys stay valid.
                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_product SET product_status = 'Inactive' WHERE product_id = @ProductID;",
                    new MySqlParameter("@ProductID", selectedProductId));

                MessageBox.Show("Product removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

            selectedProductId = row.Cells["ProductID"].Value.ToString();
            txtProductCode.Text = row.Cells["ProductCode"].Value == DBNull.Value
                ? string.Empty : row.Cells["ProductCode"].Value.ToString();
            txtProductName.Text = row.Cells["ProductName"].Value.ToString();

            string categoryName = row.Cells["CategoryName"].Value == DBNull.Value
                ? null : row.Cells["CategoryName"].Value.ToString();
            cboCategory.SelectedIndex = -1;
            foreach (DataRowView item in cboCategory.Items)
            {
                if (item["CategoryName"].ToString() == categoryName)
                {
                    cboCategory.SelectedItem = item;
                    break;
                }
            }

            numPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
            numCost.Value = Convert.ToDecimal(row.Cells["Cost"].Value);
            numQuantity.Value = Convert.ToDecimal(row.Cells["QuantityInStock"].Value);
            numReorder.Value = Convert.ToDecimal(row.Cells["ReorderLevel"].Value);

            chkHasExpiry.Checked = false;

            chkActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
        }

        private MySqlParameter[] BuildParameters(string productId)
        {
            object categoryId = cboCategory.SelectedValue == null ? (object)DBNull.Value : cboCategory.SelectedValue;
            object code = string.IsNullOrWhiteSpace(txtProductCode.Text) ? (object)DBNull.Value : txtProductCode.Text.Trim();

            return new[]
            {
                new MySqlParameter("@ProductID", productId),
                new MySqlParameter("@Name", txtProductName.Text.Trim()),
                new MySqlParameter("@Code", code),
                new MySqlParameter("@CategoryID", categoryId),
                new MySqlParameter("@Price", numPrice.Value),
                new MySqlParameter("@Cost", numCost.Value),
                new MySqlParameter("@Status", chkActive.Checked ? "Active" : "Inactive")
            };
        }

        private void ClearForm()
        {
            selectedProductId = null;
            txtProductCode.Clear();
            txtProductName.Clear();
            cboCategory.SelectedIndex = -1;
            numPrice.Value = 0;
            numCost.Value = 0;
            numQuantity.Value = 0;
            numReorder.Value = 10;
            chkHasExpiry.Checked = false;
            chkActive.Checked = true;
            dgvProducts.ClearSelection();
        }
    }
}
