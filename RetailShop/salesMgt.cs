using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailShop
{
    public partial class salesMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();

        int prodId = 0;
        List<Company> store;
        int d,m,y,lastSalesID, qty, count = 0;
        string modelno, salesno, ym, storeName, storeAddress, storePhone;
        Image storeImg;
        decimal total = 0;
        DateTime thedate = System.DateTime.Now;
      //  string salesRep;
        public salesMgt()
        {
            InitializeComponent();
        }
        private void loadProdType()
        {
            try
            {
                cboType2.Items.Clear();

                var query = from prod in ctx.ProductTypes
                            select prod;
                if (query.Count() > 0)
                {
                    List<ProductType> prodname = query.ToList();
                    cboType2.Items.Add("All");
                    prodname.ForEach(x => cboType2.Items.Add(x.Name));
                }
            //    salesRep = ctx.Users.FirstOrDefault(u => u.Id == Form1.userId).Username;
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }
        Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void loadData()
        {
            try
            {
                store = ctx.Companies.ToList();
                if (store.Count() > 0)
                {
                    foreach (var x in store)
                    {
                        storeName = x.Name;
                        storeAddress = x.Address;
                        storePhone = x.Telephone;
                        storeImg = ConvertBinaryToImage(x.Logo);

                    }
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }
        private void loadProducts()
        {
            try
            {
                lstProduct.Items.Clear();

                var query = from p in ctx.Products
                            select p;
                if (query.Count() > 0)
                {
                    List<Product> product = query.ToList();
                    product.ForEach(x => lstProduct.Items.Add(x.ModelNo + " : " + x.ProductName));
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }
        private void updateBalance()
        {
            // update total balance
            total = 0;
            foreach (DataGridViewRow item in dgvOrder.Rows)
            {
                int n = item.Index;
                total = (total + decimal.Parse(dgvOrder.Rows[n].Cells["amount"].Value.ToString()));
            }
            count = 0;
            btnPay.Text = "Pay " + total.ToString("n");
            btnPay.ForeColor = Color.Blue;
        }
        private void updateQty()
        {
            // increase product qty if already exits in order list
            if(dgvOrder.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dgvOrder.Rows)
                {
                    int n = item.Index;
                    if (modelno == dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString())
                    {
                        count++;
                    }
                }
            }

        }
        private void generateSalesno()
        {
            d = thedate.Day;
            m = thedate.Month;
          //  mth = m < 10 ? m.ToString() : "0"+ m.ToString();
            y = int.Parse(thedate.Year.ToString().Substring(2, 2));
            ym = (y.ToString() + (m >= 10 ? m.ToString() : "0" + m.ToString()));

            try
            {
              //  salesno = ctx.SalesOrders.OrderByDescending(s => s.Id).FirstOrDefault().Salesno;
                salesno = ctx.SalesOrders.OrderByDescending(s => s.Salesno).FirstOrDefault().Salesno;
                if (ym == salesno.Substring(2, 4))
                {
                    salesno = "SI" + (int.Parse(salesno.Substring(2, 8)) + 1).ToString();
                }
                else
                {
                    salesno = "SI" + ym + "0001";
                }
            }
            catch (Exception)
            {

                salesno = "SI" + ym + "0001";
            }
        }
        private void saveSales()
        {
            try
            {
                SalesOrder sale = new SalesOrder()
                {
                    Salesno = salesno,
                    PaymentMode = (rdCash.Checked ? 1 : (rdPOS.Checked ? 2 : (rdCheck.Checked ? 3 : (rdBank.Checked ? 4 : 0)))),
                    Total = total,
                    Discount = (txtDist.Text != "" ? decimal.Parse(txtDist.Text) : 0),
                    Amt_Tendered = decimal.Parse(txtTendered.Text),
                    Createdby = Form1.userId,
                    Createdon = System.DateTime.Now
                };
                ctx.SalesOrders.Add(sale);
                ctx.SaveChanges();
                lastSalesID = ctx.SalesOrders.OrderByDescending(s => s.Id).FirstOrDefault().Id;
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }
        private void saveCustomer()
        {
            try
            {
                Customer cust = new Customer()
                {
                    Name = txtCustomer.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    SalesId = lastSalesID
                };
                ctx.Customers.Add(cust);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }     
        private void saveSoldItems()
        {
            try
            {
                foreach (DataGridViewRow x in dgvOrder.Rows)
                {
                    int n = x.Index;
                    modelno = dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString();
                    prodId = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno).Id;

                    // adding each item sold in the salesitem table
                    SoldItem item = new SoldItem()
                    {
                        SalesId = lastSalesID,
                        ProductId = prodId,
                        Quantity = int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString()),
                        Status = (total == decimal.Parse(txtTendered.Text) ? 1 : 0)

                    };
                    ctx.SoldItems.Add(item);

                    // updating each products

                    if (total == decimal.Parse(txtTendered.Text))
                    {
                        Product product = ctx.Products.FirstOrDefault(p => p.Id == prodId);
                        product.Quantity -= int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString());
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

        private void cboType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboType2.SelectedIndex != -1 && cboType2.SelectedItem.ToString() != "All")
                {
                    lstProduct.Items.Clear();

                    int prodType = ctx.ProductTypes.FirstOrDefault(a => a.Name == cboType2.SelectedItem.ToString()).Id;

                    var query = from p in ctx.Products
                                where p.ProdTypeId == prodType
                                select p;
                    if (query.Count() > 0)
                    {
                        List<Product> product = query.ToList();
                        product.ForEach(x => lstProduct.Items.Add(x.ModelNo + " : " + x.ProductName));
                    }

                }
                else
                {
                    loadProducts();
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }

        private void txtDist_Leave(object sender, EventArgs e)
        {
            try
            {
                updateBalance();
                if (MessageBox.Show("Are you giving a discount for this sales ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtBal.Text = (total - (decimal.Parse(txtTendered.Text) + decimal.Parse(txtDist.Text))).ToString("n");
                    total -= decimal.Parse(txtDist.Text);
                    btnPay.Text = "Pay : " + total.ToString("n");
                    btnPay.ForeColor = Color.Blue;

                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void salesMgt_Load(object sender, EventArgs e)
        {
            loadProdType();
            loadProducts();
            
            clearContents();
            loadData();
        }

        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "";
                qty = 0;
                if (lstProduct.SelectedIndex != -1)
                {
                    int marker = lstProduct.SelectedItem.ToString().IndexOf(":") - 1;
                    modelno = lstProduct.SelectedItem.ToString().Substring(0, marker);

                    Product product = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno);
                    qty = product.Quantity;
                    if(product.Quantity > 0)
                    {
                        updateQty();

                        if (count > 0)
                        {
                            foreach (DataGridViewRow item in dgvOrder.Rows)
                            {
                                int n = item.Index;
                                if (modelno == dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString())
                                {
                                    if(qty > int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString()))
                                    {
                                        dgvOrder.Rows[n].Cells["quantity"].Value = int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString()) + 1;
                                        dgvOrder.Rows[n].Cells["amount"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString())).ToString("n");

                                    }
                                    else
                                    {
                                        label6.Text = "Product out of stock!";
                                        label6.ForeColor = Color.Yellow;
                                    }
                                }
                            }
                        }
                        else
                        {
                            salesListBindingSource.Add(new salesList()
                            {
                                modelNo = product.ModelNo,
                                productName = product.ProductName,
                                Quantity = 1,
                                Price = product.Sale.ToString("n"),
                                Amount = product.Sale.ToString("n")
                            });
                        }
                        updateBalance();
                    }
                    else
                    {
                        label6.Text = "Product out of stock!";
                        label6.ForeColor = Color.Yellow;
                    }

                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }

        private void txtTendered_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBal.Text = (total - decimal.Parse(txtTendered.Text) - decimal.Parse(txtDist.Text)).ToString("n");
                txtTendered.Text = decimal.Parse(txtTendered.Text).ToString("n");
            }
            catch (Exception)
            {

                label6.Text = "New Sales!";
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrder.Columns[e.ColumnIndex].Name == "Remove")
            {
                if (MessageBox.Show("Are you sure want to remove this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    salesListBindingSource.RemoveCurrent();
            }
            updateBalance();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                int pmt = (rdCash.Checked ? 1 : (rdPOS.Checked ? 2 : (rdCheck.Checked ? 3 : (rdBank.Checked ? 4 : 0))));
                if (txtCustomer.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
                {
                    if (dgvOrder.Rows.Count > 0)
                    {
                        if (txtTendered.Text != "" && decimal.Parse(txtTendered.Text) > 0)
                        {
                            if(decimal.Parse(txtTendered.Text) <= total)
                            {
                                if (pmt != 0)
                                {
                                    // generate sales id
                                    generateSalesno();
                                    // save sales details
                                    saveSales();
                                    // save customer details
                                    saveCustomer();
                                    // save sold items & update products qty 
                                    saveSoldItems();

                                    label6.Text = "Sales Order Saved Successfully!";
                                    label6.ForeColor = Color.Lime;

                                    // print receipt
                                    printReceipt();

                                    clearContents();
                                }
                                else
                                {
                                    label6.Text = "Select Payment Mode!";
                                }
                            }
                            else
                            {
                                label6.Text = "Amount Tendered is more than the total sales!";
                            }
                        }
                        else
                        {
                            label6.Text = "Amount tendered cannot be zero or empty!";
                        }
                    }
                    else
                    {
                        label6.Text = "Sales Line is Empty!";
                    }
                }
                else
                {
                    label6.Text = "Customer Details has not been Completed!";
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }

        private void dgvOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            qty = 0;
            label6.Text = "";
            foreach (DataGridViewRow item in dgvOrder.Rows)
            {
                int n = item.Index;
                modelno = dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString();
                Product product = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno);
                qty = product.Quantity;

                int qty_Old = int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString());
                if(qty >= qty_Old)
                {
                    if (qty_Old > 0)
                    {
                        dgvOrder.Rows[n].Cells["amount"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString())).ToString("n");
                    }
                    else
                    {
                        dgvOrder.Rows[n].Cells["quantity"].Value = 1;
                        dgvOrder.Rows[n].Cells["amount"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString())).ToString("n");
                        label6.Text = "Quantity cannot be set to zero value!";
                        label6.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    dgvOrder.Rows[n].Cells["quantity"].Value = 1;
                    dgvOrder.Rows[n].Cells["amount"].Value = (decimal.Parse(dgvOrder.Rows[n].Cells["price"].Value.ToString()) * int.Parse(dgvOrder.Rows[n].Cells["quantity"].Value.ToString())).ToString("n");
                    label6.Text = "Quantity more than stock!";
                    label6.ForeColor = Color.Yellow;
                }

            }
            updateBalance();
        }
        private void printReceipt()
        {
            PrintDialog printDialog = new PrintDialog();

            PrintDocument printDocument = new PrintDocument();

            printDialog.Document = printDocument; //add the document to the dialog box...        

            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage); //add an event handler that will do the printing

            //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

            //Open the print preview dialog
            PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            objPPdialog.Document = printDocument;
            objPPdialog.ShowDialog();
        }
        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //this prints the reciept
                SolidBrush black = new SolidBrush(Color.Black);
                Font text = new Font("Courier New", 22, FontStyle.Bold);
                Graphics graphic = e.Graphics;

                Font font = new Font("Arial", 12); //must use a mono spaced font as the spaces need to line up
                Font font1 = new Font("Arial", 11);
                Font note1 = new Font("Arial", 10, FontStyle.Italic);
                Graphics surface = this.CreateGraphics();
                Pen pen1 = new Pen(Color.Black, 1.0f);
                Pen pen2 = new Pen(Color.Black, 1.0f);

                graphic.DrawLine(pen1, 50, 260, 750, 260); // horizontal line

                graphic.DrawLine(pen2, 50, 290, 750, 290); // horizontal line

                graphic.DrawLine(pen2, 50, 550, 750, 550); // horizontal lines

                graphic.DrawLine(pen1, 50, 580, 750, 580); // horizontal lines

                // logo to print
                Point loc = new Point(50, 20);
                graphic.DrawImage(storeImg, loc);



                float fontHeight = font.GetHeight();

                //int startX = 20;
                int startY = 20;
                int offset = 40;
                //int x = 40;
                int y = 300;

                string h1 = "Model No";
                string h2 = "Product";
                string h3 = "Price";
                string h4 = "Qty";
                string h5 = "Amount";


                graphic.DrawString(storeName, text, black, 400, startY + 20);
                graphic.DrawString(storeAddress + "  " + storePhone, new Font("Arial", 12), black, 400, startY + 10 + offset);

                graphic.DrawString("To:", new Font("Arial", 11, FontStyle.Bold), black, 50, 120 + offset);
                graphic.DrawString("Name:     " + txtCustomer.Text, font, black, 50, 145 + offset);
                graphic.DrawString("Address: " + txtAddress.Text, font, black, 50, 170 + offset);
                graphic.DrawString("Phone:    " + txtPhone.Text, font, black, 50, 195 + offset);

                graphic.DrawString("Date:             " + System.DateTime.Now.ToString(), font, black, 480, 145 + offset);
                graphic.DrawString("Sales Rep:    " + Form1.user.Text, font, black, 480, 170 + offset);
                graphic.DrawString("Receipt No.: " + salesno, font, black, 480, 195 + offset);

                graphic.DrawString(h1, new Font("Arial", 12, FontStyle.Bold), black, 50, 265);
                graphic.DrawString(h2, new Font("Arial", 12, FontStyle.Bold), black, 200, 265);
                graphic.DrawString(h3, new Font("Arial", 12, FontStyle.Bold), black, 530, 265);
                graphic.DrawString(h4, new Font("Arial", 12, FontStyle.Bold), black, 620, 265);
                graphic.DrawString(h5, new Font("Arial", 12, FontStyle.Bold), black, 675, 265);

                offset = offset + 60;

                foreach (DataGridViewRow item in dgvOrder.Rows)
                {
                    int n = item.Index;
                    string val1 = dgvOrder.Rows[n].Cells["modelNo1"].Value.ToString();
                    string val2 = dgvOrder.Rows[n].Cells["product"].Value.ToString();
                    string val3 = dgvOrder.Rows[n].Cells["price"].Value.ToString();
                    string val4 = dgvOrder.Rows[n].Cells["quantity"].Value.ToString();
                    string val5 = dgvOrder.Rows[n].Cells["amount"].Value.ToString();
                    string all = val1.PadRight(21) + val2.PadRight(47) + val3.PadRight(17) + val4.PadRight(7) + val5.PadRight(1);
                    //   graphic.DrawString(all, font, black, startX + x, startY + y + offset);
                    graphic.DrawString(val1, font, black, 50, y);
                    graphic.DrawString(val2, font, black, 200, y);
                    graphic.DrawString(val3, font, black, 520, y);
                    graphic.DrawString(val4, font, black, 630, y);
                    graphic.DrawString(val5, font, black, 665, y);
                    offset = offset + (int)fontHeight + 5; //make the spacing consistent
                    y = y + (int)fontHeight + 5;
                }
                graphic.DrawString("Discount", font, black, 200, y);
                decimal p = decimal.Parse(txtDist.Text);
                graphic.DrawString("- " + p.ToString(), font, black, 665, y);
                //when we have drawn all of the items add the total


                offset = offset + 20; //make some room so that the total stands out.

                graphic.DrawString("Net Total ".PadRight(20) + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", total), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 530, 555);

                offset = offset + 30; //make some room so that the total stands out.

                if(total > decimal.Parse(txtTendered.Text))
                {
                    // part payment highlighted ...
                    string pmt = "Amount Paid: " + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", decimal.Parse(txtTendered.Text));

                    string bals = "Balance: " + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", total - decimal.Parse(txtTendered.Text));
                    graphic.DrawString("Payment Details:", font1, new SolidBrush(Color.Black), 50, 610);

                    graphic.DrawString(pmt, font1, new SolidBrush(Color.Black), 50, 630);
                    graphic.DrawString(bals, font1, new SolidBrush(Color.Black), 50, 650);
                }

                graphic.DrawString("___________________________", font1, new SolidBrush(Color.Black), 80, 800);
                graphic.DrawString("   ( Sales Rep. Sign. )    ", note1, new SolidBrush(Color.Black), 80, 820);

                graphic.DrawString("___________________________", font1, new SolidBrush(Color.Black), 500, 800);
                graphic.DrawString("    ( Customer Sign. )   ", note1, new SolidBrush(Color.Black), 500, 820);


                graphic.DrawString("Thank you for your patronage, please come back again!", note1, new SolidBrush(Color.Black), 80, 1020);

                graphic.DrawString("NOTE: No Refund of Money After Payment ", note1, new SolidBrush(Color.Black), 80, 1040);
                graphic.DrawString("Please ensure that products are tested and supplied in good condition ", note1, new SolidBrush(Color.Black), 80, 1060);
                //            graphic.DrawString("For Installmental payment, only three (3) successive installments within two (2) months period is ALLOWED. ", note1, new SolidBrush(Color.Black), 80, 1080);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }

        }
        private void clearContents()
        {
            txtCustomer.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtBal.Clear();
            txtDist.Text = "0.00";
            txtTendered.Clear();
            dgvOrder.Rows.Clear();
            label6.Text = "New Sales Order!";
            label6.ForeColor = Color.Lime;
            rdBank.Checked = rdCash.Checked = rdCheck.Checked = rdPOS.Checked = false;
            btnPay.Text = "Pay";


        }
    }
}
