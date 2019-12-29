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
    public partial class frmPrint : Form
    {
        int invoiceNo;
        string rt;

        public frmPrint(int i, string r)
        {
            InitializeComponent();
            invoiceNo = i;
            rt = r;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
          this.Hide();
            LoadReport();
           AutoPrintCls autoprintme = new AutoPrintCls(reportViewer1.LocalReport);
           autoprintme.Print();

            this.Close();
            // this.reportViewer1.RefreshReport();
        }
        private void LoadReport()
        {
            try
            {
                SQLConn.sqL = "Select SUM(Quantity) as TotalQuantity,Description,TotalAmount,ItemPrice,Discount,SUM(perItem) as perItem,T.AccType,T.AccNo,T.AccName,Cash,PChange,OldDebt,NewDebt  FROM Product as P, TransactionDetails as TD,Transactions as T,Payment as PA WHERE TD.ProductNo = P.ProductNo AND TD.InvoiceNo=T.InvoiceNo AND T.InvoiceNo=PA.InvoiceNo AND T.InvoiceNo = '" + invoiceNo + "'  GROUP BY TD.ProductNo,TD.Discount";

                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new MySqlDataAdapter(SQLConn.cmd);

                this.rahman_educational_serviceDataSet.PrintBill.Clear();
                SQLConn.da.Fill(this.rahman_educational_serviceDataSet.PrintBill);
                SQLConn.da.Dispose();

                SQLConn.dr = SQLConn.cmd.ExecuteReader();
                if (SQLConn.dr.Read() == true)
                {


                    string name = SQLConn.dr["AccName"].ToString();
                  //  MessageBox.Show(name);
                    string date = DateTime.Now.ToString("dddd, dd-MMM-yyyy").ToString();
                    string OD = SQLConn.dr["OldDebt"].ToString();
                    string ND = SQLConn.dr["NewDebt"].ToString();
                    string CA = SQLConn.dr["Cash"].ToString();
                    string chg = SQLConn.dr["PChange"].ToString();
                    string TA = SQLConn.dr["TotalAmount"].ToString();
                    string no = SQLConn.dr["AccNo"].ToString();
                    string ty = SQLConn.dr["AccType"].ToString();


                    ReportParameter invoice = new ReportParameter("InvoiceNo", invoiceNo.ToString());
                    ReportParameter accN = new ReportParameter("AccName", name);
                    ReportParameter dat = new ReportParameter("Date", date);
                    ReportParameter oldD = new ReportParameter("OldDebt", OD);
                    ReportParameter newD = new ReportParameter("NewDebt", ND);
                    ReportParameter cash = new ReportParameter("Cash", CA);
                    ReportParameter change = new ReportParameter("Change", chg);
                    ReportParameter TAmount = new ReportParameter("TotalAmount", TA);
                    ReportParameter num = new ReportParameter("AccNo", no);
                    ReportParameter type = new ReportParameter("AccType", ty);
                    ReportParameter ret = null;

                    if (rt == "frmPOS")
                    {

                      ret = new ReportParameter("Return", "");
                    }
                    else if(rt=="frmSReturn")
                    {
                        ret = new ReportParameter("Return", "Return Sale");

                    }
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { invoice, accN, dat, oldD, newD, cash, change, TAmount, num, type, ret });
                    
                    
                    }
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomPercent = 90;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

             //   this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }
        }
    }
}
