using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //============================================
            //可列舉的      每個group裡面的< int group , group裡面的int >
            IEnumerable<IGrouping<string, int>> q = from n in num
                                                 group n by n % 2 == 0 ? "偶數" : "奇數";                          // n % 2;
            dataGridView1.DataSource = q.ToList();
            //dataGridView1.DataSource = num.GroupBy(p => p % 2).ToList();
            //============================================

            //外迴圈把q帶進變數group分類
            foreach (var group in q)
            {
                //把group(q)裡面的key(屬性)分成node
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString());

                //內迴圈把分類進group的屬性+子node
                    foreach(var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            //==================================================
            foreach (var group in q)
            {
                ListViewGroup lvg=listView1.Groups.Add(group.Key.ToString(), group.Key.ToString());
                //內迴圈把分類進group的屬性+子node
                foreach (var item in group)
                {
                                                                                    //加完的item 要加入哪個group
                    listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //因為匿名型別所以只能用var
            var q = from n in num
                        //如果要做運算只能into近一個記憶體然後再做運算
                    group n by n % 2 == 0 ? "偶數" : "奇數" into G
                    select new
                    {
                        MyKey = G.Key,
                        Mycount = G.Count(),
                        MyMin = G.Min(),
                        MyMax = G.Min(),
                        //設一個Mygroup來接所有的屬性 再用treeview帶出來
                        Mygroup=G
                    };         
            dataGridView1.DataSource = q.ToList();

             foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.Mycount})";
                                                                                     //Key已經設定成G裡面的key,後面的參數為父節點的外觀要呈現的字串
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(),s);

                //內迴圈把分類進group的屬性+子node
                    foreach(var item in group.Mygroup)
                {
                    node.Nodes.Add(item.ToString());
                }
}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var q = from n in num
                        //做一個方法來判定n的大小(分成小中大三類)
                    group n by MyKey(n) into G
                    select new
                    {
                        MyKey = G.Key,
                        Mycount = G.Count(),
                        MyMin = G.Min(),
                        MyMax = G.Min(),
                        MyAvg = G.Average(),
                        Mygroup = G
                    };
           
            dataGridView1.DataSource = q.ToList();
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.Mycount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var item in group.Mygroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=======================================
            chart1.DataSource = q.ToList();
                                                                            //設定x軸的Key名字
            chart1.Series[0].XValueMember = "MyKey";
                                                                            //設定y軸的名字
            chart1.Series[0].YValueMembers = "MyCount";
                                                                           //設定圖表顯示的型態
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "MyAvg";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



        }
        private string MyKey(int n)
        {
            if (n < 5)
                return "small";
            else if (n == 5)
                return "medum";
            else
                return "Large";
        }

        private void button31_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] file = dir.GetFiles();
            //dataGridView1.DataSource = file;
                                                                                 //設定group那些欄位     select 設一個名稱來裝前面的key   在count 總數
            dataGridView1.DataSource = file.GroupBy(n => n.Extension).OrderByDescending(n=>n.Count()).Select(s => new { Key = s.Key, Count = s.Count()}).ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ordersTableAdapter1.Fill(dataSet11.Orders);

            dataGridView1.DataSource = dataSet11.Orders.
                GroupBy(o => o.OrderDate.Year).
                OrderBy(o => o.Key).
                Select(o => new { year = o.Key, count = o.Count() }).
                ToList();

            dataGridView2.DataSource = dataSet11.Orders.Where(o => o.OrderDate.Year == 1997).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] file = dir.GetFiles();

            var q =( from n in file
                    let s = n.Extension
                    where s == ".exe"
                    select n).Count();
            MessageBox.Show(q.ToString());

            //MessageBox.Show(file.Where(n=>n.Extension))
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "I Have Pen , I Have Apple , Un ApplePen";

            char[] chars = { ' ', ',', '.' };

            string[] words =s.Split(chars,StringSplitOptions.RemoveEmptyEntries);

            var q = from n in words
                    group n by n into g
                    select new { MyKey = g.Key, MyCount = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] num1 = { 1, 45, 13, 1, 2, 4, 5, 1, };
            int[] num2 = { 1,5,2,3,6,1,2,4};
            IEnumerable<int> q;
            q = num1.Intersect(num2);//交集
            q = num1.Distinct();//去重複
            q = num1.Union(num2);//重疊
            //====================================
            bool res;
            res = q.All(n=>n>1);  //陣列裡面的所有數有沒有>1
            res = q.Any(n => n >= 1); //陣列裡面的任何一個數有沒有>1
            res = q.Contains(1);//陣列裡面數有沒有包含1
            //=========================================

            int n1;
            n1 = num1.First();//陣列的第一個元素
            n1 = num1.Last();//陣列的第2個元素
            //n1 = num1.ElementAt(100);  陣列中的索引第幾個  會出錯沒有第100個
            n1 = num1.ElementAtOrDefault(100);  //出錯會直接變為預設值
            //=============================================

            var q1 = Enumerable.Range(1, 1000).Select(n => new { n }).ToList();  //產生1++ 到1000個
            dataGridView1.DataSource = q1;
            var q2 = Enumerable.Repeat(122, 1000).Select(n => new { N = n }).ToList(); //產生122*1000個
            dataGridView2.DataSource = q2;

            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(dataSet11.Products);
            dataGridView1.DataSource = dataSet11.Products.
                GroupBy(p => p.CategoryID).
                Select(p => new { CategoryID = p.Key, Avg = p.Average(u=> u.UnitPrice) }).
                ToList();
            //==========================================
            categoriesTableAdapter1.Fill(dataSet11.Categories);

            var q = from c in dataSet11.Categories
                    join p in dataSet11.Products
                    on c.CategoryID equals p.CategoryID

                    group p by c.CategoryName into g
                    select new { CategoryID = g.Key, Avg = g.Average(u => u.UnitPrice) };
            dataGridView2.DataSource = q.ToList();


            //dataGridView2.DataSource=dataSet11.Categories
        }
    }
}
