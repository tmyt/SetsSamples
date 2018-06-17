using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace SetsSample.UWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ViewGrouping PreferredMode => Preference.SelectedIndex >= 0
           ? (ViewGrouping)Preference.SelectedIndex
           : ViewGrouping.Default;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void LaunchUri(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://google.com/"), new LauncherOptions
            {
                GroupingPreference = PreferredMode,
            });
        }

        private async void LaunchFile(object sender, RoutedEventArgs e)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync("testfile.txt", CreationCollisionOption.ReplaceExisting);
            // Fill file data
            var writer = await file.OpenTransactedWriteAsync(StorageOpenOptions.None);
            await writer.Stream.WriteAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString()).AsBuffer());
            await writer.CommitAsync();
            writer.Dispose();
            // launch
            Launcher.LaunchFileAsync(file, new LauncherOptions
            {
                GroupingPreference = PreferredMode,
            });
        }

        private void LaunchFolder(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder, new FolderLauncherOptions
            {
                GroupingPreference = PreferredMode,
            });
        }

        private async void OpenView(object sender, RoutedEventArgs e)
        {
            var grouping = PreferredMode;
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                Window.Current.Content = new Frame();
                ((Frame)Window.Current.Content).Navigate(typeof(BlankPage1));
                Window.Current.Activate();
                var viewModePreferences = ViewModePreferences.CreateDefault(ApplicationViewMode.Default);
                viewModePreferences.GroupingPreference = grouping;
                await ApplicationViewSwitcher.TryShowAsViewModeAsync(
                    ApplicationView.GetApplicationViewIdForWindow(Window.Current.CoreWindow),
                    ApplicationViewMode.Default,
                    viewModePreferences);
            });
        }
    }
}
