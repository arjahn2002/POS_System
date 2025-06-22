using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class FormAccountList : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        public FormAccountList()
        {
            InitializeComponent();
        }

        public void LoadAccountRecord()
        {
            dgvAccountList.Columns["ColumnUserId"].DataPropertyName = "UserId";
            dgvAccountList.Columns["ColumnRoleId"].DataPropertyName = "RoleId";
            dgvAccountList.Columns["ColumnFname"].DataPropertyName = "FirstName";
            dgvAccountList.Columns["ColumnLname"].DataPropertyName = "LastName";
            dgvAccountList.Columns["ColumnContactNo"].DataPropertyName = "ContactNo";
            dgvAccountList.Columns["ColumnUsername"].DataPropertyName = "Username";
            dgvAccountList.Columns["ColumnPassword"].DataPropertyName = "Password";
            dgvAccountList.Columns["ColumnActive"].DataPropertyName = "IsActive";
            var userAccountList = (from db in db.tblAccounts
                            select new
                            {
                                db.UserId,
                                db.RoleId,
                                db.Username,
                                db.Password,
                                db.IsActive
                            });

            var userInfoList = (from db in db.tblUsers
                            select new
                            {
                                db.UserId,
                                db.FirstName,
                                db.LastName,
                                db.ContactNo
                            });

            var accountList = (from account in userAccountList
                                join userInfo in userInfoList on account.UserId equals userInfo.UserId
                                select new
                                {
                                    account.UserId,
                                    account.RoleId,
                                    userInfo.FirstName,
                                    userInfo.LastName,
                                    userInfo.ContactNo,
                                    account.Username,
                                    account.Password,
                                    account.IsActive
                                });

            dgvAccountList.DataSource = accountList.ToList();
        }

        private void FormAccountList_Load(object sender, EventArgs e)
        {
            LoadAccountRecord();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            FormAccountReg frm = new FormAccountReg(this);
            frm.btnUpdate.Enabled = false;
            frm.cmbActive.Visible = false;
            frm.lblActive.Visible = false;
            frm.ShowDialog();
        }

        private void dgvAccountList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvAccountList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FormAccountReg frm = new FormAccountReg(this);
                frm.btnSave.Enabled = false;
                frm.lblID.Text = dgvAccountList.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.lblRole.Text = dgvAccountList.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtFname.Text = dgvAccountList.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtLname.Text = dgvAccountList.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtContactNo.Text = dgvAccountList.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtUsername.Text = dgvAccountList.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.txtPassword.Text = dgvAccountList.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.txtConfirmPass.Text = dgvAccountList.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.cmbActive.Text = dgvAccountList.Rows[e.RowIndex].Cells[9].Value.ToString();
                frm.cmbActive.Visible = true;
                frm.lblActive.Visible = true;
                frm.ShowDialog();
            }
            else if (colName == "ColumnDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this account?", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = int.Parse(dgvAccountList[2, e.RowIndex].Value.ToString());
                    db.SP_DeleteAccount(id);
                    MessageBox.Show("Account has been successfully deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccountRecord();
                }
            }
        }
    }
}
