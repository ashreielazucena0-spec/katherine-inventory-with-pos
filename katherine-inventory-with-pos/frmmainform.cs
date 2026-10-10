using System;
using System.Windows.Forms;
using katherine_inventory_with_pos.DataAccess;
using katherine_inventory_with_pos.Forms;

namespace katherine_inventory_with_pos
{
    public partial class frmmainform : Form
    {
        public frmmainform()
        {
            InitializeComponent();
        }

        private void frmmainform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            ApplySessionToUi();

            ClearChildForms();
        }

        private void ApplySessionToUi()
        {
            label3.Text = "Welcome, " + SessionManager.FullName + "!";
            lblStatusUser.Text = SessionManager.Username;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("hh:mm:ss tt");
            label1.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        /// <summary>
        /// Removes and disposes every child form shown in the main content area,
        /// leaving the panel blank.
        /// </summary>
        private void ClearChildForms()
        {
            for (int i = panelMain.Controls.Count - 1; i >= 0; i--)
            {
                Control c = panelMain.Controls[i];
                panelMain.Controls.RemoveAt(i);
                c.Dispose();
            }
        }

        /// <summary>
        /// Loads a form into the main content area, replacing whatever was shown
        /// before. This mimics an MDI child area without requiring IsMdiContainer.
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            ClearChildForms();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            childForm.Show();
        }

        // ------------------------------------------------------------------
        // Toolbar navigation
        // ------------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearChildForms();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDashboard());
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPointOfSale());
        }

        private void manageStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmManageProduct());
        }

        private void manageStockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmManageStock());
        }

        private void expiredProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmExpiredProduct());
        }

        private void damageProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDamageProduct());
        }

        private void manageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmManageReport());
        }

        private void systemAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSystemAccount());
        }

        private void exitAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit the application?", "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Log out of the current account?", "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SessionManager.Clear();
            this.Hide();

            using (frmLogin loginForm = new frmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    ApplySessionToUi();
                    ClearChildForms();
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}