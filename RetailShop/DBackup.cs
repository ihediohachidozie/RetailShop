using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailShop
{
    public partial class DBackup : Form
    {
        public DBackup()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            try
            {
                Server dbServer = new Server(new ServerConnection(txtServer.Text, txtUsername.Text, txtPassword.Text));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = txtDatabase.Text };
                dbBackup.Devices.AddDevice(@"C:\Data\RetailShopDB.bak", DeviceType.File);
                dbBackup.Initialize = true;
                dbBackup.PercentComplete += DbBackup_PercentComplete;
                dbBackup.Complete += DbBackup_Complete;
                dbBackup.SqlBackupAsync(dbServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DbBackup_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = lblStatus.Text + " " + e.Error.Message;

                });
            }
        }

        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
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

        private void DBackup_Load(object sender, EventArgs e)
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

                // Create Data Folder
                if (!Directory.Exists(@"C:\Data"))
                {
                    Directory.CreateDirectory(@"C:\Data");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
