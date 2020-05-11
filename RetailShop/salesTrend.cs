using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RetailShop
{
    public partial class salesTrend : Form
    {
        string mnth;
        double axelLabelPos;
        CustomLabel customerLabel;
        Axis axisX;
        double sum = 0;
        decimal total = 0;
        int year;
        public salesTrend()
        {
            InitializeComponent();
        }
        private void getGraph()
        {
            total = 0;
            axisX = chart1.ChartAreas[0].AxisX;
            axelLabelPos = 0.5;

            using (RetailShopDBEntities db = new RetailShopDBEntities())
            {
                var query = from s in db.SalesOrders
                            where s.Createdon.Year == year
                            group s by s.Createdon.Month;


                foreach (var group in query)
                {
                    total += group.Sum(x => x.Amt_Tendered);
                    sum = double.Parse(group.Sum(x => x.Amt_Tendered).ToString("n"));
                    mnth = monthName(group.Key);

                    this.chart1.Series["Series1"].Points.Add(sum);
                    customerLabel = axisX.CustomLabels.Add(axelLabelPos, axelLabelPos + 1, mnth);
                    axelLabelPos = axelLabelPos + 1.0;
                    
                }
                lblTotal.Text = " Total Sales = " + total.ToString("n"); 
            }
        }

        private string monthName(int t)
        {
            switch (t)
            {
                case 1:
                    mnth = "Jan";
                    break;
                case 2:
                    mnth = "Feb";
                    break;
                case 3:
                    mnth = "Mar";
                    break;
                case 4:
                    mnth = "Apr";
                    break;
                case 5:
                    mnth = "May";
                    break;
                case 6:
                    mnth = "Jun";
                    break;
                case 7:
                    mnth = "Jul";
                    break;
                case 8:
                    mnth = "Aug";
                    break;
                case 9:
                    mnth = "Sep";
                    break;
                case 10:
                    mnth = "Oct";
                    break;
                case 11:
                    mnth = "Nov";
                    break;
                case 12:
                    mnth = "Dec";
                    break;
            }
            return mnth;
                       
        }

        private void getYear()
        {
            listBox1.Items.Clear();
            using (RetailShopDBEntities db = new RetailShopDBEntities())
            {
                var query = from s in db.SalesOrders
                            group s by s.Createdon.Year;


                foreach (var group in query)
                {
                    listBox1.Items.Add(group.Key);
                 }
            }
        }

        private void salesTrend_Load(object sender, EventArgs e)
        {
            getYear();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            if(listBox1.SelectedIndex != -1)
            {
                clearData();
                chart1.Series[0].LegendText = listBox1.SelectedItem.ToString() + " Sales";
                year = int.Parse(listBox1.SelectedItem.ToString());
                getGraph();
            }
        }

        private void clearData()
        {
            if(chart1.Series[0].Points.Count() > 0)
            {
                axisX.CustomLabels.Clear();
                chart1.Series[0].Points.Clear();

                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            }

        }
    }
}
