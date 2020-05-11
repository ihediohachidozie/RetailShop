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
    public partial class ProductTypeMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int prodId, typeNo;
        public ProductTypeMgt()
        {
            InitializeComponent();
        }

        private void loadProdType()
        {
            try
            {
                lstProdType.Items.Clear();
                var query = from prod in ctx.ProductTypes
                            select prod;
                if (query.Count() > 0)
                {
                    List<ProductType> prodname = query.ToList();
                    prodname.ForEach(x => lstProdType.Items.Add(x.TypeNo + " - " + x.Name));
                    typeNo = ctx.ProductTypes.OrderByDescending(p => p.Id).FirstOrDefault().TypeNo + 1;
                }
                else
                {
                    typeNo = 101;
                }

                txtNo.Text = typeNo.ToString();
                txtName.Clear();
                prodId = 0;
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtNo.Text != "")
                {
                    var ProdType = new ProductType()
                    {
                        TypeNo = int.Parse(txtNo.Text),
                        Name = txtName.Text
                    };
                    ctx.ProductTypes.Add(ProdType);
                    ctx.SaveChanges();

                    lblError.Text = "Product Type Created Successfully!";
                    lblError.ForeColor = Color.Lime;
                }
                else
                {
                    lblError.Text = "Name field cannot be empty!";
                    lblError.ForeColor = Color.Yellow;
                }
                loadProdType();
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            loadProdType();
            prodId = 0;

            lblError.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(prodId != 0)
                {
                    // If linked to a Product...
                    ProductType prodType = ctx.ProductTypes.FirstOrDefault(p => p.Id == prodId);
                    prodType.Name = txtName.Text;
                    prodType.TypeNo = int.Parse(txtNo.Text);

                    ctx.SaveChanges();
                    lblError.Text = "Product Type Updated Successfully!";
                    lblError.ForeColor = Color.Lime;

                    loadProdType();
                    prodId = 0;
                }
                else
                {
                    lblError.Text = "No Product type selected!";
                    lblError.ForeColor = Color.Yellow;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }


        }

        private void lstProdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstProdType.SelectedIndex != -1)
                {
                    int marker = lstProdType.SelectedItem.ToString().IndexOf("-");


                    txtName.Text = lstProdType.SelectedItem.ToString().Substring(marker + 2);
                    txtNo.Text = lstProdType.SelectedItem.ToString().Substring(0, marker - 1);
                    prodId = int.Parse(txtNo.Text);
                    prodId = ctx.ProductTypes.FirstOrDefault(p => p.TypeNo == prodId).Id;

                    lblError.Text = "";
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }

        }

        private void ProductTypeMgt_Load(object sender, EventArgs e)
        {

            loadProdType();
            lblError.Text = "";
            prodId = 0;
           
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(prodId != 0)
                {
                    // If linked to a Product...
                    var query = from p in ctx.Products where p.ProdTypeId == prodId select p;

                    if (query.Count() == 0)
                    {
                        ProductType prodType = ctx.ProductTypes.FirstOrDefault(p => p.Id == prodId);
                        prodType.Name = txtName.Text;

                        ctx.ProductTypes.Remove(prodType);
                        ctx.SaveChanges();

                        lblError.Text = "Product Type Deleted Successfully!";
                        lblError.ForeColor = Color.Lime;

                        loadProdType();

                        prodId = 0;
                    }
                    else
                    {
                        lblError.Text = "Error: product type has link to products!";
                        lblError.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    lblError.Text = "No Product type selected!";
                    lblError.ForeColor = Color.Yellow;
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
