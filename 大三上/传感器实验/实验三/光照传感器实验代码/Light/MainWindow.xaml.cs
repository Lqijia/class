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
using System.Threading;
using System.Windows.Threading;

using Interfaces;
using Newlab.Helper;
using Light.Uc;


namespace Light
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 传感器操作实体
        /// </summary>
        /// <value>The model.</value>
        public UcMainViewModel Model { get; set; }

        /// <summary>
        /// 命令板亮图片
        /// </summary>
        private ImageSource _pointHoverImage = new BitmapImage(new Uri("Images/point_hover.png", UriKind.Relative));
        /// <summary>
        /// 命令板暗图片
        /// </summary>
        private ImageSource _pointNormalImage = new BitmapImage(new Uri("Images/point_normal.png", UriKind.Relative));
        private bool isOpenSensoLamp = false;

        public MainWindow()
        {
            InitializeComponent();

            //初始化
            this.Model = new UcMainViewModel();
            //绑定窗体上的数据控件
            this.DataContext = this.Model;
            this.Model.NotifyLight += new NotifyLightValueEventHandler(Model_NotifyLight);
            //this.OpenPort();
            //this.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
        }

        /// <summary>
        /// 打开串口并且验证串口和板
        /// </summary>
        private void OpenPort()
        {
            int times = 0;
            //尝试三次打开，验证串口是否打开成功
            while (times < 3 && !this.Model.IsOpenPortSuccess)
            {
                this.Model.Open();
                times++;
                if (!this.Model.IsOpenPortSuccess)
                {
                    //关闭串口
                    this.Model.Close();
                    Thread.Sleep(100);
                }
            }
        }

        /// <summary>
        /// 当Dispatcher开始关闭时执行串口关闭方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            if (this.Model != null)
            {
                this.Model.Close();
            }
        }

        /// <summary>
        /// 返回的数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Model_NotifyLight(object sender, LightEventArgs args)
        {
            try
            {
                this.tbResistance.Dispatcher.Invoke(new Action(() => { this.tbResistance.Text = args.Resistance.ToString("f2") + "KΩ"; }));
                this.tbIll.Dispatcher.Invoke(new Action(() => { this.tbIll.Text = args.Illumination.ToString("f2") + "Lux"; }));
                this.isOpenLampImage.Dispatcher.Invoke(new Action(() =>
                {
                    var imageBrush = new ImageBrush();

                    if (args.IsOpenLamp)
                    {
                        this.isOpenLampImage.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                        imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Light.Uc;component/Uc/Images/bg_light.png", UriKind.Absolute));
                    }
                    else
                    {
                        this.isOpenLampImage.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                        imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Light.Uc;component/Uc/Images/bg_dark.png", UriKind.Absolute));
                    }
                    this.mainGrid.Background = imageBrush;
                }));

                this.tbAd1.Dispatcher.Invoke(new Action(() => { this.tbAd1.Text = args.SensorAd.ToString("f0"); }));
                this.tbAd2.Dispatcher.Invoke(new Action(() => { this.tbAd2.Text = args.PotentiometerAd.ToString("f0"); }));

                double maxSensorAd = 255;
                double sensorAdDiv = args.SensorAd / maxSensorAd;
                double sensorAdPer = Convert.ToDouble((sensorAdDiv >= 1 ? 100 : sensorAdDiv * 100));
                Dispatcher.Invoke(new Action<DependencyProperty, object>(sensorAdProgressBar.SetValue), DispatcherPriority.Background, ProgressBar.ValueProperty, sensorAdPer/*进度百分比*/);

                double maxPotentiometerAd = 255;
                double potentiometerAdDiv = args.PotentiometerAd / maxPotentiometerAd;
                double potentiometerAdPer = Convert.ToDouble((potentiometerAdDiv >= 1 ? 100 : potentiometerAdDiv * 100));
                Dispatcher.Invoke(new Action<DependencyProperty, object>(potentiometerAdProgressBar.SetValue), DispatcherPriority.Background, ProgressBar.ValueProperty, potentiometerAdPer/*进度百分比*/);
            }
            catch (Exception ex)
            {
                var tp = ex.ToString();
            }
        }

        /// <summary>
        /// 开/关传感器灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSensorLamp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ImageSource image;
                if (!isOpenSensoLamp)
                {
                    if (this.Model.SensorLamp(LampStatus.Open))
                    {
                        image = _pointHoverImage;
                        isOpenSensoLamp = !isOpenSensoLamp;
                        imgSensorLamp.Source = image;

                    }

                }
                else
                {
                    if (this.Model.SensorLamp(LampStatus.Close))
                    {
                        image = _pointNormalImage;
                        isOpenSensoLamp = !isOpenSensoLamp;
                        imgSensorLamp.Source = image;

                    }

                }
            }
            catch (Exception ex)
            {
                var temp = ex.ToString();
            }
        }

        private bool isDigitalOutputLamp = false;
        /// <summary>
        /// 开/关数字量输出灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgDigitalOutputLamp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isDigitalOutputLamp)
            {
                if (this.Model.DigitalOutputLamp(LampStatus.Open))
                {
                    imgDigitalOutputLamp.Source = new BitmapImage(new Uri("Images/point_hover.png", UriKind.Relative));
                    isDigitalOutputLamp = !isDigitalOutputLamp;
                }
            
            }
            else
            {
                if (this.Model.DigitalOutputLamp(LampStatus.Close))
                {
                    imgDigitalOutputLamp.Source = new BitmapImage(new Uri("Images/point_normal.png", UriKind.Relative));
                    isDigitalOutputLamp = !isDigitalOutputLamp;
                }
              
            }
        }

        private bool isPotentiometerLamp;
        /// <summary>
        ///开/关 电位器灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgPotentiometerLamp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isPotentiometerLamp)
            {
                if (this.Model.PotentiometerLamp(LampStatus.Open))
                {
                    imgPotentiometerLamp.Source = new BitmapImage(new Uri("Images/point_hover.png", UriKind.Relative));
                    isPotentiometerLamp = !isPotentiometerLamp;
                }

            }
            else
            {
                if (this.Model.PotentiometerLamp(LampStatus.Close))
                {
                    imgPotentiometerLamp.Source = new BitmapImage(new Uri("Images/point_normal.png", UriKind.Relative));
                    isPotentiometerLamp = !isPotentiometerLamp;
                }
              
            }
        }


        private bool isAnalogOutput;
        /// <summary>
        /// 开/关模拟量输出灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgAnalogOutputLamp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isAnalogOutput)
            {
                //if (this.Model.AnalogOutputLamp(LampStatus.Open))
                {
                    imgAnalogOutputLamp.Source = new BitmapImage(new Uri("Images/point_hover.png", UriKind.Relative));
                    isAnalogOutput = !isAnalogOutput;
                }

            }
            else
            {
                //if (this.Model.AnalogOutputLamp(LampStatus.Close))
                {
                    imgAnalogOutputLamp.Source = new BitmapImage(new Uri("Images/point_normal.png", UriKind.Relative));
                    isAnalogOutput = !isAnalogOutput;
                }

            }
        }

        //2017494095 李启佳 
        bool opened = false;
        private void ComparatorClinkEvent(object sender, MouseButtonEventArgs e)
        {
            var imageBrush = new ImageBrush();
            if (opened)
            {
                this.isOpenLampImage.Source = new BitmapImage(new Uri("Images/switch_0.png", UriKind.Relative));
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Light.Uc;component/Uc/Images/bg_light.png", UriKind.Absolute));
            }
            else
            {
                this.isOpenLampImage.Source = new BitmapImage(new Uri("Images/switch_1.png", UriKind.Relative));
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Light.Uc;component/Uc/Images/bg_dark.png", UriKind.Absolute));
            }
            opened = !opened;
            this.mainGrid.Background = imageBrush;

        }
    }
}
