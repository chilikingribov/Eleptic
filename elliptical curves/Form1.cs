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
            textBox10.ReadOnly = true; // запрещаю изменять p в textbox
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b; int p;  double x1, x2, y1, y2;
            try
            {
                a = Convert.ToDouble(textBox1.Text);
                b = Convert.ToDouble(textBox2.Text);
                p = Convert.ToInt32(textBox9.Text);
                x1 = Convert.ToDouble(textBox3.Text);
                x2 = Convert.ToDouble(textBox5.Text);
                y1 = Convert.ToDouble(textBox4.Text);
                y2 = Convert.ToDouble(textBox6.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не заполнили все поля, либо заполнили некорректно! Попробуйте снова! ");
                return;
            }
            

            if (p < 3)
            {
                MessageBox.Show("Неправильно задано p! Попробуйте снова!");
                return;
            }
            else Number7(a, b, p, x1, y1, x2, y2);
            
        }

        private void Number7(double a, double b, int p, double x1, double y1, double x2, double y2)
        {
            if (utensilsLineCurve7(a, b, p, x1, y1) && utensilsLineCurve7(a, b, p, x2, y2))
            {
                SumPoint(a, b, p, x1, y1, x2, y2);
            }
            else
            {
                MessageBox.Show("Одна или обе точки не принадлежат этой кривой");
            }
        }
        
        private void Number8Singular(double a, double b, int p, double x1, double y1, double x2, double y2)
        {
            if(utensilsLineCurve8Singular(a, b, p, x1, y1) && utensilsLineCurve8Singular(a, b, p, x2, y2)) // если точки принадлежат кривой
            {
                SumPointSingular(a, b, p, x1, y1, x2, y2);
            }
            else
            {
                MessageBox.Show("Одна или обе точки не принадлежат этой кривой");

            }

        }

        private void Number8NoSingular(double a, double b, double c,  int p, double x1, double y1, double x2, double y2)
        {

            if (utensilsLineCurve8NoSingular(a, b, c, p, x1, y1) && utensilsLineCurve8NoSingular(a, b, c, p, x2, y2))
            {

            }

        }

        //private int TypeCurv(double a, double b, int p, double x1, double y1, double x2, double y2)
        //{

        //    if()

        //}


        private bool utensilsLineCurve7(double a, double b, int p, double x, double y)
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


        private bool utensilsLineCurve8Singular(double a, double b, int p, double x, double y)
        {
    
            double l, pr;
            l = x * x * x + a * x + b; // просто посчитали без модуля
            pr = y * y+a*y;
            l = PoModul(l, p); // посчитали по модулю
            pr = PoModul(pr, p);


            if (pr == l) // сравнили уже по вычисленному модулю
            {
                return true;
            }
            return false;

        }
        // поменять!! 
        private bool utensilsLineCurve8NoSingular(double a, double b, double c, int p, double x, double y)
        {

            double l, pr;
            l = x * x * x + a * x + b; // просто посчитали без модуля
            pr = y * y + a * y;
            l = PoModul(l, p); // посчитали по модулю
            pr = PoModul(pr, p);


            if (pr == l) // сравнили уже по вычисленному модулю
            {
                return true;
            }
            return false;

        }

        private double PoModul(double ch, int p)
        {
            if (ch >= 0)
            {
                while (ch >= 0)
                {
                    if (ch == 0) return ch;
                    ch -= p;
                }
                ch += p; // т.к смотрим по положительным
            }
            else
            {
                while (ch <= 0)
                {
                    if (ch == 0) return ch;
                    ch += p;
                    
                }
                
            }
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
                    DrawGraph7(x2,y2);
                    printX3Y3(x2, y2);
                    // Рисуем график добавить
                    return;
                }
                else
                {
                    PoModul(x1, p);
                    PoModul(y1, p);
                    DrawGraph7(x1, y1);
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
                DrawGraph7(x3, y3);
                printX3Y3(x3, y3);
            }

            // 4 условие
            if(x1==x2&& y1 == y2 * (-1))
            {
                x3 = 0; y3 = 0;
                DrawGraph7(x3, y3);
                printX3Y3(x3, y3);
            }

            if(x1==x2 && y1==y2)
            {
                x3= Math.Pow((3 * x1 * x1 + a) * ObratniiElement(2 * y1, p), 2)-2*x1;
                y3 = -y1 + ((3 * x1 * x1 + a) * ObratniiElement(2 * y1, p)) * (x1 - x3);
                x3 = PoModul(x3, p);
                y3 = PoModul(y3, p);
                DrawGraph7(x3, y3);
                printX3Y3(x3, y3);
            }

        }


        private void SumPointSingular(double a, double b, int p, double x1, double y1, double x2, double y2)
        {
            double x3, y3;
            //1 условие
            if ((x1 == 0 && y1 == 0) || (x2 == 0 && y2 == 0))
            {
                if (x1 == 0 && y1 == 0)
                {
                    PoModul(x2, p);
                    PoModul(y2, p);
                    DrawGraph8(x2, y2);
                    printX3Y3Singular(x2, y2);
                    // Рисуем график добавить
                    return;
                }
                else
                {
                    PoModul(x1, p);
                    PoModul(y1, p);
                    DrawGraph8(x1, y1);
                    printX3Y3Singular(x1, y1);

                    // Рисуем график добавить
                    return;
                }
            }
            // 3 условие
            if (x1 != x2)
            {
                // Вычесть по модулю???
                x3 = Math.Pow((y2 + y1) * ObratniiElement(x2 + x1, p), 2) + x1 + x2;
                y3 = (a+y1 + ((y2 + y1) * ObratniiElement(x2 + x1, p))) * (x1 + x3);
                x3 = PoModul(x3, p);
                y3 = PoModul(y3, p);
                DrawGraph8(x3, y3);
                printX3Y3Singular(x3, y3);
            }

            // 4 условие
            if (x1 == x2 && y1 == y2 * (-1))
            {
                x3 = 0; y3 = 0;
                DrawGraph8(x3, y3);
                printX3Y3Singular(x3, y3);
            }

            if (x1 == x2 && y1 == y2)
            {
                x3 =(x1 * x1 * x1 * x1 + b * b) * ObratniiElement(a * a, p);
                y3 = (x1*x1+b)* ObratniiElement(a, p)* (x1 + x3) +y1+a;
                x3 = PoModul(x3, p);
                y3 = PoModul(y3, p);
                DrawGraph8(x3, y3);
                printX3Y3Singular(x3, y3);
            }

        }

        private double ObratniiElement(double ch, int p) 
        {
            double obratn; // братный элемент

            // спросить, включаем мы тут p или нет 
            for (int i = 1; i <= p; i++)
            {
                obratn = PoModul(ch, p)*i; 
                
                if (PoModul(obratn, p) == 1) return i;
            }
            return -1;
        }

        private void DrawGraph7(double x1=0, double y1=0)
        {
            // Считываем с формы требуемые значения

            
            double Xmin = x1-5;

            double Xmax = x1;

            double Step = 1;

            double[] x = new double[1];
            double[] y = new double[1];
            x[0] = x1;
            y[0] = y1;



            // Настраиваем оси графика

            chart1.ChartAreas[0].AxisX.Minimum = Xmin;

            chart1.ChartAreas[0].AxisX.Maximum = Xmax;

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Определяем шаг сетки

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Step;
            // добавляем точку
            chart1.Series[0].Points.DataBindXY(x, y);



        }

        private void DrawGraph8(double x1 = 0, double y1 = 0)
        {
            // Считываем с формы требуемые значения


            double Xmin = x1 - 5;

            double Xmax = x1;

            double Step = 1;

            double[] x = new double[1];
            double[] y = new double[1];
            x[0] = x1;
            y[0] = y1;



            // Настраиваем оси графика

            chart2.ChartAreas[0].AxisX.Minimum = Xmin;

            chart2.ChartAreas[0].AxisX.Maximum = Xmax;

            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Определяем шаг сетки

            chart2.ChartAreas[0].AxisX.MajorGrid.Interval = Step;
            // добавляем точку
            chart2.Series[0].Points.DataBindXY(x, y);



        }




        private void printX3Y3(double x3, double y3)
        {
            textBox7.Text = Convert.ToString(x3);
            textBox8.Text = Convert.ToString(y3);
        }

        private void printX3Y3Singular(double x3, double y3)
        {
            textBox18.Text = Convert.ToString(x3);
            textBox17.Text = Convert.ToString(y3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double a, b, c; int p; double x1, x2, y1, y2;
            a = Convert.ToDouble(textBox12.Text);
            b = Convert.ToDouble(textBox11.Text);
            p = Convert.ToInt32(textBox10.Text);
            x1 = Convert.ToDouble(textBox16.Text);
            x2 = Convert.ToDouble(textBox14.Text);
            y1 = Convert.ToDouble(textBox15.Text);
            y2 = Convert.ToDouble(textBox13.Text);

            if (radioButton1.Checked) // если выбрана суперсингулярная кривая
            {
                Number8Singular(a, b, p, x1, y1, x2, y2);

            }
            else
            {
                c = Convert.ToDouble(textBox19.Text);
                Number8NoSingular(a, b, c, p, x1, y1, x2, y2);
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true; // Устанавливает, что первоначально задан радиобатон 1
            radioButton1.PerformClick();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label27.Visible = false; // скрываем lable27
            textBox19.Visible = false; // скрываем, если выбрана категория суперсингулярная кривая
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label27.Visible = true; // скрываем lable27
            textBox19.Visible = true; // скрываем, если выбрана категория несуперсингулярная кривая
        }
    }
}
