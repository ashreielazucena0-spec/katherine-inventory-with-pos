namespace katherine_inventory_with_pos.Forms
{
    partial class frmExpiredProduct
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemoveFromStock = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.dgvExpired = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpired)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(210, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Expired Product";
            //
            // btnRefresh
            //
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(860, 16);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // btnRemoveFromStock
            //
            this.btnRemoveFromStock.BackColor = System.Drawing.Color.Firebrick;
            this.btnRemoveFromStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFromStock.ForeColor = System.Drawing.Color.White;
            this.btnRemoveFromStock.Location = new System.Drawing.Point(20, 60);
            this.btnRemoveFromStock.Name = "btnRemoveFromStock";
            this.btnRemoveFromStock.Size = new System.Drawing.Size(220, 34);
            this.btnRemoveFromStock.TabIndex = 2;
            this.btnRemoveFromStock.Text = "Remove Selected from Stock";
            this.btnRemoveFromStock.UseVisualStyleBackColor = false;
            this.btnRemoveFromStock.Click += new System.EventHandler(this.btnRemoveFromStock_Click);
            //
            // lblHint
            //
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(250, 68);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(430, 17);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "Sets stock on hand to 0 for the selected expired product.";
            //
            // dgvExpired
            //
            this.dgvExpired.AllowUserToAddRows = false;
            this.dgvExpired.AllowUserToDeleteRows = false;
            this.dgvExpired.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExpired.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpired.Location = new System.Drawing.Point(20, 106);
            this.dgvExpired.MultiSelect = false;
            this.dgvExpired.Name = "dgvExpired";
            this.dgvExpired.ReadOnly = true;
            this.dgvExpired.RowHeadersWidth = 24;
            this.dgvExpired.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpired.Size = new System.Drawing.Size(960, 352);
            this.dgvExpired.TabIndex = 4;
            //
            // frmExpiredProduct
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 470);
            this.Controls.Add(this.dgvExpired);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnRemoveFromStock);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmExpiredProduct";
            this.Text = "Expired Product";
            this.Load += new System.EventHandler(this.frmExpiredProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpired)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRemoveFromStock;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.DataGridView dgvExpired;
    }
}
