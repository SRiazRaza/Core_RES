using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
namespace Rahman_Educational_Service
{
    public partial class frmMain : Form
    {
        string Username;
        int StaffID;
        public frmMain(string username, int staffID)
        {
            InitializeComponent();
            Username = username;
            StaffID = staffID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLConn.getData();
            this.lbluser.Text = Username.ToUpper();
            load_chart();
        }
        public void load_chart()
        {
            try

            {
                string abc = DateTime.Now.ToString("MM-dd-yyyy");
               // MessageBox.Show(abc);
                SQLConn.sqL = "   SELECT  * FROM transactions where TDate= '"+abc+"'   ";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();
                while (SQLConn.dr.Read() == true)
                {

                    this.chart1.Series["POS"].Points.AddY(SQLConn.dr["TotalAmount"].ToString());

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());


            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Exit System") == MsgBoxResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            frmStaff abc = new frmStaff();
            abc.ShowDialog();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmListCategory abc = new frmListCategory();
            abc.ShowDialog();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmListProduct abc = new frmListProduct();
            abc.ShowDialog();
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            frmFilterTransactions abc = new frmFilterTransactions();
            abc.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("Are you sure you want to LogOut?", MsgBoxStyle.YesNo, "Logout") == MsgBoxResult.Yes)
            {

                frmLogin abc = new frmLogin();

                this.Dispose();
                abc.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = "Date_Time: " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");

        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS abc = new frmPOS(StaffID);
            abc.ShowDialog();
            this.chart1.Series["POS"].Points.Clear();

            load_chart();
        }

        private void btnStocksReport_Click(object sender, EventArgs e)
        {
            frmDistributorAccount abc = new frmDistributorAccount();
            abc.ShowDialog();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmSchoolAccount abc = new frmSchoolAccount();
            abc.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmSReturn abc = new frmSReturn(StaffID);
            abc.ShowDialog();

            this.chart1.Series["POS"].Points.Clear();
            load_chart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmAbout abc = new frmAbout();
            abc.ShowDialog();  
        }
    }
}
