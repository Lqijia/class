using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SensorLibrary;

namespace SensorSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 串口辅助类
        /// </summary>
        ComHelper com = null;

        /// <summary>
        /// 命令实体
        /// </summary>
        CommandModel cmd = null;

        /// <summary>
        /// 是否第一次执行
        /// </summary>
        bool _isfirst = true;

        /// <summary>
        /// 计时器
        /// </summary>
        Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        /// <summary>
        /// 窗体卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (com != null)
            {
                timer.Stop();
                com.Close();
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cmd = new CommandModel();
            //串口
            string comStr = System.Configuration.ConfigurationManager.AppSettings["ComStr"];
            //波特率
            int baudrate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BaudRate"]);
            com = new ComHelper(comStr, baudrate);
            if (!com.Open())
            {
                return;
            }

            timer = new Timer(200);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            pl.Points = com.GetPoints();
        }

        /// <summary>
        /// 轮循事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_isfirst)
            {
                SendStart();
                _isfirst = false;
            }
            SendNTC();
            SendPressurePoint();
            SendComparatorOutput();
        }

        /// <summary>
        /// 发送开始
        /// </summary>
        private void SendStart()
        {
            byte[] data = cmd.Init;
            com.Write(data, 0, 6);

        }

        /// <summary>
        /// 发送及显示NTC值
        /// </summary>
        private void SendNTC()
        {
            byte[] data = cmd.NTCInit;
            com.Write(data, 0, 6);

            byte[] dataReturn = com.GetData();
            if (dataReturn != null && dataReturn.Length > 0)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    prbNTC.Value = dataReturn[0] / 2.55;

                    var tmpv = Conversion.GetValue(dataReturn[0]);

                    lblNTC.Content = dataReturn[0];

                    lh1.Y2 = lv.Y1 = lv.Y2 = 276 - tmpv * 5;

                    lv.X2 = lh1.X1 = lh1.X2 = 70 + ((3950 * 298.85) / ((Math.Log(tmpv * 0.1) * 298.15) + 3950) - 273.15) * 5;

                }));
            }
        }

        /// <summary>
        /// 发送压力点
        /// </summary>
        private void SendPressurePoint()
        {
            byte[] data = cmd.PressurePointInit;
            com.Write(data, 0, 6);

            byte[] dataReturn = com.GetData();
            if (dataReturn != null && dataReturn.Length > 0)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    prbPressurePoint.Value = dataReturn[0] / 2.55;
                    var tmpv = Conversion.GetValue(dataReturn[0]);

                    lblPressurePoint.Content = dataReturn[0].ToString();

                    var xss = ((70 + tmpv * 5) > 475 ? 475 : (70 + tmpv * 5));
                    lh.X2 = lh.X1 = xss;

                }));
            }
        }

        /// <summary>
        /// 发送比较器输出
        /// </summary>
        private void SendComparatorOutput()
        {
            byte[] data = cmd.ComparatorInit;
            com.Write(data, 0, 6);

            byte[] dataReturn = com.GetData();
            if (dataReturn != null && dataReturn.Length > 0)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    if (lh1.X1 > lh.X1)
                    {
                        imgComparatorOutputValue.Source = new BitmapImage(new Uri("Resources/comparer_0.png", UriKind.Relative));
                        imgHeater.Source = new BitmapImage(new Uri("Resources/warm_off.png", UriKind.Relative));
                        imgFan.Visibility = System.Windows.Visibility.Hidden;
                        gifFan.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        imgComparatorOutputValue.Source = new BitmapImage(new Uri("Resources/comparer_1.png", UriKind.Relative));
                        imgHeater.Source = new BitmapImage(new Uri("Resources/warm_on.png", UriKind.Relative));
                        imgFan.Visibility = System.Windows.Visibility.Visible;
                        gifFan.Visibility = System.Windows.Visibility.Hidden;
                    }
                }));
            }
        }

        /// <summary>
        /// 发送开NTC
        /// </summary>
        private void SendOnNTC()
        {
            byte[] data = cmd.OnNTC;
            com.Write(data, 0, 6);
        }
        /// <summary>
        ///  发送开压力点
        /// </summary>
        private void SendOnPressurePoint()
        {
            byte[] data = cmd.OnPressurePoint;
            com.Write(data, 0, 6);
        }

        /// <summary>
        /// 发送开比较器输出
        /// </summary>
        private void SendOnComparatorOutput()
        {
            byte[] data = cmd.OnComparator;
            com.Write(data, 0, 6);
        }

        /// <summary>
        /// 发送关NTC
        /// </summary>
        private void SendOffNTC()
        {
            byte[] data = cmd.OffNTC;
            com.Write(data, 0, 6);
        }

        /// <summary>
        /// 发送关压力点
        /// </summary>
        private void SendOffPressurePoint()
        {
            byte[] data = cmd.OffPressurePoint;
            com.Write(data, 0, 6);
        }
        /// <summary>
        /// 发送关比较器输出
        /// </summary>
        private void SendOffComparatorOutput()
        {
            byte[] data = cmd.OffComparator;
            com.Write(data, 0, 6);
        }

        bool isOnNTC = false;
        /// <summary>
        /// NTC状态切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgNTC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnNTC == false)
            {
                SendOnNTC();
                imgNTC.Source = new BitmapImage(new Uri("Resources/point_hover.png", UriKind.Relative));

                isOnNTC = !isOnNTC;
            }
            else
            {
                SendOffNTC();
                imgNTC.Source = new BitmapImage(new Uri("Resources/point_normal.png", UriKind.Relative));

                isOnNTC = !isOnNTC;
            }
        }

        bool isOnPressurePoint = false;
        /// <summary>
        /// 压力点状态切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgPressurePoint_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnPressurePoint == false)
            {
                SendOnPressurePoint();
                imgPressurePoint.Source = new BitmapImage(new Uri("Resources/point_hover.png", UriKind.Relative));

                isOnPressurePoint = !isOnPressurePoint;
            }
            else
            {
                SendOffPressurePoint();
                imgPressurePoint.Source = new BitmapImage(new Uri("Resources/point_normal.png", UriKind.Relative));

                isOnPressurePoint = !isOnPressurePoint;
            }
        }

        bool isOnComparatorOutput = false;
        /// <summary>
        /// 比较器输出状态切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgComparatorOutput_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnComparatorOutput == false)
            {
                SendOnComparatorOutput();
                imgComparatorOutput.Source = new BitmapImage(new Uri("Resources/point_hover.png", UriKind.Relative));

                isOnComparatorOutput = !isOnComparatorOutput;
            }
            else
            {
                SendOffComparatorOutput();
                imgComparatorOutput.Source = new BitmapImage(new Uri("Resources/point_normal.png", UriKind.Relative));

                isOnComparatorOutput = !isOnComparatorOutput;
            }
        }

        //风扇
        bool isOnFan = false;
        private void imgFan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnFan == false)
            {
                SendOnNTC();
                imgFan.Source = new BitmapImage(new Uri("Resources/action_airfan_on.gif", UriKind.Relative));

                isOnFan = !isOnFan;
            }
            else
            {
                SendOnNTC();
                imgFan.Source = new BitmapImage(new Uri("Resources/airfan_off.png", UriKind.Relative));

                isOnFan = !isOnFan;
            }
        }

        //加热器
        bool isOnHeater = false;
        private void imgHeater_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnHeater == false)
            {
                SendOnNTC();
                imgHeater.Source = new BitmapImage(new Uri("Resources/warm_on.png", UriKind.Relative));

                isOnHeater = !isOnHeater;
            }
            else
            {
                SendOnNTC();
                imgHeater.Source = new BitmapImage(new Uri("Resources/warm_off.png", UriKind.Relative));

                isOnHeater = !isOnHeater;
            }
        }

        //比较器
        bool isOnComparator = false;
        private void imgComparatorOutputValue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isOnComparator == false)
            {
                SendOnNTC();
                imgComparatorOutputValue.Source = new BitmapImage(new Uri("Resources/comparer_1.png", UriKind.Relative));

                isOnComparator = !isOnComparator;
            }
            else
            {
                SendOnNTC();
                imgComparatorOutputValue.Source = new BitmapImage(new Uri("Resources/comparer_0.png", UriKind.Relative));

                isOnComparator = !isOnComparator;
            }
        }
    }
}
