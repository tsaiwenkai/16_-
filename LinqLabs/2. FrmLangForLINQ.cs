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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(dataSet11.Products);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            swap(ref n1, ref n2);

            MessageBox.Show(n1 + "," + n2);

            string s1, s2;
            s1 = "aaaa";
            s2 = "bbbb";
            MessageBox.Show(s1 + "," + s2);
            swap(ref  s1, ref s2);
            MessageBox.Show(s1 + "," + s2);

        }
        void swap(ref int x, ref int y)
        {
            int i = x;
            x = y;
            y = i;
        }
        void swap(ref string x, ref string y)
        {
            string i = x;
            x = y;
            y = i;
        }

        void swap(ref object x, ref object y)
        {
            object i = x;
            x = y;
            y = i;
        }


       static  void  swapAnyType<T>(ref T x, ref T y)
        {
            T i = x;
            x = y;
            y = i;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            // swapAnyType<int>(ref n1, ref n2);
            swapAnyType(ref n1, ref n2);// 角括號省略,自動推斷型別

            MessageBox.Show(n1 + "," + n2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //buttonX.Click += ButtonX_Click;

            //c#1.0 具名方法
            buttonX.Click += new EventHandler(aaa);
            buttonX.Click += bbb;

            //=========================
            //c#2.0 匿名方法
            //delegate 是委派 給 沒有方法明子的參數 後面的參數是=evenhandler的固定參數物件
            buttonX.Click += delegate (object sender1, EventArgs e1)
             {
                 MessageBox.Show("c#2.0 匿名方法");
             };
            //C#3.0 匿名方法 lambda
                                         // += ( ) => { }
            buttonX.Click += (object sender1, EventArgs e1) => { MessageBox.Show("c#3.0 匿名方法 lambda"); };


        }

        //private void ButtonX_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("buttonX.Click");
        //}
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        bool test(int x)
        {
            //if (x > 5)
            //    return true;
            //else
            //    return false;
            return x > 5;
        }
        bool test1(int x)
        {
            return x % 2 == 0;
        }
        //step1. ctead delegate
        //step2.cread dalegata obj
        //step3. call method
        delegate bool MYdelegate(int x);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = test(6);
            MessageBox.Show(result.ToString());

            //==================

           MYdelegate del = new MYdelegate(test);
           result= del(7);
            MessageBox.Show(result.ToString());

            //=======================

            del = test1;//語法糖同上
            result = del(8);
            MessageBox.Show(result.ToString());

            //=========================
            //C#2.0 匿名方法
            del = delegate (int x)
             {
                 return x > 5;
             };
            result = del(6);
            MessageBox.Show(result.ToString());


            //=========================
            //C#3.0 匿名方法 lambda
            del = n => n > 5;
            result = del(4);
            MessageBox.Show(result.ToString());

        }
        //========================================================
        //做一個List方法 使用委派當參數

       //List<int> mywher(int[] nm, MYdelegate del) => nm.Where(n => del(n)).ToList();
        List<int> Mywhere(int[]num ,MYdelegate del)
        {
            //new 一個list
            List<int> list = new List<int>();
            
            //迴圈把陣列裡的元素帶進list
            foreach(int n in num)
            {
                //條件  帶入先前作的委派當作布林直
                if (del(n))
                {
                    list.Add(n);
                }         
            }
            return list;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //宣告一個陣列
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //宣告一個list來裝執行方法後的原素
            List<int> result_list =   Mywhere(num,test);
            //foreach(int n in result_list)
            //{
            //    listBox1.Items.Add(n);
            //}
            //=================================


            List<int> list = Mywhere(num, n => n > 5);

            //在新作兩個list來裝使用方法後的回傳list
            List<int> oddlist = Mywhere(num, n=>n % 2 == 1);
                                                                  //lambda寫法 (陣列,匿名方法)  
            List<int> evenlist = Mywhere(num, n => n % 2 == 0);
            foreach (int n in oddlist)
            {
                listBox1.Items.Add(n);
            }
            foreach (int n in evenlist)
            {
                listBox2.Items.Add(n);
            }
        }
        //===============================================
        IEnumerable<int> MyItterator(int[] num, MYdelegate del)
        {

            foreach (int n in num)
            {
                if (del(n))
                {
                    yield return n;
                }          
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q= MyItterator(num, n => n > 5);
            //IEnumerable<int> q = (num, n => n > 5);
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = num.Where(n => n > 5);
            //foreach(int n in q)
            //{
            //    listBox1.Items.Add(n);
            //}

            //===================
            string[] s = { "aaa", "bbbb", "ccccc" };

            var q2 = s.Where(w => w.Length > 3)/*.Select(x=>x.Length).ToList()*/;

            foreach(string x in q2)
            {
                listBox2.Items.Add(x);
            }
            //==============================
            //來源                        條件                          出來會是dataset所以要用select轉成型別最後是變成list
            var q3 = dataSet11.Products.Where(x => x.UnitPrice > 30); //.Select(x=>x.UnitPrice).ToList();
            dataGridView1.DataSource = q3.ToList();

            //foreach(var y in q3)
            //{
            //    listBox1.Items.Add(y);
            //}

        }
          

        private void button45_Click(object sender, EventArgs e)
        {
            var x = 0;
            var y = "asdasda";
            var z = dataSet11.Products;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Mypoint mp = new Mypoint();
            mp.p_1 = 100;
            int w = mp.p_1;

            //MessageBox.Show(w.ToString());

            //Mypoint mpt = new Mypoint("aaa");
            //Mypoint mpt1 = new Mypoint(100);
            //Mypoint mpt2 = new Mypoint(100,200);
            //===================================================

            List<Mypoint> pt = new List<Mypoint>();
            pt.Add(mp);
            pt.Add(new Mypoint("aaa"));
            pt.Add(new Mypoint(100));
            pt.Add(new Mypoint(100, 200));



            pt.Add(new Mypoint { p_1 = 1, p_2 = 1, feild1 = "aaaaaaa", feild2 = "dasdasda" });
            pt.Add(new Mypoint { p_1 = 1,});
            pt.Add(new Mypoint { p_1 = 1, p_2 = 3});
            //dataGridView1.DataSource = pt;
            //=====================================================
            List<Mypoint> list = new List<Mypoint>
            {
                new Mypoint { p_1 = 1, p_2 = 1, feild1 = "aaaaaaa", feild2 = "dasdasda" },
                new Mypoint { p_1 = 2, p_2 = 2, feild1 = "aaaaaaasda", feild2 = "dasdasdasda" },
                new Mypoint { p_1 =3, p_2 = 3, feild1 = "aaaaaaaaaa", feild2 = "dasdasdasdaa" },
                new Mypoint { p_1 =4, p_2 =4, feild1 = "aaaaaaaaaaaaaa", feild2 = "dasdasdaaaaaaaa" },

             };
            //dataGridView2.DataSource = list;
            //=====================================================
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //因為匿名型別所以沒辦法設定變數的型別 故使用var
            var x = new { p1 = 55, p2 = 88 };
            var y = new { p1 = 55, p2 = 88 };
            var z = new { UserName = "sasdasda", Password = "asdasd" };
            listBox1.Items.Add(x.GetType());
            listBox1.Items.Add(y.GetType());
            listBox1.Items.Add(z.GetType());
            //=======================================
            int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in num
            //        where n > 5
            //        select new { N = n, S = n * n, C = n * n * n };

            dataGridView1.DataSource = num.Where(n => n > 5).Select(n=>new { N = n, S = n * n, C = n * n * n }).ToList();

            //dataGridView1.DataSource = q.ToList();

            //===========================================
            //var q = from n in dataSet11.Products
            //        where n.UnitPrice > 30
            //        select new { ID = n.ProductID, 產品名稱 = n.ProductName, n.UnitPrice, n.UnitsInStock, Totelprice = $"{n.UnitPrice * n.UnitsInStock :C2}" };
            //dataGridView2.DataSource = q.ToList();
            dataGridView2.DataSource= dataSet11.Products.
                Where(n => n.UnitPrice > 30).Select(n => new { ID = n.ProductID, 產品名稱 = n.ProductName, n.UnitPrice, n.UnitsInStock, Totelprice = $"{n.UnitPrice * n.UnitsInStock:C2}" }).ToList();
        }


       
        private void button32_Click(object sender, EventArgs e)
        {
            string s1 = "asdasda";
            int n = s1.MyWordcount();
            MessageBox.Show("Wordcount=" + n);
            string s2 = "aasdasdasdasdasda";
            n = s2.MyWordcount();
            MessageBox.Show("Wordcount=" + n);
            //===============================
            string s = "asdasd";
            char c = s.Chars(3);
            MessageBox.Show("Char=" + c);

        }
    }
}
public class Mypoint
{
    public Mypoint()
    {

    }
    public Mypoint(int p1)
    {
        p_1 = p1;
    }
    public Mypoint( int p1, int p2)
    {
        p_1 = p1;
        p_2 = p2;
    }
    public Mypoint(string feild1)
    {

    }

    public string feild1 = "1111",feild2="22222";
    private int m_p1;
    public int p_1
    {
        get
        {
            //m_p1 = m_p1 + 100;
            return m_p1;
        }
        set
        {
            //value = value - 50;
            m_p1 = value;
        }
    }
    public int p_2 { get; set; }
}

//自訂義擴充方法    自訂一個static的類別與方法  方法內的參數一定要this開頭
public static class MyStringExtend
{
    public static int MyWordcount(this string s)
    {
        return s.Length;
    }

    public static char Chars(this string s,int index)
    {
        return s[index];
    }
}
