using katherine_inventory_with_pos.DataAccess;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace katherine_inventory_with_pos.Forms
{
    /// <summary>System Accounts: list, search, add, edit, activate/deactivate, reset password, delete (tbl_account).</summary>
    public partial class frmSystemAccount : Form
    {
        private const string SearchPlaceholder = "Enter account name to search. . .";

        public frmSystemAccount()
        {
            InitializeComponent();
        }

        private void frmSystemAccount_Load(object sender, EventArgs e)
        {
            txtsrch.Text = SearchPlaceholder;
            txtsrch.ForeColor = Color.Gray;

            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.AllowUserToAddRows = false;

            // Right-click menu on a row.
            var menu = new ContextMenuStrip();
            menu.Items.Add("Edit account", null, (s, a) => btnEdit_Click(s, a));
            menu.Items.Add("Activate / Deactivate", null, (s, a) => ToggleStatus());
            menu.Items.Add("Reset password...", null, (s, a) => ResetPassword());
            menu.Items.Add("Delete account", null, (s, a) => btnDelete_Click(s, a));
            dgvUsers.ContextMenuStrip = menu;
            dgvUsers.CellMouseDown += (s, a) =>
            {
                if (a.Button == MouseButtons.Right && a.RowIndex >= 0)
                {
                    dgvUsers.ClearSelection();
                    dgvUsers.Rows[a.RowIndex].Selected = true;
                    dgvUsers.CurrentCell = dgvUsers.Rows[a.RowIndex].Cells[colAccId.Index];
                }
            };
            dgvUsers.CellDoubleClick += (s, a) => { if (a.RowIndex >= 0) btnEdit_Click(s, a); };
            dgvUsers.CellFormatting -= dgvUsers_CellFormatting;
            dgvUsers.CellFormatting += dgvUsers_CellFormatting;

            RetrieveAccounts("");
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---------- search box ----------

        private void txtsrch_Enter(object sender, EventArgs e)
        {
            if (txtsrch.Text == SearchPlaceholder)
            {
                txtsrch.Text = "";
                txtsrch.ForeColor = Color.Black;
            }
        }

        private void txtsrch_Leave(object sender, EventArgs e)
        {
            if (txtsrch.Text == "")
            {
                txtsrch.Text = SearchPlaceholder;
                txtsrch.ForeColor = Color.Gray;
                RetrieveAccounts("");
            }
        }

        private void txtsrch_TextChanged(object sender, EventArgs e)
        {
            if (txtsrch.Text != SearchPlaceholder) RetrieveAccounts(txtsrch.Text);
        }

        private string CurrentSearch()
        {
            return txtsrch.Text == SearchPlaceholder ? "" : txtsrch.Text;
        }

        // ---------- loading ----------

        /// <summary>Loads accounts, filtered by the search text (id, name, type, gender, username, email, phone, status).</summary>
        private void RetrieveAccounts(string srch)
        {
            try
            {
                string keepId = SelectedId();
                DataTable dt;
                const string cols = "acc_id, acc_name, acc_type, gender, user_name, email, phone, pass_word, created_at, user_status";

                if (string.IsNullOrWhiteSpace(srch))
                {
                    dt = DBConnection.ExecuteQuery("SELECT " + cols + " FROM tbl_account ORDER BY acc_id ASC;");
                }
                else
                {
                    dt = DBConnection.ExecuteQuery(
                        "SELECT " + cols + " FROM tbl_account " +
                        "WHERE acc_id LIKE @s OR acc_name LIKE @s OR CAST(acc_type AS CHAR) LIKE @s OR gender LIKE @s " +
                        "OR user_name LIKE @s OR email LIKE @s OR phone LIKE @s OR user_status LIKE @s " +
                        "ORDER BY acc_id ASC;",
                        new MySqlParameter("@s", "%" + srch.Trim() + "%"));
                }

                dgvUsers.Rows.Clear();
                int num = 0;
                foreach (DataRow r in dt.Rows)
                {
                    num++;
                    string created = r["created_at"] == DBNull.Value
                        ? "" : Convert.ToDateTime(r["created_at"]).ToString("yyyy-MM-dd HH:mm");

                    int idx = dgvUsers.Rows.Add(num, r["acc_id"].ToString(), r["acc_name"].ToString(),
                        r["acc_type"].ToString(), r["gender"].ToString(), r["user_name"].ToString(),
                        r["email"].ToString(), r["phone"].ToString(), r["pass_word"].ToString(),
                        created, r["user_status"].ToString());

                    if (!string.Equals(r["user_status"].ToString(), "Active", StringComparison.OrdinalIgnoreCase))
                        dgvUsers.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;

                    if (keepId != null && r["acc_id"].ToString() == keepId)
                    {
                        dgvUsers.ClearSelection();
                        dgvUsers.Rows[idx].Selected = true;
                    }
                }
                if (keepId == null) dgvUsers.ClearSelection();
                lblCountRec.Text = "TOTAL RECORD'S FOUND: " + num;
            }
            catch (Exception ex)
            {
                ShowError("Could not load accounts:\n" + ex.Message);
            }
        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Never show the stored password (hash) - mask the column.
            if (e.ColumnIndex == colPass.Index && e.Value != null)
                e.Value = "********";
        }

        // ---------- helpers ----------

        private DataGridViewRow SelectedRow()
        {
            return dgvUsers.SelectedRows.Count > 0 ? dgvUsers.SelectedRows[0] : null;
        }

        private string SelectedId()
        {
            DataGridViewRow r = SelectedRow();
            return r == null ? null : r.Cells[colAccId.Index].Value as string;
        }

        private static void Warn(string msg, string title = "Validation")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void Info(string msg, string title = "Success")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool Confirm(string msg, string title = "Confirm")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private static object DbOrNull(string s)
        {
            return string.IsNullOrEmpty(s) ? (object)DBNull.Value : s;
        }

        /// <summary>True when this account is the only active Admin (so it must not be deleted/deactivated/demoted).</summary>
        private static bool IsLastActiveAdmin(string accId)
        {
            object cur = DBConnection.ExecuteScalar(
                "SELECT acc_type FROM tbl_account WHERE acc_id = @id AND user_status = 'Active';",
                new MySqlParameter("@id", accId));
            if (cur == null || cur.ToString() != "Admin") return false;

            object others = DBConnection.ExecuteScalar(
                "SELECT COUNT(*) FROM tbl_account WHERE acc_type = 'Admin' AND user_status = 'Active' AND acc_id <> @id;",
                new MySqlParameter("@id", accId));
            return Convert.ToInt32(others) == 0;
        }

        // ---------- ADD ----------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmSystemAccountEditor(false))
            {
                if (f.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    object existing = DBConnection.ExecuteScalar(
                        "SELECT COUNT(*) FROM tbl_account WHERE user_name = @Username;",
                        new MySqlParameter("@Username", f.Username));
                    if (Convert.ToInt32(existing) > 0)
                    {
                        Warn("That username is already taken.", "Duplicate Username");
                        return;
                    }

                    string accId = IdGenerator.Next("tbl_account", "acc_id", "ACC", 3);

                    DBConnection.ExecuteNonQuery(
                        "INSERT INTO tbl_account (acc_id, acc_name, user_name, pass_word, gender, acc_type, phone, email, user_status) " +
                        "VALUES (@AccId, @FullName, @Username, @Hash, @Gender, @AccType, @Phone, @Email, @Status);",
                        new MySqlParameter("@AccId", accId),
                        new MySqlParameter("@FullName", f.FullName),
                        new MySqlParameter("@Username", f.Username),
                        new MySqlParameter("@Hash", PasswordHelper.HashPassword(f.Password)),
                        new MySqlParameter("@Gender", f.Gender),
                        new MySqlParameter("@AccType", f.AccType),
                        new MySqlParameter("@Phone", DbOrNull(f.Phone)),
                        new MySqlParameter("@Email", DbOrNull(f.Email)),
                        new MySqlParameter("@Status", f.IsActive ? "Active" : "Inactive"));

                    Info("Account created.");
                    RetrieveAccounts(CurrentSearch());
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        // ---------- EDIT ----------

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = SelectedId();
            if (string.IsNullOrEmpty(id)) { Warn("Please select an account to edit first.", "No Selection"); return; }

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(
                    "SELECT acc_id, acc_name, acc_type, gender, user_name, phone, email, user_status " +
                    "FROM tbl_account WHERE acc_id = @id;",
                    new MySqlParameter("@id", id));
                if (dt.Rows.Count == 0)
                {
                    Warn("That account no longer exists.");
                    RetrieveAccounts("");
                    return;
                }
                DataRow r = dt.Rows[0];

                using (var f = new frmSystemAccountEditor(true))
                {
                    f.LoadValues(r["user_name"].ToString(), r["acc_name"].ToString(), r["gender"].ToString(),
                        r["acc_type"].ToString(), r["phone"].ToString(), r["email"].ToString(),
                        r["user_status"].ToString() == "Active");

                    if (f.ShowDialog(this) != DialogResult.OK) return;

                    // Safety rules (same as the accounts screen).
                    bool losesAdminOrActive = !f.IsActive || f.AccType != "Admin";
                    if (id == SessionManager.UserID && !f.IsActive)
                    {
                        Warn("You cannot deactivate the account you are currently logged in with.", "Not Allowed");
                        return;
                    }
                    if (losesAdminOrActive && IsLastActiveAdmin(id))
                    {
                        Warn("This is the only active Admin account. It must stay an active Admin.", "Not Allowed");
                        return;
                    }

                    object dup = DBConnection.ExecuteScalar(
                        "SELECT COUNT(*) FROM tbl_account WHERE user_name = @Username AND acc_id <> @AccId;",
                        new MySqlParameter("@Username", f.Username),
                        new MySqlParameter("@AccId", id));
                    if (Convert.ToInt32(dup) > 0)
                    {
                        Warn("That username is already taken.", "Duplicate Username");
                        return;
                    }

                    DBConnection.ExecuteNonQuery(
                        "UPDATE tbl_account SET user_name = @Username, acc_name = @FullName, gender = @Gender, " +
                        "acc_type = @AccType, phone = @Phone, email = @Email, user_status = @Status " +
                        "WHERE acc_id = @AccId;",
                        new MySqlParameter("@Username", f.Username),
                        new MySqlParameter("@FullName", f.FullName),
                        new MySqlParameter("@Gender", f.Gender),
                        new MySqlParameter("@AccType", f.AccType),
                        new MySqlParameter("@Phone", DbOrNull(f.Phone)),
                        new MySqlParameter("@Email", DbOrNull(f.Email)),
                        new MySqlParameter("@Status", f.IsActive ? "Active" : "Inactive"),
                        new MySqlParameter("@AccId", id));

                    if (f.Password.Length > 0)
                    {
                        DBConnection.ExecuteNonQuery(
                            "UPDATE tbl_account SET pass_word = @Hash WHERE acc_id = @AccId;",
                            new MySqlParameter("@Hash", PasswordHelper.HashPassword(f.Password)),
                            new MySqlParameter("@AccId", id));
                    }

                    Info("Account updated.");
                }
                RetrieveAccounts(CurrentSearch());
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        // ---------- ACTIVATE / DEACTIVATE ----------

        /// <summary>Right-click: switches the selected account between Active and Inactive.</summary>
        private void ToggleStatus()
        {
            DataGridViewRow row = SelectedRow();
            if (row == null) { Warn("Please select an account first.", "No Selection"); return; }

            string id = row.Cells[colAccId.Index].Value as string;
            string name = row.Cells[colName.Index].Value as string;
            bool active = string.Equals(row.Cells[colStatus.Index].Value as string, "Active",
                StringComparison.OrdinalIgnoreCase);

            try
            {
                if (active)
                {
                    if (id == SessionManager.UserID) { Warn("You can't deactivate the account you are logged in with.", "Not Allowed"); return; }
                    if (IsLastActiveAdmin(id)) { Warn("This is the only active Admin account and can't be deactivated.", "Not Allowed"); return; }
                    if (!Confirm("Deactivate '" + name + "'? They will no longer be able to log in.")) return;
                }

                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_account SET user_status = @s WHERE acc_id = @id;",
                    new MySqlParameter("@s", active ? "Inactive" : "Active"),
                    new MySqlParameter("@id", id));
                RetrieveAccounts(CurrentSearch());
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        // ---------- RESET PASSWORD ----------

        /// <summary>Right-click: sets a new password for the selected account.</summary>
        private void ResetPassword()
        {
            DataGridViewRow row = SelectedRow();
            if (row == null) { Warn("Please select an account first.", "No Selection"); return; }

            string id = row.Cells[colAccId.Index].Value as string;
            string name = row.Cells[colName.Index].Value as string;

            string p1 = AskPassword("New password for '" + name + "' (at least 6 characters):");
            if (p1 == null) return;
            if (p1.Length < 6) { Warn("Password must be at least 6 characters."); return; }
            string p2 = AskPassword("Type the new password again:");
            if (p2 == null) return;
            if (p1 != p2) { Warn("The two passwords don't match."); return; }

            try
            {
                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_account SET pass_word = @Hash WHERE acc_id = @AccId;",
                    new MySqlParameter("@Hash", PasswordHelper.HashPassword(p1)),
                    new MySqlParameter("@AccId", id));
                Info("Password updated.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        /// <summary>Small masked-input prompt. Returns null if cancelled.</summary>
        private string AskPassword(string prompt)
        {
            using (var dlg = new Form())
            using (var lbl = new Label())
            using (var txt = new TextBox())
            using (var ok = new Button())
            using (var cancel = new Button())
            {
                dlg.Text = "Reset Password";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.ClientSize = new Size(380, 130);

                lbl.AutoSize = true;
                lbl.MaximumSize = new Size(350, 0);
                lbl.Location = new Point(15, 15);
                lbl.Text = prompt;

                txt.UseSystemPasswordChar = true;
                txt.Location = new Point(15, 55);
                txt.Width = 350;

                ok.Text = "OK";
                ok.DialogResult = DialogResult.OK;
                ok.Location = new Point(190, 90);
                cancel.Text = "Cancel";
                cancel.DialogResult = DialogResult.Cancel;
                cancel.Location = new Point(285, 90);

                dlg.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
                dlg.AcceptButton = ok;
                dlg.CancelButton = cancel;

                return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
            }
        }

        // ---------- DELETE ----------

        /// <summary>DELETE button: removes the selected account after confirmation.</summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = SelectedRow();
            if (row == null) { Warn("Please select an account to delete.", "No Selection"); return; }

            string id = row.Cells[colAccId.Index].Value as string ?? "";
            string name = row.Cells[colName.Index].Value as string ?? "";

            if (id.Length == 0) { ShowError("Invalid account selected."); return; }
            if (id == SessionManager.UserID) { Warn("You can't delete the account you are logged in with.", "Not Allowed"); return; }

            try
            {
                if (IsLastActiveAdmin(id)) { Warn("This is the only active Admin account and can't be deleted.", "Not Allowed"); return; }

                if (!Confirm("Delete the account '" + name + "'?\n\nSales and stock records made by this account are kept, " +
                             "but will no longer show who made them. Deactivating the account instead keeps that history.",
                             "Confirm Delete"))
                    return;

                DBConnection.ExecuteNonQuery("DELETE FROM tbl_account WHERE acc_id = @id;",
                    new MySqlParameter("@id", id));

                Info("Account deleted successfully.");
                RetrieveAccounts(CurrentSearch());
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
    }

   
    public class frmSystemAccountEditor : Form
    {
        private readonly bool _isEdit;

        private readonly TextBox txtUsername = new TextBox();
        private readonly TextBox txtFullName = new TextBox();
        private readonly TextBox txtPassword = new TextBox();
        private readonly TextBox txtPhone = new TextBox();
        private readonly TextBox txtEmail = new TextBox();
        private readonly ComboBox cboGender = new ComboBox();
        private readonly ComboBox cboAccType = new ComboBox();
        private readonly CheckBox chkActive = new CheckBox();
        private readonly Button btnSave = new Button();
        private readonly Button btnCancel = new Button();

        public string Username { get { return txtUsername.Text.Trim(); } }
        public string FullName { get { return txtFullName.Text.Trim(); } }
        /// <summary>Plain-text password typed by the user (blank when editing = keep current).</summary>
        public string Password { get { return txtPassword.Text; } }
        public string Phone { get { return txtPhone.Text.Trim(); } }
        public string Email { get { return txtEmail.Text.Trim(); } }
        public string Gender { get { return cboGender.SelectedItem == null ? null : cboGender.SelectedItem.ToString(); } }
        public string AccType { get { return cboAccType.SelectedItem == null ? null : cboAccType.SelectedItem.ToString(); } }
        public bool IsActive { get { return chkActive.Checked; } }

        public frmSystemAccountEditor(bool isEdit)
        {
            _isEdit = isEdit;
            BuildUi();
        }

        public void LoadValues(string username, string fullName, string gender, string accType,
                               string phone, string email, bool active)
        {
            txtUsername.Text = username;
            txtFullName.Text = fullName;
            cboGender.SelectedItem = cboGender.Items.Contains(gender) ? gender : null;
            cboAccType.SelectedItem = cboAccType.Items.Contains(accType) ? accType : null;
            txtPhone.Text = phone;
            txtEmail.Text = email;
            chkActive.Checked = active;
        }

        private void BuildUi()
        {
            Text = _isEdit ? "Update Account" : "Add Account";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 470);
            AcceptButton = btnSave;
            CancelButton = btnCancel;

            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.LightCoral };
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 15.75F),
                Location = new Point(16, 10),
                Text = _isEdit ? "Update Account" : "Add Account"
            });
            Controls.Add(header);

            cboGender.Items.AddRange(new object[] { "Female", "Male", "Prefer not to say" });
            cboAccType.Items.AddRange(new object[] { "Admin", "Cashier", "Inventory Staff", "Manager" });
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAccType.DropDownStyle = ComboBoxStyle.DropDownList;
            txtPassword.UseSystemPasswordChar = true;
            chkActive.Text = "Active";
            chkActive.Checked = true;
            chkActive.AutoSize = true;

            int y = 64;
            AddRow("Username", txtUsername, ref y);
            AddRow("Full Name", txtFullName, ref y);
            AddRow(_isEdit ? "New Password (leave blank to keep)" : "Password (at least 6 characters)", txtPassword, ref y);
            AddRow("Gender", cboGender, ref y);
            AddRow("Account Type", cboAccType, ref y);
            AddRow("Phone", txtPhone, ref y);
            AddRow("Email", txtEmail, ref y);

            chkActive.Location = new Point(20, y);
            Controls.Add(chkActive);

            btnSave.Text = _isEdit ? "Update" : "Save";
            btnSave.BackColor = Color.Coral;
            btnSave.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            btnSave.Size = new Size(116, 37);
            btnSave.Location = new Point(ClientSize.Width - 256, ClientSize.Height - 52);
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            Controls.Add(btnSave);

            btnCancel.Text = "CANCEL";
            btnCancel.BackColor = Color.LightCoral;
            btnCancel.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            btnCancel.Size = new Size(100, 37);
            btnCancel.Location = new Point(ClientSize.Width - 130, ClientSize.Height - 52);
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.DialogResult = DialogResult.Cancel;
            Controls.Add(btnCancel);
        }

        private void AddRow(string label, Control input, ref int y)
        {
            Controls.Add(new Label { AutoSize = true, Location = new Point(20, y), Text = label });
            input.Location = new Point(20, y + 20);
            input.Width = 380;
            Controls.Add(input);
            y += 52;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Username.Length == 0 || FullName.Length == 0)
            {
                Warn("Username and full name are required.");
                return;
            }
            if (!_isEdit && Password.Length == 0)
            {
                Warn("A password is required for a new account.");
                return;
            }
            if (Password.Length > 0 && Password.Length < 6)
            {
                Warn("Password must be at least 6 characters.");
                return;
            }
            if (cboGender.SelectedItem == null) { Warn("Please select a gender."); return; }
            if (cboAccType.SelectedItem == null) { Warn("Please select an account type."); return; }

            DialogResult = DialogResult.OK;
        }

        private static void Warn(string msg)
        {
            MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}