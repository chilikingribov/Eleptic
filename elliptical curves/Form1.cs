using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elliptical_curves
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b; int p;  double x1, x2, y1, y2;
            a = Convert.ToDouble(textBox1.Text);
            b = Convert.ToDouble(textBox2.Text);
            p = Convert.ToInt32(textBox9.Text);
            x1 = Convert.ToDouble(textBox3.Text);
            x2 = Convert.ToDouble(textBox5.Text);
            y1 = Convert.ToDouble(textBox4.Text);
            y2= Convert.ToDouble(textBox6.Text);


            if (utensilsLineCurve(a, b, p, x1, y1) && utensilsLineCurve(a, b, p, x2, y2))
            {
                SumPoint(a, b, p, x1, y1, x2, y2);
            }
            else
            {
                MessageBox.Show("Одна или обе точки не принадлежат этой кривой");
            }
        }

        private bool utensilsLineCurve(double a, double b, int p, double x, double y)
        {
            double l, pr;
            l = x * x * x + a * x + b; // просто посчитали без модуля
            pr = y * y;
            l=PoModul(l, p); // посчитали по модулю
            pr = PoModul(pr, p);


            if (pr == l) // сравнили уже по вычисленному модулю
            {
                return true;
            }
            return false;
           
        }

        private double PoModul(double ch, int p)
        {

            while (ch >= 0)
            {
                ch -= p;
            }
            ch += p; // т.к смотрим по положительным
            return ch;
        }

        private void SumPoint(double a, double b, int p, double x1, double y1, double x2, double y2)
        {
            double x3, y3;
            //1 условие
            if((x1==0 && y1 == 0) || (x2==0 &&y2==0))
            {
                if(x1==0 && y1 == 0)
                {
                    PoModul(x2, p);
                    PoModul(y2, p);
                    printX3Y3(x2, y2);

                    // Рисуем график добавить
                    return;
                }
                else
                {
                    PoModul(x1, p);
                    PoModul(y1, p);
                    printX3Y3(x1, y1);
                    // Рисуем график добавить
                    return;
                }
            }
            // 3 условие
            if (x1 != x2)
            {
                // Вычесть по модулю???
                x3 =  Math.Pow((y2 - y1) * ObratniiElement(x2 - x1, p), 2) -x1-x2;
                y3 = (-y1 + ((y2 - y1) * ObratniiElement(x2 - x1, p))) * (x1 - x3);
                x3= PoModul(x3, p);
                y3= PoModul(y3, p);

                printX3Y3(x3, y3);
            }

            // 4 условие
            if(x1==x2&& y1 == y2 * (-1))
            {
                x3 = 0; y3 = 0;
                printX3Y3(x3, y3);
            }

            if(x1==x2 && y1==y2)
            {
                x3= Math.Pow((3 * x1 * x1 + a) * ObratniiElement(2 * y1, p), 2)-2*x1;
                y3 = -y1 + ((3 * x1 * x1 + a) * ObratniiElement(2 * y1, p)) * (x1 - x3);
                x3 = PoModul(x3, p);
                y3 = PoModul(y3, p);
                // по моему тут нужно по модулю считать дальше
                printX3Y3(x3, y3);
            }

        }

        private double ObratniiElement(double ch, int p)
        {
            double obratn; // братный элемент
            // спросить, включаем мы тут p или нет 
            for (int i = 1; i <= p; i++)
            {
                obratn = PoModul(ch, p);
                if (obratn == 1) return i;
            }
            return -1;
        }

        private void DrawGraph()
        {

        }

        private void printX3Y3(double x3, double y3)
        {
            textBox7.Text = Convert.ToString(x3);
            textBox8.Text = Convert.ToString(y3);
        }
    }
}
