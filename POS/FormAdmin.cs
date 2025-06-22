using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class FormAdmin : Form
    {
        private readonly string firstName;
        public FormAdmin(string fName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            firstName = fName;
            lblFname.Text = firstName;
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            timerTime.Start();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            FormProductList frm = new FormProductList();
            frm.TopLevel = false;
            pnParent.Controls.Add(frm);
            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FormCategoryList frm = new FormCategoryList();
            frm.TopLevel = false;
            pnParent.Controls.Add(frm);
            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            FormAccountList frm = new FormAccountList();
            frm.TopLevel = false;
            pnParent.Controls.Add(frm);
            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            FormSalesList frm = new FormSalesList();
            frm.TopLevel = false;
            pnParent.Controls.Add(frm);
            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void lblLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to logout?", "LOGOUT ACCOUNT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormLogin frm = new FormLogin();
                    this.Hide();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss tt").ToUpper();
        }
    }
}
