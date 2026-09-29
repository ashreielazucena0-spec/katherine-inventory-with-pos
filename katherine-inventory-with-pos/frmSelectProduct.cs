using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    /// <summary>
    /// Searchable, multi-select product picker. Adapted from the
    /// Inventory-system-With-Pos project's "addtoproduct" screen: instead of
    /// hunting through a plain dropdown, the user can search by code/name and
    /// select one or more existing products at once. If the product they want
    /// isn't in the catalog yet, "Add New Product" opens frmQuickAddProduct
    /// right from here and the new item is added to the list automatically.
    /// </summary>
    public partial class frmSelectProduct : Form
    {
        private const string SearchPlaceholder = "Search product code or name...";
        private static readonly Color PlaceholderColor = Color.Gray;
        private static readonly Color InputTextColor = Color.Black;

        private bool _isSearchPlaceholderShown;

        /// <summary>The products the user picked, populated once ShowDialog() returns DialogResult.OK.</summary>
        public List<SelectedProduct> SelectedProducts { get; } = new List<SelectedProduct>();

        public frmSelectProduct()
        {
            InitializeComponent();
            dgvProducts.MultiSelect = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void frmSelectProduct_Load(object sender, EventArgs e)
        {
            ShowSearchPlaceholder();
            LoadProducts();
        }

        // ---------------------------------------------------------------
        // Search box placeholder behaviour
        // ---------------------------------------------------------------
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (_isSearchPlaceholderShown) HideSearchPlaceholder();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                ShowSearchPlaceholder();
        }

        private void ShowSearchPlaceholder()
        {
            _isSearchPlaceholderShown = true;
            txtSearch.ForeColor = PlaceholderColor;
            txtSearch.Text = SearchPlaceholder;
        }

        private void HideSearchPlaceholder()
        {
            _isSearchPlaceholderShown = false;
            txtSearch.ForeColor = InputTextColor;
            txtSearch.Text = "";
        }

        private string CurrentSearchText => _isSearchPlaceholderShown ? "" : txtSearch.Text.Trim();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_isSearchPlaceholderShown) return;
            LoadProducts(txtSearch.Text.Trim());
        }

        // ---------------------------------------------------------------
        // Data loading
        // ---------------------------------------------------------------
        private void LoadProducts(string search = null)
        {
            try
            {
                string query = "SELECT p.product_id AS ProductID, p.barcode AS ProductCode, p.product_name AS ProductName, " +
                                "c.category_name AS CategoryName, p.selling_price AS Price, IFNULL(s.quantity, 0) AS QuantityInStock " +
                                "FROM tbl_product p " +
                                "LEFT JOIN tbl_category c ON c.category_id = p.category_id " +
                                "LEFT JOIN tbl_stock s ON s.product_id = p.product_id " +
                                "WHERE p.product_status = 'Active' ";

                DataTable table;
                if (string.IsNullOrWhiteSpace(search))
                {
                    query += "ORDER BY p.product_name;";
                    table = DBConnection.ExecuteQuery(query);
                }
                else
                {
                    query += "AND (p.barcode LIKE @Search OR p.product_name LIKE @Search) " +
                              "ORDER BY p.product_name;";
                    table = DBConnection.ExecuteQuery(query, new MySqlParameter("@Search", "%" + search + "%"));
                }

                dgvProducts.DataSource = table;
                if (dgvProducts.Columns.Contains("ProductID"))
                    dgvProducts.Columns["ProductID"].Visible = false;

                UpdateEmptyState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load products:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEmptyState()
        {
            bool hasRows = dgvProducts.Rows.Count > 0;
            lblEmptyState.Visible = !hasRows;
        }

        // ---------------------------------------------------------------
        // Add a brand-new product straight from the picker
        // ---------------------------------------------------------------
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            using (frmQuickAddProduct form = new frmQuickAddProduct(CurrentSearchText))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadProducts(CurrentSearchText);
                    SelectRowByProductId(form.NewProductID);
                }
            }
        }

        private void SelectRowByProductId(string productId)
        {
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["ProductID"].Value == null) continue;
                if (row.Cells["ProductID"].Value.ToString() == productId)
                {
                    dgvProducts.ClearSelection();
                    row.Selected = true;
                    dgvProducts.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        // ---------------------------------------------------------------
        // Confirm selection
        // ---------------------------------------------------------------
        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least one product from the list first.", "Select Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedProducts.Clear();
            var rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvProducts.SelectedRows) rows.Add(row);
            rows.Reverse(); // SelectedRows enumerates bottom-up; keep on-screen order

            foreach (DataGridViewRow row in rows)
            {
                SelectedProducts.Add(new SelectedProduct
                {
                    ProductID = row.Cells["ProductID"].Value.ToString(),
                    ProductCode = row.Cells["ProductCode"].Value?.ToString(),
                    ProductName = row.Cells["ProductName"].Value?.ToString(),
                    Price = Convert.ToDecimal(row.Cells["Price"].Value),
                    QuantityInStock = Convert.ToInt32(row.Cells["QuantityInStock"].Value)
                });
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            btnAddSelected_Click(sender, e);
        }
    }

    /// <summary>One product as picked from frmSelectProduct.</summary>
    public class SelectedProduct
    {
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
