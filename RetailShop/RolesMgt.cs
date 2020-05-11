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
    public partial class RolesMgt : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        int roleId;
        public RolesMgt()
        {
            InitializeComponent();
        }

        private void getRoles()
        {
            lstRoles.Items.Clear();
            try
            {
                var query = from p in ctx.tblRoles
                            select p;

                if(query.Count() > 0)
                {
                    List<tblRole> post = query.ToList();

                    foreach(var x in post)
                    {
                        lstRoles.Items.Add(x.RoleName);
                    }
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }

        private void getPermits()
        {
            try
            {
                var query = from p in ctx.tblRoles
                            where p.Id == roleId
                            select p;

                if(query.Count() > 0)
                {
                    List<tblRole> post = query.ToList();
                    foreach(var x in post)
                    {
                        chkProdTypeMgt.Checked = (x.Role1 == 1 ? true : false);
                        chkProdMgt.Checked = (x.Role2 == 1 ? true : false);
                        chkSalesMgt.Checked = (x.Role3 == 1 ? true : false);
                        chkPartPay.Checked = (x.Role4 == 1 ? true : false);
                        chkReturnSales.Checked = (x.Role5 == 1 ? true : false);
                        chkExpType.Checked = (x.Role6 == 1 ? true : false);
                        chkPostExp.Checked = (x.Role7 == 1 ? true : false);
                        chkCompany.Checked = (x.Role8 == 1 ? true : false);
                        chkRoleMgt.Checked = (x.Role9 == 1 ? true : false);
                        chkUser.Checked = (x.Role10 == 1 ? true : false);
                        chkBackup.Checked = (x.Role11 == 1 ? true : false);
                        chkFinancial.Checked = (x.Role12 == 1 ? true : false);
                        chkProdList.Checked = (x.Role13 == 1 ? true : false);
                        chkSalesHistory.Checked = (x.Role14 == 1 ? true : false);
                        chkExpHistory.Checked = (x.Role15 == 1 ? true : false);
                        chkProdSold.Checked = (x.Role16 == 1 ? true : false);
                        chkRetHistory.Checked = (x.Role17 == 1 ? true : false);
                        chkProdReturn.Checked = (x.Role18 == 1 ? true : false);
                    }
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void addRole()
        {
            try
            {
                tblRole role = new tblRole()
                {
                    RoleName = txtRolename.Text,
                    Role1 = (chkProdTypeMgt.Checked ? 1 : 0),
                    Role2 = (chkProdMgt.Checked ? 1 : 0),
                    Role3 = (chkSalesMgt.Checked ? 1 : 0),
                    Role4 = (chkPartPay.Checked ? 1 : 0),
                    Role5 = (chkReturnSales.Checked ? 1 : 0),
                    Role6 = (chkExpType.Checked ? 1 : 0),
                    Role7 = (chkPostExp.Checked ? 1 : 0),
                    Role8 = (chkCompany.Checked ? 1 : 0),
                    Role9 = (chkRoleMgt.Checked ? 1 : 0),
                    Role10 = (chkUser.Checked ? 1 : 0),
                    Role11 = (chkBackup.Checked ? 1 : 0),
                    Role12 = (chkFinancial.Checked ? 1 : 0),
                    Role13 = (chkProdList.Checked ? 1 : 0),
                    Role14 = (chkSalesHistory.Checked ? 1 : 0),
                    Role15 = (chkExpHistory.Checked ? 1 : 0),
                    Role16 = (chkProdSold.Checked ? 1 : 0),
                    Role17 = (chkRetHistory.Checked ? 1 : 0),
                    Role18 = (chkProdReturn.Checked ? 1 : 0),
                    CreatedBy = Form1.userId,
                    CreatedOn = System.DateTime.Now

                };
                ctx.tblRoles.Add(role);
                ctx.SaveChanges();
                lblError.Text = "Role added successfully!";
                clear();
            }
            catch (Exception)
            {
                lblError.Text = "Database error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void updateRole()
        {
            try
            {
                tblRole p = ctx.tblRoles.FirstOrDefault(r => r.Id == roleId);
                p.Role1 = (chkProdTypeMgt.Checked ? 1 : 0);
                p.Role2 = (chkProdMgt.Checked ? 1 : 0);
                p.Role3 = (chkSalesMgt.Checked ? 1 : 0);
                p.Role4 = (chkPartPay.Checked ? 1 : 0);
                p.Role5 = (chkReturnSales.Checked ? 1 : 0);
                p.Role6 = (chkExpType.Checked ? 1 : 0);
                p.Role7 = (chkPostExp.Checked ? 1 : 0);
                p.Role8 = (chkCompany.Checked ? 1 : 0);
                p.Role9 = (chkRoleMgt.Checked ? 1 : 0);
                p.Role10 = (chkUser.Checked ? 1 : 0);
                p.Role11 = (chkBackup.Checked ? 1 : 0);
                p.Role12 = (chkFinancial.Checked ? 1 : 0);
                p.Role13 = (chkProdList.Checked ? 1 : 0);
                p.Role14 = (chkSalesHistory.Checked ? 1 : 0);
                p.Role15 = (chkExpHistory.Checked ? 1 : 0);
                p.Role16 = (chkProdSold.Checked ? 1 : 0);
                p.Role17 = (chkRetHistory.Checked ? 1 : 0);
                p.Role18 = (chkProdReturn.Checked ? 1 : 0);

                ctx.SaveChanges();
                lblError.Text = "Role updated successfully!";
                clear();
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void clear()
        {
            txtRolename.Clear();
            chkProdTypeMgt.Checked =  false;
            chkProdMgt.Checked = false;
            chkSalesMgt.Checked = false;
            chkPartPay.Checked = false;
            chkReturnSales.Checked = false;
            chkExpType.Checked = false;
            chkPostExp.Checked = false;
            chkCompany.Checked = false;
            chkRoleMgt.Checked = false;
            chkUser.Checked = false;
            chkBackup.Checked = false;
            chkFinancial.Checked = false;
            chkProdList.Checked = false;
            chkSalesHistory.Checked = false;
            chkExpHistory.Checked = false;
            chkProdSold.Checked = false;
            chkRetHistory.Checked = false;
            chkProdReturn.Checked = false;
            roleId = 0;
        }
        private void deleteRole()
        {
            try
            {
                // If linked to a user...
                var query = from r in ctx.Users where r.RoleId == roleId select r;

                if (query.Count() == 0)
                {
                    tblRole post = ctx.tblRoles.FirstOrDefault(p => p.Id == roleId);
                    //post.RoleName = txtRolename.Text;

                    ctx.tblRoles.Remove(post);
                    ctx.SaveChanges();

                    lblError.Text = "Role Deleted Successfully!";
                    lblError.ForeColor = Color.Lime;

                    getRoles();
                    clear();
                }
                else
                {
                    lblError.Text = "Error : Role linked to a User";
                    lblError.ForeColor = Color.Yellow;
                }

            }
            catch (Exception)
            {
                lblError.Text = "Error has occurred!!!";
                lblError.ForeColor = Color.Yellow;
            }
        }
        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                txtRolename.ReadOnly = true;
                if(lstRoles.SelectedIndex != -1)
                {
                    txtRolename.Text = lstRoles.SelectedItem.ToString();
                    roleId = ctx.tblRoles.FirstOrDefault(r => r.RoleName == txtRolename.Text).Id;
                    getPermits();
                }
            }
            catch (Exception)
            {

                lblError.Text = "An error has occurred!";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtRolename.Text != "")
            {
                if(roleId == 0)
                {
                    addRole();
                    getRoles();
                }
                else
                {
                    clear();
                    txtRolename.ReadOnly = false;
                    roleId = 0;
                    lblError.Text = "Error : Re-do the entry!";
                }
            }
            else
            {
                lblError.Text = "A role name is required!";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            txtRolename.ReadOnly = false;
            lblError.Text = "";
            roleId = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(roleId != 0)
            {
                updateRole();
            }
            else
            {
                lblError.Text = "No role selected!";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (roleId != 0)
            {
                deleteRole();
                
            }
            else
            {
                lblError.Text = "No role selected!";
            }
        }

        private void RolesMgt_Load(object sender, EventArgs e)
        {
            getRoles();
            clear();
            lblError.Text = "";
        }
    }
}
