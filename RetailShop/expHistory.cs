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
    public partial class expHistory : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int count;
        decimal total;
        public expHistory()
        {
            InitializeComponent();
        }
        private void getAll()
        {
            total = 0;
            count = 0;
            dgvExp.Rows.Clear();
            try
            {
                var query = from exp in ctx.ExpenseTrans
                            select exp;

                if (rdAll.Checked)
                {
                    if (query.Count() > 0)
                    {
                        List<ExpenseTran> post = query.ToList();
                        foreach (var x in post)
                        {
                            dgvExp.Rows.Add();
                            int rowCount = dgvExp.Rows.Count - 1;
                            DataGridViewRow R = dgvExp.Rows[rowCount];
                            R.Cells["Col1"].Value = ++count;
                            R.Cells["Col2"].Value = x.PostingDate.ToShortDateString();
                            R.Cells["Col3"].Value = x.Description;
                            R.Cells["Col4"].Value = x.Amount.ToString("n");
                            R.Cells["Col5"].Value = x.CreatedOn;
                            total += x.Amount;
                        }
                    }
          //          txtTotal.Text = total.ToString("n");
                }else if (rdDaily.Checked)
                {
                    if (query.Count() > 0)
                    {
                        List<ExpenseTran> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.PostingDate.Date.Equals(dateTimePicker1.Value.Date))
                            {
                                dgvExp.Rows.Add();
                                int rowCount = dgvExp.Rows.Count - 1;
                                DataGridViewRow R = dgvExp.Rows[rowCount];
                                R.Cells["Col1"].Value = ++count;
                                R.Cells["Col2"].Value = x.PostingDate.ToShortDateString();
                                R.Cells["Col3"].Value = x.Description;
                                R.Cells["Col4"].Value = x.Amount.ToString("n");
                                R.Cells["Col5"].Value = x.CreatedOn;
                                total += x.Amount;
                            }
                        }
                    }
             //       txtTotal.Text = total.ToString("n");
                }
                else if (rdRange.Checked)
                {
                    if (query.Count() > 0)
                    {
                        List<ExpenseTran> post = query.ToList();
                        foreach (var x in post)
                        {
                            if (x.PostingDate.Date >= dateTimePicker2.Value.Date && x.PostingDate.Date <= dateTimePicker3.Value.Date)
                            {
                                dgvExp.Rows.Add();
                                int rowCount = dgvExp.Rows.Count - 1;
                                DataGridViewRow R = dgvExp.Rows[rowCount];
                                R.Cells["Col1"].Value = ++count;
                                R.Cells["Col2"].Value = x.PostingDate.ToShortDateString();
                                R.Cells["Col3"].Value = x.Description;
                                R.Cells["Col4"].Value = x.Amount.ToString("n");
                                R.Cells["Col5"].Value = x.CreatedOn;
                                total += x.Amount;
                            }
                        }
                    }
                    
                }
                txtTotal.Text = total.ToString("n");
                label6.Text = (count > 1 ? count.ToString() + " Expense records" : count.ToString() + " Expense record");
            }
            catch (Exception)
            {

                label6.Text = "Database error has occurred!";
            }
        }

        private void expHistory_Load(object sender, EventArgs e)
        {
            count = 0;
            total = 0;
            
            txtTotal.Clear();
            dateTimePicker3.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker1.ResetText();
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker1.Enabled = false;
            btnFind.Enabled = false;
            dgvExp.Rows.Clear();
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            getAll();
        }

        private void rdDaily_CheckedChanged(object sender, EventArgs e)
        {
            dgvExp.Rows.Clear();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvExp.Rows.Count > 0)
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Expense Report"; // Header
                printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "**** De Royce Solution ****"; //Footer
                printer.FooterSpacing = 15;
                printer.PrintDataGridView(dgvExp);
            }
            else
            {
                label6.Text = "No rows to Print!";
            }
        }
    }
}
