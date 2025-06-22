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
    public partial class FormCategoryModule : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        FormCategoryList formList;
        public FormCategoryModule(FormCategoryList frm)
        {
            InitializeComponent();
            formList = frm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.SP_CreateCategory(txtCategory.Text);
            MessageBox.Show("Successfully Saved!");
            formList.LoadRecord();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
