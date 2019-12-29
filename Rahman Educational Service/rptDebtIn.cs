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
    public partial class rptDebtIn : Form
    {
        DateTime StartDate;
        DateTime EndDate;
        String find;
        public rptDebtIn(DateTime startDate, DateTime endDate, String a)
        {
            InitializeComponent();
            StartDate = startDate;
            EndDate = endDate;
            find = a;
        }

        private void rptDebtIn_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rahman_educational_serviceDataSet.debtin' table. You can move, or remove it, as needed.
            this.debtinTableAdapter.Fill(this.rahman_educational_serviceDataSet.debtin);

            this.reportViewer1.RefreshReport();
            LoadReport();
        }
        private void LoadReport()
        {
            try
            {
                if (find == "")
                {
                    SQLConn.sqL = "SELECT * from debtin  where DateIN BETWEEN '" + StartDate.ToString("MM-dd-yyyy") + "' AND '" + EndDate.ToString("MM-dd-yyyy") + "'   GROUP BY TDetailNo,AccountID ORDER BY DateIN";
                }
                else
                {
                    SQLConn.sqL = "SELECT * from debtin  where AccountID='" + find + "' AND DateIN BETWEEN '" + StartDate.ToString("MM-dd-yyyy") + "' AND '" + EndDate.ToString("MM-dd-yyyy") + "'   GROUP BY TDetailNo,AccountID ORDER BY DateIN";

                }
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new MySqlDataAdapter(SQLConn.cmd);

                this.rahman_educational_serviceDataSet.debtin.Clear();
//                this.rahmat_casting_centerDataSet.debtin.Clear();

                SQLConn.da.Fill(this.rahman_educational_serviceDataSet.debtin);


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
