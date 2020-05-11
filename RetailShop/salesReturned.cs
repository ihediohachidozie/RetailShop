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
    public partial class salesReturned : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int Id, prodId, lastReturnID, RitemId, rowcount, qtyrt;
        decimal sum, total, dist;
        string salesno, modelno;
        public salesReturned()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dgvOrder.Rows.Clear();
            label6.Text = "";
            try
            {
                if (txtFind.Text != "")
                {
                    salesno = txtFind.Text.ToUpper();
                    getSales();
                    getCustomer();
                    getSalesLines();
                    foreach (DataGridViewRow item in dgvOrder.Rows)
                    {
                        int n = item.Index;
                        dgvOrder.Rows[n].Cells["qtyreturned"].Value = 0;
                        dgvOrder.Rows[n].Cells["amtreturned"].Value = 0;
                    }               
                 }
                else
                {
                    label6.Text = "Enter the Sales No!";
                }
            }
            catch (Exception)
            {
                clearFields();
                label6.Text = "Invalid Sales No entered!";
            }
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
                label6.ForeColor = Color.Yellow;
            }

        }

        private void dgvOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            qtyrt = 0;
            foreach (DataGridViewRow item in dgvOrder.Rows)
            {
                int n = item.Index;
                
                if (dgvOrder.Rows[n].Cells["qtyreturned"].Value != null)
                {
                    if (int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString()) <= int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString()))
                    {
                        qtyrt++;
                        dgvOrder.Rows[n].Cells["amtreturned"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString())).ToString("n");
                        label6.Text = "";
                    }
                    else
                    {
                        qtyrt = 0;
                        dgvOrder.Rows[n].Cells["qtyreturned"].Value = 0;
                        dgvOrder.Rows[n].Cells["amtreturned"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString())).ToString("n");
                        label6.Text = "Quantity entered is more than the quantity sold!";
                        label6.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    dgvOrder.Rows[n].Cells["qtyreturned"].Value = 0;
                    dgvOrder.Rows[n].Cells["amtreturned"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString())).ToString("n");
                    
                }
            }
            updateBalance();
        }

        private void getSales()
        {
            try
            {
                sum = 0;

                SalesOrder so = ctx.SalesOrders.FirstOrDefault(s => s.Salesno == salesno);
                txtTotal.Text = so.Total.ToString("n");
                txtDist.Text = so.Discount.ToString("n");
                dist = so.Discount;
                Id = so.Id;

                //salesRep = ctx.Users.FirstOrDefault(u => u.Id == Form1.userId).Username;

                var query = from s in ctx.SalesOrders
                            where s.Salesno == salesno
                            select s;


                if (query.Count() > 0)
                {
                    List<SalesOrder> sl = query.ToList();
                    sl.ForEach(x => sum += x.Amt_Tendered);
                }
                rowcount = query.Count();
                txtPaid.Text = sum.ToString("n");
                txtBal.Text = (decimal.Parse(txtTotal.Text) - decimal.Parse(txtPaid.Text)).ToString("n");
                //btnPay.Text = "Refund: " + txtBal.Text;
                //btnPay.ForeColor = Color.Blue;

                if ((from r in ctx.SalesReturns where r.SalesId == Id select r).Count() > 0)
                {
                    RitemId = ctx.SalesReturns.FirstOrDefault(r => r.SalesId == Id).Id;
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if((from s in ctx.SalesReturns where s.SalesId == Id select s).Count() == 0)
                {
                    if (decimal.Parse(txtBal.Text) == 0)
                    {
                        if (rdRefund.Checked || rdReplace.Checked)
                        {
                            if (qtyrt != 0)
                            {
                                ReturnSales();
                                ReturnItems();
                                label6.Text = "Product Returned Successfully!";
                                label6.ForeColor = Color.Lime;
                            }
                            else
                            {
                                label6.Text = "No Sales to return!";
                                label6.ForeColor = Color.Yellow;
                            }
                        }
                        else
                        {
                            label6.Text = "No Customer Request Selected!";
                            label6.ForeColor = Color.Yellow;
                        }
                    }
                    else
                    {
                        label6.Text = "There is still an outstanding payment left!";
                        label6.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    label6.Text = "This sales had already been returned!";
                    label6.ForeColor = Color.Yellow;
                }

            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }

        private void txtPercentage_Leave(object sender, EventArgs e)
        {
            //total -= total * (decimal.Parse(txtPercentage.Text) / 100);
            //btnPay.Text = "Refund:  " + total.ToString("n");
            //btnPay.ForeColor = Color.Blue;
           // rdRefund.Checked = false;
        }

        private void salesReturned_Load(object sender, EventArgs e)
        {
            clearFields();
        }

        private void rdRefund_CheckedChanged(object sender, EventArgs e)
        {
            updateBalance();

            total -= (total > 0 ? (dist > 0 ? (rowcount > 1) ? dist/rowcount : 0 : 0 ): 0);

            btnPay.Text = "Refund:  " + total.ToString("n");
            btnPay.ForeColor = Color.Blue;
            label6.Text = "";

        }

        private void rdReplace_CheckedChanged(object sender, EventArgs e)
        {
            updateBalance();
            total = 0;
            btnPay.Text = "Refund:  " + total.ToString("n");
            btnPay.ForeColor = Color.Blue;
            label6.Text = "";
        }

        private void getSalesLines()
        {
            try
            {
                var query = from s in ctx.SoldItems
                            where s.SalesId == Id
                            select s;

                if (query.Count() > 0)
                {
                    List<SoldItem> sm = query.ToList();
                    sm.ForEach(x =>
                    salesListBindingSource.Add(new salesList()
                    {
                        modelNo = x.Product.ModelNo,
                        productName = x.Product.ProductName,
                        Quantity = x.Quantity,
                        Price = x.Product.Sale.ToString("n"),
                        Amount = (x.Quantity * x.Product.Sale).ToString("n")

                    })
                    );
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }
        private void clearFields()
        {
            txtAddress.Clear();
            txtBal.Clear();
            txtCustomer.Clear();
            txtDist.Clear();
            txtFind.Clear();
            txtPaid.Clear();
            txtPhone.Clear();
            txtTotal.Clear();
            btnPay.Text = "Refund";
            dgvOrder.Rows.Clear();
            rdRefund.Checked = rdReplace.Checked = false;
        }
        private void updateBalance()
        {
            // update total balance
            total = 0;
            foreach (DataGridViewRow item in dgvOrder.Rows)
            {
                int n = item.Index;
                   total = (total + decimal.Parse(dgvOrder.Rows[n].Cells["amtreturned"].Value.ToString()));
            }
           // total -= decimal.Parse(txtDist.Text);
            total -= (total > 0) ? decimal.Parse(txtDist.Text) : 0;


            btnPay.Text = "Refund:  " + total.ToString("n");
            btnPay.ForeColor = Color.Blue;
        }
        private void ReturnSales()
        {
            try
            {
                SalesReturn post = new SalesReturn()
                {
                    SalesId = Id,
                    Percentage = 0,
                    TotalValue = total,
                    CreatedBy = Form1.userId,
                    CreatedOn = System.DateTime.Now,
                    RequestType = (rdRefund.Checked ? 1 : 2)
                };
                ctx.SalesReturns.Add(post);
                ctx.SaveChanges();

                lastReturnID = ctx.SalesReturns.OrderByDescending(s => s.Id).FirstOrDefault().Id;
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
 
        }
        private void ReturnItems()
        {
            try
            {
                foreach (DataGridViewRow x in dgvOrder.Rows)
                {
                    int n = x.Index;

                    if (int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString()) > 0)
                    {
                        modelno = dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString();
                        prodId = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno).Id;

                        ReturnedItem item = new ReturnedItem()
                        {
                            SalesReturnId = lastReturnID,
                            ProductId = prodId,
                            Quantity = int.Parse(dgvOrder.Rows[n].Cells["qtyreturned"].Value.ToString())
                        };
                        ctx.ReturnedItems.Add(item);

                        // updating each products


                        Product product = ctx.Products.FirstOrDefault(p => p.Id == prodId);
                        product.Quantity += (rdRefund.Checked ? int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString()) : 0);
                        // if refund product quantity will be updated but replace quantity unchanged because product will go in and out same time.
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }
    }
}
