using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductTypeMgt productType = new ProductTypeMgt();
        ProductMgt productMgt = new ProductMgt();
        salesMgt salesMgt = new salesMgt();
        partPayment PartPay = new partPayment();
        salesReturned salesReturn = new salesReturned();
        expense expenseType = new expense();
        postExpense PostExpense = new postExpense();
        companyMgt CompanyMgt = new companyMgt();
        DBackup dbBackup = new DBackup();
        FinancialMgt financialMgt = new FinancialMgt();
        reports rp = new reports();
        ProductList productsList = new ProductList();
        salesHistory salesHistory = new salesHistory();
        expHistory expenseHistory = new expHistory();
        productsSold ProductsSold = new productsSold();
        returnHistory ReturnHistory = new returnHistory();
        productsReturned productsReturned = new productsReturned();
        login lg = new login();
        changePw ChangePassword = new changePw();

        salesTrend SalesTrend = new salesTrend();


        RolesMgt rolesMgt = new RolesMgt();
        userMgt UserMgt = new userMgt();

        public static int userId = new int();
        public static int roleid = new int();
        public static int status = new int();
        public static TextBox user = new TextBox();
        // int roleid;
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            
            lg.ShowDialog();
            statusBar();
            RolePermission();

        }
        private void deActivate()
        {
            mnuProductType.Enabled = false;
            mnuAddProduct.Enabled = false;
            mnuSales.Enabled = false;
            mnuPartPayment.Enabled = false;
            mnuSalesReturn.Enabled = false;
            mnuExpenseType.Enabled = false;
            mnuPostExpense.Enabled = false;
            mnuCompany.Enabled = false;
            mnuRoles.Enabled = false;
            mnuUsers.Enabled = false;
            mnuDatabaseBackup.Enabled = false;
            mnuFinancialStatus.Enabled = false;
            mnuProductsList.Enabled = false;
            mnuSalesHistory.Enabled = false;
            mnuExpenseHistory.Enabled = false;
            mnuProductSold.Enabled = false;
            mnuSalesReturnHistory.Enabled = false;
            mnuProductsReturned.Enabled = false;
        }
        private void RolePermission()
        {
            try
            {
                if(status == 1)
                {
                    var query = from p in ctx.tblRoles
                                where p.Id == roleid
                                select p;

                    if (query.Count() > 0)
                    {
                        List<tblRole> post = query.ToList();
                        foreach (var x in post)
                        {
                            mnuProductType.Enabled = (x.Role1 == 1 ? true : false);
                            mnuAddProduct.Enabled = (x.Role2 == 1 ? true : false);
                            mnuSales.Enabled = (x.Role3 == 1 ? true : false);
                            mnuPartPayment.Enabled = (x.Role4 == 1 ? true : false);
                            mnuSalesReturn.Enabled = (x.Role5 == 1 ? true : false);
                            mnuExpenseType.Enabled = (x.Role6 == 1 ? true : false);
                            mnuPostExpense.Enabled = (x.Role7 == 1 ? true : false);
                            mnuCompany.Enabled = (x.Role8 == 1 ? true : false);
                            mnuRoles.Enabled = (x.Role9 == 1 ? true : false);
                            mnuUsers.Enabled = (x.Role10 == 1 ? true : false);
                            mnuDatabaseBackup.Enabled = (x.Role11 == 1 ? true : false);
                            mnuFinancialStatus.Enabled = (x.Role12 == 1 ? true : false);
                            mnuProductsList.Enabled = (x.Role13 == 1 ? true : false);
                            mnuSalesHistory.Enabled = (x.Role14 == 1 ? true : false);
                            mnuExpenseHistory.Enabled = (x.Role15 == 1 ? true : false);
                            mnuProductSold.Enabled = (x.Role16 == 1 ? true : false);
                            mnuSalesReturnHistory.Enabled = (x.Role17 == 1 ? true : false);
                            mnuProductsReturned.Enabled = (x.Role18 == 1 ? true : false);
                        }
                    }
                }
                else
                {
                   // MessageBox.Show("Test");
                    deActivate();
                }
            }
            catch (Exception)
            {
                throw;
                //lblError.Text = "Database error has occurred!";
                //lblError.ForeColor = Color.Yellow;
            }
        }

        private void statusBar()
        {
            string statusMsg = status > 0 ? "Active" : "Disabled";
            string machineName = System.Environment.MachineName;
            toolStripStatusLabel1.Text = "Computer Name:  " + machineName + "  |  ";
            toolStripStatusLabel2.Text = "Developed By:  ";
            toolStripStatusLabel3.Text = "De Royce Solutions  | ";
            toolStripStatusLabel4.Text = "Logged in User:  " + user.Text.ToUpper() + "  |  " + DateTime.Now + "  |  ";
            toolStripStatusLabel5.Text = "User Status:  " + statusMsg;
        }
        private void mnuProductType_Click(object sender, EventArgs e)
        {
            productType.ShowDialog();
        }

        private void mnuAddProduct_Click(object sender, EventArgs e)
        {
            productMgt.ShowDialog();
        }

        private void mnuSales_Click(object sender, EventArgs e)
        {
            salesMgt.ShowDialog();
        }

        private void mnuPartPayment_Click(object sender, EventArgs e)
        {
            PartPay.ShowDialog();
        }

        private void mnuSalesReturn_Click(object sender, EventArgs e)
        {
            salesReturn.ShowDialog();
        }

        private void mnuSalesReturnHistory_Click(object sender, EventArgs e)
        {
            ReturnHistory.ShowDialog();
        }

        private void mnuSalesHistory_Click(object sender, EventArgs e)
        {
            salesHistory.ShowDialog();
        }

        private void mnuExpenseType_Click(object sender, EventArgs e)
        {
            expenseType.ShowDialog();
        }

        private void mnuPostExpense_Click(object sender, EventArgs e)
        {
            PostExpense.ShowDialog();
        }

        private void mnuExpenseHistory_Click(object sender, EventArgs e)
        {
            expenseHistory.ShowDialog();
        }

        private void mnuProductsList_Click(object sender, EventArgs e)
        {
            productsList.ShowDialog();
        }

        private void mnuProductSold_Click(object sender, EventArgs e)
        {
            ProductsSold.ShowDialog();
        }

        private void mnuProductsReturned_Click(object sender, EventArgs e)
        {
            productsReturned.ShowDialog();
        }

        private void mnuFinancialStatus_Click(object sender, EventArgs e)
        {
            financialMgt.ShowDialog();
        }

        private void mnuUsers_Click(object sender, EventArgs e)
        {
            UserMgt.ShowDialog();
        }

        private void mnuRoles_Click(object sender, EventArgs e)
        {
            rolesMgt.ShowDialog();
        }

        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword.ShowDialog();
        }

        private void mnuCompany_Click(object sender, EventArgs e)
        {
            CompanyMgt.ShowDialog();
        }

        private void mnuDatabaseBackup_Click(object sender, EventArgs e)
        {
            dbBackup.ShowDialog();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesTrend.ShowDialog();
        }
    }
}
