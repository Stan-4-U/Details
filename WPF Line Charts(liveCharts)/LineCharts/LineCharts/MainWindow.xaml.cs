using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

//使用库新增的头文件
using LiveCharts;
using LiveCharts.Wpf;

namespace LineCharts
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();
           
            this.CenterOnScreen();
        }
        private void CenterOnScreen()
        {
            System.Drawing.Rectangle screenRectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            double screenWidth = screenRectangle.Width;
            double screenHeight = screenRectangle.Height;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            this.Left = (screenWidth - windowWidth) / 2;
            this.Top = (screenHeight - windowHeight) / 2;
        }

        private void s()
        {
            Random random = new Random();
            ChartValues<double> ser = new ChartValues<double>();

            var temp = new double[1024];

            for (int i = 0; i < 1024; i++)
            {
                temp[i] = random.Next(0, 1000);
            }
            ser.AddRange(temp);
            SeriesCollection = new SeriesCollection
            {
                //折线图
                new LineSeries
                {
                    Values =  ser,
                    LineSmoothness=0,
                    StrokeThickness=1,
                    PointGeometrySize=0,
                }
                //直方图
                 //new ColumnSeries
                //{
                //    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                //}
            };
            //DataContext = this;
        }
    }
}
