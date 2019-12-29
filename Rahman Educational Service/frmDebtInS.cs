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

namespace Rahman_Educational_Service
{
    public partial class frmDebtInS : Form
    {
        int accountID;
        double rough= 0.00;
        public frmDebtInS(int abc)
        {
            InitializeComponent();
            accountID = abc;
        }

        private void frmDebtInS_Load(object sender, EventArgs e)
        {
            txtMoney_Debt.Text = "";
            txtTotalMoneyDebt.Text = "";
            GetDebtInfo();

        }
        private void GetDebtInfo()
        {

            try
            {

                SQLConn.sqL = "SELECT  Name,Money_Debt FROM SACCOUNTS WHERE AccountID =" + accountID + "";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblName.Text = SQLConn.dr[0].ToString();

                    lblMoney.Text = SQLConn.dr[1].ToString();
                    txtMoney_Debt.Text = "0";
                }
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
        private void AddDebtIn()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO DebtINS(AccountID,Name,LastMoneyDebt,NewMoneyDebt,GivenMoney,DateIn,TimeIN) Values('" + accountID + "', '" + lblName.Text + "', '" + lblMoney.Text + "',  '" + txtTotalMoneyDebt.Text + "','" + txtMoney_Debt.Text + "', '" + System.DateTime.Now.ToString("MM-dd-yyyy") + "','" + System.DateTime.Now.ToString("hh:mm") + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();

                UpdateAccountDebt();
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
        private void UpdateAccountDebt()
        {
            try
            {
                SQLConn.sqL = "UPDATE saccounts SET DateIN='" + System.DateTime.Now.ToString("MM-dd-yyyy") + "',Money_Debt =  '" + txtTotalMoneyDebt.Text + "' WHERE AccountID = '" + accountID + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
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

        private void button3_Click(object sender, EventArgs e)
        {
            AddDebtIn();
            if (System.Windows.Forms.Application.OpenForms["frmSchoolAccount"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmSchoolAccount"] as frmSchoolAccount).LoadStaffs("");
            }

            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMoney_Debt_TextChanged(object sender, EventArgs e)
        {
             rough= Convert.ToDouble((Conversion.Val(lblMoney.Text) - Conversion.Val(txtMoney_Debt.Text)));
            txtTotalMoneyDebt.Text = Math.Round(rough, 2).ToString();
        }

        private void txtMoney_Debt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46) { e.Handled = true; }

        }
    }
}
