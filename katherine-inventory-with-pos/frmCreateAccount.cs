using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;

namespace katherine_inventory_with_pos.Forms
{
    public partial class frmCreateAccount : Form
    {
        public frmCreateAccount()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;

            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblStatus.Text = "Full name, username and password are required.";
                return;
            }

            if (cboGender.SelectedItem == null)
            {
                lblStatus.Text = "Please select a gender.";
                return;
            }

            if (cboAccType.SelectedItem == null)
            {
                lblStatus.Text = "Please select an account type.";
                return;
            }

            if (password.Length < 6)
            {
                lblStatus.Text = "Password must be at least 6 characters.";
                return;
            }

            if (password != confirmPassword)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            try
            {
                object existing = DBConnection.ExecuteScalar(
                    "SELECT COUNT(*) FROM tbl_account WHERE user_name = @Username;",
                    new MySqlParameter("@Username", username));

                if (Convert.ToInt32(existing) > 0)
                {
                    lblStatus.Text = "That username is already taken.";
                    return;
                }

                string accId = IdGenerator.Next("tbl_account", "acc_id", "ACC", 3);

                DBConnection.ExecuteNonQuery(
                    "INSERT INTO tbl_account (acc_id, acc_name, user_name, pass_word, gender, acc_type, phone, email, user_status) " +
                    "VALUES (@AccId, @FullName, @Username, @Hash, @Gender, @AccType, @Phone, @Email, 'Active');",
                    new MySqlParameter("@AccId", accId),
                    new MySqlParameter("@FullName", fullName),
                    new MySqlParameter("@Username", username),
                    new MySqlParameter("@Hash", PasswordHelper.HashPassword(password)),
                    new MySqlParameter("@Gender", cboGender.SelectedItem.ToString()),
                    new MySqlParameter("@AccType", cboAccType.SelectedItem.ToString()),
                    new MySqlParameter("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone),
                    new MySqlParameter("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));

                MessageBox.Show("Account created. You can now log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (MySqlException ex)
            {
                lblStatus.Text = "Could not reach the database.";
                MessageBox.Show("Database connection error:\n" + ex.Message, "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Account creation failed.";
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