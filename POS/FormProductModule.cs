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
    public partial class FormProductModule : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        FormProductList formList;
        int qty = 0;
        public FormProductModule(FormProductList frm)
        {
            InitializeComponent();
            formList = frm;
        }

        private void ClearText()
        {
            txtDesc.Clear();
            txtPrice.Clear();
        }

        public void LoadCategory()
        {
            var categories = db.tblCategories
                .Select(c => new { c.CategoryId, c.CategoryName })
                .ToList();

            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "CategoryName"; // what shows in dropdown
            cmbCategory.ValueMember = "CategoryId";     // the actual value (ID)
        }

        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            openFileImg.Filter = "Image files (*.png) |*.png|(*.jpg)|*.jpg|(*.gif)|*.gif";
            openFileImg.ShowDialog();
            try
            {
                if (File.Exists(openFileImg.FileName))
                {
                    picboxImg.BackgroundImage = Image.FromFile(openFileImg.FileName);
                    picboxImg.BackgroundImageLayout = ImageLayout.Center;
                    picboxImg.BackgroundImageLayout = ImageLayout.Zoom;
                }
                else
                {
                    MessageBox.Show("File does not exist at the specified path.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, like invalid file format or access issues
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                picboxImg.BackgroundImage.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img = memStream.GetBuffer();
                int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);

                db.SP_ADDPRODUCT(txtDesc.Text, categoryId, img, Convert.ToDecimal(txtPrice.Text), qty);
                formList.LoadRecord();
                ClearText();
                MessageBox.Show("Record saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
