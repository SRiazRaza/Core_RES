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
    public partial class frmDistributorAccount : Form
    {
        public frmDistributorAccount()
        {
            InitializeComponent();
        }
        int staffID;
        private void button1_Click(object sender, EventArgs e)
        {
            SQLConn.adding = true;
            SQLConn.updating = false;
            int init = 0;
            frmAddEditDAccount f2 = new frmAddEditDAccount(init);
            f2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ListView1.Items.Count == 0)
            {
                Interaction.MsgBox("Please select record to update", MsgBoxStyle.Exclamation, "Update");
                return;
            }
            try
            {
                if (String.IsNullOrEmpty(ListView1.FocusedItem.Text))
                {

                }
                else
                {
                    SQLConn.adding = false;
                    SQLConn.updating = true;
                    staffID = Convert.ToInt32(ListView1.FocusedItem.Text);
                    ListView1.FocusedItem.Focused = false;
                    frmAddEditDAccount f2 = new frmAddEditDAccount(staffID);
                    f2.ShowDialog();
                }
            }
            catch
            {
                Interaction.MsgBox("Please select record to update", MsgBoxStyle.Exclamation, "Update");
                return;
            }
        }

        private void btnStocksIn_Click(object sender, EventArgs e)
        {
            if (ListView1.Items.Count == 0)
            {
                Interaction.MsgBox("Please select record to add Debt", MsgBoxStyle.Exclamation, "DebtIn");
                return;
            }
            try
            {
                if (String.IsNullOrEmpty(ListView1.FocusedItem.Text))
                {

                }
                else
                {

                    int account = Convert.ToInt32(ListView1.FocusedItem.Text);
                    ListView1.FocusedItem.Focused = false;
                    frmDebtIn aeP = new frmDebtIn(account);
                    aeP.ShowDialog();
                }
            }
            catch
            {
                Interaction.MsgBox("Please select record to add Debt", MsgBoxStyle.Exclamation, "DebtIn");
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SQLConn.strSearch = textBox1.Text;

            if (SQLConn.strSearch.Length >= 1)
            {
                LoadStaffs(SQLConn.strSearch.Trim());
            }
            else if (String.IsNullOrEmpty(SQLConn.strSearch))
            {
                LoadStaffs("");
            }
        }

        private void frmDistributorAccount_Load(object sender, EventArgs e)
        {
            /*if (System.Windows.Forms.Application.OpenForms["frmAccountMenu"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmAccountMenu"] as frmAccountMenu).closefunction();


            }
            else {
                MessageBox.Show("asdasd");

            }*/

          //  frmAccountMenu newform = (frmAccountMenu)Application.OpenForms["frmAccountMenu"];
         //   newform.Close();

            LoadStaffs("");
        }
        public void LoadStaffs(String search)
        {
            try
            {
                SQLConn.sqL = "SELECT AccountID, Name, CONCAT( Area, ', ', City) as Address, ContactNo,ShopNo,Money_Debt,DateIN FROM daccounts WHERE NAME LIKE '" + search.Trim() + "%' ORDER By AccountID";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();

                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["AccountId"].ToString());
                    x.SubItems.Add(SQLConn.dr["Name"].ToString());
                    x.SubItems.Add(SQLConn.dr["Money_debt"].ToString());
                    x.SubItems.Add(SQLConn.dr["ContactNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["Address"].ToString());
                    x.SubItems.Add(SQLConn.dr["ShopNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["DateIN"].ToString());


                    ListView1.Items.Add(x);
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

        private void frmDistributorAccount_Deactivate(object sender, EventArgs e)
        {
           // Control.Focus=true;
          //  ListView1.FocusedItem.Focused = false;

        }
    }
}
