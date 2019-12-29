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
    public partial class frmSelectProduct : Form
    {
        private frmAddEditSAccount frmSAccount = null;

        public frmSelectProduct(Form callingForm)
        {
            InitializeComponent();
            frmSAccount = callingForm as frmAddEditSAccount;

        }

        private void frmSelectProduct_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }
        private void LoadCategory()
        {
            try
            {
                SQLConn.sqL = "SELECT * FROM Product WHERE Description LIKE '" + txtCatName.Text + "%' ORDER BY ProductNo ";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();

                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["ProductNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["ProductCode"].ToString());
                    x.SubItems.Add(SQLConn.dr["Description"].ToString());

                    ListView1.Items.Add(x);
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

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(ListView1.FocusedItem.Text);


            this.frmSAccount.ProductID = id;
            this.frmSAccount.Product = ListView1.FocusedItem.SubItems[2].Text;

            this.Close();
        }

        private void txtCatName_TextChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
