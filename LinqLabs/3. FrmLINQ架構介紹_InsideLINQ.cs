using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            //因為Arratlist是非泛用性型別所以where不到他 在in的時候後面要加Cast<他不會自動判斷所以要給他型別>()
            //var q = from n in list.Cast<int>()
            //        where n > 2
            //        select new { N = n };
            //dataGridView1.DataSource= q.ToList();
            dataGridView1.DataSource = list.Cast<int>().Where(n => n > 2).Select(n => new { N = n }).ToList();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(dataSet11.Products);
            //var q = (from p in dataSet11.Products
            //         orderby p.UnitPrice descending
            //         select p).Take(5);
            //dataGridView1.DataSource = q.ToList();

            dataGridView1.DataSource = dataSet11.Products.OrderByDescending(p => p.UnitPrice).Take(5).ToList();

        }
    }
}