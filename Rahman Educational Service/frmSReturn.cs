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
    public partial class frmSReturn : Form
    {
        int productNo = 0;
        int quantity = 0;
        public int staffId = 0;
        int invoiceNo = 0;
        string Money_Debt;
        DataView AccDV;
        string des = "";
        double totalAmount = 0.00;
        public frmSReturn(int stf)
        {
            InitializeComponent();
            staffId = stf;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadAccount();
            loadInfo();

        }
        public void loadInfo()
        {

            dataGridView1.Rows.Clear();

            if (textBox1.Text != "")
            {
                string i = textBox1.Text;
                try
                {
                    SQLConn.sqL = "SELECT TD.ProductNo,ItemPrice,SUM(Quantity)as Quantity,Discount,SUM(perItem)as perItem,ProductCode,Description,AccType,AccNo FROM transactiondetails as TD ,Product as P, transactions as T WHERE AccName LIKE '" + i + "' AND TD.ProductNo=P.ProductNo AND TD.InvoiceNo=T.InvoiceNo GROUP BY TD.ProductNo,Discount";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    while (SQLConn.dr.Read() == true)
                    {

                        dataGridView1.Rows.Add(SQLConn.dr["ProductNo"].ToString(), SQLConn.dr["ProductCode"].ToString(), SQLConn.dr["Description"].ToString(), SQLConn.dr["ItemPrice"].ToString(), SQLConn.dr["Quantity"].ToString(), SQLConn.dr["Discount"].ToString(), SQLConn.dr["perItem"].ToString(),"0","0");

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Here:"+ex.Message.ToString());
                }
                finally
                {
                    SQLConn.cmd.Dispose();
                    SQLConn.conn.Close();
                }
            }

        }
        public void loadAccount()
        {

            Money_Debt = "";
            if (textBox1.Text != "")
            {

                if (accType.Text == "Counter Sale")
                {
                    accidTxt.Text = "0";

                }

                else if (accType.Text == "Distributor")
                {
                    loadDis();


                }
                else if (accType.Text == "School Sale")
                {
                    loadSchool();


                }

            }

        }
        public void loadDis()
        {

            try
            {
                SQLConn.sqL = "select * FROM daccounts where Name='" + textBox1.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    accidTxt.Text = SQLConn.dr["AccountID"].ToString();
                    Money_Debt = SQLConn.dr["Money_Debt"].ToString();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }
        }
        public void loadSchool()
        {

            try
            {
                SQLConn.sqL = "select * FROM saccounts where Name='" + textBox1.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    accidTxt.Text = SQLConn.dr["AccountID"].ToString();
                    Money_Debt = SQLConn.dr["Money_Debt"].ToString();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stockreturn();

            //  foreach (ListViewItem eachItem in ListView1.CheckedItems)
            //{
            //     eachItem.Remove();
            // }

            // CallAll();
            frmPayment pay = new frmPayment(invoiceNo, totalAmount, Money_Debt, accType.Text, accidTxt.Text,"frmSReturn");
            pay.ShowDialog();


            if (SQLConn.payment == "yes")
            {
                addTransactions();
                addTransactionsDetails();
                frmPrint pri = new frmPrint(invoiceNo,"frmSReturn");
                pri.ShowDialog();
                SQLConn.payment = "";
                if (Interaction.MsgBox("Do you want to perform another transaction?", MsgBoxStyle.YesNo, "New Transaction.") == MsgBoxResult.Yes)
                {
                    clear_trans();

                }
                else
                {
                    this.Close();
                }
            }

            else
            {


            }

        }
        void addTransactions()
        {
            string accT = "";
            string no = "";
            string name = "";
            if (accType.Text == "Counter Sale")
            {

                accT = "CS";
                no = "0";
                name = textBox1.Text;
            }
            else if (accType.Text == "Distributor")
            {
                accT = "DS";
                no = accidTxt.Text;
                name = textBox1.Text;

            }
            else if (accType.Text == "School Sale")
            {
                accT = "SS";
                no = accidTxt.Text;
                name = textBox1.Text;

            }
            else
            {
                MessageBox.Show("Error.");
            }
            try
            {
                SQLConn.sqL = "INSERT INTO transactions(InvoiceNo,TDate,TTime,TotalAmount,StaffID,AccType,AccNo,AccName) VALUES('" + invoiceNo + "', '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "', '" + System.DateTime.Now.ToString("hh:mm:ss") + "' ,'" + totalAmount + "','" + staffId + "','" + accT + "','" + no + "','" + name + "')";
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

        void addTransactionsDetails()
        {
            String quan;
            String itemcode;
            String itemdes;
            String unitP;
            String disc;
            String to;


            productNo = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                  
                    quan = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    if (quan != "" && Convert.ToInt32(quan)>0)
                    {
                       
                        productNo = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());


                        itemcode = dataGridView1.Rows[i].Cells[1].Value.ToString();

                        itemdes = dataGridView1.Rows[i].Cells[2].Value.ToString();

                        unitP = dataGridView1.Rows[i].Cells[3].Value.ToString();


                        disc = dataGridView1.Rows[i].Cells[5].Value.ToString();

                        to = dataGridView1.Rows[i].Cells[8].Value.ToString();




                        SQLConn.sqL = "INSERT INTO transactiondetails(InvoiceNo,ProductNo,ItemPrice,Quantity,Discount,perItem) VALUES('" + invoiceNo + "', '" + productNo + "', '" + unitP + "' ,'" + quan + "','" + disc + "','" + to + "')";
                        SQLConn.ConnDB();
                        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                        SQLConn.cmd.ExecuteNonQuery();


                    }
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


        public void stockreturn()
        {
            productNo = 0;
            quantity = 0;
          
            for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
            {
                if (dataGridView1.Rows[rows].Cells[7].Value.ToString() != "0")
                {

                    quantity = Convert.ToInt32(dataGridView1.Rows[rows].Cells[7].Value.ToString());
                    productNo = Convert.ToInt32(dataGridView1.Rows[rows].Cells[0].Value.ToString());
                    AddStockIn(productNo, quantity);


                }
            }
            
        }


        private void AddStockIn(int pr, int q)
        {
            try
            {
                SQLConn.sqL = "INSERT INTO StockIn(ProductNo, Quantity, DateIn) Values('" + pr + "', '" + q + "', '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();

                Interaction.MsgBox("Stocks successfully added.", MsgBoxStyle.Information, "Add Stocks");

                UpdateProductQuantity();
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

        private void UpdateProductQuantity()
        {
            try
            {
                SQLConn.sqL = "UPDATE Product SET StocksOnhand = StocksOnHand + '" + quantity + "' WHERE ProductNo = '" + productNo + "'";
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

        private void frmSReturn_Load(object sender, EventArgs e)
        {
            gettingInvoice();
            //  gettingStaff();
            accType.Focus();
        }
        void gettingInvoice()
        {
            SQLConn.sqL = "select *  FROM transactions";
            SQLConn.ConnDB();
            SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
            SQLConn.dr = SQLConn.cmd.ExecuteReader();

            try
            {

                while (SQLConn.dr.Read() == true)
                {
                    invoiceNo = Convert.ToInt32(SQLConn.dr["InvoiceNo"]);

                }
                if (invoiceNo == 0)
                {
                    invoiceNo = 100000000;

                    label9.Text = invoiceNo.ToString();
                }
                else if (invoiceNo >= 100000000)
                {
                    int sum = invoiceNo;
                    sum = sum + 1;
                    invoiceNo = sum;
                    label9.Text = invoiceNo.ToString();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // ListView1.Items.Clear();
            dataGridView1.Rows.Clear();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void accType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_trans();
            if (accType.Text != "" || accType.Text == "Counter Sale" || accType.Text == "Distributor" || accType.Text == "School Sale")
            {
                AutoCompleteAccount();
            }
            accType.Focus();

        }

        public void clear_trans()
        {


            dataGridView1.Rows.Clear();
            accidTxt.Text = "";
            textBox1.Text = "";
            label7.Text = "";
            label4.Text = "";
            totalAmount = 0;
            Money_Debt = "";
            des = "";
                
            invoiceNo = 0;
            label9.Text = "";
            gettingInvoice();
        }
        private void accType_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  // To make combo box write comment this and uncomment the following.....

            if (accType.Text != "" || accType.Text == "Counter Sale" || accType.Text == "Distributor" || accType.Text == "School Sale")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AutoCompleteAccount();

                    textBox1.Focus();

                }
            }
        }
        public void AutoCompleteAccount()
        {

            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection accColl = new AutoCompleteStringCollection();

            try
            {

                if (accType.Text == "Distributor")
                {
                    SQLConn.sqL = "SELECT * FROM daccounts ";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    while (SQLConn.dr.Read() == true)
                    {
                        des = SQLConn.dr["Name"].ToString();
                        accColl.Add(des);

                    }

                }
                else if (accType.Text == "School Sale")
                {
                    SQLConn.sqL = "SELECT * FROM saccounts ";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    while (SQLConn.dr.Read() == true)
                    {
                        des = SQLConn.dr["Name"].ToString();
                        accColl.Add(des);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            finally // only checking remove in case of error
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();

            }


            textBox1.AutoCompleteCustomSource = accColl;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AccDV = new DataView();

                AccDV.RowFilter = String.Format("Name LIKE '%{0}%'", textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Account Name not correct ." + ex.ToString());
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }
        }

        private void frmSReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                nextIndex();
            }
        }
        public void nextIndex()
        {

            if (accType.SelectedIndex == 2 || accType.SelectedIndex == -1)
            {
                accType.SelectedIndex = 0;
            }
            else
            {
                if (accType.SelectedIndex < accType.Items.Count - 1)
                {
                    accType.SelectedIndex = accType.SelectedIndex + 1;
                }
            }

        }



        
        void total_item_amount()
        {
            //Adding Total to Total
            int i;
            int sum1 = 0;


            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum1 += Int32.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

            }

            label7.Text = sum1.ToString();

            double sum2 = 0.00;

            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum2 += double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());

            }

            label4.Text = Strings.Format(sum2, "##0.0");
            totalAmount = sum2;

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            


        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                    {
                      //  MessageBox.Show(dataGridView1.Rows[rows].Cells[6].Value.ToString());
                        double ans = 0;
                        double t = Convert.ToDouble(dataGridView1.Rows[rows].Cells[6].Value.ToString());
                        int q = Convert.ToInt32(dataGridView1.Rows[rows].Cells[4].Value.ToString());
                        int ret = Convert.ToInt32(dataGridView1.Rows[rows].Cells[7].Value.ToString());

                        if (ret > 0 && q > 0 && ret<=q)
                        {

                            ans = t / q; 
                            ans = Math.Round(ans, 1);
                          //  MessageBox.Show("1. "+ ans.ToString());
                            ans = ans * ret;
                            ans = Math.Round(ans, 1);
                            dataGridView1.Rows[rows].Cells[8].Value = ans.ToString();
                            total_item_amount();

                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex);
                //  MessageBox.Show(ex.Message.ToString());

            }
        }
    }
}
