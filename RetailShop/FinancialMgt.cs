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
    public partial class FinancialMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        decimal sales, exp, salesR, inventory, totalExp, totalBal;
        public FinancialMgt()
        {
            InitializeComponent();
        }

        private void loadSales()
        {
            try
            {
                if (rdAll.Checked)
                {
                    var query = from s in ctx.SalesOrders
                                select s.Amt_Tendered;
                    sales = (query.Count() > 0 ? query.Sum() : 0);
                }
                else if (rdDaily.Checked)
                {
                    var query = from s in ctx.SalesOrders
                                select s;
                    if (query.Count() > 0)
                    {
                        List<SalesOrder> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.Createdon.Date.Equals(dateTimePicker1.Value.Date))
                            {
                                sales += x.Amt_Tendered;
                            }
                        }
                    }

                }
                else if (rdRange.Checked)
                {
                    var query = from s in ctx.SalesOrders
                                select s;

                    if (query.Count() > 0)
                    {
                        List<SalesOrder> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.Createdon.Date >= dateTimePicker2.Value.Date && x.Createdon.Date <= dateTimePicker3.Value.Date)
                            {
                                sales += x.Amt_Tendered;
                            }
                        }
                    }
                }

                dgvAcct.Rows.Add();
                int rowCount = dgvAcct.Rows.Count - 1;
                DataGridViewRow R = dgvAcct.Rows[rowCount];
                R.Cells["Col1"].Value = "Sales";
                R.Cells["Col2"].Value = sales.ToString("n");
                R.Cells["Col4"].Value = sales.ToString("n");
                totalBal += sales;
            }
            catch (Exception)
            {

                lblMsg.Text = "Database error has occurred!";
            }
        }
        private void loadExp()
        {
            try
            {
                if (rdAll.Checked)
                {
                    var query = from s in ctx.ExpenseTrans
                                select s.Amount;
                    exp = (query.Count() > 0 ? query.Sum() : 0);
                }
                else if (rdDaily.Checked)
                {
                    var query = from s in ctx.ExpenseTrans
                                select s;
                    if (query.Count() > 0)
                    {
                        List<ExpenseTran> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.CreatedOn.Date.Equals(dateTimePicker1.Value.Date))
                            {
                                exp += x.Amount;
                            }
                        }
                    }
                }
                else if (rdRange.Checked)
                {
                    var query = from s in ctx.ExpenseTrans
                                select s;
                    if (query.Count() > 0)
                    {
                        List<ExpenseTran> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.CreatedOn.Date >= dateTimePicker2.Value.Date && x.CreatedOn <= dateTimePicker3.Value.Date)
                            {
                                exp += x.Amount;
                            }
                        }
                    }
                }

                dgvAcct.Rows.Add();
                int rowCount = dgvAcct.Rows.Count - 1;
                DataGridViewRow R = dgvAcct.Rows[rowCount];
                R.Cells["Col1"].Value = "Expenses";
                R.Cells["Col3"].Value = exp.ToString("n");

                totalExp += exp;
                R.Cells["Col4"].Value = totalExp.ToString("n");
                totalBal -= totalExp;
            }
            catch (Exception)
            {

                lblMsg.Text = "Database error has occurred!";
            }
        }
        private void loadReturns()
        {
            try
            {
                if (rdAll.Checked)
                {
                    var query = from s in ctx.SalesReturns
                                select s;
                    salesR = 0;
                    if (query.Count() > 0)
                    {
                        List<SalesReturn> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.Percentage == 0)
                            {
                                salesR += x.TotalValue;
                            }
                            else
                            {
                                salesR += (x.TotalValue - (x.TotalValue * decimal.Parse((x.Percentage / 100).ToString())));
                            }
                        }
                    }
                }
                else if (rdDaily.Checked)
                {
                    var query = from s in ctx.SalesReturns
                                select s;
                    salesR = 0;
                    if (query.Count() > 0)
                    {
                        List<SalesReturn> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.CreatedOn.Date.Equals(dateTimePicker1.Value.Date))
                            {
                                if (x.Percentage == 0)
                                {
                                    salesR += x.TotalValue;
                                }
                                else
                                {
                                    salesR += (x.TotalValue - (x.TotalValue * decimal.Parse((x.Percentage / 100).ToString())));
                                }
                            }

                        }
                    }
                }
                else if (rdRange.Checked)
                {
                    var query = from s in ctx.SalesReturns
                                select s;
                    salesR = 0;
                    if (query.Count() > 0)
                    {
                        List<SalesReturn> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.CreatedOn.Date >= dateTimePicker2.Value.Date && x.CreatedOn <= dateTimePicker3.Value.Date)
                            {
                                if (x.Percentage == 0)
                                {
                                    salesR += x.TotalValue;
                                }
                                else
                                {
                                    salesR += (x.TotalValue - (x.TotalValue * decimal.Parse((x.Percentage / 100).ToString())));
                                }
                            }

                        }
                    }
                }

                dgvAcct.Rows.Add();
                int rowCount = dgvAcct.Rows.Count - 1;
                DataGridViewRow R = dgvAcct.Rows[rowCount];
                R.Cells["Col1"].Value = "Sales Returned";
                R.Cells["Col3"].Value = salesR.ToString("n");
                totalExp += salesR;
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
            }
        }

        private void loadProducts()
        {
            try
            {
                inventory = 0;
                var query = from p in ctx.Products
                            select p;

                if(query.Count() > 0)
                {
                    List<Product> post = query.ToList();

                    foreach(var x in post)
                    {
                        inventory += x.Quantity * x.Sale;
                    }
                }

                dgvAcct.Rows.Add();
                int rowCount = dgvAcct.Rows.Count - 1;
                DataGridViewRow R = dgvAcct.Rows[rowCount];
                R.Cells["Col1"].Value = "Stock Value";
                R.Cells["Col2"].Value = inventory.ToString("n");
                R.Cells["Col4"].Value = inventory.ToString("n");
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
            }

        }

        private void emptyRow()
        {
            dgvAcct.Rows.Add();
            int rowCount = dgvAcct.Rows.Count - 1;
            DataGridViewRow R = dgvAcct.Rows[rowCount];
            R.Cells["Col1"].Value = "";
            R.Cells["Col4"].Value = "";
        }
        private void loadGrid()
        {
            dgvAcct.Rows.Clear();
            sales = exp = salesR = totalExp = totalBal = 0;

            loadSales();
            loadReturns();
            loadExp();
            emptyRow();
            dgvAcct.Rows.Add();
            int rowCount = dgvAcct.Rows.Count - 1;
            DataGridViewRow R = dgvAcct.Rows[rowCount];
            R.Cells["Col1"].Value = "Total Cash Balance";
            R.Cells["Col4"].Value = totalBal.ToString("n");
            emptyRow();
            loadProducts();
        }
        private void rdDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDaily.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker1.ResetText();
                dateTimePicker2.Enabled = false;
                dateTimePicker2.ResetText();
                dateTimePicker3.Enabled = false;
                dateTimePicker3.ResetText();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(totalBal > 0)
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = (rdAll.Checked ? "Financial Report" : rdDaily.Checked ? "Financial Report as at " + dateTimePicker1.Text : rdRange.Checked ? "Financial Report from " + dateTimePicker2.Text + " to " + dateTimePicker3.Text : "Financial Report"); // Header
                printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "**** De Royce Solution ****"; //Footer
                printer.FooterSpacing = 15;
                printer.PrintDataGridView(dgvAcct);
            }
            else
            {
                lblMsg.Text = "No value to Print!";
            }
            
        }

        private void rdRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rdRange.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
                dateTimePicker1.ResetText();
                dateTimePicker2.ResetText();
                dateTimePicker3.ResetText();
            }
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAll.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.ResetText();
                dateTimePicker2.Enabled = false;
                dateTimePicker2.ResetText();
                dateTimePicker3.Enabled = false;
                dateTimePicker3.ResetText();
                loadGrid();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          //  label1.Text = dateTimePicker1.Text;
            loadGrid();
        }

        private void FinancialMgt_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker1.ResetText();
            dateTimePicker2.Enabled = false;
            dateTimePicker2.ResetText();
            dateTimePicker3.Enabled = false;
            dateTimePicker3.ResetText();
           // label1.Text = "label1";
            sales = exp = salesR = totalExp = totalBal = 0;
        }
    }
}
