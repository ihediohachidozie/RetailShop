using DGVPrinterHelper;
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
    public partial class productsSold : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int count;
        public productsSold()
        {
            InitializeComponent();
        }

        private void getAll()
        {
            dgvProduct.Rows.Clear();
            count = 0;
            try
            {
                var query = from p in ctx.SoldItems
                            select p;

                if(query.Count() > 0)
                {
                    List<SoldItem> post = query.ToList();

                    if (rdAll.Checked)
                    {
                        foreach (var x in post)
                        {
                            dgvProduct.Rows.Add();
                            int rowCount = dgvProduct.Rows.Count - 1;
                            DataGridViewRow R = dgvProduct.Rows[rowCount];
                            R.Cells["Col1"].Value = ++count;
                            R.Cells["Col2"].Value = x.SalesOrder.Salesno;
                            R.Cells["Col3"].Value = x.Product.ProductName + " - " + x.Product.ModelNo;
                            R.Cells["Col4"].Value = x.Quantity;
                            R.Cells["Col5"].Value = x.SalesOrder.Createdon;
                            R.Cells["Col6"].Value = (x.Status == 1 ? "Full Sale" : "Part-Sale");
                        }
                    }
                    else if (rdDaily.Checked)
                    {
                        foreach (var x in post)
                        {
                            if (x.SalesOrder.Createdon.Date.Equals(dateTimePicker1.Value.Date))
                            {
                                dgvProduct.Rows.Add();
                                int rowCount = dgvProduct.Rows.Count - 1;
                                DataGridViewRow R = dgvProduct.Rows[rowCount];
                                R.Cells["Col1"].Value = ++count;
                                R.Cells["Col2"].Value = x.SalesOrder.Salesno;
                                R.Cells["Col3"].Value = x.Product.ProductName + " - " + x.Product.ModelNo;
                                R.Cells["Col4"].Value = x.Quantity;
                                R.Cells["Col5"].Value = x.SalesOrder.Createdon;
                                R.Cells["Col6"].Value = (x.Status == 1 ? "Full Sale" : "Part-Sale");
                            }

                        }
                    }
                    else if (rdRange.Checked)
                    {
                        foreach (var x in post)
                        {
                            if (x.SalesOrder.Createdon.Date >= dateTimePicker2.Value.Date && x.SalesOrder.Createdon.Date <= dateTimePicker3.Value.Date)
                            {
                                dgvProduct.Rows.Add();
                                int rowCount = dgvProduct.Rows.Count - 1;
                                DataGridViewRow R = dgvProduct.Rows[rowCount];
                                R.Cells["Col1"].Value = ++count;
                                R.Cells["Col2"].Value = x.SalesOrder.Salesno;
                                R.Cells["Col3"].Value = x.Product.ProductName + " - " + x.Product.ModelNo;
                                R.Cells["Col4"].Value = x.Quantity;
                                R.Cells["Col5"].Value = x.SalesOrder.Createdon;
                                R.Cells["Col6"].Value = (x.Status == 1 ? "Full Sale" : "Part-Sale");
                            }

                        }
                    }

                }
                label6.Text = (count > 1 ? count.ToString() + " Records" : count.ToString() + " Record");
            }
            catch (Exception)
            {

                label6.Text = "Database error has occurred!";
            }
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            getAll();
            dateTimePicker3.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker1.ResetText();
            btnFind.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count > 0)
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Products Sold Report"; // Header
                printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "**** De Royce Solution ****"; //Footer
                printer.FooterSpacing = 15;
                printer.PrintDataGridView(dgvProduct);
            }
            else
            {
                label6.Text = "No records to Print!";
            }
        }

        private void rdDaily_CheckedChanged(object sender, EventArgs e)
        {
            dgvProduct.Rows.Clear();
            dateTimePicker3.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker1.ResetText();
            btnFind.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker1.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getAll();
        }

        private void rdRange_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker3.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker1.ResetText();
            btnFind.Enabled = true;
            dateTimePicker2.Enabled = true;
            dateTimePicker3.Enabled = true;
            dateTimePicker1.Enabled = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            getAll();
        }

        private void productsSold_Load(object sender, EventArgs e)
        {
            clearall();
        }
        private void clearall()
        {
            label6.Text = "";
            dateTimePicker3.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker1.ResetText();
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker1.Enabled = false;
            btnFind.Enabled = false;
            rdAll.Checked = false;
            rdDaily.Checked = false;
            rdRange.Checked = false;
            btnPrint.Focus();
            dgvProduct.Rows.Clear();
        }
    }
}
