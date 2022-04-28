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

        }
    }
}
