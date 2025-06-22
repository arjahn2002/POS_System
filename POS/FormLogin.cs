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
    public partial class FormLogin : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void ClearText()
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userAcc = db.tblAccounts.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);
            
            // Trigger when the textboxes are null
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill in the blanks.");
                ClearText();
            }
            // Trigger when the username and password are correct
            else if (userAcc != null)
            {
                // Get a first name and pass to forms
                var userInfo = db.tblUsers.FirstOrDefault(u => u.UserId == userAcc.UserId);
                string firstName = userInfo.FirstName;

                //Trigger when the active of user are true
                if (userAcc.IsActive.HasValue && userAcc.IsActive.Value)
                {
                    string roleId = userAcc.RoleId.ToString();

                    if (roleId == "1") // Admin
                    {
                        FormAdmin frm = new FormAdmin(firstName);
                        this.Hide();
                        frm.Show();
                    }
                    else if (roleId == "2") // Cashier
                    {
                        FormCashier frm = new FormCashier(firstName);
                        this.Hide();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid role for this user.");
                        ClearText();
                    }
                }
                else
                {
                    MessageBox.Show("This account is not active.");
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
                ClearText();
            }

        }

        private void picPassHide_Click(object sender, EventArgs e)
        {
            picPassHide.Visible = false;
            picPassShow.Visible = true;
            txtPassword.PasswordChar = '●';
        }

        private void picPassShow_Click(object sender, EventArgs e)
        {
            picPassHide.Visible = true;
            picPassShow.Visible = false;
            txtPassword.PasswordChar = '\0';
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
