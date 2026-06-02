using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Курсовая_работа
{
    public partial class WMA : Form
    {
        public WMA()
        {
            InitializeComponent();
            comboBox1.Items.Add("USD");
            comboBox1.Items.Add("EUR");
            comboBox1.Items.Add("RUB");

            comboBox1.SelectedIndex = 0;

            // период по умолчанию 
            textBoxPeriod.Text = "3";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            // очистка
            chart1.Series["Курс"].Points.Clear();
            chart1.Series["WMA"].Points.Clear();
            textBox2.Clear();

            // временной ряд
            List<double> data = new List<double>();

            // USD
            if (comboBox1.Text == "USD")
            {
                data.Add(89);
                data.Add(90);
                data.Add(91);
                data.Add(92);
                data.Add(93);
                data.Add(95);
                data.Add(94);
                data.Add(96);
                data.Add(97);
                data.Add(98);
            }

            // EUR
            if (comboBox1.Text == "EUR")
            {
                data.Add(97);
                data.Add(98);
                data.Add(99);
                data.Add(100);
                data.Add(101);
                data.Add(100);
                data.Add(102);
                data.Add(103);
                data.Add(104);
                data.Add(105);
            }

            // RUB
            if (comboBox1.Text == "RUB")
            {
                data.Add(1.0);
                data.Add(1.1);
                data.Add(1.2);
                data.Add(1.3);
                data.Add(1.4);
                data.Add(1.35);
                data.Add(1.5);
                data.Add(1.55);
                data.Add(1.6);
                data.Add(1.7);
            }

            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = data.Count;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            // период
            int period = Convert.ToInt32(textBoxPeriod.Text);

            // вывод курса
            for (int i = 0; i < data.Count; i++)
            {
                chart1.Series["Курс"].Points.AddXY(i + 1, data[i]);
                textBox2.Text += data[i].ToString() + "   ";
            }

            // расчет WMA
            for (int i = period - 1; i < data.Count; i++)
            {
                // сумма
                double sum = 0;

                // сумма весов
                double weightSum = 0;

                // вес
                int weight = 1;

                // цикл расчета
                for (int j = period - 1; j >= 0; j--)
                {
                    sum += data[i - j] * weight;
                    weightSum += weight;
                    weight++;
                }

                // WMA
                double wma = sum / weightSum;

                // вывод WMA
                chart1.Series["WMA"].Points.AddXY(i + 1,wma);
            }
        }
    }
}
