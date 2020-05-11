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
    public partial class postExpense : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int expId;
        public postExpense()
        {
            InitializeComponent();
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
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void postExp()
        {
            try
            {
                ExpenseTran post = new ExpenseTran()
                {
                    ExpTypeId = expId,
                    PostingDate = dateTimePicker1.Value.Date,
                    Amount = decimal.Parse(txtAmount.Text),
                    CreatedBy = Form1.userId,
                    CreatedOn = System.DateTime.Now,
                    Description = txtExpType.Text + " : " + txtDescription.Text
                };
                ctx.ExpenseTrans.Add(post);
                ctx.SaveChanges();

                expId = 0;
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void clearFields()
        {
            dateTimePicker1.ResetText();
            txtAmount.Clear();
            txtExpType.Clear();
            txtDescription.Clear();
            expId = 0;
        }
        private void postExpense_Load(object sender, EventArgs e)
        {
            loadExpType();
            clearFields();
            
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtExpType.Text != "" && txtDescription.Text != "" && txtAmount.Text != "")
                {
                    postExp();
                    clearFields();
                    lblError.Text = "Expense Transaction Posted Successfully!";
                    lblError.ForeColor = Color.Lime;
                    
                }
                else
                {
                    lblError.Text = "No field must be empty!";
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

                    txtExpType.Text = lstExpType.SelectedItem.ToString().Substring(marker + 2);
                    //txtNo.Text = lstExpType.SelectedItem.ToString().Substring(0, marker - 1);
                    expId = int.Parse(lstExpType.SelectedItem.ToString().Substring(0, marker - 1));
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
    }
}
