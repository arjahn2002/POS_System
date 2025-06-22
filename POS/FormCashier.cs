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
    public partial class FormCashier : Form
    {
        PosDataClassesDBDataContext db = new PosDataClassesDBDataContext();
        private readonly string firstName;
        public FormCashier(string fName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            firstName = fName;
            lblFname.Text = firstName;
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            timerTime.Start();
        }

        private void LoadCategory()
        {
            GetData();
        }

        private void FormCashier_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void GetData()
        {
            var productList = (from db in db.tblProducts
                               select new
                               {
                                   db.Image,
                                   db.Description,
                                   db.Price,
                               });

            // Display all list in the database
            foreach (var product in productList)
            {
                // Image in database
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = 150;
                pictureBox.Height = 150;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;

                if (product.Image != null && product.Image.Length > 0)
                {
                    byte[] byteArray = product.Image.ToArray();

                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        Image img = Image.FromStream(ms);

                        pictureBox.Image = img;
                    }
                }

                // Price label in database
                Label lblPrice = new Label();
                lblPrice.Text = product.Price.ToString();
                lblPrice.Width = 50;
                lblPrice.BackColor = Color.FromArgb(190, 49, 68);
                lblPrice.ForeColor = Color.White;
                lblPrice.TextAlign = ContentAlignment.MiddleCenter;


                // Description label in database
                Label lblDescription = new Label();
                lblDescription.Text = product.Description;
                lblDescription.BackColor = Color.FromArgb(3, 201, 136);
                lblDescription.TextAlign = ContentAlignment.MiddleCenter;
                lblDescription.Dock = DockStyle.Bottom;

                // Merge the price and description label into picture
                pictureBox.Controls.Add(lblPrice);
                pictureBox.Controls.Add(lblDescription);

                // Display the picturebox into flow layout panel
                var productDetail = pictureBox;
                flpParent.Controls.Add(productDetail);




                var categoryList = (from db in db.tblCategories
                                    select new
                                   {
                                       db.CategoryId,
                                       db.CategoryName
                                   }).ToList();

                flpCategories.Controls.Clear();

                // Looping a button, when admin add another category name
                for (int i = 0; i < categoryList.Count; i++)
                {
                    Button btn = new Button();
                    btn.Width = 195;
                    btn.Height = 50;
                    btn.Text = categoryList[i].CategoryName;
                    btn.BackColor = Color.FromArgb(158, 200, 185);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    flpCategories.Controls.Add(btn);
                }
            }
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss tt").ToUpper();
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
    }
}
