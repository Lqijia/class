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
using System.ComponentModel;

using OxyPlot;//绘图
using OxyPlot.Axes;
using OxyPlot.Series;
using ZigbeeExperiment2.Uc;//Zigbee数据操作


namespace ZigbeeSecondExperiment
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Zigbeer操作方法
        /// </summary>
        private MainViewModel _viewModel;
        /// <summary>
        /// 图像集合
        /// </summary>
        private IList<BitmapImage> _fanImagesList = new List<BitmapImage>();
        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;

            //打开串口
            _viewModel.OpenPort();

            InitFanImageTimer();
            //提供给本应用使用
            DependencyPropertyDescriptor fanProp = DependencyPropertyDescriptor.FromProperty(MainViewModel.IsOpenFanProperty, typeof(MainViewModel));
            //使其他对象在此属性更改时能得到通知
            fanProp.AddValueChanged(_viewModel, new EventHandler((obj, arg) =>
            {

                bool isOpenFan = false;

                var fanValue = fanProp.GetValue(_viewModel);
                if (fanValue == null)
                    return;
                //判断返回的值是否是整数
                bool.TryParse(fanValue.ToString(), out isOpenFan);

                if (isOpenFan)
                {
                    _timer.Start();//启动计时器
                }
                else
                {
                    _timer.Stop();
                }
            }));

        }

        /// <summary>
        /// 初始化风扇定时器
        /// </summary>
        private void InitFanImageTimer()
        {
            for (int i = 1; i <= 6; i++)
            {
               //添加图像集合
                var bitImage = new BitmapImage(new Uri(string.Format
                ("Images/fan_{0}.png", i), UriKind.Relative));
                _fanImagesList.Add(bitImage);
            }

            _timer.Interval = TimeSpan.FromMilliseconds(180);
            _timer.Tick += (obj, args) =>
            {
                //获取风扇图像
                var currentSource = imageFan.Source;

                var currentUriString = currentSource.ToString();

                //循环显示风扇
                for (int i = 0; i < _fanImagesList.Count; i++)
                {
                    var indexUriString = _fanImagesList[i].UriSource.OriginalString;
                    if (indexUriString == currentUriString || currentUriString.Contains(indexUriString))
                    {
                        int nextIndex = (i + 1) % _fanImagesList.Count;
                        imageFan.Source = _fanImagesList[nextIndex];
                        break;
                    }
                }
            };
        }

      
        /// <summary>
        /// 释放主方法
        /// </summary>
        public void Dispose()
        {
            if (_viewModel != null && _viewModel.IsOpenPort)
            {
                _viewModel.ClosePort();
            }

        }

        /// <summary>
        /// 窗体卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Dispose();
        }

    }
}
