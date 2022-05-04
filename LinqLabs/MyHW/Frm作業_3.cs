using LinqLabs;
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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        void AllClear()
        {
            treeView1.Nodes.Clear();
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            listBox1.Items.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            AllClear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            
            foreach (int n in nums)
            {
                if (treeView1.Nodes[Separate(n)] == null)
                {
                    treeView1.Nodes.Add(Separate(n), Separate(n));
                }
                treeView1.Nodes[Separate(n)].Nodes.Add(n.ToString());
                treeView1.Nodes[Separate(n)].Text = $"{Separate(n)}({treeView1.Nodes[Separate(n)].GetNodeCount(true)})";
            }
        }
        private string Separate(int n)
        {
            if (n <= 5)
                return "小";
            else if (n <= 10)
                return "中";
            else
                return "大";
        }

        FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();
        private void button38_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = files.GroupBy(f => MySize(f.Length)).Select(f => new { MySize = f.Key, MyCount = f.Count(), MyGroup = f });
            dataGridView1.DataSource = q.ToList();
            
            foreach (var group in q)
            {
                string s = $"{group.MySize} ({group.MyCount})";
                TreeNode nodes = treeView1.Nodes.Add(group.MySize.ToString(), s);

                foreach (FileInfo item in group.MyGroup)
                {
                    nodes.Nodes.Add(item.ToString());
                }
            }
        }

        private string MySize(long len)
        {
            if (len < 10000)
                return "small";
            else if (len < 50000)
                return "medium";
            else
                return "Large";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = files.GroupBy(f => f.CreationTime.Year).OrderBy(f => f.Key).Select(f => new { MyCreadyear = f.Key, MyCount = f.Count(), Mygroup = f });
            dataGridView1.DataSource = q.ToList();
            
            foreach (var group in q)
            {
                string s = $"{group.MyCreadyear} ({group.MyCount})";
                TreeNode nodes = treeView1.Nodes.Add(group.MyCreadyear.ToString(), s);

                foreach (FileInfo item in group.Mygroup)
                {
                    nodes.Nodes.Add(item.ToString());
                }
            }
        }
        private string SeparatePrice(decimal n)
        {
            if (n <= 15)
                return "低";
            else if (n <= 30)
                return "中";
            else
                return "高";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = dbContext.Products.
                AsEnumerable().
                Where(p => p.UnitPrice != null).
                GroupBy(p => SeparatePrice(p.UnitPrice.Value)).
                Select(p => new
                {
                    p.Key,
                    count = p.Count(),
                    group = p
                });
            dataGridView1.DataSource = q.ToList();
            
            foreach (var price in q)
            {
                TreeNode tree = treeView1.Nodes.Add(price.Key);
                foreach (var item in price.group)
                {
                    string s = $"{item.UnitPrice:c2}-{item.ProductName}";
                    tree.Nodes.Add(item.ToString(), s);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = dbContext.Orders.GroupBy(o => o.OrderDate.Value.Year).Select(o => new { year = o.Key, count = o.Count(), group = o });
            dataGridView1.DataSource = q.ToList();

            
            foreach (var year in q)
            {
                string s = $"{year.year}({year.count})";
                TreeNode tree = treeView1.Nodes.Add(year.year.ToString(), s);
                foreach (var item in year.group)
                {
                    string z = $"{item.OrderID}-{item.OrderDate}";
                    tree.Nodes.Add(item.ToString(), z);
                }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = dbContext.Orders.GroupBy(o => new { o.OrderDate.Value.Year, o.OrderDate.Value.Month }).Select(o => new { Date = o.Key, count = o.Count(), group = o });
            dataGridView1.DataSource = q.ToList();

            
            foreach (var date in q)
            {
                string s = $"{date.Date}({date.count})";
                TreeNode tree = treeView1.Nodes.Add(date.Date.ToString(), s);
                foreach (var item in date.group)
                {
                    string z = $"{item.OrderID}-{item.OrderDate}";
                    tree.Nodes.Add(item.ToString(), z);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
            listBox1.Items.Add($"北風總銷售額為: {dbContext.Order_Details.Sum(o => o.UnitPrice * o.Quantity):c2}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = dbContext.Orders.GroupBy(o => o.Employee.LastName).OrderByDescending(o => o.Count()).Take(5).Select(o=>new { 銷售員ID=o.Key,count=o.Count()});
            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = dbContext.Products.OrderByDescending(o => o.UnitPrice).Take(5).Select(o => new { 類別名稱 = o.Category.CategoryName,產品名稱=o.ProductName, 金額 = o.UnitPrice });
            dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {     
            MessageBox.Show($"是否有單價大於300的產品={dbContext.Products.Any(p => p.UnitPrice > 300)}");
        }
    }
}
