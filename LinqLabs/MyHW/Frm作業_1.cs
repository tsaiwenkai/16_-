using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(dataSet11.Orders);
            Creadcombobox();  
        }

        private void Creadcombobox()
        {
            var q = from da in dataSet11.Orders                    
                    select da.OrderDate.Year;

            comboBox1.ValueMember = "OrderDate";
            comboBox1.DataSource=q.Distinct().ToList();
           
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".log"
                    select f;


           this.dataGridView1.DataSource = q.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.CreationTime.Year == 2017
                    select f;
            dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Length > 10000
                    select f;
            dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataSet11.Orders;     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from da in dataSet11.Orders
                    where da.OrderDate.Year == (int)comboBox1.SelectedValue
                    select da;
            dataGridView1.DataSource = q.ToList();
            int position = ordersBindingSource.Position;
            dataGridView2.DataSource = dataSet11.Orders[position].GetOrder_DetailsRows();

        }
    }
}
