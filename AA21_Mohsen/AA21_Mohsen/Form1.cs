using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AA21_Mohsen
{
    public partial class Form1 : Form
    {
        string[] lines; 
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.Filter = "csv-Files (*.csv)|*.csv|Text-Files (*.txt)|*.txt|Alles|*.*";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] columns;
            lines = File.ReadAllLines(textBox1.Text);

            Console.WriteLine(lines[0]);
            Console.WriteLine(lines[2]); 

            for(int i=1; i<lines.Length; i++)
            {
                columns = lines[i].Split(',');
                Console.WriteLine("Country: " + columns[1]);
                
                if(comboBox1.Items.Contains(columns [1])==false)
                {
                    comboBox1.Items.Add(columns[1]); 
                }
            }
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Country has been selected");
            Console.WriteLine(comboBox1.SelectedItem.ToString());
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();

            chart1.Series[0].Name = comboBox1.SelectedItem.ToString();

            chart1.ChartAreas[0].AxisX.Title = "Date";
            chart1.ChartAreas[0].AxisY.Title = "Total Cases";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;

            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90; 

            for(int i=1; i<lines.Length; i++)
            {
                string country = "";
                country = lines[i].Split(',')[1]; 

                if(country == comboBox1.SelectedItem.ToString())
                {
                    string date = lines[i].Split(',')[0];
                    string totalcases = lines[i].Split(',')[4];

                    Console.WriteLine("Total Cases: " + totalcases);
                    Console.WriteLine("Date: " + date);

                    dataGridView1.Rows.Add(date, totalcases);

                    chart1.Series[0].Points.AddXY(DateTime.Parse(date), int.Parse(totalcases)); 

                }
            }

            chart1.Series[0].Sort(System.Windows.Forms.DataVisualization.Charting.PointSortOrder.Ascending, "X");
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending); 
        }
    }
}
