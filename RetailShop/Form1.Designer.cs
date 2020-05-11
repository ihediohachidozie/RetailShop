namespace RetailShop
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.productManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.salesManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSales = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPartPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalesReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.salesHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSalesHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalesReturnHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.expenseManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpenseType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPostExpense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExpenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductsList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductSold = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductsReturned = new System.Windows.Forms.ToolStripMenuItem();
            this.financialStatusToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFinancialStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDatabaseBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productManagementToolStripMenuItem,
            this.salesManagementToolStripMenuItem,
            this.expenseManagementToolStripMenuItem,
            this.reportsManagementToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.mnuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(923, 25);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // productManagementToolStripMenuItem
            // 
            this.productManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProductType,
            this.mnuAddProduct});
            this.productManagementToolStripMenuItem.Name = "productManagementToolStripMenuItem";
            this.productManagementToolStripMenuItem.Size = new System.Drawing.Size(151, 21);
            this.productManagementToolStripMenuItem.Text = "Product Management";
            // 
            // mnuProductType
            // 
            this.mnuProductType.Name = "mnuProductType";
            this.mnuProductType.Size = new System.Drawing.Size(153, 22);
            this.mnuProductType.Text = "Product Type";
            this.mnuProductType.Click += new System.EventHandler(this.mnuProductType_Click);
            // 
            // mnuAddProduct
            // 
            this.mnuAddProduct.Name = "mnuAddProduct";
            this.mnuAddProduct.Size = new System.Drawing.Size(153, 22);
            this.mnuAddProduct.Text = "Add Product";
            this.mnuAddProduct.Click += new System.EventHandler(this.mnuAddProduct_Click);
            // 
            // salesManagementToolStripMenuItem
            // 
            this.salesManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSales,
            this.mnuPartPayment,
            this.mnuSalesReturn,
            this.salesHistoryToolStripMenuItem,
            this.mnuSalesHistory,
            this.mnuSalesReturnHistory});
            this.salesManagementToolStripMenuItem.Name = "salesManagementToolStripMenuItem";
            this.salesManagementToolStripMenuItem.Size = new System.Drawing.Size(135, 21);
            this.salesManagementToolStripMenuItem.Text = "Sales Management";
            // 
            // mnuSales
            // 
            this.mnuSales.Name = "mnuSales";
            this.mnuSales.Size = new System.Drawing.Size(190, 22);
            this.mnuSales.Text = "Sales";
            this.mnuSales.Click += new System.EventHandler(this.mnuSales_Click);
            // 
            // mnuPartPayment
            // 
            this.mnuPartPayment.Name = "mnuPartPayment";
            this.mnuPartPayment.Size = new System.Drawing.Size(190, 22);
            this.mnuPartPayment.Text = "Part-Payment";
            this.mnuPartPayment.Click += new System.EventHandler(this.mnuPartPayment_Click);
            // 
            // mnuSalesReturn
            // 
            this.mnuSalesReturn.Name = "mnuSalesReturn";
            this.mnuSalesReturn.Size = new System.Drawing.Size(190, 22);
            this.mnuSalesReturn.Text = "Sales Return";
            this.mnuSalesReturn.Click += new System.EventHandler(this.mnuSalesReturn_Click);
            // 
            // salesHistoryToolStripMenuItem
            // 
            this.salesHistoryToolStripMenuItem.Name = "salesHistoryToolStripMenuItem";
            this.salesHistoryToolStripMenuItem.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuSalesHistory
            // 
            this.mnuSalesHistory.Name = "mnuSalesHistory";
            this.mnuSalesHistory.Size = new System.Drawing.Size(190, 22);
            this.mnuSalesHistory.Text = "Sales History";
            this.mnuSalesHistory.Click += new System.EventHandler(this.mnuSalesHistory_Click);
            // 
            // mnuSalesReturnHistory
            // 
            this.mnuSalesReturnHistory.Name = "mnuSalesReturnHistory";
            this.mnuSalesReturnHistory.Size = new System.Drawing.Size(190, 22);
            this.mnuSalesReturnHistory.Text = "Sales Return History";
            this.mnuSalesReturnHistory.Click += new System.EventHandler(this.mnuSalesReturnHistory_Click);
            // 
            // expenseManagementToolStripMenuItem
            // 
            this.expenseManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExpenseType,
            this.mnuPostExpense,
            this.toolStripMenuItem1,
            this.mnuExpenseHistory});
            this.expenseManagementToolStripMenuItem.Name = "expenseManagementToolStripMenuItem";
            this.expenseManagementToolStripMenuItem.Size = new System.Drawing.Size(152, 21);
            this.expenseManagementToolStripMenuItem.Text = "Expense Management";
            // 
            // mnuExpenseType
            // 
            this.mnuExpenseType.Name = "mnuExpenseType";
            this.mnuExpenseType.Size = new System.Drawing.Size(166, 22);
            this.mnuExpenseType.Text = "Expense Type";
            this.mnuExpenseType.Click += new System.EventHandler(this.mnuExpenseType_Click);
            // 
            // mnuPostExpense
            // 
            this.mnuPostExpense.Name = "mnuPostExpense";
            this.mnuPostExpense.Size = new System.Drawing.Size(166, 22);
            this.mnuPostExpense.Text = "Post Expense";
            this.mnuPostExpense.Click += new System.EventHandler(this.mnuPostExpense_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // mnuExpenseHistory
            // 
            this.mnuExpenseHistory.Name = "mnuExpenseHistory";
            this.mnuExpenseHistory.Size = new System.Drawing.Size(166, 22);
            this.mnuExpenseHistory.Text = "Expense History";
            this.mnuExpenseHistory.Click += new System.EventHandler(this.mnuExpenseHistory_Click);
            // 
            // reportsManagementToolStripMenuItem
            // 
            this.reportsManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProductsList,
            this.mnuProductSold,
            this.mnuProductsReturned,
            this.financialStatusToolStripMenuItem,
            this.mnuFinancialStatus,
            this.dashboardToolStripMenuItem});
            this.reportsManagementToolStripMenuItem.Name = "reportsManagementToolStripMenuItem";
            this.reportsManagementToolStripMenuItem.Size = new System.Drawing.Size(149, 21);
            this.reportsManagementToolStripMenuItem.Text = "Reports Management";
            // 
            // mnuProductsList
            // 
            this.mnuProductsList.Name = "mnuProductsList";
            this.mnuProductsList.Size = new System.Drawing.Size(185, 22);
            this.mnuProductsList.Text = "Products List";
            this.mnuProductsList.Click += new System.EventHandler(this.mnuProductsList_Click);
            // 
            // mnuProductSold
            // 
            this.mnuProductSold.Name = "mnuProductSold";
            this.mnuProductSold.Size = new System.Drawing.Size(185, 22);
            this.mnuProductSold.Text = "Products Sold";
            this.mnuProductSold.Click += new System.EventHandler(this.mnuProductSold_Click);
            // 
            // mnuProductsReturned
            // 
            this.mnuProductsReturned.Name = "mnuProductsReturned";
            this.mnuProductsReturned.Size = new System.Drawing.Size(185, 22);
            this.mnuProductsReturned.Text = "Products Returned";
            this.mnuProductsReturned.Click += new System.EventHandler(this.mnuProductsReturned_Click);
            // 
            // financialStatusToolStripMenuItem
            // 
            this.financialStatusToolStripMenuItem.Name = "financialStatusToolStripMenuItem";
            this.financialStatusToolStripMenuItem.Size = new System.Drawing.Size(182, 6);
            // 
            // mnuFinancialStatus
            // 
            this.mnuFinancialStatus.Name = "mnuFinancialStatus";
            this.mnuFinancialStatus.Size = new System.Drawing.Size(185, 22);
            this.mnuFinancialStatus.Text = "Financial Status";
            this.mnuFinancialStatus.Click += new System.EventHandler(this.mnuFinancialStatus_Click);
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.dashboardToolStripMenuItem.Text = "Yearly Sales Chart";
            this.dashboardToolStripMenuItem.Click += new System.EventHandler(this.dashboardToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUsers,
            this.mnuRoles,
            this.mnuChangePassword,
            this.mnuCompany,
            this.toolStripMenuItem2,
            this.mnuDatabaseBackup});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // mnuUsers
            // 
            this.mnuUsers.Name = "mnuUsers";
            this.mnuUsers.Size = new System.Drawing.Size(182, 22);
            this.mnuUsers.Text = "Users";
            this.mnuUsers.Click += new System.EventHandler(this.mnuUsers_Click);
            // 
            // mnuRoles
            // 
            this.mnuRoles.Name = "mnuRoles";
            this.mnuRoles.Size = new System.Drawing.Size(182, 22);
            this.mnuRoles.Text = "Roles";
            this.mnuRoles.Click += new System.EventHandler(this.mnuRoles_Click);
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Size = new System.Drawing.Size(182, 22);
            this.mnuChangePassword.Text = "Change Password";
            this.mnuChangePassword.Click += new System.EventHandler(this.mnuChangePassword_Click);
            // 
            // mnuCompany
            // 
            this.mnuCompany.Name = "mnuCompany";
            this.mnuCompany.Size = new System.Drawing.Size(182, 22);
            this.mnuCompany.Text = "Company";
            this.mnuCompany.Click += new System.EventHandler(this.mnuCompany_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 6);
            // 
            // mnuDatabaseBackup
            // 
            this.mnuDatabaseBackup.Name = "mnuDatabaseBackup";
            this.mnuDatabaseBackup.Size = new System.Drawing.Size(182, 22);
            this.mnuDatabaseBackup.Text = "Database Backup";
            this.mnuDatabaseBackup.Click += new System.EventHandler(this.mnuDatabaseBackup_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(40, 21);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(923, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 420);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retail Shop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem productManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuProductType;
        private System.Windows.Forms.ToolStripMenuItem mnuAddProduct;
        private System.Windows.Forms.ToolStripMenuItem salesManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSales;
        private System.Windows.Forms.ToolStripMenuItem mnuPartPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesReturn;
        private System.Windows.Forms.ToolStripSeparator salesHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesReturnHistory;
        private System.Windows.Forms.ToolStripMenuItem expenseManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExpenseType;
        private System.Windows.Forms.ToolStripMenuItem mnuPostExpense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExpenseHistory;
        private System.Windows.Forms.ToolStripMenuItem reportsManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuProductsList;
        private System.Windows.Forms.ToolStripMenuItem mnuProductSold;
        private System.Windows.Forms.ToolStripMenuItem mnuProductsReturned;
        private System.Windows.Forms.ToolStripSeparator financialStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFinancialStatus;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuUsers;
        private System.Windows.Forms.ToolStripMenuItem mnuRoles;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem mnuCompany;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabaseBackup;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
    }
}

