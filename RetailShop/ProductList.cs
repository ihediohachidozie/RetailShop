using System;
using DGVPrinterHelper;
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
    public partial class ProductList : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int stockCount;

        private void rdNil_CheckedChanged(object sender, EventArgs e)
        {
            nilProduct();
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            allProduct();
        }
        private void clearall()
        {           
            rdAll.Checked = false;
            rdNil.Checked = false;
            btnPrint.Focus();
            dgvProduct.Rows.Clear();
            lblMsg.Text = "";
        }

        public ProductList()
        {
            InitializeComponent();
        }
        private void allProduct()
        {
            try
            {
                dgvProduct.Rows.Clear();
                //display all products;
                stockCount = 0;

                var query = from p in ctx.Products
                            select p;

                if (query.Count() > 0)
                {
                    List<Product> post = query.ToList();
                    foreach (var x in post)
                    {
                        dgvProduct.Rows.Add();
                        int rowCount = dgvProduct.Rows.Count - 1;
                        DataGridViewRow R = dgvProduct.Rows[rowCount];
                        R.Cells["Col1"].Value = ++stockCount;
                        R.Cells["Col2"].Value = x.ModelNo;
                        R.Cells["Col3"].Value = x.ProductName;
                        R.Cells["Col4"].Value = x.Quantity;
                        R.Cells["Col5"].Value = x.Cost.ToString("n");
                        R.Cells["Col6"].Value = x.Sale.ToString("n");
                        R.Cells["Col7"].Value = x.CreatedOn.Date.ToShortDateString();
                    }
                }
                lblMsg.Text = (query.Count() > 0 ? query.Count().ToString() + " Products ..." : query.Count().ToString() + " Product ...");
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
            }

        }
        private void nilProduct()
        {
            try
            {
                dgvProduct.Rows.Clear();
                //display nil products;
                stockCount = 0;

                var query = from p in ctx.Products
                            where p.Quantity == 0
                            select p;

                if (query.Count() > 0)
                {
                    List<Product> post = query.ToList();
                    foreach (var x in post)
                    {

                        dgvProduct.Rows.Add();
                        int rowCount = dgvProduct.Rows.Count - 1;
                        DataGridViewRow R = dgvProduct.Rows[rowCount];
                        R.Cells["Col1"].Value = ++stockCount;
                        R.Cells["Col2"].Value = x.ModelNo;
                        R.Cells["Col3"].Value = x.ProductName;
                        R.Cells["Col4"].Value = x.Quantity;
                        R.Cells["Col5"].Value = x.Cost.ToString("n");
                        R.Cells["Col6"].Value = x.Sale.ToString("n");
                        R.Cells["Col7"].Value = x.CreatedOn.Date.ToShortDateString();
                    }
                }
                lblMsg.Text = (query.Count() > 0 ? query.Count().ToString() + " Products ..." : query.Count().ToString() + " Product ...");
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
            }
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            clearall();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count > 0)
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Products Report"; // Header
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
                lblMsg.Text = "No rows to Print!";
            }
        }
    }
}
