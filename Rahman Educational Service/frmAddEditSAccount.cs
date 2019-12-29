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
    public partial class frmAddEditSAccount : Form
    {
        int LSAccountID;
        int productID;
        bool tell=true;
        string spring = null;
        public frmAddEditSAccount(int accountID)
        {
            InitializeComponent();
            LSAccountID = accountID;
        }
        private void AddStaff()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO saccounts( Name, Area, City, ContactNo,ShopNo, Money_Debt ,form ) VALUES( '" + txtName.Text + "', '" + txtArea.Text + "', '" + txtCity.Text + "', '" + txtContractNo.Text + "', '" + txtShopNo.Text + "', '" + txt_money.Text + "','"+spring+"')";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                //        Interaction.MsgBox("New Account successfully added.", MsgBoxStyle.Information, "Add Account");
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

        private void UpdateStaff()
        {


            try
            {
                SQLConn.sqL = "Update saccounts SET  Name = '" + txtName.Text + "', Area = '" + txtArea.Text + "', City = '" + txtCity.Text + "', ContactNo = '" + txtContractNo.Text + "', ShopNo = '" + txtShopNo.Text + "', Money_Debt = '" + txt_money.Text + "' , form= '"+spring +"' WHERE AccountID = '" + LSAccountID + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                //  Interaction.MsgBox("Account record successfully updated", MsgBoxStyle.Information, "Update Account");

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
        private void LoadUpdateStaff()
        {


            txt_money.ReadOnly = true;

            try
            {
                SQLConn.sqL = "SELECT * FROM saccounts WHERE AccountID = '" + LSAccountID + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblAccountNo.Text = SQLConn.dr["AccountID"].ToString();
                    txtName.Text = SQLConn.dr["Name"].ToString();
                    txtArea.Text = SQLConn.dr["Area"].ToString();
                    txtCity.Text = SQLConn.dr["City"].ToString();
                    txtShopNo.Text = SQLConn.dr["ShopNo"].ToString();
                    txtContractNo.Text = SQLConn.dr["ContactNo"].ToString();
                    txt_money.Text = SQLConn.dr["Money_Debt"].ToString();
                    spring = SQLConn.dr["form"].ToString();

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
        private void GetStaffID()
        {
            try
            {
                SQLConn.sqL = "SELECT AccountID FROM saccounts ORDER BY AccountID DESC";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblAccountNo.Text = (Convert.ToInt32(SQLConn.dr["AccountID"]) + 1).ToString();
                }
                else
                {
                    lblAccountNo.Text = "1";
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



        private void ClearFields()
        {
            lblAccountNo.Text = "";
            txtName.Text = "";
            txtArea.Text = "";
            txtCity.Text = "";
            txtShopNo.Text = "";
            txtContractNo.Text = "";
            txt_money.Text = "";
            listView1.Items.Clear();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SQLConn.adding == true)
            {
                
                AEform();
                AddStaff();
            }
            else
            {
                AEform();
                UpdateStaff();
            }

            if (System.Windows.Forms.Application.OpenForms["frmSchoolAccount"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmSchoolAccount"] as frmSchoolAccount).LoadStaffs("");
            }

            this.Close();
        }
        void loadForm() {


            string[] split = spring.Split(',');
            string asd;

            try
            {
                foreach (string item in split)
                {
                    SQLConn.sqL = "SELECT Description  FROM product  WHERE ProductNo = '" + item + "'";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    if (SQLConn.dr.Read() == true)
                    {

                        asd = SQLConn.dr["Description"].ToString();
                        listView1.Items.Add(asd);
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
        public void AEform() {

            string Fall=null;

            if (listView1.Items.Count == 0)
            {
                spring = null;
            }
            else
            {
                try
                {
                    for (int i = 0; i < listView1.Items.Count; ++i)
                    {
                        string abc = listView1.Items[i].Text.ToString();
                        SQLConn.sqL = "SELECT ProductNo  FROM product  WHERE Description = '" + abc + "'";
                        SQLConn.ConnDB();
                        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                        SQLConn.dr = SQLConn.cmd.ExecuteReader();

                        if (SQLConn.dr.Read() == true)
                        {
                            if (i == 0)
                            {
                                spring = SQLConn.dr["ProductNo"].ToString();
                            }
                            else
                            {
                                Fall = SQLConn.dr["ProductNo"].ToString();
                                spring = string.Concat(spring, ",", Fall);
                            }
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
           
        }
        private void frmAddEditSAccount_Load(object sender, EventArgs e)
        {
            if (SQLConn.adding == true)
            {
                lblTitle.Text = "Adding New Account";
                ClearFields();
                GetStaffID();
            }
            else
            {
                lblTitle.Text = "Updating Account";
              
                LoadUpdateStaff();
                loadForm();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSelectProduct flc = new frmSelectProduct(this);
            flc.ShowDialog();
        }
        public string Product
        {
           // ListViewItem x = null;
           // x = new ListViewItem(SQLConn.dr["ProductCode"].ToString());

            // get {
            //  return snd();
            //   }
     

            set {
                setit(value);
         
            }

        }

        public void setit(string value) {

            if (listView1.Items.Count == 0)
            {
                listView1.Items.Add(value);
            }
            else
            {
                for (int i = 0; i < listView1.Items.Count; ++i)
                {
                    if (value == listView1.Items[i].Text.ToString())
                    {
                       // MessageBox.Show("1");

                        tell = true;
                        break;
                    }
                    else
                    {
                       // MessageBox.Show("2");

                        tell = false;
                    }

                }
                if (tell == false)
                {
                   // MessageBox.Show("3");
                    listView1.Items.Add(value);
                    
                }
            }
            tell = true;

        }
        public int ProductID
        {
            get { return productID; }
            set {productID = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                Interaction.MsgBox("Please select record to Remove", MsgBoxStyle.Exclamation, "Remove");
                return;

            }
 
                try
                {
                    if (String.IsNullOrEmpty(listView1.FocusedItem.Text))
                    {

                    }
                    else
                    {

                        foreach (ListViewItem eachItem in listView1.SelectedItems)
                        {
                            listView1.Items.Remove(eachItem);
                        }
                  //  listView1.FocusedItem.Focused = false;

                }
            }
                catch
                {
                    Interaction.MsgBox("Please select record to Remove", MsgBoxStyle.Exclamation, "Remove");
                    return;
                }

          
        }




    }
}
