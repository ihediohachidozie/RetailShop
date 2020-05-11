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
    public partial class changePw : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        public changePw()
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
        private void chgPw()
        {
            try
            {
                User cuser = ctx.Users.FirstOrDefault(u => u.Id == Form1.userId);
                if(GetMd5Sum(txtCpw.Text) == cuser.Password)
                {
                    cuser.Password = GetMd5Sum(txtPw1.Text);
                    ctx.SaveChanges();

                    lblError.Text = "Password change successful!";
                    lblError.ForeColor = Color.Lime;
                    txtCpw.Clear();
                    txtPw1.Clear();
                    txtPw2.Clear();
                }
                else
                {
                    lblError.Text = "Incorrect user password!";
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtPw1.Text != "" && txtPw2.Text != "" && txtCpw.Text != "")
            {
                if(txtPw1.Text == txtPw2.Text)
                {
                    chgPw();
                }
                else
                {
                    lblError.Text = "Password mismatched!";
                }
            }
            else
            {
                lblError.Text = "No field must be empty!";
            }
        }
    }
}
