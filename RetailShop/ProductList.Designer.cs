namespace RetailShop
{
    partial class ProductList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdNil = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.lblMsg = new System.Windows.Forms.Label();
            this.Col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.rdNil);
            this.groupBox1.Controls.Add(this.rdAll);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(724, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Filter";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(590, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(123, 37);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdNil
            // 
            this.rdNil.AutoSize = true;
            this.rdNil.Location = new System.Drawing.Point(305, 22);
            this.rdNil.Name = "rdNil";
            this.rdNil.Size = new System.Drawing.Size(112, 24);
            this.rdNil.TabIndex = 21;
            this.rdNil.TabStop = true;
            this.rdNil.Text = "Nil Products";
            this.rdNil.UseVisualStyleBackColor = true;
            this.rdNil.CheckedChanged += new System.EventHandler(this.rdNil_CheckedChanged);
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Location = new System.Drawing.Point(124, 22);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(112, 24);
            this.rdAll.TabIndex = 20;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All Products";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.rdAll_CheckedChanged);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col1,
            this.Col2,
            this.Col3,
            this.Col4,
            this.Col5,
            this.Col6,
            this.Col7});
            this.dgvProduct.Location = new System.Drawing.Point(7, 62);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.Size = new System.Drawing.Size(724, 382);
            this.dgvProduct.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Yellow;
            this.lblMsg.Location = new System.Drawing.Point(6, 450);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(16, 17);
            this.lblMsg.TabIndex = 16;
            this.lblMsg.Text = "..";
            // 
            // Col1
            // 
            this.Col1.HeaderText = "S/N";
            this.Col1.Name = "Col1";
            this.Col1.ReadOnly = true;
            this.Col1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col1.Width = 50;
            // 
            // Col2
            // 
            this.Col2.HeaderText = "Model";
            this.Col2.Name = "Col2";
            this.Col2.ReadOnly = true;
            this.Col2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col2.Width = 130;
            // 
            // Col3
            // 
            this.Col3.HeaderText = "Product Name";
            this.Col3.Name = "Col3";
            this.Col3.ReadOnly = true;
            this.Col3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col3.Width = 170;
            // 
            // Col4
            // 
            this.Col4.HeaderText = "Quantity";
            this.Col4.Name = "Col4";
            this.Col4.ReadOnly = true;
            this.Col4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col4.Width = 80;
            // 
            // Col5
            // 
            this.Col5.HeaderText = "Cost";
            this.Col5.Name = "Col5";
            this.Col5.ReadOnly = true;
            this.Col5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col5.Width = 95;
            // 
            // Col6
            // 
            this.Col6.HeaderText = "Sale";
            this.Col6.Name = "Col6";
            this.Col6.ReadOnly = true;
            this.Col6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col6.Width = 95;
            // 
            // Col7
            // 
            this.Col7.HeaderText = "Created On";
            this.Col7.Name = "Col7";
            this.Col7.ReadOnly = true;
            this.Col7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(742, 480);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Products List";
            this.Load += new System.EventHandler(this.ProductList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdNil;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col7;
    }
}