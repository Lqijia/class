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
using System.Windows.Media.Animation;
using System.Timers;

using InfraredElecLibrary;


namespace InfraredElec
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 串口辅助类
        /// </summary>
        static ComHelper com = null;
        /// <summary>
        /// 是否第一次
        /// </summary>
        bool _isfirst = true;
        /// <summary>
        /// 计时器
        /// </summary>
        static Timer timer;
        /// <summary>
        /// 动画容器A点，B点，驶出动画，驶入动画
        /// </summary>
        Storyboard sbA, sbB, sbOut, sbIn;

        /// <summary>
        /// 是否停止 :A点，B点，驶出动画，驶入动画
        /// </summary>
        bool isstopA = true, isstopB = true, isstopOut = true, isstopIn = true;
        /// <summary>
        /// 是否暂停
        /// </summary>
        static bool isprush = false;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (com != null)
            //    return;
            ////注意串口要与具体环境对应
            //string strCom = System.Configuration.ConfigurationManager.AppSettings["SName"];
            //int intBaud = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SBaud"]);
            //com = new ComHelper(strCom, intBaud);
            //if (!com.Open())
            //{
            //    return;
            //}

            sbA = (Storyboard)FindResource("sb_A");
            sbB = (Storyboard)FindResource("sb_B");
            sbOut = (Storyboard)FindResource("sb_Out");
            sbIn = (Storyboard)FindResource("sb_In");
            sbA.Completed += sb_Completed;
            sbB.Completed += sb_Completed;
            sbOut.Completed += sb_Completed;
            sbIn.Completed += sb_Completed;
            timer = new Timer(200);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
        /// <summary>
        /// 窗体卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (com != null)
                com.Close();
            timer.Stop();
        }
        /// <summary>
        /// 动画完成时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sb_Completed(object sender, EventArgs e)
        {
            var sbname = ((sender as ClockGroup).Timeline as Storyboard).Name;
           
            switch (sbname)
            {
                case "sb_A":
                    isstopA = true;
                    break;
                case "sb_B":
                    isstopB = true;
                    break;
                case "sb_Out":
                    isstopOut = true;
                    break;
                case "sb_In":
                    isstopIn = true;
                    break;
            }
        }
        /// <summary>
        /// 计时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ////防止未执行以下代码重新计时
            //var tt = sender as Timer;
            //tt.Stop();
            //if (_isfirst)
            //{
            //    _isfirst = false;
            //    com.Init(0x33);
            //}

            //var bts14 = com.GetD14Data(0x33);
            //if (bts14 != null)
            //    StardControl(bts14[0]);
            ////0x40 读取ADC数据
            //var ad0 = com.GetRetData(0x33, 0x40, 0x00);
            //if (ad0 != null)
            //{
            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        var i0 = (int)ad0[0];
            //        pro_ad1.Value = i0 / 2.55;
            //        lbad1.Content = i0.ToString();
            //    }));
            //}
            ////0x40 读取ADC数据
            //var ad1 = com.GetRetData(0x33, 0x40, 0x01);
            //if (ad1 != null)
            //{
            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        var i1 = (int)ad1[0];
            //        pro_ad2.Value = i1 / 2.55;
            //        lbad2.Content = (i1).ToString();
            //    }));
            //}

            //if (!isprush)
            //    tt.Start();//启动监听

        }

        /// <summary>
        /// 根据数值执行UI动画效果
        /// </summary>
        /// <param name="bt">数值</param>
        void StardControl(byte bt)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                //img_B.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                //img_A.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                //img_Out.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                //img_In.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
            }));
            if ((bt & 0x01) == 0)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_B");

                    StardSb(sb);
                }));
            }
            else if ((bt & 0x02) == 0x00)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_A");
                    StardSb(sb);
                }));
            }
            else if ((bt & 0x04) == 0x04)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_Out");
                    StardSb(sb);
                }));
            }
            else if ((bt & 0x08) == 0x08)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_In");
                    StardSb(sb);
                }));
            }
        }
        /// <summary>
        /// 动画执行方法
        /// </summary>
        /// <param name="sb"></param>
        void StardSb(Storyboard sb)
        {

            switch (sb.Name)
            {
                case "sb_A":
                    sbB.Stop();
                    sbOut.Stop();
                    sbIn.Stop();
                    if (isstopA)
                    {
                        sb.Begin(brd);
                        isstopA = false;
                    }

                    img_A.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                    break;
                case "sb_B":
                    sbA.Stop();
                    sbOut.Stop();
                    sbIn.Stop();
                    if (isstopB)
                    {
                        sb.Begin(brd);
                        isstopB = false;
                    }

                    img_B.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                    break;
                case "sb_Out":
                    sbA.Stop();
                    sbB.Stop();
                    sbIn.Stop();
                    if (isstopOut)
                    {
                        sb.Begin(brd);
                        isstopOut = false;
                    }
                    img_Out.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                    break;
                case "sb_In":
                    sbA.Stop();
                    sbB.Stop();
                    sbOut.Stop();
                    if (isstopIn)
                    {
                        sb.Begin(brd);
                        isstopIn = false;
                    }
                    img_In.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                    break;
            }
        }


        bool inchange = false;
        private void OnEntranceButtonClickEvent(object sender, MouseButtonEventArgs e)
        {
            if(inchange == false)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_In");
                }
                ));
                img_In.Source=new BitmapImage(new Uri("Images/switch_1.png",UriKind.Relative));
                StardControl(0x0B);
                inchange = !inchange;
            }
            else
            {
                img_In.Source=new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                inchange = !inchange;
            }
        }


        bool outchange = false;
        private void OnExitButtonClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (outchange == false)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_Out");
                }
                ));
                img_Out.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                StardControl(0x07);
                outchange = !outchange;
            }
            else
            {
                img_Out.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                outchange = !outchange;
            }
        }


        bool achange = false;
        private void OnStationAButtonClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (achange == false)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_A");
                }
                ));
                img_A.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                StardControl(0x01);
                achange = !achange;
            }
            else
            {
                img_A.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                achange = !achange;
            }
        }

        bool bchange = false;
        private void OnStationBButtonClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (bchange == false)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard sb = (Storyboard)FindResource("sb_B");
                }
                ));
                img_B.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                StardControl(0x00);
                bchange = !bchange;
            }
            else
            {
                img_B.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                bchange = !bchange;
            }
        }
    }
}
