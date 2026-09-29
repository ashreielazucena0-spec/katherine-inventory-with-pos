using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    /// <summary>
    /// Small dialog used from the POS screen to register a brand-new product on the
    /// fly (e.g. when a scanned barcode doesn't match anything in the database yet).
    /// </summary>
    public partial class frmQuickAddProduct : Form
    {
        public string NewProductID { get; private set; }
        public string NewProductCode { get; private set; }
        public string NewProductName { get; private set; }
        public decimal NewProductPrice { get; private set; }
        public int NewProductQuantity { get; private set; }

        private readonly string prefillCode;

        public frmQuickAddProduct(string scannedCode)
        {
            InitializeComponent();
            prefillCode = scannedCode;
        }

        private void frmQuickAddProduct_Load(object sender, EventArgs e)
        {
            txtCode.Text = prefillCode;

            try
            {
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";
                cboCategory.DataSource = DBConnection.ExecuteQuery(
                    "SELECT category_id AS CategoryID, category_name AS CategoryName " +
                    "FROM tbl_category ORDER BY category_name;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load categories:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrWhiteSpace(prefillCode))
                txtCode.Focus();
            else
                txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                lblStatus.Text = "Product name is required.";
                return;
            }
            if (numPrice.Value <= 0)
            {
                lblStatus.Text = "Selling price must be greater than zero.";
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    object existing = DBConnection.ExecuteScalar(
                        "SELECT COUNT(*) FROM tbl_product WHERE barcode = @Code;",
                        new MySqlParameter("@Code", code));

                    if (Convert.ToInt32(existing) > 0)
                    {
                        lblStatus.Text = "A product with that code already exists.";
                        return;
                    }
                }

                object categoryId = cboCategory.SelectedValue == null ? (object)DBNull.Value : cboCategory.SelectedValue;
                string newId = IdGenerator.Next("tbl_product", "product_id", "PRD", 4);

                const string insert = "INSERT INTO tbl_product " +
                    "(product_id, product_name, barcode, category_id, cost_price, selling_price, product_status) " +
                    "VALUES (@ProductID, @Name, @Code, @CategoryID, @Cost, @Price, 'Active');";

                DBConnection.ExecuteNonQuery(insert,
                    new MySqlParameter("@ProductID", newId),
                    new MySqlParameter("@Name", name),
                    new MySqlParameter("@Code", string.IsNullOrEmpty(code) ? (object)DBNull.Value : code),
                    new MySqlParameter("@CategoryID", categoryId),
                    new MySqlParameter("@Price", numPrice.Value),
                    new MySqlParameter("@Cost", numCost.Value));

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO tbl_stock (product_id, quantity, low_stock_threshold) VALUES (@ProductID, @Qty, 10);",
                    new MySqlParameter("@ProductID", newId),
                    new MySqlParameter("@Qty", numQuantity.Value));

                if (numQuantity.Value > 0)
                {
                    DBConnection.ExecuteNonQuery(
                        "INSERT INTO tbl_stock_log (product_id, movement_type, reason, quantity, performed_by, remarks) " +
                        "VALUES (@ProductID, 'IN', 'PURCHASE', @Qty, @UserID, 'Initial stock on registration');",
                        new MySqlParameter("@ProductID", newId),
                        new MySqlParameter("@Qty", numQuantity.Value),
                        new MySqlParameter("@UserID", SessionManager.UserID));
                }

                NewProductID = newId;
                NewProductCode = code;
                NewProductName = name;
                NewProductPrice = numPrice.Value;
                NewProductQuantity = (int)numQuantity.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not save product.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
