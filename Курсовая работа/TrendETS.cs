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
    public partial class TrendETS : Form
    {
        public TrendETS()
        {
            InitializeComponent();
            comboBox1.Items.Add("USD");
            comboBox1.Items.Add("EUR");
            comboBox1.Items.Add("RUB");

            comboBox1.SelectedIndex = 0;

            //коэффициент alpa
            textBoxAlpha.Text = "0,5";

            //коэффициент beta
            textBoxBeta.Text = "0,3";
        }

        private void buttonTrendETS_Click(object sender, EventArgs e)
        {
            // очистка графика
            chart1.Series["Курс"].Points.Clear();

            chart1.Series["TrendETS"].Points.Clear();

            // очистка временного ряда
            textBoxRow.Clear();

            // список данных
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

            // настройки оси X
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = data.Count;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            // alpha
            double alpha = Convert.ToDouble(textBoxAlpha.Text.Replace(".", ","));

            // beta
            double beta = Convert.ToDouble(textBoxBeta.Text.Replace(".", ","));

            // вывод курса
            for (int i = 0; i < data.Count; i++)
            {
                // график курса
                chart1.Series["Курс"].Points.AddXY(i + 1,data[i]);

                // временной ряд
                textBoxRow.Text +=data[i].ToString() + "   ";
            }

            // начальный уровень
            double level = data[0];

            // начальный тренд
            double trend = data[1] - data[0];

            // первая точка
            chart1.Series["TrendETS"].Points.AddXY(1,level);

            // цикл расчета
            for (int i = 1; i < data.Count; i++)
            {
                // старый уровень
                double oldLevel = level;

                // новый уровень
                level = alpha * data[i] + (1 - alpha) * (level + trend);

                // новый тренд
                trend = beta * (level - oldLevel) + (1 - beta) * trend;

                // прогноз
                double result = level + trend;

                // вывод линии
                chart1.Series["TrendETS"].Points.AddXY(i + 1,result);
            }
        }
    }
}
