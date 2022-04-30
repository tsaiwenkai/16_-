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

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(adWdataset1.ProductPhoto);
            CreadComboboxYear();
        }

        private void ShowPicturebox()
        {
            if (dataGridView1.CurrentRow == null)
            {

            }
            else
            {
                byte[] bytes = (byte[])this.dataGridView1.CurrentRow.Cells[3].Value;
                MemoryStream ms = new MemoryStream(bytes);
                pictureBox1.Image = Image.FromStream(ms);
            }
            
        }

        private void CreadComboboxYear()
        {
            var q = from p in adWdataset1.ProductPhoto
                    select p.ModifiedDate.Year;
            
            comboBox3.DataSource = q.Distinct().ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = adWdataset1.ProductPhoto.ToList();
            ShowPicturebox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime x= dateTimePicker1.Value;
            DateTime y = dateTimePicker2.Value;
            dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate >= x & p.ModifiedDate <= y).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBox3.SelectedValue).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBox3.SelectedValue & p.ModifiedDate.Month >= 1 & p.ModifiedDate.Month <= 3).ToList();
            else if(comboBox2.SelectedIndex == 1)
                dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBox3.SelectedValue & p.ModifiedDate.Month >= 4 & p.ModifiedDate.Month <= 6).ToList();
            else if (comboBox2.SelectedIndex == 2)
                dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBox3.SelectedValue & p.ModifiedDate.Month >= 7 & p.ModifiedDate.Month <= 9).ToList();
            else if (comboBox2.SelectedIndex == 3)
                dataGridView1.DataSource = adWdataset1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBox3.SelectedValue & p.ModifiedDate.Month >= 10 & p.ModifiedDate.Month <= 12).ToList();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            ShowPicturebox();
        }
    }
}
