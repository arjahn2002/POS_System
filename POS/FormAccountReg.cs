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
    public partial class FormAccountReg : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        FormAccountList formList;
        public FormAccountReg(FormAccountList frm)
        {
            InitializeComponent();
            formList = frm;
        }

        private void ClearText()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtContactNo.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPass.Clear();
            cmbRole.SelectedText = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int roleId = 0;
                roleId = cmbRole.SelectedIndex + 1;

                if (txtFname.Text == "" || txtLname.Text == "" ||
                    txtContactNo.Text == "" || txtUsername.Text == "" ||
                    txtPassword.Text == "" || txtConfirmPass.Text == "" ||
                    cmbRole.Text == "")
                {
                    MessageBox.Show("Please fill in all the fields.");
                }
                else
                {
                    db.SP_AddAccount(txtFname.Text, txtLname.Text, txtContactNo.Text, txtUsername.Text, txtPassword.Text, roleId);
                    MessageBox.Show("Account has been successfully added.");
                    ClearText();
                    formList.LoadAccountRecord();
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private void FormAccountReg_Load(object sender, EventArgs e)
        {
            //var roles = db.tblRoles.ToList();
            //cmbRole.DataSource = roles;
            //cmbRole.DisplayMember = "RoleName";
            //cmbRole.ValueMember = "RoleId";

            for (int i = 0; i < cmbRole.Items.Count; i++)
            {
                var indexValue = cmbRole.Items[i];

                Console.WriteLine($"Index: {i}, Value: {indexValue}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to you want to update this account?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int roleIndex = cmbRole.SelectedIndex + 1;
                    var role = cmbRole.SelectedItem;
                    int activeIndex = cmbActive.SelectedIndex;
                    var active = cmbActive.SelectedItem;
                    db.SP_UpdateAccount(int.Parse(lblID.Text), roleIndex, txtUsername.Text, txtPassword.Text, Convert.ToBoolean(activeIndex), txtFname.Text, txtLname.Text, txtContactNo.Text);
                    MessageBox.Show("Account has been successfully updated!");
                    formList.LoadAccountRecord();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
