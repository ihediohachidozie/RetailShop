using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailShop
{
    public partial class companyMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        string filename;
        List<Company> store;
        public companyMgt()
        {
            InitializeComponent();
        }
        byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private async void saveData()
        {
            // FileInfo file = new FileInfo(filename);


            if (txtPhone.Text != "" && txtName.Text != "" && txtAddress.Text != "")
            {
                if (picLogo != null && picLogo.Image != null)
                {
                    Bitmap img = new Bitmap(filename);

                    if (img.Width <= 300 && img.Height <= 300)
                    {
                        Company post = new Company()
                        {
                            Name = txtName.Text,
                            Address = txtAddress.Text,
                            Telephone = txtPhone.Text,
                            Logo = ConvertImageToBinary(picLogo.Image)
                        };
                        ctx.Companies.Add(post);
                        await ctx.SaveChangesAsync();

                        lblError.Text = "Company Data Saved Successfully!";
                        lblError.ForeColor = Color.Lime;
                    }
                    else
                    {
                        lblError.Text = "Image too big!";
                    }
                }
                else
                {
                    lblError.Text = "Upload the company logo!";
                }
            }
            else
            {
                lblError.Text = "No field must be empty!";
            }
        }

        private async void updateData()
        {
            //Company data = ctx.Companies.FirstOrDefault(c => c.Id == 1);
            List<Company> query = ctx.Companies.ToList();
            if(query.Count() > 0)
            {
                foreach(var x in query)
                {
                    x.Name = txtName.Text;
                    x.Address = txtAddress.Text;
                    x.Telephone = txtPhone.Text;
                    if (chkImage.Checked)
                    {
                        x.Logo = ConvertImageToBinary(picLogo.Image);
                    }
                }
            }
            await ctx.SaveChangesAsync();

            lblError.Text = "Company Data Updated Successfully!";
            lblError.ForeColor = Color.Lime;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(ctx.Companies.ToList().Count() != 1)
                {
                    saveData();

                }
                else
                {
                    updateData();
                }

            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filename = ofd.FileName;
                    //lblFileName.Text = filename;
                    picLogo.Image = Image.FromFile(filename);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            enableAll();
            lblError.Text = "";
        }

        private void enableAll()
        {
            txtPhone.ReadOnly = false;
            txtName.ReadOnly = false;
            txtAddress.ReadOnly = false;
            picLogo.Enabled = true;
            btnSave.Enabled = true;
            btnUpload.Enabled = true;
        }

        private void disableAll()
        {
            txtPhone.ReadOnly = true;
            txtName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            picLogo.Enabled = false;
            btnSave.Enabled = false;
            btnUpload.Enabled = false;
            chkImage.Checked = false;
            lblError.Text = "";
        }

        private void companyMgt_Load(object sender, EventArgs e)
        {           
            loadData();
            disableAll();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            enableAll();
        }

        private void chkImage_CheckedChanged(object sender, EventArgs e)
        {
            picLogo.Image = null;
            picLogo.Refresh();
            picLogo.InitialImage = null;
        }

        private void loadData()
        {
            try
            {
               // Company store = ctx.Companies.FirstOrDefault(s => s.Id == 1);
                store = ctx.Companies.ToList();
                if(store.Count() > 0)
                {
                    foreach( var x in store)
                    {
                        txtName.Text = x.Name;
                        txtAddress.Text = x.Address;
                        txtPhone.Text = x.Telephone;
                        picLogo.Image = ConvertBinaryToImage(x.Logo);

                    }
                    chkImage.Enabled = true;
                }
            }
            catch (Exception)
            {

                lblError.Text = "Create a Company!";
            }
        }
    }
}
