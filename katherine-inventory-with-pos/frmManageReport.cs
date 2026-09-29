using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmManageReport : Form
    {
        private DataTable currentReport;

        public frmManageReport()
        {
            InitializeComponent();
        }

        private void frmManageReport_Load(object sender, EventArgs e)
        {
            cboReportType.SelectedIndex = 0;
            btnGenerate_Click(sender, e);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cboReportType.SelectedItem == null) return;

            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddSeconds(-1); // include the whole "to" day

            if (from > to)
            {
                MessageBox.Show("The 'From' date must be before the 'To' date.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string type = cboReportType.SelectedItem.ToString();

                switch (type)
                {
                    case "Sales Report":
                        currentReport = DBConnection.ExecuteQuery(
                            "SELECT s.sale_id AS SaleID, s.created_at AS SaleDate, a.user_name AS CashierName, " +
                            "s.total_amount AS TotalAmount, s.amount_paid AS AmountPaid, s.change_due AS ChangeAmount, " +
                            "s.payment_method AS PaymentMethod " +
                            "FROM tbl_sale s " +
                            "LEFT JOIN tbl_account a ON a.acc_id = s.cashier_id " +
                            "WHERE s.created_at BETWEEN @From AND @To " +
                            "ORDER BY s.created_at DESC;",
                            new MySqlParameter("@From", from), new MySqlParameter("@To", to));

                        lblTotal.Text = "Total Sales: " + SumColumn(currentReport, "TotalAmount").ToString("N2");
                        break;

                    case "Stock Movement Report":
                        currentReport = DBConnection.ExecuteQuery(
                            "SELECT sl.created_at AS TransactionDate, p.product_name AS ProductName, " +
                            "CONCAT(sl.movement_type, ' - ', sl.reason) AS TransactionType, sl.quantity AS Quantity, " +
                            "a.user_name AS PerformedBy, sl.remarks AS Remarks " +
                            "FROM tbl_stock_log sl " +
                            "INNER JOIN tbl_product p ON p.product_id = sl.product_id " +
                            "LEFT JOIN tbl_account a ON a.acc_id = sl.performed_by " +
                            "WHERE sl.created_at BETWEEN @From AND @To " +
                            "ORDER BY sl.created_at DESC;",
                            new MySqlParameter("@From", from), new MySqlParameter("@To", to));

                        lblTotal.Text = "Transactions: " + currentReport.Rows.Count;
                        break;

                    case "Damage Report":
                        currentReport = DBConnection.ExecuteQuery(
                            "SELECT sl.created_at AS DateReported, p.product_name AS ProductName, sl.quantity AS Quantity, " +
                            "sl.remarks AS Reason, a.user_name AS ReportedBy " +
                            "FROM tbl_stock_log sl " +
                            "INNER JOIN tbl_product p ON p.product_id = sl.product_id " +
                            "LEFT JOIN tbl_account a ON a.acc_id = sl.performed_by " +
                            "WHERE sl.reason = 'DAMAGED' AND sl.created_at BETWEEN @From AND @To " +
                            "ORDER BY sl.created_at DESC;",
                            new MySqlParameter("@From", from), new MySqlParameter("@To", to));

                        lblTotal.Text = "Items Reported Damaged: " + SumColumn(currentReport, "Quantity").ToString("N0");
                        break;
                }

                dgvReport.DataSource = currentReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not generate report:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static decimal SumColumn(DataTable table, string columnName)
        {
            decimal total = 0;
            foreach (DataRow row in table.Rows)
            {
                if (row[columnName] != DBNull.Value)
                    total += Convert.ToDecimal(row[columnName]);
            }
            return total;
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (currentReport == null || currentReport.Rows.Count == 0)
            {
                MessageBox.Show("Generate a report first.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV file (*.csv)|*.csv";
                dialog.FileName = cboReportType.SelectedItem + " " + DateTime.Now.ToString("yyyyMMdd") + ".csv";

                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    StringBuilder sb = new StringBuilder();

                    string[] headers = new string[currentReport.Columns.Count];
                    for (int i = 0; i < currentReport.Columns.Count; i++)
                        headers[i] = currentReport.Columns[i].ColumnName;
                    sb.AppendLine(string.Join(",", headers));

                    foreach (DataRow row in currentReport.Rows)
                    {
                        string[] fields = new string[currentReport.Columns.Count];
                        for (int i = 0; i < currentReport.Columns.Count; i++)
                        {
                            string value = row[i] == DBNull.Value ? string.Empty : row[i].ToString();
                            value = value.Replace("\"", "\"\"");
                            if (value.Contains(",") || value.Contains("\""))
                                value = "\"" + value + "\"";
                            fields[i] = value;
                        }
                        sb.AppendLine(string.Join(",", fields));
                    }

                    File.WriteAllText(dialog.FileName, sb.ToString());
                    MessageBox.Show("Report exported.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not export report:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
