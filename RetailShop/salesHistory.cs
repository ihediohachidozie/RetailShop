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
    public partial class salesHistory : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int Id;
        decimal sum;
        string salesno, storeName, storeAddress, storePhone, salesRep;
        Image storeImg;
        List<Company> store;
        public salesHistory()
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

        private void salesHistory_Load(object sender, EventArgs e)
        {
            clearFields();
            
            loadData();
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            btnFind.Enabled = false;
            rdAll.Checked = false;
            rdDaily.Checked = false;
            rdRange.Checked = false;

            getSalesList();
        }

        private void lstSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearFields();
                label6.Text = "";
                if (lstSales.SelectedIndex != -1)
                {
                    dgvOrder.Rows.Clear();
                    salesno = lstSales.SelectedItem.ToString().Substring(0,10);

                    getSales();
                    getCustomer();
                    getSalesLines();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                label6.Text = "An Error has occurred!";
                clearFields();
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
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            printReceipt();
            clearFields();
        }

        private void rdDaily_CheckedChanged(object sender, EventArgs e)
        {
            lstSales.Items.Clear();
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            btnFind.Enabled = false;
        }

        private void rdRange_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker1.ResetText();
            dateTimePicker2.Enabled = true;
            dateTimePicker3.Enabled = true;
            btnFind.Enabled = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                lstSales.Items.Clear();

                var query = (from s in ctx.SalesOrders
                             select s).Distinct();

                if (query.Count() > 0)
                {
                    List<SalesOrder> post = query.ToList();

                    foreach (var x in post)
                    {
                        if (x.Createdon.Date >= dateTimePicker2.Value.Date && x.Createdon.Date <= dateTimePicker3.Value.Date)
                        {
                            lstSales.Items.Add(x.Salesno + " : " + x.Createdon.ToShortDateString() + " : " + x.Amt_Tendered.ToString("n"));
                        }
                    }
                }
            }
            catch (Exception)
            {

                label6.Text = "No Sales available!";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lstSales.Items.Clear();

                var query = (from s in ctx.SalesOrders
                                 // where s.Createdon == dateTimePicker1.Value
                             select s).Distinct();

                if (query.Count() > 0)
                {
                    List<SalesOrder> post = query.ToList();

                    //sl.ForEach(x => lstSales.Items.Add(x.Salesno + " : " + x.Createdon.ToShortDateString()));
                    foreach (var x in post)
                    {
                        if (x.Createdon.Date.Equals(dateTimePicker1.Value.Date))
                        {
                            lstSales.Items.Add(x.Salesno + " : " + x.Createdon.ToShortDateString() +" : "+x.Amt_Tendered.ToString("n"));
                        }
                    }
                    //label6.Text = "No Sales available!";
                }
            }
            catch (Exception)
            {

                label6.Text = "No Sales available!";
            }
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            
            dateTimePicker1.Enabled = false;
            dateTimePicker1.ResetText();
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            btnFind.Enabled = false;
            getSalesList();
        }

        private void getSales()
        {
            try
            {
                sum = 0;

                SalesOrder so = ctx.SalesOrders.FirstOrDefault(s => s.Salesno == salesno);
                txtTotal.Text = so.Total.ToString("n");
                txtDist.Text = so.Discount.ToString("n");
                Id = so.Id;
                rdCash.Checked = (so.PaymentMode == 1 ? true : false);
                rdPOS.Checked = (so.PaymentMode == 2 ? true : false);
                rdBank.Checked = (so.PaymentMode == 4 ? true : false);
                rdCheck.Checked = (so.PaymentMode == 3 ? true : false);

                salesRep = ctx.Users.FirstOrDefault(u => u.Id == so.Createdby).Username;

                var query = from s in ctx.SalesOrders
                            where s.Salesno == salesno
                            //group s by s.Salesno
                            select s;

                if (query.Count() > 0)
                {
                    List<SalesOrder> sl = query.ToList();
                    sl.ForEach(x => sum += x.Amt_Tendered);
                }
                txtPaid.Text = sum.ToString("n");
                txtBal.Text = (decimal.Parse(txtTotal.Text) - decimal.Parse(txtPaid.Text)).ToString("n");

            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
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
            }
        }

        private void clearFields()
        {
            txtAddress.Clear();
            txtBal.Clear();
            txtCustomer.Clear();
            txtDist.Clear();
            txtPaid.Clear();
            txtPhone.Clear();
            txtTotal.Clear();
            dgvOrder.Rows.Clear();
            rdBank.Checked = rdCash.Checked = rdCheck.Checked = rdPOS.Checked = false;
        }

        private void getSalesList()
        {
            try
            {
                lstSales.Items.Clear();
                var query = (from s in ctx.SalesOrders
                             select s).Distinct();

                if (query.Count() > 0)
                {
                    List<SalesOrder> sl = query.ToList();
                    sl.ForEach(x => lstSales.Items.Add(x.Salesno + " : " + x.Createdon.ToShortDateString() + " : " + x.Amt_Tendered.ToString("n")));
                }
            }
            catch (Exception)
            {
                label6.Text = "Database error has occurred!";
            }
        }
        private void getDaily()
        {
            try
            {
                lstSales.Items.Clear();

                var query = (from s in ctx.SalesOrders
                             select s).Distinct();

                if (query.Count() > 0)
                {
                    List<SalesOrder> post = query.ToList();

                    foreach (var x in post)
                    {
                        if (x.Createdon.Date.Equals(dateTimePicker1.Value.Date))
                        {
                            lstSales.Items.Add(x.Salesno + " : " + x.Createdon.ToShortDateString() + " : " + x.Amt_Tendered.ToString("n"));
                        }
                    }
                }
            }
            catch (Exception)
            {

                label6.Text = "No Sales available!";
            }
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
                graphic.DrawString("Sales Rep:    " + salesRep, font, black, 480, 170 + offset);
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

                graphic.DrawString("Net Total ".PadRight(20) + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", decimal.Parse(txtTotal.Text)), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 530, 555);

                offset = offset + 30; //make some room so that the total stands out.


                string bals = "Balance: " + string.Format(CultureInfo.CreateSpecificCulture("HA-LATN-NG"), "{0:C}", decimal.Parse(txtBal.Text));
                int k = 620;
                var query = from s in ctx.SalesOrders
                            where s.Salesno == salesno
                            select s;

                if (query.Count() > 1)
                {
                    List<SalesOrder> sl = query.ToList();
                    //    // part payment History ...
                    // payment details
                    graphic.DrawString("Payment History", font1, new SolidBrush(Color.Black), 50, 600);
                    foreach (var x in sl)
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
