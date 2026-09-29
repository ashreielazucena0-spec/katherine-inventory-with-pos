using System;
using System.Drawing;
using System.Windows.Forms;

namespace katherine_inventory_with_pos.Forms
{
    /// <summary>
    /// Reusable Yes/No confirmation dialog used in place of MessageBox.Show for
    /// destructive or important actions (delete product, clear cart, etc.).
    /// Ported from the Inventory-system-With-Pos project.
    ///
    /// Usage:
    ///   using (var confirm = new frmConfirmDialog("Delete Product", "Delete \"Milk\"? This cannot be undone."))
    ///   {
    ///       if (confirm.ShowDialog(this) != DialogResult.Yes) return;
    ///   }
    /// </summary>
    public partial class frmConfirmDialog : Form
    {
        public frmConfirmDialog(string title, string message)
        {
            InitializeComponent();
            this.Text = title;
            lblTitle.Text = title;
            lblMessage.Text = message;
            pictureBoxWarning.Image = SystemIcons.Warning.ToBitmap();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
