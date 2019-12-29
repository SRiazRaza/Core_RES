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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            SQLConn.getData();


        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                frmDatabaseConfig dc = new frmDatabaseConfig();
                dc.ShowDialog();
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPassword.Focus();
        }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Login();
            }
        }
        private void Login()
        {
            try
            {
                SQLConn.sqL = "SELECT * FROM Staff WHERE Username = '" + txtusername.Text + "' AND UPassword = '" + txtPassword.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                MessageBox.Show(SQLConn.sqL.ToString());
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    if (SQLConn.dr["Role"].ToString().ToUpper() == "ADMIN")
                    {
                        frmMain m = new frmMain(SQLConn.dr["Username"].ToString(), Convert.ToInt32(SQLConn.dr["StaffID"]));
                      //  waitsplash();
                        m.Show();
                        this.Hide();
                    }
                    else
                    {
                        frmMain m = new frmMain(SQLConn.dr["Username"].ToString(), Convert.ToInt32(SQLConn.dr["StaffID"]));
                      //  waitsplash();
                        m.Show();
                        this.Hide();

                    }
                }
                else
                {
                    Interaction.MsgBox("Invalid Password. Please try again.", MsgBoxStyle.Exclamation, "Login");
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
    }
}
