using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailShop
{
    public partial class userMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int id;
        public userMgt()
        {
            InitializeComponent();
        }

        private void getUsers()
        {
            lstUser.Items.Clear();
            try
            {
                var query = from p in ctx.Users
                            select p;

                if (query.Count() > 0)
                {
                    List<User> post = query.ToList();

                    foreach (var x in post)
                    {
                        lstUser.Items.Add(x.Username);
                    }
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void loadUser()
        {
            try
            {
                var query = from p in ctx.Users
                            where p.Id == id
                            select p;

                if (query.Count() > 0)
                {
                    List<User> post = query.ToList();

                    foreach (var x in post)
                    {
                        txtDisplayName.Text = x.Display;
                        txtUsername.Text = x.Username;
                        cboRole.Text = x.tblRole.RoleName;
                        chkStatus.Checked = (x.Status == 1 ? true : false);
                    }
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void getRoles()
        {
            cboRole.Items.Clear();
            try
            {
                var query = from p in ctx.tblRoles
                            select p;

                if (query.Count() > 0)
                {
                    List<tblRole> post = query.ToList();

                    foreach (var x in post)
                    {
                        cboRole.Items.Add(x.RoleName);
                    }
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void clearall()
        {
            txtDisplayName.Clear();
            txtPassword.Clear();
            txtUsername.Clear();
            chkStatus.Checked = false;
            id = 0;
            txtPassword.Enabled = true;
            txtUsername.Enabled = true;
        }
        static public string GetMd5Sum(string str)
        {

            // First we need to convert the string into bytes, which
            // means using a text encoder.
            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            // And return it
            return sb.ToString();
        }
        private void addUser()
        {
            try
            {
                var query = from u in ctx.Users
                            where u.Username == txtUsername.Text.ToLower()
                            select u;

                if(query.Count() == 0)
                {

                    User post = new User()
                    {
                        Display = txtDisplayName.Text,
                        Username = txtUsername.Text.ToLower(),
                        Password = GetMd5Sum(txtPassword.Text),
                        Status = (chkStatus.Checked ? 1 : 0),
                        RoleId = ctx.tblRoles.FirstOrDefault(r=>r.RoleName == cboRole.Text).Id,
                        CreatedOn = System.DateTime.Now
                        
                    };
                    ctx.Users.Add(post);
                    ctx.SaveChanges();
                    lblError.Text = "User successfully added!";
                    lblError.ForeColor = Color.Lime;
                    clearall();
                    getUsers();
                }
                else
                {
                    lblError.Text = "Username already exist!";
                    lblError.ForeColor = Color.Yellow;
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void updateUser()
        {
            try
            {
                User p = ctx.Users.FirstOrDefault(u => u.Id == id);

                p.Display = txtDisplayName.Text != "" ? txtDisplayName.Text : p.Display;
               // p.Username = txtUsername.Text.ToLower();
               // p.Password = (txtPassword.Text != "" ? GetMd5Sum(txtPassword.Text) : p.Password);
                p.Status = (chkStatus.Checked ? 1 : 0);
                p.RoleId = ctx.tblRoles.FirstOrDefault(r => r.RoleName == cboRole.Text).Id;

                ctx.SaveChanges();
                lblError.Text = "Role updated successfully!";
                lblError.ForeColor = Color.Lime;
                clearall();
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void deleteUser()
        {
            try
            {
                // If user is linked to a transaction...
                var query = from r in ctx.SalesOrders where r.Createdby == id select r;
                var query2 = from r in ctx.ExpenseTrans where r.CreatedBy == id select r;
                var query3 = from r in ctx.SalesReturns where r.CreatedBy == id select r;
                var query4 = from r in ctx.Products where r.CreatedBy == id select r;

                if (query.Count() == 0 && query2.Count() == 0 && query3.Count() == 0 && query4.Count() == 0)
                {
                    User post = ctx.Users.FirstOrDefault(p => p.Id == id);
                    //post.RoleName = txtRolename.Text;

                    ctx.Users.Remove(post);
                    ctx.SaveChanges();

                    lblError.Text = "User Deleted Successfully!";
                    lblError.ForeColor = Color.Lime;

                    getRoles();
                    clearall();
                }
                else
                {
                    lblError.Text = "Error: User is linked to a transaction!";
                    lblError.ForeColor = Color.Yellow;
                }

            }
            catch (Exception)
            {

                //throw;
                lblError.Text = "Error has occurred!!!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void userMgt_Load(object sender, EventArgs e)
        {
            getUsers();
            getRoles();
            lblError.Text = "";
            clearall();
        }

        private void lstUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            txtPassword.Enabled = false;
            txtUsername.Enabled = false;
            try
            {
                if(lstUser.SelectedIndex != -1)
                {
                    id = ctx.Users.FirstOrDefault(u => u.Username == lstUser.SelectedItem.ToString()).Id;
                    // updateUser();
                    loadUser();
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                if (cboRole.SelectedIndex != -1)
                {
                    updateUser();
                }
                else
                {
                    lblError.Text = "No role selected!";
                    lblError.ForeColor = Color.Yellow;
                }
            }
            else
            {
                lblError.Text = "No user selected!";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                deleteUser();
            }
            else
            {
                lblError.Text = "No user selected!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtDisplayName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                if(cboRole.SelectedIndex != -1)
                {
                    addUser();
                }
                else
                {
                    lblError.Text = "No role selected!";
                    lblError.ForeColor = Color.Yellow;
                }
            }
            else
            {
                lblError.Text = "No field must be empty!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearall();
            lblError.Text = "";
        }
    }
}
