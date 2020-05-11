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
    public partial class ProductMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
      //  int userId = 1;
        int prodId = 0;

        public ProductMgt()
        {
            InitializeComponent();
        }
        private void loadProdType()
        {
            try
            {
                cboType2.Items.Clear();
                cboProdType.Items.Clear();

                var query = from prod in ctx.ProductTypes
                            select prod;
                if (query.Count() > 0)
                {
                    List<ProductType> prodname = query.ToList();
                    cboType2.Items.Add("All");
                    prodname.ForEach(x => cboType2.Items.Add(x.Name));
                    prodname.ForEach(x => cboProdType.Items.Add(x.Name));

                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
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
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
            }

        }
        private void clear()
        {
            txtCost.Clear();
            txtModelNo.Clear();
            txtProductName.Clear();
            txtQty.Clear();
            txtSelling.Clear();
            txtQty.Text = "0";
            lblQty.Text = "";
            //lblMsg.Text = "";
            cboProdType.SelectedIndex = -1;
            cboType2.SelectedIndex = -1;
            prodId = 0;
        }
        private void ProductMgt_Load(object sender, EventArgs e)
        {
            loadProdType();
            loadProducts();
            clear();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (txtCost.Text != "" && txtModelNo.Text != "" && txtProductName.Text != "" && txtQty.Text != "" && txtSelling.Text != "" && cboProdType.Text != "")
                {
                    var query = from p in ctx.Products where p.ModelNo == txtModelNo.Text.ToUpper() select p;

                    if (query.Count() == 0)
                    {
                        var product = new Product()
                        {
                            ModelNo = txtModelNo.Text.ToUpper(),
                            ProductName = txtProductName.Text,
                            Cost = decimal.Parse(txtCost.Text),
                            Sale = decimal.Parse(txtSelling.Text),
                            Quantity = int.Parse(txtQty.Text),
                            ProdTypeId = ctx.ProductTypes.FirstOrDefault(p => p.Name == cboProdType.Text).Id,
                            CreatedBy = Form1.userId,
                            CreatedOn = System.DateTime.Now,
                            ModifiedBy = Form1.userId,
                            ModifiedOn = System.DateTime.Now
                        };
                        ctx.Products.Add(product);
                        ctx.SaveChanges();

                        lblMsg.Text = "Product Added Successfully!";
                        lblMsg.ForeColor = Color.Lime;
                        clear();
                        loadProducts();
                    }
                    else
                    {
                        lblMsg.Text = "Product already exists!"; 
                    }
                }
                else
                {
                    lblMsg.Text = "No field must be empty!";
                }

            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
            }
        }

        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstProduct.SelectedIndex != -1)
                {
                    int marker = lstProduct.SelectedItem.ToString().IndexOf(":") - 1;
                    string modelno = lstProduct.SelectedItem.ToString().Substring(0, marker);

                    Product product = ctx.Products.FirstOrDefault(p => p.ModelNo == modelno);
                    prodId = product.Id;
                    txtProductName.Text = product.ProductName;
                    txtCost.Text = product.Cost.ToString("n");
                    txtModelNo.Text = product.ModelNo;
                    lblQty.Text = (product.Quantity <= 1 ? product.Quantity.ToString() + " piece" : product.Quantity.ToString() + " pieces");
                    txtQty.Text = "0";
                    txtSelling.Text = product.Sale.ToString("n");
                    cboProdType.Text = product.ProductType.Name;

                    lblMsg.Text = "";
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            lblMsg.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodId != 0)
                {
                    Product product = ctx.Products.FirstOrDefault(p => p.Id == prodId);
                    product.ModelNo = txtModelNo.Text;
                    product.ProductName = txtProductName.Text;
                    product.Quantity += int.Parse(txtQty.Text);
                    product.Cost = decimal.Parse(txtCost.Text);
                    product.Sale = decimal.Parse(txtSelling.Text);
                    product.ProdTypeId = ctx.ProductTypes.FirstOrDefault(p => p.Name == cboProdType.Text).Id;
                    product.ModifiedBy = Form1.userId;
                    product.ModifiedOn = System.DateTime.Now;

                    ctx.SaveChanges();
                    lblMsg.Text = "Product Updated Successfully!";
                    lblMsg.ForeColor = Color.Lime;
                    clear();
                    loadProducts();
                }
                else
                {
                    lblMsg.Text = "No product selected!";
                    lblMsg.ForeColor = Color.Yellow;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodId != 0)
                {
                    Product product = ctx.Products.FirstOrDefault(p => p.Id == prodId);
                    var delItem = from d in ctx.SoldItems where d.ProductId == prodId select d;
                    if(delItem.Count() == 0)
                    {
                        ctx.Products.Remove(product);
                        ctx.SaveChanges();

                        lblMsg.Text = "Product Deleted Successfully!";
                        lblMsg.ForeColor = Color.Lime;

                        clear();
                        loadProducts();

                    }else
                    {
                        lblMsg.Text = "Product has link to Sales transactions!";
                        lblMsg.ForeColor = Color.Yellow;
                    }
                }
                else
                {
                    lblMsg.Text = "No Product selected!";
                    lblMsg.ForeColor = Color.Yellow;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
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
                txtCost.Clear();
                txtModelNo.Clear();
                txtProductName.Clear();
                txtQty.Clear();
                txtSelling.Clear();
                cboType2.Text = "";
                lblQty.Text = "";
            }
            catch (Exception)
            {
                lblMsg.Text = "Database error has occurred!";
                lblMsg.ForeColor = Color.Yellow;
            }
        }
    }
}
