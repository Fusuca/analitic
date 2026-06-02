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
    public partial class ETS : Form
    {
        public ETS()
        {
            InitializeComponent();
            comboBox1.Items.Add("USD");
            comboBox1.Items.Add("EUR");
            comboBox1.Items.Add("RUB");

            comboBox1.SelectedIndex = 0;

            //коэффициент alpa
            textBoxAlpha.Text = "0,5";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // очистка
            chart1.Series["Курс"].Points.Clear();
            chart1.Series["ETS"].Points.Clear();
            textBoxRow.Clear();

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

            // alpha
            double alpha = Convert.ToDouble(textBoxAlpha.Text);

            // вывод курса
            for (int i = 0; i < data.Count; i++)
            {
                chart1.Series["Курс"].Points.AddXY(i + 1, data[i]);
                textBoxRow.Text += data[i].ToString() + "   ";
            }

            // первое значение сглаживания
            double smooth = data[0];

            // первая точка сглаживания
            chart1.Series["ETS"].Points.AddXY(1, smooth);

            // расчет экспоненциального сглаживания
            for (int i = 1; i < data.Count; i++)
            {
                // формула сглаживания
                smooth = alpha * data[i] + (1 - alpha) * smooth;

                chart1.Series["ETS"].Points.AddXY(i + 1, smooth);
            }
        }
    }
}
