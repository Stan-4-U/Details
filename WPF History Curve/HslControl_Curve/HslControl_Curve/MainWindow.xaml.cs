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
using System.Windows.Threading;
using HslControls.WPF;

namespace HslControl_Curve
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private WpfAuxiliaryLable auxiliaryLableSSCLL;
        private WpfAuxiliaryLable auxiliaryLableZCLL;
        private DispatcherTimer getCurveTimer = null;
        public float ValueMaxLeft;
        public MainWindow()
        {
            InitializeComponent();
            this.CenterOnScreen();

            hslCurveCLL.SetCurve("瞬时处理量", true, null, Colors.Red, 2f, HslControls.CurveStyle.Curve);
            hslCurveCLL.SetCurve("平均处理量", true, null, Colors.Green, 2f, HslControls.CurveStyle.Curve);

            hslCurveCLL.ValueMaxLeft = 20;

            auxiliaryLableSSCLL = new WpfAuxiliaryLable()
            {
                LocationX = 0.8f,

                Text = "瞬时值：",
                TextBack = Colors.DimGray,
                TextBrush = Colors.White,
            };
            hslCurveCLL.AddAuxiliaryLabel(auxiliaryLableSSCLL);

            auxiliaryLableZCLL = new WpfAuxiliaryLable()
            {
                LocationX = 0.6f,

                Text = "平均值：",
                TextBack = Colors.DimGray,
                TextBrush = Colors.White,
            };
            hslCurveCLL.AddAuxiliaryLabel(auxiliaryLableZCLL);
            ValueMaxLeft = hslCurveCLL.ValueMaxLeft;

            getCurveTimer = new System.Windows.Threading.DispatcherTimer();
            getCurveTimer.Tick += new EventHandler(timerEvent);//处理函数
            getCurveTimer.Interval = new TimeSpan(0, 0, 0, 0,100);//间隔时间
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
        static public float SSCLL;
        static public float ZCLL;

        private void timerEvent(object sender, EventArgs e)
        {
            if (SSCLL >= (ValueMaxLeft - 10))//he 20211222 曲线Y轴修改
            {
                int value;
                value = (int)SSCLL / 50;
                if ((SSCLL - ValueMaxLeft) <= 10)
                { ValueMaxLeft = (value + 2) * 50; }
                else
                { ValueMaxLeft = (value + 1) * 50; }
                hslCurveCLL.ValueMaxLeft = ValueMaxLeft;

            }
            hslCurveCLL.AddCurveData
            (
                new string[] { "瞬时处理量", "平均处理量" },//, "Y"
                new float[]
                {
                    (float)SSCLL,
                    (float)ZCLL,
                    //(float)YS,
                }
            );
            auxiliaryLableSSCLL.Text = "瞬时值：" + Convert.ToInt32(SSCLL).ToString(); ;
            auxiliaryLableZCLL.Text = "平均值：" + Convert.ToInt32(ZCLL).ToString(); ;
            SSCLL++;
            ZCLL++;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            getCurveTimer.Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            getCurveTimer.Stop();
        }
    }
}
