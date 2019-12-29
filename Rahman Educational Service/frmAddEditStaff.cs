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
    public partial class frmAddEditStaff : Form
    {
        int LSStaffID;

        public frmAddEditStaff(int staffID)
        {
            InitializeComponent();
            LSStaffID = staffID;

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

            if (System.Windows.Forms.Application.OpenForms["frmStaff"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmStaff"] as frmStaff).LoadStaffs("");
            }

            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GetStaffID()
        {
            try
            {
                SQLConn.sqL = "SELECT StaffID FROM STAFF ORDER BY StaffID DESC";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblProductNo.Text = (Convert.ToInt32(SQLConn.dr["StaffID"]) + 1).ToString();
                }
                else
                {
                    lblProductNo.Text = "1";
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

        private void AddStaff()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO STAFF( Firstname,Lastname, Street, Area, City, Province, ContactNo, Username, Role, UPassword) VALUES( '" + txtFirstname.Text + "','" + txtLastname.Text + "', '" + txtStreet.Text + "', '" + txtArea.Text + "', '" + txtCity.Text + "', '" + txtProvince.Text + "', '" + txtContractNo.Text + "', '" + txtUsername.Text + "', '" + txtRole.Text + "', '" + txtPassword.Text + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                Interaction.MsgBox("New staff successfully added.", MsgBoxStyle.Information, "Add Staff");
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

        private void UpdateStaff()
        {
            try
            {
                SQLConn.sqL = "Update STAFF SET  Firstname = '" + txtFirstname.Text + "',Lastname = '" + txtLastname.Text + "', Street= '" + txtStreet.Text + "', Area = '" + txtArea.Text + "', City = '" + txtCity.Text + "', Province = '" + txtProvince.Text + "', ContactNo = '" + txtContractNo.Text + "', Username ='" + txtUsername.Text + "', Role = '" + txtRole.Text + "', UPassword = '" + txtPassword.Text + "' WHERE StaffID = '" + LSStaffID + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                Interaction.MsgBox("Staff record successfully updated", MsgBoxStyle.Information, "Update Staff");

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

        private void LoadUpdateStaff()
        {
            try
            {
                SQLConn.sqL = "SELECT * FROM STAFF WHERE StaffID = '" + LSStaffID + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblProductNo.Text = SQLConn.dr["StaffID"].ToString();
                    txtFirstname.Text = SQLConn.dr["Firstname"].ToString();
                    txtLastname.Text = SQLConn.dr["lastname"].ToString();


                    txtStreet.Text = SQLConn.dr["Street"].ToString();
                    txtArea.Text = SQLConn.dr["Area"].ToString();
                    txtCity.Text = SQLConn.dr["City"].ToString();
                    txtProvince.Text = SQLConn.dr["Province"].ToString();
                    txtContractNo.Text = SQLConn.dr["ContactNo"].ToString();
                    txtUsername.Text = SQLConn.dr["username"].ToString();
                    txtRole.Text = SQLConn.dr["Role"].ToString();
                    txtPassword.Text = SQLConn.dr["UPassword"].ToString();
                    txtConfirmPWD.Text = SQLConn.dr["UPassword"].ToString();

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
            lblProductNo.Text = "";
            txtLastname.Text = "";
            txtFirstname.Text = "";

            txtStreet.Text = "";
            txtArea.Text = "";
            txtCity.Text = "";
            txtProvince.Text = "";
            txtContractNo.Text = "";
            txtUsername.Text = "";
            txtRole.Text = "";
            txtPassword.Text = "";
            txtConfirmPWD.Text = "";
        }

        private void frmAddEditStaff_Load(object sender, EventArgs e)
        {
            if (SQLConn.adding == true)
            {
                lblTitle.Text = "Adding New Staff";
                ClearFields();
                GetStaffID();
            }
            else
            {
                lblTitle.Text = "Updating Staff";
                LoadUpdateStaff();

            }
        }

        private void txtContractNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46) { e.Handled = true; }

        }
    }
}
