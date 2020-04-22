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
using Microsoft.VisualBasic;
using Microsoft.Reporting.WinForms;

namespace Rahman_Educational_Service
{
    public partial class rptStockOut : Form
    {
        DateTime StartDate;
        DateTime EndDate;
        public rptStockOut(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            StartDate = startDate;
            EndDate = endDate;
        }

        private void rptStockOut_Load(object sender, EventArgs e)
        {
            LoadReport();
         
          }
        private void LoadReport()
        {
            try
            {    
                SQLConn.sqL = "SELECT td.InvoiceNo,TDate,td.ProductNo,Description,ItemPrice,SUM(TD.Quantity) as totalQuantity,Discount,perItem from transactions as t,transactiondetails as td,product as p  where p.ProductNo = td.ProductNo AND td.InvoiceNo = t.InvoiceNo AND  DATE_FORMAT(STR_TO_DATE(TDate, '%m/%d/%Y'), '%Y-%m-%d') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' GROUP BY TDate, td.InvoiceNo,P.ProductNo ORDER By TDate";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new MySqlDataAdapter(SQLConn.cmd);
                this.rahman_educational_serviceDataSet.Account.Clear();
                //this.dsReportC.StocksIn.Clear();
                SQLConn.da.Fill(this.rahman_educational_serviceDataSet.Account);

                ReportParameter startDate = new ReportParameter("StartDate", StartDate.ToString());
                ReportParameter endDate = new ReportParameter("EndDate", EndDate.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { startDate, endDate });

                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomPercent = 90;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
        }
    }
}
