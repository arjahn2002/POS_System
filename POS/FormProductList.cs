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
    public partial class FormProductList : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        public FormProductList()
        {
            InitializeComponent();
        }

        public void LoadRecord()
        {
            dgvProductList.Columns["ColumnId"].DataPropertyName = "ProductId";
            dgvProductList.Columns["ColumnDescription"].DataPropertyName = "Description";
            dgvProductList.Columns["ColumnPrice"].DataPropertyName = "Price";
            dgvProductList.Columns["ColumnCategory"].DataPropertyName = "Category";
            dgvProductList.Columns["ColumnImage"].DataPropertyName = "Image";
            dgvProductList.Columns["ColumnQty"].DataPropertyName = "Qty";
            var productList = (from db in db.tblProducts
                               select new
                               {
                                   db.ProductId,
                                   db.Description,
                                   db.Price,
                                   db.Category,
                                   db.Image,
                                   db.Qty
                               });
            dgvProductList.DataSource = productList.ToList();
        }

        private void FormProductList_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            FormProductModule frm = new FormProductModule(this);
            frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.ShowDialog();
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProductList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FormProductModule frm = new FormProductModule(this);
                frm.btnSave.Enabled = false;
                frm.lblID.Text = dgvProductList.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtDesc.Text = dgvProductList.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtPrice.Text = dgvProductList.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cmbCategory.Text = dgvProductList.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.LoadCategory();
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = int.Parse(dgvProductList.Rows[e.RowIndex].Cells[2].Value.ToString());
                    db.SP_DELETEPRODUCT(id);
                    MessageBox.Show("Product has been successfully deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecord();
                }
            }
        }
    }
}
