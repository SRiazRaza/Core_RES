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
    public partial class frmPayment : Form
    {
        int InvoiceNo;
        double TotalAmount;
        string Debt;
        string Account;
        string AccNo;
        string fName;
        double rough = 0.00;
        public frmPayment(int invoiceNo, double totalAmount,string money,string a,string n,string fn)
        {
            InitializeComponent();
            InvoiceNo = invoiceNo;
            TotalAmount = totalAmount;
            Debt = money;
            Account = a;
            AccNo = n;
            fName=fn;
          //  MessageBox.Show(Account.ToString() + " " + AccNo.ToString() + " " + fName.ToString());
            
        }
        private void AddPayment(string t,string a,string od,string nd)
        {
            try
            {
               
                    SQLConn.sqL = "INSERT INTO PAYMENT(InvoiceNo, Cash, PChange,AccType,AccNo,OldDebt,NewDebt) VALUES('" + InvoiceNo + "', '" + txtCash.Text.Replace(",", "") + "', '" + txtChange.Text.Replace(",", "") + "', '" + t + "' ,'" + a + "','" + od.Replace(",", "") + "','" + nd.Replace(",", "") + "')";
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

      
        private void frmPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
           
            if (Account == "Counter Sale")
            {
                Debt = "0";
            }
            
           txtTA.Text = Strings.Format(Convert.ToDouble(TotalAmount));

            txDebt.Text = Strings.Format(Convert.ToDouble(Debt));
            
            txtCash.Text = "";
         //   MessageBox.Show(TotalAmount.ToString() + " " + Debt.ToString());



        }
        public void insert_Distributor() {


            try
            {
                SQLConn.sqL = "UPDATE daccounts SET Money_Debt='" + txNewDebt.Text.Replace(",", "") + "',DateIN='" + System.DateTime.Now.ToString("MM-dd-yyyy") + "' where AccountID='" + AccNo + "'";
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

        public void insert_SchoolSale() {

            try
            {
                SQLConn.sqL = "UPDATE saccounts SET Money_Debt='" + txNewDebt.Text.Replace(",", "") + "',DateIN='" + System.DateTime.Now.ToString("MM-dd-yyyy") + "' where AccountID='" + AccNo + "'";
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
        private void txDebt_TextChanged(object sender, EventArgs e)
        {
            if (fName == "frmPOS")
            {
                txtTotal.Text = Strings.Format(Convert.ToDouble(txtTA.Text) + Convert.ToDouble(txDebt.Text), "#,##0.00");
                txtCash.Focus();
            }
            else if (fName == "frmSReturn") {
                txtTotal.Text = Strings.Format(Convert.ToDouble(txDebt.Text)- Convert.ToDouble(txtTA.Text), "#,##0.00");
              
                txtCash.Text = "0";
               

            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {

                double cash = Conversion.Val(txtCash.Text.Replace(",", ""));
                double Bill = Conversion.Val(txtTotal.Text.Replace(",", ""));
           //      MessageBox.Show(Bill.ToString());
            //      MessageBox.Show(cash.ToString());

                if (cash > Bill)
                {
               
                rough = Convert.ToDouble(Conversion.Val(txtCash.Text.Replace(",", "")) - Conversion.Val(txtTotal.Text.Replace(",", "")));
                txtChange.Text= Math.Round(rough, 2).ToString();
                txNewDebt.Text = "0";

                }
                else
                {
                rough= Convert.ToDouble(Conversion.Val(txtTotal.Text.Replace(",", "")) - Conversion.Val(txtCash.Text.Replace(",", "")));
                txNewDebt.Text = Math.Round(rough, 2).ToString();
                txtChange.Text = "0";
                }
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == ControlChars.Cr)
           

            if (e.KeyCode == Keys.Enter && txtCash.Text!="")
            {
                

                if (Account == "Counter Sale")
                {
                    if (Convert.ToDouble(txtTotal.Text.Replace(",", "")) > Convert.ToDouble(txtCash.Text.Replace(",", "")))
                    {
                        Interaction.MsgBox("Insuficient cash to paid the total amount", MsgBoxStyle.Exclamation, "Payment");
                        txtCash.Focus();
                    }
                    else
                    {
                        AddPayment("CS", "0", "0", "0");
                        if (System.Windows.Forms.Application.OpenForms[fName] != null)
                        {
                            SQLConn.payment = "yes";
                            this.Close();                                     ///////////////////////////After Payment
                        }

                        this.Close();
                    }
                }
                else if (Account == "Distributor")
                {
                    AddPayment("DS", AccNo, txDebt.Text, txNewDebt.Text);
                    insert_Distributor();

                    if (System.Windows.Forms.Application.OpenForms[fName] != null)
                    {
                        SQLConn.payment = "yes";
                        this.Close();                                     ///////////////////////////After Payment
                    }

                    this.Close();

                }
                else if (Account == "School Sale")
                {
                    AddPayment("SS", AccNo, txDebt.Text, txNewDebt.Text);
                    insert_SchoolSale();
                    if (System.Windows.Forms.Application.OpenForms[fName] != null)
                    {
                        SQLConn.payment = "yes";
                        this.Close();                                     ///////////////////////////After Payment
                    }

                    this.Close();

                }
                e.SuppressKeyPress = true;   // Stop beep on ENter pressing

            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46) { e.Handled = true; }

        }
    }
}
