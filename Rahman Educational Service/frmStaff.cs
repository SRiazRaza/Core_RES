using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
namespace Rahman_Educational_Service
{
    public partial class frmStaff : Form
    {
        public int staffID;

        public frmStaff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SQLConn.adding = true;
            SQLConn.updating = false;
            int init = 0;
            frmAddEditStaff f2 = new frmAddEditStaff(init);
            f2.ShowDialog();
        }
        public void LoadStaffs(string search)
        {
            try
            {
                SQLConn.sqL = "SELECT StaffID, CONCAT(Firstname, ', ', Lastname, ' ') as ClientName, CONCAT(Street, ', ', Area, ', ', City , ', ', Province) as Address, ContactNo, username, role FROM Staff WHERE Firstname LIKE '" + search.Trim() + "%' ORDER By Firstname";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();

                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["StaffId"].ToString());
                    x.SubItems.Add(SQLConn.dr["ClientName"].ToString());
                    x.SubItems.Add(SQLConn.dr["ContactNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["Address"].ToString());
                    x.SubItems.Add(SQLConn.dr["username"].ToString());
                    x.SubItems.Add(SQLConn.dr["Role"].ToString());

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
        private void button2_Click(object sender, EventArgs e)
        {
            if (ListView1.Items.Count == 0)
            {
                Interaction.MsgBox("Please select record to update", MsgBoxStyle.Exclamation, "Update");
                return;
            }
          
           
                try
                {
                    if (string.IsNullOrEmpty(ListView1.FocusedItem.Text))
                    {

                    }
                    else
                    {
                        SQLConn.adding = false;
                        SQLConn.updating = true;
                        staffID = Convert.ToInt32(ListView1.FocusedItem.Text);
                    ListView1.FocusedItem.Focused = false;
                    frmAddEditStaff f2 = new frmAddEditStaff(staffID);
                        f2.ShowDialog();
                    }
                }
                catch
                {
                    Interaction.MsgBox("Please select record to update", MsgBoxStyle.Exclamation, "Update");
                    return;
                }
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SQLConn.strSearch = textBox1.Text;

            if (SQLConn.strSearch.Length >= 1)
            {
                LoadStaffs(SQLConn.strSearch.Trim());
            }
            else if (string.IsNullOrEmpty(SQLConn.strSearch))
            {
                LoadStaffs("");
            }
        }

        private void frmStaff_Load(object sender, EventArgs e)
        {
            LoadStaffs("");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Stream myS = null;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "BackUp File (*.sql)|*.sql";
            saveFileDialog1.RestoreDirectory = true;
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if ((myS = saveFileDialog1.OpenFile()) != null)
                    {

                        SQLConn.ConnDB();
                        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                        MySqlBackup mb = new MySqlBackup(SQLConn.cmd);
                        string file = Path.GetFullPath(saveFileDialog1.FileName);
                        myS.Close();


                        mb.ExportInfo.AddCreateDatabase = true;
                        List<string> abc = new List<string>();

                        abc.Add("daccounts");
                        abc.Add("saccounts");
                        abc.Add("debtin");
                        abc.Add("debtins");
                        abc.Add("payment");
                        abc.Add("transactions");
                        abc.Add("transactiondetails");
                        abc.Add("stockin");
                        abc.Add("staff");
                        abc.Add("product");
                        abc.Add("category");

                        mb.ExportInfo.TablesToBeExportedList = abc;
                        mb.ExportInfo.ExportTableStructure = true;
                        mb.ExportInfo.ExportRows = true;
                        mb.ExportToFile(file);
                        MessageBox.Show("BackUp Successfully Completed.");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BackUp UnSuccessfull. " + ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open Text File";
                theDialog.Filter = "BackUp File (*.sql)|*.sql";
                if (theDialog.ShowDialog() == DialogResult.OK)
                {

                    string file = Path.GetFullPath(theDialog.FileName);
                    SQLConn.ConnDB();
                    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                    MySqlBackup mb = new MySqlBackup(SQLConn.cmd);
                    mb.ImportInfo.TargetDatabase = "rahman_educational_service";
                    mb.ImportInfo.DatabaseDefaultCharSet = "utf8";
                    mb.ImportFromFile(file);
                   // LoadStaffs("");
                    MessageBox.Show("Restoring Successfully Completed.");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Restroring UnSuccessfull. " + ex);
            }
        }
    }
}
