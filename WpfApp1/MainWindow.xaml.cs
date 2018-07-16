using System;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;

namespace WpfApp1
{
    /// <summary>
    ///     MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();


            _notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = Title
            };
            _notifyIcon.DoubleClick += (s, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
            };

//            _notifyIcon.ShowBalloonTip(1, "Hello World", "Description message", ToolTipIcon.Error);
        }

        private void OnMinClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnMaxClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void OnNormalClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            _notifyIcon.ShowBalloonTip(3, "테스트", "닫기", ToolTipIcon.Info);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            base.OnStateChanged(e);
        }

        private void OnNotifyClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var message = $"테스트 {button?.Content}";

            var icon = ToolTipIcon.None;
            if (button?.Content?.ToString() == "오류")
            {
                icon = ToolTipIcon.Error;
            }else if (button?.Content?.ToString() == "경고")
            {
                icon = ToolTipIcon.Warning;
            }
            else if (button?.Content?.ToString() == "정보")
            {
                icon = ToolTipIcon.Info;
            }

            _notifyIcon.ShowBalloonTip(3, "타이틀", message, icon);
        }
    }
}