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

            OpenChildForm(new frmHome());
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
        /// Loads a form into the main content area, replacing whatever was shown
        /// before. This mimics an MDI child area without requiring IsMdiContainer.
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            if (panelMain.Controls.Count > 0)
            {
                Form previous = panelMain.Controls[0] as Form;
                panelMain.Controls.RemoveAt(0);
                if (previous != null) previous.Dispose();
            }

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
            OpenChildForm(new frmHome());
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
                    OpenChildForm(new frmHome());
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
