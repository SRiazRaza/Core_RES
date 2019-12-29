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
    public partial class frmFilterTransactions : Form
    {
        public frmFilterTransactions()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((rbCounterSale.Checked == false) && (rbDistributor.Checked == false) && (rbSchoolSale.Checked == false) && (rbDebtIn.Checked == false) && (rbStockIn.Checked == false) && (rbStockOut.Checked == false) && (rbDebtInS.Checked == false))
            {
                Interaction.MsgBox("Please Choose Right Report Generator Option", MsgBoxStyle.Information, "Select Report");
                return;
            }

            if (rbCounterSale.Checked == true)
            {
                rptCounterSale rs = new rptCounterSale(dtpStartDate.Value, dtpEndDate.Value, find.Text);
                rs.ShowDialog();
            }

            if (rbDistributor.Checked == true)
            {
                rptDistributor rs = new rptDistributor(dtpStartDate.Value, dtpEndDate.Value, find.Text);
                rs.ShowDialog();

            }
            if (rbSchoolSale.Checked == true)
            {
               rptSchool abc = new rptSchool(dtpStartDate.Value, dtpEndDate.Value, find.Text);
                abc.ShowDialog();

            }
            if (rbDebtIn.Checked == true)
            {
                rptDebtIn abc = new rptDebtIn(dtpStartDate.Value, dtpEndDate.Value, find.Text);
               abc.ShowDialog();

            }
            if (rbDebtInS.Checked == true)
            {
                rptDebtInS abc = new rptDebtInS(dtpStartDate.Value, dtpEndDate.Value, find.Text);
                abc.ShowDialog();

            }
            if (rbStockIn.Checked == true)
            {
                rptStockIn abc = new rptStockIn(dtpStartDate.Value, dtpEndDate.Value);
                abc.ShowDialog();

            }
            if (rbStockOut.Checked == true)
            {
                rptStockOut abc = new rptStockOut(dtpStartDate.Value, dtpEndDate.Value);
                abc.ShowDialog();

            }
            button3.Focus();

        }
    }
}
