using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Books;

namespace SetsSample.WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnableSets(object sender, RoutedEventArgs e)
        {
            int value = 1;
            DwmInterop.DwmSetWindowAttribute(new WindowInteropHelper(this).Handle,
                DwmInterop.DWMWA_TABBING_ENABLED, ref value, Marshal.SizeOf<int>());
        }

        private void DisableSets(object sender, RoutedEventArgs e)
        {
            int value = 0;
            DwmInterop.DwmSetWindowAttribute(new WindowInteropHelper(this).Handle,
                DwmInterop.DWMWA_TABBING_ENABLED, ref value, Marshal.SizeOf<int>());
        }

        private void OpenTabDisabled(object sender, RoutedEventArgs e)
        {
            new Window1(false, 0, this).Show();
        }

        private void OpenDefault(object sender, RoutedEventArgs e)
        {
            new Window1(true, DwmInterop.DWMTGP_DEFAULT, this).Show();
        }

        private void OpenTab(object sender, RoutedEventArgs e)
        {
            new Window1(true, DwmInterop.DWMTGP_TAB_WITH_ASSOCIATED_WINDOW, this).Show();
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            new Window1(true, DwmInterop.DWMTGP_NEW_TAB_GROUP, this).Show();
        }
    }
}
