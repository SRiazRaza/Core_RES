using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;

namespace Rahman_Educational_Service
{
    public partial class rptCounterSale : Form
    {
        DateTime StartDate;
        DateTime EndDate;
        String find;
        public rptCounterSale(DateTime startDate, DateTime endDate, String a)
        {
            InitializeComponent();
            StartDate = startDate;
            EndDate = endDate;
            find = a;

        }

        private void rptCounterSale_Load(object sender, EventArgs e)
        {

            // this.reportViewer1.RefreshReport();
            LoadReport();
        }
        private void LoadReport()
        {
            string a = "CS";
            try
            {
                if (find == "")
                {
                    SQLConn.sqL = "SELECT t.InvoiceNo,TDate,AccType,AccNo,td.ProductNo,Description,ItemPrice,SUM(TD.Quantity) as totalQuantity,Discount,perItem from transactions as t,transactiondetails as td,product as p  where p.ProductNo = td.ProductNo AND td.InvoiceNo = t.InvoiceNo AND AccType= '" + a + "' AND  DATE_FORMAT(STR_TO_DATE(TDate, '%m/%d/%Y'), '%Y-%m-%d') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' GROUP BY AccType, T.InvoiceNo,P.ProductNo ORDER By AccNo";
                }
                else
                {
                    SQLConn.sqL = "SELECT t.InvoiceNo,TDate,AccType,AccNo,td.ProductNo,Description,ItemPrice,SUM(TD.Quantity) as totalQuantity,Discount,perItem from transactions as t,transactiondetails as td,product as p  where p.ProductNo = td.ProductNo AND td.InvoiceNo = t.InvoiceNo AND AccType= '" + a + "' AND AccNo='" + find + "' AND DATE_FORMAT(STR_TO_DATE(TDate, '%m/%d%Y'), '%Y-%m-%d') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' GROUP BY AccType, T.InvoiceNo,P.ProductNo ORDER By AccNo";


                }
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new MySqlDataAdapter(SQLConn.cmd);

                this.rahman_educational_serviceDataSet.Account.Clear();
                //                this.rahmat_casting_centerDataSet.debtin.Clear();

                SQLConn.da.Fill(this.rahman_educational_serviceDataSet.Account);


                ReportParameter startDate = new ReportParameter("StartDate", StartDate.ToString());
                ReportParameter endDate = new ReportParameter("EndDate", EndDate.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { startDate, endDate });

                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                //    this.reportViewer1.;
                //this.reportViewer1.ZoomPercent = 90;
                // this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
        }
    }
}
