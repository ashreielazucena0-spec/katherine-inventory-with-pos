namespace katherine_inventory_with_pos.Forms
{
    partial class frmSelectProduct
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblEmptyState = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(230, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Select Product";
            //
            // txtSearch
            //
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtSearch.Location = new System.Drawing.Point(20, 54);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(540, 24);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            //
            // dgvProducts
            //
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.Location = new System.Drawing.Point(20, 88);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersWidth = 24;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(540, 320);
            this.dgvProducts.TabIndex = 2;
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);
            //
            // lblEmptyState
            //
            this.lblEmptyState.AutoSize = true;
            this.lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this.lblEmptyState.Location = new System.Drawing.Point(240, 240);
            this.lblEmptyState.Name = "lblEmptyState";
            this.lblEmptyState.Size = new System.Drawing.Size(180, 17);
            this.lblEmptyState.TabIndex = 3;
            this.lblEmptyState.Text = "No matching products.";
            this.lblEmptyState.Visible = false;
            //
            // btnAddNew
            //
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Location = new System.Drawing.Point(20, 420);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(160, 34);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Text = "+ Add New Product";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            //
            // btnAddSelected
            //
            this.btnAddSelected.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAddSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSelected.ForeColor = System.Drawing.Color.White;
            this.btnAddSelected.Location = new System.Drawing.Point(340, 420);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(110, 34);
            this.btnAddSelected.TabIndex = 5;
            this.btnAddSelected.Text = "Select";
            this.btnAddSelected.UseVisualStyleBackColor = false;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            //
            // btnCancel
            //
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(456, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // frmSelectProduct
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 474);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddSelected);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lblEmptyState);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "frmSelectProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Product";
            this.Load += new System.EventHandler(this.frmSelectProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblEmptyState;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}
