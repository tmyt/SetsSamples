using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Books;

namespace SetsSample.WPF
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly bool _tabEnabled;
        private readonly int _preferred;
        private readonly Window _parent;

        public Window1(bool tabEnabled, int preferred, Window parent)
        {
            _tabEnabled = tabEnabled;
            _preferred = preferred;
            _parent = parent;
            InitializeComponent();
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            var value = _tabEnabled ? 1 : 0;
            var preferred = _preferred;
            var handle = new WindowInteropHelper(this).Handle;
            var parent = new WindowInteropHelper(_parent).Handle;
            DwmInterop.DwmSetWindowAttribute(handle,
                DwmInterop.DWMWA_TABBING_ENABLED, ref value, Marshal.SizeOf<int>());
            DwmInterop.DwmSetWindowAttribute(handle,
                DwmInterop.DWMWA_TAB_GROUPING_PREFERENCE, ref preferred, Marshal.SizeOf<int>());
            DwmInterop.DwmSetWindowAttribute(handle,
                DwmInterop.DWMWA_ASSOCIATED_WINDOW, ref parent, IntPtr.Size);
        }
    }
}
