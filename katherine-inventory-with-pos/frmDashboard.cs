using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class  frmDashboard : Form
    {
        // ---- Theme ----
        private static readonly Color Accent = Color.FromArgb(37, 99, 235);
        private static readonly Color Ink = Color.FromArgb(17, 24, 39);
        private static readonly Color Muted = Color.FromArgb(107, 114, 128);
        private static readonly Color Line = Color.FromArgb(229, 231, 235);
        private static readonly Color Page = Color.FromArgb(243, 244, 246);
        private const string Peso = "\u20B1#,##0.00";

        // Low-stock count. This REPLACES the missing view "vw_low_stock".
        // Adjust the column names to match your tbl_product.
        private const string LowStockSql =
            "SELECT COUNT(*) FROM tbl_product WHERE stock_qty <= reorder_level;";

        private Label lblSalesToday, lblTxToday, lblWeek, lblLowStock;

        public frmDashboard()
        {
            InitializeComponent();
            BuildCards();
            ApplyTheme();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        // ---------------------------------------------------------------
        // Data
        // ---------------------------------------------------------------
        private void LoadAll()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                DataTable sales = DBConnection.ExecuteQuery(
                    "SELECT DATE(created_at) AS SaleDay, COUNT(*) AS Transactions, " +
                    "SUM(total_amount) AS TotalSales " +
                    "FROM tbl_sale " +
                    "WHERE created_at >= DATE_SUB(CURDATE(), INTERVAL 6 DAY) " +
                    "GROUP BY DATE(created_at) " +
                    "ORDER BY SaleDay DESC;");
                dgvSalesByDay.DataSource = sales;
                Col(dgvSalesByDay, "SaleDay", "Day", "ddd, MMM dd, yyyy");
                Col(dgvSalesByDay, "Transactions", "Transactions", null, true);
                Col(dgvSalesByDay, "TotalSales", "Total Sales", Peso, true);
                UpdateSalesCards(sales);

                DataTable top = DBConnection.ExecuteQuery(
                    "SELECT p.product_name AS ProductName, SUM(si.quantity) AS UnitsSold, " +
                    "SUM(si.line_total) AS Revenue " +
                    "FROM tbl_sale_item si " +
                    "INNER JOIN tbl_product p ON p.product_id = si.product_id " +
                    "GROUP BY p.product_name " +
                    "ORDER BY UnitsSold DESC LIMIT 10;");
                dgvTopProducts.DataSource = top;
                Col(dgvTopProducts, "ProductName", "Product");
                Col(dgvTopProducts, "UnitsSold", "Units Sold", "N0", true);
                Col(dgvTopProducts, "Revenue", "Revenue", Peso, true);

                DataTable activity = DBConnection.ExecuteQuery(
                    "SELECT sl.created_at AS TransactionDate, p.product_name AS ProductName, " +
                    "CONCAT(sl.movement_type, ' - ', sl.reason) AS TransactionType, " +
                    "sl.quantity AS Quantity, a.user_name AS PerformedBy, sl.remarks AS Remarks " +
                    "FROM tbl_stock_log sl " +
                    "INNER JOIN tbl_product p ON p.product_id = sl.product_id " +
                    "LEFT JOIN tbl_account a ON a.acc_id = sl.performed_by " +
                    "ORDER BY sl.created_at DESC LIMIT 50;");
                dgvActivity.DataSource = activity;
                Col(dgvActivity, "TransactionDate", "Date", "MMM dd, yyyy  h:mm tt");
                Col(dgvActivity, "ProductName", "Product");
                Col(dgvActivity, "TransactionType", "Type");
                Col(dgvActivity, "Quantity", "Qty", "N0", true);
                Col(dgvActivity, "PerformedBy", "Performed By");
                Col(dgvActivity, "Remarks", "Remarks");
                if (dgvActivity.Columns.Contains("Remarks"))
                    dgvActivity.Columns["Remarks"].FillWeight = 200;

                lblUpdated.Text = "Last updated " + DateTime.Now.ToString("MMM dd, yyyy  h:mm tt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load dashboard data:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            LoadLowStock();
        }

        // Kept separate so a schema mismatch here can never blank the whole dashboard.
        private void LoadLowStock()
        {
            try
            {
                DataTable dt = DBConnection.ExecuteQuery(LowStockSql);
                lblLowStock.Text = Convert.ToInt32(dt.Rows[0][0]).ToString("N0");
            }
            catch
            {
                lblLowStock.Text = "\u2014";
            }
        }

        private void UpdateSalesCards(DataTable sales)
        {
            decimal today = 0, week = 0;
            int todayTx = 0;

            foreach (DataRow r in sales.Rows)
            {
                decimal amt = r["TotalSales"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TotalSales"]);
                week += amt;
                if (Convert.ToDateTime(r["SaleDay"]).Date == DateTime.Today)
                {
                    today += amt;
                    todayTx = Convert.ToInt32(r["Transactions"]);
                }
            }

            lblSalesToday.Text = today.ToString("\u20B1#,##0.00");
            lblTxToday.Text = todayTx.ToString("N0");
            lblWeek.Text = week.ToString("\u20B1#,##0.00");
        }

        // ---------------------------------------------------------------
        // Look & feel
        // ---------------------------------------------------------------
        private void BuildCards()
        {
            tlpCards.Controls.Add(BuildCard("Today's Sales", Accent, out lblSalesToday), 0, 0);
            tlpCards.Controls.Add(BuildCard("Transactions Today", Color.FromArgb(16, 185, 129), out lblTxToday), 1, 0);
            tlpCards.Controls.Add(BuildCard("Last 7 Days", Color.FromArgb(139, 92, 246), out lblWeek), 2, 0);
            tlpCards.Controls.Add(BuildCard("Low Stock Items", Color.FromArgb(245, 158, 11), out lblLowStock), 3, 0);
        }

        private Panel BuildCard(string caption, Color stripe, out Label value)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 12, 0),
                Padding = new Padding(18, 12, 8, 8)
            };
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Line))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                using (var brush = new SolidBrush(stripe))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, card.Height);
            };

            var cap = new Label
            {
                Text = caption,
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = Muted,
                Font = new Font("Segoe UI", 9.5F)
            };
            value = new Label
            {
                Text = "\u2014",
                Dock = DockStyle.Fill,
                ForeColor = Ink,
                Font = new Font("Segoe UI Semibold", 20F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(cap);
            card.Controls.Add(value);
            value.BringToFront();   // Fill control must dock last
            return card;
        }

        private void ApplyTheme()
        {
            // Dock order: header (back) -> cards -> body (front)
            pnlHeader.SendToBack();
            tlpBody.BringToFront();

            foreach (var section in new[] { pnlSales, pnlTop, pnlActivity })
            {
                var p = section;
                p.Paint += (s, e) =>
                {
                    using (var pen = new Pen(Line))
                        e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
                };
            }

            dgvSalesByDay.BringToFront();
            dgvTopProducts.BringToFront();
            dgvActivity.BringToFront();

            StyleGrid(dgvSalesByDay);
            StyleGrid(dgvTopProducts);
            StyleGrid(dgvActivity);

            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.BackColor = Accent;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Cursor = Cursors.Hand;
        }

        private static void StyleGrid(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.EnableHeadersVisualStyles = false;
            g.RowHeadersVisible = false;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Line;
            g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowTemplate.Height = 32;

            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;
            var h = g.ColumnHeadersDefaultCellStyle;
            h.BackColor = Color.FromArgb(249, 250, 251);
            h.ForeColor = Muted;
            h.Font = new Font("Segoe UI Semibold", 9F);
            h.SelectionBackColor = h.BackColor;
            h.SelectionForeColor = h.ForeColor;
            h.Padding = new Padding(8, 0, 8, 0);

            var d = g.DefaultCellStyle;
            d.BackColor = Color.White;
            d.ForeColor = Ink;
            d.Font = new Font("Segoe UI", 9.5F);
            d.SelectionBackColor = Color.FromArgb(219, 234, 254);
            d.SelectionForeColor = Ink;
            d.Padding = new Padding(8, 0, 8, 0);

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
        }

        private static void Col(DataGridView g, string name, string header,
                                string format = null, bool right = false)
        {
            if (!g.Columns.Contains(name)) return;
            var c = g.Columns[name];
            c.HeaderText = header;
            if (format != null) c.DefaultCellStyle.Format = format;
            if (right)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }
}