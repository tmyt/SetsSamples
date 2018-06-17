using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Books;

namespace DwmGetUnmetTabRequirements
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT p);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BeginSelectTarget(object sender, MouseButtonEventArgs e)
        {
            ((Grid)sender).CaptureMouse();
        }

        private void EndSelectTarget(object sender, MouseButtonEventArgs e)
        {
            GetCursorPos(out var point);
            var hwnd = WindowFromPoint(point);
            GetInfo(hwnd);
            ((Grid)sender).ReleaseMouseCapture();
        }

        private void GetInfo(IntPtr hwnd)
        {
            var sb = new StringBuilder(260);
            var parent = hwnd;
            while (parent != IntPtr.Zero)
            {
                hwnd = parent;
                parent = GetParent(hwnd);
            }
            GetWindowText(hwnd, sb, 255);
            DwmInterop.DwmGetUnmetTabRequirements(hwnd, out var requirements);
            var results = Enum.GetValues(typeof(DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS))
                .Cast<DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS>()
                .Where(e => (requirements & e) == e)
                .Select(e => new
                {
                    Label = e.ToString(),
                    Description = DwmMessages.Messages[e]
                });
            Results.ItemsSource = Enumerable.Range(0, 1).Select(_ => new
            {
                Label = "WindowName",
                Description = sb.ToString()
            }).Concat(results);
        }
    }
}
