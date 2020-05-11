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
    public partial class returnHistory : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        string salesno;
        int qtySold, custreq;
        decimal price;
        public returnHistory()
        {
            InitializeComponent();
        }
        private void getCustomer()
        {
            try
            {
                Customer cust = ctx.Customers.FirstOrDefault(c => c.SalesOrder.Salesno == salesno);
                txtCustomer.Text = cust.Name;
                txtAddress.Text = cust.Address;
                txtPhone.Text = cust.Phone;
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
            }

        }

        private void returnHistory_Load(object sender, EventArgs e)
        {
            getSalesReturn();
        }

        private void lstSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvOrder.Rows.Clear();
                if (lstSales.SelectedIndex != -1)
                {
                    salesno = lstSales.SelectedItem.ToString();
                    getCustomer();
                    getReturnItems();
                    txtTotal.Text = ctx.SalesReturns.FirstOrDefault(s => s.SalesOrder.Salesno == salesno).TotalValue.ToString("n");
                    custreq = ctx.SalesReturns.FirstOrDefault(s => s.SalesOrder.Salesno == salesno).RequestType;
                    rdRefund.Checked = (custreq == 1 ? true : false);
                    rdReplace.Checked = (custreq == 2 ? true : false);
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
            }

        }

        private void getSalesReturn()
        {
            try
            {
                lstSales.Items.Clear();
                var query = from s in ctx.SalesReturns
                            select s;

                if (query.Count() > 0)
                {
                    List<SalesReturn> post = query.ToList();
                    foreach (var x in post)
                    {
                        lstSales.Items.Add(x.SalesOrder.Salesno);
                    }
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
            }

        }

        private void getReturnItems()
        {
            try
            {
                var query = from p in ctx.ReturnedItems
                            where p.SalesReturn.SalesOrder.Salesno == salesno
                            select p;

                if (query.Count() > 0)
                {
                    List<ReturnedItem> post = query.ToList();
                    foreach (var x in post)
                    {
                        qtySold = 0;
                        price = 0;

                        qtySold = ctx.SoldItems.FirstOrDefault(s => s.SalesId == x.SalesReturn.SalesId).Quantity;
                        price = ctx.Products.FirstOrDefault(p => p.Id == x.ProductId).Sale;

                        dgvOrder.Rows.Add();
                        int rowCount = dgvOrder.Rows.Count - 1;
                        DataGridViewRow R = dgvOrder.Rows[rowCount];
                        R.Cells["Col1"].Value = x.Product.ProductName + " " + x.Product.ModelNo;
                        R.Cells["Col2"].Value = qtySold;
                        R.Cells["Col3"].Value = price.ToString("n");
                        R.Cells["Col4"].Value = (qtySold * price).ToString("n");
                        R.Cells["Col5"].Value = x.Quantity;
                        R.Cells["Col6"].Value = (price * x.Quantity).ToString("n");
                    }
                }

            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
            }
        }
    }
}
