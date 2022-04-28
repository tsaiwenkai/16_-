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
            order_DetailsTableAdapter1.Fill(dataSet11.Order_Details);
            productsTableAdapter1.Fill(dataSet11.Products);
            Creadcombobox();  
        }
        bool flag = false;

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
        int cur = 0;
        int num = 0;
        private void button13_Click(object sender, EventArgs e)
        {
           num = Int32.Parse(textBox1.Text);
            cur += num;
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            //if (dataSet11.Products.Where)
            //{
            //    MessageBox.Show("超出索引");
            //}
            //else
            //{
                dataGridView2.DataSource = dataSet11.Products.Where(x=>!x.IsSupplierIDNull()).Skip(cur).Take(num).ToArray();
            //}

        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".log"
                    select f;


           this.dataGridView1.DataSource = q.ToList();
            flag = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.CreationTime.Year == 2017
                    select f;
            dataGridView1.DataSource = q.ToList();
            flag = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Length > 10000
                    select f;
            dataGridView1.DataSource = q.ToList();
            flag = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = dataSet11.Orders;
            flag = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from da in dataSet11.Orders
                    where da.OrderDate.Year == (int)comboBox1.SelectedValue
                    select da;
            dataGridView1.DataSource = q.ToList();
            flag = true;

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (flag == true)
            {
                int point = (int)dataGridView1.CurrentRow.Cells[0].Value;

                var q = from da in dataSet11.Order_Details
                        where da.OrderID == point
                        select da;

                dataGridView2.DataSource = q.ToList();
            }
         
        }

        private void ordersBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            num = Int32.Parse(textBox1.Text);
            cur -= num;
            if (cur < 0)
            {
                cur = 0;
                MessageBox.Show("超出索引");

            }
               
                dataGridView2.DataSource = dataSet11.Products.Skip(cur).Take(num).ToArray();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = dataSet11.Products;
        }
    }
}
