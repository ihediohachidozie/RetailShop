using System;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace RetailShop
{
    public partial class dbRestore : Form
    {
        public dbRestore()
        {
            InitializeComponent();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            try
            {
                Server dbServer = new Server(new ServerConnection(txtServer.Text, txtUsername.Text, txtPassword.Text));
                Restore dbRestore = new Restore() { Database = txtDatabase.Text, Action = RestoreActionType.Database, ReplaceDatabase= true, NoRecovery = false};
                dbRestore.Devices.AddDevice(@"C:\Data\RetailShopDB.bak", DeviceType.File);
        //        dbRestore.Initialize = true;
                dbRestore.PercentComplete += DbRestore_PercentComplete;
                dbRestore.Complete += DbRestore_Complete;
                dbRestore.SqlRestoreAsync(dbServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DbRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = lblStatus.Text + " " + e.Error.Message;

                });
            }
        }

        private void DbRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = e.Percent;
                progressBar1.Update();
            });
            lblPercent.Invoke((MethodInvoker)delegate
            {
                lblPercent.Text = $"{e.Message}%";

            });
        }

        private void dbRestore_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                lblPercent.Text = "0 %";

                lblStatus.Text = "Status:";

                // Retrieve instance name
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                DataTable table = instance.GetDataSources();
                foreach (DataRow row in table.Rows)
                {
                    txtServer.Text = row[0].ToString() + "\\" + row[1].ToString();
                }

                txtDatabase.Text = "RetailShopDB";
                txtUsername.Text = "sa";
                txtPassword.Text = "test";

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
