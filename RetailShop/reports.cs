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
    public partial class reports : Form
    {
        RetailShopDBEntities ctx = new RetailShopDBEntities();
        public reports()
        {
            InitializeComponent();
        }
        int stockCount, stockQty, nilStock, salesCount, itemsold, dailySalesCount, returnCount, expCount;
        decimal stockCost, stockValue, salesValue, dailySales, totalReturn, totalExp;
        private void reports_Load(object sender, EventArgs e)
        {
            productReports();
            salesReport();
            salesReturnReport();
            expenseReport();
        }
       
        private void productReports()
        {
            //var prodR = ctx.Products.ToList().Count();
            stockCount = nilStock = stockQty = 0;
            stockCost = stockValue = 0;
            var query = from p in ctx.Products
                        select p;

            if(query.Count() > 0)
            {
                List<Product> post = query.ToList();
                foreach(var x in post)
                {
                    stockQty += x.Quantity;
                    stockCost += x.Quantity * x.Cost;
                    stockValue += x.Quantity * x.Sale;
                    nilStock += (x.Quantity == 0 ? 1 : 0);
                    stockCount++;
                }
            }
            btnTotalCount.Text = stockQty.ToString();
            btnTotalCostPrice.Text = stockCost.ToString("n");
            btnTotalSalePrice.Text = stockValue.ToString("n");
            btnNil.Text = nilStock.ToString();
        }

        private void salesReport()
        {
            salesCount = dailySalesCount = 0;
            salesValue = dailySalesCount = 0;
            var query = from s in ctx.SalesOrders
                        select s;

            if(query.Count() > 0)
            {
                List<SalesOrder> post = query.ToList();
                foreach(var x in post)
                {
                    salesValue += x.Amt_Tendered;
                    if (x.Createdon.Date.Equals(System.DateTime.Now.Date))
                    {
                        dailySalesCount++;
                        dailySales += x.Amt_Tendered;
                    }
                    
                    salesCount++;
                }
                itemsold = (from s in ctx.SoldItems select s.Quantity).ToList().Sum();
            }
            btnTotalSales.Text = salesValue.ToString("n");
            btnTotSalesCount.Text = salesCount.ToString();
            btnDailySales.Text = dailySales.ToString("n");
            btnDSalesCount.Text = dailySalesCount.ToString();
            //qua 
        }

        private void salesReturnReport()
        {
            returnCount = 0;
            totalReturn = 0;

            var query = from s in ctx.SalesReturns
                        select s;

            if (query.Count() > 0)
            {
                List<SalesReturn> post = query.ToList();
                foreach (var x in post)
                {
                    totalReturn += x.TotalValue - (x.TotalValue * decimal.Parse(x.Percentage.ToString()) / 100);
                    //if (x.Createdon.Date.Equals(System.DateTime.Now.Date))
                    //{
                    //    dailySalesCount++;
                    //    dailySales += x.Amt_Tendered;
                    //}

                    returnCount++;
                }
            }
            btnTotalReturn.Text = totalReturn.ToString("n");
            btnReturnCount.Text = returnCount.ToString();
        }

        private void expenseReport()
        {
            expCount = 0;
            totalExp = 0;

            var query = from s in ctx.ExpenseTrans
                        select s;

            if (query.Count() > 0)
            {
                List<ExpenseTran> post = query.ToList();
                foreach (var x in post)
                {
                    totalExp += x.Amount;
                    //if (x.Createdon.Date.Equals(System.DateTime.Now.Date))
                    //{
                    //    dailySalesCount++;
                    //    dailySales += x.Amt_Tendered;
                    //}

                    expCount++;
                }
            }
            btnTotalExp.Text = totalExp.ToString("n");
            btnExpCount.Text = expCount.ToString();
        }
    }
}
