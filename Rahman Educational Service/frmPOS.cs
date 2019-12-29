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
using System.Media;
namespace Rahman_Educational_Service
{
    public partial class frmPOS : Form
    {
        public int staffId = 0;

        string des = "";

        double totalAmount = 0.00;
        DataView DV;
        DataView AccDV;
        int invoiceNo = 0;
        string Money_Debt;
        string frmS = "";

        public frmPOS(int stf)
        {
            InitializeComponent();
            staffId = stf;
        }

        private void close_b_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void item_des_text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DV = new DataView();

                DV.RowFilter = String.Format("Description LIKE '%{0}%'", item_des_text.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Item Description Name not correct ." + ex.ToString());
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }
        }
        public void AutoCompleteText(bool chec)
        {

            item_des_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            item_des_text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            try
            {

                if (chec == true)
                {
                    SQLConn.sqL = "SELECT * FROM Product ";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    while (SQLConn.dr.Read() == true)
                    {
                        des = SQLConn.dr["Description"].ToString();
                        coll.Add(des);

                    }
                 
                }
                else if (chec == false)
                {
                    string[] split = frmS.Split(',');

                    foreach (string item in split)
                    {
                        SQLConn.sqL = "SELECT *  FROM product  WHERE ProductNo = '" + item + "'";
                        SQLConn.ConnDB();
                        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                        SQLConn.dr = SQLConn.cmd.ExecuteReader();
                        while (SQLConn.dr.Read() == true)
                        {
                            des = SQLConn.dr["Description"].ToString();
                            coll.Add(des);

                        }
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


            item_des_text.AutoCompleteCustomSource = coll;

        }  //// Description textBox , Drop Down Class

        private void remove_b_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count > 0)
                {

                    listView1.Items.Remove(listView1.SelectedItems[0]);
                    total_item_amount();
                    reset_indexing();
                }
            }
            catch
            {
                Interaction.MsgBox("Please select Items to remove.", MsgBoxStyle.Exclamation, "Remove Items");
                return;
            }
        }
        public void reset_indexing() {

            int a = 1;
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                listView1.Items[i].SubItems[0].Text=a.ToString();
                a = a + 1;
            }



        }
        public void clear_transc()
        {
            //barcode_text.Text = "";
            //    Stock_Show.Text = "0";
            accNo.Text = "";
            accName.Text = "";

            item_des_text.Text = "";
            discount_text.Text = "0";
            quantity_text.Text = "1";
            textBox7.Text = "0";
            textBox9.Text = "0.00";
            listView1.Items.Clear();
            label9.Text = "0";
            invoiceNo = 0;
            gettingInvoice();
            gettingStaff();


            
        }

        private void item_des_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (item_des_text.Text == "" || discount_text.Text == "" || quantity_text.Text == "")
                {
                    MessageBox.Show("Error: Empty field.");
                }
                else
                {
                    load_to_cart();
                }
            }
        }
        void load_to_cart()
        {
            try
            {


                SQLConn.sqL = "SELECT * FROM product WHERE Description LIKE '" + item_des_text.Text + "%'  ORDER By Description";

                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;


                while (SQLConn.dr.Read() == true)
                {
                    String quantity = quantity_text.Text;
                    double dco = Convert.ToDouble(discount_text.Text);
                    int check = Convert.ToInt32(SQLConn.dr["StocksOnHand"]);
                    //  Stock_Show.Text = check.ToString();
                    if (Convert.ToInt32(quantity) > check)
                    {

                        MessageBox.Show("Sorry ! You are out of Stock.(or Will be )");
                        goto xy;
                    }


                    x = new ListViewItem(indexing().ToString());
                    x.SubItems.Add(SQLConn.dr["ProductCode"].ToString());
                    x.SubItems.Add(SQLConn.dr["Description"].ToString());
                    x.SubItems.Add(Strings.Format(SQLConn.dr["UnitPrice"], "#,##0.00"));
                    x.SubItems.Add(quantity.ToString());

                    double first = Convert.ToDouble(quantity);
                    double u = SQLConn.dr.GetDouble("UnitPrice");
                    double t = u * first;
                    if (dco > 0)
                    {
                        first = t;
                        first = first / 100;
                        first = first * dco;
                        t = t - first;
                    }

                    x.SubItems.Add(Strings.Format(t, "##0.00"));

                    x.SubItems.Add(dco.ToString());

                    listView1.Items.Add(x);
                }


                total_item_amount();

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }

            //goto statement here
            xy:;



            quantity_text.Text = "1";
            discount_text.Text = "0";


        }
       public int indexing() {
            int a=0;
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
               a = int.Parse(listView1.Items[i].SubItems[0].Text);
               
            }
          
            if (a == 0)
            {
                a = 1;
                return a;
            }
            else {
                a = a + 1;
                return a;

            }
           
            
        }
        void total_item_amount()
        {
            //Adding Total to Total
            int i;
            int sum1 = 0;

            for (i = 0; i < listView1.Items.Count; ++i)
            {
                sum1 += int.Parse(listView1.Items[i].SubItems[4].Text);

            }

            textBox7.Text = sum1.ToString();

            double sum2 = 0.00;

            for (i = 0; i < listView1.Items.Count; ++i)
            {
                sum2 += double.Parse(listView1.Items[i].SubItems[5].Text);

            }

            textBox9.Text = Strings.Format(sum2, "##0.00");
            totalAmount = sum2;

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
        void gettingStaff()
        {

            try
            {

                SQLConn.sqL = "SELECT * FROM Staff WHERE StaffID ='" + staffId + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    textBox2.Text = SQLConn.dr["Role"].ToString();
                    string name = SQLConn.dr["Firstname"].ToString();
                    string last = SQLConn.dr["Lastname"].ToString();
                    name = name + " " + last;
                    textBox4.Text = name;

                }


            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
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
                name = accName.Text;
            }
            else if (accType.Text == "Distributor")
            {
                accT = "DS";
                no = accNo.Text;
                name = accName.Text;

            }
            else if (accType.Text == "School Sale")
            {
                accT = "SS";
                no = accNo.Text;
                name = accName.Text;

            }
            else {
                MessageBox.Show("Error.");
            }
            try
            {
                SQLConn.sqL = "INSERT INTO transactions(InvoiceNo,TDate,TTime,TotalAmount,StaffID,AccType,AccNo,AccName) VALUES('" + invoiceNo + "', '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "', '" + System.DateTime.Now.ToString("hh:mm:ss") + "' ,'" + totalAmount + "','" + staffId + "','" + accT + "','" + no + "','"+name+"')";
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

            try
            {
                for (int i = 0; i < listView1.Items.Count; ++i)
                {
                    String itemcode = listView1.Items[i].SubItems[1].Text;
                    String itemdes = listView1.Items[i].SubItems[2].Text;
                    String unitP = listView1.Items[i].SubItems[3].Text;
                    String quan = listView1.Items[i].SubItems[4].Text;
                    String to = listView1.Items[i].SubItems[5].Text;
                    String disc = listView1.Items[i].SubItems[6].Text;

                    //           MessageBox.Show(itemcode + itemdes + unitP + quan + to + disc);       // For Testing

                    int productNo = getProductNo(itemcode);
                    getquantity(productNo, quan);
                    SQLConn.sqL = "INSERT INTO transactiondetails(InvoiceNo,ProductNo,ItemPrice,Quantity,perItem,Discount) VALUES('" + invoiceNo + "', '" + productNo + "', '" + unitP + "' ,'" + quan + "','" + to + "','" + disc + "')";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.cmd.ExecuteNonQuery();



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
        int getProductNo(string c)
        {

            int productNo = 0;


            try
            {
                SQLConn.sqL = "select ProductNo FROM product where ProductCode LIKE '" + c + "' ";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    productNo = Convert.ToInt32(SQLConn.dr["ProductNo"].ToString());

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex);
            }

            return productNo;


        }

        private void frmPOS_Load(object sender, EventArgs e)
        {

            gettingInvoice();
            gettingStaff();
            accType.Focus();
        }
        void getquantity(int p, string q)
        {
            int s = 0;
            try
            {
                SQLConn.sqL = "SELECT StocksOnHand FROM product WHERE ProductNo ='" + p + "'  ";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    s = Convert.ToInt32(SQLConn.dr["StocksOnHand"]);



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
            cutstock(p, q, s);

        }



        void cutstock(int p, string q, int s)
        {

            s = s - Convert.ToInt32(q);
            try
            {

                SQLConn.sqL = "UPDATE Product SET StocksOnHand ='" + s + "' WHERE ProductNo  = '" + p + "'   ";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);


                SQLConn.cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex);
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();

            }




        }

        private void trans_b_Click(object sender, EventArgs e)
        {
            clear_transc();
            accType.Focus();
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                chng_acc.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                trans_b.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {

                remove_b.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                settle_b.PerformClick();
            }
      
            else if (e.KeyCode == Keys.F5)
            {
                  button2.PerformClick();
            }
        

        }

        private void textBox23_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Money_Debt = "";

            if (accName.Text != "")
            {

                if (accType.Text == "Counter Sale")
                {
                    accNo.Text = "0";
                    AutoCompleteText(true);
                    item_des_text.Focus();

                }

                else if (accType.Text == "Distributor")
                {
                    loadDis();
                    AutoCompleteText(true);

                }
                else if (accType.Text == "School Sale")
                {
                    loadSchool();
                    AutoCompleteText(false);


                }

            }

        }
       
        public void loadDis()
        {

            try
            {
                SQLConn.sqL = "select * FROM daccounts where Name='" + accName.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    accNo.Text = SQLConn.dr["AccountID"].ToString();
                    Money_Debt = SQLConn.dr["Money_Debt"].ToString();
                //    MessageBox.Show(Money_Debt.ToString());
                    item_des_text.Focus();
                }
                else {

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
            frmS = "";
            try
            {
                SQLConn.sqL = "select * FROM saccounts where Name='" + accName.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    accNo.Text = SQLConn.dr["AccountID"].ToString();
                    Money_Debt = SQLConn.dr["Money_Debt"].ToString();
                    frmS = SQLConn.dr["form"].ToString();
                    item_des_text.Focus();

                }
                else {

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

       
        private void chng_acc_Click(object sender, EventArgs e)
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

        private void accType_KeyDow(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  // To make combo box write comment this and uncomment the following.....

            if (accType.Text != "" || accType.Text == "Counter Sale" || accType.Text == "Distributor" || accType.Text == "School Sale")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AutoCompleteAccount();

                    accName.Focus();
               
                }
            }
         

            
        }

      



        private void accName_KeyDown(object sender, KeyEventArgs e)
        {
            if (accName.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button1.PerformClick();
                    //  item_des_text.Focus();
                    e.SuppressKeyPress = true;


                }
            }
            
        }

        private void settle_b_Click(object sender, EventArgs e)
        {
            frmPayment pay = new frmPayment(invoiceNo, totalAmount,Money_Debt,accType.Text,accNo.Text,"frmPOS");
            pay.ShowDialog();

            /// After Payment  control will get here
            /// Control will add Payment to dataBase and after that transactions and transactiondetails
            /// Control will then Print Reciept from last Entry
            
            if (SQLConn.payment == "yes")
            {
                addTransactions();
                addTransactionsDetails();
                frmPrint pri = new frmPrint(invoiceNo,"frmPOS");
                 pri.ShowDialog();
                SQLConn.payment = "";
                if (Interaction.MsgBox("Do you want to perform another transaction?", MsgBoxStyle.YesNo, "New Transaction.") == MsgBoxResult.Yes)
                {
                    trans_b.PerformClick();

                }
                else{
                    this.Close();
                }
            }

            else
            {
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRePrint abc = new frmRePrint();
            abc.ShowDialog();
            
           
        }
        public void AutoCompleteAccount()
        {

            accName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            accName.AutoCompleteSource = AutoCompleteSource.CustomSource;
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


            accName.AutoCompleteCustomSource = accColl;

        }  

        private void accName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AccDV = new DataView();

                AccDV.RowFilter = String.Format("Name LIKE '%{0}%'", accName.Text);
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

     

        private void accType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            clear_transc();

            if (accType.Text != "" || accType.Text == "Counter Sale" || accType.Text == "Distributor" || accType.Text == "School Sale")
            {
                AutoCompleteAccount();
            }
            accType.Focus();
        }
    }
}
