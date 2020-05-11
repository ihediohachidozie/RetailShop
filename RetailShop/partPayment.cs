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
    public partial class partPayment : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int Id, prodId;
        decimal sum;
        string salesno, modelno, storeName, storeAddress, storePhone;
        Image storeImg;
        List<Company> store;
       // string salesRep;
        public partPayment()
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
                // Company store = ctx.Companies.FirstOrDefault(s => s.Id == 1);
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
        private void getSales()
        {
            try
            {
                sum = 0;

                SalesOrder so = ctx.SalesOrders.FirstOrDefault(s => s.Salesno == salesno);
                txtTotal.Text = (so.Total).ToString("n");
                txtDist.Text = so.Discount.ToString("n");
                Id = so.Id;
             //   MessageBox.Show(Id.ToString());
                var query = from s in ctx.SalesOrders
                            where s.Salesno == salesno
                            select s;


                if (query.Count() > 0)
                {
                    List<SalesOrder> sl = query.ToList();
                    sl.ForEach(x => sum += x.Amt_Tendered);
                }
                txtPaid.Text = sum.ToString("n");
                txtBal.Text = (decimal.Parse(txtTotal.Text) - decimal.Parse(txtPaid.Text)).ToString("n");
                btnPay.Text = "Pay " + txtBal.Text;
                btnPay.ForeColor = Color.Blue;
                label6.Text = "";
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

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
                    List<SoldItem> post = query.ToList();
                    foreach(var x in post)
                    {
                        dgvOrder.Rows.Add();
                        int rowCount = dgvOrder.Rows.Count - 1;
                        DataGridViewRow R = dgvOrder.Rows[rowCount];
                        R.Cells["Col1"].Value = x.Product.ModelNo;
                        R.Cells["Col2"].Value = x.Product.ProductName;
                        R.Cells["Col3"].Value = x.Product.Sale.ToString("n"); 
                        R.Cells["Col4"].Value = x.Quantity;
                        R.Cells["Col5"].Value = (x.Quantity * x.Product.Sale).ToString("n");
                    }
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }
        private void getPartSales()
        {
            try
            {
                lstOutSales.Items.Clear();

                var qua = from s in ctx.SoldItems
                          where s.Status == 0
                          group s by s.SalesId;

                if(qua.Count() > 0)
                {
                    foreach (var list in qua)
                    {
                        salesno = ctx.SalesOrders.First(s => s.Id == list.Key).Salesno;
                        lstOutSales.Items.Add(salesno);
                    }
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
            txtTendered.Text = "0.00";
            txtTotal.Clear();
            btnPay.Text = "Pay";
            dgvOrder.Rows.Clear();
            rdBank.Checked = rdCash.Checked = rdCheck.Checked = rdPOS.Checked = false;
        }
        private void saveSales()
        {
            try
            {
                SalesOrder sale = new SalesOrder()
                {
                    Salesno = salesno,
                    PaymentMode = (rdCash.Checked ? 1 : (rdPOS.Checked ? 2 : (rdCheck.Checked ? 3 : (rdBank.Checked ? 4 : 0)))),
                    Total = decimal.Parse(txtTotal.Text),
                    Discount = decimal.Parse(txtDist.Text),
                    Amt_Tendered = decimal.Parse(txtTendered.Text),
                    Createdby = Form1.userId,
                    Createdon = System.DateTime.Now
                };
                ctx.SalesOrders.Add(sale);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }


        }

        private void updateSoldItemsStatus()
        {
            try
            {
                foreach (DataGridViewRow x in dgvOrder.Rows)
                {
                    int n = x.Index;

                    modelno = dgvOrder.Rows[n].Cells["Col1"].Value.ToString();
                    prodId = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno).Id;

                    // updating each products
                    Product product = ctx.Products.FirstOrDefault(p => p.Id == prodId);
                    product.Quantity -= int.Parse(dgvOrder.Rows[n].Cells["Col4"].Value.ToString());

                    // updating each sold item status ...
                    SoldItem sitem = ctx.SoldItems.FirstOrDefault(s => s.SalesId == Id && s.Status == 0);
                    sitem.Status = 1;

                    ctx.SaveChanges();

                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            dgvOrder.Rows.Clear();
            try
            {
                if (txtFind.Text != "")
                {
                    salesno = txtFind.Text.ToUpper();
                    getSales();
                    getCustomer();
                    getSalesLines();

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
                label6.ForeColor = Color.Yellow;
            }

            
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int pmt = (rdCash.Checked ? 1 : (rdPOS.Checked ? 2 : (rdCheck.Checked ? 3 : (rdBank.Checked ? 4 : 0))));

            try
            {
                if (txtTendered.Text != "" && decimal.Parse(txtTendered.Text) > 0 )
                {
                    if(decimal.Parse(txtBal.Text) != 0)
                    {
                        if(decimal.Parse(txtTendered.Text) <= decimal.Parse(txtBal.Text))
                        {
                            if (pmt != 0)
                            {
                                // save sales ...
                                saveSales();

                                if (decimal.Parse(txtTendered.Text) == decimal.Parse(txtBal.Text))
                                {
                                    // update product quantity & sold items ...
                                    updateSoldItemsStatus();
                                }
                                label6.Text = "Sales Order Payment made Successfully!";
                                label6.ForeColor = Color.Lime;
                                printReceipt();
                                clearFields();
                                getPartSales();
                            }
                            else
                            {
                                label6.Text = "Select Payment Mode!";
                            }
                        }
                        else
                        {
                            label6.Text = "Amount tenderd is more than outstanding balance!";
                        }
                    }
                    else
                    {
                        label6.Text = "There is no outstanding payment for this sales!";
                    }

                }
                else
                {
                    label6.Text = "Amount tendered cannot be zero or empty!";
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

                txtTendered.Text = decimal.Parse(txtTendered.Text).ToString("n");
                if (decimal.Parse(txtTendered.Text) > decimal.Parse(txtBal.Text))
                {
                    //rdBank.Checked = rdCash.Checked = rdCheck.Checked = rdPOS.Checked = false;
                    btnPay.Enabled = false;
                    label6.Text = "Amount tendered is more than the outstanding!";

                }
                else
                {
                    btnPay.Enabled = true;
                    label6.Text = "";
                }
            }
            catch (Exception)
            {

                label6.Text = "New Sales!";
            }
        }

        private void lstOutSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstOutSales.SelectedIndex != -1)
                {
                    dgvOrder.Rows.Clear();
                    salesno = lstOutSales.SelectedItem.ToString();
                    //MessageBox.Show(salesno);
                    getSales();
                    getCustomer();
                    getSalesLines();
                    
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
                label6.ForeColor = Color.Yellow;
            }

        }

        private void partPayment_Load(object sender, EventArgs e)
        {
            txtFind.Clear();
            dgvOrder.Rows.Clear();
            loadData(); 
            getPartSales();
            clearFields();
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
                    string val1 = dgvOrder.Rows[n].Cells["Col1"].Value.ToString();
                    string val2 = dgvOrder.Rows[n].Cells["Col2"].Value.ToString();
                    string val4 = dgvOrder.Rows[n].Cells["Col4"].Value.ToString();
                    string val3 = dgvOrder.Rows[n].Cells["Col3"].Value.ToString();
                    string val5 = dgvOrder.Rows[n].Cells["Col5"].Value.ToString();
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

                graphic.DrawString("Net Total ".PadRight(20) + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", decimal.Parse(txtTotal.Text)), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 530, 555);

                offset = offset + 30; //make some room so that the total stands out.

                // payment details
                string bals = "Balance: " + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", decimal.Parse(txtBal.Text) - decimal.Parse(txtTendered.Text));

                graphic.DrawString("Payment History", font1, new SolidBrush(Color.Black), 50, 600);
                int k = 620;
                var query = from s in ctx.SalesOrders
                            where s.Salesno == salesno
                            select s;

                if (query.Count() > 0)
                {
                    List<SalesOrder> sl = query.ToList();
                    //    // part payment History ...
  
                    foreach(var x in sl)
                    {
                        graphic.DrawString(string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", x.Amt_Tendered) + " - ".PadRight(5) + x.Createdon, font1, new SolidBrush(Color.Black), 50, k);
                        k += 20;
                    }
                    graphic.DrawString(bals, font1, new SolidBrush(Color.Black), 50, k);
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
    }
}
