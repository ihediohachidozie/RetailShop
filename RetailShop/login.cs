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
    public partial class login : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        dbRestore DBRestore = new dbRestore();
        bool result = false;
        public login()
        {
            InitializeComponent();
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
        private bool authUser(string usr, string pw)
        {
            pw = GetMd5Sum(pw);
            try
            {
                var query = from u in ctx.Users
                            where u.Username == usr && u.Password == pw
                            select u;

                if(query.Count() == 1)
                {
                    List<User> post = query.ToList();
                    
                    foreach(var x in post)
                    {
                        Form1.userId = x.Id;
                        Form1.roleid = x.RoleId;
                        Form1.user.Text = x.Username;
                        Form1.status = x.Status;
                    }
                    result = true;
                }                
            }
            catch (Exception)
            {

                lblErr.Text = "Database error occurred!";
                //if (usr == "admin" && pw == "admin")
                //    DBRestore.ShowDialog();
            }
            return result;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ctx.Database.Exists())
            {
                if (authUser(txtUser.Text, txtPw.Text))
                {
                    lblErr.Text = "Login Successfull!";
                    this.Close();
                }
                else
                {
                    lblErr.Text = "Login failed!";
                }
            }
            else if (txtUser.Text == "admin" && txtPw.Text == "admin")
            {
                DBRestore.ShowDialog();
            }
        }
        private void login_Load(object sender, EventArgs e)
        {
            lblErr.Text = "";
            result = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
