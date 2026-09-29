using System;
using System.Data;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + SessionManager.FullName + "!";
            LoadStats();
        }

        private void LoadStats()
        {
            try
            {
                object productCount = DBConnection.ExecuteScalar(
                    "SELECT COUNT(*) FROM tbl_product WHERE product_status = 'Active';");
                lblProductsValue.Text = productCount.ToString();

                object stockTotal = DBConnection.ExecuteScalar(
                    "SELECT IFNULL(SUM(s.quantity), 0) FROM tbl_stock s " +
                    "INNER JOIN tbl_product p ON p.product_id = s.product_id WHERE p.product_status = 'Active';");
                lblStockValue.Text = stockTotal.ToString();

                object todaySales = DBConnection.ExecuteScalar(
                    "SELECT IFNULL(SUM(total_amount), 0) FROM tbl_sale WHERE DATE(created_at) = CURDATE();");
                lblSalesValue.Text = Convert.ToDecimal(todaySales).ToString("N2");

                DataTable lowStock = DBConnection.ExecuteQuery(
                    "SELECT product_id AS ProductID, product_name AS ProductName, category_name AS CategoryName, " +
                    "quantity_in_stock AS QuantityInStock, low_stock_threshold AS ReorderLevel " +
                    "FROM vw_low_stock;");
                lblLowStockValue.Text = lowStock.Rows.Count.ToString();

                dgvLowStock.DataSource = lowStock;
                if (dgvLowStock.Columns.Contains("ProductID"))
                    dgvLowStock.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load dashboard data:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
