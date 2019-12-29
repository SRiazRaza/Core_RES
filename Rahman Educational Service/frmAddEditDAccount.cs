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
    public partial class frmAddEditDAccount : Form
    {
        int LSAccountID;
      
        public frmAddEditDAccount(int accountID)
        {
            InitializeComponent();
            LSAccountID = accountID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SQLConn.adding == true)
            {
                AddStaff();
            }
            else
            {
                UpdateStaff();
            }

            if (System.Windows.Forms.Application.OpenForms["frmDistributorAccount"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmDistributorAccount"] as frmDistributorAccount).LoadStaffs("");
            }

            this.Close();
        }
        private void AddStaff()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO DACCOUNTS( Name, Area, City, ContactNo,ShopNo, Money_Debt ) VALUES( '" + txtName.Text + "', '" + txtArea.Text + "', '" + txtCity.Text + "', '" + txtContractNo.Text + "', '" + txtShopNo.Text + "', '" + txt_money.Text + "')";
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
                SQLConn.sqL = "Update DACCOUNTS SET  Name = '" + txtName.Text + "', Area = '" + txtArea.Text + "', City = '" + txtCity.Text + "', ContactNo = '" + txtContractNo.Text + "', ShopNo = '" + txtShopNo.Text + "', Money_Debt = '" + txt_money.Text + "' WHERE AccountID = '" + LSAccountID + "'";
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
                SQLConn.sqL = "SELECT * FROM DACCOUNTS WHERE AccountID = '" + LSAccountID + "'";
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
                SQLConn.sqL = "SELECT AccountID FROM DACCOUNTS ORDER BY AccountID DESC";
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
            }

        private void frmAddEditDAccount_Load(object sender, EventArgs e)
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

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
