using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AttemptLogin();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void AttemptLogin()
        {
            lblStatus.Text = string.Empty;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Please enter both username and password.";
                return;
            }

            try
            {
                const string query = "SELECT acc_id, user_name, pass_word, acc_name, acc_type, user_status " +
                                      "FROM tbl_account WHERE user_name = @Username;";

                DataTable table = DBConnection.ExecuteQuery(query,
                    new MySqlParameter("@Username", username));

                if (table.Rows.Count == 0)
                {
                    lblStatus.Text = "Invalid username or password.";
                    return;
                }

                DataRow row = table.Rows[0];

                if (row["user_status"].ToString() != "Active")
                {
                    lblStatus.Text = "This account has been disabled. Contact an administrator.";
                    return;
                }

                string storedHash = row["pass_word"].ToString();
                if (!PasswordHelper.VerifyPassword(password, storedHash))
                {
                    lblStatus.Text = "Invalid username or password.";
                    return;
                }

                // Silently upgrade old unsalted SHA-256 hashes to the current format.
                if (PasswordHelper.NeedsRehash(storedHash))
                {
                    try
                    {
                        DBConnection.ExecuteNonQuery(
                            "UPDATE tbl_account SET pass_word = @Hash WHERE acc_id = @AccId;",
                            new MySqlParameter("@Hash", PasswordHelper.HashPassword(password)),
                            new MySqlParameter("@AccId", row["acc_id"].ToString()));
                    }
                    catch (MySqlException) { /* login still succeeds; it'll upgrade next time */ }
                }

                SessionManager.UserID = row["acc_id"].ToString();
                SessionManager.Username = row["user_name"].ToString();
                SessionManager.FullName = row["acc_name"].ToString();
                SessionManager.AccType = row["acc_type"].ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (MySqlException ex)
            {
                lblStatus.Text = "Could not reach the database.";
                MessageBox.Show("Database connection error:\n" + ex.Message +
                                 "\n\nCheck the connection string in App.config and make sure " +
                                 "the inventorydb database has been created (see the inventorydb.sql script).",
                                 "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Login failed.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            using (frmCreateAccount createForm = new frmCreateAccount())
            {
                createForm.ShowDialog();
            }
        }
    }
}