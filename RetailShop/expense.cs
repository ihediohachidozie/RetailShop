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
    public partial class expense : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int expId, typeNo;
        public expense()
        {
            InitializeComponent();
        }

        private void expense_Load(object sender, EventArgs e)
        {
            loadExpType();
            lblError.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtNo.Text != "")
                {
                    var ExpType = new ExpenseType()
                    {
                        TypeNo = int.Parse(txtNo.Text),
                        Name = txtName.Text
                    };
                    ctx.ExpenseTypes.Add(ExpType);
                    ctx.SaveChanges();

                    lblError.Text = "Expense Type Created Successfully!";
                    lblError.ForeColor = Color.Lime;
                }
                else
                {
                    lblError.Text = "Name field cannot be empty!";
                    lblError.ForeColor = Color.Yellow;
                }
                loadExpType();
            }
            catch (Exception)
            {

                lblError.Text = "Database Error has occurred!!!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (expId != 0)
                {
                    // If linked to a Product...
                    ExpenseType ExpType = ctx.ExpenseTypes.FirstOrDefault(p => p.Id == expId);
                    ExpType.Name = txtName.Text;
                    ExpType.TypeNo = int.Parse(txtNo.Text);

                    ctx.SaveChanges();
                    lblError.Text = "Expense Type Updated Successfully!";
                    lblError.ForeColor = Color.Lime;

                    loadExpType();
                    expId = 0;
                }
                else
                {
                    lblError.Text = "No expense type selected!";
                    lblError.ForeColor = Color.Yellow;
                }

            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(expId != 0)
                {
                    // If linked to an Expense Transaction...
                    var query = from p in ctx.ExpenseTrans where p.ExpTypeId == expId select p;

                    if (query.Count() == 0)
                    {
                        ExpenseType expType = ctx.ExpenseTypes.FirstOrDefault(p => p.Id == expId);
                        expType.Name = txtName.Text;

                        ctx.ExpenseTypes.Remove(expType);
                        ctx.SaveChanges();

                        lblError.Text = "Expense Type Deleted Successfully!";
                        lblError.ForeColor = Color.Lime;

                        loadExpType();
                        expId = 0;
                    }
                    else
                    {
                        lblError.Text = "Expense Type has a link to an Expense Transaction";
                        lblError.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    lblError.Text = "No expense type selected!";
                    lblError.ForeColor = Color.Yellow;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void lstExpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstExpType.SelectedIndex != -1)
                {
                    int marker = lstExpType.SelectedItem.ToString().IndexOf("-");


                    txtName.Text = lstExpType.SelectedItem.ToString().Substring(marker + 2);
                    txtNo.Text = lstExpType.SelectedItem.ToString().Substring(0, marker - 1);
                    expId = int.Parse(txtNo.Text);
                    expId = ctx.ExpenseTypes.FirstOrDefault(p => p.TypeNo == expId).Id;

                    lblError.Text = "";
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            loadExpType();
            lblError.Text = "";
        }

        private void loadExpType()
        {
            try
            {
                lstExpType.Items.Clear();
                var query = from prod in ctx.ExpenseTypes
                            select prod;
                if (query.Count() > 0)
                {
                    List<ExpenseType> Expname = query.ToList();
                    Expname.ForEach(x => lstExpType.Items.Add(x.TypeNo + " - " + x.Name));
                    typeNo = ctx.ExpenseTypes.OrderByDescending(p => p.Id).FirstOrDefault().TypeNo + 1;
                }
                else
                {
                    typeNo = 201;
                }

                txtNo.Text = typeNo.ToString();
                txtName.Clear();
                expId = 0;
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }

        }
    }
}
