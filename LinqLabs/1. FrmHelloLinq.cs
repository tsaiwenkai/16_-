using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(dataSet11.Products);
            ordersTableAdapter1.Fill(dataSet11.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //語法糖
            foreach (int n in nums)
            {
                listBox1.Items.Add(n);
            }
            listBox1.Items.Add("=============");
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //語法糖
            foreach (var n in list)
            {
                listBox1.Items.Add(n);
            }


            listBox1.Items.Add("================");


            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Step1. define date Source
            //Step2. Define Query
            //Step3. execute Query

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where (n > 5 && n<8) || (n%2==0)
                                 select n;
            foreach(int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n) 
                                 select n;
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private bool IsEven(int n)
        {

            //if (n % 2 == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            //語法糖
            return n % 2 == 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q =from n in nums
                   where n>5
                   select new Point(n, n * n);


            // execute Query
            foreach (Point pt in q)
            {
                listBox1.Items.Add(pt.X+","+pt.Y);
            }
            //------------------------------------------------
            // execute Query
            List < Point >list= q.ToList();
            
            dataGridView1.DataSource = list;

            //----------------------------------------------------
            chart1.DataSource = list;
            chart1.Series[0].XValueMember = "x";
            chart1.Series[0].YValueMembers = "y";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "apple", "xxx", "aaa", "pineapple", "Apple" };
            var q = from w in words
                    where /*w.Contains ("apple") || w.Contains("Apple")*/w.ToLower().Contains("apple") && w.Length>5
                    select w;

            foreach (string f in q)
            {
                listBox1.Items.Add(f);

            }
            //====================================
            List<string> list = q.ToList();
            dataGridView1.DataSource = list;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataSet11.Products;
            // global::LinqLabs using
            /*var*/
            IEnumerable<global::LinqLabs.DataSet1.ProductsRow> q = from da in dataSet11.Products
                                                                       //判斷資料表裡的內容是否為空值! da.IsUnitPriceNull()                                                名子內的開頭為M的
                                                                   where !da.IsUnitPriceNull() && da.UnitPrice > 30 && da.UnitPrice < 50 && da.ProductName.StartsWith("M")
                                                                   select da;
            dataGridView1.DataSource= q.ToList();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from da in dataSet11.Orders
                    where ! da.IsOrderDateNull() && da.OrderDate.Year == 1997 && da.OrderDate.Month>=1 && da.OrderDate.Month <= 3
                    select da;
            dataGridView1.DataSource = q.ToList();
        }
    }
}
