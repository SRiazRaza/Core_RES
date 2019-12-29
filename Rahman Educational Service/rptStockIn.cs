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
    public partial class rptStockIn : Form
    {
        DateTime StartDate;
        DateTime EndDate;

        public rptStockIn(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            StartDate = startDate;
            EndDate = endDate;

        }

        private void rptStockIn_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rahman_educational_serviceDataSet.stockin' table. You can move, or remove it, as needed.
            this.stockinTableAdapter.Fill(this.rahman_educational_serviceDataSet.stockin);

          //  this.reportViewer1.RefreshReport();
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                SQLConn.sqL = "SELECT S.ProductNo, Description, SUM(Quantity) as Quantity, DateIn FROM Product as P, StockIn as S WHERE S.ProductNo = P.ProductNo AND DateIn BETWEEN '" + StartDate.ToString("MM-dd-yyyy") + "' AND '" + EndDate.ToString("MM-dd-yyyy") + "' GROUP BY P.ProductNo, DateIN ORDER BY DateIn, Description";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new MySqlDataAdapter(SQLConn.cmd);
                this.rahman_educational_serviceDataSet.stockin.Clear();
                //this.dsReportC.StocksIn.Clear();
                SQLConn.da.Fill(this.rahman_educational_serviceDataSet.stockin);

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
