using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            //dbContext.Database.Log = Console.Write;
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = dbContext.Products.Where(p => p.UnitPrice > 30).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                                                                                              //父查子
            dataGridView1.DataSource = dbContext.Categories.First().Products.ToList();
                                                                                              //子查父
            MessageBox.Show(dbContext.Products.First().Category.CategoryName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
                                                                          //預存程序直接改為方法使用
            dataGridView1.DataSource = dbContext.Sales_by_Year(new DateTime(1996, 1, 2), DateTime.Now);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products.AsEnumerable()  //只翻譯到這段為止!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 以下記憶體操作
                                                                             //中間,代表then by
                    orderby p.UnitsInStock descending, p.UnitPrice descending
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        Totalprice =$"{p.UnitPrice * p.UnitsInStock:c2}"
                    };
            dataGridView1.DataSource = q.ToList();
            //====================================
            dataGridView2.DataSource = dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenByDescending(p => p.UnitPrice).ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = dbContext.Products.Select(p => new
            {
                p.ProductID,
                p.ProductName,
                p.Category.CategoryID,
                p.Category.CategoryName
            }).ToList();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //dataGridView2.DataSource=dbContext.Products.Join(dbContext.Categories)
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var q = from c in dbContext.Categories
                    from p in dbContext.Products
                    select new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        p.ProductName,
                        p.UnitPrice
                    };
            dataGridView1.DataSource = q.ToList();
            //============================
            //dataGridView2.DataSource=dbContext.Categories.SelectMany()
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbContext.Products.
                GroupBy(p => p.Category.CategoryName).
                Select(p => new
                {
                    Name = p.Key,
                    avg = p.Average(c => c.UnitPrice)
                }).ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbContext.Orders.
                GroupBy(o => o.OrderDate.Value.Year).//允許空直
                Select(o => new
                {
                    year = o.Key,
                    count = o.Count()
                }).ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product pord = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discontinued = true };
            dbContext.Products.Add(pord);
            dbContext.SaveChanges();
        }
    }
}
