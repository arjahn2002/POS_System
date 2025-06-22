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
    public partial class FormCategoryList : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        public FormCategoryList()
        {
            InitializeComponent();
        }

        public void LoadRecord()
        {
            dgvCategoryList.Columns["ColumnID"].DataPropertyName = "CategoryId";
            dgvCategoryList.Columns["ColumnCategory"].DataPropertyName = "CategoryName";

            var list = from db in db.tblCategories
                       select new
                       {
                           db.CategoryId,
                           db.CategoryName
                       };
            dgvCategoryList.DataSource = list.ToList();
        }

        private void FormCategoryList_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            FormCategoryModule frm = new FormCategoryModule(this);
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
        }

        private void dgvCategoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategoryList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FormCategoryModule frm = new FormCategoryModule(this);
                frm.btnSave.Enabled = false;
                frm.lblID.Text = dgvCategoryList.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtCategory.Text = dgvCategoryList.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = int.Parse(dgvCategoryList.Rows[e.RowIndex].Cells[2].Value.ToString());
                    db.SP_DeleteCategory(id);
                    MessageBox.Show("Category has been successfully deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecord();
                }
            }
        }
    }
}
