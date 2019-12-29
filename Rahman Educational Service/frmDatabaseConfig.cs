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
    public partial class frmDatabaseConfig : Form
    {
        private string TstServerMySQL;
        private string TstPortMySQL;
        private string TstUserNameMySQL;
        private string TstPwdMySQL;
        private string TstDBNameMySQL;

        public frmDatabaseConfig()
        {
            InitializeComponent();
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            //Test database connection

            TstServerMySQL = txtServerHost.Text;
            TstPortMySQL = txtPort.Text;
            TstUserNameMySQL = txtUserName.Text;
            TstPwdMySQL = txtPassword.Text;
            TstDBNameMySQL = txtDatabase.Text;


            try
            {
                SQLConn.conn.ConnectionString = "Server = '" + TstServerMySQL + "';  " + "Port = '" + TstPortMySQL + "'; " + "Database = '" + TstDBNameMySQL + "'; " + "user id = '" + TstUserNameMySQL + "'; " + "password = '" + TstPwdMySQL + "'";


                SQLConn.conn.Open();
                Interaction.MsgBox("Test connection successful", MsgBoxStyle.Information, "Database Settings");

            }
            catch
            {
                Interaction.MsgBox("The system failed to establish a connection", MsgBoxStyle.Information, "Database Settings");

            }
            SQLConn.DisconnMy();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            TstServerMySQL = txtServerHost.Text;
            TstPortMySQL = txtPort.Text;
            TstUserNameMySQL = txtUserName.Text;
            TstPwdMySQL = txtPassword.Text;
            TstDBNameMySQL = txtDatabase.Text;

            try
            {
                SQLConn.conn.ConnectionString = "Server = '" + TstServerMySQL + "';  " + "Port = '" + TstPortMySQL + "'; " + "Database = '" + TstDBNameMySQL + "'; " + "user id = '" + TstUserNameMySQL + "'; " + "password = '" + TstPwdMySQL + "'";
                SQLConn.conn.Open();

                SQLConn.DBNameMySQL = txtDatabase.Text;
                SQLConn.ServerMySQL = txtServerHost.Text;
                SQLConn.UserNameMySQL = txtUserName.Text;
                SQLConn.PortMySQL = txtPort.Text;
                SQLConn.PwdMySQL = txtPassword.Text;

                SQLConn.SaveData();
                this.Close();
            }
            catch
            {
                Interaction.MsgBox("The system failed to establish a connection", MsgBoxStyle.Information, "Database Settings");
            }
            SQLConn.DisconnMy();
            //save database
        }

        private void frmDatabaseConfig_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(178, 127);
            txtServerHost.Text = SQLConn.ServerMySQL;
            txtPort.Text = SQLConn.PortMySQL;
            txtUserName.Text = SQLConn.UserNameMySQL;
            txtPassword.Text = SQLConn.PwdMySQL;
            txtDatabase.Text = SQLConn.DBNameMySQL;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
