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
        
        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach(int n in nums)
            {
                if (treeView1.Nodes[Separate(n)] == null)
                {
                    treeView1.Nodes.Add(Separate(n), Separate(n));                             
                }
                treeView1.Nodes[Separate(n)].Nodes.Add(n.ToString());
            }
        }

        private  string Separate(int n)
        {
            if (n <=5)
                return "小";
            else if (n <= 10)
                return "中";
            else
                return "大";
        }

        FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();
        private void button38_Click(object sender, EventArgs e)
        {
           var q = files.GroupBy(f => MySize(f.Length)).Select(f=>new { MySize = f.Key,MyCount=f.Count(),MyGroup=f});
            dataGridView1.DataSource = q.ToList();
            treeView1.Nodes.Clear();
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
           
            var q = files.GroupBy(f=>f.CreationTime.Year).OrderBy(f=>f.Key).Select(f=>new { MyCreadyear = f.Key, MyCount = f.Count(), Mygroup=f});
            dataGridView1.DataSource = q.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.MyCreadyear} ({group.MyCount})";
                TreeNode nodes = treeView1.Nodes.Add(group.MyCreadyear.ToString(),s);

                foreach (FileInfo item in group.Mygroup)
                {
                    nodes.Nodes.Add(item.ToString());
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
           // dataGridView2.DataSource = files.Where(f => f.CreationTime.Year == (int)sender).ToList();
        }
    }
}
